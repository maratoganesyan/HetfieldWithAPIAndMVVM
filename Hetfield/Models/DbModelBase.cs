using Hetfield.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hetfield.Models
{
    public abstract class DbModelBase
    {
        public virtual async Task<bool> Validate(bool addMode)
        {
            //MyValidationBody
            throw new NotImplementedException();
        }

        protected bool ValidateResult(string message)
        {
            if (message != string.Empty)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new CustomMessageBoxView(message).ShowDialog();

                });
                return false;
            }
            return true;
        }
    }
}
