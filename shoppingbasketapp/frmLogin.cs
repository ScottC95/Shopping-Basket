using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShoppingBasketApp
{
    public partial class frmLogin : Form
    {
        public List<LoginData> logins { get; private set; }


        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            logins = new List<LoginData>();
            LoginData user = new LoginData("ABC", "123", false);
            logins.Add(user);
            LoginData admin = new LoginData("123", "ABC", true);
            logins.Add(admin);
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
