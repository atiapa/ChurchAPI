using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;

namespace ChurchApi.Helpers
{
    public class SMSDto
    {
        public string SenderName { get; set; }
        public string ReceipientNumber { get; set; }
        public string Message { get; set; }
    }
    public class Messenger
    {
        const string _ApiKey = "e773e871";
        const string _ApiSecret = "cl5Xhm3f1K0ZRHHi";


        //public void SendSMS(SMSDto sms)
        //{
        //    const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
        //    const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
        //    ServicePointManager.SecurityProtocol = Tls12;
        //    var client = new Client(creds: new Nexmo.Api.Request.Credentials
        //    {
        //        ApiKey = 38b90930,
        //        ApiSecret=QV04OQLyO66lmuHT

        //    });
        //    var result = client.SMS.Send(request: new SMS.SMSRequest
        //    {
        //        from = sms.SenderName,
        //        to = sms.ReceipientNumber,
        //        text = sms.Message
        //    });
        //}

        public async Task SendSMSAsync(SMSDto sms)
        {
           
            var credentials = Credentials.FromApiKeyAndSecret(
            
               _ApiKey,
                _ApiSecret

            );
            var vonageClient = new VonageClient(credentials);

            var response = await vonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
            {
                From = sms.SenderName,
                To = sms.ReceipientNumber,
                Text = sms.Message
            });
        }

    }
}
