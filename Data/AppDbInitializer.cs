//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ChurchApi.Models;
//using OmatrackApi.Models;

//namespace ChurchApi.Data
//{
//    public class AppDbInitializer
//    {
//        public static async Task InitializeAsync(AppDbContext context, IServiceProvider serviceProvider)
//        {
//            context.Database.Migrate();

//            //Creating Roles
//            await SeedRoles(serviceProvider.GetRequiredService<RoleManager<IdentityRole>>());

//            //Create Default Admin User
//            await SeedUsers(serviceProvider.GetRequiredService<UserManager<User>>());

            
//            SeedAppSettings(context);

//            context.SaveChanges();
//        }

//        private static async Task SeedUsers(UserManager<User> userManager)
//        {
//            var exist = await userManager.FindByNameAsync("admin");
//            if (exist != null) return;

//            var res = await userManager.CreateAsync(new User
//            {
//                UserName = "admin",
//                Name="Administrator",
//                PhoneNumber = "0000000",
//                Email = "info@tinportal.com",
//                View=View.Admin,
//                Profile = new Profile
//                {
//                    Name = "Administrator",
//                    Description = "Administrator Role",
//                    Privileges = RoleNames.Aggregate((a, b) => $"{a},{b}"),
//                    Locked = true
//                },
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow
//            }, "admin@app");

//            if (res.Succeeded)
//            {
//                var user = userManager.FindByNameAsync("admin").Result;
//                await userManager.AddToRolesAsync(user, RoleNames);
//            }
//        }

//        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
//        {
//            foreach (var roleName in RoleNames)
//            {
//                var roleExist = await roleManager.RoleExistsAsync(roleName);
//                if (!roleExist) await roleManager.CreateAsync(new IdentityRole(roleName));
//            }
//        }

//        private static void SeedAppSettings(AppDbContext context)
//        {
//            var appSettings = new List<AppSetting>
//            {
//                new AppSetting {Name = ConfigKeys.SenderName, Value = "TIN PORTAL"},
//                new AppSetting {Name = ConfigKeys.ApiKey, Value = "NONE"}
//            };

//            foreach (var setting in appSettings.Where(p => !context.AppSettings.Any(x => x.Name == p.Name)))
//            {
//                context.AppSettings.Add(setting);
//            }
//        }

//        private static readonly string[] RoleNames = {
//            Privileges.UserCreate,
//            Privileges.UserUpdate,
//            Privileges.UserRead,
//            Privileges.UserDelete,

//            Privileges.RoleCreate,
//            Privileges.RoleUpdate,
//            Privileges.RoleRead,
//            Privileges.RoleDelete,

//            Privileges.Setting
//        };
//    }
//}
