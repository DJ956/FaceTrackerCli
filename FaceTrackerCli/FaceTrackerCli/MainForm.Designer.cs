﻿
namespace FaceTrackerCli
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.labelAngle = new System.Windows.Forms.Label();
            this.buttonServoUp = new System.Windows.Forms.Button();
            this.buttonServoLeft = new System.Windows.Forms.Button();
            this.buttonServoRight = new System.Windows.Forms.Button();
            this.buttonServoDown = new System.Windows.Forms.Button();
            this.pictureBoxIpl1 = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 404);
            this.splitContainer1.SplitterDistance = 549;
            this.splitContainer1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.pictureBoxIpl1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(549, 404);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxPort);
            this.flowLayoutPanel1.Controls.Add(this.buttonConnect);
            this.flowLayoutPanel1.Controls.Add(this.buttonDisconnect);
            this.flowLayoutPanel1.Controls.Add(this.labelAngle);
            this.flowLayoutPanel1.Controls.Add(this.buttonServoUp);
            this.flowLayoutPanel1.Controls.Add(this.buttonServoLeft);
            this.flowLayoutPanel1.Controls.Add(this.buttonServoRight);
            this.flowLayoutPanel1.Controls.Add(this.buttonServoDown);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(247, 404);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(20, 20, 10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(62, 15);
            this.comboBoxPort.Margin = new System.Windows.Forms.Padding(0, 15, 10, 10);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(154, 20);
            this.comboBoxPort.TabIndex = 1;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(20, 72);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(20);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "接続";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(135, 72);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(20);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 3;
            this.buttonDisconnect.Text = "切断";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // labelAngle
            // 
            this.labelAngle.AutoSize = true;
            this.labelAngle.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelAngle.Location = new System.Drawing.Point(20, 135);
            this.labelAngle.Margin = new System.Windows.Forms.Padding(20);
            this.labelAngle.Name = "labelAngle";
            this.labelAngle.Size = new System.Drawing.Size(160, 16);
            this.labelAngle.TabIndex = 4;
            this.labelAngle.Text = "サーボアングル - X:0 Y:0";
            // 
            // buttonServoUp
            // 
            this.buttonServoUp.Location = new System.Drawing.Point(70, 191);
            this.buttonServoUp.Margin = new System.Windows.Forms.Padding(70, 20, 20, 20);
            this.buttonServoUp.Name = "buttonServoUp";
            this.buttonServoUp.Size = new System.Drawing.Size(75, 23);
            this.buttonServoUp.TabIndex = 5;
            this.buttonServoUp.Text = "↑";
            this.buttonServoUp.UseVisualStyleBackColor = true;
            this.buttonServoUp.Click += new System.EventHandler(this.buttonServoUp_Click);
            // 
            // buttonServoLeft
            // 
            this.buttonServoLeft.Location = new System.Drawing.Point(20, 237);
            this.buttonServoLeft.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.buttonServoLeft.Name = "buttonServoLeft";
            this.buttonServoLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonServoLeft.TabIndex = 6;
            this.buttonServoLeft.Text = "←";
            this.buttonServoLeft.UseVisualStyleBackColor = true;
            this.buttonServoLeft.Click += new System.EventHandler(this.buttonServoLeft_Click);
            // 
            // buttonServoRight
            // 
            this.buttonServoRight.Location = new System.Drawing.Point(118, 237);
            this.buttonServoRight.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.buttonServoRight.Name = "buttonServoRight";
            this.buttonServoRight.Size = new System.Drawing.Size(75, 23);
            this.buttonServoRight.TabIndex = 7;
            this.buttonServoRight.Text = "→";
            this.buttonServoRight.UseVisualStyleBackColor = true;
            this.buttonServoRight.Click += new System.EventHandler(this.buttonServoRight_Click);
            // 
            // buttonServoDown
            // 
            this.buttonServoDown.Location = new System.Drawing.Point(70, 283);
            this.buttonServoDown.Margin = new System.Windows.Forms.Padding(70, 20, 20, 20);
            this.buttonServoDown.Name = "buttonServoDown";
            this.buttonServoDown.Size = new System.Drawing.Size(75, 23);
            this.buttonServoDown.TabIndex = 8;
            this.buttonServoDown.Text = "↓";
            this.buttonServoDown.UseVisualStyleBackColor = true;
            this.buttonServoDown.Click += new System.EventHandler(this.buttonServoDown_Click);
            // 
            // pictureBoxIpl1
            // 
            this.pictureBoxIpl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxIpl1.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxIpl1.Name = "pictureBoxIpl1";
            this.pictureBoxIpl1.Size = new System.Drawing.Size(100, 0);
            this.pictureBoxIpl1.TabIndex = 0;
            this.pictureBoxIpl1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Label labelAngle;
        private System.Windows.Forms.Button buttonServoUp;
        private System.Windows.Forms.Button buttonServoLeft;
        private System.Windows.Forms.Button buttonServoRight;
        private System.Windows.Forms.Button buttonServoDown;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl1;
    }
}
