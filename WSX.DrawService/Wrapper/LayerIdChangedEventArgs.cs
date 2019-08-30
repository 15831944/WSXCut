using System;
using System.Collections.Generic;

namespace WSX.DrawService.Wrapper
{
    public class LayerIdChangedEventArgs : EventArgs
    {
        /// <summary>
        /// figureId, layerId
        /// </summary>
        public List<Tuple<string, int>> ChangedObjects;
    }
}
