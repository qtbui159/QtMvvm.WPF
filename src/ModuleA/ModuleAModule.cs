using ModuleA.Views;
using QtMvvm.WPF.Module;
using QtMvvm.WPF.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        public void Register()
        {
            RegionManager.Register<ViewA>(nameof(ViewA));
        }
    }
}
