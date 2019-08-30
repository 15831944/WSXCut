using System;
using System.Collections.Concurrent;

namespace WSX.CommomModel.Utilities
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
        }
    }
}
