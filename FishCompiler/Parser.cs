using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography;
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
        public static Node Parse(List<(string lexime, Classification classification)> tokens, ref int start)
        {
            Node root = new Node();

            var result = Code.Parse(tokens, ref start, false);
            root.children.Add(result.result);

            //root.GetType(); //debugging

            ast(root);

            return root;
        }

        public static void ast(Node cst)
        {
            for (int i = 0; i < cst.children.Count; i++)
            {
                ast(cst.children[i]);
            }

            if (cst.token.lexime == "E" || cst.token.lexime == "E`")
            {
                for (int i = 0; i < cst.children.Count; i++)
                {
                    if (cst.children[i].token.classification == Classification.Operator)
                    {
                        cst.token = cst.children[i].token;
                        cst.children.RemoveAt(i);
                    }
                }
            }
            else if (cst.token.lexime == "E``")
            {
                if (cst.children[0].token.classification == Classification.bracket)
                {
                    cst.token = cst.children[1].token;
                    cst.children = cst.children[1].children;
                }
                else
                {
                    cst.token = cst.children[0].token;
                    cst.children = cst.children[0].children;
                }
            }
            else if (cst.token.lexime == "assignment")
            {
                for (int i = 0; i < cst.children.Count; i++)
                {
                    if (cst.children[i].token.lexime == "=")
                    {
                        cst.token = cst.children[i].token;
                        cst.children.RemoveAt(i);
                    }
                }
            }
            else if (cst.token.classification == Classification.variableType)
            {
                cst.children.Add(cst.Parent.children[1]);
                cst.Parent.children.RemoveAt(1);
            }
            else if (cst.token.lexime == "C")
            {
                for (int i = 0; i < cst.children.Count; i++)
                {
                    if (cst.children[i].token.classification == Classification.comparison)
                    {
                        cst.token = cst.children[i].token;
                        cst.children.RemoveAt(i);
                    }
                }
            }
            else if (cst.token.lexime == "C`")
            {
                if (cst.children[0].token.classification == Classification.bracket)
                {
                    cst.token = cst.children[1].token;
                    cst.children = cst.children[1].children;
                }
                else if (cst.children[0].token.lexime == "!")
                {
                    cst.children[0].children.Add(cst.children[1]);
                    cst.children[1].Parent = cst.children[0];

                    cst.token = cst.children[0].token;
                    cst.children = cst.children[0].children;
                }
                else
                {
                    cst.token = cst.children[0].token;
                    cst.children = cst.children[0].children;
                }
            }
            else if (cst.children.Count > 0 && cst.children[0].token.classification == Classification.bracket && cst.children[cst.children.Count - 1].token.classification == Classification.bracket)
            {
                for (int i = 0; i < cst.children.Count; i++)
                {
                    if (cst.children[i].token.classification == Classification.bracket)
                    {
                        cst.children.RemoveAt(i);
                        i--;
                    }
                }
            }
            else  if (cst.token.lexime == "if")
            {
                
                cst.children.Add(cst.Parent.children[2]);
                cst.children.Add(cst.Parent.children[5]);
            }

            if (cst.children.Count == 1 && cst.token.classification == Classification.parser)
            {
                cst.token = cst.children[0].token;
                cst.children = cst.children[0].children;
            }
        }
    }
}