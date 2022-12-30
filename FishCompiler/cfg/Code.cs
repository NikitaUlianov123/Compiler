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
        public static (bool successful, Node result) Parse(List<(string lexime, Classification classification)> tokens, ref int index, bool subCode)
        {
            Node tree = new Node(("Code Block", Classification.parser));

            int line = index;

            bool inScope = false;


            while (line < tokens.Count)
            {
                List<(string lexime, Classification classification)> current = new List<(string lexime, Classification classification)>();
                for (; line < tokens.Count && (inScope ? tokens[line].lexime != "}" : tokens[line].lexime != ";"); line++)
                {
                    current.Add(tokens[line]);
                    if (tokens[line].lexime == "{")
                    {
                        inScope = true;
                    }
                }
                if (inScope && line < tokens.Count - 1 && tokens[line + 1].lexime == "}")
                {
                    current.Add(tokens[line]);
                    current.Add(tokens[line + 1]);
                }
                if (line < tokens.Count && tokens[line].lexime == "}")
                {
                    if (line == tokens.Count - 1 && subCode)
                    {
                        index = line;
                        return (true, tree);
                    }
                    current.Add(tokens[line]);
                    inScope = false;
                }
                line++;


                int index1 = 0;
                var result1 = Assignment.Parse(current, ref index1);
                int index2 = 0;
                var result2 = If.Parse(current, ref index2);
                int index3 = 0;
                var result3 = E.Parse(current, ref index3);

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
                else if (result3.successful && index3 == current.Count)
                {
                    tree.children.Add(result3.result);
                    result3.result.Parent = tree;
                    current.Clear();
                    index += index3 + 1;
                }
                else if(line < tokens.Count)
                {
                    throw new Exception("Syntax error");
                }
            }

            return (true, tree);
        }
    }
}