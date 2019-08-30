using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool;
using WSX.Engine.Models;
using WSX.GlobalData.Model;

namespace WSX.Engine.Utilities
{
    public class DrawObjectParser
    {
        private readonly IDrawObject drawObject;
        private readonly int layerId;

        public DrawObjectParser(IDrawObject drawObject)
        {
            this.drawObject = drawObject;
            this.layerId = ((DrawObjectBase)drawObject).LayerId;
        }

        public List<DataUnit> Parse()
        {
            var res = new List<DataUnit>();
            if (drawObject is IDrawData)
            {
                var part1 = this.GetEvaporationFilmPart();
                if (part1 != null && part1.Any())
                {
                    res.AddRange(part1);
                }

                var part2 = this.GetNormalPart();
                if (part2 != null && part2.Any())
                {
                    res.AddRange(part2);
                }

                var part3 = this.GetCoolingPart();
                if (part3 != null && part3.Any())
                {
                    res.AddRange(part3);
                }
            }
            return res;
        }

        private List<DataUnit> GetEvaporationFilmPart()
        {          
            var res = new List<DataUnit>();
            bool condition1 = GlobalModel.Params.LayerConfig.LayerCrafts[this.layerId].IsEvaporationFilm;
            bool condition2 = !(this.drawObject is SingleDot);
            if (condition1 && condition2)
            {
                res = this.GetDataCore(true);
            }
            return res;
        }

        private List<DataUnit> GetNormalPart()
        {
            return this.GetDataCore();
        }

        private List<DataUnit> GetCoolingPart()
        {
            var res = new List<DataUnit>();
            bool condition1 = GlobalModel.Params.LayerConfig.LayerCrafts[this.layerId].IsPathRecooling;
            bool condition2 = !(this.drawObject is SingleDot);
            if (condition1 && condition2)
            {
                res = this.GetDataCore(true);
            }
            return res;
        }

        //private void InsertOriginal(List<DataUnit> items, IDrawData drawData)
        //{

        //}
        private List<DataUnit> GetDataCore(bool skipCoolingPoints = false)
        {
            IDrawData drawData = this.drawObject as IDrawData;
            var res = new List<DataUnit>();
            var basicPart = new List<DataUnit>();

            this.InsertBasic(res, drawData);
            if (!skipCoolingPoints)
            {
                this.SpiltByCoolingPoints(res, drawData);
            }
           
            //this.InsertLeadIn(res, drawData);
            //this.InsertLeadOut(res, drawData);
            var leadInPart = this.GetLeadIn(drawData);
            var leadOutPart = this.GetLeadOut(drawData);
            if (leadInPart.Any())
            {
                var p = leadInPart.Last().Points.Last();
                res = this.LocateAndReshape(res, p);
            }
            else
            {
                bool condition1 = this.drawObject.OverCutLen.GetDecimal() < 0.0001f;
                bool condition2 = leadOutPart.Any();
                if (condition1 && condition2)
                {
                    var p = leadOutPart.First().Points.First();
                    res = this.LocateAndReshape(res, p);
                }
            }

            //Reshape data base on start move point
            var startPoint = this.drawObject.StartMovePoint.ToPointF();
            if (!leadInPart.Any() && !leadOutPart.Any() && !MathEx.Equals(res[0].Points[0], startPoint))
            {
                res = this.LocateAndReshape(res, startPoint);
            }

            //basicPart = CopyUtil.DeepCopy(res);
            basicPart = new List<DataUnit>(res);
            this.InsertConnerRing(res, drawData);

            var tmp = new List<DataUnit>();
            tmp.AddRange(res);
            int cnt = this.drawObject.OverCutLen.GetInteger() - 1;
            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    tmp.AddRange(CopyUtil.DeepCopy(res));
                }
            }

            double ratio = this.drawObject.OverCutLen.GetDecimal();
            if (ratio > 0.0001f)
            {
                if (leadOutPart.Any())
                {
                    tmp.AddRange(GetRange(basicPart, leadOutPart.First().Points[0]));
                }
                else
                {
                    tmp.AddRange(GetRange(basicPart, ratio));
                }
            }

            if (leadInPart.Any())
            {
                tmp.InsertRange(0, leadInPart);
            }
            if (leadOutPart.Any())
            {
                tmp.AddRange(leadOutPart);
            }

