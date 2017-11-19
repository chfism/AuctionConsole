using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AuctionSocketClient.Dto
{
    class LoginRequestDto
    {
        public string info;
        public string uniqueid;
        public string bidnumber;
        public string bidpassword;
        public string imagenumber;
        public string idcard;
        public string clientId;
        public string idtype;
    }
}