using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariAcik
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadText();
        }
        void LoadText()
        {
            tabPage1.Text = "MÜŞTERİLER";
            tabPage2.Text = "SATIŞ YÖNETİMİ";
            tabPage3.Text = "RAPORLAR";
            tabPage4.Text = "ÜRÜN YÖNETİMİ";
            tabPage5.Text = "KATEGORİ YÖNETİMİ";
            tabPage6.Text = "KULLANICI AYARLARI";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Musteri m = new Musteri();
            m.Ad = textBox1.Text;
            m.Soyad = textBox2.Text;
            m.Telefon = textBox3.Text;
            MessageBox.Show("Yeni Kayıt İşlemi Başarılı", "Yeni Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Müşteri Bilgileri Düzenlendi", "Düzenleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show("Bu işlem geri alınamaz. Yine de silinsin mi?","Sil",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr==DialogResult.Yes)
            {

            }
        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
