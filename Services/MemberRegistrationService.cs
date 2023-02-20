using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChurchApi.Services
{
    public interface IMemberRegistrationService : IModelService<MemberRegistrationDto> {
        Task<MemberRegistrationDto> Find(int memberID);
        //Task<decimal> UpdateMember(MemberRegistrationDto record); 
        Task<int> UpdateMember(MemberRegistrationDto record); 
        Task<MemberRegistrationDto> SearchMember(SearchFilter record); 
        
         Task<int> LastMember(int ChurchID);
        Task<List<MemberRegistrationDto>> DependantList(string dependantID);

        Task<List<MemberRegistrationDto>> GetMembersList();

        Task<List<MemberRegistrationDto>> GetActiveMembers();
        Task<List<MemberRegistrationDto>> GetInActiveMembers();
        /*Task<List<MemberRegistrationDto>> GetConfirmedMembers(string approved);*/
        Task<List<MemberRegistrationDto>> GetUnConfirmedMembers();

        Task<List<MemberRegistrationDto>> GetDeceasedMembers();
        Task<List<MemberRegistrationDto>> GetAdultMembers();

        Task<List<MemberRegistrationDto>> GetChildMembers();
        Task<List<MemberRegistrationDto>> TransferedMembersOut();
        Task<List<MemberRegistrationDto>> TransferedMembersIn();
        
       // Task DeleteMember(int memberID);
        Task DeleteMember(int memberID);
    }


    public class MemberRegistrationService : BaseService<MemberRegistrationDto, MemberRegistration>, IMemberRegistrationService
    {
        public MemberRegistrationService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<MemberRegistrationDto>> DependantList(string dependantID)
        {
            var data = await _context.Membership_Tbl.Where(q => q.DependantID == dependantID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName =q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                //Gender = q?.Gender,
                //Age = q?.Age,
                //DOB = q?.DOB,
                //MaritalStatus = q?.MaritalStatus,
                //Email = q?.Email,
                //AcademicLevel = q?.AcademicLevel,
                //ResidentialAddress = q?.ResidentialAddress,
                //Landmark = q?.Landmark,
                //Baptized = q?.Baptized,
                //Employment = q?.Employment,
                //Occupation = q?.Occupation,
                //PhoneNumber = q?.PhoneNumber,
                //ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                //BibleStudyGroup = q?.BibleStudyGroup,
                //Title = q?.Title,
                //PositionInChurch = q?.PositionInChurch,
                //ChurchID = q?.ChurchID,
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                //BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();
        }

        public async Task<List<MemberRegistrationDto>> getMemberList()
        {

            //if (data == null) throw new Exception("Record not found");
            return await Task.FromResult(_context.Membership_Tbl.Select(q => new MemberRegistrationDto
           {
               MemberID = q.MemberID,
               FirstName = q.FirstName,
               MiddleName = q.MiddleName,
               LastName = q.LastName,
               DependantID = q.DependantID,
               Relation = q.Relation,
               Gender = q.Gender,
               
               //Photo = q?.Photo
           }).ToList());
        }

        public override async Task<long> Save(MemberRegistrationDto record)
        {
            try
            {
                //var member = await _context.MemberLogin.FirstOrDefaultAsync(q => q.Email == record.Email);
                //if (member!=null) throw new Exception("Email already exist");

                var data = _mapper.Map<MemberRegistration>(record);
           await _context.Membership_Tbl.AddAsync(data);
                _context.SaveChanges();

            //return base.Save(model);
            var userData = await _context.MemberLogin.FirstOrDefaultAsync(q => q.MemberID == record.MemberID);
            var church = await _context.churchDetail_tbl.FirstOrDefaultAsync(q => q.ChurchID == record.ChurchID);

            var msg = $"Welcome {data.FirstName} {data.LastName}, <br> You are registered as a member of {church.branchName} <br>" +
                $"Your credentials are as follows: <br> Member ID: {data.MemberID} <br> Username: {userData.Username}" +
                $" <br> Password: {userData.Password}  <br> Church ID: {data.ChurchID}";

            SendEmail(msg, data.Email, church);

            return data.MemberID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        public async Task<MemberRegistrationDto> Find(int memberID)
        {
            var data = await _context.Membership_Tbl.Where(q => q.MemberID == memberID).FirstOrDefaultAsync();
            if (data == null) throw new Exception("Record not found");

            var record = new MemberRegistrationDto();
            record.MemberID = data.MemberID;
            record.FirstName = data.FirstName;
            record.MiddleName = data.MiddleName;
                record.LastName = data.LastName;
                record.Gender = data.Gender;
                record.Age = data.Age;
                record.DOB = data.DOB;
                record.MaritalStatus = data.MaritalStatus; 
                record.Email = data.Email;
                record.AcademicLevel = data.AcademicLevel;
                record.ResidentialAddress = data.ResidentialAddress;
                record.Landmark = data.Landmark;
                record.Baptized = data.Baptized;
                record.Employment = data.Employment;
                record.Occupation = data.Occupation;
                record.PhoneNumber = data.PhoneNumber;
                record.ChurchGroups = data.ChurchGroups;
                record.DigitalAddress = data.DigitalAddress;
                record.BibleStudyGroup = data.BibleStudyGroup;
                record.Title = data.Title;
                record.PositionInChurch = data.PositionInChurch;
                record.ChurchID = data.ChurchID;
                record.PostalAddress = data.PostalAddress;
                record.MemberStatus = data.MemberStatus;
                record.EntryDate = data.EntryDate;
                record.HomeCellGroup = data.HomeCellGroup;
                record.Deceased = data.Deceased;
                record.MarriageDate = data.MarriageDate;
                record.Status = data.Status;
                record.Remarks = data.Remarks;
                record.Inactive_Reason = data.Inactive_Reason;
                record.HolySpiritBaptism = data.HolySpiritBaptism;
                record.BaptizmaDate = data.BaptizmaDate;
                record.Service = data.Service;
                record.Communicant = data.Communicant;
                record.Transfered = data.Transfered;
                record.TransferedDate = data.TransferedDate;
                record.TransferedToFrom = data.TransferedToFrom;
                record.Officer = data.Officer;
                record.DependantID = data.DependantID;
                record.Relation = data.Relation;
                        record.Photo = data.Photo;
            record.MemberApproved = data.MemberApproved;
            return record;

            //return new MemberRegistrationDto
            //{
            //   MemberID=data.MemberID,
            //    FirstName = data.FirstName,
            //    MiddleName = data.MiddleName,
            //    LastName = data.LastName,
            //    Gender = data.Gender,
            //    Age = data.Age,
            //    //DOB = data.DOB,
            //    MaritalStatus = data.MaritalStatus, 
            //    Email = data.Email,
            //    AcademicLevel = data.AcademicLevel,
            //    ResidentialAddress = data.ResidentialAddress,
            //    Landmark = data.Landmark,
            //    Baptized = data.Baptized,
            //    Employment = data.Employment,
            //    Occupation = data.Occupation,
            //    PhoneNumber = data.PhoneNumber,
            //    ChurchGroups = data.ChurchGroups,
            //    DigitalAddress = data.DigitalAddress,
            //    BibleStudyGroup = data.BibleStudyGroup,
            //    Title = data.Title,
            //    PositionInChurch = data.PositionInChurch,
            //    ChurchID = data.ChurchID,
            //    PostalAddress = data.PostalAddress,
            //    MemberStatus = data.MemberStatus,
            //    EntryDate = data.EntryDate,
            //    HomeCellGroup = data.HomeCellGroup,
            //    Deceased =data.Deceased,
            //    MarriageDate = data.MarriageDate,
            //    Status = data.Status,
            //    Remarks = data.Remarks,
            //    Inactive_Reason = data.Inactive_Reason,
            //    HolySpiritBaptism = data.HolySpiritBaptism,
            //    BaptizmaDate = data.BaptizmaDate,
            //    Service=data.Service,
            //    Communicant = data.Communicant,
            //    Transfered = data.Transfered,
            //    TransferedDate = data.TransferedDate,
            //    TransferedToFrom = data.TransferedToFrom,
            //    Officer = data.Officer,
            //    DependantID = data.DependantID,
            //    Relation = data.Relation,
            //    Photo=data.Photo
            //};
        }
    
        
        public async Task DeleteMember(int memberID)
        {
            var theUser = await _context.Membership_Tbl.Where(q => q.MemberID == memberID).FirstOrDefaultAsync();
            if (theUser == null) throw new Exception("Record not found");
            _context.Membership_Tbl.Remove(theUser);
            await _context.SaveChangesAsync();
            //return false;
        }
        
         /*public async Task<MemberRegistrationDto> DeleteMember(int memberID)
        {
            var data = await _context.Membership_Tbl
                .Where(q => q.MemberID == memberID).FirstOrDefaultAsync();
            return (List<MemberRegistrationDto>)data.Sele(q => new MemberRegistrationDto
            {
                
            }

          
        }*/

        public async Task<int> LastMember(int churchID)
        {
            var data = await _context.Membership_Tbl.Where(q => q.ChurchID == churchID)
                .OrderBy(q=>q.MemberID)
                .LastOrDefaultAsync();
            //var data = record.LastOrDefault();
            if (data == null) return 1000;
            else return data.MemberID;

            
            //return new MemberRegistrationDto
            //{
            //    MemberID = data.MemberID,
            //    FirstName = data.FirstName,
            //    MiddleName = data.MiddleName,
            //    LastName = data.LastName,
                //Gender = data.Gender,
                //Age = data.Age,
                //DOB = data.DOB,
                //MaritalStatus = data.MaritalStatus,
                //Email = data.Email,
                //AcademicLevel = data.AcademicLevel,
                //ResidentialAddress = data.ResidentialAddress,
                //Landmark = data.Landmark,
                //Baptized = data.Baptized,
                //Employment = data.Employment,
                //Occupation = data.Occupation,
                //PhoneNumber = data.PhoneNumber,
                //ChurchGroups = data.ChurchGroups,
                //DigitalAddress = data.DigitalAddress,
                //BibleStudyGroup = data.BibleStudyGroup,
                //Title = data.Title,
                //PositionInChurch = data.PositionInChurch,
                //ChurchID = data.ChurchID,
                //PostalAddress = data.PostalAddress,
                //MemberStatus = data.MemberStatus,
                //EntryDate = data.EntryDate,
                //HomeCellGroup = data.HomeCellGroup,
                //Deceased = data.Deceased,
                //MarriageDate = data.MarriageDate,
                //Status = data.Status,
                //Remarks = data.Remarks,
                //Inactive_Reason = data.Inactive_Reason,
                //HolySpiritBaptism = data.HolySpiritBaptism,
                //BaptizmaDate = data.BaptizmaDate,
                //Service = data.Service,
                //Communicant = data.Communicant,
                //Transfered = data.Transfered,
                //TransferedDate = data.TransferedDate,
                //TransferedToFrom = data.TransferedToFrom,
                //Officer = data.Officer

            //};
        }

        public async Task<MemberRegistrationDto> SearchMember(SearchFilter record)
        {
            var data = await _context.Membership_Tbl
                .Where(q => q.MemberID == record.MemberID && q.DOB == record.DOB &&  q.FirstName == record.FirstName)
                .FirstOrDefaultAsync();
            if (data == null) throw new Exception("Record not found");
            return new MemberRegistrationDto
            {
                MemberID = data.MemberID,
                FirstName = data.FirstName,
                MiddleName = data.MiddleName,
                LastName = data.LastName,
                Gender = data.Gender,
                Age = data.Age,
                DOB = data.DOB,
                MaritalStatus = data.MaritalStatus,
                Email = data.Email,
                AcademicLevel = data.AcademicLevel,
                ResidentialAddress = data.ResidentialAddress,
                Landmark = data.Landmark,
                Baptized = data.Baptized,
                Employment = data.Employment,
                Occupation = data.Occupation,
                PhoneNumber = data.PhoneNumber,
                ChurchGroups = data.ChurchGroups,
                DigitalAddress = data.DigitalAddress,
                BibleStudyGroup = data.BibleStudyGroup,
                Title = data.Title,
                PositionInChurch = data.PositionInChurch,
                ChurchID = data.ChurchID,
                PostalAddress = data.PostalAddress,
                MemberStatus = data.MemberStatus,
                EntryDate = data.EntryDate,
                HomeCellGroup = data.HomeCellGroup,
                Deceased = data.Deceased,
                MarriageDate = data.MarriageDate,
                Status = data.Status,
                Remarks = data.Remarks,
                Inactive_Reason = data.Inactive_Reason,
                HolySpiritBaptism = data.HolySpiritBaptism,
                BaptizmaDate = data.BaptizmaDate,
                Service = data.Service,
                Communicant = data.Communicant,
                Transfered = data.Transfered,
                TransferedDate = data.TransferedDate,
                TransferedToFrom = data.TransferedToFrom,
                Officer = data.Officer,
                DependantID = data.DependantID,
                Relation = data.Relation,
                MemberApproved = data.MemberApproved
        };
        }

        public  async Task<int> UpdateMember(MemberRegistrationDto record)
        {
            //var lastMember = await _context.Membership_Tbl.Where(q => q.ChurchID == record.ChurchID)
            //   .OrderBy(q => q.MemberID).LastOrDefaultAsync();
            //var newMemberId = 0M;
            //if (lastMember != null) { newMemberId = lastMember.MemberID + 1; }
            //else { newMemberId = 1000; }

          

            //if (record.)
                if (record.File.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await record.File.CopyToAsync(memoryStream);

                    record.Photo = memoryStream.ToArray();
                }
            }

            var data = await _context.Membership_Tbl.FindAsync(record.MemberID);
            if (data != null)
            {

                data.FirstName = record.FirstName;
                data.LastName = record.LastName;
                data.MiddleName = record.MiddleName;
                data.Email = record.Email;
                 data.Age = record.Age;
                data.DOB = record.DOB;
                data.ChurchID = record.ChurchID;
                data.Gender = record.Gender;
                data.Title = record.Title;
                data.MaritalStatus = record.MaritalStatus;
                data.Employment = record.Employment;
                data.Occupation = record.Occupation;
                data.ResidentialAddress = record.ResidentialAddress;
                data.Landmark = record.Landmark;
                data.PostalAddress = record.PostalAddress;
                data.AcademicLevel = record.AcademicLevel;
                data.PositionInChurch = record.PositionInChurch;
                data.BibleStudyGroup = record.BibleStudyGroup;
                data.HomeCellGroup = record.HomeCellGroup;
                data.Baptized = record.Baptized;
                data.BaptizmaDate = record.BaptizmaDate;
                data.HolySpiritBaptism = record.HolySpiritBaptism;
                data.MaritalStatus = record.MaritalStatus;
                data.MarriageDate = record.MarriageDate;
                data.Officer = record.Occupation;
                data.DigitalAddress = record.DigitalAddress;
                data.Dateofdeath = record.Dateofdeath;
                data.Deceased = record.Deceased;
                data.ChurchGroups = record.ChurchGroups;
                data.Communicant = record.Communicant;
                data.Officer = record.Officer;
                data.Service = record.Service;
                data.Status = record.Status;
                data.Transfered = record.Transfered;
                data.TransferedDate = record.TransferedDate;
                data.TransferedToFrom = record.TransferedToFrom;
                data.Remarks = record.Remarks;
                data.MemberStatus = record.MemberStatus;
                data.MemberID = record.MemberID ;
                data.DependantID = record.DependantID;               
                data.Relation = record.Relation;
                record.MemberApproved = data.MemberApproved;
                data.Photo = record.Photo;
                _context.Membership_Tbl.Update(data);
                _context.SaveChanges();
            }


            return data.MemberID;
        }

        public static void SendEmail(string htmlString,string mailto,Churches church)
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
                smtp.Host =church.smtpserver; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(church.smtpemail, church.smtppassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }


        public async Task<List<MemberRegistrationDto>> GetMembersList()
        {
            var members = await _context.Membership_Tbl.ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);
            return members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();
        }
    
        
        public async Task<List<MemberRegistrationDto>> GetActiveMembers()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.Status == "Active").ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);GetApprovedMembers

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }
        public async Task<List<MemberRegistrationDto>> GetInActiveMembers()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.Status == "InActive").ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);GetApprovedMembers

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }

        public async Task<List<MemberRegistrationDto>> GetUnConfirmedMembers()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.MemberApproved != "Yes").ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);GetApprovedMembers

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }

        public async Task<List<MemberRegistrationDto>> GetDeceasedMembers()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.Deceased == "Yes").ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }



        public async Task<List<MemberRegistrationDto>> GetAdultMembers()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.Age >= 18).ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }

        public async Task<List<MemberRegistrationDto>> GetChildMembers()
        {
            var members = await _context.Membership_Tbl
                //  13 Years and below  
                .Where(q => q.Age <= 13).ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }

        public async Task<List<MemberRegistrationDto>> TransferedMembersOut()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.Transfered == "TRANSFERED TO").ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);GetApprovedMembers

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }

        public async Task<List<MemberRegistrationDto>> TransferedMembersIn()
        {
            var members = await _context.Membership_Tbl
                .Where(q => q.Transfered == "TRANSFERED FROM").ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);GetApprovedMembers

            return (List<MemberRegistrationDto>)members.Select(q => new MemberRegistrationDto
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                MiddleName = q?.MiddleName,
                LastName = q?.LastName,
                Gender = q?.Gender,
                Age = (int)q?.Age,
                DOB = q?.DOB,
                MaritalStatus = q?.MaritalStatus,
                Email = q?.Email,
                AcademicLevel = q?.AcademicLevel,
                ResidentialAddress = q?.ResidentialAddress,
                Landmark = q?.Landmark,
                Baptized = q?.Baptized,
                Employment = q?.Employment,
                Occupation = q?.Occupation,
                PhoneNumber = q?.PhoneNumber,
                ChurchGroups = q?.ChurchGroups,
                //DigitalAddress = q?.DigitalAddress,
                BibleStudyGroup = q?.BibleStudyGroup,
                Title = q?.Title,
                PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                DependantID = q?.DependantID,
                Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();

        }

    }
}
