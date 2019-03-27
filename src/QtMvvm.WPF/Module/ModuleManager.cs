using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Module
{
    public static class ModuleManager
    {
        /// <summary>
        /// 加入模块，不可重复加入
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        public static void Add<TModule>() where TModule : IModule
        {
            TModule module = Activator.CreateInstance<TModule>();
            module.Register();
        }
    }
}
