using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.Logger;
using WSX.WSXCut.Models;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmStartup : Form
    {
        private List<InitStep> initSteps;
        private const int STEP_INTERVAL_MS = 500;

        public bool IsInitDone { get; private set; }

        public FrmStartup()
        {
            InitializeComponent();
            this.labelInitInfo.Text = "";       
            this.IsInitDone = true;
            this.labelName.Text = "切割控制平台" + VersionInfo.SoftVersion;
        }

        public FrmStartup(List<InitStep> initSteps) : this()
        {
            this.initSteps = initSteps;
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
            if (this.initSteps == null)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                try
                {
                    foreach (InitStep m in this.initSteps)
                    {
                        this.BeginInvoke(new Action(() => this.labelInitInfo.Text = m.Description));
                        m.Excute();
                        Thread.Sleep(STEP_INTERVAL_MS);
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Instance.Error(ex.Message);
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show(this, ex.Message, "错误消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.IsInitDone = false;
                    }));
                }
                finally
                {
                    this.BeginInvoke(new Action(() => this.Close()));
                }
            });
        }
    }
}
