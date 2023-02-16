using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChurchApi.Configurations;
using ChurchApi.Data;
using ChurchApi.Helpers;
using ChurchApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChurchApi.Services
{

    public class RegisterUserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
        public long ProfileId { get; set; }
        public string RootPath { get; set; }
        public View View { get; set; }
        public long? BankId { get; set; }
        public string Bank { get; set; }
    }


    public class UpdateUserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password), StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string RootPath { get; set; }
        public long ProfileId { get; set; }
        public View View { get; set; }
        public long? BankId { get; set; }
        public string Bank { get; set; }
    }


    public class SignUpDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        public View View { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public long CountryId { get; set; }
        public string RootPath { get; set; }
        public string Picture { get; set; }
        public long? BankId { get; set; }
        public string Bank { get; set; }
    }


    public class LoginParams
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Username { get; set; }
        //public string View { get; set; }
        public string Token { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public View View { get; set; }
        public string Role { get; set; }
        public long ProfileId { get; set; }
        //public long? BankId { get; set; }
        //public string Bank { get; set; }
    }

    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        public string RootPath { get; set; }
        public View View { get; set; }
        public long? BankId { get; set; }
    }

    public class ChangePasswordDto
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserFilter
    {
        [FromQuery]
        public string Text { get; set; }
        [FromQuery]
        public string PhoneNumber { get; set; }
        [FromQuery]
        public string Name { get; set; }
        [FromQuery]
        public string Username { get; set; }
        [FromQuery]
        public string Bank { get; set; }

        [FromQuery]
        public bool Locked { get; set; }

        [FromQuery(Name = "_page")]
        public int Page { get; set; }

        [FromQuery(Name = "_size")]
        public int Size { get; set; }

        public int Skip() { return (Page - 1) * Size; }

        public IQueryable<User> BuildQuery(IQueryable<User> query)
        {
            if (!string.IsNullOrEmpty(Text)) query = query.Where(q => q.Name.Contains(Text) || q.UserName.Contains(Text) || q.PhoneNumber.Contains(Text));
            if (!string.IsNullOrEmpty(PhoneNumber)) query = query.Where(q => q.PhoneNumber.Contains(PhoneNumber));
            if (!string.IsNullOrEmpty(Name)) query = query.Where(q => q.Name.Contains(Name));
            //if (!string.IsNullOrEmpty(Bank)) query = query.Where(q => q.Bank.Name.Contains(Bank));

            return query;
        }
    }

    public interface IUserService
    {
        Task<UserProfileDto> UserProfile(string username);
        Task<bool> UpdateUserProfile(UserProfileDto profile);
        Task<bool> ChangePassword(ChangePasswordDto passwords);
        Task<LoginResponse> Authenticate(LoginParams loginParams);
        Task<bool> CreateUser(RegisterUserModel model);
        Task<bool> UpdateUser(UpdateUserModel model);
        Task<bool> DeleteUser(string username);
        Task<List<UserDto>> GetAllUsers();
        Task<List<string>> GetPrivileges();
        Task<bool> SignUp(SignUpDto model);
        Task<bool> ResetPassword(SignUpDto model);
        Task<Tuple<ICollection<UserDto>, long>> Query(UserFilter filter);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IMessageService _messageService;

        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            //IMessageService messageService,
            AppDbContext context,
            IOptions<JwtSettings> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_messageService = messageService;
            _context = context;
            _jwtSettings = jwt.Value;
        }

        public async Task<bool> CreateUser(RegisterUserModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Email = model.Email,
                ProfileId = model.ProfileId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                //BankId = model.BankId,
                View = model.View
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) throw new Exception(ExceptionHelper.ProcessException(result));

            return result.Succeeded;
        }

        public async Task<bool> SignUp(SignUpDto model)
        {
            //var verify = await _context.VerificationCodes
            //    .FirstOrDefaultAsync(q => q.Code == model.Code
            //    && q.PhoneNumber == model.PhoneNumber);
            //if (verify == null) throw new Exception("I can't verify your account. Please ensure your email address and verification code are correct.");
            //if (verify.ExpiresOn < DateTime.UtcNow) throw new Exception("Verification code expired.");

            var user = new User
            {
                UserName = model.Username,
                // Role = Role.User,
                Name = model.Name,
                Email = model.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PhoneNumber = model.PhoneNumber,
                View = model.View,
                //BankId = model.BankId,
                // Account = new Account
                // {
                //     Name = model.Name,
                //     PhoneNumber = model.PhoneNumber,
                //     SenderName = GetSenderName(model.Username),
                //     ApiKey = GeneralHelpers.TokenCode(32),
                //     PublicKey = GeneralHelpers.TokenCode(8),
                //     Gateway = MessageGateway.SmsGlobal
                // }
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) throw new Exception(ExceptionHelper.ProcessException(result));

            _context.SaveChanges();

            //  await  new Messenger().SendSMSAsync(new SMSDto
            //{
            //    SenderName = "Tawo",
            //    ReceipientNumber = user.PhoneNumber,
            //    Message = $"{user.VerificationCode} is your Tawo verification code."
            //});
            //return result.Succeeded;
            return true;
        }

        public async Task<bool> ResetPassword(SignUpDto model)
        {
            //var verify = await _context.VerificationCodes
            //    .FirstOrDefaultAsync(q => q.Code == model.Code
            //    && q.PhoneNumber == model.PhoneNumber);
            //if (verify == null) throw new Exception("I can't verify your account. Please ensure your email and verification code are correct.");
            //if (verify.ExpiresOn < DateTime.UtcNow) throw new Exception("Verification code expired.");

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) throw new Exception("I can't find your account. Please signup.");

            var clearPassword = await _userManager.RemovePasswordAsync(user);
            if (clearPassword.Succeeded)
            {
                var res = await _userManager.AddPasswordAsync(user, model.Password);
                if (!res.Succeeded) throw new Exception(ExceptionHelper.ProcessException(res));
            }

            return true;
        }

        public async Task<Tuple<ICollection<UserDto>, long>> Query(UserFilter filter)
        {
            var query = filter.BuildQuery(_context.Users.Where(x => x.Id != null));
            var data = await query.OrderByDescending(x => x.Id)
                .Skip(filter.Skip()).Take(filter.Size)
                .Select(q => new UserDto
                {
                    Id = q.Id,
                    Username = q.UserName,
                    Name = q.Name,
                    Email = q.Email,
                    Role = q.Profile.Name,
                    View = q.View,
                    PhoneNumber = q.PhoneNumber,
                    ProfileId = q.ProfileId,
                    //BankId = q.BankId,
                    //Bank = q.Bank.Name
                }).ToListAsync();
            var total = await query.CountAsync();
            return new Tuple<ICollection<UserDto>, long>(data, total);
        }

        public async Task<bool> DeleteUser(string username)
        {
            var theUser = await _userManager.FindByNameAsync(username);
            await _userManager.DeleteAsync(theUser);
            return true;
        }

        public async Task<List<string>> GetPrivileges()
        {
            return await Task.FromResult(_roleManager.Roles.Select(q => q.Name).ToList());
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await Task.FromResult(_context.Users.Select(q => new UserDto
            {
                Id = q.Id,
                Username = q.UserName,
                Name = q.Name,
                Email = q.Email,
                View = q.View,
                PhoneNumber = q.PhoneNumber,
                //Bank = q.Bank.Name
            }).ToList());
        }

        public async Task<LoginResponse> Authenticate(LoginParams loginParams)
        {
            var user = await _userManager.FindByNameAsync(loginParams.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginParams.Password))
            {
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

                var roles = _userManager.GetRolesAsync(user).Result;
                var claims = roles.Select(x => new Claim("roles", x)).ToList();
                claims.Add(new Claim("username", user.UserName));
                claims.Add(new Claim("email", user.Email ?? ""));
                claims.Add(new Claim("phoneNumber", user.PhoneNumber ?? ""));
                claims.Add(new Claim("name", user.Name));
                claims.Add(new Claim("view", user.View.ToString()));

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    expires: DateTime.UtcNow.AddHours(8),
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                    claims: claims);

                return new LoginResponse
                {
                    Username = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }

            throw new Exception("Invalid username or password");
        }

        public async Task<bool> UpdateUser(UpdateUserModel model)
        {
            var user = _context.Users.FirstOrDefault(q => q.UserName == model.Username);
            if (user == null) throw new Exception("User not found.");

            user.Name = model.Name;
            user.UpdatedAt = DateTime.Now;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.ProfileId = model.ProfileId;
            user.View = model.View;
            //user.BankId = model.BankId;
            var res = await _userManager.UpdateAsync(user);

            if (!res.Succeeded) throw new Exception(ExceptionHelper.ProcessException(res));


            //Change password
            if (!string.IsNullOrEmpty(model.Password))
            {
                var clearPassword = await _userManager.RemovePasswordAsync(user);
                if (clearPassword.Succeeded) await _userManager.AddPasswordAsync(user, model.Password);
            }

            return true;
        }

        public async Task<UserProfileDto> UserProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) throw new Exception("Sorry, I can't find your account profile.");
            return new UserProfileDto
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                View = user.View,
                //BankId = user.BankId
        };
        }

        public async Task<bool> UpdateUserProfile(UserProfileDto profile)
        {
            var user = await _userManager.FindByNameAsync(profile.Username);
            if (user == null) throw new Exception("I can't find your account. Ensure you are logged in.");
            var validPassword = await _userManager.CheckPasswordAsync(user, profile.Password);
            if (!validPassword) throw new Exception("Invalid Password");

            user.Name = profile.Name;
            user.PhoneNumber = profile.PhoneNumber;
            user.Email = profile.Email;

            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded) throw new Exception(ExceptionHelper.ProcessException(res));

            return true;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto passwords)
        {
            if (passwords.NewPassword != passwords.ConfirmPassword) throw new Exception("Password Mismatch");
            var user = await _userManager.FindByNameAsync(passwords.Username);
            if (user == null) throw new Exception("I can't find your account. Ensure you are logged in.");
            var validPassword = await _userManager.CheckPasswordAsync(user, passwords.CurrentPassword);
            if (!validPassword) throw new Exception("Invalid Password");

            var clearPassword = await _userManager.RemovePasswordAsync(user);
            if (clearPassword.Succeeded)
            {
                var res = await _userManager.AddPasswordAsync(user, passwords.NewPassword);
                if (!res.Succeeded) throw new Exception(ExceptionHelper.ProcessException(res));
            }
            return true;

        }

    }
}
