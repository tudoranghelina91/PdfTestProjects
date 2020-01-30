﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Regex3
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "file.txt";
            StreamReader sr = new StreamReader(fileName);
            string input;
            string pattern = @"\b(\w+)\s\1\b";
            while (sr.Peek() >= 0)
            {
                input = sr.ReadLine();
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
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
