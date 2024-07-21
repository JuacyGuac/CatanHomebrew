
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

        public Point getEastOf()
        {
            return new Point(this.xPos + 1, this.yPos, 0);
        }
        public Point getNorthEastOf()
        {
            return new Point(this.xPos, this.yPos, -1);
        }
        public Point getNorthWestOf()
        {
            return new Point(this.xPos, this.yPos + 1, 0);
        }
        public Point getWestOf()
        {
            return new Point(this.xPos - 1, this.yPos, 0);
        }
        public Point getSouthWestOf()
        {
            return new Point(this.xPos, this.yPos, 1);
        }
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