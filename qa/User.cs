using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QA
{
    public class User
    {
        public string username { get; private set; }
        public string Password { get; private set; }
        public string email { get; private set; }

        public User(string username, string password, string email)
        {
            this.username = username;
            this.Password = password;
            this.email = email;
        }

        public User(string username, string password)
            : this(username, password, "Email not set")
        {
            this.username = username;
            this.Password = password;
        }

    }
}
