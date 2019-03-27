using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Dialog
{
    public static class DialogManager
    {
        /// <summary>
        /// 创建弹窗
        /// </summary>
        /// <param name="viewID"></param>
        /// <param name="isModal">是否是模态窗口</param>
        /// <param name="dialogInformation"></param>
        /// <returns></returns>
        public static DialogRequest Build(string viewID, bool isModal, DialogInformation dialogInformation = null)
        {
            DialogRequest dialogRequest = new DialogRequest(viewID, isModal, dialogInformation);
            return dialogRequest;
        }
    }
}
