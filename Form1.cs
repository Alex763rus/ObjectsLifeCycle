using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Reflection;
using ReaderB;
using System.IO.Ports;
using System.IO;
using System.Data.SqlClient;

namespace UHFReader09demomain
{
    public partial class Form1 : Form
    {
        private bool fAppClosed; 
        private byte fComAdr=0xff;
        private int ferrorcode;
        private byte fBaud;
        private double fdminfre;
        private double fdmaxfre;
        private int fCmdRet=30;
        private int fOpenComIndex;
        private bool fIsInventoryScan;
        private byte[] fOperEPC=new byte[36];
        private byte[] fPassWord=new byte[4];
        private byte[] fOperID_6B=new byte[8];
        ArrayList list = new ArrayList();
        private string fInventory_EPC_List;
        private int frmcomportindex;
        private bool ComOpen=false;


        SqlConnection myConnection;
        private int tagId;
        private int access;
        private int employeeId;

        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
            //autorization();
        }
        private void RefreshStatus()
        { 
              if(!(ComboBox_AlreadyOpenCOM.Items.Count != 0)) 
                StatusBar1.Panels[1].Text = "COM Closed";
              else
                StatusBar1.Panels[1].Text = " COM" + Convert.ToString(frmcomportindex);
              StatusBar1.Panels[0].Text ="";
              StatusBar1.Panels[2].Text ="";
        }
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Operation Successed";
                case 0x01:
                    return "Return before Inventory finished";
                case 0x02:
                    return "the Inventory-scan-time overflow";
                case 0x03:
                    return "More Data";
                case 0x04:
                    return "Reader module MCU is Full";
                case 0x05:
                    return "Access Password Error";
                case 0x09:
                    return "Destroy Password Error";
                case 0x0a:
                    return "Destroy Password Error Cannot be Zero";
                case 0x0b:
                    return "Tag Not Support the command";
                case 0x0c:
                    return "Use the commmand,Access Password Cannot be Zero";
                case 0x0d:
                    return "Tag is protected,cannot set it again";
                case 0x0e:
                    return "Tag is unprotected,no need to reset it";
                case 0x10:
                    return "There is some locked bytes,write fail";
                case 0x11:
                    return "can not lock it";
                case 0x12:
                    return "is locked,cannot lock it again";
                case 0x13:
                    return "Parameter Save Fail,Can Use Before Power";
                case 0x14:
                    return "Cannot adjust";
                case 0x15:
                    return "Return before Inventory finished";
                case 0x16:
                    return "Inventory-Scan-Time overflow";
                case 0x17:
                    return "More Data";
                case 0x18:
                    return "Reader module MCU is full";
                case 0x19:
                    return "Not Support Command Or AccessPassword Cannot be Zero";
                case 0xFA:
                    return "Get Tag,Poor Communication,Inoperable";
                case 0xFB:
                    return "No Tag Operable";
                case 0xFC:
                    return "Tag Return ErrorCode";
                case 0xFD:
                    return "Command length wrong";
                case 0xFE:
                    return "Illegal command";
                case 0xFF:
                    return "Parameter Error";
                case 0x30:
                    return "Communication error";
                case 0x31:
                    return "CRC checksummat error";
                case 0x32:
                    return "Return data length error";
                case 0x33:
                    return "Communication busy";
                case 0x34:
                    return "Busy,command is being executed";
                case 0x35:
                    return "ComPort Opened";
                case 0x36:
                    return "ComPort Closed";
                case 0x37:
                    return "Invalid Handle";
                case 0x38:
                    return "Invalid Port";
                case 0xEE:
                    return "Return command error";
                default:
                    return "";
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Other error";
                case 0x03:
                    return "Memory out or pc not support";
                case 0x04:
                    return "Memory Locked and unwritable";
                case 0x0b:
                    return "No Power,memory write operation cannot be executed";
                case 0x0f:
                    return "Not Special Error,tag not support special errorcode";
                default:
                    return "";
            }
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }
        private void AddCmdLog(string CMD, string cmdStr, int cmdRet)
        {
            try
            {
                StatusBar1.Panels[0].Text = "";
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " " +
                                            cmdStr + ": " +
                                            GetReturnCodeDesc(cmdRet);
            }
            finally
            {
                ;
            }
        }
        
        private void InitComList()
        {
            int i = 0;
            ComboBox_COM.Items.Clear();
              ComboBox_COM.Items.Add(" АВТО");
              for (i = 1; i < 13;i++ )
                  ComboBox_COM.Items.Add(" COM" + Convert.ToString(i));
              ComboBox_COM.SelectedIndex = 0;
              RefreshStatus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
              fOpenComIndex = -1;
              fComAdr = 0;
              ferrorcode= -1;
              fBaud =5;
             InitComList();
               
              fAppClosed = false;
              fIsInventoryScan = false;
              Timer_Test_.Enabled = false;
              Timer_G2_Read.Enabled = false;
              Timer_G2_Alarm.Enabled = false;
              timer1.Enabled = false;

               button2.Enabled = false;

              gpSecondInf.Visible = false;
              tagId = 0;
              this.Size = new System.Drawing.Size(685, 316);
             // tabControl1.Size = new System.Drawing.Size(669, 267);

            linkLabelCoupling.Visible = false;
            linkLabelIntercoating.Visible = false;
            linkLabelBetwHipple.Visible = false;

            //открыть соединение
            try
            {
                myConnection = new SqlConnection("server=ALEXPC\\SQLEXPRESS;" + "Trusted_Connection=yes;" + "database=ObjectsLifeCycle; " + "connection timeout=30");
                myConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка при соединении с базой");
                return;
            }

            fillComboBoxFromSql("select pipeTypeVal from pipeType", comboBoxTypeDiametr);
            fillComboBoxFromSql("select name from strength", comboBoxStrength);
            fillComboBoxFromSql("select lengthVal from standardLen", comboBoxStandardLen);

            access = 1;
            employeeId = 1;
            this.Visible = false;
        }
        private void fillComboBoxFromSql(string query, ComboBox comboBox)
        {
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            SqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                comboBox.Items.Add(myReader[0].ToString());
            }
            myReader.Close();
        }

        private void OpenPort_Click(object sender, EventArgs e)
        {
            int port=0;
            int openresult,i;
            openresult = 30;
            string temp;
            Cursor = Cursors.WaitCursor;
              fComAdr = Convert.ToByte("FF",16); // $FF;
              try
              {
                  if (ComboBox_COM.SelectedIndex == 0)//Auto
                  {
                    openresult =StaticClassReaderB.AutoOpenComPort(ref port,ref fComAdr,5,ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    if (openresult == 0 )
                    {
                        ComOpen = true;

                        Button3_Click(sender, e); //自动执行读取写卡器信息
                      if ((fCmdRet==0x35) |(fCmdRet==0x30))
                        {
                            MessageBox.Show ("Serial Communication Error or Occupied", "Information");
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                            ComOpen = false;
                        }
                    }          
                  }
                  else
                  {
                    temp = ComboBox_COM.SelectedItem.ToString();
                    temp = temp.Trim();
                    port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                    for (i = 6; i >= 0; i--)
                    {
                        fBaud = Convert.ToByte(i);
                        if (fBaud == 3)
                            continue;
                        openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                        fOpenComIndex = frmcomportindex;
                        if (openresult == 0x35)
                        {
                            MessageBox.Show("COM Opened", "Information");
                            return;
                        }
                        if (openresult == 0)
                        {
                            ComOpen = true;
                            Button3_Click(sender, e); //自动执行读取写卡器信息
                            if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                            {
                                ComOpen = false;
                                MessageBox.Show("Serial Communication Error or Occupied", "Information");
                                StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                                return;
                            }
                            RefreshStatus();
                            break;
                        }

                    }
                  }
              }
              finally
              {
                  Cursor = Cursors.Default;
              }

              if ((fOpenComIndex != -1) &(openresult != 0X35)  &(openresult != 0X30))
              {
                ComboBox_AlreadyOpenCOM.Items.Add("COM"+Convert.ToString(fOpenComIndex)) ;
                ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
                button2.Enabled = true;
            
                ComOpen = true;
              }
              if ((fOpenComIndex == -1) &&(openresult == 0x30)) 
                MessageBox.Show("Serial Communication Error", "Information");

            if ((ComboBox_AlreadyOpenCOM.Items.Count != 0)&(fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30)&(fCmdRet==0)) 
              {
                temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
              }
              RefreshStatus();
          }

        private void ClosePort_Click(object sender, EventArgs e)
        {
            int port;
            string temp;
            ComboBox_AlreadyOpenCOM.Refresh();
            RefreshStatus();
            try
              {
                if (ComboBox_AlreadyOpenCOM.SelectedIndex  < 0 )
                {
                    MessageBox.Show("Please Choose COM Port to close", "Information");
                }
                else
                {
                    temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                  port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                  fCmdRet = StaticClassReaderB.CloseSpecComPort(port);
                     if (fCmdRet == 0)
                  {
                    ComboBox_AlreadyOpenCOM.Items.RemoveAt(0);
                    if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
                    {
                        temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                         port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                         StaticClassReaderB.CloseSpecComPort(port);
                        fComAdr = 0xFF;
                        StaticClassReaderB.OpenComPort(port,ref fComAdr, fBaud,ref frmcomportindex);
                        fOpenComIndex = frmcomportindex;
                        RefreshStatus();
                       Button3_Click(sender,e); //自动执行读取写卡器信息
                    }
                   }               
                  else                
                    MessageBox.Show("Serial Communication Error", "Information");
                  }
              }
              finally
              {

              }
              if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
                ComboBox_AlreadyOpenCOM.SelectedIndex = 0;
              else
              {
                  fOpenComIndex = -1;
                  ComboBox_AlreadyOpenCOM.Items.Clear();
                  ComboBox_AlreadyOpenCOM.Refresh();
                  RefreshStatus();
                  button2.Enabled = false;

                  button2.Text = "Остановить";
                  ComOpen = false;            
                  timer1.Enabled = false;
              }
         }
        private void Button3_Click(object sender, EventArgs e)
        {
             byte[] TrType=new byte[2];
             byte[] VersionInfo=new byte[2];
             byte ReaderType=0;
             byte ScanTime=0;
             byte dmaxfre=0;
             byte dminfre = 0;
             byte powerdBm=0;
             byte FreBand = 0;
              fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime, frmcomportindex);
              if (fCmdRet == 0)
              {
                  FreBand= Convert.ToByte(((dmaxfre & 0xc0)>> 4)|(dminfre >> 6)) ;
                  switch (FreBand)
                  {
                      case 0:
                          {
                              fdminfre = 902.6 + (dminfre & 0x3F) * 0.4;
                              fdmaxfre = 902.6 + (dmaxfre & 0x3F) * 0.4;
                          }
                          break;
                      case 1:
                          {
                              fdminfre = 920.125 + (dminfre & 0x3F) * 0.25;
                              fdmaxfre = 920.125 + (dmaxfre & 0x3F) * 0.25;
                          }
                          break;
                      case 2:
                          {
                              fdminfre = 902.75 + (dminfre & 0x3F) * 0.5;
                              fdmaxfre = 902.75 + (dmaxfre & 0x3F) * 0.5;
                          }
                          break;
                      case 3:
                          {
                              fdminfre = 917.1 + (dminfre & 0x3F) * 0.2;
                              fdmaxfre = 917.1 + (dmaxfre & 0x3F) * 0.2;
                          }
                          break;
                      case 4:
                          {
                              fdminfre = 865.1 + (dminfre & 0x3F) * 0.2;
                              fdmaxfre = 865.1 + (dmaxfre & 0x3F) * 0.2;
                          }
                          break;
                  }


              }
              AddCmdLog("GetReaderInformation","GetReaderInformation", fCmdRet);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Timer_Test_.Enabled = !Timer_Test_.Enabled;
            Timer_Test_.Interval = 1000;
            if (!Timer_Test_.Enabled)
            {
                AddCmdLog("Inventory", "Exit Query", 0);
                button2.Text = "Считывать";
            }
            else
            {
                button2.Text = "Остановить";
            }
        }
        private void Inventory()
        {
              int CardNum=0;
              int Totallen = 0;
              byte[] EPC=new byte[5000];
              string temps;
            string tagNum;
              fIsInventoryScan = true;
              byte AdrTID = 0;
              byte LenTID = 0;
              byte TIDFlag = 0;
                  AdrTID = 0;
                  LenTID = 0;
                  TIDFlag = 0;
              ListViewItem aListItem = new ListViewItem();
              fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, frmcomportindex);
              if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))//代表已查找结束，
              {
                 byte[] daw = new byte[Totallen];
                 Array.Copy(EPC, daw, Totallen);               
                 temps = ByteArrayToHexString(daw);
                 fInventory_EPC_List = temps;            //存贮记录               
                 if (CardNum==0)
                 {
                     fIsInventoryScan = false;
                     return;
                 }
                tagNum = temps.Substring(2, daw[0] * 2);
                textBoxTagId.Text = tagNum;
                //-------------------------------------
                if (myConnection != null)
                {
                    sqlFieldFill(tagNum);
                }
                else
                {
                    MessageBox.Show("Отсутствует соединение с базой", "Ошибка", MessageBoxButtons.OK);
                }

                    //-------------------------------------

                }
            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }
        private void Timer_Test__Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            Inventory();
        }

        private void Timer_G2_Read_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            fIsInventoryScan = true;
                byte Num = 0;
                byte[] CardData=new  byte[320];

                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[Num*2];
                    Array.Copy(CardData, daw, Num * 2);
                    AddCmdLog("ReadData", "Read", fCmdRet);
                }
                if (ferrorcode != -1)
             {
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +
                   " 'Read' Response ErrorCode=0x" + Convert.ToString(ferrorcode, 2) +
                   "(" + GetErrorCodeDesc(ferrorcode) + ")";
                    ferrorcode=-1;
             }
             fIsInventoryScan = false;
              if (fAppClosed)
                    Close();
        }

        private void Timer_G2_Alarm_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            fIsInventoryScan = true;
             fCmdRet=StaticClassReaderB.CheckEASAlarm_G2(ref fComAdr,ref ferrorcode,frmcomportindex);
            if (fCmdRet==0)
            {
                 StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Check EAS Alarm'Command Response=0x00" +
                          "(EAS alarm detected)";
            }
            else
            {
              AddCmdLog("CheckEASAlarm_G2", "Check EAS Alarm", fCmdRet);
            }
            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_Test_.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            fAppClosed = true;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
                timer1.Enabled = false;

                Timer_G2_Alarm.Enabled = false;
                Timer_G2_Read.Enabled = false;
                Timer_Test_.Enabled = false;
                button2.Text = "Считывать";
                if (ComOpen)
                {
                    button2.Enabled = true;
                }
                if (ComOpen)
                {
                    button2.Enabled = true;
                }

                Timer_Test_6B.Enabled = false;
                Timer_6B_Read.Enabled = false;
                Timer_6B_Write.Enabled = false;
        }

        private void sqlFieldFill(string tagNum)
        {

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(" select * from v_mainInf where tagNum = '" + tagNum + "'", myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                tagId = Convert.ToInt32(myReader["tagId"]);

                textBoxPipeId.Text = (myReader["pipeNum"].ToString());
                textBoxFactoryNum.Text = (myReader["factoryNum"].ToString());
                textBoxBatchNum.Text = (myReader["batchNum"].ToString());
                textBoxSmeltingNum.Text = (myReader["smeltingNum"].ToString());
                textBoxGostThCon.Text = (myReader["gostThCon"].ToString());
                textBoxPackageNum.Text = (myReader["packageNum"].ToString());
                textBoxCertificate.Text = (myReader["certificate"].ToString());
                textBoxOTK.Text = (myReader["otk"].ToString());
                dateTimeReleaseDate.Value = new DateTime(Convert.ToInt32(myReader["yeReleasDate"]), Convert.ToInt32(myReader["monReleasDate"]), Convert.ToInt32(myReader["dayReleasDate"]));
                dateTimeTagInstall.Value = new DateTime(Convert.ToInt32(myReader["yeDateInstall"]), Convert.ToInt32(myReader["monDateInstall"]), Convert.ToInt32(myReader["dayDateInstall"]));

                comboBoxTypeDiametr.SelectedIndex = Convert.ToInt32(myReader["typeDiametr"]) - 1;
                comboBoxStrength.SelectedIndex = Convert.ToInt32(myReader["strengthId"]) - 1;
                comboBoxStandardLen.SelectedIndex = Convert.ToInt32(myReader["standardLenId"]) - 1;

                checkBoxCarving.Checked = Convert.ToInt32(myReader["carving"].ToString()) != 0;
                checkBoxIsCoupling.Checked = Convert.ToInt32(myReader["isCoupling"].ToString()) != 0;
                checkBoxIsOuterCoating.Checked = Convert.ToInt32(myReader["isOuterCoating"].ToString()) != 0;
                checkBoxIsBetwHipple.Checked = Convert.ToInt32(myReader["isBetwHipple"].ToString()) != 0;
                checkBoxIntercoating.Checked = Convert.ToInt32(myReader["isIntercoating"].ToString()) != 0;

                linkLabelCoupling.Visible = checkBoxIsCoupling.Checked;
                linkLabelIntercoating.Visible = checkBoxIntercoating.Checked;
                linkLabelBetwHipple.Visible = checkBoxIsBetwHipple.Checked;

                myReader.Close();

                dataGridWiewFill(" select * from v_Document where tagId = " + tagId + " order by num", dataGridViewDocument);
                dataGridWiewFill(" select * from v_LifeCicle where tagId = " + tagId + " order by dateAdded", dataGridLifeCicle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR2");
            }

            //===============================
            // SqlCommand myCommand = new SqlCommand("insert into testTable(a) select 2", myConnection);
            //myCommand.ExecuteNonQuery();
            /*
            try
            {
                myConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/

        }
        private void dataGridWiewFill(string query, DataGridView dataGridViewDocument)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myReader = myCommand.ExecuteReader();
            for (int row = 0; myReader.Read(); ++ row)
            {
                dataGridViewDocument.Rows.Add();
                for (int i = 0; i < dataGridViewDocument.ColumnCount; ++i)
                {
                    dataGridViewDocument.Rows[row].Cells[i].Value = myReader[i].ToString();
                }
            }
            myReader.Close();
        }
 
        private void button4_Click_1(object sender, EventArgs e) // Дополнительная информация
        {
            if (gpSecondInf.Visible)
            {
                gpSecondInf.Visible = false;
                butSecondInf.Text = "Подробнее";
                this.Size = new System.Drawing.Size(685, 316);
               // tabControl1.Size = new System.Drawing.Size(686, 283);
            }
            else
            {
                gpSecondInf.Visible = true;
                butSecondInf.Text = "Скрыть";
                this.Size = new System.Drawing.Size(685, 504);
               // tabControl1.Size = new System.Drawing.Size(686, 503);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //муфта подробнее
            const int size = 400;
            Form frm = new Form();
            RichTextBox tb = new RichTextBox();

            SqlCommand myCommand = new SqlCommand(" select * from v_CouplingDetail where tagId = " + tagId, myConnection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                tb.Text += "Группа прочности: "  + myReader[0].ToString() + "\r\n";
                tb.Text += "Диаметр, мм:"        + myReader[1].ToString() + "\r\n";
                tb.Text += "Номер партии: "      + myReader[2].ToString() + "\r\n";
                tb.Text += "Номер плавки: "      + myReader[3].ToString() + "\r\n";
                tb.Text += "Защита/упрочнение: " + myReader[4].ToString() + "\r\n";
            }
            myReader.Close();

            tb.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            frm.Controls.Add(tb);
            tb.Size = new Size(size, size);
            frm.Size = new Size(size, size);
            frm.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //покрытие внутреннее подробнее
            const int size = 400;
            Form frm = new Form();
            RichTextBox tb = new RichTextBox();

            SqlCommand myCommand = new SqlCommand(" select * from v_IntercoatingDetail where tagId = " + tagId, myConnection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                tb.Text += "Производитель: " + myReader[0].ToString() + " " + myReader[1].ToString() + "\r\n";
                tb.Text += "Техническое условие: " + myReader[2].ToString() + "\r\n";
                tb.Text += "Толщина, мкм: " + myReader[3].ToString() + "\r\n";
                tb.Text += "Цвет: " + myReader[4].ToString() + "\r\n";
            }
            myReader.Close();

            myCommand = new SqlCommand(" select * from v_caseObjectDeail where tagId = " + tagId, myConnection);
            myReader = myCommand.ExecuteReader();

            tb.Text += "\r\n" + "Характеристики: " + "\r\n";
            while (myReader.Read())
            {
                tb.Text += myReader[0].ToString() + " " + myReader[1].ToString() + " " + myReader[2].ToString() + "\r\n";
            }
            myReader.Close();

            tb.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            frm.Controls.Add(tb);
            tb.Size = new Size(size, size);
            frm.Size = new Size(size, size);
            frm.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //покрытие межниппельное подробнее
            const int h = 700;
            const int l = 400;
            Form frm = new Form();
            RichTextBox tb = new RichTextBox();

            SqlCommand myCommand = new SqlCommand(" select * from v_BetwHippleDeail where tagId = " + tagId, myConnection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                tb.Text += "Название: " + myReader[0].ToString() + "\r\n";
                tb.Text += "Описание: " + myReader[1].ToString() + "\r\n";
            }
            myReader.Close();

            tb.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            frm.Controls.Add(tb);
            tb.Size = new Size(h, l);
            frm.Size = new Size(h, l);
            frm.Show();
        }

        private void buttonDocOpen_Click(object sender, EventArgs e)
        {
            //документы -> открыть
            if (dataGridViewDocument.CurrentRow == null)
            {
                MessageBox.Show("Для открытия файла необходимо выделить соответствующую строку таблицы");
                return;
            }
            string patch = Convert.ToString(dataGridViewDocument.Rows[dataGridViewDocument.CurrentRow.Index].Cells["DocumentPath"].Value);
            string name = Convert.ToString(dataGridViewDocument.Rows[dataGridViewDocument.CurrentRow.Index].Cells["DocumentName"].Value);

            if (String.IsNullOrEmpty(patch) || String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Выделенная строка не содержит информации о файле");
                return;
            }
            Form frm = new Form();
            frm.WindowState = FormWindowState.Maximized;

            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);

            frm.Controls.Add(webBrowser); //new Point(oldbutton.Location.X, oldbutton.Location.Y + oldbutton.Height + 10);
            webBrowser.Navigate(patch + "\\"+ name);
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //жизненный цикл -> подробнее
            if (dataGridLifeCicle.CurrentRow == null)
            {
                MessageBox.Show("Для открытия файла необходимо выделить соответствующую строку таблицы");
                return;
            }
            string lifeCicleID = Convert.ToString(dataGridLifeCicle.Rows[dataGridLifeCicle.CurrentRow.Index].Cells["lifeCicleId"].Value);
            if (String.IsNullOrEmpty(lifeCicleID) || String.IsNullOrEmpty(lifeCicleID))
            {
                MessageBox.Show("Выделенная строка не содержит информации");
                return;
            }
  
            const int size = 400;
            Form frm = new Form();
            RichTextBox tb = new RichTextBox();

            SqlCommand myCommand = new SqlCommand("select Comment from lifeCycle where lifeCycleId = 1", myConnection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            myReader.Read();
            tb.Text += myReader[0].ToString() + "\r\n";

            myReader.Close();

            tb.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            frm.Controls.Add(tb);
            tb.Size = new Size(size, size);
            frm.Size = new Size(size, size);
            frm.Show();

        }

        private void autorization()
        {
            Form frm = new Form();
            Container components = new System.ComponentModel.Container();
            Panel panel1 = new System.Windows.Forms.Panel();
            Label label1 = new System.Windows.Forms.Label();
            Label label2 = new System.Windows.Forms.Label();
            TextBox textBox1 = new System.Windows.Forms.TextBox();
            ContextMenuStrip contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            TextBox textBox2 = new System.Windows.Forms.TextBox();
            Button button1 = new System.Windows.Forms.Button();
            Label label3 = new System.Windows.Forms.Label();
            //panel1.SuspendLayout();
          //  SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(12, 10);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(338, 159);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label1.Location = new System.Drawing.Point(3, 48);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(128, 20);
            label1.TabIndex = 0;
            label1.Text = "Введите логин:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label2.Location = new System.Drawing.Point(3, 79);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(139, 20);
            label2.TabIndex = 1;
            label2.Text = "Введите пароль:";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(148, 81);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new System.Drawing.Size(180, 20);
            textBox1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(148, 48);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(180, 20);
            textBox2.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(253, 123);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Войти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label3.Location = new System.Drawing.Point(83, 12);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(158, 20);
            label3.TabIndex = 5;
            label3.Text = "Добро пожаловать!";
            // 
            // Form1
            // 
          //  AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         //   AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         //   ClientSize = new System.Drawing.Size(364, 181);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            frm.MaximumSize = new System.Drawing.Size(380, 220);
            frm.MinimumSize = new System.Drawing.Size(380, 220);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            frm.Controls.Add(panel1);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
              InitializeComponent();
        }
    }
 }