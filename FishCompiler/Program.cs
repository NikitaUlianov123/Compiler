namespace FishCompiler
{
    public enum Classification
    { 
        keyword,
        variableType,
        variableName,
        Operator,
        bracket,
        number,
        text,
        comment,
        parser
    };
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);

            string program = System.IO.File.ReadAllText(args[0]);

            Tokeniser tokeniser = new Tokeniser();

            var tokens = tokeniser.tokenise(program);

            Parser parser = new Parser();

            /*var tree = */parser.Parse(tokens);
        }
    }
}