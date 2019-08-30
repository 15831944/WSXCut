using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM;
using DevExpress.XtraBars;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;
using WSX.ControlLibrary.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.ViewModels.RightPanel;
using WSX.WSXCut.Views.Forms;

namespace WSX.WSXCut.Views.CustomControl.RightPanel
{
    public partial class UCMannual : UserControl
    {
        public UCMannual()
        {
            InitializeComponent();
            InitializeBindings();   
            
            this.Load += (sender, e) =>
            {             
                var viewModel = MVVMContext.FromControl(this).GetViewModel<ManualViewModel>();
                var monitor = new UnitMonitor(this, () => viewModel.RaisePropertiesChanged());
                monitor.Listen();
            };           
        }

        public MVVMContext DataContext { get => MVVMContext.FromControl(this); }
       
        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(ManualViewModel);
          
            var fluent = context.OfType<ManualViewModel>();
            var viewModel = context.GetViewModel<ManualViewModel>();
            viewModel.StatusChanged += ViewModel_StatusChanged;

            #region Event to command
            fluent.WithEvent<ItemClickEventArgs>(this.barManager1, "ItemClick")
                  .EventToCommand(x => x.SwichContext(0), args => this.HandlePopupClickInfo(args));

            fluent.WithEvent<MouseEventArgs>(this.btnXPositive, "MouseDown")
                  .EventToCommand(x => x.MakeMovement(null), delegate (MouseEventArgs args) { return Tuple.Create(AxisTypes.AxisX, 1.0d); });
            fluent.WithEvent<MouseEventArgs>(this.btnXPositive, "MouseUp").EventToCommand(x => x.Cancel());

            fluent.WithEvent<MouseEventArgs>(this.btnXNegative, "MouseDown")
                  .EventToCommand(x => x.MakeMovement(null), delegate (MouseEventArgs args) { return Tuple.Create(AxisTypes.AxisX, -1.0d); });
            fluent.WithEvent<MouseEventArgs>(this.btnXNegative, "MouseUp").EventToCommand(x => x.Cancel());

            fluent.WithEvent<MouseEventArgs>(this.btnYPositive, "MouseDown")
                  .EventToCommand(x => x.MakeMovement(null), delegate (MouseEventArgs args) { return Tuple.Create(AxisTypes.AxisY, 1.0d); });
            fluent.WithEvent<MouseEventArgs>(this.btnYPositive, "MouseUp").EventToCommand(x => x.Cancel());

            fluent.WithEvent<MouseEventArgs>(this.btnYNegative, "MouseDown")
                  .EventToCommand(x => x.MakeMovement(null), delegate (MouseEventArgs args) { return Tuple.Create(AxisTypes.AxisY, -1.0d); });
            fluent.WithEvent<MouseEventArgs>(this.btnYNegative, "MouseUp").EventToCommand(x => x.Cancel());

            fluent.WithEvent<MouseEventArgs>(this.btnZPositive, "MouseDown")
                  .EventToCommand(x => x.MakeMovement(null), delegate (MouseEventArgs args) { return Tuple.Create(AxisTypes.AxisZ, 1.0d); });
            fluent.WithEvent<MouseEventArgs>(this.btnZPositive, "MouseUp").EventToCommand(x => x.Cancel());

            fluent.WithEvent<MouseEventArgs>(this.btnZNegative, "MouseDown")
                  .EventToCommand(x => x.MakeMovement(null), delegate (MouseEventArgs args) { return Tuple.Create(AxisTypes.AxisZ, -1.0d); });
            fluent.WithEvent<MouseEventArgs>(this.btnZNegative, "MouseUp").EventToCommand(x => x.Cancel());

            fluent.WithEvent<MouseEventArgs>(this.btnLaser, "MouseDown").EventToCommand(x => x.LaserOn());
            fluent.WithEvent<MouseEventArgs>(this.btnLaser, "MouseUp").EventToCommand(x => x.LaserOff());
            #endregion

            #region Property
            //Using legacy method to make two-way binding availible
            //this.ucInputSpeed.DataBindings.Add("Number", viewModel, "Speed", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.ucInputPower.DataBindings.Add("Number", viewModel, "Power", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.ucInputStep.DataBindings.Add("Number", viewModel, "Step", true, DataSourceUpdateMode.OnPropertyChanged);

            fluent.SetBinding(this.ucInputSpeed, e => e.Number, x => x.Speed, m => double.Parse(SpeedUnitConverter.Convert(m)), r => SpeedUnitConverter.ConvertBack(r.ToString()));
            fluent.SetBinding(this.ucInputStep, e => e.Number, x => x.Step, m => double.Parse(SpeedUnitConverter.Convert(m)), r => SpeedUnitConverter.ConvertBack(r.ToString()));
            fluent.SetBinding(this.ucInputPower, e => e.Number, x => x.Power, m => double.Parse(SpeedUnitConverter.Convert(m)), r => SpeedUnitConverter.ConvertBack(r.ToString()));

            //this.ucInputSpeed.Number = viewModel.Speed;
            //this.ucInputSpeed.NumberChanged += (sender, e) => viewModel.Speed = this.ucInputSpeed.Number;
            //fluent.SetTrigger(x => x.Speed, speed => this.ucInputSpeed.Number = speed);

