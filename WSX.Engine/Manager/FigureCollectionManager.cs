using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WSX.CommomModel.Utilities;
using WSX.Engine.Figure;
using WSX.GlobalData.Messenger;

namespace WSX.Engine.Manager
{
    public class FigureCollectionManager
    {
        private static FigureCollectionManager instance;
        private static object SyncRoot = new object();
        private List<FigureUnit> figureCollection = new List<FigureUnit>();
        private Dictionary<string, FigureUnit> figureMap = new Dictionary<string, FigureUnit>();
        private bool? isLaserOff = null;

        public event Action<FigureUnit> OnAdd;
        public event Action<string> OnRemove;
        public event Action<FigureUnit> OnUpdate;
        public event Action OnSelectedStatusChanged;
        public event Action OnClear;

        private FigureCollectionManager()
        {

        }

        public static FigureCollectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new FigureCollectionManager();
                        }
                    }
                }
                return instance;
            }
        }

        public static List<FigureUnit> ReadFromFile(string filePath)
        {
            List<FigureUnit> items = null;
            try
            {
                string content = null;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    content = sr.ReadToEnd();
                }
                items = JsonConvert.DeserializeObject<List<FigureUnit>>(content);
            }
            catch
            {
            }
            return items;
        }

        public static void WriteToFile(string filePath, List<FigureUnit> items)
        {
            try
            {
                string content = JsonConvert.SerializeObject(items);
                content = SerializeUtil.FormatJsonString(content);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(content);
                }
            }
            catch (Exception)
            {
            }
        }

        public bool IsValid()
        {
            return this.figureCollection.Any();
        }

        public void UpdateTempInfo(bool isLaserOff)
        {
            this.isLaserOff = (bool?)isLaserOff;
        }

        public void AddFigure(FigureUnit item)
        {
            if (this.isLaserOff != null)
            {
                item.IsLaserOff = this.isLaserOff.Value;
                this.isLaserOff = null;
            }
            this.figureCollection.Add(item);
            this.figureMap.Add(item.Id, item);
            this.OnAdd?.Invoke(item);
        }

        public void Remove(string id)
        {
            try
            {
                var item = this.figureMap[id];
                this.figureCollection.Remove(item);
                this.figureMap.Remove(id);
                this.OnRemove?.Invoke(id);
            }
            catch
            {

            }
        }

        public void UpdateSelectedStatus(List<string> idCollection)
        {
            foreach (var m in this.figureCollection)
            {
                m.IsSelected = false;
            }
            foreach (var m in idCollection)
            {
                this.figureMap[m].IsSelected = true;
            }
            this.SelectedObjectsChanged(idCollection);
            this.OnSelectedStatusChanged?.Invoke();
        }

        private void SelectedObjectsChanged(List<string> idCollection)
        {
            if (idCollection != null)
            {
                if (idCollection.Count > 0)
                {
                    Messenger.Instance.Send("OnSelectedObjectCountChanged", true);
                }
                else if (idCollection.Count == 0)
                {
                    Messenger.Instance.Send("OnSelectedObjectCountChanged", false);
                }
            }
        }

        public List<FigureUnit> TakeAll()
        {
            return this.figureCollection;
        }

        public void Update(string id, FigureUnit item)
        {
            try
            {
                var figure = this.figureMap[id];
                int index = this.figureCollection.IndexOf(figure);
                this.figureCollection[index] = item;
                this.figureMap[id] = item;
                this.OnUpdate?.Invoke(item);
            }
            catch
            {

            }
        }

        public void UpdateLayerId(string id, int layerId)
        {
            try
            {
                var figure = this.figureMap[id];
                figure.LayerId = layerId;
                this.OnUpdate?.Invoke(figure);
            }
            catch
            {

            }
        }

        public void Clear()
        {
            this.figureCollection.Clear();
            this.figureMap.Clear();
            this.OnClear?.Invoke();
        }

        public FigureUnit Take(string id)
        {

            FigureUnit figure = null;
            try
            {
                var temp = this.figureMap[id];
                figure = CopyUtil.DeepCopy<FigureUnit>(temp);
            }
            catch
            {

            }
            return figure;
        }

    }
}
