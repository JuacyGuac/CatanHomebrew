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

            IPoint<PointImpl> topLeftPoint = GetTopLeftPoint(b);
            IPoint<PointImpl> bottomRightPoint = GetBottomRightPoint(b);

            int hexRow = 0;
            IPoint<PointImpl> currPoint = topLeftPoint;

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

        private IPoint<PointImpl> GetTopLeftPoint(IBoard<PointImpl> b)
        {
            IPoint<PointImpl> leftMostPoint = GetLeftMostPoint(b);
            IPoint<PointImpl> topMostPoint = GetTopMostPoint(b);
            IPoint<PointImpl> potentialTopLeftPoint = leftMostPoint;

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

        private IPoint<PointImpl> GetBottomRightPoint(IBoard<PointImpl> b)
        {
            IPoint<PointImpl> rightMostPoint = GetRightMostPoint(b);
            IPoint<PointImpl> bottomMostPoint = GetBottomMostPoint(b);
            IPoint<PointImpl> potentialBottomRightPoint = rightMostPoint;

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

        private IPoint<PointImpl> GetLeftMostPoint(IBoard<PointImpl> b)
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<IPoint<PointImpl>> points = hexes.Select(b.GetPosition).ToList();
            IPoint<PointImpl> leftMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsWestOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        private IPoint<PointImpl> GetRightMostPoint(IBoard<PointImpl> b)
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<IPoint<PointImpl>> points = hexes.Select(b.GetPosition).ToList();
            IPoint<PointImpl> rightMostPoint = points.First();

            foreach (PointImpl p in points)
            {
                if (p.IsEastOf(rightMostPoint))
                {
                    rightMostPoint = p;
                }
            }

            return rightMostPoint;
        }

        private IPoint<PointImpl> GetTopMostPoint(IBoard<PointImpl> b)
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<IPoint<PointImpl>> points = hexes.Select(b.GetPosition).ToList();
            IPoint<PointImpl> topMostPoint = points.First();

            foreach (IPoint<PointImpl> p in points)
            {
                if (p.IsWestOf(topMostPoint))
                {
                    topMostPoint = p;
                }
            }

            return topMostPoint;
        }

        private IPoint<PointImpl> GetBottomMostPoint(IBoard<PointImpl> b)
        {
            IList<IHex> hexes = b.GetAllHexes();
            List<IPoint<PointImpl>> points = hexes.Select(b.GetPosition).ToList();
            IPoint<PointImpl> leftMostPoint = points.First();

            foreach (IPoint<PointImpl> p in points)
            {
                if (p.IsWestOf(leftMostPoint))
                {
                    leftMostPoint = p;
                }
            }

            return leftMostPoint;
        }

        private IPoint<PointImpl> GetPoint(IPoint<PointImpl> topLeftPoint, int hexRow, int hexCol)
        {
            IPoint<PointImpl> currPoint = topLeftPoint;

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
                currPoint.ToPointEast().ToPointEast().ToPointEast();
                    
                hexCol--;
            }

            return currPoint;
        }

        private char[][] GetTextBoard(IBoard<PointImpl> b)
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

        private int GetNumTextRows(IBoard<PointImpl> b)
        {
            IPoint<PointImpl> topMostPoint = GetTopMostPoint(b);
            IPoint<PointImpl> bottomMostPoint = GetBottomMostPoint(b);
            int numRows = slashesPerHex + 1;

            IPoint<PointImpl> currPoint = topMostPoint;

            while (!currPoint.IsSouthOf(bottomMostPoint))
            {
                currPoint.ToHexSouthEast();
                numRows += slashesPerHex;
            }

            return numRows;
        }

        private int GetNumTextCols(IBoard<PointImpl> b)
        {
            IPoint<PointImpl> leftMostPoint = GetLeftMostPoint(b);
            IPoint<PointImpl> rightMostPoint = GetRightMostPoint(b);
            int numCols = slashesPerHex;

            IPoint<PointImpl> currPoint = leftMostPoint;

            while (!currPoint.IsEastOf(rightMostPoint))
            {
                currPoint.ToHexSouthEast();
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
