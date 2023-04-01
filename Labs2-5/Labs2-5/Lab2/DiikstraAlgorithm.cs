using System.Collections.Generic;
using System.Linq;
using Labs_DM.Shared.Models;

namespace Labs_DM.Lab2
{
    public static class DiikstraExtension
    {
        public static Dictionary<char, AdditionalEdge> FindMinimalDistance(this Dictionary<char, List<Node>> nodes, char from)
        {
            int length = nodes.Count;
            List<char> usedNodes = new List<char>
            {
                from
            };
            Dictionary<char, AdditionalEdge> distances = new Dictionary<char, AdditionalEdge>
            {
                { from, new AdditionalEdge("", 0) }
            };


            for (int i = 0; i < length - 1; i++)
            {
                char minimalNodeChar = ' ';
                int minimalNodeDistance = int.MaxValue;
                string path = string.Empty;

                usedNodes.ForEach(x =>
                {
                    string temporaryPath = $"{x}";
                    Node node = nodes[x].Where(y => !usedNodes.Contains(y.Name)).OrderBy(y => y.Weight).FirstOrDefault();
                    if (node != null)
                    {
                        int minimalLength = node.Weight;
                        if (x != from)
                        {
                            temporaryPath = distances[x].Path;
                            minimalLength += distances[x].Length;
                        }

                        if (minimalNodeDistance > minimalLength)
                        {
                            minimalNodeDistance = minimalLength;
                            minimalNodeChar = node.Name;
                            path = temporaryPath;
                        }
                    }
                });

                path += minimalNodeChar;
                usedNodes.Add(minimalNodeChar);
                distances.Add(minimalNodeChar, new AdditionalEdge(path, minimalNodeDistance));
            }

            return distances;
        }
    }
}
