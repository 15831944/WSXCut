using WSX.Hardware.Common;

namespace WSX.Hardware.IO
{
    public class IOController : IAssistantIO
    {
        private readonly TcpClientStream tcpClient;
        private uint ttlData = 0xFFFF;
        private const int SEND_DELAY_MS = 5;

        public IOController(TcpClientStream tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        public void ResetBits(uint pins)
        {
            uint lastContent = this.ReadData();
            uint newContent = lastContent & (~pins);
            this.WriteData(newContent);
            this.ttlData = newContent;
        }

        public void SetBits(uint pins)
        {
            uint lastContent = this.ReadData();
            uint newContent = lastContent | pins;
            this.WriteData(newContent);
            this.ttlData = newContent;
        }

        public void WriteBit(uint pin, uint bitVal)
        {
            if (bitVal == 0)
            {
                this.ResetBits(pin);
            }
            else
            {
                this.SetBits(pin);
            }
        }

        public void WriteBits(uint multiPinVal)
        {
            this.WriteData(multiPinVal);
        }

        private uint ReadData()
        {
            return this.ttlData;
        }

        private void WriteData(uint data)
        {
            //var para = PciProtocol.CreateExtCommand(RegisterDefinition.TTLOut, data);
            //foreach (var pair in para)
            //{
            //    this.tcpClient.WriteMemoryData(pair.Key, pair.Value);
            //    Thread.Sleep(SEND_DELAY_MS);
            //}
        }
    }
}
