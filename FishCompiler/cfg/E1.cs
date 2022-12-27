using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public class E1
    {
        private enum states
        {
            Multiply,
            Divide,
            EPrimePrime
        }

        private states state = 0;

        public (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("E`", Classification.parser));
            var eDouble = new E2();
            var result = eDouble.Parse(tokens, ref index);
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

                        var e = new E1();
                        var result2 = e.Parse(tokens, ref index);
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

                        var e = new E1();
                        var result2 = e.Parse(tokens, ref index);
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
