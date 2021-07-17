using FaceTrackerCli.service.BluetoothService;
using FaceTrackerCli.service.CameraService;
using FaceTrackerCli.service.DrawingService;
using FaceTrackerCli.service.FaceDetectService;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceTrackerCli
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private BluetoothService bluetoothService;
        /// <summary>
        /// カメラ接続サービス
        /// </summary>
        private CameraService cameraService;
        /// <summary>
        /// 顔識別サービス
        /// </summary>
        private FaceDetectService faceDetectService;
        /// <summary>
        /// 図形描画サービス
        /// </summary>
        private DrawingService drawingService;
        /// <summary>
        /// バックグラウンド
        /// </summary>
        private BackgroundWorker backgroundWorker;

        private Graphics graphics;


        /// <summary>
        /// 接続状態のステータス
        /// </summary>
        private bool _connect;
        /// <summary>
        /// 接続状態のステータスのプロパティ
        /// </summary>
        public bool Connect
        {
            get { return _connect; }
            set
            {
                _connect = value;
                //接続
                if (_connect)
                {
                    comboBoxPort.Enabled = false;
                    buttonConnect.Enabled = false;
                    buttonDisconnect.Enabled = true;
                    //サーボ操作ボタン
                    buttonServoUp.Enabled = true;
                    buttonServoDown.Enabled = true;
                    buttonServoLeft.Enabled = true;
                    buttonServoRight.Enabled = true;
                }
                else//切断
                {
                    comboBoxPort.Enabled = true;
                    buttonConnect.Enabled = true;
                    buttonDisconnect.Enabled = false;
                    //サーボ操作ボタン
                    buttonServoUp.Enabled = false;
                    buttonServoDown.Enabled = false;
                    buttonServoLeft.Enabled = false;
                    buttonServoRight.Enabled = false;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorkerDoWork;

            comboBoxPort.Items.AddRange(BluetoothService.GetPortNames());
            comboBoxPort.SelectedIndex = 0;
            Connect = false;

            cameraService = new CameraService();
            drawingService = new DrawingService(pictureBox1.Width, pictureBox1.Height);
            faceDetectService = new FaceDetectService();

            graphics = pictureBox1.CreateGraphics();
        }


        /// <summary>
        /// 画面表示用のサーボアングルのラベルにテキストを設定する
        /// </summary>
        /// <param name="xAngle"></param>
        /// <param name="yAngle"></param>
        private void setAngleText(int xAngle, int yAngle)
        {
            labelAngle.Text = $"サーボアングル - X:{xAngle * 2} Y:{yAngle * 2}";
        }

        /// <summary>
        /// 送信コマンドをラベルにテキスト設定する
        /// </summary>
        /// <param name="cmd"></param>
        private void setCmdText(byte[] cmd)
        {
            if (cmd.Length == 0) { return; }
            const int mask = 0x80;
            int data = Convert.ToInt32(cmd[0]);
            data = data & ~mask;
            data = data * 2;
            statusLabelCmd.Text = string.Format("Command:0x{0:X2} - 0b{1} - Angle:{2}",
                cmd[0], Convert.ToString(cmd[0], 2), data);
        }

        /// <summary>
        /// バックグラウンド処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                cameraService.Capture();
                drawingService.DrawCenterRectangle(cameraService.frame);
                if (bluetoothService != null)
                {
                    string text = $"Servo Angle - X:{bluetoothService.XAngle} Y:{bluetoothService.YAngle}";
                    drawingService.PutText(cameraService.frame, text);
                }

                List<Rect> rects = new List<Rect>(faceDetectService.DetectFace(cameraService.frame));
                Parallel.ForEach(rects, r =>
                {
                    drawingService.DrawRectangle(cameraService.frame, r, Scalar.Red);
                });


                graphics.DrawImage(cameraService.CaptureImg, 0, 0, CameraService.WIDTH, CameraService.HEIGHT);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonCamStop.PerformClick();
            faceDetectService.Dispose();
        }

        /// <summary>
        /// キー入力を受け付けて、サーボを操作させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (bluetoothService == null || !Connect) { return; }
            //アップ
            if (e.KeyCode == Keys.Left) { buttonServoUp.PerformClick(); }
            //ダウン
            if (e.KeyCode == Keys.Down) { buttonServoDown.PerformClick(); }
            //レフト
            if (e.KeyCode == Keys.Left) { buttonServoLeft.PerformClick(); }
            //ライト
            if (e.KeyCode == Keys.Right) { buttonServoRight.PerformClick(); }
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (comboBoxPort.SelectedIndex == -1)
            {
                MessageBox.Show("接続するポートを選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string portName = comboBoxPort.SelectedItem.ToString();
            bluetoothService = new BluetoothService(portName);
            bluetoothService.Open();
            setAngleText(bluetoothService.XAngle, bluetoothService.YAngle);
            Connect = true;
        }

        /// <summary>
        /// 切断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            //切断
            if (bluetoothService != null) { bluetoothService.Close(); }
            Connect = false;
        }


        private void buttonServoUp_Click(object sender, EventArgs e)
        {
            byte[] cmd = bluetoothService.ServoUp();
            setCmdText(cmd);
            setAngleText(bluetoothService.XAngle, bluetoothService.YAngle);

        }

        private void buttonServoDown_Click(object sender, EventArgs e)
        {
            byte[] cmd = bluetoothService.ServoDown();
            setCmdText(cmd);
            setAngleText(bluetoothService.XAngle, bluetoothService.YAngle);
        }

        private void buttonServoLeft_Click(object sender, EventArgs e)
        {
            byte[] cmd = bluetoothService.ServoLeft();
            setCmdText(cmd);
            setAngleText(bluetoothService.XAngle, bluetoothService.YAngle);
        }

        private void buttonServoRight_Click(object sender, EventArgs e)
        {
            byte[] cmd = bluetoothService.ServoRight();
            setCmdText(cmd);
            setAngleText(bluetoothService.XAngle, bluetoothService.YAngle);
        }


        /// <summary>
        /// カメラ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCamStart_Click(object sender, EventArgs e)
        {
            try
            {
                cameraService.Open();
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// カメラ停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCamStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
            cameraService.Close();
        }


    }
}
