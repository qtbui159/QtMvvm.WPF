using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Module
{
    public interface IModule
    {
        /// <summary>
        /// 请使用RegionManager来注册View
        /// </summary>
        void Register();
    }
}
