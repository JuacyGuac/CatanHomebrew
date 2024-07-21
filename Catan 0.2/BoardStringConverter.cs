namespace MyApp
{
    public class BoardStringConverter : IBoardStringConverter
    {
        private int slashesPerHex;
        private int underscoresPerHex;

        public BoardStringConverter(int size)
        {
            slashesPerHex = size;
            underscoresPerHex = size * 2;
        }

        public string ConvertToString(IBoard b)
        {
            char[][] textBoard = GetTextBoard(b);

            IPoint topLeftPoint = GetTopLeftPoint(b);
            IPoint bottomRightPoint = GetBottomRightPoint(b);

            int hexRow = 0;
            IPoint currPoint = topLeftPoint;

            // For each row of the board
            while (!currPoint.IsBelow(bottomRightPoint))
            {
                int hexCol = 0;

                // For each hex in the row
                while (!currPoint.IsRightOf(bottomRightPoint))
                {
                    if (b.HasHex(currPoint))
                    {
                        // Get the top left indices of the current hex on the text board
                        int x = 2 * hexCol * (slashesPerHex + underscoresPerHex) + hexRow % 2 == 0 ? slashesPerHex : 0;
                        int y = hexRow * slashesPerHex;

                        WriteHex(textBoard, x, y);
                    }

                    // Advance to next hex in row
                    hexCol++;
                    currPoint = GetPoint(topLeftPoint, hexRow, hexCol);
                }

                // Advance to first hex in next row
                hexRow++;
                currPoint = GetPoint(topLeftPoint, hexRow, 0);
            }

            return ToString(textBoard);
        }

        private IPoint GetTopLeftPoint(IBoard b)
        {
            IPoint leftMostPoint = GetLeftMostPoint(b);
            IPoint topMostPoint = GetTopMostPoint(b);
            IPoint potentialTopLeftPoint = leftMostPoint;

            while (potentialTopLeftPoint.IsBelow(topMostPoint))
            {
                if (potentialTopLeftPoint.IsLeftOf(leftMostPoint))
                {
                    potentialTopLeftPoint = potentialTopLeftPoint.AddDeg30Root3();
                }
                else
                {
                    potentialTopLeftPoint = potentialTopLeftPoint.AddDeg150Root3();
                }
            }

            return potentialTopLeftPoint;
        }

        private IPoint GetBottomRightPoint(IBoard b)
        {
            IPoint rightMostPoint = GetRightMostPoint(b);
            IPoint bottomMostPoint = GetBottomMostPoint(b);
            IPoint potentialBottomRightPoint = rightMostPoint;

            while (potentialBottomRightPoint.IsAbove(bottomMostPoint))
            {
                if (potentialBottomRightPoint.IsRightOf(rightMostPoint))
                {
                    potentialBottomRightPoint = potentialBottomRightPoint.AddDeg210Root3();
                }
                else
                {
                    potentialBottomRightPoint = potentialBottomRightPoint.AddDeg330Root3();
                }
            }

            return potentialBottomRightPoint;
        }

        private IPoint GetLeftMostPoint(IBoard b)
        {
            IList<IHex> hexes = b.GetHexes();
            List<IPoint> points = hexes.Select(b.GetPoint).ToList();
            IPoint leftMostPoint = points.First();

            foreach (IPoint p in points)
            {
                if (p.IsLeftOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        private IPoint GetRightMostPoint(IBoard b)
        {
            IList<IHex> hexes = b.GetHexes();
            List<IPoint> points = hexes.Select(b.GetPoint).ToList();
            IPoint rightMostPoint = points.First();

            foreach (IPoint p in points)
            {
                if (p.IsRightOf(rightMostPoint))
                {
                    rightMostPoint = p;
                }
            }

            return rightMostPoint;
        }

        private IPoint GetTopMostPoint(IBoard b)
        {
            IList<IHex> hexes = b.GetHexes();
            List<IPoint> points = hexes.Select(b.GetPoint).ToList();
            IPoint topMostPoint = points.First();

            foreach (IPoint p in points)
            {
                if (p.IsLeftOf(topMostPoint))
                {
                    topMostPoint = p;
                }
            }

            return topMostPoint;
        }

        private IPoint GetBottomMostPoint(IBoard b)
        {
            IList<IHex> hexes = b.GetHexes();
            List<IPoint> points = hexes.Select(b.GetPoint).ToList();
            IPoint leftMostPoint = points.First();

            foreach (IPoint p in points)
            {
                if (p.IsLeftOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        private IPoint GetPoint(IPoint topLeftPoint, int hexRow, int hexCol)
        {
            IPoint currPoint = topLeftPoint;

            while (hexRow != 0)
            {
                if (currPoint.IsRightOf(topLeftPoint))
                {
                    currPoint = currPoint.AddDeg210Root3();
                }
                else
                {
                    currPoint = currPoint.AddDeg330Root3();
                }

                hexRow--;
            }

            while (hexCol != 0)
            {
                currPoint.AddDeg0().AddDeg0().AddDeg0();

                hexCol--;
            }

            return currPoint;
        }

        private char[][] GetTextBoard(IBoard b)
        {
            int numTextRows = GetNumTextRows(b);
            int numTextCols = GetNumTextCols(b);

            char[][] output = new char[numTextCols][];

            for (int x = 0; x < numTextCols; x++)
            {
                output[x] = new char[numTextRows];
            }

            return output;
        }

        private string ToString(char[][] b)
        {
            string output = " ";

            for (int y = 0; y < b[0].Length; y++)
            {
                for (int x = 0; x < b.Length; x++)
                {
                    output += b[x][y];
                }

                output += "\n";
            }

            return output;
        }

        private int GetNumTextRows(IBoard b)
        {
            IPoint topMostPoint = GetTopMostPoint(b);
            IPoint bottomMostPoint = GetBottomMostPoint(b);
            int numRows = slashesPerHex + 1;

            IPoint currPoint = topMostPoint;

            while (!currPoint.IsBelow(bottomMostPoint))
            {
                currPoint.AddDeg330Root3();
                numRows += slashesPerHex;
            }

            return numRows;
        }

        private int GetNumTextCols(IBoard b)
        {
            IPoint leftMostPoint = GetLeftMostPoint(b);
            IPoint rightMostPoint = GetRightMostPoint(b);
            int numCols = slashesPerHex;

            IPoint currPoint = leftMostPoint;

            while (!currPoint.IsRightOf(rightMostPoint))
            {
                currPoint.AddDeg330Root3();
                numCols += underscoresPerHex + slashesPerHex;
            }

            return numCols;
        }

        private void WriteHex(char[][] output, int hexX, int hexY)
        {
            int x = slashesPerHex;
            int y = 0;

            // Draw top underscores
            for (int i = 0; i < underscoresPerHex; i++)
            {
                output[hexX + x][hexY + y] = '_';
                x++;
            }

            x = slashesPerHex + underscoresPerHex;
            y = 1;

            // Draw top right slashes
            for (int i = 0; i < slashesPerHex; i++)
            {
                output[hexX + x][hexY + y] = '\\';
                x++;
                y++;
            }

            x = 2 * slashesPerHex + underscoresPerHex - 1;
            y = slashesPerHex + 1;

            // Draw bottom right slashes
            for (int i = 0; i < slashesPerHex; i++)
            {
                output[hexX + x][hexY + y] = '/';
                x--;
                y++;
            }

            x = slashesPerHex + underscoresPerHex - 1;
            y = 2 * slashesPerHex;

            // Draw bottom underscores
            for (int i = 0; i < underscoresPerHex; i++)
            {
                output[hexX + x][hexY + y] = '_';
                x--;
            }

            x = slashesPerHex - 1;
            y = 2 * slashesPerHex;

            // Draw bottom left slashes
            for (int i = 0; i < slashesPerHex; i++)
            {
                output[hexX + x][hexY + y] = '\\';
                x--;
                y--;
            }

            x = 0;
            y = slashesPerHex;

            // Draw top left slashes
            for (int i = 0; i < slashesPerHex; i++)
            {
                output[hexX + x][hexY + y] = '/';
                x++;
                y--;
            }
        }
    }
}
