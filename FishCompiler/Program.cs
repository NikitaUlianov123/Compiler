namespace FishCompiler
{
    public enum Classification
    { 
        keyword,
        variableType,
        variableName,
        Operator,
        comparison,
        bracket,
        number,
        text,
        comment,
        parser,
        conditional
    };
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);

            string program = System.IO.File.ReadAllText(args[0]);

            Tokeniser tokeniser = new Tokeniser();

            var tokens = tokeniser.tokenise(program);

            int number = 0;
            var tree = Parser.Parse(tokens, ref number);

            var analysis = Semantic.Analyse(tree);
        }
    }
}