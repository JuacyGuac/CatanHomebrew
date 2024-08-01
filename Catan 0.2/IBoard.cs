namespace MyApp
{
    public interface IBoard<PointImpl> where PointImpl : IPoint<PointImpl>
    {
        public void Place(PointImpl position, IHex hex, IToken token);
        public void Place(PointImpl position, IHex hex);
        public void Place(PointImpl position, IToken token);

        public IHex GetHex(PointImpl position);
        public bool HasHex(PointImpl position);
        public IList<IHex> GetAllHexes();
        
        public IToken GetToken(PointImpl position);
        public bool HasToken(PointImpl position);
        public IList<IToken> GetAllTokens();

        public PointImpl GetPosition(IHex hex);
        public PointImpl GetPosition(IToken token);
    }
}