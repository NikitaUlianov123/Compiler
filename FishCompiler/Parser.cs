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
            children = new List<Node>();
        }
        public Node(Node parent)
        {
            Parent = parent;
            children = new List<Node>();
        }
        public Node((string lexime, Classification classification) token)
        {
            children = new List<Node>();
            this.token = token;
        }
        public Node((string lexime, Classification classification) token, Node parent)
        {
            Parent = parent;
            children = new List<Node>();
            this.token = token;
        }
    }

    public class Parser
    {
        public void Parse(List<(string lexime, Classification classification)> tokens)
        {
            Node root = new Node();

            List<(string lexime, Classification classification)> current = new List<(string lexime, Classification classification)>();
            for (int i = 0; i < tokens.Count && (tokens[i].lexime != ";" && tokens[i].lexime != "{" && tokens[i].lexime != "}"); i++)
            {
                current.Add(tokens[i]);
            }

            E e = new E();

            int index = 0;
            var result = e.Parse(current, ref index);
            if (result.successful)
            {
                root.children.Add(result.result);
                result.result.Parent = root;
            }
        }
    }
}