using System;
using System.Collections.Generic;
using WSX.DXF.Tables;

namespace WSX.DXF
{
    /// <summary>
    /// Represents the extended data information.
    /// </summary>
    /// <remarks>
    /// Do not store your own data under the ACAD application registry it is used by some entities to store special data,
    /// it might be overwritten when the file is saved. Instead, create a new application registry and store your data there.
    /// </remarks>
    public class XData :
        ICloneable
    {
        #region private fields

        private ApplicationRegistry appReg;
        private readonly List<XDataRecord> xData;

        #endregion

        #region constructors

        public XData(ApplicationRegistry appReg)
        {
            if(appReg == null)
                throw new ArgumentNullException(nameof(appReg));
            this.appReg = appReg;
            this.xData = new List<XDataRecord>();
        }

        #endregion

        #region public properties

        public ApplicationRegistry ApplicationRegistry
        {
            get { return this.appReg; }
            internal set { this.appReg = value; }
        }

        public List<XDataRecord> XDataRecord
        {
            get { return this.xData; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return this.appReg.Name;
        }

        #endregion

        #region implements ICloneable

        public object Clone()
        {
            XData xdata = new XData((ApplicationRegistry) this.appReg.Clone());
            foreach (XDataRecord record in this.xData)
                xdata.XDataRecord.Add(new XDataRecord(record.Code, record.Value));

            return xdata;
        }

        #endregion
    }
}