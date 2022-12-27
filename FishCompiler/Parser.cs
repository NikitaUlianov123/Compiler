using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public class Node
    {
        public (string lexime, Classification classification) token;

        public Node Parent;
        public List<Node> children;

        public Node()
        {
            Parent = new Node();
            children = new List<Node>();
        }
        public Node((string lexime, Classification classification) token)
        {
            Parent = new Node();
            children = new List<Node>();
            this.token = token;
        }
        public Node(Node parent)
        {
            Parent = parent;
            children = new List<Node>();
        }
    }

    public class Parser
    {
        void parse(List<(string lexime, Classification classification)> tokens)
        {
            
        }

        (bool successful, Node result) E(ref List<(string lexime, Classification classification)> tokens)
        {
            var e1 = E1(ref tokens);
            if (e1.successful)
            {
                if (tokens[0].classification == Classification.Operator && tokens[0].lexime == "+")
                {
                    (string lexime, Classification classification)[] rest = new (string lexime, Classification classification)[tokens.Count - 1];
                    tokens.CopyTo(1, rest, 0, tokens.Count - 1);
                    List<(string lexime, Classification classification)> rest2 = new List<(string lexime, Classification classification)>(rest);

                    var e = E(ref rest2);
                    if (e.successful)
                    {
                        if (rest2.Count == 0)
                        { 
                            
                        }
                    }
                }
            }
        }
        (bool successful, Node result) E1(ref List<(string lexime, Classification classification)> tokens)
        {
            if (tokens[0].classification == Classification.variableName)
        }

        Node Assignment(List<(string lexime, Classification classification)> tokens)
        {

        }
    }
}