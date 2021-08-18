using ProjectATI.Code.Parser;
using ProjectATI.Code.PKB;
using ProjectATI.Code.Statements;
using System;
using ProjectATI.Code.QueryProcessor;

namespace PipeTesterConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    System.Console.WriteLine("Please enter test file argument.");
                    return;
                }

                Pkb Pkb = Pkb.GetPkbInstance();
                string path = args[0];
                string code = System.IO.File.ReadAllText(path);

                //string code = @"program p {
                //                 procedure First {
                //                  assign x = 2;
                //                  assign z = 3;
                //                  calls Second; }
                //                 procedure Second {
                //                  assign x = 0;
                //                  assign i = 5;
                //                  while i {
                //                   assign x = x + 2 + y;
                //                   calls Third;
                //                   assign i = i + 1; }
                //                 if x then {
                //                  assign x = x + 1; }
                //                 else {
                //                  assign z = 1; }
                //                 assign z = z + x + i;
                //                 assign y = z + 2;
                //                 assign x = x + y + z; }
                //                procedure Third {
                //                 assign z = 5;
                //                 assign v = z; }}";

                Parser parser = new Parser(code);
                Node node = parser.Parse();
                Pkb.SetAST((ProgramStatement)node);
                Pkb.SetProcedureList();
                Pkb.PopulateTables(node);

                Console.WriteLine("Ready");
                while (true)
                {
                    var variablesLine = Console.ReadLine();
                    var queryLine = Console.ReadLine();
                    var queryText = variablesLine + "\r\n" + queryLine;

                    // TODO: change to working code for QueryProcessor
                    QueryPreprocessor queryPreprocessor = new QueryPreprocessor(queryText);
                    Query query = queryPreprocessor.Query;
                    QueryEvaluator queryEvaluator = new QueryEvaluator(query);
                    queryEvaluator.Evaluate();
                    var resultProjector = queryEvaluator.ResultProjector;
                    var resultQueryEvaluated = resultProjector.Project();
                    Console.WriteLine(resultQueryEvaluated);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR");
            }
        }
    }
}
