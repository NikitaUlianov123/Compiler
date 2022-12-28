using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class E1
    {
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("E`", Classification.parser));
            var result = E2.Parse(tokens, ref index);
            if (result.successful)
            {
                tree.children.Add(result.result);
                result.result.Parent = tree;
                if (index < tokens.Count - 1)
                {
                    if (tokens[index].classification == Classification.Operator && tokens[index].lexime == "*")
                    {
                        tree.children.Add(new Node(tokens[index], tree));
                        index++;

                        var result2 = E1.Parse(tokens, ref index);
                        if (result2.successful)
                        {
                            tree.children.Add(result2.result);
                            result2.result.Parent = tree;
                            return (true, tree);
                        }

                        index--;
                        tree.children.RemoveAt(1);
                    }
                    else if (tokens[index].classification == Classification.Operator && tokens[index].lexime == "/")
                    {
                        tree.children.Add(new Node(tokens[index], tree));
                        index++;

                        var result2 = E.Parse(tokens, ref index);
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
                }
                else
                {
                    return (true, tree);
                }
            }

            return (false, tree);
        }
    }
}
