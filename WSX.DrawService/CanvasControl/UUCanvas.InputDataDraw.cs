using System.Collections.Generic;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine;
using WSX.DrawService.Utils;
using WSX.DrawService.Wrapper;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using static WSX.DrawService.DrawCommand;

namespace WSX.DrawService.CanvasControl
{
    public partial class UCCanvas
    {
        public CommandEntity CommandEntityDraw = new CommandEntity();
        public CommandEntity LastCommandEntityDraw = new CommandEntity();

        private void OnInDrawCommand(object obj)
        {
            CommandEntity commandEntity = obj as CommandEntity;
            if (obj == null) return;
            if (string.IsNullOrEmpty(commandEntity.CanvasCommand) == false)
            {
                CommandEntityDraw = obj as CommandEntity;
            }
            this.commandType = CommandType.Draw;
            ///value不重复显示
            CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].IsShowValue = false;
            UnitPoint unitPoint = new UnitPoint();
            if (CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValueDataFormat.Equals(DrawDataFormat.PointFormat.ToString()))
            {
                UnitPoint temp = StringToUniPoint(CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue, ',');
                unitPoint.X = temp.X;
                unitPoint.Y = temp.Y;
                this.HandleMouseDownWhenDrawing(unitPoint, null, null);
            }
            else if (CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValueDataFormat.Equals(DrawDataFormat.FloatFormat.ToString()))
            {
                float radius = float.Parse(CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue);
                if (drawObjectId.Equals(CanvasCommands.CircleCR.ToString()))
                {
                    UnitPoint temp = StringToUniPoint(CommandEntityDraw.Parameters[0].ParameterValue, ',');
                    unitPoint.X = temp.X + (double)radius;
                    unitPoint.Y = temp.Y;
                    this.HandleMouseDownWhenDrawing(unitPoint, null, null);
                }

                if (drawObjectId.Equals(CanvasCommands.ArcCR.ToString()))
                {
                    var drawcom = NewObject as SweepArc;
                    drawcom.InRadiusValue = radius;

                    SendDrawMsg(drawObjectId, radius.ToString());
                }
            }
            else if (CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValueDataFormat.Equals(DrawDataFormat.IntFormat.ToString()))
            {
                //目前使用在多边形和星形
                if (drawObjectId.Equals(CanvasCommands.Hexagon.ToString())
                    || drawObjectId.Equals(CanvasCommands.StarCommon.ToString()))
                {
                    SendDrawMsg(drawObjectId, CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue);
                }
            }
            else if (CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValueDataFormat.Equals(DrawDataFormat.PointOrCharFormat.ToString()))
            {
                if (drawObjectId.Equals(CanvasCommands.MultiLine.ToString()))
                {
                    if (CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue.ToLower().Equals("c"))
                    {
                        var drawcom = NewObject as PolyLine;
                        if (drawcom != null)
                        {
                            drawcom.IsCloseFigure = true;
                            drawcom.IsCompleteDraw = true;
                            CommandEntityDraw.IsComplete = true;
                            this.HandleMouseDownWhenDrawing(drawcom.FirstDrawPoint, null, null);
                            this.DoInvalidate(true);
                        }
                        DrawComplere(obj.ToString());
                    }
                }
            }
            else if (CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValueDataFormat.Equals(DrawDataFormat.PointOrFloatFormat.ToString()))
            {
                if (float.TryParse(CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue, out float f))
                {
                    UnitPoint u1 = StringToUniPoint(CommandEntityDraw.Parameters[0].ParameterValue, ',');
                    UnitPoint u2 = StringToUniPoint(CommandEntityDraw.Parameters[1].ParameterValue, ',');
                    unitPoint = (u1.X > u2.X) ? (u1) : u2;
                    unitPoint.X = unitPoint.X - f;
                }
                else
                {
                    UnitPoint temp = StringToUniPoint(CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue, ',');
                    unitPoint.X = temp.X;
                    unitPoint.Y = temp.Y;
                }
                this.HandleMouseDownWhenDrawing(unitPoint, null, null);
            }
        }
        private UnitPoint StringToUniPoint(string param, char splitChar)
        {
            UnitPoint unitPoint = new UnitPoint();

            List<string> pam = param.Split(splitChar).ToList();
            double x = 0;
            double.TryParse(pam[0], out x);
            double y = 0;
            double.TryParse(pam[1], out y);
            unitPoint.X = x;
            unitPoint.Y = y;
            return unitPoint;
        }

