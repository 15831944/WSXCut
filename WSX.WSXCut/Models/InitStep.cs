using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.WSXCut.Models
{
    public class InitStep
    {
        private Action excute;

        public InitStep(Action action, string description)
        {
            this.excute = action;
            Description = description;
        }

        public void Excute()
        {
            if (this.excute != null)
            {
                this.excute();
            }
        }

        public string Description { get; private set; }
    }
}
