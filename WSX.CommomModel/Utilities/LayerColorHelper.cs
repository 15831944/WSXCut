using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace WSX.CommomModel.Utilities
{
    public class LayerColorHelper    // : IDisposable
    {
        //private bool isDisposed = false;
        public Dictionary<string, Color> LayerColorInfo { get; private set; }
        private string dataPath;

        private static LayerColorHelper instance;
        private static object SyncRoot = new object();

        public bool Initialized { get; private set; }

        public static LayerColorHelper Instance
        {
            get
            {
                return instance ?? (instance = new LayerColorHelper());
            }
        }

        private LayerColorHelper()
        {
            this.Initialized = false;
        }

        public void Initialize(string path)
        {
            try
            {
                this.dataPath = path;
                if (!File.Exists(this.dataPath))
                {
                    this.CreateDefault(this.dataPath);
                }
                string content = null;
                using (StreamReader sr = new StreamReader(this.dataPath))
                {
                    content = sr.ReadToEnd();
                }
                this.LayerColorInfo = JsonConvert.DeserializeObject<Dictionary<string, Color>>(content);
                this.Initialized = true;
            }
            catch
            {

            }
            if (this.LayerColorInfo == null)
            {
                this.CreateDefault(this.dataPath);
                this.LayerColorInfo = this.GetDefaultColorMap();
            }
        }


        public void Update(string layerId, Color color)
        {
            if (this.LayerColorInfo.Keys.Contains(layerId))
            {
                this.LayerColorInfo[layerId] = color;
            }
        }

        public Color GetLayerColor(string layerId)
        {
            Color color = Color.White;
            if (this.LayerColorInfo.Keys.Contains(layerId))
            {
                color = this.LayerColorInfo[layerId];
            }
            return color;
        }

        public void Save()
        {
            this.SaveAs(this.dataPath, this.LayerColorInfo);
        }

        public void SaveAs(string filePath, Dictionary<string, Color> layerColorInfo)
        {
            string jsonContent = JsonConvert.SerializeObject(layerColorInfo);
            jsonContent = SerializeUtil.FormatJsonString(jsonContent);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(jsonContent);
            }
        }

        private void CreateDefault(string filePath)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            this.SaveAs(filePath, this.GetDefaultColorMap());
        }

        private Dictionary<string, Color> GetDefaultColorMap()
        {
            return new Dictionary<string, Color>
            {
               {"Layer1", Color.FromArgb(255, 255, 255)},
               {"Layer2", Color.FromArgb(255, 0, 0) },
               {"Layer3", Color.FromArgb(0, 255, 0)},
               {"Layer4", Color.FromArgb(0, 0, 255)},
               {"Layer5", Color.FromArgb(125, 125, 255)},

               {"Layer6", Color.FromArgb(255, 125, 125)},
               {"Layer7", Color.FromArgb(125, 255, 125)},
               {"Layer8", Color.FromArgb(125, 0, 0)},
               {"Layer9", Color.FromArgb(0, 125, 125)},
               {"Layer10",Color.FromArgb(255, 125, 0)},

               {"Layer11", Color.FromArgb(34, 177, 76)},
               {"Layer12", Color.FromArgb(163, 0, 164)},
               {"Layer13", Color.FromArgb(0, 64, 128)},
               {"Layer14", Color.FromArgb(0, 255, 255)},
               {"Layer15", Color.FromArgb(255, 0, 100)}
            };
        }

        ~LayerColorHelper()
        {
            if (this.LayerColorInfo != null && this.LayerColorInfo.Any())
            {
                this.SaveAs(this.dataPath, this.LayerColorInfo);
            }
        }
    }
}
