using System;
using System.Collections.Generic;
using System.Linq;
using Labs_DM.Shared.Models;

namespace Labs_DM.Lab4
{
    public class GraphWithChannels: Graph<Channel>
    {
        public char From { get; private set; }
        public char To { get; private set; }

        private int LastMaxFlow = 0;
        private int MaxFlow = 0;
        public override void AfterMatrixFill()
        {
            To = Nodes.FirstOrDefault(x => !x.Value.Any()).Key;
            From = Nodes.Keys.FirstOrDefault(x => !Nodes.Any(node => node.Value.Any(chanel => chanel.Name == x)));
        }

        public void FordFulkerson()
        {
            int i = 0;
            do
            {
                Console.WriteLine($"Iтерацiя: {++i}");
                LastMaxFlow = MaxFlow;
                FindFlow();
                MaxFlow = CalculateFlow();
                Console.WriteLine($"Пропускна можливiсть: {MaxFlow}"); 
                Console.WriteLine();
            }
            while (PathWasUpdated());
        }

        private void FindFlow()
        {
            Stack<char> usedNodes = new Stack<char>();
            Stack<char> badNodes = new Stack<char>();
            int min = int.MaxValue;
            usedNodes.Push(From);

            while (usedNodes.Peek() != To)
            {
                char current = usedNodes.Peek();
                Channel inNode = Nodes[current]
                    .Where(x => !usedNodes.Contains(x.Name)
                        && !badNodes.Contains(x.Name)
                        && x.RemainingWeight != 0)
                    .OrderByDescending(x => x.RemainingWeight)
                    .ThenBy(x => x.Name)
                    .FirstOrDefault();

                char? outNode = Nodes
                    .Where(x => x.Value.Any(t => t.Name == current))
                    .Where(x => !usedNodes.Contains(x.Key)
                        && !badNodes.Contains(x.Key)
                        && x.Value.First(t => t.Name == current).UsedWeight != 0)
                    .OrderByDescending(x => x.Value.First(t => t.Name == current).UsedWeight)
                    .ThenBy(x => x.Key)
                    .Select(x => x.Key)
                    .FirstOrDefault();
                
                if (outNode == default(char) && inNode == null)
                {
                    badNodes.Push(usedNodes.Peek());
                    usedNodes.Pop();
                }
                else
                {
                    if (outNode != default(char) && inNode != null)
                    {
                        int usedWeight = Nodes[outNode.Value].First(x => x.Name == current).UsedWeight;
                        if (inNode.RemainingWeight >= usedWeight)
                        {
                            usedNodes.Push(inNode.Name);
                            if (min > inNode.RemainingWeight)
                            {
                                min = inNode.RemainingWeight;
                            }
                        } else
                        {
                            usedNodes.Push(outNode.Value);
                            if (min > usedWeight)
                            {
                                min = usedWeight;
                            }
                        }
                    }
                    else if (outNode != default(char))
                    {
                        int usedWeight = Nodes[outNode.Value].First(x => x.Name == current).UsedWeight;
                        usedNodes.Push(outNode.Value);
                        if (min > usedWeight)
                        {
                            min = usedWeight;
                        }
                    }
                    else if (inNode != null)
                    {
                        usedNodes.Push(inNode.Name);
                        if (min > inNode.RemainingWeight)
                        {
                            min = inNode.RemainingWeight;
                        }
                    }
                }

                if (usedNodes.Peek() == From)
                {
                    Console.WriteLine("Кiнець роботи алгоритму");
                    return;
                }
            }

            UpdateNodes(usedNodes.Reverse().ToList(), min);
        }

        private void UpdateNodes(List<char> path, int min)
        {
            Console.Write($"{From} ");
            for (int i = 0; i < path.Count - 1; i++)
            {
                char from = path[i];
                char to = path[i + 1];
                if (Nodes.ContainsKey(from)
                    && Nodes[from].Any(x => x.Name == to))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Channel node = Nodes[from].First(x => x.Name == to);
                    node.UsedWeight += min;
                    Console.Write($"({node.UsedWeight}/{node.RemainingWeight}) {node.Name} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Channel node = Nodes[to].First(x => x.Name == from);
                    node.UsedWeight -= min;
                    Console.Write($"({node.UsedWeight}/{node.RemainingWeight}) {to} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.WriteLine();
        }

        private int CalculateFlow()
        {
            return Nodes.SelectMany(x => x.Value).Where(x => x.Name == To).Sum(x => x.UsedWeight);
        }

        private bool PathWasUpdated()
        {
            return MaxFlow != LastMaxFlow;
        }
    }

    public class Channel : Node
    {
        public int UsedWeight { get; set; } = 0;
        public int RemainingWeight => Weight - UsedWeight;
    }
}