        /// <summary>
        /// 发送绘图信息
        /// </summary>
        /// <param name="drawObjectId"></param>
        /// <param name="obj"></param>
        public void SendDrawMsg(string drawObjectId, string obj, List<Patameter> SubPatameter = null)
        {
            if (CommandEntityDraw.IsCancel || CommandEntityDraw.IsComplete) return;
            ReDraw();
            if (CommandEntityDraw == null || CommandEntityDraw.Parameters == null || CommandEntityDraw.Parameters.Count <= 0)
            {
                return;
            }
            {//draw
                if (CanvasCommands.SingleDot.ToString().Equals(drawObjectId))
                {
                    DrawComplere(obj); ;
                }

                if (CanvasCommands.Lines.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);
                    if (CommandEntityDraw.Parameters.Count == 1)
                    {
                        AddParameters("请指定下一点:", DrawDataFormat.PointFormat);
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                    else
                    {
                        DrawComplere(obj);
                    }
                }
                if (CanvasCommands.MultiLine.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);

                    if (CommandEntityDraw.Parameters.Count < 3)
                    {
                        AddParameters("请指定下一点:", DrawDataFormat.PointFormat);
                    }
                    else
                    {
                        AddParameters("请指定下一点: 闭合(C)", DrawDataFormat.PointOrCharFormat, new List<string>() { "c" });
                    }
                    Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                }
                if (CanvasCommands.SingleRectangle.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);
                    if (CommandEntityDraw.Parameters.Count == 1)
                    {
                        AddParameters("请指定对角点:", DrawDataFormat.PointFormat);
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                    else
                    {
                        DrawComplere(obj);
                    }
                }
                if (CanvasCommands.CircleCR.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);
                    if (CommandEntityDraw.Parameters.Count == 1)
                    {
                        AddParameters("请指定半径:", DrawDataFormat.FloatFormat);
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                    else
                    {
                        UnitPoint p1 = new UnitPoint();
                        p1.X = double.Parse(CommandEntityDraw.Parameters[0].ParameterValue.Split(',')[0]);
                        p1.Y = double.Parse(CommandEntityDraw.Parameters[0].ParameterValue.Split(',')[1]);

                        UnitPoint p2 = new UnitPoint();
                        p2.X = double.Parse(obj.Split(',')[0]);
                        p2.Y = double.Parse(obj.Split(',')[1]);

                        double distance = HitUtil.Distance(p1, p2);
                        DrawComplere(distance.ToString());
                    }
                }
                if (CanvasCommands.Arc3P.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);
                    if (CommandEntityDraw.Parameters.Count == 3)
                    {
                        DrawComplere(obj);
                    }
                    else
                    {
                        AddParameters("请指定下一点:", DrawDataFormat.PointFormat);
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                }///
                if (CanvasCommands.ArcCR.ToString().Equals(drawObjectId))//圆弧
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);

                    if ((CommandEntityDraw.Parameters.Count == 3 && float.TryParse(CommandEntityDraw.Parameters[1].ParameterValue, out float f) == false)
                        || CommandEntityDraw.Parameters.Count == 4)

                    {
                        DrawComplere(obj);
                    }
                    else
                    {
                        if (CommandEntityDraw.Parameters.Count == 1)
                            AddParameters("请指定半径:", DrawDataFormat.FloatFormat);
                        else if (CommandEntityDraw.Parameters.Count == 2)
                        {
                            if (float.TryParse(CommandEntityDraw.Parameters[1].ParameterValue, out float q))
                            {
                                AddParameters("请指定起点:", DrawDataFormat.PointFormat);
                            }
                            else
                            {
                                AddParameters("请指定终点:", DrawDataFormat.PointFormat);
                            }
                        }
                        else
                        {
                            AddParameters("请指定终点:", DrawDataFormat.PointFormat);
                        }
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                }
                if (CanvasCommands.RoundRect.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);
                    if (CommandEntityDraw.Parameters.Count == 3)
                    {
                        DrawComplere(obj);
                    }
                    else
                    {
                        if (CommandEntityDraw.Parameters.Count == 1)
                            AddParameters("请指定对角点:", DrawDataFormat.PointFormat);
                        else if (CommandEntityDraw.Parameters.Count == 2)
                            AddParameters("请指定圆角半径或[倒角]:", DrawDataFormat.PointOrFloatFormat);
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                }

