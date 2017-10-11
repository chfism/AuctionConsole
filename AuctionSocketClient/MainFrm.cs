using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace AuctionSocketClient
{
    public partial class MainFrm : Form
    {
        private bool debug = true;
        public MainFrm()
        {
            InitializeComponent();
            comboIP.SelectedIndex = 0;
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string key = "ji!@p!a".Substring(2, 5);
        private static string clientId = "27d8ead720994414bb4931ef3b2bafeb";
        private static string bidnumber = "54297820";
        private static string version = "1.0";
        //key = gfheru3
        //key = shcarbid

        private void btnStart_Click(object sender, EventArgs e)
        {
            int port = 8300;
            var host = comboIP.SelectedItem.ToString();
            if (debug)
            {
                host = "10.80.65.40";
            }         
            var ip = IPAddress.Parse(host);
            var ipe = new IPEndPoint(ip, port);

            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(ipe);

            //receive message
            new Task(() =>
            {
                while (true)
                {
                    string recStr = "";
                    byte[] recBytes = new byte[4096];
                    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                    //Online XXTEA Decrypt https://www.tools4noobs.com/online_tools/xxtea_decrypt/
                    recStr += "CurrentTime" + DateTime.Now.ToLongTimeString() + ": ";
                    recStr += Encoding.UTF8.GetString(recBytes, 0, bytes);
                    logOutPut.Text += recStr;
                    log.Info(recStr);
                    Thread.Sleep(100);
                }
            }).Start();
            clientSocket.Close();
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            //         this.currenttime = String(_currentdate.getHours()) + String(_currentdate.getMinutes()) + String(_currentdate.getSeconds()) + String(_currentdate.getMilliseconds());
            var currenttime = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            var requestid = bidnumber + ".f" + currenttime;
            var checkcode = MD5ToString(clientId + bidnumber + version + requestid + version + currenttime).ToLower();
            var ByteArrayCollection;
            var _rawdata = "{requestid:\"" + requestid + "\",timestamp:\"" + currenttime + "\",bidnumber:\"" + bidnumber + "\",checkcode:\"" + checkcode + "\",version:\"" + version + "\"}";
            string _encryptedstr = XXTEA.Base64Encrypted(_rawdata);
            ByteArrayCollection.toStr(_encryptedstr);
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
    }
}
