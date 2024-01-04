using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Animations;

namespace Hetfield.Tools
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

        private FrameworkElement ElementForAnimation { get; set; }

        public ViewModelBase(Action closeAction, FrameworkElement elementForAnimation)
        {
            CloseAction = closeAction;
            ElementForAnimation = elementForAnimation;
        }

        public ViewModelBase(Action closeAction)
        {
            CloseAction = closeAction;
        }
        public ViewModelBase()
        {
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void StartAnimation(int duration)
        {
            if (ElementForAnimation != null)
                StartAnimation(ElementForAnimation, duration);
        }

        protected void StartAnimation(FrameworkElement elementForAnimation, int duration)
        {
            Transitions.ApplyTransition(elementForAnimation, TransitionType.FadeInWithSlide, duration);
        }
    }
}
