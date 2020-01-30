using org.pdfclown.documents;
using org.pdfclown.documents.contents;
using org.pdfclown.files;
using org.pdfclown.tools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PdfTest1
{
    class AdvancedTextExtractionSample
    {
        static void Main(string[] args)
        {
            try
            {
                File file = new File("Lista discount Ianuarie 2020.pdf");
                Document pdfDocument = file.Document;

                TextExtractor textExtractor = new TextExtractor();
                foreach (Page page in pdfDocument.Pages)
                {
                    IList<ITextString> textStrings = textExtractor.Extract(page)[TextExtractor.DefaultArea];
                    foreach (ITextString textString in textStrings)
                    {
                        RectangleF textStringBox = textString.Box.Value;
                        Console.WriteLine(
                          "Text ["
                            + "x:" + Math.Round(textStringBox.X) + ","
                            + "y:" + Math.Round(textStringBox.Y) + ","
                            + "w:" + Math.Round(textStringBox.Width) + ","
                            + "h:" + Math.Round(textStringBox.Height)
                            + "]: " + textString.Text
                            );
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
