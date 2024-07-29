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
                hexagon.Points = CreateHexagonPoints(this.Width, this.Height);
            }
        }

        private PointCollection CreateHexagonPoints(double width, double height)
        {
            var points = new PointCollection();
            
            double size = Math.Min(width / 2.0, height / SQRT_3);

            points.Add(new Point(0, size * SQRT_3 / 2));
            points.Add(new Point(size / 2, 0));
            points.Add(new Point(size * 3 / 2, 0));
            points.Add(new Point(size * 2, size * SQRT_3 / 2));
            points.Add(new Point(size * 3 / 2, size * SQRT_3));
            points.Add(new Point(size / 2, size * SQRT_3));

            return points;
        }
    }
}
