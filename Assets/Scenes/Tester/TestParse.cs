using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ConsoleTestApplication1
{
    public class TestParse : MonoBehaviour
    {   
        public void Start()
        {
           
           SimpleDemo(); 
        }

        private static string ReadInput()
        {
            var sb = new StringBuilder();
            int c;
            while ((c = Console.Read()) >= 0)
            {
                sb.Append((char)c);
            }
            return sb.ToString();
        }

        private static string Escapse(string expr)
        {
            return expr.Replace("\r", "\\r").Replace("\n", "\\n");
        }

        private static void PrintAst(Node node, int level)
        {
            var indension = new string('.', level);
            var ns = node.ToString();
            Console.WriteLine("{0,-20} [{1}]", indension + node.GetType().Name, 
                Escapse(ns.Substring(0, Math.Min(20, ns.Length))));
            foreach (var child in node.EnumChildren())
                PrintAst(child, level + 1);
        }


        static void Main(string[] args)
        {
            
            //ParseAndPrint();
            //FetchAndParse();
            //LoadAndParse();
        }

        static void SimpleDemo()
        {
            // Fills the missing template parameters.
            var parser = new WikitextParser();
            var templateNames = new [] {"Expand section", "Cleanup"};
            var text = "==Hello==<!--comment-->{{Expand section|date=2010-10-05}}{{Cleanup}}This is a nice '''paragraph'''.==References=={{Reflist}}";
            var ast = parser.Parse(text);
            Debug.Log(ast.ToString());
            Debug.Log(ast.ToPlainText());
            
            
            // Convert the code snippets to nodes
            var dateName = parser.Parse("date");
            var dateValue = parser.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
            Console.WriteLine("Issues:");
            // Search and set
            foreach (var t in ast.EnumDescendants().OfType<Template>()
                .Where(t => templateNames.Contains(MwParserUtility.NormalizeTemplateArgumentName(t.Name))))
            {
                // Get the argument by name.
                var date = t.Arguments["date"];
                if (date != null)
                {
                    // To print the wikitext instead of user-friendly text, use ToString()
                    Console.WriteLine("{0} ({1})", t.Name.ToPlainText(), date.Value.ToPlainText());
                }
                // Update/Add the argument
                t.Arguments.SetValue(dateName, dateValue);
            }
            Console.WriteLine();
            Console.WriteLine("Wikitext:");
            
        }

        private static void ParseAndPrint()
        {
            var parser = new WikitextParser();
            Console.WriteLine("Please input the wikitext to parse; use EOF (Ctrl+Z) to accept:");
            var ast = parser.Parse(ReadInput());
            Console.WriteLine("Parsed AST");
            PrintAst(ast, 0);
            Console.WriteLine("Plain text");
            Console.WriteLine(ast.ToPlainText());
        }

        private static void FetchAndParse()
        {
            Console.WriteLine("Please input the title of the Wikipedia page to parse:");
            var title = Console.ReadLine();
            Console.WriteLine();
            var ast = FetchAndParse(title);
            Console.WriteLine("Headings:");
            // Show all headings
            foreach (var h in ast.EnumDescendants().OfType<Heading>())
            {
                Console.WriteLine(h.ToString());
            }
        }

        private static void LoadAndParse()
        {
            Console.WriteLine("Please input the path of the file to parse:");
            var fileName = Console.ReadLine();
            Console.WriteLine();
            var ast = LoadAndParse(fileName.Trim(' ', '\t', '"'));
            PrintAst(ast, 0);
            Console.WriteLine("Headings:");
            // Show all headings
            foreach (var h in ast.EnumDescendants().OfType<Heading>())
            {
                Console.WriteLine(h.ToString());
            }
        }

        /// <summary>
        /// Fetches a page from en Wikipedia, and parse it.
        /// </summary>
        private static Wikitext FetchAndParse(string title)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            const string EndPointUrl = "https://en.wikipedia.org/w/api.php";
            var client = new HttpClient();
            var requestContent = new Dictionary<string, string>
            {
                {"format", "json"},
                {"action", "query"},
                {"prop", "revisions"},
                {"rvlimit", "1"},
                {"rvprop", "content"},
                {"titles", title}
            };
            var response = client.PostAsync(EndPointUrl, new FormUrlEncodedContent(requestContent)).Result;
            var root = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            var content = (string) root["query"]["pages"].Children<JProperty>().First().Value["revisions"][0]["*"];
            var parser = new WikitextParser();
            return parser.Parse(content);
        }

        /// <summary>
        /// Loads a page from file and parse it.
        /// </summary>
        private static Wikitext LoadAndParse(string fileName)
        {
            var content = File.ReadAllText(fileName);
            var parser = new WikitextParser();
            return parser.Parse(content);
        }
    }
}