namespace WSX.Hardware.IO
{
    public interface IAssistantIO
    {
        void SetBits(uint pins);
        void ResetBits(uint pins);
        void WriteBit(uint pin, uint bitVal);
        void WriteBits(uint multiPinVal);
    }
}
