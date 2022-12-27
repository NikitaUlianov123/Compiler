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
                index++;

                var e = new E();
                var result = e.Parse(tokens, ref index);
                if (result.successful)
                {
                    if (tokens[index].classification == Classification.bracket && tokens[index].lexime == ")")
                    {
                        index++;
                        return (true, tree);
                    }
                }

                index--;
            }
        }
    }
}
