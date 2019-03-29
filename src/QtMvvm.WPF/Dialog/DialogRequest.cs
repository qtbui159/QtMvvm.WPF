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
        public DialogRequest SetSizeToContent(SizeToContent sizeToContent)
        {
            InnerWindow.SizeToContent = sizeToContent;
            return this;
        }

        public DialogRequest SetOwner(Window window)
        {
            InnerWindow.Owner = window;
            return this;
        }

        /// <summary>
        ///  获取或设置窗口首次显示时的位置
        /// </summary>
        /// <param name="windowStartupLocation"></param>
        /// <returns></returns>
        public DialogRequest SetWindowStartupLocation(WindowStartupLocation windowStartupLocation)
        {
            InnerWindow.WindowStartupLocation = windowStartupLocation;
            return this;
        }

        /// <summary>
        /// 设置窗口的宽度和高度
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public DialogRequest SetWidthAndHeight(double width, double height)
        {
            InnerWindow.Width = width;
            InnerWindow.Height = height;
            return this;
        }

        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public DialogRequest SetTitle(string title)
        {
            InnerWindow.Title = title;
            return this;
        }
    }
}
