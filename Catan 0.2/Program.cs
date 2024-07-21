using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = getHailstoneInput();
            while(input != 0)
            {
                hailstone(input);
                input = getHailstoneInput();
            }

            Hex hex = new Hex();
        }

















        /*
        static void TestCatan()
        {
            IBoard board = BoardProvider.GetNewBoard();
            IHex hex = HexProvider.GetWaterTile();
            board.Place(hex, Point.Origin.Add60Deg().Add120Deg());
            board.Place(hex, Point.Origin);

            board.Print();
        }
        */

        /* prompts the user for a non-zero whole number until they do so
         * output: 0 if the user wants to quit. otherwise, the number that the user successfully inputted
         */
        static int getHailstoneInput()
        {
            Console.WriteLine("Please input a non-zero whole number for the hailstone sequence. Enter 0 to quit:");
            string input = Console.ReadLine();
            if(!int.TryParse(input, out int inputAsNum))
            {
                return getHailstoneInput();
            }
            return inputAsNum;
            
        }

        /* hailstone prints the hailstone sequence starting with the input and ending with 1
         * Input: start must be a non-zero whole number
         * Output: hailstone sequence printed on one line
         */
        static void hailstone(int start)
        {
            if (start <= 0)
            {
                Console.WriteLine("The hailstone sequence is not defined for zero and negative numbers.");
                return;
            }
            if (start == 1)
            {
                Console.WriteLine(start);
                return;
            }
            else
                Console.Write(start + ", ");
            if (start % 2 == 0)
            {
                hailstone(start / 2);
            } else
            {
                hailstone((start * 3) + 1);
            }
        }
    }
}