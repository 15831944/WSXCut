using System.Collections.Generic;
using WSX.Engine.Models;

namespace WSX.Engine.Utilities
{
    public class DataSplitter
    {
        private readonly DataProvider provider;
        public bool IsValid { get; }
        private double startLen;
        private double endLen;

        public DataSplitter(List<DataUnit> collection, double startLen, double endLen)
        {
            this.provider = new DataProvider();
            this.provider.DataCollection = collection;
            this.IsValid = this.provider.TotalLen > (startLen + endLen);
            this.startLen = startLen;
            this.endLen = endLen;
        }

        public List<DataUnit> GetStartData()
        {
            if (!this.IsValid)
            {
                return null;
            }
            this.provider.CurrentLen = 0;
            return this.provider.GetMovementData(this.startLen);
        }

        public List<DataUnit> GetMiddleData()
        {
            if (!this.IsValid)
            {
                return null;
            }
            this.provider.CurrentLen = this.startLen;
            return this.provider.GetMovementData(this.provider.TotalLen - this.endLen - this.startLen);
        }

        public List<DataUnit> GetEndData()
        {
            if (!this.IsValid)
            {
                return null;
            }
            this.provider.CurrentLen = this.provider.TotalLen - this.endLen;
            return this.provider.GetMovementData(this.endLen);
        }      
    }
}
