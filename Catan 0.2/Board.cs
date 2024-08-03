
namespace MyApp

{
    public class Board<PointImpl> : IBoard<PointImpl> where PointImpl : IPoint<PointImpl>
    {
        // data structures
        private Dictionary<IPoint<PointImpl>, IHex> pointToHex;
        private Dictionary<IHex, IPoint<PointImpl>> hexToPoint;
        private Dictionary<IPoint<PointImpl>, IToken> pointToToken;
        private Dictionary<IToken, IPoint<PointImpl>> tokenToPoint;
        


        // constructor
        Board()
        {
            pointToHex = new Dictionary<IPoint<PointImpl>, IHex>();
        }

        public void Place(IPoint<PointImpl> position, IHex hex, IToken token)
        {
            pointToHex.Add(position, hex);
            hexToPoint.Add(hex, position);
            pointToToken.Add(position, token);
            tokenToPoint.Add(token, position);
        }
        public void Place(IPoint<PointImpl> position, IToken token)
        {
            pointToToken.Add(position, token);
            tokenToPoint.Add(token, position);
        }
        public void Place(IPoint<PointImpl> position, IHex hex)
        {
            pointToHex.Add(position, hex);
            hexToPoint.Add(hex, position);
        }

        public IHex GetHex(IPoint<PointImpl> position)
        {
            return pointToHex[position];  // what is our intended behaviour if the Hex isn't in the dict?
        }
        public bool HasHex(IPoint<PointImpl> position)
        {
            return pointToHex.ContainsKey(position);
        }
        public IList<IHex> GetAllHexes()
        {
            List<IHex> hexes = new List<IHex>(hexToPoint.Keys);
            return hexes;
        }

        public IToken GetToken(IPoint<PointImpl> position)
        {
            return pointToToken[position];
        }
        public bool HasToken(IPoint<PointImpl> position)
        {
            return pointToToken.ContainsKey(position);
        }
        public IList<IToken> GetAllTokens()
        {
            List<IToken> tokens = new List<IToken>(tokenToPoint.Keys);
            return tokens;
        }

        public IPoint<PointImpl> GetPosition(IHex hex)
        {
            return hexToPoint[hex];
        }
        public IPoint<PointImpl> GetPosition(IToken token)
        {
            return tokenToPoint[token]; 
        }





        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}