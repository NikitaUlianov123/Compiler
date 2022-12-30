using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public static class Semantic
    {
        public static bool Analyse(Node ast)
        {
            var variables = getVariables(ast);
            return typecheck(ast, variables);
        }

        public static bool typecheck(Node ast, Dictionary<string, string> variables)
        {
            for (int i = 0; i < ast.children.Count; i++)
            {
                var result = typecheck(ast.children[i], variables);

                if (!result)
                {
                    return result;
                }
            }

            if (ast.token.classification == Classification.comparison)
            {
                string type1 = variables[ast.children[0].token.lexime];
                string type2 = "";

                switch (type1)
                {
                    case "int":
                        type2 = ast.children[1].token.classification == Classification.number ? "int" : variables[ast.children[1].token.lexime];
                        break;

                    case "string":
                        type2 = ast.children[1].token.classification == Classification.text ? "string" : variables[ast.children[1].token.lexime];
                        break;

                    case "bool":
                        type2 = ast.children[1].token.lexime == "true" || ast.children[1].token.lexime == "false" ? "bool" : variables[ast.children[1].token.lexime];
                        break;
                }

                return type1 == type2;
            }
            else if (ast.token.lexime == "=")
            {
                if (ast.children[1].token.classification != Classification.Operator)
                {

                    string type1 = ast.children[0].token.classification == Classification.variableType ? ast.children[0].token.lexime : variables[ast.children[0].token.lexime];
                    string type2 = "";

                    switch (type1)
                    {
                        case "int":
                            type2 = ast.children[1].token.classification == Classification.number ? "int" : variables[ast.children[1].token.lexime];
                            break;

                        case "string":
                            type2 = ast.children[1].token.classification == Classification.text ? "string" : variables[ast.children[1].token.lexime];
                            break;

                        case "bool":
                            type2 = ast.children[1].token.lexime == "true" || ast.children[1].token.lexime == "false" ? "bool" : variables[ast.children[1].token.lexime];
                            break;
                    }

                    return type1 == type2;
                }
            }
            else if (ast.token.classification == Classification.Operator)
            {
                string type1 = variables[ast.children[0].token.lexime];
                string type2 = "";

                switch (type1)
                {
                    case "int":
                        type2 = ast.children[1].token.classification == Classification.number ? "int" : variables[ast.children[1].token.lexime];
                        break;

                    case "string":
                        type2 = ast.children[1].token.classification == Classification.text ? "string" : variables[ast.children[1].token.lexime];
                        break;

                    case "bool":
                        type2 = ast.children[1].token.lexime == "true" || ast.children[1].token.lexime == "false" ? "bool" : variables[ast.children[1].token.lexime];
                        break;
                }

                return type1 == type2;
            }

            return true;
        }

        public static Dictionary<string, string> getVariables(Node ast)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();

            for (int i = 0; i < ast.children.Count; i++)
            {
                var result = getVariables(ast.children[i]);

                foreach (var item in result)
                { 
                    variables.Add(item.Key, item.Value);
                }
            }

            if (ast.token.classification == Classification.variableType)
            {
                variables.Add(ast.children[0].token.lexime, ast.token.lexime);
            }

            return variables;
        }
    }
}