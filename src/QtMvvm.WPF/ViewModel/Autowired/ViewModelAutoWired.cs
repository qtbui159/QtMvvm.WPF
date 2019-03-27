using QtMvvm.WPF.Design;
using QtMvvm.WPF.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QtMvvm.WPF.ViewModel.Autowired
{
    public class ViewModelAutoWired
    {
        public static bool GetValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, bool value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(bool), typeof(ViewModelAutoWired), new PropertyMetadata(false,ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignHelper.IsInDesignMode())
            {
                return;
            }

            bool value = (bool)e.NewValue;
            if (!value)
            {
                return;
            }

            FrameworkElement fe = d as FrameworkElement;
            if (fe == null)
            {
                return;
            }

            Type type = FindViewModel(fe);
            if (type == null)
            {
                return;
            }

            Type interfaceType = typeof(INavigationBase);
            object vm = null;

            //没有继承INavigationAware，默认SaveViewModel=false

            if (interfaceType.IsAssignableFrom(type))
            {
                //查下这个type是否被缓存了
                object tmp = ViewModelResposity.Get(type);
                if (tmp == null)
                {
                    tmp = Activator.CreateInstance(type);
                }

                INavigationBase @base = tmp as INavigationBase;
                bool isSave = @base.SaveViewModel();
                if (isSave)
                {
                    vm = tmp;
                }
            }

            if (vm == null)
            {
                vm = Activator.CreateInstance(type);
            }

            ViewModelResposity.Save(vm);
            fe.DataContext = vm;
        }

        private static Type FindViewModel(FrameworkElement fe)
        {
            Type[] types = fe.GetType().Assembly.GetTypes();

            const string viewModelSuffix1 = "Model";
            const string viewModelSuffix2 = "ViewModel";

            Dictionary<string, Type> nameMapType = types.ToDictionary(x => x.Name.ToLower(), x => x);
            string viewTypeName = fe.GetType().Name;
            List<string> destNames = new List<string>()
            {
                (viewTypeName + viewModelSuffix1).ToLower(),
                (viewTypeName + viewModelSuffix2).ToLower(),
            };

            foreach (string name in nameMapType.Keys)
            {
                foreach (string viewTypeNameWithSuffix in destNames)
                {
                    if (name == viewTypeNameWithSuffix)
                    {
                        return nameMapType[name];
                    }
                }
            }

            return null;
        }
    }
}
