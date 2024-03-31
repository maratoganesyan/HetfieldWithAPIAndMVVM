using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.Documents;
using Hetfield.Tools.MVVMTools;
using Hetfield.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hetfield.ViewModel
{
    internal class OrderPageVM : EmployeeViewPagesVM<Order>
    {
        public OrderPageVM() : base()
        {

        }


        public RelayCommand GeneratePaidContractCommand => new RelayCommand(obj => GeneratePaidContractAsync(obj));

        private async void GeneratePaidContractAsync(object parameter)
        {
            if(parameter is int IdOrder)
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
                if (sv.ShowDialog() == true)
                {
                    Order order = TableValue.Single(o => o.IdOrder == IdOrder);
                    PaidContractModel contractModel = new PaidContractModel(order);
                    ContentDialogService.userControlInDialog = new LoadingUserControlView("Идёт генерация договора купли-продажи. Подождите пожайлуста");
                    ContentDialogService service = new ContentDialogService();
                    service.OpenDialog();
                    await Task.Run(() => DocumentsGeneration.GeneratePaidContract(contractModel, sv.FileName));
                    new CustomMessageBoxView("Генерация ДКП успешно завершена!").ShowDialog();
                    service.CloseDialog();

                }
            }
        }
    }
}
