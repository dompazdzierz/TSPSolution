using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TSPProject
{
    public class Coord
    {
        public Coord(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static Coord ConvertStringToCoord(string line, char separator)
        {
            List<string> values = line.Split(separator).ToList();
            float x, y;

            if (!float.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out x))
            {
                return null;
            }

            if (!float.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out y))
            {
                return null;
            }

            return new Coord(x, y);
        }

        public override string ToString()
        {
            return X + " " + Y;
        }
    }
}
