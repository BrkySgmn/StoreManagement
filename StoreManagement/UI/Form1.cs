using StoreManagement.DAL;
using System;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class Form1 : Form
    {
        public static int loggedInUserID;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Stok Takip";
            this.CenterToScreen();
            textBoxPassword.PasswordChar = '*';
        }
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (SearchUserWithName() != null)
            {
                User user = SearchUserWithName();
                if (textBoxUserName.Text == user.Name &&
                    textBoxPassword.Text == user.Password)
                {
                    MessageBox.Show("Başarılı Giriş Yapıldı", "Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form form2 = new Form2()
                    {
                        Text = "Stok Takip"
                    };
                    form2.ShowDialog();
                    this.Dispose();
                }
                else if (textBoxUserName.Text == null || textBoxPassword.Text == null)
                {
                    MessageBox.Show("Boş Alan Bırakılamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        User SearchUserWithName()
        {
            var users = HelperUser.GetUsers();
            foreach (var user in users)
            {
                if (user.Name == textBoxUserName.Text)
                {
                    loggedInUserID = user.ID;
                    return user;
                }
            }
            return null;
        }

        
    }
}
