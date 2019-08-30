using DevExpress.XtraBars;
using System;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace WSX.GlobalData.Messenger
{
    public class Messenger : IMessenger
    {
        private ConcurrentDictionary<string, Action<object>> actionMap = new ConcurrentDictionary<string, Action<object>>();
        private static object SyncRoot = new object();
        private static Messenger instance;

        public static Messenger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Messenger();
                        }
                    }
                }
                return instance;
            }
        }

        public void Register(string token, Action<object> action)
        {
            if (!this.actionMap.ContainsKey(token))
            {
                this.actionMap.TryAdd(token, action);
            }
            else
            {
                this.actionMap[token] += action;
            }
        }

        public void UnRegister(string token)
        {
            Action<object> action = null;
            this.actionMap.TryRemove(token, out action);
        }

        public void UnRegisterAll()
        {
            this.actionMap.Clear();
        }

        public void Send(string token, object arg)
        {
            Action<object> action = null;
            if (this.actionMap.TryGetValue(token, out action))
            {
                action.Invoke(arg);
            }
            else
            {
                #region 事件测试
                BarItem item = arg as BarItem;
                if (item != null)
                    MessageBox.Show("该功能尚未开发，敬请期待！", item.Name, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk, System.Windows.Forms.MessageBoxDefaultButton.Button1); 
                #endregion
            }
        }
    }
}
