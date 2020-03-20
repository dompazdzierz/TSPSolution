using System;
using System.IO;

namespace TSPProject
{
    class Problem
    { 
        public double[,] AdjacencyMatrix { get; }
        public string FileName { get; }

        public Problem(string fileName)
        {
            AdjacencyMatrix = LoadAdjacencyMatrix(fileName);
            FileName = fileName;
        }

        private double[,] LoadAdjacencyMatrix(string fileName)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDirectory, "DataFiles", fileName);
            return DataLoader.GenerateAdjacencyMatrix(filePath);
        }

        public void WriteAdjacencyMatrix()
        {
            for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < AdjacencyMatrix.GetLength(1); j++)
                {
                    Console.Write(AdjacencyMatrix[i, j] + "; ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
