using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanLibrary
{
    public static class Utils
    {
        public static double GetBoardWidthInHexes<PointImpl>(PointImpl topLeftPoint, PointImpl bottomRightPoint) where PointImpl : IPoint<PointImpl>
        {
            double boardWidthInHexes = 0.25;

            while (!topLeftPoint.IsEastOf(bottomRightPoint))
            {
                topLeftPoint = topLeftPoint.ToHexSouthEast();
                boardWidthInHexes += 0.75;
            }

            return boardWidthInHexes;
        }

        public static double GetBoardHeightInHexes<PointImpl>(PointImpl topLeftPoint, PointImpl bottomRightPoint) where PointImpl : IPoint<PointImpl>
        {
            double boardWidthInHexes = 0.5;

            while (!topLeftPoint.IsSouthOf(bottomRightPoint))
            {
                topLeftPoint = topLeftPoint.ToHexSouthEast();
                boardWidthInHexes += 0.5;
            }

            return boardWidthInHexes;
        }

        public static PointImpl GetTopLeftPoint<PointImpl>(IList<PointImpl> points) where PointImpl : IPoint<PointImpl>
        {
            PointImpl leftMostPoint = GetLeftMostPoint(points);
            PointImpl topMostPoint = GetTopMostPoint(points);
            PointImpl potentialTopLeftPoint = leftMostPoint;

            while (potentialTopLeftPoint.IsSouthOf(topMostPoint))
            {
                if (potentialTopLeftPoint.IsWestOf(leftMostPoint))
                {
                    potentialTopLeftPoint = potentialTopLeftPoint.ToHexNorthEast();
                }
                else
                {
                    potentialTopLeftPoint = potentialTopLeftPoint.ToHexNorthWest();
                }
            }

            return potentialTopLeftPoint;
        }

        public static PointImpl GetBottomRightPoint<PointImpl>(IList<PointImpl> points) where PointImpl : IPoint<PointImpl>
        {
            PointImpl rightMostPoint = GetRightMostPoint(points);
            PointImpl bottomMostPoint = GetBottomMostPoint(points);
            PointImpl potentialBottomRightPoint = rightMostPoint;

            while (potentialBottomRightPoint.IsNorthOf(bottomMostPoint))
            {
                if (potentialBottomRightPoint.IsEastOf(rightMostPoint))
                {
                    potentialBottomRightPoint = potentialBottomRightPoint.ToHexSouthWest();
                }
                else
                {
                    potentialBottomRightPoint = potentialBottomRightPoint.ToHexSouthEast();
                }
            }

            return potentialBottomRightPoint;
        }

        public static PointImpl GetLeftMostPoint<PointImpl>(IList<PointImpl> points) where PointImpl : IPoint<PointImpl>
        {
            PointImpl leftMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsWestOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        public static PointImpl GetRightMostPoint<PointImpl>(IList<PointImpl> points) where PointImpl : IPoint<PointImpl>
        {
            PointImpl rightMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsEastOf(rightMostPoint))
                {
                    rightMostPoint = p;
                }
            }

            return rightMostPoint;
        }

        public static PointImpl GetTopMostPoint<PointImpl>(IList<PointImpl> points) where PointImpl : IPoint<PointImpl>
        {
            PointImpl topMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsWestOf(topMostPoint))
                {
                    topMostPoint = p;
                }
            }

            return topMostPoint;
        }

        public static PointImpl GetBottomMostPoint<PointImpl>(IList<PointImpl> points) where PointImpl : IPoint<PointImpl>
        {
            PointImpl leftMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsWestOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }
    }
}
