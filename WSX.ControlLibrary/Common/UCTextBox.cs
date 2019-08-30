using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WSX.ControlLibrary.Common
{
    public partial class UCTextBox : DevExpress.XtraEditors.TextEdit
    {
        private readonly string intputPattern = @"^(\-|\+)?((0|[1-9][0-9]*)((\.\d+)?|\.))?$";
        private string lastStr = null;

        public double Max { get; set; }
        public double Min { get; set; }
        public bool IsInterger { get; set; }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //Only allow to input '0'-'9' or '.' or "backspace"
            bool condition1 = e.KeyChar >= '0' && e.KeyChar <= '9';
            bool condition2 = e.KeyChar == 0x08 || e.KeyChar == '-';
            bool condition3 = this.IsInterger ? false : e.KeyChar == '.';
            if (!(condition1 || condition2 || condition3))
            {
                e.Handled = true;
                return;
            }
            StringBuilder temp = new StringBuilder(this.Text);
            if (e.KeyChar == 0x08)
            {
                temp.Remove(this.SelectionStart, this.SelectionLength);
            }
            else
            {
                if (this.SelectionLength > 0)
                {
                    temp.Remove(this.SelectionStart, this.SelectionLength);
                }
                temp.Insert(this.SelectionStart, e.KeyChar);
            }
            if (!string.IsNullOrEmpty(temp.ToString()))
            {
                if (!Regex.IsMatch(temp.ToString(), intputPattern))
                {
                    e.Handled = true;
                }
                else
                {
                    if (temp.ToString().Equals("-") || temp.ToString().Equals("+"))
                    {
                    }
                    else
                    {
                        double number = double.Parse(temp.ToString());
                        if (number < this.Min)
                        {
                            this.Text = this.Min.ToString();
                            e.Handled = true;
                        }
                        if (number > this.Max)
                        {
                            this.Text = this.Max.ToString();
                            e.Handled = true;
                        }
                    }
                }
            }
            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return;
            }

            if (!Regex.IsMatch(this.Text, intputPattern))
            {
                this.Text = this.lastStr;
                return;
            }

            if (this.Text != this.lastStr)
            {
                this.lastStr = this.Text;
                base.OnTextChanged(e);
            }
        }
    }
}
