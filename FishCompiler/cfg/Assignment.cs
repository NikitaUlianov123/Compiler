using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class Assignment
    {
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("assignment", Classification.parser));

            if (tokens[index].classification == Classification.variableType)
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;

                if (tokens[index].classification == Classification.variableName)
                {
                    tree.children.Add(new Node(tokens[index], tree));
                    index++;

                    if (tokens[index].lexime == "=")
                    {
                        tree.children.Add(new Node(tokens[index], tree));
                        index++;

                        var result = E.Parse(tokens, ref index);
                        if (result.successful)
                        {
                            tree.children.Add(result.result);
                            return (true, tree);
                        }

                        index--;
                        tree.children.RemoveAt(2);
                    }

                    index--;
                    tree.children.RemoveAt(1);
                }

                index--;
                tree.children.RemoveAt(0);
            }
            else if (tokens[index].classification == Classification.variableName)
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;

                if (tokens[index].lexime == "=")
                {
                    tree.children.Add(new Node(tokens[index], tree));
                    index++;

                    var result = E.Parse(tokens, ref index);
                    if (result.successful)
                    {
                        tree.children.Add(result.result);
                        return (true, tree);
                    }

                    index--;
                    tree.children.RemoveAt(2);
                }

                index--;
                tree.children.RemoveAt(0);
            }

            return (false, tree);
        }
    }
}