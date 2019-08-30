namespace WSX.DrawService.CanvasControl
{
    public enum DrawObjectMouseDown
    {
        Unknown,
        Done,  //图形对象绘制完成
        DoneRepeat,//图形对象绘制完成，但需要创建一个同样类型的对象
        Continue,//图形对象需要额外的鼠标输入操作
        Change
    }
}
