using System;
using System.Collections.Generic;

namespace TSPProject.Extensions
{
    public static class CoordExtensions
    {
        public static void Write(this List<Coord> coords)
        {
            foreach(Coord coord in coords)
            {
                Console.WriteLine(coord);
            }
        }

        public static double CalcDistance(this Coord coord, Coord otherCoord, bool isGeoType = false)
        {
            if(!isGeoType)
            {
                var diffX = coord.X - otherCoord.X;
                var diffY = coord.Y - otherCoord.Y;

                return Math.Sqrt((diffX * diffX) + (diffY * diffY));
            }

            //TODO: implement CalcDistance for geoType
            return 0;
        }
    }
}
