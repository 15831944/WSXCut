using System;
using System.Collections.Generic;
using System.Linq;
using WSX.Hardware.Models;

namespace WSX.Hardware.Common
{
    public static class Protocol
    {
        private static readonly uint RequestHeader = 0xAA55F00F;
        private static readonly uint ResponseHeader = 0x55AA0FF0;

        public static readonly List<AxisTypes> Axises = new List<AxisTypes>
        {
            AxisTypes.AxisX, AxisTypes.AxisY, AxisTypes.AxisZ,
            AxisTypes.AxisA, AxisTypes.AxisB, AxisTypes.AxisC,
            AxisTypes.AxisU, AxisTypes.AxisV, AxisTypes.AxisW
        };

        public static byte[] CreateRequest(uint cmd, uint[] content)
        {
            var res = new List<uint>();
            res.Add(cmd);
            if (content != null)
            {
                res.AddRange(content);
            }
            res.Add(GetSumCheck(res.ToArray()));
            res.Insert(0, (uint)res.Count - 1);
            res.Insert(0, RequestHeader);
            return GetBytes(res);
        }

        public static byte[] CreateResponse(uint cmd, uint[] content)
        {
            var res = new List<uint>();
            res.Add(cmd);
            if (content != null)
            {
                res.AddRange(content);
            }
            res.Add(GetSumCheck(res.ToArray()));
            res.Insert(0, (byte)(res.Count - 1));
            res.Insert(0, ResponseHeader);
            return GetBytes(res);
        }

        public static uint GetCmdType(uint[] items)
        {
            return items[2];
        }

        public static uint[] GetResponseContent(List<uint> response)
        {        
            return response.GetRange(3, response.Count - 4).ToArray();
        }

        public static byte[] ToBytes(uint content)
        {
            var bytes = BitConverter.GetBytes(content);
#if BIG_ENDIAN
            bytes = bytes.Reverse().ToArray();       
#endif
            return bytes;
        }

        public static uint ToUInt32(byte[] bytes)
        {
#if BIG_ENDIAN
            bytes = bytes.Reverse().ToArray();
#endif
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static byte[] GetBytes(List<uint> items)
        {
            var list = new List<byte>();
            foreach (var m in items)
            {
                list.AddRange(ToBytes(m));
            }
            return list.ToArray();
        }

        public static List<uint> GetList(byte[] bytes)
        {
            var list = new List<uint>();
            var tmp = new List<byte>(bytes);
            if (bytes.Length >= 4 && bytes.Length % 4 == 0)
            {
                for (int i = 0; i < bytes.Length; i += 4)
                {
                    list.Add(ToUInt32(tmp.GetRange(i, 4).ToArray()));
                }
            }
            return list;
        }

        private static uint GetSumCheck(uint[] content)
        {
            uint sum = 0;
            foreach (var m in content)
            {
                sum += m;
            }
            return sum;
        }

        public static bool Verify(byte[] bytes)
        {
            var list = GetList(bytes);
            if (!list.Any())
            {
                return false;
            }
            
            bool condition1 = list[0] != RequestHeader;
            bool condition2 = list[0] != ResponseHeader;
            if (condition1 && condition2)
            {
                return false;
            }

            //if (response[5] != cmd)
            //{
            //    return false;
            //}

            int len = list.Count - 3;
            if (len != list[1])
            {
                return false;
            }
    
            var part = list.GetRange(2, list.Count - 3).ToArray();
            uint sumCheck = GetSumCheck(part);
            if (sumCheck != list.Last())
            {
                return false;
            }

            return true;
        }

        public static bool SameAs(uint[] a, uint[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            bool res = true;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    res = false;
                    break;
                }
            }
            return res;
        }
    }
}
