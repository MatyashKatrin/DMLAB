using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs_DM.Shared
{
    public static class FileHelper
    {
        public static int[][] ReadFromFile(string fileName, bool skipFisrt = false)
        {
            string[] lines = File.ReadAllLines($"../../../{fileName}");
            int[][] array = (skipFisrt
                    ? lines.Skip(1)
                    : lines)
                .Select(l => l.Trim().Split(' ').Select(i => int.Parse(i)).ToArray())
                .ToArray();

            return array;
        }
    }

    public static class IEnumerableExtension
    {
        public static T Random<T>(this IEnumerable<T> list)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, list.Count());
            return list.ElementAt(index);
        }
    }
}
