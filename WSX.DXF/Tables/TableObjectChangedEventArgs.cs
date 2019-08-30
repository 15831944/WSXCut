using System;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Event data for changes or substitutions of table objects in entities or other tables.
    /// </summary>
    /// <typeparam name="T">A table object</typeparam>
    public class TableObjectChangedEventArgs<T> :EventArgs
    {
        #region private fields

        private readonly T oldValue;
        private T newValue;

        #endregion

        #region constructor

        public TableObjectChangedEventArgs(T oldTable, T newTable)
        {
            this.oldValue = oldTable;
            this.newValue = newTable;
        }

        #endregion

        #region public properties

        public T OldValue
        {
            get { return this.oldValue; }
        }

        public T NewValue
        {
            get { return this.newValue; }
            set { this.newValue = value; }
        }

        #endregion
    }
}