using FaceTrackerCli.service.BluetoothService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceTrackerCli
{
    public partial class MainForm : Form
    {

        private BluetoothService bluetoothService;


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
                this._connect = value;
                //接続
                if (_connect)
                {
                    this.comboBoxPort.Enabled = false;
                    this.buttonConnect.Enabled = false;
                    this.buttonDisconnect.Enabled = true;
                }
                else//切断
                {
                    this.comboBoxPort.Enabled = true;
                    this.buttonConnect.Enabled = true;
                    this.buttonDisconnect.Enabled = false;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            this.comboBoxPort.Items.AddRange(BluetoothService.GetPortNames());
            this.Connect = false;
        }


        /// <summary>
        /// 画面表示用のサーボアングルのラベルにテキストを設定する
        /// </summary>
        /// <param name="xAngle"></param>
        /// <param name="yAngle"></param>
        private void setAngleText(int xAngle, int yAngle)
        {
            this.labelAngle.Text = $"サーボアングル - X:{xAngle} Y:{yAngle}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// キー入力を受け付けて、サーボを操作させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.bluetoothService == null || !this.Connect) { return; }
            //アップ
            if (e.KeyCode == Keys.Left) { this.buttonServoUp.PerformClick(); }
            //ダウン
            if (e.KeyCode == Keys.Down) { this.buttonServoDown.PerformClick(); }
            //レフト
            if (e.KeyCode == Keys.Left) { this.buttonServoLeft.PerformClick(); }
            //ライト
            if (e.KeyCode == Keys.Right) { this.buttonServoRight.PerformClick(); }
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (this.comboBoxPort.SelectedIndex == -1)
            {
                MessageBox.Show("接続するポートを選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string portName = this.comboBoxPort.SelectedItem.ToString();
            this.bluetoothService = new BluetoothService(portName);
            this.Connect = true;
        }

        /// <summary>
        /// 切断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            //切断
            if (this.bluetoothService != null) { this.bluetoothService.Close(); }
            this.Connect = false;
        }

        private void buttonServoUp_Click(object sender, EventArgs e)
        {
            this.bluetoothService.ServoUp();
            this.setAngleText(this.bluetoothService.XAngle, this.bluetoothService.YAngle);
        }

        private void buttonServoDown_Click(object sender, EventArgs e)
        {
            this.bluetoothService.ServoDown();
            this.setAngleText(this.bluetoothService.XAngle, this.bluetoothService.YAngle);
        }

        private void buttonServoLeft_Click(object sender, EventArgs e)
        {
            this.bluetoothService.ServoLeft();
            this.setAngleText(this.bluetoothService.XAngle, this.bluetoothService.YAngle);
        }

        private void buttonServoRight_Click(object sender, EventArgs e)
        {
            this.bluetoothService.ServoRight();
            this.setAngleText(this.bluetoothService.XAngle, this.bluetoothService.YAngle);
        }
    }
}
