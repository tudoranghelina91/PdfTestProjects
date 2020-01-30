using IronXL;
using System.Collections.Generic;

namespace PdfToExcel
{
    static class ExcelHandler
    {
        internal static void ExportToExcel(IEnumerable<Client> clients)
        {
            try
            {
                WorkBook workBook = WorkBook.Create(ExcelFileFormat.XLSX);
                WorkSheet workSheet = workBook.CreateWorkSheet("discounts");

                workSheet["A1"].Value = "Id";
                workSheet["B1"].Value = "Name";
                workSheet["C1"].Value = "Discount";

                int row = 2;

                foreach (var client in clients)
                {
                    workSheet[$"A{row}"].Value = client.Id;
                    workSheet[$"B{row}"].Value = client.Name;
                    workSheet[$"C{row}"].Value = $"{client.Discount}%";
                    row++;
                }

                try
                {
                    workBook.SaveAs("ClientsDiscount.xlsx");
                }
                catch (System.IO.IOException)
                {
                    throw;
                }
            }

            catch
            {
                throw;
            }
        }
    }
}
