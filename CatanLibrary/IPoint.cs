using Windows.Graphics.Display;

namespace CatanLibrary
{
    public interface IPoint<PointImpl>
    {
        public PointImpl ToPointEast();
        public PointImpl ToPointNorthEast();
        public PointImpl ToPointNorthWest();
        public PointImpl ToPointWest();
        public PointImpl ToPointSouthWest();
        public PointImpl ToPointSouthEast();

        public PointImpl ToHexNorthEast();
        public PointImpl ToHexNorth();
        public PointImpl ToHexNorthWest();
        public PointImpl ToHexSouthWest();
        public PointImpl ToHexSouth();
        public PointImpl ToHexSouthEast();
        
        /* for the following methods, comparison must be the same IPoint implementation as called with
         */
        public bool IsEastOf(PointImpl comparison);
        public bool IsWestOf(PointImpl comparison);
        public bool IsNorthOf(PointImpl comparison);
        public bool IsSouthOf(PointImpl comparison);

        public bool IsEastNorthEastOf(PointImpl comparison);
        public bool IsNorthEastOf(PointImpl comparison);
        public bool IsNorthWestOf(PointImpl comparison);
        public bool IsWestNorthWestOf(PointImpl comparison);
        public bool IsWestSouthWestOf(PointImpl comparison);
        public bool IsSouthWestOf(PointImpl comparison);
        public bool IsSouthEastOf(PointImpl comparison);
        public bool IsEastSouthEastOf(PointImpl comparison);
    }
}