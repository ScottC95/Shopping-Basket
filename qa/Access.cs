using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.Data;

namespace QA
{
    public class Access : IDataRepository
    {
        private string _filePath;

        private List<User> _allUsers;

        private OleDbConnection _conn = new OleDbConnection();
        private DataSet _dtasetLogin = new DataSet();
        private string _sqlQuery = "";
        private OleDbDataAdapter adapter;

        enum type
        {
            none,
            username = 1,
            password = 2,
            email = 3

        };


        public Access(string filePath)
        {
            if (File.Exists(filePath))
            {
                _filePath = filePath;
            }
            else
            {
                throw new FileNotFoundException(filePath + " does not exist!");
            }

            _allUsers = new List<User> {
                new User("user", "abc", "scott@gmail.com"),
                new User("admin", "123", "scottc@gmail.com")
            };

            
            _conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _filePath;
            _sqlQuery = "SELECT * FROM tblLogin";
            OleDbDataAdapter adapter = new OleDbDataAdapter(_sqlQuery, _conn);
            adapter.Fill(_dtasetLogin, "Login");

            foreach (DataRow dr in _dtasetLogin.Tables[0].Rows)
            {
                
            }

            string username, password, email;

            

            for (int i = 0; i < _dtasetLogin.Tables[0].Rows.Count ; i++)
			{
			    for (int n = 0; n < 3; n++)
			    {
                    switch (n)
                    {
                        case 0:
                            {
                                username = _dtasetLogin.Tables[0].Rows[i][n].ToString();
                                return;
                            }
                        case 1:
                            {
                                return;
                            }
                        case 2:
                            {
                                return;
                            }
                        default:
                            return;
              
                    } 
			    }
                //_allUsers.Add(new User(_dtasetLogin.Tables[0].Rows[i][n],
			}


        }

        #region IDataRepository Members

        public void ConnectToRepo()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _allUsers;
        }

        public User GetUserById(string userID)
        {
            return _allUsers.Where(u => u.username.Equals(userID)).SingleOrDefault();
        }

        public bool AddNewUser(string username, string password, string email)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf.GetBytes("ABCD4321"));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateEncryptor();
            password = BitConverter.ToString(trans.TransformFinalBlock(utf.GetBytes(password), 0, utf.GetBytes(password).Length));

            _allUsers.Add(new User(username, password, email));
            return true;
        }

        public bool UserExists(string userID, string email = null)
        {
            var user = _allUsers.Where(u => u.username.Equals(userID) && string.IsNullOrEmpty(email) ? true : u.email.Equals(email));
            return null == user;
        }

        public bool EmailExists(string userEmail)
        {
            var user = _allUsers.Where(e => e.email.Equals(userEmail)).FirstOrDefault();

            return (null != user && !string.IsNullOrEmpty(user.email));
        }

        public string GetPasswordFromEmailOrUserID(string userEmail, string userID = null)
        {
            var user = _allUsers.Where(g => g.email.Equals(userEmail)).FirstOrDefault();
            string password = user.Password;
            if (null != user && !string.IsNullOrEmpty(user.Password))
            {
                //Decryption of the password
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                UTF8Encoding utf = new UTF8Encoding();
                TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
                tDES.Key = md5.ComputeHash(utf.GetBytes("ABCD4321"));
                tDES.Mode = CipherMode.ECB;
                tDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform trans = tDES.CreateDecryptor();
                password = BitConverter.ToString(trans.TransformFinalBlock(utf.GetBytes(password), 0, utf.GetBytes(password).Length));
                return password;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

    }
}
