using CatanLibrary;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Point = Windows.Foundation.Point;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CatanGUI
{
    public class BoardControlBase<PointImpl> : Control where PointImpl : IPoint<PointImpl>
    {
        private static readonly double SQRT_3 = Math.Sqrt(3.0);

        public static readonly DependencyProperty BoardProperty = DependencyProperty.Register(
            "Board",
            typeof(IBoard<PointImpl>),
            typeof(BoardControlBase<PointImpl>),
            new PropertyMetadata(null, OnBoardChanged)
        );

        public IBoard<PointImpl> Board
        {
            get { return (IBoard<PointImpl>)GetValue(BoardProperty); }
            set { SetValue(BoardProperty, value); }
        }

        private static void OnBoardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (BoardControlBase<PointImpl>)d;
            control.InvalidateArrange(); // Redraw the control when the Board changes
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

        public BoardControlBase()
        {
            this.DefaultStyleKey = typeof(BoardControlBase<PointImpl>);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Board != null)
            {
                Canvas canvas = GetTemplateChild("PART_Canvas") as Canvas;

                if (canvas != null)
                {
                    canvas.Children.Clear();

                    List<PointImpl> hexPoints = Board.GetAllHexes().Select(Board.GetPosition).ToList();
                    PointImpl topLeftPoint = Utils.GetTopLeftPoint(hexPoints);
                    PointImpl bottomRightPoint = Utils.GetBottomRightPoint(hexPoints);
                    double hexWidth = Width / Utils.GetBoardWidthInHexes(topLeftPoint, bottomRightPoint);
                    double hexHeight = Height / Utils.GetBoardHeightInHexes(topLeftPoint, bottomRightPoint);

                    foreach (IHex hex in Board.GetAllHexes())
                    {
                        HexagonControl hexControl = new HexagonControl
                        {
                            Size = Size
                        };

                        Point point = GetHexPointOnControl(Board.GetPosition(hex), topLeftPoint);
                        Canvas.SetLeft(hexControl, point.X);
                        Canvas.SetTop(hexControl, point.Y);

                        canvas.Children.Add(hexControl);
                    }
                }
            }

            return base.ArrangeOverride(finalSize);
        }

        private Point GetHexPointOnControl(PointImpl hexPoint, PointImpl topLeftHexPoint)
        {
            PointImpl currPoint = topLeftHexPoint;
            Point hexPointOnControl = new Point(Size, Size * SQRT_3 / 2);

            // Iteratively move closer to the hex point we are trying to get coordinates for
            while (!Equals(currPoint, hexPoint))
            {
                // If we are up and to the left of the hex point
                if (currPoint.IsNorthOf(hexPoint) && currPoint.IsWestOf(hexPoint))
                {
                    // Move diagonally down and to the right towards the hex point
                    currPoint = currPoint.ToHexSouthEast();
                    hexPointOnControl.X += Size * 1.5;
                    hexPointOnControl.Y += Size * SQRT_3 / 2;
                }
                // If we are directly above the hex point
                else if (currPoint.IsNorthOf(hexPoint))
                {
                    // Move down towards the hex point
                    currPoint = currPoint.ToHexSouth();
                    hexPointOnControl.Y += Size * SQRT_3;
                }
                // If we are directly to the left of the hex point
                else if (currPoint.IsWestOf(hexPoint))
                {
                    // Move right towards the hex point
                    currPoint = currPoint.ToHexSouthEast().ToHexNorthEast();
                    hexPointOnControl.X += Size * 3;
                }
                else
                {
                    // This case is unexpected, it would mean we overshot the point we were moving towards somehow
                    throw new ArgumentException();
                }
            }

            return hexPointOnControl;
        }

        private static bool Equals(PointImpl p1, PointImpl p2)
        {
            return !p1.IsEastOf(p2) &&
                !p1.IsNorthOf(p2) &&
                !p1.IsWestOf(p2) &&
                !p1.IsSouthOf(p2);
        }
    }
}
