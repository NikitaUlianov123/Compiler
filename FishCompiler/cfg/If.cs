using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class If
    {
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            if (tokens[index].classification == Classification.keyword && tokens[index].lexime == "if")
            {
                Node tree = new Node(("if", Classification.conditional));
                index++;

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

                            if (tokens[index].classification == Classification.bracket && tokens[index].lexime == "{")
                            {
                                tree.children.Add(new Node(tokens[index], tree));
                                index++;
                                
                                var result2 = Code.Parse(tokens, ref index);
                                if (result.successful)
                                {
                                    tree.children.Add(result2.result);

                                    if (tokens[index].classification == Classification.bracket && tokens[index].lexime == "}")
                                    {
                                        tree.children.Add(new Node(tokens[index], tree));
                                        index++;

                                        return (true, tree);
                                    }
                                }

                                index--;
                                tree.children.RemoveAt(3);
                            }

                            index--;
                            tree.children.RemoveAt(2);
                        }

                        tree.children.RemoveAt(1);
                    }

                    index--;
                    tree.children.RemoveAt(0);
                }

                index--;
                return (false, tree);
            }
            else
            {
                return (false, new Node());
            }
        }
    }
}
