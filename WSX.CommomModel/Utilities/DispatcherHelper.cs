using System;
using System.Windows.Forms;

namespace WSX.CommomModel.Utilities
{
    public static class DispatcherHelper
    {
        private static Control dispatcher;

        public static void Initialize(Control owner)
        {
            dispatcher = owner;
        }

        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (dispatcher == null)
            {
                throw new Exception("Please initialize firstly before the operation!");
            }

            if (dispatcher.InvokeRequired)
            {
                dispatcher.BeginInvoke(new Action(() => action()));
            }
            else
            {
                action();
            }
        }

        public static void CheckInvokeOnUI(Action action)
        {
            if (dispatcher == null)
            {
                throw new Exception("Please initialize firstly before the operation!");
            }

            if (dispatcher.InvokeRequired)
            {
                dispatcher.Invoke(new Action(() => action()));
            }
            else
            {
                action();
            }
        }
    }
}
