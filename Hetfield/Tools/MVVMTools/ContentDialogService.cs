using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfield.Tools.MVVMTools
{
    internal class ContentDialogService
    {
        public static ContentDialog dialog;

        public static System.Windows.Controls.UserControl userControlInDialog;

        public ContentDialogService(ContentDialog dialog, System.Windows.Controls.UserControl userControlIndDialog)
        {
            ContentDialogService.dialog = dialog;
            ContentDialogService.userControlInDialog = userControlIndDialog;
        }

        public ContentDialogService(System.Windows.Controls.UserControl userControlIndDialog)
        {
            ContentDialogService.userControlInDialog = userControlIndDialog;
            if (dialog != null)
                dialog.Content = userControlIndDialog;
        }

        public ContentDialogService()
        {
            if (dialog == null)
                throw new Exception("Dialog Don't init");
            if (userControlInDialog == null)
                throw new Exception("Page in Dialog don't init");
            dialog.Content = userControlInDialog;
        }

        public async void OpenDialog()
        {
            dialog.ShowAsync();
        }

        public void CloseDialog()
        {
            dialog.Hide();
        }
    }
}
