using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.ViewModel
{
    static class ViewModelResposity
    {
        private static Dictionary<Type, object> m_TypeMapViewModel = new Dictionary<Type, object>();

        public static object Get(Type vmType)
        {
            if (!m_TypeMapViewModel.ContainsKey(vmType))
            {
                return null;
            }

            return m_TypeMapViewModel[vmType];
        }
        
        public static void Save(object viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            Type type = viewModel.GetType();
            if (m_TypeMapViewModel.ContainsKey(type))
            {
                m_TypeMapViewModel[type] = viewModel;
            }
            else
            {
                m_TypeMapViewModel.Add(type, viewModel);
            }
        }
    }
}
