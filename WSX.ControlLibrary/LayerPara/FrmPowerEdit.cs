using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.Utilities;

namespace WSX.ControlLibrary.LayerPara
{
    public partial class FrmPowerEdit : DevExpress.XtraEditors.XtraForm
    {
        private UCPowerEdit pwrComponent;
        private UCPowerEdit freComponent;
      
        public DataCurveInfo PwrData
        {
            get
            {         
                return this.pwrComponent == null ? null : this.pwrComponent.Curve;
            }
            private set
            {
                if (this.pwrComponent != null)
                {
                    this.pwrComponent.UpdateCurveData(value);
                }
            }
        }

        public DataCurveInfo FreqData
        {
            get
            {
                return this.freComponent == null ? null : this.freComponent.Curve;
            }
            private set
            {
                if (this.freComponent != null)
                {
                    this.freComponent.UpdateCurveData(value);
                }
            }
        }

        public FrmPowerEdit()
        {
            InitializeComponent();       
        }

        public FrmPowerEdit(DataCurveInfo pwrData, DataCurveInfo freqData) : this()
        {
            this.pwrComponent = new UCPowerEdit("PwrCurve", pwrData) { Dock = DockStyle.Fill };
            this.panelPwr.Controls.Add(this.pwrComponent);
            this.freComponent = new UCPowerEdit("FreCurve", freqData) { Dock = DockStyle.Fill };
            this.panelFreq.Controls.Add(this.freComponent);

            this.PwrData = pwrData;
            this.FreqData = freqData;        
        }
         
        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveDlg = new SaveFileDialog
            {
                Filter = "wpd files(*.wpd)|*.wpd",
                FileName = "CurveData_" + DateTime.Now.ToString("yyMMddHHmmss") + ".wpd",
                RestoreDirectory = true
            };

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string path = saveDlg.FileName.ToString();
                SerializeUtil.JsonWriteToFile(Tuple.Create(this.PwrData, this.FreqData), path);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog
            {
                Filter = "wpd files(*.wpd)|*.wpd",
                RestoreDirectory = true
            };

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                string path = openDlg.FileName.ToString();
                var data = SerializeUtil.JsonReadByFile<Tuple<DataCurveInfo, DataCurveInfo>>(path);
                if (data.Item1.Data.Points.Any() && data.Item2.Data.Points.Any())
                {
                    this.PwrData = data.Item1;
                    this.FreqData = data.Item2;                  
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    
        private void tabControlEditor_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            int index = this.tabControlEditor.SelectedTabPageIndex;
            if (index == 0)
            {
                this.btnCopy.Text = "复制到频率";
                this.btnCopy.Image = global::WSX.ControlLibrary.Properties.Resources.next_16x16;             
            }
            else
            {
                this.btnCopy.Text = "复制到功率";
                this.btnCopy.Image = global::WSX.ControlLibrary.Properties.Resources.prev_16x16;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            int index = this.tabControlEditor.SelectedTabPageIndex;
            if (index == 0)
            {               
                this.FreqData.Data.Points = CopyUtil.DeepCopyBaseOnJSon(this.PwrData.Data.Points);
                this.freComponent?.UpdateCurveData(this.FreqData);
            }
            else
            {              
                this.PwrData.Data.Points = CopyUtil.DeepCopyBaseOnJSon(this.FreqData.Data.Points);
                this.pwrComponent?.UpdateCurveData(this.PwrData);
            }
        }
    }  
}
