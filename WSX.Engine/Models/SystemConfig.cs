using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;
using WSX.Hardware.Models;
using WSX.Logger;

namespace WSX.CommomModel.SystemConfig
{
    public class AxisParameter
    {
       public double Ratio { get; set; }
       public double Distance { get; set; }
       public bool Reversed { get; set; }
    }

    public class SystemConfig
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public AxisParameter XPara { get; set; }
        public AxisParameter YPara { get; set; }
        public AxisParameter ZPara { get; set; }
        public uint Blowing1_Pin { get; set; }
        public uint Blowing2_Pin { get; set; }
        public double MaxSpeed { get; set; }
        public double MaxFrequency { get; set; }
        public double MinSpeed { get; set; }
        public double SpeedThreshold { get; set; }

        public SystemConfig()
        {
            this.IpAddress = "192.168.100.200";
            this.Port = 7;
            this.XPara = new AxisParameter
            {
                Ratio = 100,
                Distance = 100
            };
            this.YPara = new AxisParameter
            {
                Ratio = 100,
                Distance = 100
            };
            this.ZPara = new AxisParameter
            {
                Ratio = 100,
                Distance = 100
            };
            this.Blowing1_Pin = IOPinDefinition.Pin_0;
            this.Blowing2_Pin = IOPinDefinition.Pin_1;
            this.MaxSpeed = 200;
            this.MaxFrequency = 10000;
            this.MinSpeed = 20;
            this.SpeedThreshold = 60;
        }

        public RectangleF GetMachiningRegion()
        {
            return new RectangleF(0, 0, 150, 300);
        }

        public static SystemConfig Load(string path)
        {
            SystemConfig config = null;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string content = sr.ReadToEnd();
                    config = JsonConvert.DeserializeObject<SystemConfig>(content);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
            }

            if (config == null)
            {
                config = new SystemConfig();
                Save(config, path);
            }

            return config;
        }

        public static void Save(SystemConfig config, string path)
        {
            try
            {
                string content = JsonConvert.SerializeObject(config);
                content = SerializeUtil.FormatJsonString(content);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(content);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
            }
        }
    }
}
