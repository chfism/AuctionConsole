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

namespace AuctionSocketClient
{
    public partial class MainFrm : Form
    {
        private bool debug = true;
        private string host = "";
        private IPAddress ip;
        private IPEndPoint ipe;

        private string _version = "1.0";
        private string _timestamp = "1510973511872";
        private string _bidnumber = "54397068";
        private string _requestid = "1510973511873";
        private string _checkcode = "8906ea80b9693d0401305f458e069fbf";
        private string _info = "Win7;ie:11;27";
        private string _uniqueid = "3451a8e7-0760-4a42-87f9-317fb509ab42";
        private string _bidpassword = "912ca04c3a2c1e871e768957246d18f0";
        private string _imagenumber = "736424";
        private string _idcard = "";
        private string _clientId = "";
        private string _idtype = "0";

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
        //uid=54465795&uname=鲍洁莹&clientId=42b198991e44411087d5dce78ab18e3c&tradeserverstr=180.153.29.213:8300,180.153.15.118:8300,180.153.24.227:8300,180.153.38.219:8300&informationserverstr=&webserverstr=paimai2.alltobid.com:80&lcserverstr=&auctype=0&pwd=6e08e2b216444285aa310a8785129cd3
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
                    //Online XXTEA Decrypt https://www.tools4noobs.com/online_tools/xxtea_decrypt/
                    recStr += "CurrentTime:" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +": ";
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
            var currenttime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var requestid = bidnumber + ".f" + currenttime;
            var checkcode = MD5ToString(clientId + bidnumber + version + requestid + version + currenttime).ToLower();
            //var ByteArrayCollection;
            var _rawdata = "{requestid:\"" + requestid + "\",timestamp:\"" + currenttime + "\",bidnumber:\"" + bidnumber + "\",checkcode:\"" + checkcode + "\",version:\"" + version + "\"}";
            //TODO:  XXTEA.Base64Encrypte??
            //string _encryptedstr = XXTEA.Base64Encrypted(_rawdata);
            //ByteArrayCollection.toStr(_encryptedstr);

            if (clientSocket.Connected == false)
            {
                clientSocket.Connect(ipe);
            }
            SendMessage(clientSocket,_rawdata);

        }

        public static int SendMessage(Socket clientSocket, string sentmsg)
        {
            byte[] msg = Encoding.UTF8.GetBytes(sentmsg);
            //byte[] msg = Encoding.GetEncoding("GB2312").GetBytes(sentmsg);
            byte[] bytes = new byte[256];
            try
            {
                // Blocks until send returns.
                int i = clientSocket.Send(msg, msg.Length, SocketFlags.None);
                Console.WriteLine("Sent {0} bytes.", i);

                // Get reply from the server.
                int byteCount = clientSocket.Receive(bytes, clientSocket.Available,
                                                   SocketFlags.None);
                if (byteCount > 0)
                    Console.WriteLine(Encoding.UTF8.GetString(bytes));
            }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                return (e.ErrorCode);
            }
            return 0;
        }


        private static string MD5ToString(String argString)
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
            //clientSocket.Close();
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
        }
        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientSocket.Close();
        }


        private void btnGetImageCode_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://paimai2.alltobid.com/webwcf/BidCmd.svc/WebCmd");

            var dto = new ImageCodeDto
            {
                version = _version,
                timestamp = _timestamp,
                requestid = _requestid,
                request = "", //request={}
                checkcode = _checkcode,
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
            request.Referer = "https://paimai2.alltobid.com/bid/2017111801/login.htm";


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            var imagedata = responseString.Substring(responseString.IndexOf("data") + 9, 36);
            var imgurl = responseString.Substring(responseString.IndexOf("data") + 9 + 36 + 1, 82).Replace("\\", "");
            pictureBoxLogin.Load(imgurl);
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://paimai2.alltobid.com/webwcf/BidCmd.svc/WebCmd");

            var dto = new LoginDto
            {
                version = _version,
                timestamp = _timestamp,
                bidnumber = _bidnumber,
                requestid = _requestid,
                checkcode = _checkcode,
                request = new LoginRequestDto
                {
                    info = _info,
                    uniqueid = _uniqueid,
                    bidnumber = _bidnumber,
                    bidpassword = _bidpassword,
                    imagenumber = textBoxImageCode.Text,
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
        }
    }
}
