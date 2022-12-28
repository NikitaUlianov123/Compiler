using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class C
    {
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("C", Classification.parser));

            var result = C1.Parse(tokens, ref index);
            if (result.successful)
            {
                tree.children.Add(result.result);

                if (tokens[index].classification == Classification.comparison)
                {
                    tree.children.Add(new Node(tokens[index], tree));
                    index++;

                    var result2 = C1.Parse(tokens, ref index);
                    if (result2.successful)
                    {
                        tree.children.Add(result2.result);
                        result2.result.Parent = tree;
                        return (true, tree);
                    }

                    index--;
                    tree.children.RemoveAt(1);
                }
                else
                {
                    return (true, tree);
                }

                tree.children.RemoveAt(0);
            }

            return (false, tree);
        }
    }
}