using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using WSX.Logger;

namespace WSX.Hardware.Laser
{
    public class SerialPortStream
    {
        private readonly SerialPort serialPort;
        private const int READ_DATA_INTERVAL_MS = 1;

        public SerialPortStream(string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            try
            {
                this.serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                this.serialPort.Open();
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
            }
        }

        public void WriteData(byte[] buffer)
        {
            this.serialPort.DiscardInBuffer();
            this.serialPort.Write(buffer, 0, buffer.Length);
        }

        public byte[] Query(byte[] buffer)
        {
            List<byte> response = new List<byte>();
            this.WriteData(buffer);
            while (true)
            {
                Thread.Sleep(READ_DATA_INTERVAL_MS);
                int size = this.serialPort.BytesToRead;
                if (size != 0)
                {
                    byte[] temp = new byte[size];
                    this.serialPort.Read(temp, 0, size);
                    response.AddRange(temp);
                }
                else
                {
                    break;
                }
            }
            return response.ToArray();
        }
    }
}
