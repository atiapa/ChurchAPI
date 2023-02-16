//using Microsoft.AspNetCore.Identity;
//using OmatrackApi.Models;
//using ChurchApi.Data;
//using ChurchApi.DTOs;
//using ChurchApi.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ChurchApi.Services
//{
//    public class ProfileDto
//    {
//        [Required]
//        public long Id { get; set; }
//        [Required]
//        public string Name { get; set; }
//        [Required]
//        public List<string> Privileges { get; set; }
//        public string Description { get; set; }
//    }

//    public interface IProfileService : IModelService<ProfileDto> { }

//    public class ProfileService : IProfileService
//    {
//        private readonly AppDbContext _context;
//        private readonly UserManager<User> _userManager;

//        public ProfileService(UserManager<User> userManager, AppDbContext context)
//        {
//            _userManager = userManager;
//            _context = context;
//        }

//        public async Task<ProfileDto> FindAsync(long Id)
//        {
//            var record = _context.Profiles.Find(Id);
//            return await Task.FromResult(record != null ? new ProfileDto
//            {
//                Id = record.Id,
//                Name = record.Name,
//                //DefaultView = record.DefaultView,
//                Description = record.Description,
//                Privileges = record.Privileges.Split(',').ToList()
//            } : null);
//                    }

//        public async Task<List<ProfileDto>> FetchAllAsync()
//        {
//            return await Task.FromResult(_context.Profiles.ToList().Select(x => new ProfileDto
//            {
//                Id = x.Id,
//                Name = x.Name,
//                Description = x.Description,
//                Privileges = x.Privileges.Split(',').ToList()
//            }).ToList());
//        }

//        public async Task<long> Save(ProfileDto record)
//        {
//            var profile = new Profile
//            {
//                Name = record.Name,
//                Description = record.Description,
//                Privileges = record.Privileges.Aggregate((a, b) => $"{a},{b}")
//            };

//            await _context.Profiles.AddAsync(profile);
//            _context.SaveChanges();

//            return profile.Id;
//        }

//        public async Task<long> Update(ProfileDto record)
//        {
//            var profile = await _context.Profiles.FindAsync(record.Id);
//            profile.Name = record.Name;
//            profile.Description = record.Description;
//            profile.Privileges = record.Privileges.Aggregate((a, b) => $"{a},{b}");

//            //Update user privileges with profile new privileges
//            var users = _context.Users.Where(q => q.ProfileId == record.Id).ToList();
//            foreach (var user in users)
//            {
//                var oldRoles = await _userManager.GetRolesAsync(user);
//                var clearRoles = await _userManager.RemoveFromRolesAsync(user, oldRoles);
//                if (clearRoles.Succeeded)
//                {
//                    await _userManager.AddToRolesAsync(user, record.Privileges);
//                }
//            }


//            _context.Profiles.Update(profile);
//            _context.SaveChanges();
//            return record.Id;
//        }

//        public async Task<bool> Delete(long Id)
//        {
//            var record = await _context.Profiles.FindAsync(Id);
//            _context.Profiles.Remove(record);
//            _context.SaveChanges();
//            return true;
//        }


//    }
//}
