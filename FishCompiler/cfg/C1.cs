using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class C1
    {
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("C`", Classification.parser));

            if (tokens[index].classification == Classification.bracket && tokens[index].lexime == "(")
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;

                var result = C.Parse(tokens, ref index);
                if (result.successful)
                {
                    tree.children.Add(result.result);
                    result.result.Parent = tree;

                    if (tokens[index].classification == Classification.bracket && tokens[index].lexime == ")")
                    {
                        tree.children.Add(new Node(tokens[index], tree));
                        index++;
                        return (true, tree);
                    }

                    tree.children.RemoveAt(1);
                }

                index--;
                tree.children.RemoveAt(0);
            }
            else if (tokens[index].lexime == "!")
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;

                var result = C.Parse(tokens, ref index);
                if (result.successful)
                {
                    tree.children.Add(result.result);
                    return (true, tree);
                }

                index--;
                tree.children.RemoveAt(0);
            }
            else
            {
                var result = E.Parse(tokens, ref index);
                if (result.successful)
                {
                    tree.children.Add(result.result);
                    return (true, tree);
                }
            }

            return (false, tree);
        }
    }
}