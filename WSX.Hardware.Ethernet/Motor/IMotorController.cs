using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;
using WSX.Hardware.Models;

namespace WSX.Hardware.Motor
{
    public class MotorInfoMap<T>
    {
        private Dictionary<AxisTypes, T> posMap = new Dictionary<AxisTypes, T>();

        public MotorInfoMap()
        {         
            //if (typeof(T).IsClass)
            //{
            //    this.posMap.Add(AxisTypes.AxisX, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisY, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisZ, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisA, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisB, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisC, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisU, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisV, Activator.CreateInstance<T>());
            //    this.posMap.Add(AxisTypes.AxisW, Activator.CreateInstance<T>());
            //}
            //else
            //{
            //    this.posMap.Add(AxisTypes.AxisX, default(T));
            //    this.posMap.Add(AxisTypes.AxisY, default(T));
            //    this.posMap.Add(AxisTypes.AxisZ, default(T));
            //    this.posMap.Add(AxisTypes.AxisA, default(T));
            //    this.posMap.Add(AxisTypes.AxisB, default(T));
            //    this.posMap.Add(AxisTypes.AxisC, default(T));
            //    this.posMap.Add(AxisTypes.AxisU, default(T));
            //    this.posMap.Add(AxisTypes.AxisV, default(T));
            //    this.posMap.Add(AxisTypes.AxisW, default(T));
            //}

            var axises = EnumHelper.GetAllEnumMembers<AxisTypes>();          
            foreach (var m in axises)
            {
                this.posMap[m] = typeof(T).IsClass ? Activator.CreateInstance<T>() : default(T);
            }
        }

        public T this[AxisTypes axis]
        {
            get
            {
                return this.posMap[axis];
            }
            set
            {
                this.posMap[axis] = value;
            }
        }
    }

    public class MovementInfo
    {
        public double Distance { get; set; }
        public double Speed { get; set; }

        public MovementInfo(double distance, double speed)
        {
            this.Distance = distance;
            this.Speed = speed;
        }
    }
 
    public interface IMotorController
    {
        event Action<MotorInfoMap<double>> OnReportPositionInfo;
        event Action<MotorInfoMap<MotorStatus>> OnReportStatusInfo;
        event Action<MotorInfoMap<double>> OnReportSpeedInfo;
        event Action<MotorInfoMap<bool>> OnReportAlarmInfo;
       
        MotorInfoMap<double> CurrentPosInfo { get; }
        MotorInfoMap<MotorStatus> CurrentStatusInfo { get; }
        MotorInfoMap<double> CurrentSpeedInfo { get; }
        MotorInfoMap<bool> CurrentAlarmInfo { get; }

        //double GetZeroPos(AxisTypes axis);
        //void SetZeroPos(AxisTypes axis, double pos);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="token"></param>
        /// <param name="infoHandler">first: position second:speed</param>
        /// <returns></returns>
        Task<MovementStatus> MoveAsync(List<LineSegment> segments, CancellationToken token, Action<MotorInfoMap<double>, MotorInfoMap<double>> infoHandler);
        void Reset(Dictionary<AxisTypes, ResetParameter> resetPara);
        void WriteMovingFlag(bool isMoving);
        void Stop(bool isBlocking = false);
        int GetReversedFactor(AxisTypes axis);
        double GetRatio(AxisTypes axis);
    }
}
