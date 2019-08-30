using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.Logger;

namespace WSX.WSXCut.Manager
{
    /// <summary>
    /// 系统，绘图，警告日志显示
    /// </summary>
    public class LoggerManager
    {
        public static void AddDrawInfos(string msg)
        {
            Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, Tuple.Create(msg, GetColor(LogLevel.Info)));
        }
        public static void AddSystemInfos(string msg, LogLevel level)
        {
            Messenger.Instance.Send(MainEvent.OnAddLogSystemInfos, Tuple.Create(msg, GetColor(level)));
        }
        public static void AddAlarmInfos(string msg, LogLevel level)
        {
            Messenger.Instance.Send(MainEvent.OnAddLogAlarmInfos, Tuple.Create(msg, GetColor(level)));
        }
        private static Color GetColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug: return Color.Blue;
                case LogLevel.Error: return Color.Red;
                case LogLevel.Fatal: return Color.GreenYellow;
                case LogLevel.Info: return Color.Green;
                case LogLevel.Warn: return Color.LightSkyBlue;
                default: return Color.Black;
            }
        }
    }
}
