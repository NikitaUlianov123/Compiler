using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public class E
    {
        private enum states
        {
            Add,
            Subtract,
            EPrime
        }

        private states state = 0;

        public (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("E", Classification.parser));
            var ePrime = new E1();
            var result = ePrime.Parse(tokens, ref index);
            if (result.successful)
            {
                tree.children.Add(result.result);
                if (index < tokens.Count - 1)
                {
                    if (tokens[index].classification == Classification.Operator && tokens[index].lexime == "+")
                    {
                        index++;
                        tree.children.Add(new Node(("+", Classification.Operator)));

                        var e = new E();
                        var result2 = e.Parse(tokens, ref index);
                        if (result2.successful)
                        {
                            tree.children.Add(result2.result);
                            return (true, tree);
                        }

                        index--;
                        tree.children.RemoveAt(1);
                    }
                    else if (tokens[index].classification == Classification.Operator && tokens[index].lexime == "-")
                    {
                        index++;
                        tree.children.Add(new Node(("-", Classification.Operator)));

                        var e = new E();
                        var result2 = e.Parse(tokens, ref index);
                        if (result2.successful)
                        {
                            tree.children.Add(result2.result);
                            return (true, tree);
                        }

                        index--;
                        tree.children.RemoveAt(1);
                    }
                    else
                    {
                        return (false, tree);
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