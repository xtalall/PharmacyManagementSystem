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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
        userMGMT1.Visible = false;
        }

        private void btnColes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUserMGMT_Click(object sender, EventArgs e)
        {
           userMGMT1.Visible = true;
        }
    }
}
