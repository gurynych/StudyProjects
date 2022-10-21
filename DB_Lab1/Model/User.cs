using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab1.Model
{
    public class User
    {        

        public string Login { get; private set; }

        public string PasswordHash { get; private set; }        

        //public string Role { get; private set; }

        public DateTime EntryTime { get; private set; }

        public static User ActiveUser { get; set; }        

        public User(string login, string password, DateTime entryTime)
        {
            Login = login;
            
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.Default.GetBytes(password);
                byte[] hash = md5.ComputeHash(bytes);
                PasswordHash = Encoding.Default.GetString(hash);
            }

            /*switch (role)
            {
                case UserRole.Admin:
                    {
                        Role = "Admin";
                        break;
                    }
                case UserRole.CommonUser:
                    {
                        Role = "Student";
                        break;
                    }
                default:
                    {
                        Role = "Unknown";
                        break;
                    }
            }*/

            EntryTime = entryTime;
        }        
    }

    public enum UserRole
    {
        Admin,
        CommonUser
    }
}
