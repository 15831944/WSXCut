using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WSX.CommomModel.Utilities
{
    public static class CopyUtil
    {
        public static T DeepCopy<T>(T obj)
        {
            if (obj == null) return default(T);
            T ret;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                ret = (T)bf.Deserialize(ms);
                ms.Close();
            }
            return ret;
        }

        public static T DeepCopyBaseOnJSon<T>(T obj)
        {
            string tmp = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(tmp);
        }

        public static void CopyModel<T>(T target, T source) where T : class
        {
            if (target == null || source == null)
            {
                return;
            }

            Type type = typeof(T);
            var items = type.GetProperties();
            foreach (var m in items)
            {
                try
                {
                    m.SetValue(target, m.GetValue(source, null), null);
                }
                catch
                {
                   
                }
            }         
        }
    }
}