            return tmp;
        }

        private void InsertBasic(List<DataUnit> items, IDrawData drawData)
        {
            var part1 = drawData.GetBasic();
            var part2 = drawData.GetCompensation();
            if (part2 != null && part2.Any())
            {
                items.AddRange(ParserUtils.Convert(part2, this.layerId));
            }
            else
            {
                items.AddRange(ParserUtils.Convert(part1, this.layerId));
            }
        }
       
        private void SpiltByCoolingPoints(List<DataUnit> items, IDrawData drawData)
        {
            var part = drawData.GetCoolingPoints();
            if (part != null && part.Any())
            {
                var tmp = part.Select(x => x as DotLite).Where(x => x.IsInCompensation).ToList();
                if (!tmp.Any())
                {
                    tmp = part.Select(x => x as DotLite).ToList();
                }
                foreach (var m in tmp)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        var data = items[i];
                        if (data.Id == DataUnitTypes.PointCooling)
                        {
                            continue;
                        }                     
                        var p = m.Point.ToPointF();
                        if (data.TrySpilt(p, out List<DataUnit> tmp1))
                        {
                            var coolingPoint = new DataUnit(DataUnitTypes.PointCooling, Constants.CoolingPointLayerId, new List<PointF> { p, p.Offset(0.0001f, 0.0001f) });
                            coolingPoint.AttachedLayerId = this.layerId;
                            tmp1.Insert(1, coolingPoint);

                            items.RemoveAt(i);
                            items.InsertRange(i, tmp1);

                            break;
                        }
                    }
                }          
            }
        }

        private void InsertConnerRing(List<DataUnit> items, IDrawData drawData)
        {
            var part = drawData.GetCornerRing();
            if (part != null && part.Any())
            {
                var tmp = ParserUtils.Convert(part, this.layerId);
                foreach (var m in tmp)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        var p1 = m.Points[0];
                        var p2 = items[i].Points.Last();
                        if (MathEx.Equals(p1, p2))
                        {
                            items.Insert(i + 1, m);
                            break;
                        }
                    }
                }
            }
        }

        private List<DataUnit> GetLeadIn(IDrawData drawData)
        {
            var result = new List<DataUnit>();
            var part = drawData.GetLeadIn();
            if (part != null && part.Any())
            {
                result.AddRange(ParserUtils.Convert(part, this.layerId));
            }
            return result;
        }

        private List<DataUnit> GetLeadOut(IDrawData drawData)
        {
            var result = new List<DataUnit>();
            var part = drawData.GetLeadOut();
            if (part != null && part.Any())
            {
                result.AddRange(ParserUtils.Convert(part, this.layerId));
            }
            return result;
        }

        private List<DataUnit> LocateAndReshape(List<DataUnit> items, PointF point)
        {
            List<DataUnit> result = new List<DataUnit>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].TrySpilt(point, out List<DataUnit> tmp))
                {
                    var part1 = items.GetRange(i, items.Count - i);
                    var part2 = items.GetRange(0, i + 1);

                    part1.RemoveAt(0);
                    if (tmp[1].Length > 0.001)
                    {
                        part1.Insert(0, tmp[1]);
                    }

                    part2.RemoveAt(part2.Count - 1);
                    if (tmp[0].Length > 0.001)
                    {
                        part2.Add(tmp[0]);
                    }

                    if (part1.Any())
                    {
                        result.AddRange(part1);
                    }
                    if (part2.Any())
                    {
                        result.AddRange(part2);
                    }

                    break;
                }
            }
            return result;
        }

        private List<DataUnit> GetRange(List<DataUnit> items, double ratio)
        {
            var provider = new DataProvider();
            provider.DataCollection = items;
            provider.CurrentLen = 0;
            return provider.GetMovementData(provider.TotalLen * ratio);
        }

        private List<DataUnit> GetRange(List<DataUnit> items, PointF point)
        {
            var result = new List<DataUnit>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].TrySpilt(point, out List<DataUnit> tmp))
                {
                    result.AddRange(items.GetRange(0, i));
                    result.Add(tmp[0]);
                    break;
                }
            }
            return result;
        }      
    }
}

   
