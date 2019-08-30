using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSX.ViewModels.Forms
{

    public class FrmCircleViewModel
    {
        public virtual int Count { get; set; } = 1;
        public virtual double Interval { get; set; } = 1;
        public virtual bool Normal { get; set; } = true;
        public virtual bool ClearMachineCount { get; set; } = false;
        public virtual bool MachineImmediately { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        public DialogResult Result { get; set; } = DialogResult.Cancel;
    }
}
