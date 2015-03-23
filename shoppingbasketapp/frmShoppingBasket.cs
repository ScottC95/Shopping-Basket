using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QA;
using System.Globalization;

namespace ShoppingBasketApp
{
    public partial class frmShoppingBasket : Form
    {
        private ShoppingBasket Basket = new ShoppingBasket();
        private int rowIndex = 0;
        private List<string> loaded = new List<string>();
        public bool admin;

        public frmShoppingBasket()
        {
            InitializeComponent();
        }

        private void frmShoppingBasket_Load(object sender, EventArgs e)
        {
            CultureInfo.CreateSpecificCulture("en-GB");
            if (!admin)
            {
                btnRemove.Enabled = false;
                btnRemoveQuantity.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                decimal price;
                decimal.TryParse(txtLatestPrice.Text, out price);
                if (price > 0.00m)
                {
                    if (nudQuantity.Value == 0)
                    {
                        Basket.AddProduct(txtProductName.Text, price);
                    }
                    else
                    {

                        Basket.AddProduct(txtProductName.Text, price, (int)nudQuantity.Value);
                    }
                }
                else
                {
                    MessageBox.Show("Latest Price should be a valid price");
                }
            }
            //RenderBasket();
            RenderDGV();

        }

        private void RenderDGV()
        {
            /*DataTable table = new DataTable();

            int columns = 4;

            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            foreach (var item in Basket.Products)
            {
                table.Rows.Add(item.ProductName);
                table.Rows.Add(item.Quantity);
                table.Rows.Add(item.LatestPrice);
                table.Rows.Add(item.TotalOrder);
            }
            */
            dgvBasket.DataSource = "";
            dgvBasket.DataSource = Basket.Products;
            FormatDGV();
            txtNumberOfItems.Text = Basket.NumberOfItems.ToString();
            txtTotal.Text = Basket.BasketTotal.ToString("C2");
        }

        private void FormatDGV()
        {
            dgvBasket.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvBasket.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvBasket.Columns[2].DefaultCellStyle.Format = "c";
            dgvBasket.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvBasket.Columns[3].DefaultCellStyle.Format = "c";
            dgvBasket.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvBasket.Columns[0].HeaderText = "Product Name";
            dgvBasket.Columns[2].HeaderText = "Latest Price";
            dgvBasket.Columns[3].HeaderText = "Total Price";
        }

        private bool Validation()
        {
            if (txtProductName.Text != "" && txtLatestPrice.Text != "")
            {
                return true;
            }
            return false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            /*if (lbBasket.SelectedIndex >= 0)
            {
                OrderItem item = Basket.Products[lbBasket.SelectedIndex];
                Basket.RemoveProduct(item.ProductName);
                RenderBasket();
            }*/

            if (dgvBasket.SelectedRows.Count > 0)
            {
                string itemName = dgvBasket.SelectedRows[0].Cells[0].Value.ToString();
                DialogResult ans = MessageBox.Show("Are you sure you want to remove from " + itemName, "Remove", MessageBoxButtons.YesNo);
                if (ans == DialogResult.Yes)
                {
                    Basket.RemoveProduct(itemName);
                }
            }
            else
            {
                MessageBox.Show("You need to select a product, click the Arrow next to the Product Name");
            }
            RenderDGV();
        }

        private void btnRemoveQuantity_Click(object sender, EventArgs e)
        {
            /*if (lbBasket.SelectedIndex >= 0)
            {
                OrderItem item = Basket.Products[lbBasket.SelectedIndex];
                frmRemoveQuantity remove = new frmRemoveQuantity();
                int quantity;
                if (remove.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(remove.Quantity, out quantity);
                    if (quantity >= 0)
                    {
                        Basket.RemoveProduct(item.ProductName, quantity);
                    }

                    RenderBasket();
                }
            } */
            if (dgvBasket.SelectedRows.Count > 0)
            {
                string itemName = dgvBasket.SelectedRows[0].Cells[0].Value.ToString();
                int quantity;
                frmRemoveQuantity remove = new frmRemoveQuantity();
                if (remove.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(remove.Quantity, out quantity);
                    if (quantity >= 0)
                    {
                        Basket.RemoveProduct(itemName, quantity);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a valid product, click the Arrow next to the Product Name");
            }
            RenderDGV();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBasket.SelectedRows.Count > 0)
            {
                string itemName = dgvBasket.SelectedRows[0].Cells[0].Value.ToString();
                int quantity;
                decimal price;
                frmEdit edit = new frmEdit();
                edit.ProductName = itemName;
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    quantity = edit.Quantity;
                    price = edit.Price;
                    if (quantity > 0 && price >= 0)
                    {
                        Basket.Edit(itemName, quantity, price);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select a valid product, click the Arrow next to the Product Name");
            }
            RenderDGV();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                Basket.ClearBasket();
                //RenderBasket();
                RenderDGV();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            MessageBox.Show("Select the folder you wish the txt document be saved to");
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                folderPath += "\\Basket.txt";
                Basket.SaveBasket(folderPath);
            }
        }

        private void dgvBasket_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dgvBasket.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;
                dgvBasket.CurrentCell = dgvBasket.Rows[e.RowIndex].Cells[1];
                this.cmsRemove.Show(dgvBasket, e.Location);
                cmsRemove.Show(Cursor.Position);
            }
            else if (e.Button == MouseButtons.Left && e.RowIndex != -1)
            {
                dgvBasket.Rows[e.RowIndex].Selected = true;
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.dgvBasket.Rows[this.rowIndex].IsNewRow)
            {
                Basket.Products.RemoveAt(this.rowIndex);
                RenderDGV();
            }
        }

        private void nudQuantity_Enter(object sender, EventArgs e)
        {
            nudQuantity.Select(0, nudQuantity.Text.Length);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            string file = "";
            OpenFileDialog browser = new OpenFileDialog();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                file = browser.FileName;
                if (PreviousFileLoaded(file))
                {
                    DialogResult ans = MessageBox.Show(string.Format("The file {0} has already been loaded before, are you sure you want to load again? (Products will be duplicated)", file), "Loading" ,MessageBoxButtons.YesNo);
                    if (ans == DialogResult.No)
                    {
                        return;
                    }
                }
                Basket.LoadBasket(file);
                RenderDGV();
                loaded.Add(file);
            }
        }

        private bool PreviousFileLoaded(string file)
        {
            for (int i = 0; i < loaded.Count; i++)
            {
                if (loaded[i] == file)
                {
                    return true;
                }
            }
            return false;
        }

        private void frmShoppingBasket_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application doesn't exit normally on close, so i force it. 
            System.Windows.Forms.Application.Exit();
        }
    }
}
