using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;
using WSX.ControlLibrary.Utilities;
using WSX.ViewModels.Forms;
using WSX.ViewModels.RightPanel;
using WSX.WSXCut.Views.Forms;

namespace WSX.WSXCut.Views.CustomControl.RightPanel
{
    public partial class UCMachineControl : UserControl
    {
        private string status;

        public UCMachineControl()
        {
            InitializeComponent();
            InitializeBindings();

            this.btnPause.Enabled = false;
            this.btnBreakPointStart.Enabled = false;
            this.btnLocate.Enabled = false;
            this.btnForward.Enabled = false;
            this.btnBackward.Enabled = false;

            this.Load += (sender, e) => 
            {               
                var viewModel = MVVMContext.FromControl(this).GetViewModel<MachineViewModel>();
                var monitor = new UnitMonitor(this, () => viewModel.RaisePropertiesChanged());
                monitor.Listen();
            };        
        }

        public void UpdateCircleConfig(object para)
        {
            var frm = new FrmCircleConfig();
            frm.SetDataContext((FrmCircleViewModel)para);
            frm.ShowDialog();
        }

        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(MachineViewModel);

            var viewModel = context.GetViewModel<MachineViewModel>();
            var fluent = context.OfType<MachineViewModel>();
        
            #region Command Bindings
            fluent.BindCommand(this.btnStart, (x, para) => x.RunOperation(para), x => "Start");
            fluent.BindCommand(this.btnPause, x => x.Pause());
            fluent.BindCommand(this.btnStop, x => x.StopOperation());
            fluent.BindCommand(this.btnOutline, (x, para) => x.RunOperation(para), x => "Outline");
            fluent.BindCommand(this.btnSimulate, (x, para) => x.RunOperation(para), x => "Simulate");
            fluent.BindCommand(this.btnFastStart, (x, para) => x.RunOperation(para), x => "Fast");
            fluent.BindCommand(this.btnEmpty, (x, para) => x.RunOperation(para), x => "Empty");
            fluent.BindCommand(this.btnCircle, x => x.ConfigCirclePara());
            fluent.BindCommand(this.btnLocate, x => x.LoacateBreakPoint());
            fluent.BindCommand(this.btnBreakPointStart, (x, para) => x.RunOperation(para), x => "BreakPointStart");
            fluent.BindCommand(this.btnForward, (x, para) => x.RunOperation(para), x => "Forward");
            fluent.BindCommand(this.btnBackward, (x, para) => x.RunOperation(para), x => "Backward");
            fluent.BindCommand(this.btnZero, x => x.MoveToZero());
            #endregion

            #region Property Bindings
            fluent.SetBinding(this.checkReturn, e => e.Checked, x => x.IsReturnAfterMachine);
            fluent.SetBinding(this.cmbReturnPoint, e => e.SelectedIndex, x => x.ReturnPointIndex);
            fluent.SetBinding(this.checkReturnAfterStop, e => e.Checked, x => x.IsReturnZeroWhenStop);
            fluent.SetBinding(this.checkSelected, e => e.Checked, x => x.IsOnlyMachineSelected);
            fluent.SetBinding(this.checkSoftLimit, e => e.Checked, x => x.SoftwareLimitEnalbed);
            fluent.SetBinding(this.checkEdgeDetection, e => e.Checked, x => x.EdgeDetectoinEnabled);
            fluent.SetBinding(this.ucInputStep, e => e.Number, x => x.Step, m => double.Parse(SpeedUnitConverter.Convert(m)), r => SpeedUnitConverter.ConvertBack(r.ToString()));
            fluent.SetBinding(this.ucInputSpeed, e => e.Number, x => x.StepSpeed, m => double.Parse(SpeedUnitConverter.Convert(m)), r => SpeedUnitConverter.ConvertBack(r.ToString()));
            //this.ucInputStep.Number = viewModel.Step;
            //this.ucInputStep.NumberChanged += (sender, e) => viewModel.Step = this.ucInputStep.Number;
            //fluent.SetTrigger(x => x.Step, step => this.ucInputStep.Number = step);
            //this.ucInputSpeed.Number = viewModel.StepSpeed;
            //this.ucInputSpeed.NumberChanged += (sender, e) => viewModel.StepSpeed = this.ucInputSpeed.Number;
            //fluent.SetTrigger(x => x.StepSpeed, speed => this.ucInputSpeed.Number = speed);
            #endregion

            this.cmbReturnPoint.DataBindings.Add("Enabled", this.checkReturn, "Checked");
            viewModel.StatusChanged += ViewModel_StatusChanged;
            viewModel.DisableLocateStatus += ViewModel_DisableLocateStatus;
            viewModel.Register("UpdateCirclePara", this.UpdateCircleConfig);
        }

        private void ViewModel_DisableLocateStatus()
        {
            this.BeginInvoke(new Action(()=> 
            {
                this.btnLocate.Enabled = false;
                this.btnBreakPointStart.Enabled = false;
            }));                
        }

