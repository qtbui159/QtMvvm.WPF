using QtMvvm.WPF.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QtMvvm.WPF.Dialog.DefaultDialogs
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultDialog : Window
    {
        private DialogRequest m_Request = null;
        private string m_ViewID = null;

        public DefaultDialog(DialogRequest dialogRequest, string viewID)
        {
            InitializeComponent();

            m_Request = dialogRequest;
            m_ViewID = viewID;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Type viewType = RegionManager.GetViewTypeByViewID(m_ViewID);
            FrameworkElement frameworkElement = Activator.CreateInstance(viewType) as FrameworkElement;
            if (frameworkElement == null)
            {
                return;
            }

            IDialogAware dialogAware = frameworkElement.DataContext as IDialogAware;
            if (dialogAware != null)
            {
                dialogAware.DialogFinish = m_Request.Close;
                dialogAware.DialogInfomation = m_Request.DialogInformation;
            }

            contentControl.Content = frameworkElement;
        }
    }
}
