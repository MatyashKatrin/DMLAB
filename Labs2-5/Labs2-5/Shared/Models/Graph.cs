using System.Collections.Generic;

namespace Labs_DM.Shared.Models
{
    public abstract class Graph<TNode> where TNode : Node, new()
    {
        public Dictionary<char, List<TNode>> Nodes = new Dictionary<char, List<TNode>>();
        public int Count => Nodes.Count;

        public void FillFromMatrix(char[] names, int[][] array)
        {
            for (int i = 0; i < names.Length; i++)
            {
                List<TNode> nodes = new List<TNode>();
                for (int j = 0; j < names.Length; j++)
                {
                    if (array[i][j] != 0)
                    {
                        nodes.Add(new TNode
                        {
                            Name = names[j],
                            Weight = array[i][j]
                        });
                    }
                }

                Nodes.Add(names[i], nodes);
            }
            AfterMatrixFill();
        }

        public abstract void AfterMatrixFill();
    }
}
