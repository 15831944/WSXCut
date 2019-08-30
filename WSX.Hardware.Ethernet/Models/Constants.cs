namespace WSX.Hardware.Models
{
    public class IOPinDefinition
    {
        public static uint Pin_0 = 0x01;
        public static uint Pin_1 = 0x01 << 1;
        public static uint Pin_2 = 0x01 << 2;
        public static uint Pin_3 = 0x01 << 3;
        public static uint Pin_4 = 0x01 << 4;
        public static uint Pin_5 = 0x01 << 5;
        public static uint Pin_6 = 0x01 << 6;
        public static uint Pin_7 = 0x01 << 7;
        public static uint Pin_8 = 0x01 << 8;
        public static uint Pin_9 = 0x01 << 9;
        public static uint Pin_10 = 0x01 << 10;
        public static uint Pin_11 = 0x01 << 11;
        public static uint Pin_12 = 0x01 << 12;
        public static uint Pin_13 = 0x01 << 13;
        public static uint Pin_14 = 0x01 << 14;
        public static uint Pin_15 = 0x01 << 15;
    }

    //public static class LaserCommands
    //{
    //    public readonly static uint LaserOn = 0xA5A5A5A5;
    //    public readonly static uint LaserOff = 0x00;
    //}

    public static class Commands
    {
        public readonly static uint MovementData = 0xA1;
        public readonly static uint SetMotorStatus = 0xA2;
        public readonly static uint Laser1 = 0xA3;
        public readonly static uint Laser2 = 0xA4;
        public readonly static uint Action = 0xA5;
        public readonly static uint SetOutput = 0xA8;
        public readonly static uint SetMotorEnable = 0xA9;
        public readonly static uint SetMotorAlarm = 0xAA;

        public readonly static uint GetMotorStatus = 0xB1;
        public readonly static uint GetVersion = 0xB2;
        public readonly static uint GetHeight = 0xB3;
        public readonly static uint GetInput = 0xB4;
        public readonly static uint GetAnalog = 0xB5;
        public readonly static uint GetCacheStatus = 0xB6;
        public readonly static uint GetMotorAlarm = 0xB7;
    }
}
