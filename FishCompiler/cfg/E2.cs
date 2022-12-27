using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public class E2
    {
        private enum states
        {
            Parenthesis,
            ID,
            Num
        }

        private states state = 0;

        public (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("E``", Classification.parser));

            if (tokens[index].classification == Classification.bracket && tokens[index].lexime == "(")
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;

                var e = new E();
                var result = e.Parse(tokens, ref index);
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
            else if (tokens[index].classification == Classification.variableName)
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;
                return (true, tree);
            }
            else if (tokens[index].classification == Classification.number)
            {
                tree.children.Add(new Node(tokens[index], tree));
                index++;
                return (true, tree);
            }

            return (false, tree);
        }
    }
}
