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

    public partial class UserMGMT : UserControl
    {
        public UserMGMT()
        {
            InitializeComponent();
        }
        private List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            string filePath = @"C:\Users\El Alamia\source\repos\PharmacyManagementSystem\Userinfo.txt";
            string[] line = File.ReadAllLines(filePath);
            foreach (string item in line)
            {
                string[] parts = item.Split(',');
                users.Add(new User
                {

                    FullName = parts[0],
                    ID = (parts[1]),
                    Password = parts[2],
                    Role = parts[3],
                    DateAdded = parts[4],
                    LastLogin = parts[5],
                    Status = parts[6]
                });
            }
            return users;
        }
        private void LoadUserData()
        {
            gridviewUsers.Rows.Clear();
            List<User> users = LoadUsers();
            foreach (User u in users)
            {
                gridviewUsers.Rows.Add(u.FullName, u.ID, u.Role, u.DateAdded, u.LastLogin, u.Status);
            }
        }


        private void UserMGMT_Load(object sender, EventArgs e)
        { LoadUserData();
            gridviewUsers.CellClick += (s, a) =>
            { if (a.ColumnIndex == gridviewUsers.Columns["btnRemove"].Index && a.RowIndex >= 0)
                { string selectedID = gridviewUsers.Rows[a.RowIndex].Cells["Column2"].Value.ToString();
                    string[] lines = File.ReadAllLines(@"C:\Users\El Alamia\source\repos\PharmacyManagementSystem\Userinfo.txt"); 
                    List<string> newLines = new List<string>();
                    foreach (string line in lines)
                    { string[] parts = line.Split(','); if (parts[1].Trim() != selectedID) newLines.Add(line); }
                    File.WriteAllLines(@"C:\Users\El Alamia\source\repos\PharmacyManagementSystem\Userinfo.txt", newLines); LoadUserData();
                    
                }
            };
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (textboxUserName.Text == "" || textUserPassword.Text == "" || txtUserPasswordC.Text == "" || txtboxUserID.Text == "" || comboboxRole.SelectedIndex == -1 )
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(textUserPassword.Text != txtUserPasswordC.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            User newUser = new User
            {
                FullName = textboxUserName.Text,
                ID = txtboxUserID.Text,
                Password = textUserPassword.Text,
                Role = comboboxRole.SelectedItem.ToString(),
                DateAdded = DateTime.Now.ToString("yyyy-MM-dd"),
                LastLogin = "",
                Status = "Active"
            };
            string filePath = @"C:\Users\El Alamia\source\repos\PharmacyManagementSystem\Userinfo.txt";
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine($"{newUser.FullName},{newUser.ID},{newUser.Password}," +
                                $"{newUser.Role},{newUser.DateAdded},{newUser.LastLogin},{newUser.Status}");
            }
            textboxUserName.Text="";
            txtboxUserID.Text="";
            textUserPassword.Text="";
            txtUserPasswordC.Text="";
            comboboxRole.SelectedIndex = -1;
            MessageBox.Show("User added successfully!");
            LoadUserData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search= txtboxSearchUser.Text.ToLower();
            List<User> users = LoadUsers();
            gridviewUsers.Rows.Clear();
            foreach (User u in users)
            {
                if (u.FullName.ToLower().Contains(search)||u.ID.ToString().Contains(search)||u.Role.ToLower().Contains(search))
                {
                    gridviewUsers.Rows.Add(u.FullName, u.ID, u.Role, u.DateAdded, u.LastLogin, u.Status);
                }
            }

        }
    }
    
}
