using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.ViewModels.Common;

namespace WSX.ViewModels.CustomControl
{
    public class UCLogDetailViewModel: ViewModelExtensions
    {
        public bool DrawLogEnabled { get; set; } = true;

        public UCLogDetailViewModel()
        {
            Messenger.Default.Register<object>(this, "AddSysLog", this.OnSystemLogAdd);
        }

        private void OnSystemLogAdd(object arg)
        {
            this.ExecuteCmd("AddSysLog", arg);
        }

        private void OnCanvasLogAdd(object arg)
        {

        }

        private void OnErrorLogAdd(object arg)
        {

        }
    }
}
