using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace QA
{
    public class Login
    {
        private IDataRepository _repository;
        private List<LoggedInUsers> _userSessions;
        public string _filePath;
        

        public Login()
        {
            //TODO: make _repository = access database + Put in Provider
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                _repository = new Access(openFile.FileName);
            }            
        }

        #region User

        public void AddNewUser(string username, string password, string email)
        {
            _repository.AddNewUser(username, password, email);
        }

        public bool UserExists(string username, string email)
        {
            return _repository.UserExists(username, email);
        }

        public bool EmailExists(string email)
        {
            return _repository.EmailExists(email);
        }

        public string GetPasswordFromEmailOrUserID(string userEmail, string userID = null)
        {
            return _repository.GetPasswordFromEmailOrUserID(userEmail, userID);
        }

        #endregion

        #region Session

        public string LoginUser(string userID, string password)
        {
            var user = _repository.GetUserById(userID);

            if (null != user)
            {
                LoggedInUsers userSession = new LoggedInUsers(userID);
                _userSessions.Add(userSession);
                return userSession.SessionID;
            }
            else
                return "Error: User does not exist!";
        }
        
        public bool CloseSession(string sessionID, string userID)
        {
            var userSession = _userSessions.Where(us => us.SessionID.Equals(sessionID) && us.UserID.Equals(userID)).SingleOrDefault();
            return _userSessions.Remove(userSession);
        }

        #endregion

    }

}
