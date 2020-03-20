using System;
using System.Collections.Generic;
using System.Text;

namespace TSPProject.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            var rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
