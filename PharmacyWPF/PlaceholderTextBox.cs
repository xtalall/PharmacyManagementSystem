using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PharmacyWPF
{
    public class PlaceholderTextBox : TextBox
    {
        public string Placeholder { get; set; }
        public CornerRadius CornerRadius { get; set; }
        public PlaceholderTextBox()
        {
            this.Loaded += (s, e) =>
            {
                if (string.IsNullOrEmpty(this.Text))
                    this.Text = Placeholder;

                var internalBorder = FindInternalBorder(this);
                if (internalBorder != null)
                { internalBorder.CornerRadius = this.CornerRadius; }
            };


            this.GotFocus += (s, e) =>
             {
                 if (this.IsReadOnly) return;
                 if (this.Text == Placeholder)
                 {
                     this.Text = string.Empty;
                 }
             };
            this.LostFocus += (s, e) =>
            {
                if (this.IsReadOnly) return;
                if (string.IsNullOrWhiteSpace(this.Text))
                {
                    this.Text = Placeholder;

                }
            };
        }



            private Border FindInternalBorder(DependencyObject parent)
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Border border)
                    return border; // أول ما تلاقيه رجعه فوراً

                var result = FindInternalBorder(child);
                if (result != null) return result;
            }
            return null;
        }

    }
}
