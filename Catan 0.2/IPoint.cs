namespace MyApp
{
    public interface IPoint<PointImpl>
    {
        public IPoint<PointImpl> ToPointEast();
        public IPoint<PointImpl> ToPointNorthEast();
        public IPoint<PointImpl> ToPointNorthWest();
        public IPoint<PointImpl> ToPointWest();
        public IPoint<PointImpl> ToPointSouthWest();
        public IPoint<PointImpl> ToPointSouthEast();

        public IPoint<PointImpl> ToHexNorthEast();
        public IPoint<PointImpl> ToHexNorth();
        public IPoint<PointImpl> ToHexNorthWest();
        public IPoint<PointImpl> ToHexSouthWest();
        public IPoint<PointImpl> ToHexSouth();
        public IPoint<PointImpl> ToHexSouthEast();
        
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