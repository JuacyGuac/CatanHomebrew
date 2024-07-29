using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;

namespace CatanGUI
{
    public sealed class HexagonControl : Control
    {
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
            var boundingBox = GetTemplateChild("PART_BoundingBox") as Rectangle;

            if (hexagon != null && boundingBox != null)
            {
                boundingBox.Width = this.Width;
                boundingBox.Height = this.Height;

                hexagon.Points = CreateHexagonPoints(this.Width, this.Height);
            }
        }

        private PointCollection CreateHexagonPoints(double width, double height)
        {
            var points = new PointCollection();
            double size = Math.Min(width, height) / 2;
            double halfSize = size / 2;
            double heightOffset = Math.Sqrt(3) * size / 2;

            points.Add(new Windows.Foundation.Point(width / 2, (height - 2 * heightOffset) / 2));
            points.Add(new Windows.Foundation.Point((width + size) / 2, (height - heightOffset) / 2));
            points.Add(new Windows.Foundation.Point((width + size) / 2, (height + heightOffset) / 2));
            points.Add(new Windows.Foundation.Point(width / 2, (height + 2 * heightOffset) / 2));
            points.Add(new Windows.Foundation.Point((width - size) / 2, (height + heightOffset) / 2));
            points.Add(new Windows.Foundation.Point((width - size) / 2, (height - heightOffset) / 2));

            return points;
        }
    }
}
