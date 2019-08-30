using System;
using System.Collections.Generic;
using WSX.DXF.Collections;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Defines classes that can be accessed by name. They are usually part of the dxf table section but can also be part of the objects section.
    /// </summary>
    public abstract class TableObject :
        DxfObject,
        IHasXData,
        ICloneable,
        IComparable,
        IComparable<TableObject>,
        IEquatable<TableObject>
    {
        #region delegates and events

        public delegate void NameChangedEventHandler(TableObject sender, TableObjectChangedEventArgs<string> e);
        public event NameChangedEventHandler NameChanged;
        protected virtual void OnNameChangedEvent(string oldName, string newName)
        {
            NameChangedEventHandler ae = this.NameChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<string> eventArgs = new TableObjectChangedEventArgs<string>(oldName, newName);
                ae(this, eventArgs);
            }
        }

        public event XDataAddAppRegEventHandler XDataAddAppReg;
        protected virtual void OnXDataAddAppRegEvent(ApplicationRegistry item)
        {
            XDataAddAppRegEventHandler ae = this.XDataAddAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        public event XDataRemoveAppRegEventHandler XDataRemoveAppReg;
        protected virtual void OnXDataRemoveAppRegEvent(ApplicationRegistry item)
        {
            XDataRemoveAppRegEventHandler ae = this.XDataRemoveAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        #endregion

        #region private fields

        private static readonly IReadOnlyList<string> invalidCharacters = new[] {"\\", "/", ":", "*", "?", "\"", "<", ">", "|", ";", ",", "=", "`"};
        private bool reserved;
        private string name;
        private readonly XDataDictionary xData;

        #endregion

        #region constructors

        protected TableObject(string name, string codeName, bool checkName)
            : base(codeName)
        {
            if (checkName)
            {
                if (!IsValidName(name))
                    throw new ArgumentException("The name should be at least one character long and the following characters \\<>/?\":;*|,=` are not supported.", nameof(name));
            }

            this.name = name;
            this.reserved = false;
            this.xData = new XDataDictionary();
            this.xData.AddAppReg += this.XData_AddAppReg;
            this.xData.RemoveAppReg += this.XData_RemoveAppReg;
        }

        #endregion

        #region public properties

        public string Name
        {
            get { return this.name; }
            set { this.SetName(value, true); }
        }

        public bool IsReserved
        {
            get { return this.reserved; }
            internal set { this.reserved = value; }
        }

        public static IReadOnlyList<string> InvalidCharacters
        {
            get { return invalidCharacters; }
        }

        public XDataDictionary XData
        {
            get { return this.xData; }
        }

        #endregion

        #region public methods

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            foreach (string s in InvalidCharacters)
            {
                if (name.Contains(s))
                    return false;
            }

            // using regular expressions is slower
            //if (Regex.IsMatch(name, "[\\<>/?\":;*|,=`]"))
            //    throw new ArgumentException("The following characters \\<>/?\":;*|,=` are not supported for table object names.", "name");

            return true;
        }

        #endregion

        #region internal methods

        internal void SetName(string newName, bool checkName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));
            if (this.IsReserved)
                throw new ArgumentException("Reserved table objects cannot be renamed.", nameof(newName));
            if (string.Equals(this.name, newName, StringComparison.OrdinalIgnoreCase))
                return;
            if (checkName)
                if (!IsValidName(newName))
                    throw new ArgumentException("The following characters \\<>/?\":;*|,=` are not supported for table object names.", nameof(newName));
            this.OnNameChangedEvent(this.name, newName);
            this.name = newName;
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region implements IComparable

        public int CompareTo(object other)
        {
            return this.CompareTo((TableObject) other);
        }

        public int CompareTo(TableObject other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return this.GetType() == other.GetType() ? string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) : 0;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        //////////////public static bool operator ==(TableObject u, TableObject v)
        //{
        //    if (ReferenceEquals(u, null) && ReferenceEquals(v, null))
        //        return true;

        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return false;

        //    return string.Equals(u.Name, v.Name, StringComparison.OrdinalIgnoreCase);
        //}

        //////////////public static bool operator !=(TableObject u, TableObject v)
        //{
        //    if (ReferenceEquals(u, null) && ReferenceEquals(v, null))
        //        return false;

        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return true;

        //    return !string.Equals(u.Name, v.Name, StringComparison.OrdinalIgnoreCase);
        //}

        //////////////public static bool operator <(TableObject u, TableObject v)
        //{
        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return false;

        //    return string.Compare(u.Name, v.Name, StringComparison.OrdinalIgnoreCase) < 0;
        //}

        //////////////public static bool operator >(TableObject u, TableObject v)
        //{
        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return false;

        //    return string.Compare(u.Name, v.Name, StringComparison.OrdinalIgnoreCase) > 0;
        //}

        #endregion

        #region implements IEquatable

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            if (this.GetType() != other.GetType())
                return false;

            return this.Equals((TableObject) other);
        }

        public bool Equals(TableObject other)
        {
            if (other == null)
                return false;

            return string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region ICloneable

        public abstract TableObject Clone(string newName);

        public abstract object Clone();

        #endregion

        #region XData events

        private void XData_AddAppReg(XDataDictionary sender, ObservableCollectionEventArgs<ApplicationRegistry> e)
        {
            this.OnXDataAddAppRegEvent(e.Item);
        }

        private void XData_RemoveAppReg(XDataDictionary sender, ObservableCollectionEventArgs<ApplicationRegistry> e)
        {
            this.OnXDataRemoveAppRegEvent(e.Item);
        }

        #endregion
    }
}