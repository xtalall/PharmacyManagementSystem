using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PharmacyWPF
{
    public class RoundedButton : Button
    {
        public CornerRadius CornerRadius { get; set; }
        public Brush HoverBackground { get; set; } // خاصية اختيارية للون عند مرور الماوس

        private Border _internalBorder;

        public RoundedButton()
        {
            // القيم الافتراضية لو مكتبتهاش في الـ XAML
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66e0d9"));
            this.HoverBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33bdc2"));
            this.CornerRadius = new CornerRadius(15);
            this.Cursor = Cursors.Hand; // يخلي الماوس شكل إيد لما يقف على الزرار (حركة صايعة)

            this.Loaded += (s, e) =>
            {
                _internalBorder = FindInternalBorder(this);
                if (_internalBorder != null)
                {
                    _internalBorder.CornerRadius = this.CornerRadius;

                    // تثبيت لون خلفيتك الأصلي جوه الـ Border الداخلي
                    _internalBorder.Background = this.Background;
                }
            };

            // 2. التحكم في الألوان بالـ C# عشان نكسر تأثير الويندوز الافتراضي

            // لما الماوس يدخل على الزرار (Hover)
            this.MouseEnter += (s, e) =>
            {
                if (_internalBorder != null)
                {
                    // لو أنت محدد لون للـ Hover هيعرضه، لو مش محدد هيفضل ثابت على لونك الأصلي
                    _internalBorder.Background = HoverBackground ?? this.Background;
                }
            };

            // لما الماوس يخرج من الزرار
            this.MouseLeave += (s, e) =>
            {
                if (_internalBorder != null)
                {
                    _internalBorder.Background = this.Background; // يرجع للونك الأصلي
                }
            };

            // حماية إضافية عند الضغط والكليك عشان اللون ميتغيرش للأزرق الرخم
            this.PreviewMouseDown += (s, e) => { if (_internalBorder != null) _internalBorder.Background = HoverBackground ?? this.Background; };
            this.PreviewMouseUp += (s, e) => { if (_internalBorder != null) _internalBorder.Background = this.Background; };
        }

        // دالة التفتيش جوه الـ Visual Tree
        private Border FindInternalBorder(DependencyObject parent)
        {
            if (parent == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is Border border) return border;
                var result = FindInternalBorder(child);
                if (result != null) return result;
            }
            return null;
        }
    }
    }