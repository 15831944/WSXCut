using DevExpress.XtraBars;
using WSX.WSXCut.Views.Forms;
using System.Windows.Forms;
using WSX.CommomModel.Utilities;

namespace WSX.WSXCut
{
	//引入引出线设置
    public partial class MainView
    {
        private FrmLineInOutParams frmLineInOutParams;
        private void btnLeadInOrOutWireItem_ItemClick(object sender, ItemClickEventArgs e)
        {           
            if (this.frmLineInOutParams == null)
            {
                this.frmLineInOutParams = new FrmLineInOutParams();               
            }
            if (this.frmLineInOutParams.ShowDialog()==DialogResult.OK)
            {
                this.canvasWrapper1.OnSetLeadwire(this.frmLineInOutParams.LeadwireParam);
            }
        }
        private void btnCheckLeadInOrOutWire_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCheckLeadInOrOutWire(false);
        }
        private void btnLeadInOrOutWireDifferentiateMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            //特殊情况下区分内外模的引出引出线检查功能逻辑中没有判断引线与每个图形的干涉情况,当区分内外模存在引线干涉,可再次点击引入引出线检查即可
            this.canvasWrapper1.OnCheckLeadInOrOutWire(true);
        }
        private void btnClearLeadInOrOutWire_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnClearLeadWire();
        }
    }
}
