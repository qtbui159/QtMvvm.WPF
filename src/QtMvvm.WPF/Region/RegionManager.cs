using QtMvvm.WPF.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QtMvvm.WPF.Region
{
    public static partial class RegionManager
    {
        private static Dictionary<string, Type> m_ViewIDMapView = new Dictionary<string, Type>();
        private static Dictionary<string, ContentControl> m_RegionNameMapControl = new Dictionary<string, ContentControl>();

        /// <summary>
        /// 注册View
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="viewID">ID不可重复</param>
        public static void Register<TView>(string viewID)
        {
            if (m_ViewIDMapView.ContainsKey(viewID))
            {
                throw new Exception($"重复注册{viewID}");
            }

            m_ViewIDMapView.Add(viewID, typeof(TView));
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="regionName"></param>
        /// <param name="viewID"></param>
        public static void Navigate(string regionName, string viewID)
        {
            if (!m_RegionNameMapControl.ContainsKey(regionName))
            {
                throw new Exception($"不存在此RegionName {regionName}");
            }
            if (!m_ViewIDMapView.ContainsKey(viewID))
            {
                throw new Exception($"不存在此ViewID {viewID}");
            }

            Type viewType = m_ViewIDMapView[viewID];
            ContentControl control = m_RegionNameMapControl[regionName];

            control.Content = Activator.CreateInstance(viewType);
        }

        /// <summary>
        /// 带参数导航
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="regionName"></param>
        /// <param name="viewID"></param>
        /// <param name="parameter">参数实体</param>
        public static void Navigate<T>(string regionName, string viewID, T parameter)
        {
            if (!m_RegionNameMapControl.ContainsKey(regionName))
            {
                throw new Exception($"不存在此RegionName {regionName}");
            }
            if (!m_ViewIDMapView.ContainsKey(viewID))
            {
                throw new Exception($"不存在此ViewID {viewID}");
            }

            Type viewType = m_ViewIDMapView[viewID];
            ContentControl control = m_RegionNameMapControl[regionName];
            object view = Activator.CreateInstance(viewType);

            FrameworkElement fe = view as FrameworkElement;
            if (fe != null)
            {
                INavigationAware<T> aware = fe.DataContext as INavigationAware<T>;
                aware?.GetNavigationParameter(parameter);
            }
            
            control.Content = view;
        }

        internal static Type GetViewTypeByViewID(string viewID)
        {
            if (!m_ViewIDMapView.ContainsKey(viewID))
            {
                throw new Exception($"不存在此ViewID {viewID}");
            }

            Type viewType = m_ViewIDMapView[viewID];
            return viewType;
        }

        private static void Register(string regionName, ContentControl control)
        {
            if (m_RegionNameMapControl.ContainsKey(regionName))
            {
                throw new Exception($"重复注册{regionName}");
            }

            m_RegionNameMapControl.Add(regionName, control);
        }
    }

    public static partial class RegionManager
    {
        public static string GetRegionName(DependencyObject obj)
        {
            return (string)obj.GetValue(RegionNameProperty);
        }

        public static void SetRegionName(DependencyObject obj, string value)
        {
            obj.SetValue(RegionNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for RegionName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.RegisterAttached("RegionName", typeof(string), typeof(RegionManager), new PropertyMetadata(null, RegionNameChanged));

        private static void RegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignHelper.IsInDesignMode())
            {
                return;
            }

            ContentControl control = d as ContentControl;
            if (control == null)
            {
                return;
            }

            string value = e.NewValue as string;
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            Register(e.NewValue as string, control);
        }
    }
}
