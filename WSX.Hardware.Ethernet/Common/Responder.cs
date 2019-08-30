using System.Collections.Generic;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Hardware.Common
{
    public class Responder
    {
        private readonly static uint[] SUCCESS_RESPONSE_DATA = { 0x4F, 0x4B };
        private readonly static uint[] FAILURE_RESPONSE_DATA = { 0x46, 0x49 };

        public static bool GetResponseFlag(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            if (Protocol.SameAs(content, SUCCESS_RESPONSE_DATA))
            {
                return true;
            }
            return false;
        }

        public static MotorInfoMap<MotorInfoUnit> GetMotorStatus(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            var result = new MotorInfoMap<MotorInfoUnit>();
            
            foreach (var m in Protocol.Axises)
            {
                int index = (int)m;
                result[m].Counter = (int)content[index];

                index += 9;
                result[m].Encoder = (int)content[index];

                index += 9;
                result[m].Status = (int)content[index];

                index += 9;
                result[m].Speed = (int)content[index];
            }

            return result;
        }

        public static string GetVersion(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            string version = null;
            foreach (var m in content)
            {
                version += m.ToString("X2");
            }     
            return version;
        }

        public static double GetHeight(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            return content[0] / 1000.0;
        }

        public static uint GetInputData(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            return content[0];
        }

        public static uint GetAnalogData(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            return content[0];
        }

        public static bool GetCacheStatus(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            return content[0] == 0x00;
        }

        public static uint GetMotorAlarmData(byte[] response)
        {
            var content = Protocol.GetResponseContent(Protocol.GetList(response));
            return content[0];
        }
    }
}
