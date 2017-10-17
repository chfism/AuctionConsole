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

namespace AuctionSocketClient
{
    public partial class MainFrm : Form
    {
        private bool debug = true;
        private string host = "";
        private IPAddress ip;
        private IPEndPoint ipe;
        public MainFrm()
        {
            MessageQueue = new Queue();
            InitializeComponent();
            comboIP.SelectedIndex = 0;
            host = comboIP.SelectedItem.ToString();
            if (debug)
            {
                host = "10.80.65.100";
            }
            ip = IPAddress.Parse(host);
            ipe = new IPEndPoint(ip, port);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string key = "ji!@p!a".Substring(2, 5);
        private static string clientId = "27d8ead720994414bb4931ef3b2bafeb";
        private static string bidnumber = "54297820";
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
                    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                    //Online XXTEA Decrypt https://www.tools4noobs.com/online_tools/xxtea_decrypt/
                    recStr += "CurrentTime" + DateTime.Now.ToLongTimeString() + ": ";
                    recStr += Encoding.UTF8.GetString(recBytes, 0, bytes);
                    //recStr += Encoding.GetEncoding("GB2312").GetString(recBytes, 0, bytes);
                    recStr += "\r\n";
                    MessageQueue.Enqueue(recStr);                   
                    Thread.Sleep(100);
                }
            }).Start();

                      
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            //this.currenttime = String(_currentdate.getHours()) + String(_currentdate.getMinutes()) + String(_currentdate.getSeconds()) + String(_currentdate.getMilliseconds());
            var currenttime = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            var requestid = bidnumber + ".f" + currenttime;
            var checkcode = MD5ToString(clientId + bidnumber + version + requestid + version + currenttime).ToLower();
            //var ByteArrayCollection;
            var _rawdata = "{requestid:\"" + requestid + "\",timestamp:\"" + currenttime + "\",bidnumber:\"" + bidnumber + "\",checkcode:\"" + checkcode + "\",version:\"" + version + "\"}";
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
    }
}
