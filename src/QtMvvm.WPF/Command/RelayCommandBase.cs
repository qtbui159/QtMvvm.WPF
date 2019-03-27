using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QtMvvm.WPF.Command
{
    public abstract class RelayCommandBase : ICommand
    {
        private event EventHandler m_CanExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                m_CanExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                m_CanExecuteChanged -= value;
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        public virtual void RaiseCanExecuteChanged()
        {
            m_CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void Execute(object obj);
        protected abstract bool CanExecute(object obj);
    }
}
