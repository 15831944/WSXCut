using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WSX.ControlLibrary.Common
{
    public partial class UCNumberInputer : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void OnInputCompleteDelegate(double value);
        public OnInputCompleteDelegate OnInputComplete;
        /// <summary>
        /// 当前值
        /// </summary>
        [Browsable(true)]
        public override string Text
        {
            get
            {
                if (string.IsNullOrEmpty(this.PopupEdit.Text) || this.PopupEdit.Text.Trim().Equals(""))
                {
                    return "0";
                }
                else
                {
                    return this.PopupEdit.Text;
                }
            }
            set
            {
                this.PopupEdit.Text = value;
                if (double.TryParse(value, out double res))
                {
                    this.Number = res;
                }
            }
        }
        private NumberInputControl numberInputControl;
        private PopupContainerControl popupContainerControl;

        private float textSize = 9.0f;
        private double number = 0;
        private string suffix;
        private string lastText = null;
        private bool updateSuffix = true;

        public event Action<object, EventArgs> NumberChanged;

        public UCNumberInputer()
        {
            InitializeComponent();

            this.PopupEdit.TextChanged += this.PopupEdit_TextChanged;
        }

        public bool IsInterger { get; set; }

        public float TextSize
        {
            get
            {
                return this.textSize;
            }
            set
            {
                this.textSize = value;
                this.PopupEdit.Font = new Font("Tahoma", value);
            }
        }

        public bool ReadOnly
        {
            get => this.PopupEdit.Properties.TextEditStyle != TextEditStyles.Standard;
            set => this.PopupEdit.Properties.TextEditStyle = value ? TextEditStyles.DisableTextEditor : TextEditStyles.Standard;
        }
      
        public string Format { get; set; }
        

        [Bindable(true)]
        public double Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                this.UpdateText();
                this.NumberChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string Suffix
        {
            get
            {
                return this.suffix;
            }
            set
            {
                this.suffix = value;
                this.UpdateText();
            }
        }

        public double Max { get; set; } = 100;
        public double Min { get; set; } = 0;

        private void UpdateText()
        {
            string numStr = string.IsNullOrEmpty(this.Format) ? this.Number.ToString() : this.Number.ToString(this.Format);
            this.PopupEdit.Text = numStr + (this.updateSuffix ? this.suffix : "");
        }
      
        private void UCNumberInputer_Load(object sender, EventArgs e)
        {
            //this.numberInputControl = new NumberInputControl();// { Width = 20};
            //this.popupContainerControl = new PopupContainerControl();
            //this.numberInputControl.Dock = DockStyle.Fill;
            //this.numberInputControl.OnClosed += NumberInputControl_OnClosed;
            //this.popupContainerControl.Controls.Add(this.numberInputControl);
            //this.PopupEdit.Properties.PopupControl = this.popupContainerControl;
        }

        private void NumberInputControl_OnClosed(object sender, CloseEventArgs e)
        {
            this.PopupEdit.ClosePopup();
            if (e.Result == DialogResult.Yes)
            {
                if (this.ReadOnly)
                {
                    this.UpdateNumber(this.numberInputControl.Number);
                }
                else
                {
                    this.UpdateNumber(this.numberInputControl.Number, true);
                    this.PopupEdit.SelectionStart = this.PopupEdit.Text.Length;
                }              
            }
        }

        private void popupContainerEdit1_Popup(object sender, EventArgs e)
        {          
            this.numberInputControl.SetNumber(this.Number);
            this.numberInputControl.FirstAppend = true;
        }

        private void PopupEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (this.numberInputControl == null)
            {
                this.numberInputControl = new NumberInputControl();// { Width = 20};
                this.popupContainerControl = new PopupContainerControl();
                this.numberInputControl.Dock = DockStyle.Fill;
                this.numberInputControl.OnClosed += NumberInputControl_OnClosed;
                this.popupContainerControl.Controls.Add(this.numberInputControl);
                this.PopupEdit.Properties.PopupControl = this.popupContainerControl;
            }
        }

        private void UpdateNumber(double num, bool numOnly = false)
        {          
            if (num < this.Min)
            {
                num = this.Min;
            }
            if (num > this.Max)
            {
                num = this.Max;
            }
            if (this.IsInterger)
            {
                num = Math.Round(num);
            }
            this.updateSuffix = !numOnly;
            this.Number = num;
            this.updateSuffix = true;
            this.OnInputComplete?.Invoke(this.Number);
        }

        private void PopupEdit_Enter(object sender, EventArgs e)
        {
            if (this.ReadOnly)
            {
                return;
            }

            if (!string.IsNullOrEmpty(this.Suffix))
            {
                string str = this.Number.ToString();
                this.PopupEdit.Text = str;
                this.PopupEdit.SelectionStart = str.Length;
            }
            this.lastText = this.PopupEdit.Text;
        }

        private void PopupEdit_Leave(object sender, EventArgs e)
        {
            if (this.ReadOnly)
            {
                return;
            }
            this.UpdateNumber(this.number);  
        }

        private void PopupEdit_TextChanged(object sender, EventArgs e)
        {
            string str = this.PopupEdit.Text;
            if (string.IsNullOrEmpty(this.PopupEdit.Text) || str.Equals("-"))
            {
                this.lastText = str;
                return;
            }

            if (!string.IsNullOrEmpty(this.suffix))
            {
                str = str.Replace(this.suffix, "");
            }

            if (double.TryParse(str, out double num))
            {
                this.number = num;
            }
            else
            {
                this.PopupEdit.TextChanged -= this.PopupEdit_TextChanged;
                int index = this.PopupEdit.SelectionStart;
                if (!string.IsNullOrEmpty(this.Suffix))
                {
                    str = this.lastText.Replace(this.suffix, "");
                }
                if (double.TryParse(str, out num))
                {
                    this.number = num;
                    this.PopupEdit.Text = this.lastText;
                }
                else
                {
                    if (this.lastText == "-")
                    {
                        this.PopupEdit.Text = this.lastText;
                    }
                    else if (double.TryParse(this.lastText, out num))
                    {
                        this.number = num;
                        this.PopupEdit.Text = this.lastText;
                    }
                    else
                    {
                        this.PopupEdit.Text = this.number.ToString() + this.Suffix;
                    }
                }
                this.PopupEdit.Refresh();
                if (index > 0)
                {
                    this.PopupEdit.SelectionStart = index - 1;
                }
                this.PopupEdit.TextChanged += this.PopupEdit_TextChanged;
            }

            this.lastText = this.PopupEdit.Text;
        }

        private void PopupEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' ||
                 e.KeyChar == '.' ||
                 e.KeyChar == (char)Keys.Back ||
                 //e.KeyChar == (char)Keys.Enter ||
                 e.KeyChar == '-'))
            {
                e.Handled = true;
            }

            if (!this.ReadOnly && e.KeyChar == (char)Keys.Enter)
            {
                this.UpdateNumber(this.number, true);
                this.PopupEdit.SelectionStart = this.PopupEdit.Text.Length;
            }
        }
    }
}
