namespace WSX.DXF
{
    /// <summary>
    /// Represents the base class for all DXF objects.
    /// </summary>
    public abstract class DxfObject
    {
        #region private fields

        private string codename;
        private string handle;
        private DxfObject owner;

        #endregion

        #region constructors

        protected DxfObject(string codename)
        {
            this.codename = codename;
            this.handle = null;
            this.owner = null;
        }

        #endregion

        #region public properties

        public string CodeName
        {
            get { return this.codename; }
            protected set { this.codename = value; }
        }

        public string Handle
        {
            get { return this.handle; }
            internal set { this.handle = value; }
        }

        public DxfObject Owner
        {
            get { return this.owner; }
            internal set { this.owner = value; }
        }

        #endregion

        #region internal methods

        internal virtual long AsignHandle(long entityNumber)
        {
            this.handle = entityNumber.ToString("X");
            return entityNumber + 1;
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return this.codename;
        }

        #endregion
    }
}