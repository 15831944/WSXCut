using DevExpress.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.Logger;
using WSX.ViewModels.CustomControl;
using static WSX.DrawService.DrawCommand;

namespace WSX.WSXCut.Views.CustomControl
{
    public partial class UCLogDetail : UserControl
    {
        ToolTip tips;
        Font fontBoldForKeyDown = new Font("宋体", 10, FontStyle.Bold);
        public UCLogDetail()
        {
            InitializeComponent();
            RegisterAction();
            //InitToolTip();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(UCLogDetailViewModel);
            var viewModel = context.GetViewModel<UCLogDetailViewModel>();
            this.tabLogDetailInfo.SelectedPageChanging += (sender, e) =>
            {
                if (e.Page == this.tabDrawInfos && (!viewModel.DrawLogEnabled))
                {
                    e.Cancel = true;
                }
            };
            viewModel.Register("AddSysLog", x =>
            {
                this.tabLogDetailInfo.BeginInvoke(new Action(() =>
                {
                    this.tabLogDetailInfo.SelectedTabPageIndex = 1;
                }));
                this.AddSystemInfos(x);
            });
        }
        public void FocusWindowsToShowDrawMsg(object sender, char s)
        {
            if (rtxDrawInfos.Focused == false)
            {
                rtxDrawInfos_PreviewKeyDown(null, null);
                rtxDrawInfos.Focus();
                rtxDrawInfos.SelectionColor = Color.Blue;
                rtxDrawInfos.SelectionFont = fontBoldForKeyDown;
                rtxDrawInfos.AppendText(s.ToString());
                InputText = InputText + s.ToString();              
            }
        }

        private void RegisterAction()
        {
            Messenger.Instance.Register(MainEvent.OnAddLogAlarmInfos, AddAlarmInfos);
            Messenger.Instance.Register(MainEvent.OnAddLogDrawInfos, AddDrawInfos);
            Messenger.Instance.Register(MainEvent.OnAddLogSystemInfos, AddSystemInfos);
        }

        private CommandEntity commandEntity = new CommandEntity();
        private string pattern = @"(-?\d+)(\.\d+)?";
        private void AddDrawInfos(object obj)
        {
            List<Dictionary<string, Color>> keyValuePairs = new List<Dictionary<string, Color>>();            
            string msg = string.Empty;
            CommandEntity commandEntity1 = obj as CommandEntity;
            if (commandEntity1 != null)
            {
                #region 拦截字符串，小数点后面保留三位
                commandEntity1.Parameters?.ForEach(x => 
                {                
                    if (string.IsNullOrEmpty(x.ParameterValue))
                    {
                        return;
                    }

                    var items = Regex.Matches(x.ParameterValue, pattern);
                    if (items.Count != 0)
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            string str = double.Parse(items[i].ToString()).ToString("0.###");
                            x.ParameterValue = x.ParameterValue.Replace(items[i].ToString(), str);
                        }
                    }
                });
                #endregion

