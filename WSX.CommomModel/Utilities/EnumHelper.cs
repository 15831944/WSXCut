using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WSX.CommomModel.Utilities
{
    public static class EnumHelper
    {
        public static List<T> GetAllEnumMembers<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Only support Enum!");
            }

            List<T> list = new List<T>();
            T t = default(T);
            foreach (var m in Enum.GetValues(t.GetType()))
            {
                list.Add((T)m);
            }
            return list;
        }

        public static string GetDescription(this Enum source)
        {
            string description = null;
            Type type = source.GetType();
            FieldInfo[] fields = type.GetFields();
            var attr = fields.First(x => x.Name == source.ToString()).GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
            {
                description = attr.Description;
            }
            return description;
        }
    }
}