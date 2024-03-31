using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using Hetfield.View;
using Microsoft.Win32;
using System.Windows;
using Hetfield.Models;

namespace Hetfield.Tools.Documents
{
    internal class SalesReportGeneration
    {
        public static void GenerateWorkReport(SalesReportModel Model, string NewPath)
        {
            try
            {
                byte[] FileBytes = Properties.Resources.HetfieldWorkReport;
                string TempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(TempFilePath, FileBytes);

                Word.Application App = new Word.Application();
                App.Visible = false;
                Word.Document document = App.Documents.Open(TempFilePath);

                ChangeWordsWorkReport(Model, document);
                GenerateTableWorkReport(Model, document);

                document.SaveAs2(FileName: NewPath);
                document.Close();
                App.Quit();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void ChangeWordsWorkReport(SalesReportModel Model, Word.Document document)
        {
            ReplaceWordWithUnderline("{DateOfDrawingUp}", $"{Model.DateOfDrawingUp:dd.MM.yyyy}", document);
            ReplaceWord("{TotalCost}", $"{Model.TotalCost} ₽", document);
            ReplaceWord("{StartDate}", $"{Model.StartDate:dd.MM.yyyy}", document);
            ReplaceWord("{EndDate}", $"{Model.EndDate:dd.MM.yyyy}", document);
            ReplaceWord("{CurrentDate}", $"{Model.CurrentDate:dd.MM.yyyy}", document);
        }
        private static void GenerateTableWorkReport(SalesReportModel Model, Word.Document document)
        {
            Table table = document.Tables[1];
            int CountOfOrders = Model.Orders.Count;
            for (int i = 1; i <= CountOfOrders; i++)
                table.Rows.Add();
            int index = 0;
            foreach (Row row in table.Rows)
            {
                if (row.Index == 1)
                    continue;
                foreach (Cell cell in row.Cells)
                {
                    _ = cell.ColumnIndex == 1 ? cell.Range.Text = Model.Orders[index].idOrder.ToString() :
                        cell.ColumnIndex == 2 ? cell.Range.Text = Model.Orders[index].Owner :
                        cell.ColumnIndex == 3 ? cell.Range.Text = Model.Orders[index].Buyer :
                        cell.ColumnIndex == 4 ? cell.Range.Text = Model.Orders[index].Staff :
                        cell.ColumnIndex == 5 ? cell.Range.Text = Model.Orders[index].CarMarkAndModel :
                        cell.ColumnIndex == 6 ? cell.Range.Text = $"{Model.Orders[index].CarPrice} ₽" :
                        cell.ColumnIndex == 7 ? cell.Range.Text = $"{Model.Orders[index].FinalPrice} ₽" :
                        cell.Range.Text = $"{Model.Orders[index].DateOfOrder:dd.MM.yyyy}";
                }
                index++;
            }
        }

        private static Word.Range ReplaceWord(string Original, string NewText, Word.Document WordDocument, bool Returnable = false)
        {
            Word.Range Range = WordDocument.Content;
            Range.Find.ClearFormatting();
            Range.Find.Execute(FindText: Original, ReplaceWith: NewText);
            return Range;
        }

        public static void ReplaceWordWithUnderline(string Original, string NewText, Word.Document WordDocument)
        {
            Word.Range Range = ReplaceWord(Original, NewText, WordDocument, true);
            Range.Font.Underline = WdUnderline.wdUnderlineSingle;
        }
    }
}
