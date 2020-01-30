using org.pdfclown.documents;
using org.pdfclown.documents.contents;
using org.pdfclown.files;
using org.pdfclown.tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractFirstThreeColumnsPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"[0-9]+\s(\w+\s)+[0-9]+[.][0-9]+[%]");
            // pdfclown file class
            try
            {
                File file = new File("Lista discount Ianuarie 2020.pdf");
                Document pdfDocument = file.Document;

                // TextExtractor to extract text
                TextExtractor textExtractor = new TextExtractor();
                foreach (Page page in pdfDocument.Pages)
                {
                    IList<ITextString> textStrings = textExtractor.Extract(page)[TextExtractor.DefaultArea];
                    foreach (ITextString textString in textStrings)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (char letter in textString.Text)
                        {
                            sb.Append(letter);
                        }

                        string finalString = sb.ToString().Trim();  // Trim ending whitespace character

                        // find match for regular exp
                        Match match = regex.Match(finalString);
                        if (match.Success)
                        {
                            string escapedString = match.Value;
                            Console.WriteLine(escapedString);
                        }
                    }
                }            
            }

            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Could not open file! File not found!");
            }

        }
    }
}
