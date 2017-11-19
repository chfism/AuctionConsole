using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AuctionSocketClient.Dto
{
    class LoginDto
    {
        public string version;
        public string timestamp;
        public string bidnumber;
        public string requestid;
        public string checkcode;
        public LoginRequestDto request;
    }
}