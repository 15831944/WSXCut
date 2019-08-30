using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;

namespace WSX.ViewModels.Common
{
    public class DispatcherService : IDispatcherService
    {
        public void BeginInvoke(Action action)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(action);
        }
    }
}