            //this.ucInputStep.Number = viewModel.Step;
            //this.ucInputStep.NumberChanged += (sender, e) => viewModel.Step = this.ucInputStep.Number;
            //fluent.SetTrigger(x => x.Step, step => this.ucInputStep.Number = step);

            //this.ucInputPower.Number = viewModel.Power;
            //this.ucInputPower.NumberChanged += (sender, e) => viewModel.Power = this.ucInputPower.Number;
            //fluent.SetTrigger(x => x.Power, power => this.ucInputPower.Number = power);

            fluent.SetBinding(this.checkStep, e => e.Checked, x => x.IsStepMove);
            fluent.SetBinding(this.checkPointLaser, e => e.Checked, x => x.IsPointMoveCut);
            fluent.SetBinding(this.checkFast, e => e.Checked, x => x.IsFast);
            #endregion

            #region Command
            fluent.BindCommand(this.btnMark, x => x.CaptureMarkPoint());
            fluent.BindCommand(this.btnMoveToMark, x => x.MoveToMarkPoint());
            fluent.BindCommand(this.btnLaserSwitch, x => x.ControlLigthSwitch());
            fluent.BindCommand(this.btnRedLight, x => x.ControlRedLight());
            fluent.BindCommand(this.btnFollow, x => x.ControlHeightDevice());
            fluent.BindCommand(this.btnBlowing, x => x.ControlBlowing());
            fluent.BindCommand(this.btnPreview, x => x.Preview());
            fluent.BindCommand(this.btnLaserPara, x => x.ConfigParameter());
            viewModel.Register("ConfigPara", x =>
            {
                FrmLayerConfig frm = new FrmLayerConfig(GlobalModel.Params.LayerConfig);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GlobalModel.Params.LayerConfig = frm.Model;
                }
            });
            #endregion
        }

        private void ViewModel_StatusChanged(string item, bool enabled)
        {
            this.BeginInvoke(new Action(() => 
            {
                switch (item)
                {
                    case "LigthSwitch":
                        this.btnLaserSwitch.ImageIndex = enabled ? 8 : 7;
                        break;
                    case "RedLight":
                        this.btnRedLight.ImageIndex = enabled ? 8 : 7;
                        break;
                    case "Laser":
                        this.btnLaser.ImageIndex = enabled ? 8 : 7;
                        break;
                    case "Follow":
                        this.btnFollow.ImageIndex = enabled ? 8 : 7;
                        break;
                    case "Blowing":
                        this.btnBlowing.ImageIndex = enabled ? 8 : 7;
                        break;
                    //case "All":           
                    case "Idle":
                    case "Running":
                        bool flag = item == "Idle";
                        this.dropBtnCoordinate.Enabled = flag;
                        this.btnXPositive.Enabled = flag;
                        this.btnXNegative.Enabled = flag;
                        this.btnYPositive.Enabled = flag;
                        this.btnYNegative.Enabled = flag;
                        this.btnZPositive.Enabled = flag;
                        this.btnZNegative.Enabled = flag;
                        this.btnMark.Enabled = flag;
                        this.btnMoveToMark.Enabled = flag;
                        this.dropBtnMark.Enabled = flag;
                        break;
                    case "Suspend":
                        this.dropBtnCoordinate.Enabled = false;
                        this.btnXPositive.Enabled = true;
                        this.btnXNegative.Enabled = true;
                        this.btnYPositive.Enabled = true;
                        this.btnYNegative.Enabled = true;
                        this.btnZPositive.Enabled = true;
                        this.btnZNegative.Enabled = true;
                        this.btnMark.Enabled = false;
                        this.btnMoveToMark.Enabled = false;
                        this.dropBtnMark.Enabled = false;
                        break;

                }
            }));          
        }

        private int HandlePopupClickInfo(ItemClickEventArgs e)
        {
            int id = e.Item.Id;
            if (id > 0 && id < 12)
            {
                this.UpdateCoordinateInfo(id);
            }
            if (id >= 12)
            {
                this.UpdateMarkInfo(id - 12);
            }
            return id;
        }

        private void UpdateCoordinateInfo(int id)
        {
            var items = this.popupMenuCoordinate.ItemLinks.ToList();
            foreach (var m in items)
            {
                m.Item.ImageIndex = -1;
            }

            if (id != 0)
            {
                items[id].ImageIndex = 0;
                this.dropBtnCoordinate.Text = items[id].Item.Caption;
                this.dropBtnCoordinate.ForeColor = (id == 1) ? Color.Red : Color.Black;
            }
            this.barButtonItem1.Enabled = id != 1;
        }

        private void UpdateMarkInfo(int id)
        {
            var items = this.popupMenuMark.ItemLinks.ToList();
            this.dropBtnMark.Text = items[id].Item.Caption;
            this.dropBtnMark.ImageIndex = items[id].Item.ImageIndex;
        }

        private void OnDisposing()
        {
            var context = MVVMContext.FromControl(this);
            context?.Dispose();
        }
    }
}
