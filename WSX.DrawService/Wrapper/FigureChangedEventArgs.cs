using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Wrapper
{
    public enum FigureChangedTypes
    {
        Add,     
        Update,
        Remove,
        Selected,
    }

    public class FigureChangedEventArgs: EventArgs
    {
        public FigureChangedTypes ChangedType { get; set; }
        public List<IDrawObject> DrawObjects { get; set; }
    }

    public class LayerIdChangedEventArgs : EventArgs
    {
        /// <summary>
        /// figureId, layerId
        /// </summary>
        public List<Tuple<string, string>> ChangedObjects;
    }
}
