
namespace MyApp
{
    internal partial class Board : IBoard
    {
        // data structures
        public Dictionary<Point, Hex> hexes;
        public Dictionary<Edge, EdgeData> edges;
        public Dictionary<Vertex, VertexData> vertices;
        public static Point origin;

        // constructor
        static Board()
        {
            origin = new Point(0, 0, 0);

        }

        /*
        static Board(DefaultBoard boardSeed)
        {
            hexes = new Dictionary<Point, Hex>();
            edges = new Dictionary<Edge, EdgeData>();
            vertices = new Dictionary<Vertex, VertexData>();

        }
        */

        public void Place(IHex hex, Point value)
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}