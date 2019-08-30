namespace WSX.DXF.IO
{
    internal interface ICodeValueWriter
    {
        short Code { get; }

        object Value { get; }

        long CurrentPosition { get; }

        void Write(short code, object value);
        void WriteByte(byte value);
        void WriteBytes(byte[] value);
        void WriteShort(short value);
        void WriteInt(int value);
        void WriteLong(long value);
        void WriteBool(bool value);
        void WriteDouble(double value);
        void WriteString(string value);
        void Flush();
    }
}