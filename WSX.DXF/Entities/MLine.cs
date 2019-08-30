#region WSX.DXF library, Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using WSX.DXF.Objects;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    public class MLine :
        EntityObject
    {
        #region internal properties

        internal MLineFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        #endregion

        #region public methods


        public void Update()
        {
            if (this.vertexes.Count == 0)
                return;

            double reference = 0.0;
            switch (this.justification)
            {
                case MLineJustification.Top:
                    reference = this.style.Elements[this.style.Elements.Count - 1].Offset;
                    break;
                case MLineJustification.Zero:
                    reference = 0.0;
                    break;
                case MLineJustification.Bottom:
                    reference = this.style.Elements[0].Offset;
                    break;
            }

            Vector2 prevDir;
            if (this.vertexes[0].Location.Equals(this.vertexes[this.vertexes.Count - 1].Location))
                prevDir = Vector2.UnitY;
            else
            {
                prevDir = this.vertexes[0].Location - this.vertexes[this.vertexes.Count - 1].Location;
                prevDir.Normalize();
            }

            for (int i = 0; i < this.vertexes.Count; i++)
            {
                Vector2 position = this.vertexes[i].Location;
                Vector2 mitter;
                Vector2 dir;
                if (i == 0)
                {
                    if (this.vertexes[i + 1].Location.Equals(position))
                        dir = Vector2.UnitY;
                    else
                    {
                        dir = this.vertexes[i + 1].Location - position;
                        dir.Normalize();
                    }
                    if (this.IsClosed)
                    {
                        mitter = prevDir - dir;
                        mitter.Normalize();
                    }
                    else
                        mitter = MathHelper.Transform(dir, this.style.StartAngle*MathHelper.DegToRad, CoordinateSystem.Object, CoordinateSystem.World);
                }
                else if (i + 1 == this.vertexes.Count)
                {
                    if (this.IsClosed)
                    {
                        if (this.vertexes[0].Location.Equals(position))
                            dir = Vector2.UnitY;
                        else
                        {
                            dir = this.vertexes[0].Location - position;
                            dir.Normalize();
                        }
                        mitter = prevDir - dir;
                        mitter.Normalize();
                    }
                    else
                    {
                        dir = prevDir;
                        mitter = MathHelper.Transform(dir, this.style.EndAngle*MathHelper.DegToRad, CoordinateSystem.Object, CoordinateSystem.World);
                    }
                }
                else
                {
                    if (this.vertexes[i + 1].Location.Equals(position))
                        dir = Vector2.UnitY;
                    else
                    {
                        dir = this.vertexes[i + 1].Location - position;
                        dir.Normalize();
                    }

                    mitter = prevDir - dir;
                    mitter.Normalize();
                }
                prevDir = dir;

                List<double>[] distances = new List<double>[this.style.Elements.Count];
                double angleMitter = Vector2.Angle(mitter);
                double angleDir = Vector2.Angle(dir);
                double cos = Math.Cos(angleMitter + (MathHelper.HalfPI - angleDir));
                for (int j = 0; j < this.style.Elements.Count; j++)
                {
                    double distance = (this.style.Elements[j].Offset + reference)/cos;
                    distances[j] = new List<double>
                    {
                        distance*this.scale,
                        0.0
                    };
                }

                this.vertexes[i] = new MLineVertex(position, dir, mitter, distances);
            }
        }

        #endregion

        #region overrides


        public override object Clone()
        {
            MLine entity = new MLine
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //MLine properties
                Elevation = this.elevation,
                Scale = this.scale,
                Justification = this.justification,
                Style = (MLineStyle) this.style.Clone(),
                Flags = this.flags
            };

            foreach (MLineVertex vertex in this.vertexes)
                entity.vertexes.Add((MLineVertex) vertex.Clone());

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion

        #region delegates and events

        public delegate void MLineStyleChangedEventHandler(MLine sender, TableObjectChangedEventArgs<MLineStyle> e);

        public event MLineStyleChangedEventHandler MLineStyleChanged;

        protected virtual MLineStyle OnMLineStyleChangedEvent(MLineStyle oldMLineStyle, MLineStyle newMLineStyle)
        {
            MLineStyleChangedEventHandler ae = this.MLineStyleChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<MLineStyle> eventArgs = new TableObjectChangedEventArgs<MLineStyle>(oldMLineStyle, newMLineStyle);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newMLineStyle;
        }

        #endregion

        #region private fields

        private double scale;
        private MLineStyle style;
        private MLineJustification justification;
        private double elevation;
        private MLineFlags flags;
        private readonly List<MLineVertex> vertexes;

        #endregion

        #region constructors


        public MLine()
            : this(new List<Vector2>())
        {
        }

        public MLine(IEnumerable<Vector2> vertexes)
            : this(vertexes, MLineStyle.Default, 1.0, false)
        {
        }

        public MLine(IEnumerable<Vector2> vertexes, bool isClosed)
            : this(vertexes, MLineStyle.Default, 1.0, isClosed)
        {
        }

        public MLine(IEnumerable<Vector2> vertexes, double scale)
            : this(vertexes, MLineStyle.Default, scale, false)
        {
        }

 
        public MLine(IEnumerable<Vector2> vertexes, double scale, bool isClosed)
            : this(vertexes, MLineStyle.Default, scale, isClosed)
        {
        }


        public MLine(IEnumerable<Vector2> vertexes, MLineStyle style, double scale)
            : this(vertexes, style, scale, false)
        {
        }


        public MLine(IEnumerable<Vector2> vertexes, MLineStyle style, double scale, bool isClosed)
            : base(EntityType.MLine, DxfObjectCode.MLine)
        {
            this.scale = scale;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            if (isClosed)
                this.flags = MLineFlags.Has | MLineFlags.Closed;
            else
                this.flags = MLineFlags.Has;

            this.style = style;
            this.justification = MLineJustification.Zero;
            this.elevation = 0.0;
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new List<MLineVertex>();
            foreach (Vector2 point in vertexes)
                this.vertexes.Add(new MLineVertex(point, Vector2.Zero, Vector2.Zero, null));
            this.Update();
        }

        #endregion

        #region public properties

        public List<MLineVertex> Vertexes
        {
            get { return this.vertexes; }
        }

        public double Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        public double Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        public bool IsClosed
        {
            get { return this.flags.HasFlag(MLineFlags.Closed); }
            set
            {
                if (value)
                    this.flags |= MLineFlags.Closed;
                else
                    this.flags &= ~MLineFlags.Closed;
            }
        }

        public bool NoStartCaps
        {
            get { return this.flags.HasFlag(MLineFlags.NoStartCaps); }
            set
            {
                if (value)
                    this.flags |= MLineFlags.NoStartCaps;
                else
                    this.flags &= ~MLineFlags.NoStartCaps;
            }
        }

        public bool NoEndCaps
        {
            get { return this.flags.HasFlag(MLineFlags.NoEndCaps); }
            set
            {
                if (value)
                    this.flags |= MLineFlags.NoEndCaps;
                else
                    this.flags &= ~MLineFlags.NoEndCaps;
            }
        }

        public MLineJustification Justification
        {
            get { return this.justification; }
            set { this.justification = value; }
        }

        public MLineStyle Style
        {
            get { return this.style; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.style = this.OnMLineStyleChangedEvent(this.style, value);
            }
        }

        #endregion
    }
}