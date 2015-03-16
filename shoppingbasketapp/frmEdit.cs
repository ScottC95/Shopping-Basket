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
    public partial class frmEdit : Form
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Quantity  < 0 || checkPrice() == false)
            {
                MessageBox.Show("Quantity or Latest price is incorrect");
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        public string ProductName
        {
            get
            {
                return txtProductName.Text;
            }
            set
            {
                txtProductName.Text = value;
            }
        }

        public int Quantity
        {
            get
            {
                return (int)nudQuantity.Value;
            }
        }

        public decimal Price
        {
            get
            {
                decimal i;
                decimal.TryParse(txtLatestPrice.Text, out i);
                return i;
            }
            set
            {
                txtLatestPrice.Text = value.ToString();
            }
        }

        private void nudQuantity_Enter(object sender, EventArgs e)
        {
            nudQuantity.Select(0, nudQuantity.Text.Length);
        }

        public bool checkPrice()
        {
            decimal i;
            decimal.TryParse(txtLatestPrice.Text, out i);
            if (i > 0)
            {
                return true;
            }
            return false;
        }

    }
}
