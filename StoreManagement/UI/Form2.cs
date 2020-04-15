using StoreManagement.BLL;
using StoreManagement.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class Form2 : Form
    {
        Customer customer = new Customer();
        Category category = new Category();
        User user = new User();
        Product product = new Product();
        List<Sale> sales = new List<Sale>();
        List<Product> allProducts = new List<Product>();
        List<Product> productsInCategory = new List<Product>();
        List<Customer> customers = new List<Customer>();
        List<Category> categories = new List<Category>();

        public Form2()
        {
            InitializeComponent();
            RefreshSalePage();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            dateTimePickStart.Value = DateTime.Today;
            dateTimePickStart.MaxDate = DateTime.Today;
            txtBoxOldPassword.PasswordChar = '*';
            txtBoxNewPassword.PasswordChar = '*';
            txtBoxNewPassword2.PasswordChar = '*';
        }
        void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageSale)
            {
                RefreshSalePage();
            }
            else if (tabControl1.SelectedTab == tabPageProfitReport)
            {
                RefreshReportPage();
            }
            else if (tabControl1.SelectedTab == tabPageCustomer)
            {
                RefreshCustomerPage();
            }

            else if (tabControl1.SelectedTab == tabPageProduct)
            {
                RefreshProductPage();

            }
            else if (tabControl1.SelectedTab == tabPageCategory)
            {
                RefreshCategoryPage();
            }
            else if (tabControl1.SelectedTab == tabPageUser)
            {
                RefreshUserPage();
            }
        }
        private void ButtonSelectCustomer_Click(object sender, EventArgs e)
        {
            if (comboBoxCustomerID.SelectedItem != null)
            {
                var selectedCustomer = HelperCustomer.GetCustomer((int)comboBoxCustomerID.SelectedItem);
                txtBoxUpdateCustomerName.Text = selectedCustomer.Name;
                txtBoxUpdateCustomerSurname.Text = selectedCustomer.Surname;
                txtBoxUpdateCustomerTelephone.Text = selectedCustomer.PhoneNumber;
                txtBoxUpdateCustomerAddress.Text = selectedCustomer.Address;
                ButtonUpdateCustomer.Enabled = true;
            }
        }
        private void ButtonSelectProduct_Click(object sender, EventArgs e)
        {
            if (comboBoxProductID.SelectedItem != null)
            {
                var selectedProduct = HelperProduct.GetProduct((int)comboBoxProductID.SelectedItem);
                txtBoxUpdateProductName.Text = selectedProduct.Name;
                comboBoxUpdateProductCategory.SelectedItem = HelperCategory.GetCategoryFromProduct(selectedProduct).Name;
                txtBoxUpdatePurchasePrice.Text = selectedProduct.PurchasePrice.ToString();
                txtBoxUpdateSalePrice.Text = selectedProduct.SalePrice.ToString();
                numericUpDownUpdateProductStock.Value = (int)selectedProduct.Stock;
                txtBoxUpdateProductExplanation.Text = selectedProduct.Explanation;
                ButtonUpdateProduct.Enabled = true;
                numericUpDownUpdateProductStock.Enabled = true;
            }
        }
        private void ButtonSelectCategory_Click(object sender, EventArgs e)
        {
            if (comboBoxCategoryID.SelectedItem != null)
            {
                var selectedCategory = HelperCategory.GetCategory((int)comboBoxCategoryID.SelectedItem);
                txtBoxUpdateCategoryName.Text = selectedCategory.Name;
                txtBoxUpdateCategoryExplanation.Text = selectedCategory.Explanation;
                ButtonUpdateCategory.Enabled = true;
            }

        }
        private void ButtonAddCustomer_Click(object sender, EventArgs e)
        {
            if (txtBoxAddCustomerName.Text != "")
            {
                string customerName = char.ToUpper(txtBoxAddCustomerName.Text[0]) + txtBoxAddCustomerName.Text.Substring(1);
                customer.Name = customerName;
            }
            else
            {
                customer.Name = "";
            }
            if (txtBoxAddCustomerSurname.Text != "")
            {
                string customerSurname = char.ToUpper(txtBoxAddCustomerSurname.Text[0]) + txtBoxAddCustomerSurname.Text.Substring(1);
                customer.Surname = customerSurname;
            }
            else
            {
                customer.Surname = "";
            }
            customer.PhoneNumber = txtBoxAddCustomerTelephone.Text;
            customer.Address = txtBoxAddCustomerAddress.Text;
            if (HelperCustomer.CRUDCustomer(customer, EntityState.Added))
            {
                MessageBox.Show("Müşteri Kaydı Yapıldı", "Müşteri Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCustomerPage();
            }
            else
                MessageBox.Show("Müşteri Kayıt Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonAddProduct_Click(object sender, EventArgs e)
        {
            if (txtBoxAddProductName != null)
            {
                string productName = char.ToUpper(txtBoxAddProductName.Text[0]) + txtBoxAddProductName.Text.Substring(1);
                product.Name = productName;
            }
            else
            {
                product.Name = "";
            }
            product.CategoryID = HelperCategory.GetCategory(comboBoxAddProductCategory.SelectedItem.ToString()).ID;
            product.PurchasePrice = Convert.ToDecimal(txtBoxAddPurchasePrice.Text.Replace(".", ","));
            product.SalePrice = Convert.ToDecimal(txtBoxAddSalePrice.Text.Replace(".", ","));
            product.Stock = Convert.ToInt32(numericUpDownAddProductStock.Value);
            product.Explanation = txtBoxAddProductExplanation.Text;
            if (HelperProduct.CRUDProduct(product, EntityState.Added))
            {
                MessageBox.Show("Ürün Başarıyla Eklendi", "Ürün Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshProductPage();
            }
            else
                MessageBox.Show("Ürün Eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonAddCategory_Click(object sender, EventArgs e)
        {
            if (txtBoxAddCategoryName.Text != "")
            {
                string categoryName = char.ToUpper(txtBoxAddCategoryName.Text[0]) + txtBoxAddCategoryName.Text.Substring(1);
                category.Name = categoryName;
            }
            else
            {
                category.Name = "";
            }
            category.Explanation = txtBoxAddCategoryExplanation.Text;
            if (HelperCategory.CRUDCategory(category, EntityState.Added))
            {
                MessageBox.Show("Yeni Kategori Eklendi", "Kategori Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCategoryPage();
            }
            else
                MessageBox.Show("Kategori Eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (txtBoxUpdateCustomerName.Text != "")
            {
                string customerName = char.ToUpper(txtBoxUpdateCustomerName.Text[0]) + txtBoxUpdateCustomerName.Text.Substring(1);
                customer.Name = customerName;
            }
            else
            {
                customer.Name = "";
            }
            if (txtBoxUpdateCustomerSurname.Text != "")
            {
                string customerSurname = char.ToUpper(txtBoxUpdateCustomerSurname.Text[0]) + txtBoxUpdateCustomerSurname.Text.Substring(1);
                customer.Surname = customerSurname;
            }
            else
            {
                customer.Surname = "";
            }
            customer.ID = HelperCustomer.GetCustomer((int)comboBoxCustomerID.SelectedItem).ID;
            customer.PhoneNumber = txtBoxUpdateCustomerTelephone.Text;
            customer.Address = txtBoxUpdateCustomerAddress.Text;
            if (HelperCustomer.CRUDCustomer(customer, EntityState.Modified))
            {
                MessageBox.Show("Müşteri Bilgileri Güncellendi", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCustomerPage();
            }
            else
                MessageBox.Show("Müşteri Bilgileri Güncellenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonUpdateProduct_Click(object sender, EventArgs e)
        {
            if (txtBoxUpdateProductName.Text != "")
            {
                string productName = char.ToUpper(txtBoxUpdateProductName.Text[0]) + txtBoxUpdateProductName.Text.Substring(1);
                product.Name = productName;
            }
            else
            {
                product.Name = "";
            }
            product.ID = allProducts[comboBoxProductID.SelectedIndex].ID;
            product.Explanation = txtBoxUpdateProductExplanation.Text;
            product.PurchasePrice = Convert.ToDecimal(txtBoxUpdatePurchasePrice.Text.Replace(".", ","));
            product.SalePrice = Convert.ToDecimal(txtBoxUpdateSalePrice.Text.Replace(".", ","));
            product.Stock = (int)numericUpDownUpdateProductStock.Value;
            product.CategoryID = categories[comboBoxUpdateProductCategory.SelectedIndex].ID;
            if (HelperProduct.CRUDProduct(product, EntityState.Modified))
            {
                MessageBox.Show("Ürün Bilgileri Güncellendi", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshProductPage();
            }
            else
                MessageBox.Show("Ürün Bilgileri Güncellenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonUpdateCategory_Click(object sender, EventArgs e)
        {
            if (txtBoxUpdateCategoryName.Text != "")
            {
                string categoryName = char.ToUpper(txtBoxUpdateCategoryName.Text[0]) + txtBoxUpdateCategoryName.Text.Substring(1);
                category.Name = categoryName;
            }
            else
            {
                category.Name = "";
            }
            category.ID = (int)comboBoxCategoryID.SelectedItem;
            category.Explanation = txtBoxUpdateCategoryExplanation.Text;
            if (HelperCategory.CRUDCategory(category, EntityState.Modified))
            {
                MessageBox.Show("Kategori Bilgileri Güncellendi", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCategoryPage();
            }
            else
                MessageBox.Show("Kategori Bilgileri Güncellenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonDeleteCustomer_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bu işlem geri alınamaz. Yine de silinsin mi?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                var selectedCustomer = customers[comboBoxCustomerID.SelectedIndex];
                if (HelperCustomer.CRUDCustomer(selectedCustomer, EntityState.Deleted))
                {
                    MessageBox.Show("Silme İşlemi Başarılı", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshCustomerPage();
                }
                else
                    MessageBox.Show("Silme İşlemi Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonDeleteProduct_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bu işlem geri alınamaz. Yine de silinsin mi?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                var selectedProduct = allProducts[comboBoxProductID.SelectedIndex];
                if (HelperProduct.CRUDProduct(selectedProduct, EntityState.Deleted))
                {
                    MessageBox.Show("Silme İşlemi Başarılı", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshProductPage();
                }
                else
                    MessageBox.Show("Silme İşlemi Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonDeleteCategory_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bu işlem geri alınamaz. Yine de silinsin mi?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                var selectedCategory = categories[comboBoxCategoryID.SelectedIndex];
                if (HelperCategory.CRUDCategory(selectedCategory, EntityState.Deleted))
                {
                    MessageBox.Show("Silme İşlemi Başarılı", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshCategoryPage();
                }
                else
                    MessageBox.Show("Silme İşlemi Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonChangePassword_Click(object sender, EventArgs e)
        {
            user = HelperUser.GetUser(Form1.loggedInUserID);
            if (user.Password == txtBoxOldPassword.Text)
            {
                if (txtBoxNewPassword.Text == txtBoxNewPassword2.Text)
                {
                    user.Password = txtBoxNewPassword2.Text;
                    if (HelperUser.ChangeUserPassword(user))
                        MessageBox.Show("Şifreniz Başarıyla Değiştirildi", "Şifre Değişimi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Şifreler Aynı Olmalıdır", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Eski Şifreyi Hatalı Girdiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonAddToBasket_Click(object sender, EventArgs e)
        {
            if (numericUpDownSaleNumber.Value != 0)
            {
                var selectedProduct = allProducts[(comboBoxSaleProduct.SelectedIndex)];
                var selectedCustomer = categories[(comboBoxSaleCustomer.SelectedIndex)];
                Sale basketItem = new Sale()
                {
                    ProductID = selectedProduct.ID,
                    CustomerID = selectedCustomer.ID,
                    NumberOfProducts = (int)numericUpDownSaleNumber.Value
                };
                sales.Add(basketItem);
                var basketRowItems = new string[]
                {
                selectedProduct.Name,
                numericUpDownSaleNumber.Value.ToString(),
                selectedProduct.SalePrice.ToString()
                };
                var listviewItem = new ListViewItem(basketRowItems);
                listViewBasket.Items.Add(listviewItem);
                listViewBasket.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                MessageBox.Show("Sepete Ürün Ekleme Başarılı", "Sepet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ButtonSale.Enabled == false)
                    ButtonSale.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ürün Adeti Sıfırdan Farklı Olmalıdır", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonSale_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Ürünlerin satışı gerçekleştirilsin mi?", "Satış", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            int saleCount = 0;
            decimal totalSale = 0;
            if (dr == DialogResult.Yes)
            {
                foreach (var sale in sales)
                {
                    var product = HelperProduct.GetProduct((int)sale.ProductID);
                    var customer = HelperCustomer.GetCustomer((int)sale.CustomerID);
                    sale.RegistrationStatus = (byte)RegistrationStatus.Active;
                    sale.InstantPurchasePrice = product.PurchasePrice;
                    sale.InstantSalePrice = product.SalePrice;
                    sale.ProductID = product.ID;
                    sale.DateOfSale = DateTime.Now;
                    sale.CustomerID = customer.ID;
                    if (HelperSale.CRUDSale(sale, EntityState.Added) && HelperProduct.DecreaseFromStock((int)sale.ProductID, (int)sale.NumberOfProducts))
                    {
                        saleCount++;
                        totalSale += (decimal)(product.SalePrice * sale.NumberOfProducts);
                    }
                }
                if (saleCount == sales.Count)
                {
                    MessageBox.Show("Satış Gerçekleştirildi", "Satış", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listViewBasket.Items.Clear();
                    labelBasketPrice.Text = "Toplam:" + totalSale.ToString() + " " + "TL";
                    labelBasketPrice.Visible = true;
                }
                else
                    MessageBox.Show("Satış Gerçekleştirilemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonListSales_Click(object sender, EventArgs e)
        {
            int rowCounter = 0;
            decimal saleProfits = 0;
            dataGridReport.Rows.Clear();
            var sales = HelperSale.GetSalesBetweenSelectedDates(dateTimePickStart.Value, dateTimePickEnd.Value);
            foreach (var sale in sales)
            {
                dataGridReport.Rows.Add();
                dataGridReport.Rows[rowCounter].Cells[0].Value = HelperCustomer.GetCustomer((int)sale.CustomerID).Name + " " + HelperCustomer.GetCustomer((int)sale.CustomerID).Surname;
                dataGridReport.Rows[rowCounter].Cells[1].Value = HelperProduct.GetProduct((int)sale.ProductID).Name;
                dataGridReport.Rows[rowCounter].Cells[2].Value = sale.NumberOfProducts;
                dataGridReport.Rows[rowCounter].Cells[3].Value = HelperProduct.GetProduct((int)sale.ProductID).SalePrice;
                dataGridReport.Rows[rowCounter].Cells[4].Value = sale.DateOfSale;
                rowCounter++;
                saleProfits += (decimal)((sale.InstantSalePrice - sale.InstantPurchasePrice)* sale.NumberOfProducts);
            }
            labelProfit.Text = saleProfits.ToString() + " TL";
            groupBoxProfit.Visible = true;
        }
        private void comboBoxCustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCustomerID.SelectedItem != null)
            {
                ButtonSelectCustomer.Enabled = true;
                ButtonDeleteCustomer.Enabled = true;
                txtBoxUpdateCustomerName.Enabled = true;
                txtBoxUpdateCustomerSurname.Enabled = true;
                txtBoxUpdateCustomerTelephone.Enabled = true;
                txtBoxUpdateCustomerAddress.Enabled = true;
            }
        }
        private void comboBoxProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProductID.SelectedItem != null)
            {
                ButtonSelectProduct.Enabled = true;
                ButtonDeleteProduct.Enabled = true;
                numericUpDownAddProductStock.Enabled = false;
            }
        }
        private void comboBoxCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategoryID.SelectedItem != null)
            {
                ButtonSelectCategory.Enabled = true;
                ButtonDeleteCategory.Enabled = true;
            }
        }
        private void comboBoxCategoryForSale_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshForSale();
        }
        private void txtBoxAddPurchasePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
        private void txtBoxAddSalePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
        private void txtBoxUpdatePurchasePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
        private void txtBoxUpdateSalePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
        private void txtBoxOldPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxOldPassword.TextLength != 0)
            {
                txtBoxNewPassword.Enabled = true;
                txtBoxNewPassword2.Enabled = true;
                ButtonChangePassword.Enabled = true;
            }
            else
            {
                txtBoxNewPassword.Enabled = false;
                txtBoxNewPassword2.Enabled = false;
                ButtonChangePassword.Enabled = false;
            }
        }
        private void RefreshForSale()
        {
            comboBoxSaleProduct.Items.Clear();
            dataGridSaleList.Rows.Clear();
            numericUpDownSaleNumber.Minimum = 0;
            if (comboBoxCategoryForSale.Items.Count == 0)
            {
                allProducts = HelperProduct.GetProducts();
                categories = HelperCategory.GetGategories();
                foreach (var category in categories)
                {
                    comboBoxCategoryForSale.Items.Add(category.Name);
                }
            }
            if (comboBoxCategoryForSale.SelectedItem != null)
            {
                ButtonAddToBasket.Enabled = true;
                comboBoxSaleProduct.Enabled = true;
                comboBoxSaleCustomer.Enabled = true;
                numericUpDownSaleNumber.Enabled = true;
                listViewBasket.Visible = true;
                ButtonSale.Visible = true;
                var selectedCategory = categories[comboBoxCategoryForSale.SelectedIndex];
                productsInCategory = HelperProduct.GetProductsFromCategory(selectedCategory.ID);
                int rowCounter = 0;
                foreach (var product in productsInCategory)
                {
                    dataGridSaleList.Rows.Add();
                    dataGridSaleList.Rows[rowCounter].Cells[0].Value = product.Name;
                    dataGridSaleList.Rows[rowCounter].Cells[1].Value = product.SalePrice;
                    dataGridSaleList.Rows[rowCounter].Cells[2].Value = product.Explanation;
                    comboBoxSaleProduct.Items.Add(product.Name);
                    rowCounter++;
                }
                if (comboBoxSaleCustomer.Items.Count == 0)
                {
                    customers = HelperCustomer.GetCustomers();
                    foreach (var customer in customers)
                    {
                        comboBoxSaleCustomer.Items.Add(customer.Name + " " + customer.Surname);
                    }
                }
            }
        }
        private void RefreshSalePage()
        {
            sales.Clear();
            listViewBasket.Items.Clear();
            labelBasketPrice.Visible = false;
            listViewBasket.Visible = false;
            ButtonSale.Visible = false;
            comboBoxSaleCustomer.Items.Clear();
            comboBoxCategoryForSale.SelectedItem = null;
            comboBoxSaleCustomer.SelectedItem = null;
            comboBoxSaleProduct.SelectedItem = null;
            RefreshForSale();
            labelSaleInfo1.Visible = true;
            labelSaleInfo2.Visible = false;
            labelSaleInfo3.Visible = false;
            ButtonAddToBasket.Enabled = false;
            ButtonSale.Enabled = false;
            comboBoxSaleProduct.Enabled = false;
            comboBoxSaleCustomer.Enabled = false;
            numericUpDownSaleNumber.Enabled = false;
        }
        private void RefreshReportPage()
        {
            customers = HelperCustomer.GetCustomers();
            allProducts = HelperProduct.GetProducts();
            dateTimePickEnd.MaxDate = DateTime.Now;
            groupBoxProfit.Visible = false;
            labelProfit.Text = null;
            dataGridReport.Rows.Clear();
        }
        private void RefreshCustomerPage()
        {
            comboBoxCustomerID.SelectedItem = null;
            customers.Clear();
            dataGridCustomerList.Rows.Clear();
            comboBoxCustomerID.Items.Clear();
            txtBoxAddCustomerAddress.Clear();
            txtBoxAddCustomerName.Clear();
            txtBoxAddCustomerSurname.Clear();
            txtBoxAddCustomerTelephone.Clear();
            txtBoxUpdateCustomerAddress.Clear();
            txtBoxUpdateCustomerName.Clear();
            txtBoxUpdateCustomerSurname.Clear();
            txtBoxUpdateCustomerTelephone.Clear();
            ButtonSelectCustomer.Enabled = false;
            ButtonUpdateCustomer.Enabled = false;
            ButtonDeleteCustomer.Enabled = false;
            txtBoxUpdateCustomerName.Enabled = false;
            txtBoxUpdateCustomerSurname.Enabled = false;
            txtBoxUpdateCustomerTelephone.Enabled = false;
            txtBoxUpdateCustomerAddress.Enabled = false;
            int rowCounter = 0;
            customers = HelperCustomer.GetCustomers();
            foreach (var customer in customers)
            {
                comboBoxCustomerID.Items.Add(customer.ID);
                dataGridCustomerList.Rows.Add();
                dataGridCustomerList.Rows[rowCounter].Cells[0].Value = customer.ID;
                dataGridCustomerList.Rows[rowCounter].Cells[1].Value = customer.Name;
                dataGridCustomerList.Rows[rowCounter].Cells[2].Value = customer.Surname;
                dataGridCustomerList.Rows[rowCounter].Cells[3].Value = customer.PhoneNumber;
                dataGridCustomerList.Rows[rowCounter].Cells[4].Value = customer.Address;
                rowCounter++;
            }
        }
        private void RefreshProductPage()
        {
            allProducts.Clear();
            dataGridProductList.Rows.Clear();
            comboBoxProductID.Items.Clear();
            comboBoxAddProductCategory.Items.Clear();
            comboBoxUpdateProductCategory.Items.Clear();
            txtBoxAddProductName.Clear();
            txtBoxAddProductExplanation.Clear();
            txtBoxAddPurchasePrice.Clear();
            txtBoxAddSalePrice.Clear();
            txtBoxUpdateProductName.Clear();
            txtBoxUpdateProductExplanation.Clear();
            txtBoxUpdatePurchasePrice.Clear();
            txtBoxUpdateSalePrice.Clear();
            numericUpDownAddProductStock.Value = 1;
            numericUpDownUpdateProductStock.Value = 0;
            ButtonSelectProduct.Enabled = false;
            ButtonDeleteProduct.Enabled = false;
            ButtonUpdateProduct.Enabled = false;
            numericUpDownUpdateProductStock.Enabled = false;
            comboBoxProductID.SelectedItem = null;
            comboBoxAddProductCategory.SelectedItem = null;
            int rowCounter = 0;
            allProducts = HelperProduct.GetProducts();
            foreach (var product in allProducts)
            {
                comboBoxProductID.Items.Add(product.ID);
                dataGridProductList.Rows.Add();
                dataGridProductList.Rows[rowCounter].Cells[0].Value = product.ID;
                dataGridProductList.Rows[rowCounter].Cells[1].Value = product.Name;
                dataGridProductList.Rows[rowCounter].Cells[2].Value = HelperCategory.GetCategory((int)product.CategoryID).Name;
                dataGridProductList.Rows[rowCounter].Cells[3].Value = product.PurchasePrice;
                dataGridProductList.Rows[rowCounter].Cells[4].Value = product.SalePrice;
                dataGridProductList.Rows[rowCounter].Cells[5].Value = product.Stock;
                dataGridProductList.Rows[rowCounter].Cells[6].Value = product.Explanation;
                rowCounter++;
            }
            foreach (var category in categories)
            {
                comboBoxAddProductCategory.Items.Add(category.Name);
                comboBoxUpdateProductCategory.Items.Add(category.Name);
            }
        }
        private void RefreshCategoryPage()
        {
            categories.Clear();
            dataGridCategoryList.Rows.Clear();
            ButtonSelectCategory.Enabled = false;
            ButtonDeleteCategory.Enabled = false;
            ButtonUpdateCategory.Enabled = false;
            comboBoxCategoryID.SelectedItem = null;
            comboBoxCategoryID.Items.Clear();
            txtBoxAddCategoryName.Clear();
            txtBoxAddCategoryExplanation.Clear();
            txtBoxUpdateCategoryName.Clear();
            txtBoxUpdateCategoryExplanation.Clear();
            categories = HelperCategory.GetGategories();
            int rowCounter = 0;
            foreach (var category in categories)
            {
                comboBoxCategoryID.Items.Add(category.ID);
                dataGridCategoryList.Rows.Add();
                dataGridCategoryList.Rows[rowCounter].Cells[0].Value = category.ID;
                dataGridCategoryList.Rows[rowCounter].Cells[1].Value = category.Name;
                dataGridCategoryList.Rows[rowCounter].Cells[2].Value = category.Explanation;
                rowCounter++;
            }
        }
        private void RefreshUserPage()
        {
            ButtonChangePassword.Enabled = false;
            txtBoxNewPassword.Enabled = false;
            txtBoxNewPassword2.Enabled = false;
            txtBoxOldPassword.Clear();
            txtBoxNewPassword.Clear();
            txtBoxNewPassword2.Clear();
        }
    }
}
