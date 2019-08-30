using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace WSX.CommomModel.Utilities
{
    public class SerializeUtil
    {
        public static byte[] Serialize<T>(T obj)
        {
            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                buffer = new byte[ms.Length];
                ms.Read(buffer, 0, (int)ms.Length);
                ms.Close();
            }
            return buffer;
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            T ret;
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(buffer, 0, (int)buffer.Length);
                ms.Position = 0;
                BinaryFormatter bf = new BinaryFormatter();
                ret = (T)bf.Deserialize(ms);
                ms.Close();
            }
            return ret;
        }

        public static string FormatJsonString(string str)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// json 保存到文件
        /// </summary>
        /// <param name="para"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool JsonWriteToFile(object para, string fileName)
        {
            try
            {
                string filePath = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                File.WriteAllText(fileName, FormatJsonString(JsonConvert.SerializeObject(para)));
            }
            catch (System.Exception ex)
            {
                return false;
                //throw;
            }
            return true;
        }

        /// <summary>
        /// json读取文件数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T JsonReadByFile<T>(string fileName)
        {
            try
            {             
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            }
            catch (System.Exception ex)
            {
                return default(T);
            }
        }

        public static string SerializeXml(object data)
        {
            if (data == null) return string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(data.GetType());
                xz.Serialize(sw, data);
                return sw.ToString();
            }
        }
    }
}