                if (commandEntity != null)
                {
                    //和上次命令不一致
                    if (string.IsNullOrEmpty(commandEntity.CanvasCommand) == false
                        && string.IsNullOrEmpty(commandEntity1.CanvasCommand) == false
                        && commandEntity.CanvasCommand.Equals(commandEntity1.CanvasCommand) == false)
                    {
                        if (string.IsNullOrEmpty(GetLineFromText()) == false)
                        {
                            msg = $"{Environment.NewLine} {"*取消*"} {Environment.NewLine}";
                        }
                        else
                        {
                            msg = $"{"*取消*"} {Environment.NewLine}";
                        }
                        keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                        commandEntity = null;
                    }
                }
                commandEntity = new CommandEntity()
                {
                    CanvasCommand = commandEntity1.CanvasCommand,
                    CommandExplain = commandEntity1.CommandExplain,
                    IsCancel = commandEntity1.IsCancel,
                    IsComplete = commandEntity1.IsComplete,
                    IsWritePatameter = commandEntity1.IsWritePatameter,
                };
                if (commandEntity1.Parameters != null && commandEntity1.Parameters.Count > 0)
                {
                    commandEntity.Parameters = new List<Patameter>();
                    foreach (var item in commandEntity1.Parameters)
                    {
                        commandEntity.Parameters.Add(new Patameter()
                        {
                            Explain = item.Explain,
                            ParameterId = item.ParameterId,
                            ParameterValue = item.ParameterValue,
                            IsShowValue = item.IsShowValue,
                            ParameterValueDataFormat = item.ParameterValueDataFormat,
                            DefaultParameterCommand = item.DefaultParameterCommand,
                            SubPatameter = item.SubPatameter,
                        });
                    }
                }
                if (commandEntity.IsCancel)
                {
                    if (string.IsNullOrEmpty(GetLineFromText()) == false)
                    {
                        msg = $"{Environment.NewLine} {"*取消*"} {Environment.NewLine}";
                    }
                    else
                    {
                        msg = $"{"*取消*"} {Environment.NewLine}";
                    }
                    keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                    commandEntity = null;
                }
                else
                {
                    if (commandEntity.Parameters == null || commandEntity.Parameters.Count <= 0)//单独命令
                    {
                        if (string.IsNullOrEmpty(GetLineFromText()) == false)
                        {
                            msg = $"{Environment.NewLine}{commandEntity.CommandExplain}{Environment.NewLine}";
                        }
                        else
                        {
                            msg = string.Format(@"{0}{1}", commandEntity.CommandExplain, Environment.NewLine);
                        }
                        keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                    }
                    else
                    {
                        var item = commandEntity.Parameters[commandEntity.Parameters.Count - 1];

                        if (string.IsNullOrEmpty(item.ParameterValue))
                        {
                            if (commandEntity.Parameters.Count == 1)
                            {
                                if (string.IsNullOrEmpty(GetLineFromText()) == false)
                                {
                                    msg = $"{Environment.NewLine}{commandEntity.CommandExplain}{Environment.NewLine}";
                                }
                                else
                                {
                                    msg = string.Format(@"{0}{1}", commandEntity.CommandExplain, Environment.NewLine);
                                }
                                msg = msg + string.Format(@"{0}", item.Explain);
                                keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                            }
                            else
                            {
                                if (commandEntity.Parameters[commandEntity.Parameters.Count - 2].IsShowValue == false)
                                {
                                    msg = string.Format(@"{0}", Environment.NewLine);
                                }
                                else
                                {
                                    msg = string.Format(@" ({0}){1}", commandEntity.Parameters[commandEntity.Parameters.Count - 2].ParameterValue, Environment.NewLine);

                                }
                                keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                                if (item.SubPatameter != null && item.SubPatameter.Count > 0)
                                {
                                    for (int i = 0; i < item.SubPatameter.Count; i++)
                                    {
                                        keyValuePairs.Add(AddDictionaryVale(item.SubPatameter[i].Explain, Color.Black));
                                        keyValuePairs.Add(AddDictionaryVale(item.SubPatameter[i].ParameterValue, Color.Green));
                                        if (i < (item.SubPatameter.Count - 1))
                                        {
                                            keyValuePairs.Add(AddDictionaryVale(",", Color.Black));
                                        }

                                    }
                                }
                                keyValuePairs.Add(AddDictionaryVale(item.Explain, Color.Black));
                            }
                        }
                        else//手动输入值后不需要重复显示
                        {
                            if (commandEntity.Parameters[commandEntity.Parameters.Count - 1].IsShowValue == false)
                            {
                                msg = string.Format(@"{0}", Environment.NewLine);
                            }
                            else
                            {
                                msg = string.Format(@" ({0}){1}", item.ParameterValue, Environment.NewLine);
                            }
                            keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                            if (item.SubPatameter != null && item.SubPatameter.Count > 0)
                            {
                                for (int i = 0; i < item.SubPatameter.Count; i++)
                                {
                                    keyValuePairs.Add(AddDictionaryVale(item.SubPatameter[i].Explain, Color.Black));
                                    keyValuePairs.Add(AddDictionaryVale(item.SubPatameter[i].ParameterValue, Color.Green));
                                    if (i < (item.SubPatameter.Count - 1))
                                    {
                                        keyValuePairs.Add(AddDictionaryVale(",", Color.Black));
                                    }

                                }
                            }
                        }
                    }
                    if (commandEntity.IsComplete)
                    {
                        if (string.IsNullOrEmpty(commandEntity.Parameters[commandEntity.Parameters.Count - 1].ParameterValue))
                        {
                            msg = $"{Environment.NewLine}{"完成"}{Environment.NewLine}";
                        }
                        else
                        {
                            msg = string.Format(@"{0}{1}", "完成", Environment.NewLine);
                        }
                        keyValuePairs.Add(AddDictionaryVale(msg, Color.Black));
                        commandEntity = null;
                    }
                }
            }
            this.rtxDrawInfos.Invoke(new Action(() =>
            {
                this.tabLogDetailInfo.SelectedTabPageIndex = 0;

                foreach (var item in keyValuePairs)
                {
                    rtxDrawInfos.SelectionColor = item.First().Value;
                    rtxDrawInfos.Focus();
                    rtxDrawInfos.Select(rtxDrawInfos.TextLength, 0);
                    rtxDrawInfos.ScrollToCaret();
                    rtxDrawInfos.AppendText(item.First().Key);
                }
                InputText = string.Empty;
            }));
        }
        private Dictionary<string,Color> AddDictionaryVale(string key,Color value)
        {
            Dictionary<string, Color> keyValues = new Dictionary<string, Color>();
            keyValues.Add(key, value);
            return keyValues;
        }
        /// <summary>
        /// 增加系统消息
        /// </summary>
        /// <param name="obj"></param>

        private void AddSystemInfos(object obj)
        {
            if (obj is object[])
            {
                #region Code by Liang
                object[] msgs = obj as object[];
                string time = $"({DateTime.Now.ToString("MM/dd HH:mm:ss")})";
                string content = msgs[0].ToString() + Environment.NewLine;
                //string msg = string.Format(@"({0}){1}{2}", time, msgs[0].ToString(), Environment.NewLine);
                string msg = time + content;
                LogLevel logLevel = (LogLevel)msgs[1];
                Color color = GetColor(logLevel);

                this.rtxSystemInfos.BeginInvoke(new Action(() =>
                {
                    rtxSystemInfos.AppendText(time);
                    rtxSystemInfos.Select(rtxSystemInfos.TextLength, 0);
                    rtxSystemInfos.SelectionColor = color;
                    rtxSystemInfos.AppendText(content);
                }));
                this.WriteLog(msg, logLevel);
                #endregion
            }
            else
            {
                #region Code by HB
                var logInfo = obj as Tuple<string, Color>;
                string time = $"({DateTime.Now.ToString("MM/dd HH:mm:ss")})";
                string content = logInfo.Item1.ToString() + Environment.NewLine;
                string msg = time + content;
                this.rtxSystemInfos.BeginInvoke(new Action(() =>
                {
                    rtxSystemInfos.AppendText(time);
                    rtxSystemInfos.Select(rtxSystemInfos.TextLength, 0);
                    rtxSystemInfos.SelectionColor = logInfo.Item2;
                    rtxSystemInfos.AppendText(content);
                }));
                this.WriteLog(msg, LogLevel.Info);
                #endregion
            }
        }
        /// <summary>
        /// 增加报警消息
        /// </summary>
        /// <param name="obj"></param>
        private void AddAlarmInfos(object obj)
        {
            //object[] msgs = obj as object[];
            //string msg = string.Format(@"({0}){1}\n", DateTime.Now.ToString(), msgs[0].ToString());
            //LogLevel logLevel = (LogLevel)msgs[1];
            //this.rtxDrawInfos.Invoke(new Action(() =>
            //{
            //    rtxDrawInfos.SelectionColor = GetColor(logLevel);
            //    rtxDrawInfos.AppendText(msg);
            //}));
        }

        private Color GetColor(LogLevel logLevel)
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

        private void WriteLog(string msg, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug: LogUtil.Instance.Debug(msg); break;
                case LogLevel.Error: LogUtil.Instance.Error(msg); break;
                case LogLevel.Fatal: LogUtil.Instance.Fatal(msg); break;
                case LogLevel.Info: LogUtil.Instance.Info(msg); break;
                case LogLevel.Warn: LogUtil.Instance.Warn(msg); break;
                default: break;
            }
        }

        private void UCLogDetail_Load(object sender, EventArgs e)
        {

        }

        private void InitToolTip()
        {
            tips = new ToolTip();
            tips.SetToolTip(this.rtxSystemInfos, "系统日志，双击可以查看详细历史记录！");
            tips.UseAnimation = true;
            tips.ShowAlways = true;
            tips.IsBalloon = true;
        }

        private string InputText = string.Empty;
        private void rtxDrawInfos_KeyDown(object sender, KeyEventArgs e)
        {
            rtxDrawInfos.SelectionColor = Color.Blue;
            rtxDrawInfos.SelectionFont = fontBoldForKeyDown;
            string lineStr = GetLineFromText();
            InputText = lineStr +e.KeyValue.ToString();
            if (e.KeyCode == Keys.Enter)//Enter
            {
                if(commandEntity != null &&commandEntity.IsWritePatameter==false)
                {
                    ShowErrorMsg(lineStr);
                    e.Handled = true;
                    return;
                }
                if (commandEntity != null && string.IsNullOrEmpty(commandEntity.CanvasCommand) == false)
                    if (commandEntity.IsCancel == false && commandEntity.IsComplete == false)
                    {
                        var item = commandEntity.Parameters[commandEntity.Parameters.Count - 1];
                        #region 参数规则判定

                        if (string.IsNullOrEmpty(lineStr))
                        {
                            ShowErrorMsg(lineStr);
                            e.Handled = true;
                            return;
                        }
                        if (item.ParameterValueDataFormat.Equals(DrawDataFormat.PointFormat.ToString()))
                        {

                            List<string> lst = lineStr.Split(',').ToList();
                            if (lst.Count == 2)
                            {
                                double x = 0;
                                bool boolx = double.TryParse(lst[0], out x);
                                double y = 0;
                                bool booly = double.TryParse(lst[0], out y);
                                if (boolx == false || booly == false)
                                {
                                    ShowErrorMsg(lineStr);
                                    e.Handled = true;
                                    return;
                                }
                            }
                            else
                            {
                                ShowErrorMsg(lineStr);
                                e.Handled = true;
                                return;
                            }
                        }

                        if (item.ParameterValueDataFormat.Equals(DrawDataFormat.FloatFormat.ToString()))
                        {

                            if (float.TryParse(lineStr, out float x) == false)
                            {
                                ShowErrorMsg(lineStr);
                                e.Handled = true;
                                return;
                            }

                        }

                        if (item.ParameterValueDataFormat.Equals(DrawDataFormat.DoubleFormat.ToString()))
                        {
                            if (double.TryParse(lineStr, out double x) == false)
                            {
                                ShowErrorMsg(lineStr);
                                e.Handled = true;
                                return;
                            }
                        }
                        if (item.ParameterValueDataFormat.Equals(DrawDataFormat.IntFormat.ToString()))
                        {
                            if (int.TryParse(lineStr, out int x) == false)
                            {
                                ShowErrorMsg(lineStr);
                                e.Handled = true;
                                return;
                            }
                        }

                        if (item.ParameterValueDataFormat.Equals(DrawDataFormat.PointOrCharFormat.ToString()))
                        {
                            List<string> lst = lineStr.Split(',').ToList();
                            if (lst.Count == 2)
                            {
                                if (double.TryParse(lst[0], out double z) == false || double.TryParse(lst[0], out double y) == false)
                                {
                                    ShowErrorMsg(lineStr);
                                    e.Handled = true;
                                    return;
                                }
                            }
                            else if (char.TryParse(lineStr, out char x) == false || item.DefaultParameterCommand.Contains(lineStr.ToLower()) == false)
                            {
                                ShowErrorMsg(lineStr);
                                e.Handled = true;
                                return;
                            }

                        }

                        if (item.ParameterValueDataFormat.Equals(DrawDataFormat.PointOrFloatFormat.ToString()))
                        {
                            List<string> lst = lineStr.Split(',').ToList();
                            if (lst.Count == 2)
                            {
                                if (double.TryParse(lst[0], out double z) == false || double.TryParse(lst[0], out double y) == false)
                                {
                                    ShowErrorMsg(lineStr);
                                    e.Handled = true;
                                    return;
                                }
                            }
                            else if (float.TryParse(lineStr, out float x) == false)
                            {
                                ShowErrorMsg(lineStr);
                                e.Handled = true;
                                return;
                            }

                        }
                        #endregion
                        if (string.IsNullOrEmpty(item.ParameterValue))
                        {
                            commandEntity.Parameters[commandEntity.Parameters.Count - 1].ParameterValue = lineStr;
                            Messenger.Instance.Send(MainEvent.OnInDataDrawCommand, commandEntity);
                        }
                        //reback
                    }
                e.Handled = true;
            }
        }

        public void ShowErrorMsg(string param)
        {
            rtxDrawInfos.AppendText(Environment.NewLine);
            rtxDrawInfos.AppendText(string.Format(@"参数{0}无效{1}", param, Environment.NewLine));
        }

        private void rtxDrawInfos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (string.IsNullOrEmpty(GetLineFromText()) == false)
            {
                if (string.IsNullOrEmpty(InputText))

                    rtxDrawInfos.AppendText(Environment.NewLine);
            }
        }

        private string GetLineFromText()
        {
            int index = rtxDrawInfos.GetFirstCharIndexOfCurrentLine();
            int line = rtxDrawInfos.GetLineFromCharIndex(index);
            string lineStr = "";
            if (line > 0)
            {
                lineStr = rtxDrawInfos.Lines[line];
            }
            return lineStr;
        }      
    }
}
