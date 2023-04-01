using System;
using System.Collections.Generic;
using System.Linq;
using Labs_DM.Shared;
using Labs_DM.Shared.Models;

namespace Labs_DM.Lab3
{
    public class GraphWithSalesman: Graph<Node>
    {
        private List<Path> Branches = new List<Path>();
        private bool RandomPath = false;
        private Path BasePath => Branches.Where(x => x.IsBase).FirstOrDefault();
        public void FindSalesmanPath(char from)
        {
            if (!Nodes.ContainsKey(from))
            {
                throw new Exception("Node is not found");
            }

            Path bestBranch = GetBestPath(from);
            while (bestBranch == null)
            {
                bestBranch = GetBestPath(from);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Best path: {string.Join("-", bestBranch.UsedNodes)} ({bestBranch.GetLength(Nodes)})\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Path GetBestPath(char from)
        {

            Console.WriteLine($"Step 0\n{from}\n");

            for (int i = 0; i < Nodes.Count - 1; i++)
            {
                Console.WriteLine($"Step {i + 1}");
                if (Branches.Count() == 0)
                {
                    Branches = GetBranches(from.ToString(), true);
                }
                else
                {
                    Branches = Branches.SelectMany(x => GetBranches(string.Join("", x.UsedNodes), x.IsBase)).ToList();
                }
                FilterBracnes();
                if (Branches.Count == 0)
                {
                    Console.Clear();
                    return null;
                }

                Console.WriteLine($"\n");
            }

            Branches = Branches.Where(x => Nodes[x.Last].Any(x => x.Name == from)).ToList();
            if (Branches.Count == 0)
            {
                RandomPath = true;
                Console.Clear();
                return null;
            }

            Branches = Branches.Select(x => new Path(false, string.Join("", x.UsedNodes) + from)).ToList();

            return Branches.OrderBy(x => x.CalculateWeight(Nodes)).FirstOrDefault();
        }

        private void FilterBracnes()
        {
            if (Branches.Count != 0)
            {
                if (BasePath == null)
                {
                    Branches.Random().IsBase = true;
                }

                int basePathLength = BasePath.CalculateWeight(Nodes);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{string.Join("", BasePath.UsedNodes)} ({basePathLength}) ");
                Console.ForegroundColor = ConsoleColor.White;

                Branches = Branches.Where(x => x.IsBase || x.ComparePath(basePathLength, Nodes)).ToList();
            }
            else
            {
                RandomPath = true;
            }
        }

        private List<Path> GetBranches(string path, bool hasBase)
        {
            List<Node> nodes = Nodes[path.Last()].Where(x => !path.Contains(x.Name)).ToList();
            int i = 0;
            if (RandomPath)
            {
                Random rnd = new Random();
                i = rnd.Next(0, nodes.Count);
            }

            List<Path> branches = nodes.Select((x, index) => index == i
                ? new Path(isBase: hasBase, node: path + x.Name.ToString())
                : new Path(node: path + x.Name.ToString())).ToList();

            return branches;
        }

        public override void AfterMatrixFill() { }
    }

    public class Path
    {
        public bool IsBase { get; set; } = false;
        public List<char> UsedNodes { get; private set; } = new List<char>();
        public char Last => UsedNodes.Last();

        public Path(string node) {
            UsedNodes = node.ToList();
        }

        public Path(bool isBase, string node): this(node)
        {
            IsBase = isBase;
        }

        public int GetLength(Dictionary<char, List<Node>> nodes)
        {
            int weight = 0;
            for (int i = 0; i < UsedNodes.Count - 1; i++)
            {
                weight += nodes[UsedNodes[i]].FirstOrDefault(x => x.Name == UsedNodes[i + 1]).Weight;
            }

            return weight;
        }

        public int CalculateWeight(Dictionary<char, List<Node>> nodes)
        {
            int weight = 0;
            Dictionary<char, List<Node>> notUsed = nodes;
            for (int i = 0; i < UsedNodes.Count - 1; i++)
            {
                weight += nodes[UsedNodes[i]].FirstOrDefault(x => x.Name == UsedNodes[i + 1]).Weight;
                notUsed = notUsed
                    .Where(x => x.Key != UsedNodes[i])
                    .ToDictionary(i => i.Key, j => j.Value.Where(x => x.Name != UsedNodes[i + 1]).ToList());
            }

            Dictionary<char, int> minValues = notUsed
                .ToDictionary(i => i.Key, x => x.Value.Count > 0 ? x.Value.Min(i => i.Weight) : 0);
            weight += minValues.Sum(x => x.Value);
            Dictionary<char, List<Node>> transformed = notUsed
                .ToDictionary(i => i.Key, j => j.Value.Select(x => new Node 
                    { 
                        Name = x.Name, 
                        Weight = x.Weight - minValues[j.Key] 
                    })
                .ToList());

            int thirdStep = 0;
            IEnumerable<char> nodesForTransformed = nodes.Keys.Where(x => !UsedNodes.Contains(x));
            foreach (char node in nodesForTransformed)
            {
                IEnumerable<Node> column = transformed.SelectMany(x => x.Value.Where(i => i.Name == node));
                thirdStep += column.Count() > 0
                    ? column.Min(x => x.Weight)
                    : 0;
            }
            weight += thirdStep;

            return weight;
        }

        public bool ComparePath(int basePathLength, Dictionary<char, List<Node>> nodes)
        {
            int pathLength = CalculateWeight(nodes);
            if (pathLength > basePathLength)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write($"{string.Join("", UsedNodes)}:({pathLength}) ");
            Console.ForegroundColor = ConsoleColor.White;
            return pathLength <= basePathLength;
        }
    }
}
