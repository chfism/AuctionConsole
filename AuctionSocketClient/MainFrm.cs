using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections;
using log4net;
using System.Web;
using AuctionSocketClient.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuctionSocketClient
{
    public partial class MainFrm : Form
    {
        private bool debug = false;
        private string host = "";
        private static IPAddress ip;
        private static IPEndPoint ipe;

        private static string _timestamp = "";
        private static string _requestid = "";
        private static string _version = "1.0";
        private static string _bidnumber = "";
        private static string _info = "Win7;ie:11;27";
        private static string _uniqueid = "";
        private static string _bidpassword = "";
        private static string _imagenumber = "";
        private static string _idcard = "";
        private static string _clientId = "";
        private static string _idtype = "0";


        public MainFrm()
        {
            MessageQueue = new Queue();
            InitializeComponent();
            comboIP.SelectedIndex = 0;
            host = comboIP.SelectedItem.ToString();
            if (debug)
            {
                host = "192.168.1.180";
            }
            ip = IPAddress.Parse(host);
            ipe = new IPEndPoint(ip, port);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }
        //uid=54465795&uname=鲍XX&clientId=42b198991e44411087d5dce78ab18e3c&tradeserverstr=180.153.29.213:8300,180.153.15.118:8300,180.153.24.227:8300,180.153.38.219:8300&informationserverstr=&webserverstr=paimai2.alltobid.com:80&lcserverstr=&auctype=0&pwd=6e08e2b216444285aa310a8785129cd3
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string key = "ji!@p!a".Substring(2, 5);
        private static string clientId = "42b198991e44411087d5dce78ab18e3c";
        private static string bidnumber = "54465795";
        private static string version = "1.0";
        //private static bool stop = false;
        private static Queue MessageQueue;
        private static Socket clientSocket;
        private static int port = 8300;
        //key = gfheru3
        //key = shcarbid

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            timer1.Enabled = true;

            clientSocket.Connect(ipe);

            //receive message
            new Task(() =>
            {
                while (clientSocket.Connected)
                {
                    string recStr = "";
                    byte[] recBytes = new byte[4096];

                    //byte[] t = new byte[] { 20 };

                    Thread.Sleep(100);
                    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                    recStr += "CurrentTime:" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ": ";
                    //recStr += Encoding.UTF8.GetString(recBytes, 0, bytes);
                    recStr += Encoding.GetEncoding("GB2312").GetString(recBytes, 0, bytes);

                    recStr += "\r\n";
                    MessageQueue.Enqueue(recStr);
                    Thread.Sleep(100);
                }
            }).Start();


        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            //if (clientSocket.Connected == false)
            //{
            //    clientSocket.Connect(ipe);
            //}
            SendOnlineMessage();
        }

        public static int SendMessage(string command, byte[] sentmsg)
        {
            //if (clientSocket.Connected == false)
            //{
            //    clientSocket.Connect(ipe);
            //}
            clientSocket.Send(SetPackHeader(command, sentmsg));
            return 0;
        }

        private static string EncryptWithMD5(String argString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(argString);
            byte[] result = md5.ComputeHash(data);
            String strReturn = String.Empty;
            for (int i = 0; i < result.Length; i++)
                strReturn += result[i].ToString("x").PadLeft(2, '0');
            return strReturn;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MessageQueue.Count > 0)
            {
                var message = MessageQueue.Dequeue();
                logOutPut.Text += message;
                logOutPut.Refresh();
                log.Info(message);
            }
            SendHeartMessage();
        }
        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientSocket.Close();
        }

        private void btnGetImageCode_Click(object sender, EventArgs e)
        {
            _bidnumber = txtBidNumber.Text;
            _bidpassword = txtPassword.Text;
            _idcard = txtidcard.Text;
            _timestamp = gettimestamp_JS();
            _requestid = _timestamp;

            //var request = (HttpWebRequest)WebRequest.Create("https://paimai2.alltobid.com/webwcf/BidCmd.svc/WebCmd");
            var request = (HttpWebRequest)WebRequest.Create("https://paimai.alltobid.com/webwcf/BidCmd.svc/WebCmd");

            var dto = new ImageCodeDto
            {
                version = _version,
                timestamp = _timestamp,
                requestid = _requestid,
                request = { }, //request={}
                checkcode = EncryptWithMD5(_timestamp + _requestid + _version)
            };

            var json = JsonConvert.SerializeObject(dto);
            var encodedJson = HttpUtility.UrlEncode(json);

            var postData = new WebCmdDto
            {
                method = "getimagecode",
                cmd = encodedJson
            };

            var postDataJson = JsonConvert.SerializeObject(postData);

            var data = Encoding.ASCII.GetBytes(postDataJson);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Referer = "https://paimai.alltobid.com/pubbid/2017112001/login.htm";


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            var imagedata = responseString.Substring(responseString.IndexOf("data") + 9, 36);
            _uniqueid = imagedata;
            var imgurl = responseString.Substring(responseString.IndexOf("data") + 9 + 36 + 1, 81).Replace("\\", "");
            pictureBoxLogin.Load(imgurl);
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            _imagenumber = txtImageNumber.Text;
            var request = (HttpWebRequest)WebRequest.Create("https://paimai.alltobid.com/webwcf/BidCmd.svc/WebCmd");
            _timestamp = gettimestamp_JS();
            _requestid = getrequestid_JS();
            var dto = new LoginDto
            {
                version = _version,
                timestamp = _timestamp,
                bidnumber = _bidnumber,
                requestid = _requestid,
                checkcode = EncryptWithMD5(EncryptWithMD5(_bidnumber + _bidpassword) + _bidnumber + _imagenumber + _idcard + _requestid + _uniqueid + _version),
                request = new LoginRequestDto
                {
                    info = _info,
                    uniqueid = _uniqueid,
                    bidnumber = _bidnumber,
                    bidpassword = EncryptWithMD5(_bidnumber+_bidpassword),
                    imagenumber = _imagenumber,
                    idcard = _idcard,
                    clientId = _clientId,
                    idtype = _idtype
                }
            };

            var json = JsonConvert.SerializeObject(dto);
            var encodedJson = HttpUtility.UrlEncode(json);

            var postData = new WebCmdDto
            {
                method = "login",
                cmd = encodedJson
            };

            var postDataJson = JsonConvert.SerializeObject(postData);

            var data = Encoding.ASCII.GetBytes(postDataJson);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Referer = "https://paimai2.alltobid.com/bid/2017111801/login.htm";


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            SetCookie(JsonConvert.DeserializeObject(responseString));
        }

        private static string gettimestamp_JS()
        {
            long lLeft = 621355968000000000;
            DateTime dt = DateTime.Now;
            long timestamp = (dt.Ticks - lLeft) / 10000;
            return timestamp.ToString();
        }

        private static string getrequestid_JS()
        {
            return gettimestamp_JS();
        }

        private static string getrequestid_AS()
        {
            return _bidnumber + "." + createTimestamp();
        }

        private static void SetCookie(Object logindata)
        {
            //System.Web.HttpCookie newcookie = new HttpCookie("logindata");
            //newcookie.Values["bidnumber"] = _bidnumber;
            //newcookie.Values["username"] = logindata.name;
            //newcookie.Values["clientId"+ _bidnumber] = logindata.clientid;
            //newcookie.Values["bidcount"] = logindata.bidcount;
            //newcookie.Values["vdate"] = logindata.date;
            //newcookie.Values["pwd"] = logindata.b;
            //newcookie.Values["bidcount"] = logindata.bidcount;
            //var traderserverstr = "";
            //for (var i = 0; i < logindata.tradeserver.length; i++)
            //{
            //    traderserverstr += ',' + logindata.tradeserver[i].server + ':' + logindata.tradeserver[i].port
            //};
            //if (traderserverstr != "") traderserverstr = traderserverstr.Substring(1);
            //newcookie.Values["tradeserver"] = traderserverstr;

            //var webserverstr = "";
            //for (var k = 0; k < logindata.webserver.length; k++)
            //{
            //    webserverstr += ',' + logindata.webserver[k].server + ':' + logindata.webserver[k].port
            //};
            //if (webserverstr != "") webserverstr = webserverstr.Substring(1);
            //newcookie.Values["webserver"] = webserverstr;
            //Response.AppendCookie(newcookie);          
        }

        private static void SendHeartMessage()
        {
            var dto = new Bytes0_0Dto
            {
                ts = createTimestamp()
            };

            var json = JsonConvert.SerializeObject(dto);

            SendMessage("0-0",EncryptWithXXTEA(json) );
        }

        //"投标板块","上线1-1"
        private static void SendOnlineMessage()
        {
            var _timestamp = createTimestamp();
            var _requestid = _bidnumber + "." + _timestamp;
            var dto = new Bytes1_1Dto
            {
                requestid = _requestid,
                timestamp = _timestamp,
                bidnumber = _bidnumber,
                //checkcode = EncryptWithMD5(_bidnumber+ _clientId+ _requestid+ _timestamp+ _version).ToLower()),
                //反编译出来的checkcode版本
                checkcode = EncryptWithMD5((_clientId + _bidnumber + _version + _requestid + _version + _timestamp).ToLower()),
                version = _version
            };

            var json = JsonConvert.SerializeObject(dto);

            SendMessage("1-1", EncryptWithXXTEA(json));
        }

        private static string createTimestamp()
        {
            DateTime now = DateTime.Now;
            return now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + now.Millisecond.ToString();
        }

        private static byte[] EncryptWithXXTEA(String argString)
        {
            //string keyStr = "shcarbid";
            string keyStr = "ji!@p!a".Substring(2, 5);
            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyStr);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(argString);
            byte[] encryptData = XXTEA.Encrypt(byteArray,key);

            return encryptData;
        }

        public static byte[] SetPackHeader(String header, byte[] data)
        {
            var headerArr = header.Split('-');
            var header0 = int.Parse(headerArr[0]);
            var header1 = int.Parse(headerArr[1]);
            var convertedHeader0 = ToUInt8(header0);
            var convertedHeader1 = ToUInt8(header1);
            var length = ToUInt32(data.Length + 4 + 1 + 1);
            var byteList = new List<byte>();

            var byteToWrite = BitConverter.GetBytes(length);
            byteList.AddRange(byteToWrite);

            byteList.Add(convertedHeader0);
            byteList.Add(convertedHeader1);
            byteList.AddRange(data);
            return byteList.ToArray();

            //          string[] headerArr = header.Split('-');
            //          //trace(data.length+4+2)
            //          byte[] appendHeaderPack = new byte[4096];
            //          //整包长度= 包体长度+主包头+子包头;
            //          //4字节 表示包长度
            //          appendHeaderPack.toUInt32(data.Length + 4 + 1 + 1);
            //          appendHeaderPack.toUInt8(headerArr[0]);
            //          appendHeaderPack.toUInt8(headerArr[1]);
            //          appendHeaderPack.writeBytes(data);

            //          /*1字节 8位整数 0 和 255 之间的 32 位无符号整数
            //          public function toUInt8(value:int):void {
            //              if (value > 255) value = 255;
            //              this.writeByte(value);
            //          }4字节 代表无符号32位整数，取值范围在 0 ~ 4,294,967,295之间
            //          public function toUInt32(value:uint):void
            //{
            //   	if (value > 4294967295) value = 4294967295;			
            //	this.writeUnsignedInt(value);
            //}
            //          */

            //          return appendHeaderPack;
        }

        private static byte ToUInt8(int value)
        {
            if (value > 255)
                value = 255;
            return (byte)value;
        }


        private static uint ToUInt32(int value)
        {
            //if (value > 4294967295)
            //    value = 4294967295;
            return (uint)value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendHeartMessage();
        }
      
    }
}