        private void ViewModel_StatusChanged(string status)
        {
            this.BeginInvoke(new Action(() =>
            {
                switch (status)
                {
                    case "Idle":
                        this.btnStart.Enabled = true;
                        this.btnStart.Text = "开始";
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = true;
                        this.btnOutline.Enabled = true;
                        this.btnSimulate.Enabled = true;
                        this.btnFastStart.Enabled = true;
                        this.btnOutline.Visible = true;
                        this.btnSimulate.Visible = true;
                        this.btnFastStart.Visible = false;
                        this.btnEmpty.Enabled = true;
                        this.btnCircle.Enabled = true;
                        this.btnLocate.Enabled = false;
                        this.btnBreakPointStart.Enabled = false;
                        this.btnBackward.Enabled = false;
                        this.btnForward.Enabled = false;
                        this.btnZero.Enabled = true;
                        this.checkSelected.Enabled = true;
                        this.checkSoftLimit.Enabled = true;
                        break;

                    case "Start":
                    case "Outline":
                    case "Simulate":
                    case "Empty":
                    case "Zero":
                    case "Forward":
                    case "Backward":
                    case "BreakPointLocate":
                    case "BreakPointStart":
                    case "Fast":
                    case "Stopping":
                    case "PointMove":
                        this.btnStart.Enabled = false;
                        this.btnStart.Text = "开始";
                        this.btnPause.Enabled = status == "Start" || status == "Empty" || status == "Fast" || status == "BreakPointStart";
                        this.btnStop.Enabled = true;
                        this.btnOutline.Enabled = false;
                        this.btnSimulate.Enabled = false;
                        this.btnFastStart.Enabled = false;
                        this.btnOutline.Visible = true;
                        this.btnSimulate.Visible = true;
                        this.btnFastStart.Visible = false;
                        this.btnEmpty.Enabled = false;
                        this.btnCircle.Enabled = true;
                        this.btnLocate.Enabled = false;
                        this.btnBreakPointStart.Enabled = false;
                        this.btnBackward.Enabled = false;
                        this.btnForward.Enabled = false;
                        this.btnZero.Enabled = false;
                        this.checkSelected.Enabled = false;
                        this.checkSoftLimit.Enabled = false;
                        break;

                    case "Pause":
                        this.btnStart.Enabled = true;
                        this.btnStart.Text = "继续";
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = true;
                        this.btnOutline.Enabled = false;
                        this.btnSimulate.Enabled = false;
                        this.btnFastStart.Enabled = true;
                        this.btnOutline.Visible = false;
                        this.btnSimulate.Visible = false;
                        this.btnFastStart.Visible = true;
                        this.btnEmpty.Enabled = true;
                        this.btnCircle.Enabled = true;
                        this.btnLocate.Enabled = false;
                        this.btnBreakPointStart.Enabled = false;
                        this.btnBackward.Enabled = true;
                        this.btnForward.Enabled = true;
                        this.btnZero.Enabled = false;
                        this.checkSelected.Enabled = false;
                        this.checkSoftLimit.Enabled = true;
                        break;

                    case "Stop":
                        this.btnStart.Enabled = true;
                        this.btnStart.Text = "开始";
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = true;
                        this.btnOutline.Enabled = true;
                        this.btnSimulate.Enabled = true;
                        this.btnFastStart.Enabled = false;
                        this.btnOutline.Visible = true;
                        this.btnSimulate.Visible = true;
                        this.btnFastStart.Visible = false;
                        this.btnEmpty.Enabled = true;
                        this.btnCircle.Enabled = true;
                        //if (this.status == "Start" || this.status == "Empty" || this.status == "Fast" || this.status == "BreakPointStart" || this.status == "Pause")
                        //{
                        //    this.btnLocate.Enabled = true;
                        //    this.btnBreakPointStart.Enabled = true;
                        //}
                        //else
                        //{
                        //    this.btnLocate.Enabled = false;
                        //    this.btnBreakPointStart.Enabled = false;
                        //}
                        if (this.status == "Simulate" || this.status == "Outline" || this.status == "Zero")
                        {
                            this.btnLocate.Enabled = false;
                            this.btnBreakPointStart.Enabled = false;
                        }
                        else
                        {
                            this.btnLocate.Enabled = true;
                            this.btnBreakPointStart.Enabled = true;
                        }
                        this.btnLocate.Enabled = true;
                        this.btnBreakPointStart.Enabled = true;
                        this.btnBackward.Enabled = false;
                        this.btnForward.Enabled = false;
                        this.btnZero.Enabled = true;
                        this.checkSelected.Enabled = true;
                        this.checkSoftLimit.Enabled = true;
                        break;

                }
                this.status = status;
            }));
            
        }

        private void checkSoftLimit_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                if (XtraMessageBox.Show("您确定要启用软件限位保护吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (XtraMessageBox.Show("关闭软限位保护将会增加机械撞击的风险，您确定要关闭吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

       
    }
}
