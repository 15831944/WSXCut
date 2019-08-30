using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.Engine.Models
{
    public class MachineParameter
    {
        public OperationTypes OperationType { get; set; }
        public int MachiningCnt { get; set; } = 1;
        public double MachiningIntervalSeconds { get; set; }
        public PointF? ReturnPoint { get; set; }
        public bool IsReturnZeroWhenStop { get; set; }
        public bool IsOnlyMachineSelected { get; set; }
        public bool SoftwareLimitEnabled { get; set; }
        public bool EdgeDetectionEnabled { get; set; }
        public double Step { get; set; } = 10;
        public double StepSpeed { get; set; } = 10;
    }
}
