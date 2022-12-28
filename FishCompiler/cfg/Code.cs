using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class Code
    {
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index)
        {
            Node tree = new Node(("E", Classification.parser));

            int line = index;
            int cry = index;


            while (line < tokens.Count)
            {
                List<(string lexime, Classification classification)> current = new List<(string lexime, Classification classification)>();
                for (; line < tokens.Count && (tokens[line].lexime != ";" && tokens[line].lexime != "}"); line++)
                {
                    current.Add(tokens[line]);
                }
                if (tokens[line].lexime == "}")
                {
                    current.Add(tokens[line]);
                }
                line++;


                int index1 = 0;
                var result1 = Assignment.Parse(current, ref index1);
                int index2 = 0;
                var result2 = If.Parse(current, ref index2);

                if (result1.successful && index1 == current.Count)
                {
                    tree.children.Add(result1.result);
                    result1.result.Parent = tree;
                    current.Clear();
                    index += index1 + 1;
                }
                else if (result2.successful && index2 == current.Count)
                {
                    tree.children.Add(result2.result);
                    result2.result.Parent = tree;
                    current.Clear();
                    index += index2 + 1;
                }
                else
                {
                    throw new Exception("Syntax error");
                }
            }

            return (true, tree);
        }
    }
}