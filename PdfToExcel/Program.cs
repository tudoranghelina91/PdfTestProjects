using org.pdfclown.documents;
using org.pdfclown.documents.contents;
using org.pdfclown.files;
using org.pdfclown.tools;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfToExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            PDFDeserializer pdfDeserializer = new PDFDeserializer("Lista discount Ianuarie 2020.pdf");
            try
            {
                var clients = pdfDeserializer.Deserialize();

                try
                {
                    ExcelHandler.ExportToExcel(clients);
                    Console.WriteLine("Export done!");
                }

                catch (System.IO.IOException)
                {
                    Console.WriteLine("Could not save file. File is in use!");
                }
            }

            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("ERROR! FILE NOT FOUND!");
            }

            catch (Exception e)
            {
                if (e.Message.StartsWith("IronXL License Exception."))
                {
                    Console.WriteLine("ERROR: IronXL in trial mode");
                }
            }

            finally
            {
                Console.WriteLine("Press any key to exit!");
            }

            Console.ReadKey();
        }


    }
}
