using System;
using System.Collections.Generic;

namespace Labs_DM.Lab5
{
    public static class DisplayExtension
    {
        public static void Display(this List<List<int>> array, char[] headers)
        {
            if (headers.Length != 0 && headers.Length != array.Count)
            {
                throw new Exception("Not valid length of headers");
            }

            if (headers.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\t");

                for (int j = 0; j < headers.Length; j++)
                {
                    Console.Write($"{headers[j]}\t");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

            for (int i = 0; i < array.Count; i++)
            {
                if (headers.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{headers[i]}\t");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int j = 0; j < array[i].Count; j++)
                {
                    Console.Write($"{array[i][j]}\t");
                }

                Console.WriteLine();
            }
        }
    }
}
