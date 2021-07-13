using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerCli.service.BluetoothService
{
    public class BluetoothService
    {
        /// <summary>
        /// 通信速度
        /// </summary>
        public const int BAUD_RATE = 9600;

        /// <summary>
        /// サーボのX軸のチャンネル
        /// </summary>
        private const int SERVO_X_CHANNEL = 0;
        /// <summary>
        /// サーボのY軸のチャンネル
        /// </summary>
        private const int SERVO_Y_CHANNEL = 1;

        /// <summary>
        /// サーボを1回の操作で動かすアングル値
        /// </summary>
        private const int INCREASE_VALUE = 3;

        /// <summary>
        /// X座標アングル
        /// </summary>
        private int xAngle;
        /// <summary>
        /// X座標アングル(0~90)
        /// </summary>
        public int XAngle
        {
            get { return xAngle; }
            private set
            {
                int angle = value;
                if (angle < 0) { angle = 0; }
                if (angle > 90) { angle = 90; }

                xAngle = angle;
            }
        }

        /// <summary>
        /// Y座標アングル
        /// </summary>
        private int yAngle;
        /// <summary>
        /// Y座標アングル(0~90)
        /// </summary>
        public int YAngle
        {
            get { return this.yAngle; }
            private set
            {
                int angle = value;
                if (angle < 0) { angle = 0; }
                if (angle > 90) { angle = 90; }

                yAngle = angle;
            }
        }


        private SerialPort bluetooth;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portName">COMポート名</param>
        public BluetoothService(string portName)
        {
            this.bluetooth = new SerialPort();
            this.bluetooth.BaudRate = BAUD_RATE;
            this.bluetooth.PortName = portName;

            this.XAngle = 0;
            this.YAngle = 0;
        }

        /// <summary>
        /// COMポート名を取得する
        /// </summary>
        /// <returns></returns>
        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// シリアルポートを接続する
        /// </summary>
        public void Open()
        {
            this.bluetooth.Open();
        }

        /// <summary>
        /// シリアルポートを切断する
        /// </summary>
        public void Close()
        {
            this.bluetooth.Close();
        }

        /// <summary>
        /// サーボモータを動作させる
        /// </summary>
        /// <param name="servoCh">動作させるサーボのチャンネル</param>
        /// <param name="angle">サーボを動作させるアングル(0~90)</param>
        private void SendCommand(int servoCh, int angle)
        {
            if (!this.bluetooth.IsOpen) { return; }

            int ch = servoCh == 0 ? 0x0 : 0x80;

            char c = Convert.ToChar((ch | angle));
            byte[] cmd = new byte[] { Convert.ToByte(c) };
            this.bluetooth.Write(cmd, 0, cmd.Length);
        }

        public int Recived()
        {
            if (!this.bluetooth.IsOpen) { return -1; }

            int data = this.bluetooth.ReadChar();
            return data;
        }


        public void ServoUp()
        {
            this.YAngle += INCREASE_VALUE;
            this.SendCommand(SERVO_Y_CHANNEL, YAngle);
        }

        public void ServoDown()
        {
            this.YAngle -= INCREASE_VALUE;
            this.SendCommand(SERVO_Y_CHANNEL, YAngle);
        }


        public void ServoRight()
        {
            this.XAngle += INCREASE_VALUE;
            this.SendCommand(SERVO_X_CHANNEL, XAngle);
        }


        public void ServoLeft()
        {
            this.XAngle -= INCREASE_VALUE;
            this.SendCommand(SERVO_X_CHANNEL, XAngle);
        }
    }
}
