using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCompiler
{
    public class Tokeniser
    {
        public List<(string lexime, Classification classification)> tokenise(string program)
        {
            List<(string lexime, Classification classification)> tokens = new List<(string lexime, Classification classification)>();

            const int quoteIndex = 7;

            string[] keywords = { "if", "else", "true", "false" };

            string[] variableTypes = { "int", "double", "string" };
            string[] operators = { "+", "-", "*", "/", "=", "=?", "!=", "\"", ";", "+=", "-=", "*=", "/=", "." };
            string[] brackets = { "(", ")", "{", "}", "[", "]" };
            string[] whitespace = { " ", "\r", "\n", "\t" };
            string[] comments = { "//", "/*", "*/" };

            string current = "";

            for (int i = 0; i < program.Length; i++)
            {
                current += program[i];

                //Removing whitespace
                while (whitespace.Contains(current) && i < program.Length - 1)
                {
                    current = "";
                    current += program[++i];
                }

                //comments
                if (current == comments[0])
                {
                    while (program[i + 1].ToString() != "\n" && program[i + 1].ToString() != "\r")
                    {
                        current += program[++i];
                    }
                    tokens.Add((current, Classification.comment));
                    current = "";
                }
                else if (current == comments[1])
                {
                    while (program[i - 1].ToString() + program[i].ToString() != comments[2].ToString())
                    {
                        current += program[++i];
                    }
                    tokens.Add((current, Classification.comment));
                    current = "";
                }

                //string literals
                else if (current == operators[quoteIndex])
                {
                    tokens.Add((current, Classification.Operator));
                    current = "";
                    while (program[i + 1].ToString() != operators[quoteIndex])
                    {
                        current += program[++i];
                    }
                    tokens.Add((current, Classification.text));
                    tokens.Add((operators[quoteIndex], Classification.Operator));
                    current = "";
                    i++;
                }

                else if (keywords.Contains(current) && i < program.Length - 1 && (whitespace.Contains(program[i + 1].ToString()) || operators.Contains(program[i + 1].ToString()) || brackets.Contains(program[i + 1].ToString())))
                {
                    tokens.Add((current, Classification.keyword));
                    current = "";
                }
                else if (variableTypes.Contains(current) && i < program.Length - 1 && (whitespace.Contains(program[i + 1].ToString()) || operators.Contains(program[i + 1].ToString()) || brackets.Contains(program[i + 1].ToString())))
                {
                    tokens.Add((current, Classification.variableType));
                    current = "";
                }
                else if (operators.Contains(current) && (i < program.Length - 1 ? !(operators.Contains(current + program[i + 1]) || comments.Contains(current + program[i + 1])) : true))
                {
                    tokens.Add((current, Classification.Operator));
                    current = "";
                }
                else if (brackets.Contains(current))
                {
                    tokens.Add((current, Classification.bracket));
                    current = "";
                }
                else if (current != "" && i < program.Length - 1 && (whitespace.Contains(program[i + 1].ToString()) || operators.Contains(program[i + 1].ToString()) || brackets.Contains(program[i + 1].ToString())) && !operators.Contains(current + program[i + 1]) && !comments.Contains(current + program[i + 1]))
                {
                    int number;
                    if (int.TryParse(current, out number))
                    {
                        tokens.Add((number.ToString(), Classification.number));
                    }
                    else
                    {
                        tokens.Add((current, Classification.variableName));
                    }
                    current = "";
                }
            }

            return tokens;
        }
    }
}