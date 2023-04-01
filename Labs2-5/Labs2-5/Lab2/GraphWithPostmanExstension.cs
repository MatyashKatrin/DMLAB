using System.Collections.Generic;
using System.Linq;
using Labs_DM.Shared.Models;

namespace Labs_DM.Lab2
{
    public static class GraphWithPostmanExstension
    {
        public static List<char> GetOddNodes(this Dictionary<char, List<Node>> nodes)
        {
            return nodes.Where(i => i.Value.Count % 2 != 0).Select(x => x.Key).ToList();
        }

        public static bool CheckIfRepeats(this IReadOnlyList<AdditionalEdge> edges)
        {
            foreach (AdditionalEdge edge in edges)
            {
                foreach (AdditionalEdge secondEdge in edges)
                {
                    if (edge.Path != secondEdge.Path
                        && edge.Intersect(secondEdge.Path))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static int GetSum(this IReadOnlyList<AdditionalEdge> edges)
        {
            return edges.Sum(x => x.Length);
        }
    }
}
