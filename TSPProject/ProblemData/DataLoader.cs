using System;
using System.Collections.Generic;
using System.IO;
using TSPProject.Extensions;

namespace TSPProject
{
    public static class DataLoader
    {
        public static bool isGeoType = false;

        public static double[,] GenerateAdjacencyMatrix(string filePath)
        {
            List<Coord> coords = LoadCoordsFromFile(filePath);
            double[,] adjacencyMatrix = new double[coords.Count, coords.Count];

            for(int i = 0; i < coords.Count; i++)
            {
                for(int j = 0; j < coords.Count; j++)
                {
                    var val = coords[i].CalcDistance(coords[j]);
                    adjacencyMatrix[i, j] = val;
                }
            }

            return adjacencyMatrix;
        }

        public static List<Coord> LoadCoordsFromFile(string filePath)
        {
            List<Coord> coords = new List<Coord>();
            isGeoType = false;

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Cannot find file of path: " + filePath);
            
            using (StreamReader sr = File.OpenText(filePath))
            {
                string line = "";
                bool isSectionPresent = false;

                while ((line = sr.ReadLine()) != null)
                {
                    if(line.StartsWith("EDGE_WEIGHT_TYPE"))
                    {
                        isSectionPresent = true;
                        break;
                    }
                }

                if (line.Contains("GEO"))
                    isGeoType = true;

                while ((line = sr.ReadLine()) != null) 
                { 
                    if (line.StartsWith("NODE_COORD_SECTION"))
                    {
                        isSectionPresent = true;
                        break;
                    }
                }
                
                if (!isSectionPresent)
                    return null;
                    
                while ((line = sr.ReadLine()) != null && !line.StartsWith("EOF"))
                {
                    coords.Add(Coord.ConvertStringToCoord(line, ' '));
                }
            }

            return coords; 
        }
    }
}
