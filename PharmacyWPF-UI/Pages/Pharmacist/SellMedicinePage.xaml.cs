using System.Windows;
using System.Windows.Controls;

namespace PharmacyWPF_UI.Pages.Pharmacist
{
    public partial class SellMedicinePage : Page
    {
        public SellMedicinePage()
        {
            InitializeComponent();
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { }
        private void SearchResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void MedicineCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void UnitsBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void AddToCartBtn_Click(object sender, RoutedEventArgs e) { }
        private void ResetBtn_Click(object sender, RoutedEventArgs e) { }
        private void PurchaseBtn_Click(object sender, RoutedEventArgs e) { }
    }
}
