using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class UndoRedoBuffer
    {
        private List<EditCommandBase> undoBuffer = new List<EditCommandBase>();
        private List<EditCommandBase> redoBuffer = new List<EditCommandBase>();
        private bool canCapture = true;

        public bool Dirty { get; set; }

        public bool CanCapture
        {
            get
            {
                return this.canCapture;
            }
        }

        public bool CanUndo
        {
            get
            {
                return this.undoBuffer.Count > 0;
            }
        }

        public bool CanRedo
        {
            get
            {
                return this.redoBuffer.Count > 0;
            }
        }
        public void Clear()
        {
            this.undoBuffer.Clear();
            this.redoBuffer.Clear();
        }

        public void AddCommand(EditCommandBase editCommandBase)
        {
            if (this.canCapture && editCommandBase != null)
            {
                this.undoBuffer.Add(editCommandBase);
                this.redoBuffer.Clear();
                this.Dirty = true;
            }
        }

        public bool DoUndo(IModel dataModel)
        {
            if(this.undoBuffer.Count==0)
            {
                return false;
            }
            this.canCapture = false;
            EditCommandBase editCommandBase = this.undoBuffer[this.undoBuffer.Count - 1];
            bool result = editCommandBase.DoUndo(dataModel);
            this.undoBuffer.RemoveAt(this.undoBuffer.Count - 1);
            this.redoBuffer.Add(editCommandBase);
            this.canCapture = true;
            this.Dirty = true;
            return result;
        }

        public bool DoRedo(IModel dataModel)
        {
            if(this.redoBuffer.Count==0)
            {
                return false;
            }
            this.canCapture = false;
            EditCommandBase editCommandBase = this.redoBuffer[this.redoBuffer.Count - 1];
            bool result = editCommandBase.DoRedo(dataModel);
            this.redoBuffer.RemoveAt(this.redoBuffer.Count - 1);
            this.undoBuffer.Add(editCommandBase);
            this.canCapture = true;
            this.Dirty = true;
            return result;
        }

    }
}
