using org.pdfclown.documents;
using org.pdfclown.documents.contents;
using org.pdfclown.files;
using org.pdfclown.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfToExcel
{
    class PDFDeserializer
    {
        public string FilePath { get; set; }

        public PDFDeserializer(string filePath)
        {
            FilePath = filePath;
        }

        public IEnumerable<Client> Deserialize()
        {
            const string matchIdExp = @"[0-9]+";
            const string matchNameExp = @"([^0-9]\w+\s)+";
            const string matchDiscountExp = @"[0-9]+[.][0-9]+[%]";

            Regex idRegex = new Regex(matchIdExp);
            Regex nameRegex = new Regex(matchNameExp);
            Regex discountRegex = new Regex(matchDiscountExp);
            Regex extractRegex = new Regex(@"[0-9]+\s(\w+\s)+[0-9]+[.][0-9]+[%]");

            File file;

            try
            {
                file = new File(FilePath);
            }

            catch(System.IO.FileNotFoundException)
            {
                throw;
            }

            if (file != null)
            {
                Document pdfDocument = file.Document;

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

                        // The whole line
                        string finalString = sb.ToString().Trim();
                        Match match = extractRegex.Match(finalString);
                        if (match.Success)
                        {
                            Match matchId = idRegex.Match(finalString);
                            Match matchName = nameRegex.Match(finalString);
                            Match matchDiscount = discountRegex.Match(finalString);

                            string idString = matchId.Value.Trim();
                            string nameString = matchName.Value.Trim();
                            string discountString = matchDiscount.Value.Trim();

                            // remove percentage sign from discount string
                            discountString = discountString.Remove(discountString.Length - 1);

                            Client client = new Client();
                            client.Id = Convert.ToInt32(idString);
                            client.Name = nameString;
                            client.Discount = (float)Convert.ToDecimal(discountString);

                            yield return client;
                        }
                    }
                }
            }

            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
