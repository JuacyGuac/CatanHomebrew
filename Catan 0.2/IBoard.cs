namespace MyApp
{
    internal interface IBoard
    {
        void Place(IHex hex, Point value);
        void Print();
    }
}