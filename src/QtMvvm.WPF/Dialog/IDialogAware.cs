using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Dialog
{
    public interface IDialogAware
    {
        Action DialogFinish { get; set; }
        DialogInformation DialogInfomation { get; set; }
    }
}
