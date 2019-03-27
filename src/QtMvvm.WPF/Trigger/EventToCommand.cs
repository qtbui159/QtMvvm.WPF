using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace QtMvvm.WPF.Trigger
{
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(EventToCommandProperty); }
            set { SetValue(EventToCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventToCommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventToCommand), new PropertyMetadata(null));
        
        public bool PassEventArgsToCommand
        {
            get { return (bool)GetValue(PassEventArgsToCommandProperty); }
            set { SetValue(PassEventArgsToCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassEventArgsToCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassEventArgsToCommandProperty =
            DependencyProperty.Register(nameof(PassEventArgsToCommand), typeof(bool), typeof(EventToCommand), new PropertyMetadata(false));
        

        protected override void Invoke(object parameter)
        {
            object param = null;
            if (PassEventArgsToCommand)
            {
                param = parameter;
            }

            Command?.Execute(param);
        }
    }
}
