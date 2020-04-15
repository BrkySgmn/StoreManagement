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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Text = "Cari Açık";
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Admin Giriş";
        }
    }
}
