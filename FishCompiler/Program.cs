using System;
using System.Reflection;
using System.Reflection.Emit;

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



            var programName = "runme";
            var filename = $"{programName}.exe";

            var namespaceName = "Code";
            var className = "Program";
            var moduleName = $"{namespaceName}.{className}";
            var entryPointMethodName = "Main";

            var assemblyName = new AssemblyName("Program");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName,
                                                        AssemblyBuilderAccess.Save);

            var moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName, filename);

            var programClassBuilder = moduleBuilder.DefineType("Program",
                                        TypeAttributes.Public | TypeAttributes.Class);

            var mainMethodBuilder = programClassBuilder.DefineMethod(name: entryPointMethodName,
                                        attributes: MethodAttributes.Public | MethodAttributes.Static,
                                        returnType: typeof(void),
                                        parameterTypes: new[] { typeof(string[]) });

            var mainILGen = mainMethodBuilder.GetILGenerator();



            mainILGen.Emit(OpCodes.Ldstr, "Hello, World!");
            mainILGen.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            mainILGen.Emit(OpCodes.Ret);



            var programClass = programClassBuilder.CreateType();

            assemblyBuilder.SetEntryPoint(mainMethodBuilder, PEFileKinds.ConsoleApplication);

            assemblyBuilder.Save(filename);
        }
    }
}