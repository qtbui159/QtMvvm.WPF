using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QtMvvm.WPF.App
{
    public abstract class QtApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            RegisterModule();

            bool canCreate = PrepareCreateMainWindow();
            if (canCreate)
            {
                Window window = CreateMainWindow();
                window.Closed += (obj, args) =>
                {
                    Shutdown();
                };
                window.Show();
            }
            else
            {
                Shutdown();
            }
        }

        /// <summary>
        /// 注册模块
        /// </summary>
        protected abstract void RegisterModule();
        /// <summary>
        /// 创建主页面之前的动作，可以用作验证等等
        /// </summary>
        /// <returns>false为直接退出</returns>
        protected abstract bool PrepareCreateMainWindow();
        /// <summary>
        /// 创建主进程
        /// </summary>
        /// <returns></returns>
        protected abstract Window CreateMainWindow();
    }
}
