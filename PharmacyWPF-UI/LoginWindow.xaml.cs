using PharmacyWPF_UI.Controls;
using System.Windows;
using System.Windows.Input;

namespace PharmacyWPF_UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            UsernameBox.Focus();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e) => AttemptLogin();

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AttemptLogin();
        }

        private void AttemptLogin()
        {
            ErrorText.Visibility = Visibility.Collapsed;

            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            if (username == "admin" && password == "admin123")
            {
                OpenMain(SidebarRole.Admin, "A");
                return;
            }

            if (username == "t" && password == "t123")
            {
                OpenMain(SidebarRole.Pharmacist, "P");
                return;
            }

            ErrorText.Text = "Invalid username or password.";
            ErrorText.Visibility = Visibility.Visible;
            PasswordBox.Clear();
            PasswordBox.Focus();
        }

        private void OpenMain(SidebarRole role, string userInitial)
        {
            var main = new MainWindow(role, userInitial);
            main.Show();
            Close();
        }
    }
}
