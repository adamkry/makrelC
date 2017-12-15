using makrelC.Data;
using makrelC.Import;
using makrelC.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC
{
    class Program
    {
        static void Main(string[] args)
        {            
            string input = String.Empty;
            Console.WriteLine("Type 'import <path>' to import, 'x' - exit, 'query'");
            while ((input = Console.ReadLine()) != "x")
            {
                if (input.StartsWith("import "))
                {
                    var importer = new InputDataImporter();
                    importer.RunImport(input);
                }
                else if (input == "q")
                {
                    QueryScenario scenario = new QueryScenario(new QueryDefinition(4, 5.0), new Model.Entity.Company { Id = 1 }, new DateTime(2017, 02, 07), 5);
                    scenario.Run();
                }
            }
        }
    }
}
