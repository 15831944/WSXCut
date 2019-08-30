using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.GlobalData.Model;

namespace WSX.WSXCut
{
    [POCOViewModel()]
    public class MainViewModel
    {
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
    }
}
