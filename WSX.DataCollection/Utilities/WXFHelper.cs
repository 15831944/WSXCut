using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.FigureModel;
using WSX.DataCollection.WXF;
using WSX.Logger;

namespace WSX.DataCollection.Utilities
{
    /// <summary>
    /// wxf（自定义）配置文件操作
    /// </summary>
    public class WxfHelper
    {
        /// <summary>
        /// 序列化类到xml文档
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="para">类的对象</param>
        /// <param name="filePath">xml文档路径（包含文件名）</param>
        /// <returns>成功：true，失败：false</returns>
        public static bool XMLWriteToFile(List<FigureBaseModel> figures, string filePath, Type[] types = null)
        {

            try
            {
                XmlWriter writer = null;    //声明一个xml编写器
                XmlWriterSettings writerSetting = new XmlWriterSettings
                {
                    Indent = true,//定义xml格式，自动创建新的行
                    Encoding = UTF8Encoding.UTF8,//编码格式
                };
                writer = XmlWriter.Create(filePath, writerSetting);

                XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();
                XmlAttributes attrs = new XmlAttributes();
                XmlElementAttribute attrArc = new XmlElementAttribute();
                attrArc.ElementName = "Arc";
                attrArc.Type = typeof(ArcModel);
                XmlElementAttribute attrCircle = new XmlElementAttribute();
                attrCircle.ElementName = "Circle";
                attrCircle.Type = typeof(CircleModel);
                XmlElementAttribute attrEllipse = new XmlElementAttribute();
                attrEllipse.ElementName = "Ellipse";
                attrEllipse.Type = typeof(EllipseModel);
                XmlElementAttribute attrLwPolyline = new XmlElementAttribute();
                attrLwPolyline.ElementName = "LwPolyline";
                attrLwPolyline.Type = typeof(LwPolylineModel);
                XmlElementAttribute attrPoint = new XmlElementAttribute();
                attrPoint.ElementName = "Point";
                attrPoint.Type = typeof(PointModel);
                XmlElementAttribute attrPolyBezier = new XmlElementAttribute();
                attrPolyBezier.ElementName = "PolyBezier";
                attrPolyBezier.Type = typeof(PolyBezierModel);

                // Add the XmlElementAttribute to the collection of objects.
                attrs.XmlElements.Add(attrArc);
                attrs.XmlElements.Add(attrCircle);
                attrs.XmlElements.Add(attrEllipse);
                attrs.XmlElements.Add(attrLwPolyline);
                attrs.XmlElements.Add(attrPoint);
                attrs.XmlElements.Add(attrPolyBezier);

                attrOverrides.Add(typeof(Entities), "Figures", attrs);

                WXFDocument fgs = new WXFDocument() { Entities = new Entities() { Figures = figures } };

                XmlSerializer xser = new XmlSerializer(typeof(WXFDocument), attrOverrides);  //实例化序列化对象
                xser.Serialize(writer, fgs);  //序列化对象到xml文档
                writer.Close();
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 从 XML 文档中反序列化为对象
        /// </summary>
        /// <param name="filePath">文档路径（包含文档名）</param>
        /// <param name="type">对象的类型</param>
        /// <returns>返回object类型</returns>
        public static List<FigureBaseModel> XMLReadByFile(string filePath)
        {
            try
            {
                string xmlString = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(xmlString))
                {
                    return null;
                }
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();
                    XmlAttributes attrs = new XmlAttributes();
                    XmlElementAttribute attrArc = new XmlElementAttribute();
                    attrArc.ElementName = "Arc";
                    attrArc.Type = typeof(ArcModel);
                    XmlElementAttribute attrCircle = new XmlElementAttribute();
                    attrCircle.ElementName = "Circle";
                    attrCircle.Type = typeof(CircleModel);
                    XmlElementAttribute attrEllipse = new XmlElementAttribute();
                    attrEllipse.ElementName = "Ellipse";
                    attrEllipse.Type = typeof(EllipseModel);
                    XmlElementAttribute attrLwPolyline = new XmlElementAttribute();
                    attrLwPolyline.ElementName = "LwPolyline";
                    attrLwPolyline.Type = typeof(LwPolylineModel);
                    XmlElementAttribute attrPoint = new XmlElementAttribute();
                    attrPoint.ElementName = "Point";
                    attrPoint.Type = typeof(PointModel);
                    XmlElementAttribute attrPolyBezier = new XmlElementAttribute();
                    attrPolyBezier.ElementName = "PolyBezier";
                    attrPolyBezier.Type = typeof(PolyBezierModel);

                    // Add the XmlElementAttribute to the collection of objects.
                    attrs.XmlElements.Add(attrArc);
                    attrs.XmlElements.Add(attrCircle);
                    attrs.XmlElements.Add(attrEllipse);
                    attrs.XmlElements.Add(attrLwPolyline);
                    attrs.XmlElements.Add(attrPoint);
                    attrs.XmlElements.Add(attrPolyBezier);

                    attrOverrides.Add(typeof(Entities), "Figures", attrs);

                    XmlSerializer serializer = new XmlSerializer(typeof(WXFDocument), attrOverrides);
                    return ((WXFDocument)serializer.Deserialize(stream)).Entities.Figures;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
                return null;
            }
        }
    }
}
