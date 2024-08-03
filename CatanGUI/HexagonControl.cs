using Microsoft.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using Windows.Foundation;

namespace CatanGUI
{
    public sealed class HexagonControl : Control
    {
        private static readonly double SQRT_3 = Math.Sqrt(3.0);

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof(Brush),
            typeof(HexagonControl),
            new PropertyMetadata(new SolidColorBrush(Colors.Blue))
        );

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size",
            typeof(double),
            typeof(HexagonControl),
            new PropertyMetadata(0.0, OnSizeChanged)
        );

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (HexagonControl)d;
            control.InvalidateArrange();
        }

        public HexagonControl()
        {
            this.DefaultStyleKey = typeof(HexagonControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateHexagonPoints();
        }

        private void UpdateHexagonPoints()
        {
            var hexagon = GetTemplateChild("PART_Hexagon") as Polygon;

            if (hexagon != null)
            {
                hexagon.Points = CreateHexagonPoints(this.Size);
            }
        }

        private static PointCollection CreateHexagonPoints(double size)
        {
            var points = new PointCollection
            {
                new Point(0, size * SQRT_3 / 2),
                new Point(size / 2, 0),
                new Point(size * 3 / 2, 0),
                new Point(size * 2, size * SQRT_3 / 2),
                new Point(size * 3 / 2, size * SQRT_3),
                new Point(size / 2, size * SQRT_3)
            };

            return points;
        }
    }
}
