using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFDataGridView
{
    public partial class Form1 : Form
    {
        BindingList<Client> clients;
        BindingSource bindingSource;
        PDFDeserializer pdfDeserializer;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                pdfDeserializer = new PDFDeserializer("Lista discount Ianuarie 2020.pdf");
                clients = pdfDeserializer.ToBindingList();
                bindingSource = new BindingSource();
                bindingSource.DataSource = clients;
                dataGridView1.DataSource = bindingSource;

                // format cell using percent symbol
                
                dataGridView1.Columns["Discount"].DefaultCellStyle.Format = @"##.##";
                dataGridView1.Columns["Discount"].HeaderText = "Discount %";
            }

            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("ERROR! FILE NOT FOUND!");
            }
        }

        private void exportSpreadsheetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelHandler.ExportToExcel(clients);
                MessageBox.Show("Export done!");
            }

            catch (System.IO.IOException)
            {
                MessageBox.Show("Could not save file. File is in use!");
            }

            catch (Exception ex)
            {
                if (ex.Message.StartsWith("IronXL License Exception."))
                {
                    MessageBox.Show("ERROR: IronXL in trial mode");
                }

                else
                {
                    MessageBox.Show("Unknown error occured");
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = e.Exception.Message;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = null;
        }
    }
}
