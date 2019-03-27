using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QtMvvm.WPF.Command
{
    public class RelayCommand : RelayCommandBase
    {
        private Action m_Action = null;
        private Func<bool> m_CanExecute = null;

        public RelayCommand(Action relayCommandMethod)
        {
            if (relayCommandMethod == null)
            {
                throw new ArgumentNullException(nameof(relayCommandMethod));
            }
            m_Action = relayCommandMethod;
        }

        public RelayCommand(Action relayCommandMethod, Func<bool> canExecuteMethod)
        {
            if (relayCommandMethod == null)
            {
                throw new ArgumentNullException(nameof(relayCommandMethod));
            }
            m_Action = relayCommandMethod;
            m_CanExecute = canExecuteMethod;
        }

        protected override void Execute(object obj)
        {
            m_Action.Invoke();
        }

        protected override bool CanExecute(object obj)
        {
            if (m_CanExecute == null)
            {
                return true;
            }
            return m_CanExecute.Invoke();
        }

        public void Execute()
        {
            Execute(null);
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }
    }

    public class RelayCommand<T> : RelayCommandBase
    {
        protected Action<T> m_Action = null;
        protected Func<bool> m_CanExecute = null;

        public RelayCommand(Action<T> relayCommandMethod)
        {
            if (relayCommandMethod == null)
            {
                throw new ArgumentNullException(nameof(relayCommandMethod));
            }
            m_Action = relayCommandMethod;
        }

        public RelayCommand(Action<T> relayCommandMethod, Func<bool> canExecuteMethod)
        {
            if (relayCommandMethod == null)
            {
                throw new ArgumentNullException(nameof(relayCommandMethod));
            }
            m_Action = relayCommandMethod;
            m_CanExecute = canExecuteMethod;
        }

        protected override void Execute(object obj)
        {
            m_Action.Invoke((T)obj);
        }

        protected override bool CanExecute(object obj)
        {
            if (m_CanExecute == null)
            {
                return true;
            }
            return m_CanExecute.Invoke();
        }

        public void Execute(T arg1)
        {
            Execute(arg1);
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }
    }
}
