using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QtMvvm.WPF.Design
{
    static class DesignHelper
    {
        /// <summary>
        /// 判断现在是设计模式还是运行模式
        /// </summary>
        /// <returns></returns>
        public static bool IsInDesignMode()
        {
            return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
        }
    }
}
