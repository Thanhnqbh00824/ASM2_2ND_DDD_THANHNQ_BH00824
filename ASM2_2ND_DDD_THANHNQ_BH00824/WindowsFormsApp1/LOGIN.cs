using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpass.Text;

            if (txtusername.Text == "admin" && txtpass.Text == "12345678")
            {
                Main main = new Main();
                main.Show();
                this.Hide();

            }
            else if (txtusername.Text == "teacher" && txtpass.Text == "123456789")
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else if (txtusername.Text == "student" && txtpass.Text == "012345678")
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Please try again.");
            }
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
