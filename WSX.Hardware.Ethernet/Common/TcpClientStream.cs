using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace WSX.Hardware.Common
{
    public class TcpClientStream : IDisposable
    {
        private string ipAddress;
        private int port;
        private Socket tcpClient;
        private const int RETRY_COUNT = 5;
        //private const int RETRY_DELAY_MS = 500;
        private const int BUFFER_SIZE = 1024 * 1024;
        private static object SyncRoot = new object();

        public static TcpClientStream SearchDevice(string ipAddress, int port)
        {
            TcpClientStream tcpClient = null;

            try
            {
                if (IPAddress.TryParse(ipAddress, out IPAddress ip))
                {
                    Ping p = new Ping();
                    var reply = p.Send(ip, 2000);
                    if (reply.Status == IPStatus.Success)
                    {
                        var tmp = new TcpClientStream(ipAddress, port);
                        if (tmp.Connect())
                        {
                            tcpClient = tmp;
                        }
                    }
                }
            }
            catch
            {
                
            }

            return tcpClient;
        }

        public static bool IsExist(string ipAddress, int port)
        {
            bool valid = false;
            var device = SearchDevice(ipAddress, port);
            if (device != null)
            {
                valid = true;
                device.Dispose();
                device = null;
            }
            return valid;
        }

        public TcpClientStream(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public bool Connect()
        {
            this.tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = true,
                ReceiveBufferSize = BUFFER_SIZE,
                SendBufferSize = BUFFER_SIZE,
                ReceiveTimeout = 5000,
                SendTimeout = 5000,           
            };
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(this.ipAddress), this.port);

            bool success = true;
            try
            {
                this.tcpClient.Connect(ie);
            }
            catch
            {
                success = false;
                //throw new Exception("Unable to connect the ethernet board！");
            }

            return success;
        }

        private void Disconnect()
        {
            if (this.tcpClient != null)
            {
                if (this.tcpClient.Connected)
                    this.tcpClient.Shutdown(SocketShutdown.Both);
                this.tcpClient.Close();
            }
        }

        public byte[] Query(byte[] command)
        {
            lock (SyncRoot)
            {
                byte[] buffer = new byte[BUFFER_SIZE];
                bool success = false;

                for (int i = 0; i < RETRY_COUNT; i++)
                {
                    try
                    {
                        this.tcpClient.Send(command);
                        int size = this.tcpClient.Receive(buffer);
                        Array.Resize(ref buffer, size);
                        success = true;
                        break;
                    }
                    catch
                    {
                        this.Disconnect();
                        if (!this.Connect())
                        {
                            throw new Exception("Unable to connect the ethernet board");
                        }
                    }
                }

                if (!success)
                {
                    throw new Exception("Error occurred when send command to the ethernet board!");
                }

                return buffer;
            }
        }

        public byte[] Query(byte[] command, Predicate<byte[]> predicate, out bool valid)
        {          
            lock (SyncRoot)
            {
                var res = new List<byte>();
                bool success = false;
                valid = false;

                for (int i = 0; i < RETRY_COUNT; i++)                 //retry to handle exception
                {
                    try
                    {
                        this.tcpClient.Send(command);
                        for (int j = 0; j < RETRY_COUNT; j++)         //retry to handle uncompleted data
                        {
                            var buffer = new byte[BUFFER_SIZE];
                            int size = this.tcpClient.Receive(buffer);
                            if (size != 0)
                            {
                                Array.Resize(ref buffer, size);
                                res.AddRange(buffer);
                            }

                            if (Protocol.Verify(res.ToArray()))
                            {
                                valid = true;
                                break;
                            }
                            else
                            {
                                Thread.Sleep(10);
                            }
                        }
                        success = true;
                        break;
                    }
                    catch
                    {
                        this.Disconnect();
                        if (!this.Connect())
                        {
                            throw new Exception("Unable to connect the ethernet board");
                        }
                    }
                }

                if (!success)
                {
                    throw new Exception("Error occurred when send command to the ethernet board!");
                }

                return res.ToArray();
            }
        }

        public void Dispose()
        {
            this.Disconnect();
        }

        ~TcpClientStream()
        {
            this.Dispose();
        }
    }

}
