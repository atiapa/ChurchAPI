using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Integrations
{
    public class Postman
    {
        private const string BaseUrl = "https:/..../api";
        private readonly string _apiKey;
        private readonly string _senderName;

        public Postman(string apiKey, string senderName)
        {
            _apiKey = apiKey;
            _senderName = senderName;
        }

        public async Task<Tuple<bool, string>> SendMessage(string phoneNumbers, string text)
        {
            var response = await BaseUrl.AppendPathSegments("messages", "sendsms").PostJsonAsync(new
            {
                ApiKey = _apiKey,
                Sender = _senderName,
                Recipients = phoneNumbers,
                Text = text
            }).ReceiveJson<MsgResponse>();

            return new Tuple<bool, string>(response.Success, response.Response);
        }

        public async Task<Tuple<bool, string>> SendMessage(string emailAddresses, string subject, string text, byte[] attachment = null, string fileName = null)
        {
            var response = await BaseUrl.AppendPathSegments("messages", "sendemail").PostJsonAsync(new
            {
                ApiKey = _apiKey,
                Sender = _senderName,
                Subject = subject,
                Recipients = emailAddresses,
                Text = text,
                Attachment = attachment,
                FileName = fileName
            }).ReceiveJson<MsgResponse>();

            return new Tuple<bool, string>(response.Success, response.Response);
        }

        public async Task<decimal> CreditBalance()
        {
            var response = await BaseUrl.AppendPathSegments("accounts", "smsbalance")
                .SetQueryParams(new { ApiKey = _apiKey })
                .GetJsonAsync<decimal>();

            return response;
        }

        //public string GetPaymentLink() => $"https://msg.devnestsystems.com/buycredit/{_apiKey}";

        internal class CreditBalances
        {
            public decimal SmsBalance { get; set; }
            public decimal EmailBalance { get; set; }
        }

        internal class MsgResponse
        {
            public string Response { get; set; }
            public bool Success { get; set; }
        }
    }
}

