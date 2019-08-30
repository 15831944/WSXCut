using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace WSX.ControlLibrary.Common
{
    public partial class NumberInputControl : UserControl
    {
        public double Number { get; private set; }
        public event Action<object, CloseEventArgs> OnClosed;
        public bool FirstAppend { get; set; } = false;
        private bool positive = true;

        public NumberInputControl()
        {
            InitializeComponent();
        }

        public void SetNumber(double number)
        {
            this.labelContent.Text = number.ToString();
            this.Number = number;
            this.positive = this.Number >= 0;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            string text = (sender as SimpleButton).Text;
            string content = this.GetContent() + text;
            this.ParseInput(content);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.labelContent.Text = "0";
            this.Number = 0;
            this.positive = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.OnClosed?.Invoke(this, new CloseEventArgs { Result = DialogResult.No });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void NumberInputControl_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            bool isValid = true;
            string content = this.GetContent(key == Keys.OemMinus);

            if (key == Keys.Back)
            {
                if (!(string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content)))
                {
                    content = content.Remove(content.Length - 1);
                    if (string.IsNullOrEmpty(content) || content.Equals("-"))
                    {
                        content = "0";
                        this.positive = true;
                    }
                }
            }
            else if (key == Keys.Enter)
            {
                isValid = false;
                this.Finish();
            }
            else if (key >= Keys.D0 && key <= Keys.D9)
            {
                int diff = (int)key - (int)Keys.D0;
                content += diff.ToString();
            }
            else if (key == Keys.OemMinus)
            {
                this.positive = !this.positive;
            }
            else if (key >= Keys.NumPad0 && key < Keys.NumPad9)
            {
                int diff = (int)key - (int)Keys.NumPad0;
                content += diff.ToString();
            }
            else if (key == Keys.OemPeriod || key == Keys.Decimal)
            {
                content += ".";
            }
            else
            {
                isValid = false;
            }

            if (isValid)
            {
                this.ParseInput(content);
            }
        }

        private void ParseInput(string content)
        {
            double number;
            if (double.TryParse(content, out number))
            {
                this.Number = this.positive ? Math.Abs(number) : -1 * Math.Abs(number);
                string tmp = this.Number.ToString();
                if (content.LastIndexOf(".") == content.Length - 1)
                {
                    tmp += ".";
                }
                if (!this.positive && this.Number == 0)
                {
                    tmp = tmp.Insert(0, "-");
                }
                this.labelContent.Text = tmp;
            }
        }

        private string GetContent(bool original = false)
        {
            if (!original && this.FirstAppend)
            {
                this.FirstAppend = false;
                this.labelContent.Text = "0";
            }
            return this.labelContent.Text;
        }

        private void Finish()
        {
            if (string.IsNullOrEmpty(this.labelContent.Text))
            {
                this.Number = 0;
            }
            this.OnClosed?.Invoke(this, new CloseEventArgs { Result = DialogResult.Yes });
        }

        private void btnSign_Click(object sender, EventArgs e)
        {           
            this.positive = !this.positive;
            this.ParseInput(this.GetContent(true));
        }
    }

    public class CloseEventArgs : EventArgs
    {
        public DialogResult Result { get; set; }
    }
}
