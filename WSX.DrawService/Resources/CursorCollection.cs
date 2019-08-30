using System.Collections.Generic;
using System.Windows.Forms;

namespace WSX.DrawService.Resources
{
    public class CursorCollection
    {
        private Dictionary<object, Cursor> dicCursor = new Dictionary<object, Cursor>();

        public void AddCursor(object key,Cursor cursor)
        {
            this.dicCursor[key] = cursor;
        }

        public void AddCursor(object key,string resourceName)
        {
            Cursor cursor = new Cursor(GetType(), resourceName);
            this.dicCursor[key] = cursor;
        }

        public Cursor GetCursor(object key)
        {
            if(this.dicCursor.ContainsKey(key))
            {
                return this.dicCursor[key];
            }
            return Cursors.Cross;
        }
    }
}
