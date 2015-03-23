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
    public partial class frmRemoveQuantity : Form
    {
        public frmRemoveQuantity()
        {
            InitializeComponent();
        }

        public string Quantity
        {
            get { return txtQuantity.Text; }
            set { txtQuantity.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
