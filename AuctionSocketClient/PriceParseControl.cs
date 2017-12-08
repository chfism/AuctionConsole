using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using AuctionSocketClient.Dto;

namespace AuctionSocketClient
{
    //没什么用，都是右侧的出价信息
    class PriceParseControl
    {
        private static void PriceParse(string jsonstr, int type)
        {
            var result = RemoveSpecialCharacters(jsonstr);
            switch (type)
            {
                case 0:
                    setPriceInfo(ComposeStatusTxt(jsonstr));
                    break;
                case 1:
                    //第一阶段出价情况？
                    //httpState(jsonstr);
                    break;
                case 2:
                    //get2To3Header(jsonstr);
                    break;
                case 3:
                    //第二阶段出价情况？
                    //httpState2_3(jsonstr);
                    break;

            }
        }

        public static string RemoveSpecialCharacters(string input)
        {
            string result = input.Replace(">", "").Replace("<", "").Replace("&", "");
            return result;
        }

        private static string ComposeStatusTxt(string jsonstr)
      {
         var _responseobject = new ResponseDTO();
         _responseobject = JsonConvert.DeserializeObject<ResponseDTO>(jsonstr);
         string _responsecode = _responseobject.responsecode;
         if(_responsecode == "0" && _responseobject.results != null && _responseobject.results[0] != null)
         {
            var _bidcount = _responseobject.results[0].bidcount;
            var _type = _responseobject.results[0].type;
            var _txt = "您第" + _bidcount + "次出价\n";
            _txt = _txt + ("出价金额:" + _responseobject.results[0].bidamount + "\n");
            _txt = _txt + ("出价时间:" + Remove000(_responseobject.results[0].dealtime) + "\n");
            if(_type == "1")
            {
            	_txt = _txt + "出价方式:网络出价";
            }
            else
            {
            	_txt = _txt + "出价方式:电话出价";
            }
            return _txt;
         }
         return "";
      }

        private static string Remove000(string input)
        {
            string result;
            if (input.IndexOf("000") != -1)
            {
                result = input.Substring(0, input.Length - 4);
            }
            return result;
        }

        public static string setPriceInfo(string val) 
      {
            var htmlText = "<font color=\'#FF0000\'>" + val + "</font>";
            return htmlText;
      }
}
}