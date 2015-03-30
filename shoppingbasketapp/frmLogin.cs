using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ShoppingBasketApp
{
    public partial class frmLogin : Form
    {
        public List<LoginData> logins { get; private set; }


        public frmLogin()
        {
            InitializeComponent();
        }
        byte[] encrypted;
        private void frmLogin_Load(object sender, EventArgs e)
        {
            logins = new List<LoginData>();
            LoginData user = new LoginData("ABC", "123", false);
            logins.Add(user);
            LoginData admin = new LoginData("123", "ABC", true);
            logins.Add(admin);

            ////
            string password = "ABC";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf.GetBytes("ABCD4321"));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateEncryptor();
            encrypted = trans.TransformFinalBlock(utf.GetBytes(password), 0, utf.GetBytes(password).Length);
            password = utf.GetString(encrypted);
            MessageBox.Show(password);
            string s = decrypt();
            MessageBox.Show(s);
            
        }

        public string decrypt()
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf.GetBytes("ABCD4321"));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateDecryptor();
            string pass = utf.GetString(trans.TransformFinalBlock(encrypted, 0, encrypted.Length));
            return pass;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < logins.Count; i++)
            {
                if (txtUsername.Text == logins[i].username && txtPassword.Text == logins[i].password)
                {
                    frmShoppingBasket basket = new frmShoppingBasket();
                    basket.admin = logins[i].admin;
                    basket.Show();
                    this.Hide();
                }
            }
        }

    }
            
    public class LoginData
    {
        public string username { get; private set; }
        public string password { get; private set; }
        public bool admin { get; private set; }

        public LoginData(string newUsername, string newPassword, bool newAdmin)
        {
            this.username = newUsername;
            this.password = newPassword;
            this.admin = newAdmin;
        }
    }
}
