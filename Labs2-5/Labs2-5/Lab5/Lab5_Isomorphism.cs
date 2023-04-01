using System;
using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;
using Labs_DM.Shared;

namespace Labs_DM.Lab5
{
    public class Lab5_Isomorphism
    {
        public static void Lab5_Execute()
        {
            List<List<int>> graph1 = new List<List<int>>
            {
                new List<int>{ 0, 0, 1, 0, 1},
                new List<int>{ 0, 0, 1, 1, 0},
                new List<int>{ 1, 1, 0, 0, 0},
                new List<int>{ 0, 1, 0, 0, 1},
                new List<int>{ 1, 0, 0, 1, 0}
            };

            char[] headers = StringHelper.GetNames(graph1.Count());

            List<List<int>> graph2 = new List<List<int>>
            {
                new List<int>{ 0, 0, 1, 1, 0},
                new List<int>{ 0, 0, 1, 0, 1},
                new List<int>{ 1, 1, 0, 0, 0},
                new List<int>{ 1, 0, 0, 0, 1},
                new List<int>{ 0, 1, 0, 1, 0}
            };

            Console.WriteLine("Graph 1");
            graph1.Display(headers);
            Console.WriteLine("\nGraph 2");
            graph2.Display(headers);
            Console.WriteLine($"Isomorphic? {GraphIsomporphism(graph1, graph2)}");

            List<List<int>> graph3 = new List<List<int>>
            {
                new List<int>{ 0, 0, 0, 0, 1},
                new List<int>{ 0, 0, 1, 1, 0},
                new List<int>{ 0, 1, 0, 0, 0},
                new List<int>{ 0, 1, 0, 0, 1},
                new List<int>{ 1, 0, 0, 1, 0}
            };

            Console.WriteLine("\nGraph 1");
            graph1.Display(headers);
            Console.WriteLine("\nGraph 3");
            graph3.Display(headers);
            Console.WriteLine($"Isomorphic? {GraphIsomporphism(graph1, graph3)}");
        }

        public static bool GraphIsomporphism(List<List<int>> graph1, List<List<int>> graph2)
        {
            List<int> edges1 = GetEdgesCount(graph1);
            List<int> edges2 = GetEdgesCount(graph2);
            bool result = false;
            if (edges1.Count == edges2.Count)
            {
                Permutations<int> availableVariants = new Permutations<int>(edges1,
                    GenerateOption.WithoutRepetition);

                result = availableVariants.Any(x => IsGraphsSimilar(x.ToList(), edges2));
            }

            return result;
        }

        public static List<int> GetEdgesCount(List<List<int>> graph)
        {
            return graph.Select(x => x.Count(y => y != 0)).ToList();
        }

        public static bool IsGraphsSimilar(List<int> edges1, List<int> edges2)
        {
            for (int i = 0; i < edges1.Count; i++)
            {
                if (edges1[i] != edges2[i])
                    return false;
            }

            return true;
        }
    }
}