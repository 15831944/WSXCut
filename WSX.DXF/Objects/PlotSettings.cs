#region WSX.DXF library, Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)
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

namespace WSX.DXF.Objects
{
    /// <summary>
    /// Represents the plot settings of a layout.
    /// </summary>
    public class PlotSettings :
        ICloneable
    {
        #region private fields

        private string pageSetupName;
        private string plotterName;
        private string paperSizeName;
        private string viewName;
        private string currentStyleSheet;

        private PaperMargin paperMargin;
        private Vector2 paperSize;
        private Vector2 origin;
        private Vector2 windowUpRight;
        private Vector2 windowBottomLeft;

        private bool scaleToFit ;
        private double numeratorScale;
        private double denominatorScale;
        private PlotFlags flags;
        private PlotType plotType;

        private PlotPaperUnits paperUnits;
        private PlotRotation rotation;

        private ShadePlotMode shadePlotMode;
        private ShadePlotResolutionMode shadePlotResolutionMode;
        private short shadePlotDPI;
        private Vector2 paperImageOrigin;

        #endregion

        #region constructors

        public PlotSettings()
        {
            this.pageSetupName = string.Empty;
            this.plotterName = "none_device";
            this.paperSizeName = "ISO_A4_(210.00_x_297.00_MM)";
            this.viewName = string.Empty;
            this.currentStyleSheet = string.Empty;

            this.paperMargin = new PaperMargin(7.5, 20.0, 7.5, 20.0);

            this.paperSize = new Vector2(210.0, 297.0);
            this.origin = Vector2.Zero;
            this.windowUpRight = Vector2.Zero;
            this.windowBottomLeft = Vector2.Zero;

            this.scaleToFit = true;
            this.numeratorScale = 1.0;
            this.denominatorScale = 1.0;
            this.flags = PlotFlags.DrawViewportsFirst | PlotFlags.PrintLineweights | PlotFlags.PlotPlotStyles | PlotFlags.UseStandardScale;
            this.plotType = PlotType.DrawingExtents;

            this.paperUnits = PlotPaperUnits.Milimeters;
            this.rotation = PlotRotation.Degrees90;

            this.shadePlotMode = ShadePlotMode.AsDisplayed;
            this.shadePlotResolutionMode = ShadePlotResolutionMode.Normal;
            this.shadePlotDPI = 300;
            this.paperImageOrigin = Vector2.Zero;
        }

        #endregion

        #region public properties

        public string PageSetupName
        {
            get { return this.pageSetupName; }
            set { this.pageSetupName = value; }
        }

        public string PlotterName
        {
            get { return this.plotterName; }
            set { this.plotterName = value; }
        }

        public string PaperSizeName
        {
            get { return this.paperSizeName; }
            set { this.paperSizeName = value; }
        }

        public string ViewName
        {
            get { return this.viewName; }
            set { this.viewName = value; }
        }

        public string CurrentStyleSheet
        {
            get { return this.currentStyleSheet; }
            set { this.currentStyleSheet = value; }
        }

        public PaperMargin PaperMargin
        {
            get { return this.paperMargin; }
            set { this.paperMargin = value; }
        }

        public Vector2 PaperSize
        {
            get { return this.paperSize; }
            set { this.paperSize = value; }
        }

        public Vector2 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public Vector2 WindowUpRight
        {
            get { return this.windowUpRight; }
            set { this.windowUpRight = value; }
        }

        public Vector2 WindowBottomLeft
        {
            get { return this.windowBottomLeft; }
            set { this.windowBottomLeft = value; }
        }

        public bool ScaleToFit
        {
            get { return this.scaleToFit; }
            set { this.scaleToFit = value; }
        }

        public double PrintScaleNumerator
        {
            get { return this.numeratorScale; }
            set
            {
                if(value <= 0.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The print scale numerator must be a number greater than zero.");
                this.numeratorScale = value;
            }
        }

        public double PrintScaleDenominator
        {
            get { return this.denominatorScale; }
            set
            {
                if (value <= 0.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The print scale denominator must be a number greater than zero.");
                this.denominatorScale = value;
            }
        }

        public double PrintScale
        {
            get { return this.numeratorScale / this.denominatorScale; }
        }

        public PlotFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        public PlotType PlotType
        {
            get { return this.plotType; }
            set { this.plotType = value; }
        }

        public PlotPaperUnits PaperUnits
        {
            get { return this.paperUnits; }
            set { this.paperUnits = value; }
        }

        public PlotRotation PaperRotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public ShadePlotMode ShadePlotMode
        {
            get { return this.shadePlotMode; }
            set { this.shadePlotMode = value; }
        }

        public ShadePlotResolutionMode ShadePlotResolutionMode
        {
            get { return this.shadePlotResolutionMode; }
            set { this.shadePlotResolutionMode = value; }
        }

        public short ShadePlotDPI
        {
            get { return this.shadePlotDPI; }
            set
            {
                if(value <100 || value > 32767)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The valid shade plot DPI values range from 100 to 23767.");
                this.shadePlotDPI = value;
            }
        }

        public Vector2 PaperImageOrigin
        {
            get { return this.paperImageOrigin; }
            set { this.paperImageOrigin = value; }
        }

        #endregion

        #region implements ICloneable

        public object Clone()
        {
            return new PlotSettings
            {
                PageSetupName = this.pageSetupName,
                PlotterName = this.plotterName,
                PaperSizeName = this.paperSizeName,
                ViewName = this.viewName,
                CurrentStyleSheet = this.currentStyleSheet,
                PaperMargin = this.PaperMargin,
                PaperSize = this.paperSize,
                Origin = this.origin,
                WindowUpRight = this.windowUpRight,
                WindowBottomLeft = this.windowBottomLeft,
                ScaleToFit = this.scaleToFit,
                PrintScaleNumerator = this.numeratorScale,
                PrintScaleDenominator = this.denominatorScale,
                Flags = this.flags,
                PlotType = this.plotType,
                PaperUnits = this.paperUnits,
                PaperRotation = this.rotation,
                ShadePlotMode = this.shadePlotMode,
                ShadePlotResolutionMode = this.shadePlotResolutionMode,
                ShadePlotDPI = this.shadePlotDPI,
                PaperImageOrigin = this.paperImageOrigin
            };
        }

        #endregion
    }
}