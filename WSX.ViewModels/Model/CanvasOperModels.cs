using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.ViewModels.Model
{
    public enum CanvasOperTypes
    {
        Preview = 0x01,
        Move = 0x02
    }

    public static class CanvasOperTypesExtensions
    {
        public static bool IsPreview(this CanvasOperTypes operType)
        {
            return ((int)operType & 0x01) != 0;
        }

        public static bool IsMove(this CanvasOperTypes operType)
        {
            return ((int)operType & 0x02) != 0;
        }
    }

    public class CanvasOperParameter
    {
        public CanvasOperTypes OperType { get; set; }
        public bool? Selected { get; set; }
        public bool? NeedToSort { get; set; }
    }
}
