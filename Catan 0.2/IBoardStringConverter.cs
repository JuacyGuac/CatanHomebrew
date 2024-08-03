namespace MyApp
{
    public interface IBoardStringConverter<PointImpl> where PointImpl : IPoint<PointImpl>
    {
        public string ConvertToString(IBoard<PointImpl> b);
    }
}
=t