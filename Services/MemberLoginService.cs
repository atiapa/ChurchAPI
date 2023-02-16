using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChurchApi.Services
{ 
    public interface IMemberLoginService : IModelService<MemberLoginDto> 
    {
        Task<MemberLoginDto> Login(string username,string password);
        Task<MemberLoginDto> FindAsync(int memberId);
        
        Task<bool> RecoverPassword(string email);
    }
    public class MemberLoginService : BaseService<MemberLoginDto, MemberLogin>, IMemberLoginService
    {
        public MemberLoginService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public  async Task<MemberLoginDto> FindAsync(int memberId)
        {
            var data = await _context.MemberLogin.Where(q => q.MemberID == memberId).FirstOrDefaultAsync();
            if (data == null) throw new Exception("Record not found");

            return new MemberLoginDto
            {
                FirstName = data.FirstName,
                MiddleName = data.MiddleName,
                LastName = data.LastName,
                Username = data.Username,
                MemberID = data.MemberID,
                ChurchID = data.ChurchID
            };
        }

        public async Task<MemberLoginDto> Login(string username, string password)
        {
            var user = await _context.MemberLogin.Where(q => q.Username == username && q.Password == password).FirstOrDefaultAsync();
            if (user == null) throw new Exception("Invalid username or password");

            return new MemberLoginDto
            {
                FirstName=user.FirstName,
                MiddleName=user.MiddleName,
                LastName=user.LastName,
                Username=user.Username,
                MemberID=user.MemberID,
                DOB=user.DOB,
                ChurchID = user.ChurchID

            };
        }

        public override async Task<long> Save(MemberLoginDto record)
        {
            try
            {
                var member = await _context.MemberLogin.FirstOrDefaultAsync(q => q.Email == record.Email);
                if (member != null) throw new Exception("Email already exist");

                 member = await _context.MemberLogin.FirstOrDefaultAsync(q => q.Username == record.Username);
                if (member != null) throw new Exception("Email already exist");


                var model = _mapper.Map<MemberLoginDto>(record);
                //await _context.Membership_Tbl.AddAsync(data);
                //_context.SaveChanges();

                return await base.Save(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RecoverPassword(string email)
        {
            var member = await _context.MemberLogin.FirstOrDefaultAsync(q => q.Email == email);
            if (member == null) throw new Exception("Your email was not found");

            var church = await _context.churchDetail_tbl.FirstOrDefaultAsync(q => q.ChurchID == member.ChurchID);

            var msg = $"Dear {member.FirstName} {member.LastName}, <br> You recovered password is {member.Password} <br>";
             


            SendEmail(msg, member.Email, church);

            return true;
        }

        public static void SendEmail(string htmlString, string mailto, Churches church)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(church.smtpemail);
                message.To.Add(new MailAddress(mailto));
                message.Subject = "Member Registration";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = church.smtpport;
                smtp.Host = church.smtpserver; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(church.smtpemail, church.smtppassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }


        //public override async Task<long> Update(DocumentDto record)
        //{
        //    var doc = await _context.MemberLogin.FindAsync(record.Id);
        //    if (doc != null)
        //    {

        //        doc.ReferenceId = record.ReferenceId;
        //        doc.Description = record.Description;
        //        doc.FileName = await UpdateFile(record, doc.FileName, record.RootPath);
        //        doc.FileLabel = record.FileLabel;
        //        doc.RootPath = record.RootPath;
        //        doc.Timestamp = DateTime.UtcNow;
        //        doc.ModifiedBy = record.Username;
        //        doc.IsTemplate = record.IsTemplate;
        //        //doc.BankId = record.BankId;


        //        _context.Documents.Update(doc);
        //        _context.SaveChanges();
        //    }

        //    return doc.Id;
        //}


    }
}
