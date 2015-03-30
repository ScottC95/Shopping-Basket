using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA
{
    public interface IDataRepository
    {
        void ConnectToRepo();

        IEnumerable<User> GetAllUsers();

        User GetUserById(string userID);
        
        bool AddNewUser(string username, string password, string email);

        bool UserExists(string userID, string email = null);

        bool EmailExists(string userEmail);

        string GetPasswordFromEmailOrUserID(string userEmail, string userID = null);
    }
}
