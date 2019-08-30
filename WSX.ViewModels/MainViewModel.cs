using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.GlobalData.Model;
using WSX.ViewModels.CustomControl.Menu;
using WSX.ViewModels.Common;

namespace WSX.ViewModels
{
    [POCOViewModel()]
    public class MainViewModel
    {
        public OperationStatus OperStatus { get; private set; }

        public MainViewModel()
        {
            Messenger.Default.Register<object>(this, "OperStatusChanged", status => this.OperStatus = (OperationStatus)status);
        }

        public AdditionalInfo AdditionalInfo
        {
            get
            {
                return GlobalModel.Params.AdditionalInfo;
            }
            //set
            //{
            //    GlobalModel.Params.AdditionalInfo = value;
            //}
        }

        public void AddSysLog(string msg, Color color)
        {
            Messenger.Default.Send<object>(Tuple.Create(msg, color), "AddSysLog");
        }

       
    }
}
