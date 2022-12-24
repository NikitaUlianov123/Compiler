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

        (bool successful, Node) E(List<(string lexime, Classification classification)> tokens)
        {
            if (tokens[0].classification == )
        }
        Node Eprime(List<(string lexime, Classification classification)> tokens)
        {
            if (tokens[0].classification == Classification.variableName)
        }

        Node Assignment(List<(string lexime, Classification classification)> tokens)
        {

        }
    }
}