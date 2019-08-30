using Microsoft.Win32;
using System.Collections.Generic;
using System.Threading;
using WSX.Hardware.Models;

namespace WSX.Hardware.Motor
{
    public class MotorInfo
    {
        public int CurrrentPos { get; set; }
        public int Encoder { get; set; }
        public int ZeroPos { get; set; }
        public bool IsMoving { get; set; }
        
        //public MotorInfo(int currentPos, int zeroPos)
        //{
        //    this.CurrrentPos = currentPos;
        //    this.ZeroPos = zeroPos;
        //    this.IsMoving = false;
        //}
    }

    public class MotorInfoManager
    {
        private static MotorInfoManager instance;
        private static object SyncRoot = new object();

        //private readonly string MOTOR_INFO_PATH = AppDomain.CurrentDomain.BaseDirectory + @"Data\MotorInfo.json";
        private Dictionary<AxisTypes, MotorInfo> motorInfoMap;
        //private ReaderWriterLock rwLock = new ReaderWriterLock();
        private Mutex mutex = new Mutex();
        private readonly string REGISTRY_PATH = @"SOFTWARE\WSXCut\";

        private MotorInfoManager()
        {
            this.motorInfoMap = new Dictionary<AxisTypes, MotorInfo>
            {
                { AxisTypes.AxisX, new MotorInfo() },
                { AxisTypes.AxisY, new MotorInfo() },
                { AxisTypes.AxisZ, new MotorInfo() },
                { AxisTypes.AxisA, new MotorInfo() },
                { AxisTypes.AxisB, new MotorInfo() },
                { AxisTypes.AxisC, new MotorInfo() },
                { AxisTypes.AxisU, new MotorInfo() },
                { AxisTypes.AxisV, new MotorInfo() },
                { AxisTypes.AxisW, new MotorInfo() },
            };

            this.Initialize();
        }

        public static MotorInfoManager Instance
        {
            get
            {
                return instance ?? (instance = new MotorInfoManager());
            }
        }

        public bool IsValid { get; private set; }

        public int GetCurrentPos(AxisTypes axis)
        {
            this.mutex.WaitOne();
            int pos = this.motorInfoMap[axis].CurrrentPos;
            this.mutex.ReleaseMutex();
            return pos;
        }

        public void SetCurrentPos(AxisTypes axis, int pos)
        {
            this.mutex.WaitOne();
            this.motorInfoMap[axis].CurrrentPos = pos;
            this.Save();
            this.mutex.ReleaseMutex();
        }

        public int GetEncoder(AxisTypes axis)
        {
            this.mutex.WaitOne();
            int pos = this.motorInfoMap[axis].Encoder;
            this.mutex.ReleaseMutex();
            return pos;
        }

        public void SetEncoder(AxisTypes axis, int pos)
        {
            this.mutex.WaitOne();
            this.motorInfoMap[axis].Encoder = pos;
            this.Save();
            this.mutex.ReleaseMutex();
        }

        public int GetZeroPos(AxisTypes axis)
        {
            this.mutex.WaitOne();
            int pos = this.motorInfoMap[axis].ZeroPos;
            this.mutex.ReleaseMutex();
            return pos;
        }

        public void SetZeroPos(AxisTypes axis, int pos)
        {
            this.mutex.WaitOne();
            this.motorInfoMap[axis].ZeroPos = pos;
            this.Save();
            this.mutex.ReleaseMutex();
        }

        public void SetMovingStatus(AxisTypes axis, bool flag)
        {
            this.mutex.WaitOne();
            this.motorInfoMap[axis].IsMoving = flag;
            this.Save();
            this.mutex.ReleaseMutex();
        }

        private void Initialize()
        {                
            if (!this.IsMotorInfoExists)
            {
                this.Save();
                this.IsValid = false;
            }
            else
            {
                this.Load();
                bool flag = true;
                foreach (var m in this.motorInfoMap.Keys)
                { 
                    if (this.motorInfoMap[m].IsMoving)
                    {
                        flag = false;
                        break;
                    }
                }
                this.IsValid = flag;
            }
        }

        private void Save()
        {          
            foreach (var m in this.motorInfoMap)
            {
                using (var key = Registry.LocalMachine.CreateSubKey(REGISTRY_PATH + m.Key.ToString()))
                {
                    key.SetValue("CurrentPos", m.Value.CurrrentPos.ToString());
                    key.SetValue("Encoder", m.Value.Encoder.ToString());
                    //key.SetValue("ZeroPos", m.Value.ZeroPos.ToString());
                    key.SetValue("IsMoving", m.Value.IsMoving.ToString());
                }
            }
        }

        private void Load()
        {
            foreach (var m in this.motorInfoMap)
            {
                using (var regKey = Registry.LocalMachine.OpenSubKey(REGISTRY_PATH + m.Key.ToString()))
                {                  
                    m.Value.CurrrentPos = int.Parse(regKey.GetValue("CurrentPos").ToString());
                    m.Value.Encoder = int.Parse(regKey.GetValue("Encoder").ToString());
                    //m.Value.ZeroPos = int.Parse(regKey.GetValue("ZeroPos").ToString());
                    m.Value.IsMoving = bool.Parse(regKey.GetValue("IsMoving").ToString());
                }
            }
        }

        private bool IsMotorInfoExists
        {
            get
            {
                bool flag = true;

                var regKey = Registry.LocalMachine.OpenSubKey(REGISTRY_PATH);
                if (regKey == null)
                {
                    flag = false;
                    regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_PATH);
                }
                regKey.Close();

                return flag;
            }
        }

    }
}