                if (CanvasCommands.Hexagon.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);

                    if (CommandEntityDraw.Parameters.Count == 3)
                    {
                        DrawComplere(obj);
                    }
                    else
                    {
                        if (CommandEntityDraw.Parameters.Count == 1)
                        {
                            string sendmmsg = "请指定圆心:";
                            string sidecount = "";
                            if (int.TryParse(obj, out int result) == false)//单击生成

                            {
                                if (LastCommandEntityDraw != null &&
                                    string.IsNullOrEmpty(LastCommandEntityDraw.CanvasCommand) == false
                                    && LastCommandEntityDraw.CanvasCommand.Equals(drawObjectId))
                                {
                                    if (LastCommandEntityDraw.Parameters != null && LastCommandEntityDraw.Parameters.Count > 0
                                        && string.IsNullOrEmpty(LastCommandEntityDraw.Parameters[0].ParameterValue) == false)
                                    {
                                        sidecount = sendmmsg = LastCommandEntityDraw.Parameters[0].ParameterValue;
                                    }
                                }
                                else
                                {
                                    var drawcom = new WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine.PolygonCommon();
                                    sidecount = drawcom.SideCount.ToString();
                                }
                                sendmmsg = $"使用上次边数: {sidecount} 请指定圆心:";

                                SetParameterValue(sidecount, DrawDataFormat.IntFormat, true);
                                AddParameters(sendmmsg, DrawDataFormat.PointFormat);
                                Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                                SetParameterValue(obj, DrawDataFormat.PointFormat);
                                AddParameters("请指定顶点:", DrawDataFormat.PointFormat);
                            }
                            else
                            {
                                AddParameters(sendmmsg, DrawDataFormat.PointFormat);
                            }
                        }
                        else if (CommandEntityDraw.Parameters.Count == 2)
                        {
                            AddParameters("请指定顶点:", DrawDataFormat.PointFormat);
                        }
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                }
                if (CanvasCommands.StarCommon.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);

