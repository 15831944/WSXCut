using DevExpress.Utils.MVVM;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Windows.Forms;
using WSX.ViewModels.CustomControl.Menu;

namespace WSX.WSXCut.Views.CustomControl.Menu
{
    public partial class RibPageMachine : UserControl
    {
        public RibbonControl Ribbon { get; private set; }
        private RibbonPageCategory category;
        private RibbonPage page;
        private bool pageFixed = false;
        private RibbonPage lastPage;

        public event Action<object, DisplayChangedEventArgs> DisplayChanged;

        public RibPageMachine(RibbonControl ribbon) : this()
        {
            this.page = new RibbonPage { Text = "正在加工" };
            this.category = new RibbonPageCategory { Text = "加工" };
            this.category.Pages.Add(this.page);
            this.Ribbon = ribbon;
            this.Ribbon.SelectedPageChanging += Ribbon_SelectedPageChanging;
        }

        public RibPageMachine()
        {
            InitializeComponent();
            InitializeBindings(); 
        }

        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(RibPageMachineViewModel);

            var fluent = context.OfType<RibPageMachineViewModel>();
            var viewModel = context.GetViewModel<RibPageMachineViewModel>();

            #region Register UI handler
            viewModel.Register("AttachToMenu", x => this.AttachToMenu());
            viewModel.Register("RemoveFromMenu", x => this.RemoveFromMenu());
            viewModel.Register("UpdateOperation", x => this.UpdateStatus((string)x));
            #endregion

            #region Property                  
            fluent.SetBinding(this.txtTime, e => e.Text, x => x.MachineTime, m => m.ToString(@"hh\:mm\:ss\.fff"));
            fluent.SetBinding(this.progressBarMachine, e => e.EditValue, x => x.Progress);
            fluent.SetBinding(this.txtSpeedX, e => e.Text, x => x.XSpeed, m => m.ToString("0.00") + "mm/s");
            fluent.SetBinding(this.txtSpeedY, e => e.Text, x => x.YSpeed, m => m.ToString("0.00") + "mm/s");
            fluent.SetBinding(this.txtSpeed, e => e.Text, x => x.Speed, m => m.ToString("0.00") + "mm/s");
            fluent.SetBinding(this.txtPosX, e => e.Text, x => x.XPos, m => m.ToString("0.00") + "mm");
            fluent.SetBinding(this.txtPosY, e => e.Text, x => x.YPos, m => m.ToString("0.00") + "mm");
            fluent.SetBinding(this.txtFollow, e => e.Text, x => x.FollowHeight, m => m.ToString("0.00") + "mm");
            fluent.SetBinding(this.txtActualFollow, e => e.Text, x => x.ActualFollowHeight, m => m.ToString("0.00") + "mm");
            fluent.SetBinding(this.txtPosZ, e => e.Text, x => x.ZPos, m => m.ToString("0.00") + "mm");
            fluent.SetBinding(this.txtFrequency, e => e.Text, x => x.Frequency, m => m.ToString("0.00") + "Hz");
            fluent.SetBinding(this.txtPower, e => e.Text, x => x.Power, m => m.ToString("0.00") + "%");
            fluent.SetBinding(this.txtDuty, e => e.Text, x => x.DutyCircle, m => m.ToString("0.00") + "%");          
            #endregion

            #region Command
            fluent.BindCommand(this.btnStart, x => x.Resume());
            fluent.BindCommand(this.btnPause, x => x.Pause());
            fluent.BindCommand(this.btnStop, x => x.Stop());
            #endregion
        }

        private void OnDisposing()
        {
            var viewModel = MVVMContext.GetViewModel<RibPageMachineViewModel>(this);
            viewModel?.Dispose();
        }

        public void AttachToMenu()
        {
            if (this.Ribbon == null)
            {
                return;
            }
         
            this.Execute(() =>
            {
                this.lastPage = this.Ribbon.SelectedPage;
                this.Ribbon.PageCategories.Add(this.category);
                this.Ribbon.SelectedPage = this.page;
                this.DisplayChanged?.Invoke(this, new DisplayChangedEventArgs() { IsDisplay = true });
                this.pageFixed = true;      
            });
           
        }

        private void Ribbon_SelectedPageChanging(object sender, RibbonPageChangingEventArgs e)
        {
            e.Cancel = this.pageFixed;
        }

        public void RemoveFromMenu()
        {
            if (this.Ribbon == null)
            {
                return;
            }

            this.Execute(() =>
            {             
                this.Ribbon.PageCategories.Remove(this.category);
                this.DisplayChanged?.Invoke(this, new DisplayChangedEventArgs() { IsDisplay = false });
                this.pageFixed = false;
                this.Ribbon.SelectedPage = this.lastPage;
            });
        }

        private void UpdateStatus(string item)
        {
            this.Execute(() =>
            {
                switch (item)
                {
                    //case "Start":
                    //    this.btnStop.Enabled = true;
                    //    this.btnPause.Enabled = true;
                    //    this.btnStart.Enabled = false;
                    //    break;
                    case "Pause":
                        this.btnStop.Enabled = true;
                        this.btnPause.Enabled = false;
                        this.btnStart.Enabled = true;
                        break;
                    case "Stop":
                    case "Stopping":
                    case "Zero":
                    case "Forward":
                    case "Backward":
                        this.btnStop.Enabled = true;
                        this.btnPause.Enabled = false;
                        this.btnStart.Enabled = false;
                        break;
                    default:
                        this.btnStop.Enabled = true;
                        this.btnPause.Enabled = true;
                        this.btnStart.Enabled = false;
                        break;
                }
            });
        }

        private void Execute(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
    }

    public class DisplayChangedEventArgs : EventArgs
    {
        public bool IsDisplay { get; set; }
    }
}
