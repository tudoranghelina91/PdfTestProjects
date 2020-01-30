using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Regex4
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "file.txt";
            StreamReader sr = new StreamReader(filename);
            string input;
            string pattern = @"\b(\w+)\s\1\b";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

            while (sr.Peek() >= 0)
            {
                input = sr.ReadLine();
                MatchCollection matches = rgx.Matches(input);
                if (matches.Count > 0)
                {
                    Console.WriteLine("{0} ({1} matches):", input, matches.Count);
                    foreach (Match match in matches)
                        Console.WriteLine("   " + match.Value);
                }
            }
            sr.Close();
        }
    }
}
