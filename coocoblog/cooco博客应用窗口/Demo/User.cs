using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    class User
    {
        public string userID;
        public string username;
        public string passwords;
        public string sex;

        public User(string Username, string Passwords,string Sex)
        {
            userID = Guid.NewGuid().ToString();
            username = Username;
            passwords = Passwords;
            sex = Sex;
        }
        public static string logname;
    }
}
