namespace FishCompiler
{
    enum Classification
    { 
        keyword,
        variableType,
        variableName,
        Operator,
        bracket,
        number
    };
    
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string lexime, Classification classification)> tokens = new List<(string lexime, Classification classification)>();

            Console.WriteLine(args[0]);

            string program = System.IO.File.ReadAllText(args[0]);

            string[] keywords = { "if", "else", "true", "false" };
            string[] variableTypes = { "int", "double", "string" };
            string[] operators = { "+", "-", "*", "/", "=", "=?", "!=", "(", ")", "\"", ";", "{", "}", "[", "]", "=+", "-=", "*=", "/=", "?" };

            string current = "";

            for (int i = 0; i < program.Length; i++)
            {
                current += program[i];
                while (current == " " || current == "\r" || current == "\n" || current == "\t")
                {
                    current = "";
                    current += program[++i];
                }

                foreach (string keyword in keywords)
                {
                    if (current == keyword)
                    {
                        tokens.Add((current, Classification.keyword));
                        current = "";
                        break;
                    }
                }
                foreach (string variableType in variableTypes)
                {
                    if (current == variableType)
                    {
                        tokens.Add((current, Classification.variableType));
                        current = "";
                        break;
                    }
                }
                foreach (string Operator in operators)
                {
                    if (current == Operator && (i < program.Length - 1 ? !operators.Contains(current + program[i + 1]) : true))
                    {
                        tokens.Add((current, Classification.Operator));
                        current = "";
                        break;
                    }
                }
                if (current != "" && (program[i + 1] == ' ' || operators.Contains(program[i + 1].ToString())))
                {
                    int number = 0;
                    if (int.TryParse(current, out number))
                    {
                        tokens.Add((current, Classification.number));
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