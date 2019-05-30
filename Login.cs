using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Dice_Game
{
    class Login
    {
        public static void CheckUser(string user)
        {
            DataTable dtb = DB.GetUsers();
            dtb.DefaultView.RowFilter = "User ='" + user + "'";
            if(dtb.DefaultView.Count > 0)
            {
                Console.WriteLine("Password");
                var password = Console.ReadLine();
                if(CheckPass(user, password))
                {
                    Console.WriteLine("Welcome {0}", user);
                }
                else
                {
                    Console.WriteLine("The password was incorrect");
                    CheckUser(user);
             
                }
            }
            else
            {
                Console.WriteLine("Please create a password");
                var newPass = Console.ReadLine();
                // create salt and hash passwords
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var key = new Rfc2898DeriveBytes(newPass, salt, 1000);
                byte[] hash = key.GetBytes(20);
                byte[] saltHashBytes = new byte[36];
                Array.Copy(salt, 0, saltHashBytes, 0, 16);
                Array.Copy(hash, 0, saltHashBytes, 16, 20);
                string passwordHash = Convert.ToBase64String(saltHashBytes);
                DB.NewUser(user, passwordHash);
            }
        }
        public static bool CheckPass(string user, string pass)
        {
            // get user hashed pass from Data Base
            string savedPass = DB.GetHash(user);
            // get bytes from string
            byte[] hashBytes = Convert.FromBase64String(savedPass);
            // pull out the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            // use entered pass and salt to make key 
            var key = new Rfc2898DeriveBytes(pass, salt, 1000);
            byte[] hash = key.GetBytes(20);
            // Compare the hash
            for(int i = 0; i<20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
