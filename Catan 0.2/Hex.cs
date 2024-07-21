namespace MyApp
{
    public class Hex
    {
        public HexType type { get; set; }  // What type of hex is it?
        public HashSet<int> numberToken { get; set; }  // What number token(s) are on it?
        // private Point position;  // wait how do we assign this
        public bool hasRobber { get; set; }
        public bool hasPirate { get; set; }
        public bool hasMerchant { get; set; }
        public bool hasPiece { get; set; }

        public Hex()
        {
            type = HexType.Sea;
            numberToken = new HashSet<int>();
            hasRobber = false;
            hasPirate = false;
            hasMerchant = false;
            hasPiece = false;
        }
    }
}