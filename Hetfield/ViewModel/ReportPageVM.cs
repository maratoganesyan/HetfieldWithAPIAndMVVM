using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.DbUtils;
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
    internal class ReportPageVM : ViewModelBase
    {
        private DateTime _salesReportStartDate;

        private DateTime _salesReportEndDate;

        private DateTime _employeeReportStartDate;

        private DateTime _employeeReportEndDate;

        private User _selectedEmployee;

        private ICollection<User> _employees;

        private List<Order> orders;


        public DateTime SalesReportStartDate { get => _salesReportStartDate; set { _salesReportStartDate = value; OnPropertyChanged(nameof(SalesReportStartDate)); } }
        public DateTime SalesReportEndDate { get => _salesReportEndDate; set { _salesReportEndDate = value; OnPropertyChanged(nameof(SalesReportEndDate)); } }
        public DateTime EmployeeReportStartDate { get => _employeeReportStartDate; set { _employeeReportStartDate = value; OnPropertyChanged(nameof(EmployeeReportStartDate)); } }
        public DateTime EmployeeReportEndDate { get => _employeeReportEndDate; set { _employeeReportEndDate = value; OnPropertyChanged(nameof(EmployeeReportEndDate)); } }
        public User SelectedEmployee { get => _selectedEmployee; set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); } }
        public ICollection<User> Employees { get => _employees; set { _employees = value; OnPropertyChanged(nameof(Employees)); } }

        public ReportPageVM()
        {
            FillEmployees();
        }


        public RelayCommand GeneratSalesReportCommand => new RelayCommand(obj => GenerateSalesReport());
        public RelayCommand GeneratEmployeeReportCommand => new RelayCommand(obj => GenerateEmployeeReport());

        private async void FillEmployees()
        {
            ApiClient apiClient = new ApiClient();
            List<User> users = (await apiClient.GetAllEntityData<User>()).ToList();
            orders = (await apiClient.GetAllEntityData<Order>()).ToList();
            Employees = users.Where(u => orders.Any(o => o.IdStaff == u.IdUser)).ToList();
            SalesReportStartDate = orders.Min(p => p.DateOfOrder);
            SalesReportEndDate = orders.Max(p => p.DateOfOrder);
            EmployeeReportStartDate = SalesReportStartDate;
            EmployeeReportEndDate = SalesReportEndDate;
        }

        private async Task GenerateSalesReport()
        {
            List<Order> ordersForReport = orders.Where(p => SalesReportStartDate <= p.DateOfOrder && SalesReportEndDate >= p.DateOfOrder && p.IdOrderStatus == 2).ToList();
            if(ordersForReport.Count() == 0)
            {
                new CustomMessageBoxView("В данном промежутке времени не было сделок").ShowDialog();
                return;
            }
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                
                ContentDialogService.userControlInDialog = new LoadingUserControlView("Идёт генерация отчёта по совершенным сделкам. Подождите пожайлуста");
                ContentDialogService service = new ContentDialogService();
                service.OpenDialog();
                DateOnly startdate = new DateOnly(SalesReportStartDate.Year, SalesReportStartDate.Month, SalesReportStartDate.Day);
                DateOnly enddate = new DateOnly(SalesReportEndDate.Year, SalesReportEndDate.Month, SalesReportEndDate.Day);
                SalesReportModel Model = new SalesReportModel(ordersForReport, startdate, enddate);
                await Task.Run(() => SalesReportGeneration.GenerateWorkReport(Model, sv.FileName));
                new CustomMessageBoxView("Генерация отчёта по выполненным договорам успешно завершена!").ShowDialog();
                service.CloseDialog();
            }
        }

        private async Task GenerateEmployeeReport()
        {
            if (SelectedEmployee == null)
            {
                new CustomMessageBoxView("Сотрудник не выбран").ShowDialog();
                return;
            }
            List<Order> ordersForReport = orders.Where(p => EmployeeReportStartDate <= p.DateOfOrder && 
                                                                EmployeeReportEndDate >= p.DateOfOrder && 
                                                                p.IdStaff == SelectedEmployee.IdUser).ToList();
            if (ordersForReport.Count() == 0)
            {
                new CustomMessageBoxView("В данном промежутке времени не было сделок").ShowDialog();
                return;
            }
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {

                ContentDialogService.userControlInDialog = new LoadingUserControlView("Идёт генерация отчёта по сделкам работника. Подождите пожайлуста");
                ContentDialogService service = new ContentDialogService();
                service.OpenDialog();
                DateOnly startdate = new DateOnly(EmployeeReportStartDate.Year, EmployeeReportStartDate.Month, EmployeeReportStartDate.Day);
                DateOnly enddate = new DateOnly(EmployeeReportEndDate.Year, EmployeeReportEndDate.Month, EmployeeReportEndDate.Day);
                EmployeeReportModel Model = new EmployeeReportModel(SelectedEmployee, ordersForReport, startdate, enddate);
                await Task.Run(() => EmployeeReportGeneration.GenerateEmployeeReport(Model, sv.FileName));
                new CustomMessageBoxView("Генерация отчёта по выполненным договорам успешно завершена!").ShowDialog();
                service.CloseDialog();
            }
        }
    }
}
