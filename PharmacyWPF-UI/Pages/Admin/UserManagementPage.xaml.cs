using System.Windows;
using System.Windows.Controls;

namespace PharmacyWPF_UI.Pages.Admin
{
    public partial class UserManagementPage : Page
    {
        public UserManagementPage()
        {
            InitializeComponent();
            RoleCombo.ItemsSource = new[] { "Pharmacist", "Admin" };
            RoleCombo.SelectedIndex = 0;
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e) { }
        private void ClearForm_Click(object sender, RoutedEventArgs e) { }
        private void EditUser_Click(object sender, RoutedEventArgs e) { }
        private void DeleteUser_Click(object sender, RoutedEventArgs e) { }
    }
}
