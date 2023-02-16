//using Nexmo.Api;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Security.Authentication;
//using System.Threading.Tasks;

//namespace PortalApi.Integrations
//{
//    public class Messenger
//    {

//        //public static IRestResponse SendEmailMessage(long id)
//        //{
//        //    var db = new AppDbContext();
//        //    var eoe = db.EmailOutboxEntries.First(x => x.Id == id && !x.IsSent);
//        //    eoe.LastAttemptDate = DateTime.Now;
//        //    var client = new RestClient
//        //    {
//        //        BaseUrl = new Uri("https://api.mailgun.net/v3"),
//        //        Authenticator = new HttpBasicAuthenticator("api",
//        //            "key-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
//        //    };
//        //    var request = new RestRequest();
//        //    //request.
//        //    request.AddParameter("domain",
//        //        "sandboxxxxxxxxxxxxxxxxxxxxxxxxxxxx.mailgun.org", ParameterType.UrlSegment);
//        //    request.Resource = "{domain}/messages";
//        //    request.AddParameter("from", "App Name <mailgun@sandboxxxxxxxxxxxxxx.mailgun.org>");
//        //    request.AddParameter("to", eoe.Receiver);
//        //    request.AddParameter("subject", eoe.Subject);
//        //    request.AddParameter("html", eoe.Message);
//        //    request.AddParameter("text", "App Name");
//        //    request.Method = Method.POST;
//        //    var res = client.Execute(request);
//        //    if (res.StatusCode == HttpStatusCode.OK)
//        //    {
//        //        eoe.IsSent = true;
//        //        eoe.LastAttemptMessage = res.ResponseStatus.ToString();
//        //    }
//        //    else
//        //    {
//        //        eoe.LastAttemptMessage = res.ErrorMessage;
//        //    }

//        //    db.SaveChanges();
//        //    return res;
//        //}


//        //public static void SendVerificationEmail(User user)
//        //{
//        //    using (MailMessage mm = new MailMessage("resetpassword@mytawo.com", user.Email))
//        //    {
//        //        mm.Subject = "Verification Code";
//        //        string body = "Hello " + user.Name + ",";
//        //        body += "<br /><br />Your verification code is: ";
//        //        body += "<br />" + string.Format("{0}", user.VerificationCode);
//        //        body += "<br /><br />Thanks";
//        //        mm.Body = body;
//        //        mm.IsBodyHtml = true;
//        //        SmtpClient smtp = new SmtpClient();
//        //        smtp.Host = "mail.mytawo.com";
//        //        smtp.EnableSsl = false;
//        //        NetworkCredential NetworkCred = new NetworkCredential("resetpassword@mytawo.com", "Reset@2019");
//        //        smtp.UseDefaultCredentials = true;
//        //        smtp.Credentials = NetworkCred;
//        //        smtp.Port = 587;
//        //        smtp.Send(mm);
//        //    }
//        //}

//        //public static void SendNoReplyEmail(EmailObj obj)
//        //{
//        //    using (MailMessage mm = new MailMessage("noreply@mytawo.com", obj.ReceipientEmail))
//        //    {
//        //        mm.Subject = obj.Subject;
//        //        //string body = "Hello " + user.Name + ",";
//        //        //body += "<br /><br />Your verification code is: ";
//        //        //body += "<br />" + string.Format("{0}", user.VerificationCode);
//        //        //body += "<br /><br />Thanks";
//        //        mm.Body = obj.Body;
//        //        mm.IsBodyHtml = true;
//        //        SmtpClient smtp = new SmtpClient();
//        //        smtp.Host = "mail.mytawo.com";
//        //        smtp.EnableSsl = false;
//        //        NetworkCredential NetworkCred = new NetworkCredential("noreply@mytawo.com", "Noreply@2019");
//        //        smtp.UseDefaultCredentials = true;
//        //        smtp.Credentials = NetworkCred;
//        //        smtp.Port = 587;
//        //        smtp.Send(mm);
//        //    }
//        //}

//        public static void SendSMS(SMSObj sms)
//        {
//            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
//            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
//            ServicePointManager.SecurityProtocol = Tls12;

//            var client = new Client(creds: new Nexmo.Api.Request.Credentials
//            {
//                ApiKey = "38b90930",
//                ApiSecret = "QV04OQLyO66lmuHT",
//            });
//            var results = client.SMS.Send(request: new SMS.SMSRequest
//            {
//                from = sms.SenderName,
//                to = sms.ReceipientNumber,
//                text = sms.Message,
//            });

//        }


//    }
//}
