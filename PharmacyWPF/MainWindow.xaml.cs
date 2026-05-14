using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PharmacyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Dashboard());
        }

        private void SetActiveButton(Button selectedButton)
        
        {
            //// الخطوة 1: نلف على كل العناصر اللي جوه الـ Grid ونخليها شفافة
            //if (NavbarGrid != null)
            //{
            //    foreach (var element in NavbarGrid.Children)
            //    {
            //        if (element is Button btn)
            //        {
            //            btn.Background = Brushes.Transparent;
            //        }
            //    }
            //}
            dashboard_btn.BorderThickness = new Thickness (0,0,0,0);
            sellMedicine_Btn.BorderThickness = new Thickness(0, 0, 0, 0);
            Medicine_Btn.BorderThickness = new Thickness(0, 0, 0, 0);
            // الخطوة 2: نلون الزرار اللي ضغطنا عليه (دي لازم تكون بَرّه الـ loop)
            if (selectedButton != null)
            {
                selectedButton.BorderThickness = new Thickness(0, 0, 0, 3);
            }
        }
       


        public void dashboard_btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Dashboard());
            SetActiveButton(sender as Button);
        }

        public void sellMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SellMedicine());
            SetActiveButton(sender as Button);
        }

        public void Medcine_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Medicine());
            SetActiveButton(sender as Button);
        }
    }
}
