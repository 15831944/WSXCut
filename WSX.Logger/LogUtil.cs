using System;
using System.Reflection;
using log4net;

namespace WSX.Logger
{
    public class LogUtil
    {
        private static LogUtil instance;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private LogUtil() { }

        public static LogUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogUtil();
                }
                return instance;
            }
        }

        #region 属性
        public bool IsDebugEnabled
        {
            get { return log.IsDebugEnabled; }
        }
        public bool IsInfoEnabled
        {
            get { return log.IsInfoEnabled; }
        }
        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }
        public bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }
        public bool IsFatalEnabled
        {
            get { return log.IsFatalEnabled; }
        }
        #endregion

        #region [ 接口方法 ]

        #region [ Debug ]
        public void Debug(string message)
        {
            if (this.IsDebugEnabled)
            {
                this.Log(LogLevel.Debug, message);
            }
        }

        public void Debug(string message, Exception exception)
        {
            if (this.IsDebugEnabled)
            {
                this.Log(LogLevel.Debug, message, exception);
            }
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (this.IsDebugEnabled)
            {
                this.Log(LogLevel.Debug, format, args);
            }
        }

        public void DebugFormat(string format, Exception exception, params object[] args)
        {
            if (this.IsDebugEnabled)
            {
                this.Log(LogLevel.Debug, string.Format(format, args), exception);
            }
        }
        #endregion

        #region [ Info ]
        public void Info(string message)
        {
            if (this.IsInfoEnabled)
            {
                this.Log(LogLevel.Info, message);
            }
        }

        public void Info(string message, Exception exception)
        {
            if (this.IsInfoEnabled)
            {
                this.Log(LogLevel.Info, message, exception);
            }
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (this.IsInfoEnabled)
            {
                this.Log(LogLevel.Info, format, args);
            }
        }

        public void InfoFormat(string format, Exception exception, params object[] args)
        {
            if (this.IsInfoEnabled)
            {
                this.Log(LogLevel.Info, string.Format(format, args), exception);
            }
        }
        #endregion

        #region  [ Warn ]

        public void Warn(string message)
        {
            if (this.IsWarnEnabled)
            {
                this.Log(LogLevel.Warn, message);
            }
        }

        public void Warn(string message, Exception exception)
        {
            if (this.IsWarnEnabled)
            {
                this.Log(LogLevel.Warn, message, exception);
            }
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (this.IsWarnEnabled)
            {
                this.Log(LogLevel.Warn, format, args);
            }
        }

        public void WarnFormat(string format, Exception exception, params object[] args)
        {
            if (this.IsWarnEnabled)
            {
                this.Log(LogLevel.Warn, string.Format(format, args), exception);
            }
        }
        #endregion

        #region  [ Error ]

        public void Error(string message)
        {
            if (this.IsErrorEnabled)
            {
                this.Log(LogLevel.Error, message);
            }
        }

        public void Error(string message, Exception exception)
        {
            if (this.IsErrorEnabled)
            {
                this.Log(LogLevel.Error, message, exception);
            }
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (this.IsErrorEnabled)
            {
                this.Log(LogLevel.Error, format, args);
            }
        }

        public void ErrorFormat(string format, Exception exception, params object[] args)
        {
            if (this.IsErrorEnabled)
            {
                this.Log(LogLevel.Error, string.Format(format, args), exception);
            }
        }
        #endregion

        #region  [ Fatal ]

        public void Fatal(string message)
        {
            if (this.IsFatalEnabled)
            {
                this.Log(LogLevel.Fatal, message);
            }
        }

        public void Fatal(string message, Exception exception)
        {
            if (this.IsFatalEnabled)
            {
                this.Log(LogLevel.Fatal, message, exception);
            }
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (this.IsFatalEnabled)
            {
                this.Log(LogLevel.Fatal, format, args);
            }
        }

        public void FatalFormat(string format, Exception exception, params object[] args)
        {
            if (this.IsFatalEnabled)
            {
                this.Log(LogLevel.Fatal, string.Format(format, args), exception);
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 输出普通日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        private void Log(LogLevel level, string format, params object[] args)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    log.DebugFormat(format, args);
                    break;
                case LogLevel.Info:
                    log.InfoFormat(format, args);
                    break;
                case LogLevel.Warn:
                    log.WarnFormat(format, args);
                    break;
                case LogLevel.Error:
                    log.ErrorFormat(format, args);
                    break;
                case LogLevel.Fatal:
                    log.FatalFormat(format, args);
                    break;
            }
        }

        /// <summary>
        /// 格式化输出异常信息
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private void Log(LogLevel level, string message, Exception exception)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    log.Debug(message, exception);
                    break;
                case LogLevel.Info:
                    log.Info(message, exception);
                    break;
                case LogLevel.Warn:
                    log.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    log.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    log.Fatal(message, exception);
                    break;
            }
        }
    }
}