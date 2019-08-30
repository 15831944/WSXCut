using System;
using System.Text;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a multiline text <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// Formatting codes for MText, you can use them directly while setting the text value or use the Write() method.<br />
    /// \L Start underline<br />
    /// \l Stop underline<br />
    /// \O Start overstrike<br />
    /// \o Stop overstrike<br />
    /// \K Start strike-through<br />
    /// \k Stop strike-through<br />
    /// \P New paragraph (new line)<br />
    /// \pxi Control codes for bullets, numbered paragraphs and columns<br />
    /// \X Paragraph wrap on the dimension line (only in dimensions)<br />
    /// \Q Slanting (obliquing) text by angle - e.g. \Q30;<br />
    /// \H Text height - e.g. \H3x;<br />
    /// \W Text width - e.g. \W0.8x;<br />
    /// \F Font selection<br />
    /// <br />
    /// e.g. \Fgdt;o - GDT-tolerance<br />
    /// e.g. \Fkroeger|b0|i0|c238|p10; - font Kroeger, non-bold, non-italic, code page 238, pitch 10<br />
    /// <br />
    /// \S Stacking, fractions<br />
    /// <br />
    /// e.g. \SA^B;<br />
    /// A<br />
    /// B<br />
    /// e.g. \SX/Y<br />
    /// X<br />
    /// -<br />
    /// Y<br />
    /// e.g. \S1#4;<br />
    /// 1/4<br />
    /// <br />
    /// \A Alignment<br />
    /// \A0; = bottom<br />
    /// \A1; = center<br />
    /// \A2; = top<br />
    /// <br />
    /// \C Color change<br />
    /// \C1; = red<br />
    /// \C2; = yellow<br />
    /// \C3; = green<br />
    /// \C4; = cyan<br />
    /// \C5; = blue<br />
    /// \C6; = magenta<br />
    /// \C7; = white<br />
    /// <br />
    /// \T Tracking, char.spacing - e.g. \T2;<br />
    /// \~ Non-wrapping space, hard space<br />
    /// {} Braces - define the text area influenced by the code<br />
    /// \ Escape character - e.g. \\ = "\", \{ = "{"<br />
    /// <br />
    /// Codes and braces can be nested up to 8 levels deep.<br />
    /// </remarks>
    public class MText :EntityObject
    {
        #region delegates and events

        public delegate void TextStyleChangedEventHandler(MText sender, TableObjectChangedEventArgs<TextStyle> e);

        public event TextStyleChangedEventHandler TextStyleChanged;

        protected virtual TextStyle OnTextStyleChangedEvent(TextStyle oldTextStyle, TextStyle newTextStyle)
        {
            TextStyleChangedEventHandler ae = this.TextStyleChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<TextStyle> eventArgs = new TableObjectChangedEventArgs<TextStyle>(oldTextStyle, newTextStyle);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newTextStyle;
        }

        #endregion

        #region private fields

        private Vector3 position;
        private double rectangleWidth;
        private double height;
        private double rotation;
        private double lineSpacing;
        private double paragraphHeightFactor;
        private MTextLineSpacingStyle lineSpacingStyle;
        private MTextDrawingDirection drawingDirection;
        private MTextAttachmentPoint attachmentPoint;
        private TextStyle style;
        private string text;

        #endregion

        #region constructors

        public MText()
            : this(string.Empty, Vector3.Zero, 1.0, 0.0, TextStyle.Default)
        {
        }

        public MText(string text)
            : this(text, Vector3.Zero, 1.0, 0.0, TextStyle.Default)
        {
        }

        public MText(Vector3 position, double height, double rectangleWidth)
            : this(string.Empty, position, height, rectangleWidth, TextStyle.Default)
        {
        }

        public MText(Vector2 position, double height, double rectangleWidth)
            : this(string.Empty, new Vector3(position.X, position.Y, 0.0), height, rectangleWidth, TextStyle.Default)
        {
        }

        public MText(Vector3 position, double height, double rectangleWidth, TextStyle style)
            : this(string.Empty, position, height, rectangleWidth, style)
        {
        }

        public MText(Vector2 position, double height, double rectangleWidth, TextStyle style)
            : this(string.Empty, new Vector3(position.X, position.Y, 0.0), height, rectangleWidth, style)
        {
        }

        public MText(string text, Vector2 position, double height, double rectangleWidth, TextStyle style)
            : this(text, new Vector3(position.X, position.Y, 0.0), height, rectangleWidth, style)
        {
        }

        public MText(string text, Vector2 position, double height, double rectangleWidth)
            : this(text, new Vector3(position.X, position.Y, 0.0), height, rectangleWidth, TextStyle.Default)
        {
        }

        public MText(string text, Vector3 position, double height, double rectangleWidth)
            : this(text, position, height, rectangleWidth, TextStyle.Default)
        {
        }

        public MText(string text, Vector3 position, double height, double rectangleWidth, TextStyle style)
            : base(EntityType.MText, DxfObjectCode.MText)
        {
            this.text = text;
            this.position = position;
            this.attachmentPoint = MTextAttachmentPoint.TopLeft;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.style = style;
            this.rectangleWidth = rectangleWidth;
            if (height <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(height), this.text, "The MText height must be greater than zero.");
            this.height = height;
            this.lineSpacing = 1.0;
            this.paragraphHeightFactor = 1.0;
            this.lineSpacingStyle = MTextLineSpacingStyle.AtLeast;
            this.drawingDirection = MTextDrawingDirection.ByStyle;
            this.rotation = 0.0;
        }

        #endregion

        #region public properties

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The MText height must be greater than zero.");
                this.height = value;
            }
        }

        public double LineSpacingFactor
        {
            get { return this.lineSpacing; }
            set
            {
                if (value < 0.25 || value > 4.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The MText LineSpacingFactor valid values range from 0.25 to 4.00");
                this.lineSpacing = value;
            }
        }

        public double ParagraphHeightFactor
        {
            get { return this.paragraphHeightFactor; }
            set
            {
                if (value < 0.25 || value > 4.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The MText ParagraphHeightFactor valid values range from 0.25 to 4.00");
                this.paragraphHeightFactor = value;
            }
        }

        public MTextLineSpacingStyle LineSpacingStyle
        {
            get { return this.lineSpacingStyle; }
            set { this.lineSpacingStyle = value; }
        }

        public MTextDrawingDirection DrawingDirection
        {
            get { return this.drawingDirection; }
            set { this.drawingDirection = value; }
        }

        ///  </remarks>
        public double RectangleWidth
        {
            get { return this.rectangleWidth; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The MText rectangle width must be equals or greater than zero.");
                this.rectangleWidth = value;
            }
        }

        public MTextAttachmentPoint AttachmentPoint
        {
            get { return this.attachmentPoint; }
            set { this.attachmentPoint = value; }
        }

        public TextStyle Style
        {
            get { return this.style; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.style = this.OnTextStyleChangedEvent(this.style, value);
            }
        }

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public string Value
        {
            get { return this.text; }
            set { this.text = value; }
        }

        #endregion

        #region public methods

        public void Write(string txt)
        {
            this.Write(txt, null);
        }

        public void Write(string txt, MTextFormattingOptions options)
        {
            if (options == null)
                this.text += txt;
            else
                this.text += options.FormatText(txt);
        }

        public void EndParagraph()
        {
            if (!MathHelper.IsOne(this.paragraphHeightFactor))
                this.text += "{\\H" + this.paragraphHeightFactor + "x;}\\P";
            else
                this.text += "\\P";
        }

        public string PlainText()
        {
            if (string.IsNullOrEmpty(this.text))
                return string.Empty;

            string txt = this.text;

            //text = text.Replace("%%c", "Ø");
            //text = text.Replace("%%d", "°");
            //text = text.Replace("%%p", "±");

            StringBuilder rawText = new StringBuilder();
            CharEnumerator chars = txt.GetEnumerator();

            while (chars.MoveNext())
            {
                char token = chars.Current;
                if (token == '\\') // is a formatting command
                {
                    if (chars.MoveNext())
                        token = chars.Current;
                    else
                        return rawText.ToString(); // premature end of text

                    if (token == '\\' | token == '{' | token == '}') // escape chars
                        rawText.Append(token);
                    else if (token == 'L' | token == 'l' | token == 'O' | token == 'o' | token == 'K' | token == 'k' | token == 'P' | token == 'X') // one char commands
                        if (token == 'P')
                            rawText.Append(Environment.NewLine);
                        else
                        {
                        } // discard other commands
                    else // formatting commands of more than one character always terminate in ';'
                    {
                        bool stacking = token == 'S'; // we want to preserve the text under the stacking command
                        while (token != ';')
                        {
                            if (chars.MoveNext())
                                token = chars.Current;
                            else
                                return rawText.ToString(); // premature end of text

                            if (stacking && token != ';')
                                rawText.Append(token); // append user data of stacking command
                        }
                    }
                }
                else if (token == '{' | token == '}')
                {
                    // discard group markers
                }
                else // char is what it is, a character
                    rawText.Append(token);
            }
            return rawText.ToString();
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            MText entity = new MText
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
                //MText properties
                Position = this.position,
                Rotation = this.rotation,
                Height = this.height,
                LineSpacingFactor = this.lineSpacing,
                ParagraphHeightFactor = this.paragraphHeightFactor,
                LineSpacingStyle = this.lineSpacingStyle,
                RectangleWidth = this.rectangleWidth,
                AttachmentPoint = this.attachmentPoint,
                Style = (TextStyle) this.style.Clone(),
                Value = this.text
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}