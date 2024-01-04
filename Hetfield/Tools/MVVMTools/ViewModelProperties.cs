using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ModernWpf.Controls;

namespace Hetfield.Tools
{
    internal class ViewModelProperties
    {
        #region LoadCommand
        public static DependencyProperty LoadedCommandProperty
        = DependencyProperty.RegisterAttached(
             "LoadedCommand",
             typeof(ICommand),
             typeof(ViewModelProperties),
             new PropertyMetadata(null, OnLoadedCommandChanged));

        private static void OnLoadedCommandChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = depObj as FrameworkElement;
            if (frameworkElement != null && e.NewValue is ICommand)
            {
                frameworkElement.Loaded
                  += (o, args) =>
                  {
                      (e.NewValue as ICommand).Execute(null);
                  };
            }
        }

        public static ICommand GetLoadedCommand(DependencyObject depObj)
        {
            return (ICommand)depObj.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(DependencyObject depObj, ICommand value)
        {
            depObj.SetValue(LoadedCommandProperty, value);
        }
        #endregion



        #region AutoSuggestTextBoxQuerySubbmitedCommand

        public static DependencyProperty QuerySubmittedProperty
        = DependencyProperty.RegisterAttached(
            "QuerySubmittedCommand",
            typeof(ICommand),
            typeof(ViewModelProperties),
            new PropertyMetadata(null, OnQuerySubmittedCommandChanged));

        private static void OnQuerySubmittedCommandChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = depObj as AutoSuggestBox;
            if (frameworkElement != null && e.NewValue is ICommand)
            {
                frameworkElement.QuerySubmitted
                  += (o, args) =>
                  {
                      (e.NewValue as ICommand).Execute(null);
                  };
            }
        }

        public static ICommand GetQuerySubmittedCommand(DependencyObject depObj)
        {
            return (ICommand)depObj.GetValue(QuerySubmittedProperty);
        }

        public static void SetQuerySubmittedCommand(DependencyObject depObj, ICommand value)
        {
            depObj.SetValue(QuerySubmittedProperty, value);
        }

        #endregion

    }
}