                    if (CommandEntityDraw.Parameters.Count == 4)
                    {
                        DrawComplere(obj);
                    }
                    else
                    {
                        if (CommandEntityDraw.Parameters.Count == 1)
                        {
                            string sendmmsg = "请指定圆心:";
                            string sidecount = "";

                            if (int.TryParse(obj, out int result) == false)//单击生成
                            {
                                if (LastCommandEntityDraw != null &&
                                    string.IsNullOrEmpty(LastCommandEntityDraw.CanvasCommand) == false
                                    && LastCommandEntityDraw.CanvasCommand.Equals(drawObjectId))
                                {
                                    if (LastCommandEntityDraw.Parameters != null && LastCommandEntityDraw.Parameters.Count > 0
                                        && string.IsNullOrEmpty(LastCommandEntityDraw.Parameters[0].ParameterValue) == false)
                                    {
                                        sidecount = sendmmsg = LastCommandEntityDraw.Parameters[0].ParameterValue;
                                    }
                                }
                                else
                                {
                                    var drawcom = new WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine.StarCommon();
                                    sidecount = drawcom.SideCount.ToString();
                                }
                                sendmmsg = $"使用上次边数: {sidecount} 请指定圆心:";

                                SetParameterValue(sidecount, DrawDataFormat.IntFormat, true);
                                AddParameters(sendmmsg, DrawDataFormat.PointFormat);
                                Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                                SetParameterValue(obj, DrawDataFormat.PointFormat);
                                AddParameters("请指定顶点:", DrawDataFormat.PointFormat);
                            }
                            else
                            {
                                AddParameters(sendmmsg, DrawDataFormat.PointFormat);
                            }
                        }
                        else if (CommandEntityDraw.Parameters.Count == 2)
                        {
                            AddParameters("请指定顶点:", DrawDataFormat.PointFormat);
                        }
                        else if (CommandEntityDraw.Parameters.Count == 3)
                        {
                            AddParameters("请指定内顶点:", DrawDataFormat.PointFormat);
                        }
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                }
            }
            {//command
                if (CanvasCommands.Measure.ToString().Equals(drawObjectId))
                {
                    SetParameterValue(obj, DrawDataFormat.PointFormat);
                    if (CommandEntityDraw.Parameters.Count == 1)
                    {
                        AddParameters("请指定测量终点:", DrawDataFormat.PointFormat);
                        Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                    }
                    else if (CommandEntityDraw.Parameters.Count == 2)
                    {
                        DrawComplete(obj, SubPatameter, false);
                    }

                }
            }
        }

        public void ReDraw()
        {
            ///第二次绘图使用点击后重复上次绘图效果
            if (CommandEntityDraw == null || string.IsNullOrEmpty(CommandEntityDraw.CanvasCommand))
            {
                if (LastCommandEntityDraw != null && string.IsNullOrEmpty(LastCommandEntityDraw.CanvasCommand) == false)
                {
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = LastCommandEntityDraw.CanvasCommand,
                        CommandExplain = LastCommandEntityDraw.CommandExplain,
                        Parameters = new List<Patameter>()
                            {
                                new Patameter(){
                                    Explain =LastCommandEntityDraw.Parameters[0].Explain,
                                 ParameterId=LastCommandEntityDraw.Parameters[0].ParameterId,
                                  ParameterValueDataFormat=LastCommandEntityDraw.Parameters[0].ParameterValueDataFormat
                                }
                            }
                    };
                    Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                }
            }
        }

        private void SetParameterValue(string pValue, DrawDataFormat dataFormat, bool isChange = false)
        {
            if (CommandEntityDraw != null && CommandEntityDraw.Parameters != null && CommandEntityDraw.Parameters.Count > 0)
            {
                if (string.IsNullOrEmpty(CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue) || isChange)
                {
                    CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue = pValue;
                    CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValueDataFormat = dataFormat.ToString();
                }
            }
        }

        private void AddParameters(string explain, DrawDataFormat dataFormat, List<string> dafaultCommand = null)
        {
            CommandEntityDraw.Parameters.Add(new Patameter()
            {
                Explain = explain,
                ParameterId = CommandEntityDraw.Parameters.Count + 1,
                ParameterValueDataFormat = dataFormat.ToString(),
                DefaultParameterCommand = dafaultCommand
            });
        }

        private void DrawComplere(string obj, bool IsShowComplete = true)
        {
            if (CommandEntityDraw != null && CommandEntityDraw.Parameters != null && CommandEntityDraw.Parameters.Count > 0)
            {
                CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue = obj;
                if (IsShowComplete)//非绘图命令不用设置
                {
                    CommandEntityDraw.IsComplete = true;
                }
                Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                DrawFinshReset();
            }
        }
        private void DrawComplete(string obj, List<Patameter> subPatamter, bool IsShowComplete = true)
        {
            if (CommandEntityDraw != null && CommandEntityDraw.Parameters != null && CommandEntityDraw.Parameters.Count > 0)
            {
                CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].ParameterValue = obj;
                if (subPatamter != null && subPatamter.Count > 0)
                {
                    CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].SubPatameter = new List<Patameter>();
                    subPatamter.ForEach(s =>
                    {
                        CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].SubPatameter.Add(
                            new Patameter()
                            {
                                DefaultParameterCommand = s.DefaultParameterCommand,
                                Explain = s.Explain,
                                IsShowValue = s.IsShowValue,
                                ParameterId = s.ParameterId,
                                ParameterValue = s.ParameterValue
                            });
                    });
                }
                if (IsShowComplete)//非绘图命令不用设置
                {
                    CommandEntityDraw.IsComplete = true;
                }
                Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                DrawFinshReset();
            }
        }
        private void DrawFinshReset()
        {
            CommandEntity commandEntity = new CommandEntity()
            {
                CanvasCommand = CommandEntityDraw.CanvasCommand,
                CommandExplain = CommandEntityDraw.CommandExplain,
                IsCancel = CommandEntityDraw.IsCancel,
                IsComplete = CommandEntityDraw.IsComplete,
            };
            if (CommandEntityDraw.Parameters != null && CommandEntityDraw.Parameters.Count > 0)
            {
                commandEntity.Parameters = new List<Patameter>();
                foreach (var item in CommandEntityDraw.Parameters)
                {
                    commandEntity.Parameters.Add(new Patameter()
                    {
                        Explain = item.Explain,
                        ParameterId = item.ParameterId,
                        ParameterValue = item.ParameterValue,
                        IsShowValue = item.IsShowValue,
                        ParameterValueDataFormat = item.ParameterValueDataFormat
                    });
                }
            }
            LastCommandEntityDraw = commandEntity;
            commandEntity = new CommandEntity();
            CommandEntityDraw = new CommandEntity();
        }

        public void DrawButtonStatus(bool isComplete)
        {
            if (CommandEntityDraw != null && string.IsNullOrEmpty(CommandEntityDraw.CanvasCommand) == false)
            {
                CommandEntityDraw.Parameters[CommandEntityDraw.Parameters.Count - 1].IsShowValue = false;
                if (isComplete)
                {
                    CommandEntityDraw.IsComplete = true;
                    Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                }
                else
                {
                    CommandEntityDraw.IsCancel = true;
                    Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);
                }
                CommandEntityDraw = new CommandEntity();
            }
        }

        private void SetDrawComponetAttribute()
        {
            if (CanvasCommands.Hexagon.ToString().Equals(drawObjectId))
            {
                if (CommandEntityDraw.Parameters.Count > 1)
                {
                    if (string.IsNullOrEmpty(CommandEntityDraw.Parameters[0].ParameterValue) == false)//当前输入数值
                    {
                        var drawcom = NewObject as WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine.PolygonCommon;
                        drawcom.SideCount = int.Parse(CommandEntityDraw.Parameters[0].ParameterValue);
                    }
                }
            }
            if (CanvasCommands.StarCommon.ToString().Equals(drawObjectId))
            {
                if (CommandEntityDraw.Parameters.Count > 1)
                {
                    if (string.IsNullOrEmpty(CommandEntityDraw.Parameters[0].ParameterValue) == false)//当前输入数值
                    {
                        var drawcom = NewObject as WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine.StarCommon;
                        drawcom.SideCount = int.Parse(CommandEntityDraw.Parameters[0].ParameterValue);
                    }
                }
            }
        }

        /// <summary>
        /// 发送命令至绘图消息栏
        /// </summary>
        /// <param name="drawObjectId"></param>
        private void SendCommandMSG(string drawObjectId)
        {
            CommandEntityDraw = new CommandEntity();
            {//Draw
                if (drawObjectId.Equals(CanvasCommands.SingleDot.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.SingleDot.ToString(),
                        CommandExplain = "命令: 新孤立点",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定位置：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.Lines.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.Lines.ToString(),
                        CommandExplain = "命令: 新直线",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定起点：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.MultiLine.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.MultiLine.ToString(),
                        CommandExplain = "命令: 新多段线",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定起点：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.SingleRectangle.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.SingleRectangle.ToString(),
                        CommandExplain = "命令: 新矩形",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定起点：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.RoundRect.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.RoundRect.ToString(),
                        CommandExplain = "命令: 新圆角矩形矩形",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定起点：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.Hexagon.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.Hexagon.ToString(),
                        CommandExplain = "命令: 新多边形",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定顶点数[3-100]：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.IntFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.StarCommon.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.StarCommon.ToString(),
                        CommandExplain = "命令: 星形",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定顶点数[3-100]：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.IntFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.CircleCR.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.CircleCR.ToString(),
                        CommandExplain = "命令: 新整圆",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定圆心：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.ArcCR.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.ArcCR.ToString(),
                        CommandExplain = "命令: 新圆弧",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定圆心：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
                if (drawObjectId.Equals(CanvasCommands.Arc3P.ToString()))
                {
                    ///OnDrawCommand
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.Arc3P.ToString(),
                        CommandExplain = "命令: 新三点圆弧",
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定起点：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
            }
            {
                if (drawObjectId.Equals(CanvasCommands.Measure.ToString()))
                {
                    CommandEntityDraw = new CommandEntity()
                    {
                        CanvasCommand = CanvasCommands.Measure.ToString(),
                        CommandExplain = "命令: 测量长度",
                        IsWritePatameter = false,
                        Parameters = new List<Patameter>()
                      {
                          new Patameter(){ ParameterId=1, Explain ="请指定起始点：", ParameterValueDataFormat=DrawCommand.DrawDataFormat.PointFormat.ToString()}
                      }
                    };
                }
            }
            if (string.IsNullOrEmpty(CommandEntityDraw.CanvasCommand) == false)
                Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, CommandEntityDraw);

        }
    }
}
