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
using System.Windows.Input;

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
        /// true:カメラ切替注, false:カメラ切替中ではない
        /// </summary>
        private bool camChange;


        /// <summary>
        /// シリアル通信接続状態のステータス
        /// </summary>
        private bool _connect;
        /// <summary>
        /// シリアル通信接続状態のステータスのプロパティ
        /// </summary>
        public bool Connect
        {
            get { return _connect; }
            set
            {
                _connect = value;

                comboBoxPort.Enabled = !_connect;
                buttonConnect.Enabled = !_connect;
                buttonDisconnect.Enabled = _connect;
                //サーボ操作ボタン
                buttonServoUp.Enabled = _connect;
                buttonServoDown.Enabled = _connect;
                buttonServoLeft.Enabled = _connect;
                buttonServoRight.Enabled = _connect;
            }
        }

        /// <summary>
        /// true:カメラ接続中, false:カメラ切断中
        /// </summary>
        private bool _camConnect;
        /// <summary>
        /// カメラ接続スタータスのプロパティ
        /// </summary>
        public bool CamConnect
        {
            get { return _camConnect; }
            set
            {
                _camConnect = value;
                buttonCamStart.Enabled = !_camConnect;
                buttonCamStop.Enabled = _camConnect;
                buttonChangeCamera.Enabled = _camConnect;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            //非同期処理設定
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorkerDoWork;
            //シリアルポート名設定
            comboBoxPort.Items.AddRange(BluetoothService.GetPortNames());
            if(comboBoxPort.Items.Count > 0) { comboBoxPort.SelectedIndex = 0; }
            Connect = false;

            try
            {
                //サービス初期化
                cameraService = new CameraService();
                drawingService = new DrawingService(pictureBox1.Width, pictureBox1.Height);
                faceDetectService = new FaceDetectService();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            graphics = pictureBox1.CreateGraphics();

            CamConnect = false;

            //カメラ切り替えフラグ
            camChange = false;

            //キー入力監視
            Application.Idle += new EventHandler(WatchKeyPress);
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
                //カメラ切り替え
                if (camChange)
                {
                    cameraService.ChangeCameraAndOpen();
                    camChange = false;
                }
                //キャプチャ
                cameraService.Capture();
                drawingService.DrawCenterRectangle(cameraService.frame);
                //文字描画
                if (bluetoothService != null)
                {
                    string text = $"Servo Angle - X:{bluetoothService.XAngle * 2} Y:{bluetoothService.YAngle * 2}";
                    drawingService.PutText(cameraService.frame, text);
                }

                //顔認識描画
                List<Rect> rects = new List<Rect>(faceDetectService.DetectFace(cameraService.frame));
                Parallel.ForEach(rects, r =>
                {
                    drawingService.DrawRectangle(cameraService.frame, r, Scalar.Red);
                });


                graphics.DrawImage(cameraService.CaptureImg, 0, 0, CameraService.WIDTH, CameraService.HEIGHT);
            }

            e.Cancel = true;
        }

        private void MainForm_Load(object sender, EventArgs e) { }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { Exit(); }

        private void exitMenuItem_Click(object sender, EventArgs e) { Exit(); }

        /// <summary>
        /// 終了する
        /// </summary>
        private void Exit()
        {
            backgroundWorker.CancelAsync();
            backgroundWorker.Dispose();
            faceDetectService.Dispose();
            cameraService.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// キー入力を受け付けて、サーボを操作させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WatchKeyPress(object sender, EventArgs e)
        {
            if (bluetoothService == null || !Connect) { return; }

            await Task.Delay(1500);

            //アップ
            if (Keyboard.IsKeyDown(Key.Up)) { buttonServoUp.PerformClick(); }
            //ダウン
            if (Keyboard.IsKeyDown(Key.Down)) { buttonServoDown.PerformClick(); }
            //レフト
            if (Keyboard.IsKeyDown(Key.Left)) { buttonServoLeft.PerformClick(); }
            //ライト
            if (Keyboard.IsKeyDown(Key.Right)) { buttonServoRight.PerformClick(); }
        }

        /// <summary>
        /// シリアル通信接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (comboBoxPort.SelectedIndex == -1 || comboBoxPort.SelectedItem.ToString() == "")
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
        /// シリアル通信切断
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
                CamConnect = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            CamConnect = false;
        }

        /// <summary>
        /// カメラ切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangeCamera_Click(object sender, EventArgs e)
        {
            if (cameraService == null) { return; }
            camChange = true;
        }


    }
}
