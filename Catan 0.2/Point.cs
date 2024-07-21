
namespace MyApp
{
    internal class Point : IPoint
    {
        int xPos { get; }
        int yPos { get; }

        public Point(int xInit, int yInit, int zInit)
        {
            xPos = xInit - zInit;
            yPos = yInit - zInit;
        }

        // Returns the point one unit in the positive x direction aka east aka 0deg direction
        public Point getEastOf()
        {
            return new Point(this.xPos + 1, this.yPos, 0);
        }
        // Returns the point one unit in the negative z direction aka northeast aka 60deg direction
        public Point getNorthEastOf()
        {
            return new Point(this.xPos, this.yPos, -1);
        }
        // Returns the point one unit in the positive y direction aka northwest aka 120deg direction
        public Point getNorthWestOf()
        {
            return new Point(this.xPos, this.yPos + 1, 0);
        }
        // Returns the point one unit in the negative x direction aka west aka 180deg direction
        public Point getWestOf()
        {
            return new Point(this.xPos - 1, this.yPos, 0);
        }
        // Returns the point one unit in the positive z direction aka southwest aka 240deg direction
        public Point getSouthWestOf()
        {
            return new Point(this.xPos, this.yPos, 1);
        }
        // Returns the point one unit in the negative y direction aka southeast aka 300deg direction
        public Point getSouthEastOf()
        {
            return new Point(this.xPos, this.yPos - 1, 0);
        }
        
        
        public Point getDiagonalEastNorthEast()
        {
            return this.getEastOf().getNorthEastOf();
        }
        public Point getDiagonalNorth()
        {
            return this.getNorthWestOf().getNorthEastOf();
        }
        public Point getDiagonalWestNorthWest()
        {
            return this.getWestOf().getNorthWestOf();
        }
        public Point getDiagonalWestSouthWest()
        {
            return this.getWestOf().getSouthWestOf();
        }
        public Point getDiagonalSouth()
        {
            return this.getSouthWestOf().getSouthEastOf();
        }
        public Point getDiagonalEastSouthEast()
        {
            return this.getEastOf().getSouthEastOf();
        }
    }
}