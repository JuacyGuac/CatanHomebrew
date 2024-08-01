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
        public bool IsEastOf(IPoint<PointImpl> comparison);
        public bool IsWestOf(IPoint<PointImpl> comparison);
        public bool IsNorthOf(IPoint<PointImpl> comparison);
        public bool IsSouthOf(IPoint<PointImpl> comparison);

        public bool IsEastNorthEastOf(IPoint<PointImpl> comparison);
        public bool IsNorthEastOf(IPoint<PointImpl> comparison);
        public bool IsNorthWestOf(IPoint<PointImpl> comparison);
        public bool IsWestNorthWestOf(IPoint<PointImpl> comparison);
        public bool IsWestSouthWestOf(IPoint<PointImpl> comparison);
        public bool IsSouthWestOf(IPoint<PointImpl> comparison);
        public bool IsSouthEastOf(IPoint<PointImpl> comparison);
        public bool IsEastSouthEastOf(IPoint<PointImpl> comparison);

    }
}