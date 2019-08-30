using DevExpress.Utils.MVVM;
using DevExpress.XtraBars;
using WSX.ViewModels.CustomControl;

namespace WSX.WSXCut.Views.CustomControl
{
    public partial class UCStatusBarInfo : DevExpress.XtraEditors.XtraUserControl
    {
        public UCStatusBarInfo()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(UCStatusBarInfoViewModel);

            var fluent = context.OfType<UCStatusBarInfoViewModel>();
            var viewModel = context.GetViewModel<UCStatusBarInfoViewModel>();

            fluent.WithEvent<ItemClickEventArgs>(this.barManager1, "ItemClick")
                 .EventToCommand(x => x.MakeOperation(0), args => this.HandlePopupClickInfo(args));

            #region Property                  
            fluent.SetBinding(this.labelCanvasPos, e => e.Text, x => x.CanvasPos,
               canvasPos => $"{canvasPos.X.ToString("0.00")}, {canvasPos.Y.ToString("0.00")}");
            fluent.SetBinding(this.dropBtnCoordinate, e => e.Text, x => x.CurrentPos, 
               currentPos => $"(X:{currentPos.X.ToString("0.00")}, Y:{currentPos.Y.ToString("0.00")})");
            fluent.SetBinding(this.checkBtnFine, e => e.Checked, x => x.FineEnabled);
            fluent.SetBinding(this.labelOperation, e => e.Text, x => x.Operation);
            fluent.SetBinding(this.barButtonItem3, e => e.Enabled, x => x.OperEnabled);
            fluent.SetBinding(this.barButtonItem4, e => e.Enabled, x => x.OperEnabled);
            fluent.SetBinding(this.barButtonItem5, e => e.Enabled, x => x.OperEnabled);
            this.ucInputDis.DataBindings.Add("Enabled", this.checkBtnFine, "Checked");
            this.ucInputDis.Number = viewModel.Distance;
            this.ucInputDis.NumberChanged += (sender, e) => viewModel.Distance = this.ucInputDis.Number;
            fluent.SetTrigger(x => x.Distance, distance => this.ucInputDis.Number = distance);
            #endregion
        }

        private int HandlePopupClickInfo(ItemClickEventArgs args)
        {
            int id = args.Item.Id;
            if (id == 0)
            {
                //this.popupMenuOper.ItemLinks[0].ImageIndex = 0;
                //this.popupMenuOper.ItemLinks[1].ImageIndex = -1;
                this.barButtonItem1.ImageIndex = 0;
                this.barButtonItem2.ImageIndex = -1;
            }
            if (id == 1)
            {
                //this.popupMenuOper.ItemLinks[0].ImageIndex = -1;
                //this.popupMenuOper.ItemLinks[1].ImageIndex = 0;
                this.barButtonItem1.ImageIndex = -1;
                this.barButtonItem2.ImageIndex = 0;
            }
            return id;
        }
    }
}
