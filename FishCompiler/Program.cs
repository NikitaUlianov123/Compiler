namespace FishCompiler
{
    enum Classification
    { 
        keyword,
        variableType,
        variableName,
        Operator,
        bracket,
        number,
        text
    };
    
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string lexime, Classification classification)> tokens = new List<(string lexime, Classification classification)>();

            Console.WriteLine(args[0]);

            string program = System.IO.File.ReadAllText(args[0]);

            const int quoteIndex = 7;

            string[] keywords = { "if", "else", "true", "false" };
            string[] variableTypes = { "int", "double", "string" };
            string[] operators = { "+", "-", "*", "/", "=", "=?", "!=", "\"", ";", "+=", "-=", "*=", "/=", "?" };
            string[] brackets = { "(", ")", "{", "}", "[", "]"};
            string[] whitespace = { " ", "\r", "\n" };

            string current = "";

            for (int i = 0; i < program.Length; i++)
            {
                current += program[i];
                while (current == " " || current == "\r" || current == "\n" || current == "\t")
                {
                    current = "";
                    current += program[++i];
                }
                if (current == operators[quoteIndex])
                {
                    tokens.Add((current, Classification.Operator));
                    current = "";
                    while (program[i + 1].ToString() != operators[quoteIndex])
                    {
                        current += program[++i];
                    }
                    tokens.Add((current, Classification.text));
                    current = operators[quoteIndex];
                    i++;
                }

                if (keywords.Contains(current))
                {
                    tokens.Add((current, Classification.keyword));
                    current = "";
                }
                if (variableTypes.Contains(current))
                {
                    tokens.Add((current, Classification.variableType));
                    current = "";
                }
                if (operators.Contains(current) && (i < program.Length - 1 ? !operators.Contains(current + program[i + 1]) : true))
                {
                    tokens.Add((current, Classification.Operator));
                    current = "";
                }
                if (brackets.Contains(current))
                {
                    tokens.Add((current, Classification.bracket));
                    current = "";
                }
                if (current != "" && (whitespace.Contains(program[i + 1].ToString()) || operators.Contains(program[i + 1].ToString()) || brackets.Contains(program[i + 1].ToString())) && !operators.Contains(current + program[i + 1]))
                {
                    int number = 0;
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

            ;
        }
    }
}