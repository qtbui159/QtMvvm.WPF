using QtMvvm.WPF.App;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Example.Views;
using QtMvvm.WPF.Module;
using ModuleA;

namespace Example
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : QtApplication
    {
        protected override Window CreateMainWindow()
        {
            return new MainWindow();
        }

        protected override void RegisterModule()
        {
            
        }

        protected override bool PrepareCreateMainWindow()
        {
            ModuleManager.Add<ModuleAModule>();

            return true;
        }

    }
}
