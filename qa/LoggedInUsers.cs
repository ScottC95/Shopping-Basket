using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QA
{
    class LoggedInUsers
    {
        public string UserID { get; set; }

        public string UserEmail { get; set; }

        public DateTime LoginDatetime { get; set; }

        public List<string> UserRoles { get; set; }

        public string SessionID { get; set; }

        public LoggedInUsers(string userId, string email = null, List<string> roles = null)
        {
            this.UserID = userId;
            this.UserEmail = email;
            this.UserRoles = roles;
            this.LoginDatetime = DateTime.Now;
            this.SessionID = Guid.NewGuid().ToString();
        }
    }
}
