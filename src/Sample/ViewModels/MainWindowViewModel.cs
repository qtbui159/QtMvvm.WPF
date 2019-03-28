using QtMvvm.WPF.Command;
using QtMvvm.WPF.Dialog;
using QtMvvm.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ViewModel
{
   public  class MainWindowViewModel: ViewModelBase
    {

        private string m_Title;
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                SetProperty(ref m_Title, value);
            }
        }
        

        public RelayCommand ShowMessageBoxCommand { get; private set; }


        public MainWindowViewModel()
        {
            Title = "Fuck";

            ShowMessageBoxCommand = new RelayCommand(RaiseShowMessageBoxCommand);
        }

        private void RaiseShowMessageBoxCommand()
        {
           var dialogRequest= DialogManager.Build("ViewA", true);
            dialogRequest.Raise();
            if (dialogRequest.DialogInformation.Confirm == true)
            {
               
            }
        }
    }
}
