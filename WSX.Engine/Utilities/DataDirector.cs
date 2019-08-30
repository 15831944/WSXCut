namespace WSX.Engine.Utilities
{
    public class DataDirector
    {
        public static void Bulid(DataBuilder builder)
        {
            builder.CalMovementData();
            builder.InsertEmptyLine();
            builder.CalOutLine();
        }
    }
}
