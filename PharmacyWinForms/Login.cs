using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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



        private void btnSignin_Click(object sender, EventArgs e)
        {
            string usernaeme = textboxUserName.Text.Trim();
            string id = textboxUserId.Text.Trim();
            string password = textboxPassword.Text.Trim();
            string path = @"C:\Users\El Alamia\source\repos\PharmacyManagementSystem\Userinfo.txt";

            if (!File.Exists(path))
            {
                MessageBox.Show("User Info File not found.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length < 4) continue;

                    {
                        if (
                            parts[0].Trim() == usernaeme &&
                            parts[1].Trim() == id &&
                            parts[2].Trim() == password
                            )
                        {
                            string role = parts[3].Trim().ToLower();

                            if (role == "admin")
                            {
                                AdminDashboard adminDashboard = new AdminDashboard();
                                adminDashboard.Show();
                                this.Hide();
                                return;
                            }
                            else if (role == "pharmacist")
                            {
                                Dashboard dashboard = new Dashboard();
                                dashboard.Show();
                                this.Hide();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Unknown user role.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }  
            MessageBox.Show("Invalid username, ID, or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
      
    
}
    