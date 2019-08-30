using DevExpress.XtraBars;
using DevExpress.XtraRichEdit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.WSXCut.Views.CustomControl.Menu
{
    /// <summary>
    /// 组合带头部按钮的下拉式弹出按钮
    /// </summary>
    public class ComboHeadBarSubItem : PopupMergeBasedItem
    {
        public ComboHeadBarSubItem() { }

        [Category("Behavior"),
        Description("弹出菜单集合"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),]
        /// <summary>
        /// 弹出菜单集合
        /// </summary>
        public BarItemLinkCollection ItemLinks
        {
            get
            {
                if (base.PopupMenu != null) return base.PopupMenu.ItemLinks;
                return null;
            }
        }

    }
}
