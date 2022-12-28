using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Principal;
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

    public static class Parser
    {
        public static void Parse(List<(string lexime, Classification classification)> tokens, ref int start)
        {
            Node root = new Node();

            Code.Parse(tokens, ref start);

            root.GetType();
        }
    }
}