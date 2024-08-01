namespace MyApp
{
    public class BoardStringConverter<PointImpl> : IBoardStringConverter<PointImpl> where PointImpl : IPoint<PointImpl>
    {
        private int slashesPerHex;
        private int underscoresPerHex;

        public BoardStringConverter(int size)
        {
            slashesPerHex = size;
            underscoresPerHex = size * 2;
        }

        public string ConvertToString(IBoard<PointImpl> b)
        {
            char[][] textBoard = GetTextBoard(b);

            PointImpl topLeftPoint = GetTopLeftPoint(b);
            PointImpl bottomRightPoint = GetBottomRightPoint(b);

            int hexRow = 0;
            PointImpl currPoint = topLeftPoint;

            // For each row of the board
            while (!currPoint.IsSouthOf(bottomRightPoint))
            {
                int hexCol = 0;

                // For each hex in the row
                while (!currPoint.IsEastOf(bottomRightPoint))
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

        private PointImpl GetTopLeftPoint(IBoard<PointImpl> b)
        {
            PointImpl leftMostPoint = GetLeftMostPoint(b);
            PointImpl topMostPoint = GetTopMostPoint(b);
            PointImpl potentialTopLeftPoint = leftMostPoint;

            while (potentialTopLeftPoint.IsSouthOf(topMostPoint))
            {
                if (potentialTopLeftPoint.IsWestOf(leftMostPoint))
                {
                    potentialTopLeftPoint = potentialTopLeftPoint.ToHexNorthEast();
                }
                else
                {
                    potentialTopLeftPoint = potentialTopLeftPoint.ToHexNorthWest();
                }
            }

            return potentialTopLeftPoint;
        }

        private IPoint<PointImpl> GetBottomRightPoint<PointImpl>(IBoard<PointImpl> b) where PointImpl : IPoint<PointImpl>
        {
            IPoint<PointImpl> rightMostPoint = GetRightMostPoint(b);
            IPoint<PointImpl> bottomMostPoint = GetBottomMostPoint(b);
            PointImpl potentialBottomRightPoint = rightMostPoint;

            while (potentialBottomRightPoint.IsNorthOf(bottomMostPoint))
            {
                if (potentialBottomRightPoint.IsEastOf(rightMostPoint))
                {
                    potentialBottomRightPoint = potentialBottomRightPoint.ToHexSouthWest();
                }
                else
                {
                    potentialBottomRightPoint = potentialBottomRightPoint.ToHexSouthEast();
                }
            }

            return potentialBottomRightPoint;
        }

        private PointImpl GetLeftMostPoint<PointImpl>(IBoard<PointImpl> b) where PointImpl : IPoint<PointImpl>
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<PointImpl> points = hexes.Select(b.GetPosition).ToList();
            PointImpl leftMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsWestOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        private PointImpl GetRightMostPoint<PointImpl>(IBoard<PointImpl> b) where PointImpl : IPoint<PointImpl>
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<PointImpl> points = hexes.Select(b.GetPosition).ToList();
            PointImpl rightMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsEastOf(rightMostPoint))
                {
                    rightMostPoint = p;
                }
            }

            return rightMostPoint;
        }

        private PointImpl GetTopMostPoint<PointImpl>(IBoard<PointImpl> b) where PointImpl : IPoint<PointImpl>
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<PointImpl> points = hexes.Select(b.GetPosition).ToList();
            PointImpl topMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsWestOf(topMostPoint))
                {
                    topMostPoint = p;
                }
            }

            return topMostPoint;
        }

        private PointImpl GetBottomMostPoint<PointImpl>(IBoard<PointImpl> b) where PointImpl : IPoint<PointImpl>
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<PointImpl> points = hexes.Select(b.GetPosition).ToList();
            PointImpl leftMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsLeftOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        private PointImpl GetPoint<PointImpl>(PointImpl topLeftPoint, int hexRow, int hexCol) where PointImpl : IPoint<PointImpl>
        {
            PointImpl currPoint = topLeftPoint;

            while (hexRow != 0)
            {
                if (currPoint.IsEastOf(topLeftPoint))
                {
                    currPoint = currPoint.ToHexSouthWest();
                }
                else
                {
                    currPoint = currPoint.ToHexSouthEast();
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

        private char[][] GetTextBoard<PointImpl>(IBoard<PointImpl> b) where PointImpl : IPoint<PointImpl>
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
