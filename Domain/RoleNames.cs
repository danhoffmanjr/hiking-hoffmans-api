using System.ComponentModel;

namespace Domain
{
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string ProUser = "Pro User";
        public const string User = "User";

        public enum RoleNamesEnum
        {
            [Description("Admin")]
            Admin,
            [Description("Pro User")]
            ProUser,
            [Description("User")]
            User
        }
    }
}