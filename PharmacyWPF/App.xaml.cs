using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PharmacyWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var alexandriaFont = new FontFamily("Alexandria");

            // مصفوفة بكل العناصر اللي بتستخدمها في صيدليتك وعايز خطها يتغير فوراً
            Type[] controlTypes = {
                typeof(Window),
                typeof(Page),
                typeof(TextBlock),
                typeof(TextBox),
                typeof(PlaceholderTextBox), // التكست بوكس الذكي بتاعك
                typeof(Button),
                typeof(DataGrid),
                typeof(Label),
                typeof(ComboBox)
            };

            // اللفة السحرية: بتعدي على عنصر عنصر وتصب عليه الخط صب
            foreach (var type in controlTypes)
            {
                Style implicitStyle = new Style(type);

                if (type == typeof(TextBlock))
                {
                    implicitStyle.Setters.Add(new Setter(TextBlock.FontFamilyProperty, alexandriaFont));
                }
                else
                {
                    implicitStyle.Setters.Add(new Setter(Control.FontFamilyProperty, alexandriaFont));
                }

                // إضافة الستايل للبرنامج بأعلى أولوية تكسر ستايل الويندوز
                this.Resources.Add(type, implicitStyle);
            }
        }
    }
}
