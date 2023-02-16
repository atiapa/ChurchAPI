using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Data
{
    public class Privileges
    {
        //Permissions on the User Model
        public const string UserCreate = "User.Create";
        public const string UserUpdate = "User.Update";
        public const string UserRead = "User.Read";
        public const string UserDelete = "User.Delete";

        //Permissions on the Role
        public const string RoleCreate = "Role.Create";
        public const string RoleUpdate = "Role.Update";
        public const string RoleRead = "Role.Read";
        public const string RoleDelete = "Role.Delete";

        //Permissions on the Bank
        public const string BankCreate = "Bank.Create";
        public const string BankUpdate = "Bank.Update";
        public const string BankRead = "Bank.Read";
        public const string BankDelete = "Bank.Delete";

        //Permissions on systems configuration
        public const string Setting = "Setting";
    }

    public class ConfigKeys
    {
        public static string SenderName = "SenderName";
        public static string ApiKey = "ApiKey";
        public static string SplitCharacter = "§";
    }
}
