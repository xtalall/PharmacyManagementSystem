using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyWinForms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnColes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textboxUserName_Click(object sender, EventArgs e)
        {
            textboxUserName.Clear();
        }

        private void textboxUserId_Click(object sender, EventArgs e)
        {
           textboxUserId.Clear();
        }

        private void textboxPassword_Click(object sender, EventArgs e)
        {
            textboxPassword.Clear();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            if(textboxUserName.Text == "admin" && textboxPassword.Text == "admin")
            {
                AdminDashboard adminDashboard = new AdminDashboard();
                adminDashboard.ShowDialog();
                this.Hide();
            }

            else if (textboxUserName.Text == "talal" && textboxPassword.Text == "talal")
            {
                Dashboard dashboard = new Dashboard();
                dashboard.ShowDialog();
            }

            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

    }
}
