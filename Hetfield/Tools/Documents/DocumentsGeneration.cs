using Hetfield.Models;
using ScottPlot.Palettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using Hetfield.View;

namespace Hetfield.Tools.Documents
{
    internal class DocumentsGeneration
    {
        public static void GeneratePaidContract(PaidContractModel Model, string NewPath)
        {
            try
            {
                byte[] FileBytes = Properties.Resources.HetfieldPaidContract;
                string TempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(TempFilePath, FileBytes);

                Word.Application App = new Word.Application();
                App.Visible = false;
                Word.Document document = App.Documents.Open(TempFilePath);

                ChangeWordsPaidContract(Model, document);

                document.SaveAs2(FileName: NewPath);
                document.Close();
                App.Quit();
            }
            catch (System.Exception ex)
            {
                new CustomMessageBoxView("Ошибка генерации договора купли-продажи").ShowDialog();
            }
        }

        private static void ChangeWordsPaidContract(PaidContractModel Model, Word.Document document)
        {
            ReplaceWordWithUnderline("{DateOfDrawingUp}", $"{DateTime.Today:dd.MM.yyyy}", document);
            ReplaceWord("{MarkAndModel}", Model.MarkAndModel, document);
            ReplaceWord("{VIN}", Model.VIN, document);
            ReplaceWord("{CarType}", Model.CarType, document);
            ReplaceWord("{ManufactureYear}", Model.ManufactureYear.ToString(), document);
            ReplaceWord("{CarColor}", Model.CarColor, document);
            ReplaceWord("{CarPower}", Model.CarPower.ToString(), document);
            ReplaceWord("{EngineDisplacement}", Model.EngineDisplacement.ToString(), document);
            ReplaceWord("{PassportSeriesAndNumber}", Model.PassportSeriesAndNumber, document);
            ReplaceWord("{Transmission}", Model.Transmission, document);
            ReplaceWord("{Configuration}", Model.Configuration, document);
            ReplaceWord("{Mileage}", Model.Mileage.ToString(), document);
            ReplaceWord("{TankCapacity}", Model.TankCapacity.ToString(), document);
            ReplaceWord("{OwnerInitials}", Model.OwnerInitials, document);
            ReplaceWord("{OwnerInitials2}", Model.OwnerInitials, document);
            if (Model.BuyerInitials.Length > 28)
            {
                ReplaceWord("{BuyerInitials}", "", document);
                ReplaceWord("{BuyerInitials2}", "", document);
                return;
            }
            ReplaceWord("{BuyerInitials}", Model.BuyerInitials, document);
            ReplaceWord("{BuyerInitials2}", Model.BuyerInitials, document);

            ReplaceWord("{Buyer}", Model.Buyer, document);
            ReplaceWord("{Owner}", Model.Owner, document);
            ReplaceWord("{TotalPrice}", Model.TotalPrice.ToString(), document);
            ReplaceWord("{TotalPrice2}", Model.TotalPrice.ToString(), document);

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
