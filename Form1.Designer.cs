﻿namespace UHFReader09demomain
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabSheet_CMD = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ComboBox_scantime = new System.Windows.Forms.ComboBox();
            this.ComboBox_baud = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ComboBox_dmaxfre = new System.Windows.Forms.ComboBox();
            this.ComboBox_dminfre = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ComboBox_baud2 = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.ComboBox_AlreadyOpenCOM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ClosePort = new System.Windows.Forms.Button();
            this.OpenPort = new System.Windows.Forms.Button();
            this.ComboBox_COM = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabSheet_EPCC1G2 = new System.Windows.Forms.TabPage();
            this.gpSecondInf = new System.Windows.Forms.GroupBox();
            this.linkLabelBetwHipple = new System.Windows.Forms.LinkLabel();
            this.linkLabelIntercoating = new System.Windows.Forms.LinkLabel();
            this.linkLabelCoupling = new System.Windows.Forms.LinkLabel();
            this.checkBoxIsOuterCoating = new System.Windows.Forms.CheckBox();
            this.checkBoxIsBetwHipple = new System.Windows.Forms.CheckBox();
            this.checkBoxIntercoating = new System.Windows.Forms.CheckBox();
            this.checkBoxIsCoupling = new System.Windows.Forms.CheckBox();
            this.checkBoxCarving = new System.Windows.Forms.CheckBox();
            this.comboBoxStandardLen = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxStrength = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxTypeDiametr = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimeTagInstall = new System.Windows.Forms.DateTimePicker();
            this.dateTimeReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOTK = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.butSecondInf = new System.Windows.Forms.Button();
            this.textBoxCertificate = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxPackageNum = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxGostThCon = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxSmeltingNum = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxBatchNum = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxFactoryNum = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.buttCertificateOpen = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.textBoxPipeId = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxTagId = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.StatusBar1 = new System.Windows.Forms.StatusBar();
            this.TStatusPanel = new System.Windows.Forms.StatusBarPanel();
            this.Port = new System.Windows.Forms.StatusBarPanel();
            this.Manufacturername = new System.Windows.Forms.StatusBarPanel();
            this.Timer_Test_ = new System.Windows.Forms.Timer(this.components);
            this.Timer_G2_Read = new System.Windows.Forms.Timer(this.components);
            this.Timer_G2_Alarm = new System.Windows.Forms.Timer(this.components);
            this.Timer_Test_6B = new System.Windows.Forms.Timer(this.components);
            this.Timer_6B_Read = new System.Windows.Forms.Timer(this.components);
            this.Timer_6B_Write = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1.SuspendLayout();
            this.TabSheet_CMD.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.TabSheet_EPCC1G2.SuspendLayout();
            this.gpSecondInf.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TStatusPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manufacturername)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabSheet_CMD);
            this.tabControl1.Controls.Add(this.TabSheet_EPCC1G2);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(669, 445);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // TabSheet_CMD
            // 
            this.TabSheet_CMD.BackColor = System.Drawing.Color.Transparent;
            this.TabSheet_CMD.Controls.Add(this.groupBox3);
            this.TabSheet_CMD.Controls.Add(this.progressBar1);
            this.TabSheet_CMD.Controls.Add(this.GroupBox1);
            this.TabSheet_CMD.Location = new System.Drawing.Point(4, 22);
            this.TabSheet_CMD.Name = "TabSheet_CMD";
            this.TabSheet_CMD.Padding = new System.Windows.Forms.Padding(3);
            this.TabSheet_CMD.Size = new System.Drawing.Size(661, 419);
            this.TabSheet_CMD.TabIndex = 1;
            this.TabSheet_CMD.Text = "Считыватель";
            this.TabSheet_CMD.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ComboBox_scantime);
            this.groupBox3.Controls.Add(this.ComboBox_baud);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.ComboBox_dmaxfre);
            this.groupBox3.Controls.Add(this.ComboBox_dminfre);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(147, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(506, 79);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры считывателя";
            // 
            // ComboBox_scantime
            // 
            this.ComboBox_scantime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_scantime.FormattingEnabled = true;
            this.ComboBox_scantime.Location = new System.Drawing.Point(379, 45);
            this.ComboBox_scantime.Name = "ComboBox_scantime";
            this.ComboBox_scantime.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_scantime.TabIndex = 12;
            // 
            // ComboBox_baud
            // 
            this.ComboBox_baud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_baud.FormattingEnabled = true;
            this.ComboBox_baud.Items.AddRange(new object[] {
            "9600bps",
            "19200bps",
            "38400bps",
            "57600bps",
            "115200bps"});
            this.ComboBox_baud.Location = new System.Drawing.Point(379, 18);
            this.ComboBox_baud.Name = "ComboBox_baud";
            this.ComboBox_baud.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_baud.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(244, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(125, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Max InventoryScanTime:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(244, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Baud Rate:";
            // 
            // ComboBox_dmaxfre
            // 
            this.ComboBox_dmaxfre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_dmaxfre.FormattingEnabled = true;
            this.ComboBox_dmaxfre.Location = new System.Drawing.Point(95, 45);
            this.ComboBox_dmaxfre.Name = "ComboBox_dmaxfre";
            this.ComboBox_dmaxfre.Size = new System.Drawing.Size(100, 21);
            this.ComboBox_dmaxfre.TabIndex = 7;
            this.ComboBox_dmaxfre.SelectedIndexChanged += new System.EventHandler(this.ComboBox_dfreSelect);
            // 
            // ComboBox_dminfre
            // 
            this.ComboBox_dminfre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_dminfre.FormattingEnabled = true;
            this.ComboBox_dminfre.Location = new System.Drawing.Point(95, 18);
            this.ComboBox_dminfre.Name = "ComboBox_dminfre";
            this.ComboBox_dminfre.Size = new System.Drawing.Size(100, 21);
            this.ComboBox_dminfre.TabIndex = 6;
            this.ComboBox_dminfre.SelectedIndexChanged += new System.EventHandler(this.ComboBox_dfreSelect);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Max.Frequency:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Min.Frequency:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(242, 339);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(399, 25);
            this.progressBar1.TabIndex = 43;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ComboBox_baud2);
            this.GroupBox1.Controls.Add(this.label47);
            this.GroupBox1.Controls.Add(this.ComboBox_AlreadyOpenCOM);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.ClosePort);
            this.GroupBox1.Controls.Add(this.OpenPort);
            this.GroupBox1.Controls.Add(this.ComboBox_COM);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Location = new System.Drawing.Point(5, 7);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(136, 222);
            this.GroupBox1.TabIndex = 40;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Считыватель";
            // 
            // ComboBox_baud2
            // 
            this.ComboBox_baud2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_baud2.FormattingEnabled = true;
            this.ComboBox_baud2.Items.AddRange(new object[] {
            "9600bps",
            "19200bps",
            "38400bps",
            "57600bps",
            "115200bps"});
            this.ComboBox_baud2.Location = new System.Drawing.Point(10, 85);
            this.ComboBox_baud2.Name = "ComboBox_baud2";
            this.ComboBox_baud2.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_baud2.TabIndex = 12;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(7, 69);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(61, 13);
            this.label47.TabIndex = 9;
            this.label47.Text = "Baud Rate:";
            // 
            // ComboBox_AlreadyOpenCOM
            // 
            this.ComboBox_AlreadyOpenCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_AlreadyOpenCOM.FormattingEnabled = true;
            this.ComboBox_AlreadyOpenCOM.Location = new System.Drawing.Point(6, 131);
            this.ComboBox_AlreadyOpenCOM.Name = "ComboBox_AlreadyOpenCOM";
            this.ComboBox_AlreadyOpenCOM.Size = new System.Drawing.Size(125, 21);
            this.ComboBox_AlreadyOpenCOM.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Opened COM Port:";
            // 
            // ClosePort
            // 
            this.ClosePort.Location = new System.Drawing.Point(6, 158);
            this.ClosePort.Name = "ClosePort";
            this.ClosePort.Size = new System.Drawing.Size(125, 25);
            this.ClosePort.TabIndex = 5;
            this.ClosePort.Text = "ClosePort";
            this.ClosePort.UseVisualStyleBackColor = true;
            this.ClosePort.Click += new System.EventHandler(this.ClosePort_Click);
            // 
            // OpenPort
            // 
            this.OpenPort.Location = new System.Drawing.Point(6, 41);
            this.OpenPort.Name = "OpenPort";
            this.OpenPort.Size = new System.Drawing.Size(125, 25);
            this.OpenPort.TabIndex = 4;
            this.OpenPort.Text = "Open COM Port";
            this.OpenPort.UseVisualStyleBackColor = true;
            this.OpenPort.Click += new System.EventHandler(this.OpenPort_Click);
            // 
            // ComboBox_COM
            // 
            this.ComboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_COM.FormattingEnabled = true;
            this.ComboBox_COM.Location = new System.Drawing.Point(65, 14);
            this.ComboBox_COM.Name = "ComboBox_COM";
            this.ComboBox_COM.Size = new System.Drawing.Size(65, 21);
            this.ComboBox_COM.TabIndex = 1;
            this.ComboBox_COM.SelectedIndexChanged += new System.EventHandler(this.ComboBox_COM_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(5, 17);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "COM порт：";
            // 
            // TabSheet_EPCC1G2
            // 
            this.TabSheet_EPCC1G2.Controls.Add(this.gpSecondInf);
            this.TabSheet_EPCC1G2.Controls.Add(this.groupBox4);
            this.TabSheet_EPCC1G2.Controls.Add(this.groupBox12);
            this.TabSheet_EPCC1G2.Location = new System.Drawing.Point(4, 22);
            this.TabSheet_EPCC1G2.Name = "TabSheet_EPCC1G2";
            this.TabSheet_EPCC1G2.Size = new System.Drawing.Size(661, 419);
            this.TabSheet_EPCC1G2.TabIndex = 2;
            this.TabSheet_EPCC1G2.Text = "Метка";
            this.TabSheet_EPCC1G2.UseVisualStyleBackColor = true;
            // 
            // gpSecondInf
            // 
            this.gpSecondInf.Controls.Add(this.linkLabelBetwHipple);
            this.gpSecondInf.Controls.Add(this.linkLabelIntercoating);
            this.gpSecondInf.Controls.Add(this.linkLabelCoupling);
            this.gpSecondInf.Controls.Add(this.checkBoxIsOuterCoating);
            this.gpSecondInf.Controls.Add(this.checkBoxIsBetwHipple);
            this.gpSecondInf.Controls.Add(this.checkBoxIntercoating);
            this.gpSecondInf.Controls.Add(this.checkBoxIsCoupling);
            this.gpSecondInf.Controls.Add(this.checkBoxCarving);
            this.gpSecondInf.Controls.Add(this.comboBoxStandardLen);
            this.gpSecondInf.Controls.Add(this.label9);
            this.gpSecondInf.Controls.Add(this.comboBoxStrength);
            this.gpSecondInf.Controls.Add(this.label8);
            this.gpSecondInf.Controls.Add(this.comboBoxTypeDiametr);
            this.gpSecondInf.Controls.Add(this.label7);
            this.gpSecondInf.Controls.Add(this.dateTimeTagInstall);
            this.gpSecondInf.Controls.Add(this.dateTimeReleaseDate);
            this.gpSecondInf.Controls.Add(this.label6);
            this.gpSecondInf.Controls.Add(this.label5);
            this.gpSecondInf.Controls.Add(this.textBoxOTK);
            this.gpSecondInf.Controls.Add(this.label4);
            this.gpSecondInf.Location = new System.Drawing.Point(3, 232);
            this.gpSecondInf.Name = "gpSecondInf";
            this.gpSecondInf.Size = new System.Drawing.Size(655, 186);
            this.gpSecondInf.TabIndex = 6;
            this.gpSecondInf.TabStop = false;
            this.gpSecondInf.Text = "Дополнительная информация";
            // 
            // linkLabelBetwHipple
            // 
            this.linkLabelBetwHipple.AutoSize = true;
            this.linkLabelBetwHipple.Cursor = System.Windows.Forms.Cursors.Default;
            this.linkLabelBetwHipple.Location = new System.Drawing.Point(552, 114);
            this.linkLabelBetwHipple.Name = "linkLabelBetwHipple";
            this.linkLabelBetwHipple.Size = new System.Drawing.Size(63, 13);
            this.linkLabelBetwHipple.TabIndex = 45;
            this.linkLabelBetwHipple.TabStop = true;
            this.linkLabelBetwHipple.Text = "Подробнее";
            this.linkLabelBetwHipple.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabelIntercoating
            // 
            this.linkLabelIntercoating.AutoSize = true;
            this.linkLabelIntercoating.Cursor = System.Windows.Forms.Cursors.Default;
            this.linkLabelIntercoating.Location = new System.Drawing.Point(527, 68);
            this.linkLabelIntercoating.Name = "linkLabelIntercoating";
            this.linkLabelIntercoating.Size = new System.Drawing.Size(63, 13);
            this.linkLabelIntercoating.TabIndex = 44;
            this.linkLabelIntercoating.TabStop = true;
            this.linkLabelIntercoating.Text = "Подробнее";
            this.linkLabelIntercoating.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabelCoupling
            // 
            this.linkLabelCoupling.AutoSize = true;
            this.linkLabelCoupling.Cursor = System.Windows.Forms.Cursors.Default;
            this.linkLabelCoupling.Location = new System.Drawing.Point(451, 45);
            this.linkLabelCoupling.Name = "linkLabelCoupling";
            this.linkLabelCoupling.Size = new System.Drawing.Size(63, 13);
            this.linkLabelCoupling.TabIndex = 43;
            this.linkLabelCoupling.TabStop = true;
            this.linkLabelCoupling.Text = "Подробнее";
            this.linkLabelCoupling.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkBoxIsOuterCoating
            // 
            this.checkBoxIsOuterCoating.AutoSize = true;
            this.checkBoxIsOuterCoating.Location = new System.Drawing.Point(393, 90);
            this.checkBoxIsOuterCoating.Name = "checkBoxIsOuterCoating";
            this.checkBoxIsOuterCoating.Size = new System.Drawing.Size(129, 17);
            this.checkBoxIsOuterCoating.TabIndex = 37;
            this.checkBoxIsOuterCoating.Text = "Покрытие наружное";
            this.checkBoxIsOuterCoating.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsBetwHipple
            // 
            this.checkBoxIsBetwHipple.AutoSize = true;
            this.checkBoxIsBetwHipple.Location = new System.Drawing.Point(393, 113);
            this.checkBoxIsBetwHipple.Name = "checkBoxIsBetwHipple";
            this.checkBoxIsBetwHipple.Size = new System.Drawing.Size(162, 17);
            this.checkBoxIsBetwHipple.TabIndex = 36;
            this.checkBoxIsBetwHipple.Text = "Покрытие межниппельное";
            this.checkBoxIsBetwHipple.UseVisualStyleBackColor = true;
            // 
            // checkBoxIntercoating
            // 
            this.checkBoxIntercoating.AutoSize = true;
            this.checkBoxIntercoating.Location = new System.Drawing.Point(393, 67);
            this.checkBoxIntercoating.Name = "checkBoxIntercoating";
            this.checkBoxIntercoating.Size = new System.Drawing.Size(138, 17);
            this.checkBoxIntercoating.TabIndex = 35;
            this.checkBoxIntercoating.Text = "Покрытие внутреннее";
            this.checkBoxIntercoating.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsCoupling
            // 
            this.checkBoxIsCoupling.AutoSize = true;
            this.checkBoxIsCoupling.Location = new System.Drawing.Point(393, 44);
            this.checkBoxIsCoupling.Name = "checkBoxIsCoupling";
            this.checkBoxIsCoupling.Size = new System.Drawing.Size(59, 17);
            this.checkBoxIsCoupling.TabIndex = 34;
            this.checkBoxIsCoupling.Text = "Муфта";
            this.checkBoxIsCoupling.UseVisualStyleBackColor = true;
            // 
            // checkBoxCarving
            // 
            this.checkBoxCarving.AutoSize = true;
            this.checkBoxCarving.Location = new System.Drawing.Point(393, 22);
            this.checkBoxCarving.Name = "checkBoxCarving";
            this.checkBoxCarving.Size = new System.Drawing.Size(63, 17);
            this.checkBoxCarving.TabIndex = 33;
            this.checkBoxCarving.Text = "Резьба";
            this.checkBoxCarving.UseVisualStyleBackColor = true;
            // 
            // comboBoxStandardLen
            // 
            this.comboBoxStandardLen.FormattingEnabled = true;
            this.comboBoxStandardLen.Location = new System.Drawing.Point(110, 153);
            this.comboBoxStandardLen.Name = "comboBoxStandardLen";
            this.comboBoxStandardLen.Size = new System.Drawing.Size(186, 21);
            this.comboBoxStandardLen.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Длина, м";
            // 
            // comboBoxStrength
            // 
            this.comboBoxStrength.FormattingEnabled = true;
            this.comboBoxStrength.Location = new System.Drawing.Point(110, 126);
            this.comboBoxStrength.Name = "comboBoxStrength";
            this.comboBoxStrength.Size = new System.Drawing.Size(186, 21);
            this.comboBoxStrength.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Группа прочности";
            // 
            // comboBoxTypeDiametr
            // 
            this.comboBoxTypeDiametr.FormattingEnabled = true;
            this.comboBoxTypeDiametr.Location = new System.Drawing.Point(110, 98);
            this.comboBoxTypeDiametr.Name = "comboBoxTypeDiametr";
            this.comboBoxTypeDiametr.Size = new System.Drawing.Size(186, 21);
            this.comboBoxTypeDiametr.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Типоразмер";
            // 
            // dateTimeTagInstall
            // 
            this.dateTimeTagInstall.Location = new System.Drawing.Point(134, 71);
            this.dateTimeTagInstall.Name = "dateTimeTagInstall";
            this.dateTimeTagInstall.Size = new System.Drawing.Size(162, 20);
            this.dateTimeTagInstall.TabIndex = 24;
            // 
            // dateTimeReleaseDate
            // 
            this.dateTimeReleaseDate.Location = new System.Drawing.Point(134, 45);
            this.dateTimeReleaseDate.Name = "dateTimeReleaseDate";
            this.dateTimeReleaseDate.Size = new System.Drawing.Size(162, 20);
            this.dateTimeReleaseDate.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Дата установки метки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Дата выпуска трубы";
            // 
            // textBoxOTK
            // 
            this.textBoxOTK.Location = new System.Drawing.Point(42, 19);
            this.textBoxOTK.Name = "textBoxOTK";
            this.textBoxOTK.Size = new System.Drawing.Size(254, 20);
            this.textBoxOTK.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "ОТК";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.butSecondInf);
            this.groupBox4.Controls.Add(this.textBoxCertificate);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.textBoxPackageNum);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.textBoxGostThCon);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.textBoxSmeltingNum);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.textBoxBatchNum);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.textBoxFactoryNum);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.buttCertificateOpen);
            this.groupBox4.Location = new System.Drawing.Point(3, 65);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(655, 161);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Основная информация";
            // 
            // butSecondInf
            // 
            this.butSecondInf.Location = new System.Drawing.Point(540, 132);
            this.butSecondInf.Name = "butSecondInf";
            this.butSecondInf.Size = new System.Drawing.Size(109, 23);
            this.butSecondInf.TabIndex = 19;
            this.butSecondInf.Text = "Подробнее";
            this.butSecondInf.UseVisualStyleBackColor = true;
            this.butSecondInf.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // textBoxCertificate
            // 
            this.textBoxCertificate.Location = new System.Drawing.Point(81, 71);
            this.textBoxCertificate.Name = "textBoxCertificate";
            this.textBoxCertificate.Size = new System.Drawing.Size(215, 20);
            this.textBoxCertificate.TabIndex = 18;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, 74);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(68, 13);
            this.label25.TabIndex = 17;
            this.label25.Text = "Сертификат";
            // 
            // textBoxPackageNum
            // 
            this.textBoxPackageNum.Location = new System.Drawing.Point(387, 70);
            this.textBoxPackageNum.Name = "textBoxPackageNum";
            this.textBoxPackageNum.Size = new System.Drawing.Size(262, 20);
            this.textBoxPackageNum.TabIndex = 15;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(325, 73);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 13);
            this.label24.TabIndex = 14;
            this.label24.Text = "№ пакета";
            // 
            // textBoxGostThCon
            // 
            this.textBoxGostThCon.Location = new System.Drawing.Point(81, 43);
            this.textBoxGostThCon.Name = "textBoxGostThCon";
            this.textBoxGostThCon.Size = new System.Drawing.Size(215, 20);
            this.textBoxGostThCon.TabIndex = 13;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 46);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 13);
            this.label23.TabIndex = 12;
            this.label23.Text = "Гост/ТУ";
            // 
            // textBoxSmeltingNum
            // 
            this.textBoxSmeltingNum.Location = new System.Drawing.Point(387, 44);
            this.textBoxSmeltingNum.Name = "textBoxSmeltingNum";
            this.textBoxSmeltingNum.Size = new System.Drawing.Size(262, 20);
            this.textBoxSmeltingNum.TabIndex = 11;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(325, 47);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 13);
            this.label22.TabIndex = 10;
            this.label22.Text = "№ плавки";
            // 
            // textBoxBatchNum
            // 
            this.textBoxBatchNum.Location = new System.Drawing.Point(387, 17);
            this.textBoxBatchNum.Name = "textBoxBatchNum";
            this.textBoxBatchNum.Size = new System.Drawing.Size(262, 20);
            this.textBoxBatchNum.TabIndex = 9;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(325, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "№ партии";
            // 
            // textBoxFactoryNum
            // 
            this.textBoxFactoryNum.Location = new System.Drawing.Point(122, 17);
            this.textBoxFactoryNum.Name = "textBoxFactoryNum";
            this.textBoxFactoryNum.Size = new System.Drawing.Size(174, 20);
            this.textBoxFactoryNum.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(109, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "Заводской № трубы";
            // 
            // buttCertificateOpen
            // 
            this.buttCertificateOpen.Location = new System.Drawing.Point(328, 132);
            this.buttCertificateOpen.Name = "buttCertificateOpen";
            this.buttCertificateOpen.Size = new System.Drawing.Size(75, 23);
            this.buttCertificateOpen.TabIndex = 16;
            this.buttCertificateOpen.Text = "тест";
            this.buttCertificateOpen.UseVisualStyleBackColor = true;
            this.buttCertificateOpen.Click += new System.EventHandler(this.buttCertificateOpen_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.textBoxPipeId);
            this.groupBox12.Controls.Add(this.label19);
            this.groupBox12.Controls.Add(this.textBoxTagId);
            this.groupBox12.Controls.Add(this.label18);
            this.groupBox12.Controls.Add(this.button2);
            this.groupBox12.Location = new System.Drawing.Point(3, 3);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(655, 56);
            this.groupBox12.TabIndex = 3;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Поиск метки";
            // 
            // textBoxPipeId
            // 
            this.textBoxPipeId.Location = new System.Drawing.Point(286, 19);
            this.textBoxPipeId.Name = "textBoxPipeId";
            this.textBoxPipeId.Size = new System.Drawing.Size(228, 20);
            this.textBoxPipeId.TabIndex = 7;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(238, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(42, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Pipe id:";
            // 
            // textBoxTagId
            // 
            this.textBoxTagId.Location = new System.Drawing.Point(52, 19);
            this.textBoxTagId.Name = "textBoxTagId";
            this.textBoxTagId.Size = new System.Drawing.Size(174, 20);
            this.textBoxTagId.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Tag id:";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(565, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Считывать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // StatusBar1
            // 
            this.StatusBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.StatusBar1.Location = new System.Drawing.Point(0, 443);
            this.StatusBar1.Name = "StatusBar1";
            this.StatusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.TStatusPanel,
            this.Port,
            this.Manufacturername});
            this.StatusBar1.ShowPanels = true;
            this.StatusBar1.Size = new System.Drawing.Size(669, 22);
            this.StatusBar1.SizingGrip = false;
            this.StatusBar1.TabIndex = 26;
            this.StatusBar1.Text = "StatusBar1";
            // 
            // TStatusPanel
            // 
            this.TStatusPanel.Name = "TStatusPanel";
            this.TStatusPanel.Width = 740;
            // 
            // Port
            // 
            this.Port.MinWidth = 66;
            this.Port.Name = "Port";
            this.Port.Text = "Port:";
            // 
            // Manufacturername
            // 
            this.Manufacturername.Name = "Manufacturername";
            this.Manufacturername.Text = "statusManufacturer nameBarPanel1";
            // 
            // Timer_Test_
            // 
            this.Timer_Test_.Tick += new System.EventHandler(this.Timer_Test__Tick);
            // 
            // Timer_G2_Read
            // 
            this.Timer_G2_Read.Interval = 200;
            this.Timer_G2_Read.Tick += new System.EventHandler(this.Timer_G2_Read_Tick);
            // 
            // Timer_G2_Alarm
            // 
            this.Timer_G2_Alarm.Tick += new System.EventHandler(this.Timer_G2_Alarm_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(669, 465);
            this.Controls.Add(this.StatusBar1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "UHFReader09CSHarp V1.5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.TabSheet_CMD.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.TabSheet_EPCC1G2.ResumeLayout(false);
            this.gpSecondInf.ResumeLayout(false);
            this.gpSecondInf.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TStatusPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manufacturername)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabSheet_CMD;
        internal System.Windows.Forms.StatusBar StatusBar1;
        private System.Windows.Forms.StatusBarPanel TStatusPanel;
        private System.Windows.Forms.StatusBarPanel Port;
        private System.Windows.Forms.StatusBarPanel Manufacturername;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button ClosePort;
        internal System.Windows.Forms.Button OpenPort;
        internal System.Windows.Forms.ComboBox ComboBox_COM;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TabPage TabSheet_EPCC1G2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBox_AlreadyOpenCOM;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox ComboBox_dmaxfre;
        private System.Windows.Forms.ComboBox ComboBox_scantime;
        private System.Windows.Forms.ComboBox ComboBox_baud;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer Timer_Test_;
        private System.Windows.Forms.Timer Timer_G2_Read;
        private System.Windows.Forms.Timer Timer_G2_Alarm;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer Timer_Test_6B;
        private System.Windows.Forms.Timer Timer_6B_Read;
        private System.Windows.Forms.Timer Timer_6B_Write;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox ComboBox_baud2;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox textBoxTagId;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxPipeId;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button buttCertificateOpen;
        private System.Windows.Forms.TextBox textBoxPackageNum;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxGostThCon;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxSmeltingNum;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxBatchNum;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxFactoryNum;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBoxCertificate;
        private System.Windows.Forms.GroupBox gpSecondInf;
        private System.Windows.Forms.Button butSecondInf;
        private System.Windows.Forms.LinkLabel linkLabelBetwHipple;
        private System.Windows.Forms.LinkLabel linkLabelIntercoating;
        private System.Windows.Forms.LinkLabel linkLabelCoupling;
        private System.Windows.Forms.CheckBox checkBoxIsOuterCoating;
        private System.Windows.Forms.CheckBox checkBoxIsBetwHipple;
        private System.Windows.Forms.CheckBox checkBoxIntercoating;
        private System.Windows.Forms.CheckBox checkBoxIsCoupling;
        private System.Windows.Forms.CheckBox checkBoxCarving;
        private System.Windows.Forms.ComboBox comboBoxStandardLen;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxStrength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxTypeDiametr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimeTagInstall;
        private System.Windows.Forms.DateTimePicker dateTimeReleaseDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOTK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboBox_dminfre;
        private System.Windows.Forms.Label label14;
    }
}

