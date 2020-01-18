﻿using System;
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
              ComboBox_COM.Items.Add(" AUTO");
              for (i = 1; i < 13;i++ )
                  ComboBox_COM.Items.Add(" COM" + Convert.ToString(i));
              ComboBox_COM.SelectedIndex = 0;
              RefreshStatus();
        }
        private void InitReaderList()
        {
            int i=0;
           // ComboBox_PowerDbm.SelectedIndex = 0;
            ComboBox_baud.SelectedIndex =3;
             for (i=0 ;i< 63;i++)
             {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6+i*0.4)+" MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
             }
             ComboBox_dmaxfre.SelectedIndex = 62;
              ComboBox_dminfre.SelectedIndex = 0;
              for (i=0x03;i<=0xff;i++)
                  ComboBox_scantime.Items.Add(Convert.ToString(i) + "*100ms");
              ComboBox_scantime.SelectedIndex = 7;
              i=40;
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
              fOpenComIndex = -1;
              fComAdr = 0;
              ferrorcode= -1;
              fBaud =5;
             InitComList();
              InitReaderList();
             
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
               ComboBox_baud2.SelectedIndex = 3;

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
                      fBaud = Convert.ToByte(ComboBox_baud2.SelectedIndex);
                      if (fBaud>2)
                      {
                          fBaud = Convert.ToByte(fBaud + 2);
                      }
                    openresult =StaticClassReaderB.AutoOpenComPort(ref port,ref fComAdr,fBaud,ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    if (openresult == 0 )
                    {
                        ComOpen = true;
                        if (fBaud > 3)
                        {
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                        }
                        else
                        {
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                        }
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
                            if (fBaud > 3)
                            {
                                ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                            }
                            else
                            {
                                ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                            }
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
                 ComboBox_scantime.SelectedIndex = ScanTime - 3;

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
                  ComboBox_dminfre.SelectedIndex = dminfre & 0x3F;
                  ComboBox_dmaxfre.SelectedIndex = dmaxfre & 0x3F;

              }
              AddCmdLog("GetReaderInformation","GetReaderInformation", fCmdRet);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
              byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime, band=0;
              string returninfo="";
              string returninfoDlg="";
              string setinfo;
              band = 0;
              progressBar1.Visible = true;
              progressBar1.Minimum = 0;
              dminfre = Convert.ToByte(((band & 3) << 6) | (ComboBox_dminfre.SelectedIndex & 0x3F));
              dmaxfre = Convert.ToByte(((band & 0x0c) << 4) | (ComboBox_dmaxfre.SelectedIndex & 0x3F));
                  aNewComAdr = Convert.ToByte("00");
                  powerDbm = Convert.ToByte(13);
                  fBaud = Convert.ToByte(ComboBox_baud.SelectedIndex);
                  if (fBaud > 2)
                      fBaud = Convert.ToByte(fBaud + 2);
                  scantime = Convert.ToByte(ComboBox_scantime.SelectedIndex + 3);
                  setinfo = "Write";
              progressBar1.Value =10;     
              fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr,ref aNewComAdr,frmcomportindex);
              if (fCmdRet==0x13)
              fComAdr = aNewComAdr;
              if (fCmdRet == 0)
              {
                fComAdr = aNewComAdr;
                returninfo=returninfo+setinfo+"Address Successfully";
              }
              else if (fCmdRet==0xEE )
              returninfo=returninfo+setinfo+"Address Response Command Error";
              else
              {
              returninfo=returninfo+setinfo+"Address Fail";
              returninfoDlg=returninfoDlg+setinfo+"Address Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

              progressBar1.Value =25; 
              fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr,powerDbm,frmcomportindex);
              if (fCmdRet == 0)
               returninfo=returninfo+",Power Success";
              else if (fCmdRet==0xEE )
              returninfo=returninfo+",Power Response Command Error";
              else
              {
                  returninfo=returninfo+",Power Fail";
                  returninfoDlg=returninfoDlg+" "+setinfo+"Power Fail Command Response=0x"
                       +Convert.ToString(fCmdRet)+"("+GetReturnCodeDesc(fCmdRet)+")";
              }
              
              progressBar1.Value =40; 
              fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr,ref dmaxfre,ref dminfre,frmcomportindex);
              if (fCmdRet == 0 )
               returninfo=returninfo+",Frequency Success";
              else if (fCmdRet==0xEE)
              returninfo=returninfo+",Frequency Response Command Error";
              else
              {
              returninfo =returninfo+",Frequency Fail";
              returninfoDlg=returninfoDlg+" "+setinfo+"Frequency Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

                    progressBar1.Value =55; 
                  fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr,ref fBaud,frmcomportindex);
                  if (fCmdRet == 0)
                   returninfo=returninfo+",Baud Rate Success";
                  else if (fCmdRet==0xEE)
                  returninfo=returninfo+",Baud Rate Response Command Error";
                  else
                  {
                  returninfo=returninfo+",Baud Rate Fail";
                  returninfoDlg=returninfoDlg+" "+setinfo+"Baud Rate Fail Command Response=0x"
                       + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
                  }

             progressBar1.Value =70; 
              fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr,ref scantime,frmcomportindex);
              if (fCmdRet == 0 )
               returninfo=returninfo+",InventoryScanTime Success";
             else if (fCmdRet==0xEE)
              returninfo=returninfo+",InventoryScanTime Response Command Error";
              else
              {
              returninfo=returninfo+",InventoryScanTime Fail";
              returninfoDlg=returninfoDlg+" "+setinfo+"InventoryScanTime Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
             }

              progressBar1.Value =100; 
              Button3_Click(sender,e);
              progressBar1.Visible=false;
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
              if  (returninfoDlg!="")
                 MessageBox.Show(returninfoDlg, "Information");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime;
            string returninfo = "";
            string returninfoDlg = "";
            string setinfo;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
             dminfre = 0;
            dmaxfre = 62;
            aNewComAdr =0x00;
            powerDbm = 13;
            fBaud=5;
            scantime=10;
            setinfo=" Recovery ";
            ComboBox_baud.SelectedIndex = 3;
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr, ref aNewComAdr, frmcomportindex);
            if (fCmdRet == 0x13)
                fComAdr = aNewComAdr;
            if (fCmdRet == 0)
            {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "Address Successfully";
            }
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + setinfo + "Address Response Command Error";
            else
            {
                returninfo = returninfo + setinfo + "Address Fail";
                returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 25;
            fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr, powerDbm, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Power Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Power Response Command Error";
            else
            {
                returninfo = returninfo + ",Power Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 40;
            fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr, ref dmaxfre, ref dminfre, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Frequency Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Frequency Response Command Error";
            else
            {
                returninfo = returninfo + ",Frequency Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 55;
            fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr, ref fBaud, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Baud Rate Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Baud Rate Response Command Error";
            else
            {
                returninfo = returninfo + ",Baud Rate Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 70;
            fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr, ref scantime, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",InventoryScanTime Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",InventoryScanTime Response Command Error";
            else
            {
                returninfo = returninfo + ",InventoryScanTime Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 100;
            Button3_Click(sender, e);
            progressBar1.Visible = false;
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
            if (returninfoDlg != "")
                MessageBox.Show(returninfoDlg, "Information");
            
        }


        private void ComboBox_dfreSelect(object sender, EventArgs e)
        {
        if  (ComboBox_dminfre.SelectedIndex> ComboBox_dmaxfre.SelectedIndex )
             {
                 ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
                MessageBox.Show("Min.Frequency is equal or lesser than Max.Frequency", "Error Information");
              }
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

        private void Edit_CmdComAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }
     
        private void radioButton_band1_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 63; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 62;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band2_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 20; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 19;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band3_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 50; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 49;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band4_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 32; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 31;
            ComboBox_dminfre.SelectedIndex = 0;
        }
 
        private void ComboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_baud2.Items.Clear();
            if(ComboBox_COM.SelectedIndex==0)
           { 
              ComboBox_baud2.Items.Add("9600bps");
              ComboBox_baud2.Items.Add("19200bps");
              ComboBox_baud2.Items.Add("38400bps");
              ComboBox_baud2.Items.Add("57600bps");
              ComboBox_baud2.Items.Add("115200bps");
              ComboBox_baud2.SelectedIndex = 3;
            }
            else
            {
              ComboBox_baud2.Items.Add("Auto");
              ComboBox_baud2.SelectedIndex = 0;
            }
        }

        private void radioButton_band5_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 15; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 14;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void sqlFieldFill(string tagNum)
        {

            //====================



            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(" select t.tagId, pip.pipeNum, pip.factoryNum, pip.batchNum, pip.smeltingNum, pip.gostThCon, pip.packageNum, pip.releaseDate, pip.[certificate], pip.otk,"
                             + " year(pip.releaseDate) yeReleasDate, month(pip.releaseDate) monReleasDate, day(pip.releaseDate) dayReleasDate,"
                             + " year(t.dateInstall) yeDateInstall, month(t.dateInstall) monDateInstall, day(t.dateInstall) dayDateInstall,"
                             + " pipeTyp.pipeTypeId as typeDiametr, stren.strengthId, standartLe.standardLenId,"
                             + " pip.carving, isnull(coup.couplingId,0) as isCoupling, pip.outerCoating as isOuterCoating, isnull(intercoat.intercoatingId,0) as isIntercoating, isnull( betw.betwHippleId,0) as isBetwHipple"
                             + " from pipe pip"
                             + " inner join strength stren on  stren.strengthId = pip.strengthId"
                             + " inner join standardLen standartLe on  standartLe.standardLenId = pip.standardLenId"
                             + " inner join pipeType pipeTyp on  pipeTyp.pipeTypeId = pip.pipeTypeId"
                             + " left join coupling coup on  coup.couplingId = pip.couplingId"
                             + " inner join tag t on  t.tagId = pip.tagId"
                             + " left join intercoating intercoat on intercoat.intercoatingId = pip.intercoatingId"
                             + " left join betwHipple betw on betw.betwHippleId = pip.betwHippleId"
                             + " where t.tagNum = '" 
                             + tagNum + "'"
                            , myConnection);
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

                int pointer = 1;
                pointer = documentTableFill(" select 'Труба' as Documenttype, docPip.name, docPip.addedDate, docPip.patch  "
                                          + " from pipe pip"
                                          + " inner join document docPip on docPip.objectId = pip.pipeId and docPip.type = 1 --pipe"
                                          + " where pip.tagId = " + tagId, pointer, dataGridViewDocument);
                pointer = documentTableFill(" select 'Внутреннее покрытие' as Documenttype, docInter.name, docInter.addedDate, docInter.patch"
                                          + " from pipe pip"
                                          + " inner join document docInter on docInter.objectId = pip.intercoatingId and docInter.type = 2 --intercoating"
                                          + " where pip.tagId = " + tagId, pointer, dataGridViewDocument);
                pointer = 1;
                pointer = documentTableFill(" select lc.lifeCycleId, lc.lifeCycleDateAdded,  toper.value, fi.brief, emp.surname + ' ' + LEFT(emp.name,1) + '. ' + LEFT(emp.patronymic,1) + '.', lc.geoposition "
                                          + " from lifeCycle lc "
                                          + " inner join typicalOperation toper on toper.typicalOperationId = lc.typicalOperationId "
                                          + " inner join employee emp on emp.employeeId = lc.employeeId "
                                          + " inner join firm fi on fi.firmId = lc.firmId "
                                          + " where lc.tagId = " + tagId + " order by lifeCycleDateAdded asc", pointer, dataGridLifeCicle);

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
        private int documentTableFill(string query, int pointer, DataGridView dataGridViewDocument)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myReader = myCommand.ExecuteReader();         

            while (myReader.Read())
            {
                dataGridViewDocument.Rows.Add();
                dataGridViewDocument.Rows[pointer - 1].Cells[0].Value = pointer;
                for (int i = 0; i < dataGridViewDocument.ColumnCount - 1; ++i)
                {
                    dataGridViewDocument.Rows[pointer - 1].Cells[i + 1].Value = myReader[i].ToString();
                }
                ++pointer;
            }
            myReader.Close();
            return pointer;
        }
        private void button4_Click(object sender, EventArgs e)
        {


            //====================

            SqlConnection myConnection = new SqlConnection("server=ALEXPC\\SQLEXPRESS;" +
                           "Trusted_Connection=yes;" +
                           "database=ObjectLifeCycle; " +
                           "connection timeout=30");
            try
            {
                myConnection.Open();
                MessageBox.Show("OKK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                //MessageBox.Show(ex.ToString());
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from testTable",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    Console.WriteLine(myReader["Column1"].ToString());
                    Console.WriteLine(myReader["Column2"].ToString());
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

        private void buttCertificateOpen_Click(object sender, EventArgs e)
        {


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

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

            SqlCommand myCommand = new SqlCommand(" select stren.name, diam.pipeDiameterVal, coup.batchNum, coup.smeltingNum, prot.brief"
                                                + " from pipe pip"
                                                + " inner join coupling coup on coup.couplingId = pip.couplingId"
                                                + " inner join protection prot on prot.protectionId = coup.protectionId"
                                                + " inner join pipeDiameter diam on diam.pipeDiameterId = coup.pipeDiameterId"
                                                + " inner join strength stren on stren.strengthId = coup.strengthId"
                                                + " where pip.tagId = " + tagId, myConnection);
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

            SqlCommand myCommand = new SqlCommand(" select f.brief, f.name, ic.techCase, ic.thickness, ic.color"
                                                + " from pipe pip"
                                                + " inner join intercoating ic on ic.intercoatingId = pip.intercoatingId"
                                                + " inner join firm f on f.firmId = ic.firmId"
                                                + " where pip.tagId = " + tagId, myConnection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                tb.Text += "Производитель: " + myReader[0].ToString() + " " + myReader[1].ToString() + "\r\n";
                tb.Text += "Техническое условие: " + myReader[2].ToString() + "\r\n";
                tb.Text += "Толщина, мкм: " + myReader[3].ToString() + "\r\n";
                tb.Text += "Цвет: " + myReader[4].ToString() + "\r\n";
            }
            myReader.Close();

            myCommand = new SqlCommand(" select co.caseObjectValue, ct.caseTypeValue, cr.caseResultValue"
                                    + " from pipe pip"
                                    + " inner join intercoating ic on ic.intercoatingId = pip.intercoatingId"
                                    + " inner join intercoatingCaseRelation icr on icr.intercoatingId = ic.intercoatingId"
                                    + " inner join caseObject co on co.caseObjectId = icr.caseObjectId"
                                    + " inner join caseType ct on ct.caseTypeId = icr.caseTypeId"
                                    + " inner join caseResult cr on cr.caseResultId = icr.caseResultId"
                                    + " where pip.tagId = " + tagId, myConnection);
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

            SqlCommand myCommand = new SqlCommand(" select betw.brief, betw.discript"
                                                + " from pipe pip"
                                                + " inner join betwHipple betw on betw.betwHippleId = pip.betwHippleId"
                                                + " where pip.tagId = " + tagId, myConnection);
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
    }
}