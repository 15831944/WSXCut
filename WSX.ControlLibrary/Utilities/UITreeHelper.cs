using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WSX.ControlLibrary.Utilities
{
    public static class UITreeHelper
    {
        private static List<Control> Collection = new List<Control>();

        public static List<Control> GetControls(Control control)
        {
            Collection.Clear();
            GetControlCore(control);
            return Collection;
        }

        private static void GetControlCore(Control control)
        {
            Collection.Add(control);
            foreach (Control m in control.Controls)
            {
                GetControlCore(m);
            }
        }

        public static List<Control> SearchAllControl(Control control, object tag, Type searchType)
        {
            Collection.Clear();
            SearchControlCore(control, tag, searchType);
            return Collection;
        }

        private static void SearchControlCore(Control control, object tag, Type searchType)
        {
            bool condition1 = control.GetType() == searchType;
            bool condition2 = tag == control.Tag;
            if (condition1 && condition2)
            {
                Collection.Add(control);
            }

            if (control.Controls.Count != 0)
            {
                foreach (Control m in control.Controls)
                {
                    SearchControlCore(m, tag, searchType);
                }
            }
            //else
            //{
            //    bool condition1 = control.GetType() == searchType;          
            //    bool condition2 = tag == control.Tag;
            //    if (condition1 && condition2)
            //    {
            //        Collection.Add(control);                   
            //    }
            //}
        }

        public static void ChangeLabelText(List<LabelControl> items, string text)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Text = text;
            }
        }

        public static List<T> GetAll<T>(Control control, object tag) where T : Control
        {
            var tmp = SearchAllControl(control, tag, typeof(T));
            return tmp.Select(x => x as T).ToList();
        }
    }
}
