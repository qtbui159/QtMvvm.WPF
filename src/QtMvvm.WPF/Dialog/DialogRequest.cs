using QtMvvm.WPF.Dialog.DefaultDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QtMvvm.WPF.Dialog
{
    public class DialogRequest
    {
        #region 属性

        /// <summary>
        /// 窗口标签
        /// </summary>
        public String Title
        {
            get
            {
                return InnerWindow.Title;
            }
            set
            {
                InnerWindow.Title = value;
            }
        }

        /// <summary>
        /// 获取或设置高度
        /// </summary>
        public double Height
        {
            get
            {
                return InnerWindow.Height;
            }
            set
            {
                InnerWindow.Height = value;
            }
        }

        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public double Width
        {
            get
            {
                return InnerWindow.Width;
            }
            set
            {
                InnerWindow.Width = value;
            }
        }


        /// <summary>
        /// 获取或设置窗口首次显示时的位置。
        /// </summary>
        public bool ShowInTaskbar
        {
            get
            {
                return InnerWindow.ShowInTaskbar;
            }
            set
            {
                InnerWindow.ShowInTaskbar = value;
            }
        }

        /// <summary>
        ///  获取或设置窗口首次显示时的位置
        /// </summary>
        public WindowStartupLocation WindowStartupLocation
        {
            get
            {
                return InnerWindow.WindowStartupLocation;
            }
            set
            {
                InnerWindow.WindowStartupLocation = value;
            }
        }

        /// <summary>
        ///  获取或设置一个值，该值指示窗口的工作区是否支持透明。
        /// </summary>
        public bool AllowsTransparency
        {
            get
            {
                return InnerWindow.AllowsTransparency;
            }
            set
            {
                InnerWindow.AllowsTransparency = value;
            }
        }


        public Window Owner
        {
            get
            {
                return InnerWindow.Owner;
            }
            set
            {
                InnerWindow.Owner = value;
            }
        }



        #endregion

        public Window InnerWindow { private set; get; }

        private bool m_IsModal = false;
        public DialogInformation DialogInformation { private set; get; }

        private Action<DialogInformation> m_ClosedAction = null;

        internal DialogRequest(string viewID, bool isModal, DialogInformation dialogInformation)
        {
            InnerWindow = new DefaultDialog(this, viewID);
            InnerWindow.Closed += InnerWindow_Closed;

            m_IsModal = isModal;
            if (dialogInformation == null)
            {
                DialogInformation = new DialogInformation() { Confirm = false };
            }
            else
            {
                DialogInformation = dialogInformation;
            }
        }
        
        /// <summary>
        /// 显示
        /// </summary>
        public void Raise()
        {
            if (m_IsModal)
            {
                InnerWindow.ShowDialog();
            }
            else
            {
                InnerWindow.Show();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            InnerWindow.Close();
        }

        /// <summary>
        /// 设置关闭后动作
        /// </summary>
        /// <param name="action"></param>
        public DialogRequest SetClosedAction(Action<DialogInformation> action) 
        {
            m_ClosedAction = action;
            return this;
        }

        private void InnerWindow_Closed(object sender, EventArgs e)
        {
            m_ClosedAction?.Invoke(DialogInformation);
        }

        /// <summary>
        /// 获取或设置一个值，该值指示窗口是否自动调整自身大小以适应其内容大小
        /// </summary>
        /// <param name="sizeToContent"></param>
        /// <returns></returns>
        public DialogRequest SizeToContent(SizeToContent sizeToContent)
        {
            InnerWindow.SizeToContent = sizeToContent;
            return this;
        }
    }
}
