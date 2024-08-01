namespace MyApp
{
    public interface IBoard<PointImpl> where PointImpl : IPoint<PointImpl>
    {
        public void Place(IPoint<PointImpl> position, IHex hex, IToken token);
        public void Place(IPoint<PointImpl> position, IHex hex);
        public void Place(IPoint<PointImpl> position, IToken token);

        public IHex GetHex(IPoint<PointImpl> position);
        public bool HasHex(IPoint<PointImpl> position);
        public IList<IHex> GetAllHexes();
        
        public IToken GetToken(IPoint<PointImpl> position);
        public bool HasToken(IPoint<PointImpl> position);
        public IList<IToken> GetAllTokens();

        public IPoint<PointImpl> GetPosition(IHex hex);
        public IPoint<PointImpl> GetPosition(IToken token);
    }
}