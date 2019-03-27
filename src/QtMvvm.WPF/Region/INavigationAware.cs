using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Region
{
    public interface INavigationBase
    {
        /// <summary>
        /// 是否保存该ViewModel
        /// 如果为true,则View始终使用对应的唯一ViewModel
        /// 如果为false,则View每次都是使用重新创建的ViewModel
        /// </summary>
        /// <returns></returns>
        bool SaveViewModel();
    }

    public interface INavigationAware<T> : INavigationBase
    {
        /// <summary>
        /// 获取导航参数
        /// </summary>
        /// <param name="parameter"></param>
        void GetNavigationParameter(T parameter);
    }
}
