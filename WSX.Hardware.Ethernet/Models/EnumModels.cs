namespace WSX.Hardware.Models
{
    public enum AxisTypes
    {
        AxisX,
        AxisY,
        AxisZ,
        AxisA,
        AxisB,
        AxisC,
        AxisU,
        AxisV,
        AxisW
    }

    public enum MovementStatus
    {
        Cancel,
        Done,
    }

    public enum MoveTypes
    {
        RelativeMove,
        AbsoluteMove
    }

    public enum ResetDirection
    {
        Negative = -1,
        Positive = 1,
    }
}
