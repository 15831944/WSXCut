using System;

namespace WSX.GlobalData.Messenger
{
    public interface IMessenger
    {
        void Register(string token, Action<object> action);
        void UnRegister(string token);
        void UnRegisterAll();
        void Send(string token, object arg);
    }
}
