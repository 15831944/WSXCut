using System;
using System.Drawing;
using System.Windows.Forms;

namespace WSX.DrawService.Wrapper
{
    public class ToolStripDropdownButton : ToolStripButton
    {
        private ToolStripDropDown dropDown;

        protected override void OnClick(EventArgs e)
        {          
            if (this.DropDown != null)
            {
                this.Checked = true;
                int x = this.Bounds.X + this.Bounds.Width;
                int y = this.Bounds.Y;
                this.DropDown.Show(this.GetCurrentParent(), new Point(x, y));
            }         
            base.OnClick(e);
        }

        public ToolStripDropDown DropDown
        {
            get
            {
                return this.dropDown;
            }
            set
            {
                this.dropDown = value;
                if (value != null)
                {
                    this.dropDown.Closed += (sender, e) => this.Checked = false;
                }
            }
        }
    }
}
