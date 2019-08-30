#region WSX.DXF library, Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections;
using System.Reflection;

namespace WSX.DXF
{
    #region Class StringEnum

    /// <summary>
    /// Helper class for working with 'extended' enums using <see cref="StringValueAttribute"/> attributes.
    /// </summary>
    public class StringEnum
    {
        #region Instance implementation

        private readonly Type enumType;
        private static readonly Hashtable stringValues = new Hashtable();

        public StringEnum(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));

            if (!enumType.IsEnum)
                throw new ArgumentException(string.Format("The supplied type \"{0}\" must be an Enum.", enumType));

            this.enumType = enumType;
        }

        public string GetStringValue(string valueName)
        {
            string stringValue;

            try
            {
                Enum type = (Enum)Enum.Parse(this.enumType, valueName);
                stringValue = GetStringValue(type);
            }
            catch
            {
                return null;
            }

            return stringValue;
        }

        public Array GetStringValues()
        {
            ArrayList values = new ArrayList();
            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in this.enumType.GetFields())
            {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                        values.Add(attrs[0].Value);
            }

            return values.ToArray();
        }

        public IList GetListValues()
        {
            Type underlyingType = Enum.GetUnderlyingType(this.enumType);

            ArrayList values = new ArrayList();
            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in this.enumType.GetFields())
            {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                    {
                        object str = Convert.ChangeType(Enum.Parse(this.enumType, fi.Name), underlyingType);
                        if (str == null)
                            throw new Exception();
                        values.Add(new DictionaryEntry(str, attrs[0].Value));
                    }
            }

            return values;
        }

        public bool IsStringDefined(string value)
        {
            return Parse(this.enumType, value) != null;
        }

        public bool IsStringDefined(string value, StringComparison comparisonType)
        {
            return Parse(this.enumType, value, comparisonType) != null;
        }

        /// <value></value>
        public Type EnumType
        {
            get { return this.enumType; }
        }

        #endregion

        #region Static implementation

        public static string GetStringValue(Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            string output = null;
            Type type = value.GetType();

            if (stringValues.ContainsKey(value))
                output = ((StringValueAttribute) stringValues[value]).Value;
            else
            {
                //Look for our 'StringValueAttribute' in the field's custom attributes
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                    {
                        stringValues.Add(value, attrs[0]);
                        output = attrs[0].Value;
                    }
            }
            return output;
        }

        public static object Parse(Type type, string value)
        {
            return Parse(type, value, StringComparison.Ordinal);
        }

        public static object Parse(Type type, string value, StringComparison comparisonType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            object output = null;
            string enumStringValue = null;

            if (!type.IsEnum)
                throw new ArgumentException(string.Format("The supplied type \"{0}\" must be an Enum.", type));

            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in type.GetFields())
            {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                        enumStringValue = attrs[0].Value;

                //Check for equality then select actual enum value.
                if (string.Compare(enumStringValue, value, comparisonType) == 0)
                {
                    if (Enum.IsDefined(type, fi.Name))
                        output = Enum.Parse(type, fi.Name);
                    break;
                }
            }

            return output;
        }

        public static bool IsStringDefined(Type enumType, string value)
        {
            return Parse(enumType, value) != null;
        }

        public static bool IsStringDefined(Type enumType, string value, StringComparison comparisonType)
        {
            return Parse(enumType, value, comparisonType) != null;
        }

        #endregion
    }

    #endregion

    #region Class StringValueAttribute

    /// <summary>
    /// Simple attribute class for storing String Values
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringValueAttribute : Attribute
    {
        private readonly string value;

        public StringValueAttribute(string value)
        {
            this.value = value;
        }

        /// <value></value>
        public string Value
        {
            get { return this.value; }
        }
    }

    #endregion
}