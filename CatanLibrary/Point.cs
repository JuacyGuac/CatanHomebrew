
namespace CatanLibrary
{
    public class Point : IPoint<Point>
    {
        int XPos { get; }
        int YPos { get; }

        public Point(int xInit, int yInit, int zInit)
        {
            XPos = xInit - zInit;
            YPos = yInit - zInit;
        }

        // Returns the point one unit in the positive x direction aka east aka 0deg direction
        public Point ToPointEast()
        {
            return new Point(this.XPos + 1, this.YPos, 0);
        }
        // Returns the point one unit in the negative z direction aka northeast aka 60deg direction
        public Point ToPointNorthEast()
        {
            return new Point(this.XPos, this.YPos, -1);
        }
        // Returns the point one unit in the positive y direction aka northwest aka 120deg direction
        public Point ToPointNorthWest()
        {
            return new Point(this.XPos, this.YPos + 1, 0);
        }
        // Returns the point one unit in the negative x direction aka west aka 180deg direction
        public Point ToPointWest()
        {
            return new Point(this.XPos - 1, this.YPos, 0);
        }
        // Returns the point one unit in the positive z direction aka southwest aka 240deg direction
        public Point ToPointSouthWest()
        {
            return new Point(this.XPos, this.YPos, 1);
        }
        // Returns the point one unit in the negative y direction aka southeast aka 300deg direction
        public Point ToPointSouthEast()
        {
            return new Point(this.XPos, this.YPos - 1, 0);
        }



        // Returns the point +1 x unit, -1 z unit aka 30deg direction
        public Point ToHexNorthEast()
        {
            return this.ToPointEast().ToPointNorthEast();
        }
        // Returns the point +1 y unit, -1 z unit aka 90deg direction
        public Point ToHexNorth()
        {
            return this.ToPointNorthWest().ToPointNorthEast();
        }
        // Returns the point -1 x unit, +1 y unit aka 150deg direction
        public Point ToHexNorthWest()
        {
            return this.ToPointWest().ToPointNorthWest();
        }
        // Returns the point -1 x unit, +1 z unit aka 210deg direction
        public Point ToHexSouthWest()
        {
            return this.ToPointWest().ToPointSouthWest();
        }
        // Returns the point -1 y unit, +1 z unit aka 270deg direction
        public Point ToHexSouth()
        {
            return this.ToPointSouthWest().ToPointSouthEast();
        }
        // Returns the point +1 x unit, -1 y unit aka 330deg direction
        public Point ToHexSouthEast()
        {
            return this.ToPointEast().ToPointSouthEast();
        }



        // Returns true if Point called is strictly further to the right/east/x direction than comparison
        public bool IsEastOf(Point comparison)
        {
            return ((this.XPos * 2) - this.YPos) > ((comparison.XPos * 2) - comparison.YPos);
        }
        // Returns true if Point called is strictly further above/to the North/perpendicular x direction than comparison
        public bool IsNorthOf(Point comparison)
        {
            if (comparison is Point p)
            {
                // compare points
            }
            else
            {

            }
            return this.YPos > comparison.YPos;
        }
        // Returns true if Point called is strictly further to the left/west/negative x direction than comparison
        public bool IsWestOf(Point comparison)
        {
            return ((this.XPos * 2) - this.YPos) < ((comparison.XPos * 2) - comparison.YPos);
        }
        // Returns true if Point called is strictly further below/to the South/perpendicular x direction than comparison
        public bool IsSouthOf(Point comparison)
        {
            return this.YPos < comparison.YPos;
        }



        // Returns true if Point called is strictly further to the EastNortheast/perpendicular y direction than comparison
        public bool IsEastNorthEastOf(Point comparison)
        {
            return this.XPos > comparison.XPos;
        }
        // Returns true if Point called is strictly further to the Northeast/neg z direction than comparison
        public bool IsNorthEastOf(Point comparison)
        {
            return (this.XPos + this.YPos) > (comparison.XPos + comparison.YPos);
        }
        // Returns true if Point called is strictly further to the Northwest/y direction than comparison
        public bool IsNorthWestOf(Point comparison)
        {
            return ((this.YPos * 2) - this.XPos) > ((comparison.YPos * 2) - comparison.XPos);
        }
        // Returns true if Point called is strictly further to the WestNorthwest/perpendicular z direction than comparison
        public bool IsWestNorthWestOf(Point comparison)
        {
            return (this.YPos - this.XPos) > (comparison.YPos - comparison.XPos);
        }
        // Returns true if Point called is strictly further to the WestSouthwest/perpendicular y direction than comparison
        public bool IsWestSouthWestOf(Point comparison)
        {
            return this.XPos < comparison.XPos;
        }
        // Returns true if Point called is strictly further to the Southwest/z direction than comparison
        public bool IsSouthWestOf(Point comparison)
        {
            return (this.XPos + this.YPos) < (comparison.XPos + comparison.YPos);
        }
        // Returns true if Point called is strictly further to the Southeast/neg y direction than comparison
        public bool IsSouthEastOf(Point comparison)
        {
            return ((this.YPos * 2) - this.XPos) < ((comparison.YPos * 2) - comparison.XPos);
        }
        // Returns true if Point called is strictly further to the EastSoutheast/perpendicular z direction than comparison
        public bool IsEastSouthEastOf(Point comparison)
        {
            return (this.YPos - this.XPos) < (comparison.YPos - comparison.XPos);
        }
    }
}