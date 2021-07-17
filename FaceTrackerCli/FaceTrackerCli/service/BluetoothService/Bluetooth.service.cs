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
            get { return yAngle; }
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
            bluetooth = new SerialPort();
            bluetooth.BaudRate = BAUD_RATE;
            bluetooth.PortName = portName;

            XAngle = 0;
            YAngle = 0;
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
            bluetooth.Open();
        }

        /// <summary>
        /// シリアルポートを切断する
        /// </summary>
        public void Close()
        {
            bluetooth.Close();
        }

        /// <summary>
        /// サーボモータを動作させる
        /// </summary>
        /// <param name="servoCh">動作させるサーボのチャンネル</param>
        /// <param name="angle">サーボを動作させるアングル(0~90)</param>
        /// <returns>送信したコマンド</returns>
        private byte[] SendCommand(int servoCh, int angle)
        {
           if (!bluetooth.IsOpen) { return new byte[0]; }

            int ch = servoCh == 0 ? 0x0 : 0x80;

            char c = Convert.ToChar((ch | angle));
            byte[] cmd = new byte[] { Convert.ToByte(c) };
            bluetooth.Write(cmd, 0, cmd.Length);

            return cmd;
        }

        public int Recived()
        {
            if (!bluetooth.IsOpen) { return -1; }

            int data = bluetooth.ReadChar();
            return data;
        }


        public byte[] ServoUp()
        {
            YAngle += INCREASE_VALUE;
            return SendCommand(SERVO_Y_CHANNEL, YAngle);
        }

        public byte[] ServoDown()
        {
            YAngle -= INCREASE_VALUE;
            return SendCommand(SERVO_Y_CHANNEL, YAngle);
        }


        public byte[] ServoRight()
        {
            XAngle += INCREASE_VALUE;
            return SendCommand(SERVO_X_CHANNEL, XAngle);
        }


        public byte[] ServoLeft()
        {
            XAngle -= INCREASE_VALUE;
            return SendCommand(SERVO_X_CHANNEL, XAngle);
        }
    }
}
