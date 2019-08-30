using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WSX.ViewModels.Common
{
    public class ViewModelExtensions
    {
        private Dictionary<string, Action<object>> cmdMap = new Dictionary<string, Action<object>>();

        public void Register(string id, Action<object> handler)
        {
            if (!this.cmdMap.Keys.Contains(id))
            {
                this.cmdMap.Add(id, handler);
            }
            else
            {
                this.cmdMap[id] += handler;
            }
        }

        public void Unregister(string id)
        {
            if (!this.cmdMap.Keys.Contains(id))
            {
                this.cmdMap.Remove(id);
            }
        }

        public virtual void ExecuteCmd(string id, object arg)
        {
            if (this.cmdMap.Keys.Contains(id))
            {
                this.cmdMap[id].Invoke(arg);
            }
        }

        #region Service
        protected IDispatcherService DispatcherService
        {
            get
            {
                return new DispatcherService();
                //return this.GetService<IDispatcherService>();
            }
        }

        protected IMessageBoxService MsgBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }
        #endregion
    }
}
