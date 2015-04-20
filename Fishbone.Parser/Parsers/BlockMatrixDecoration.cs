using System;
using System.Drawing;
using System.Linq;

namespace Fishbone.Parsing.Parsers
{
    public class BlockMatrixDecoration : MatrixDecoration
    {

        public override Color GetCellColor(int col, int row)
        {
            var lastCol = Mask.Last(b => b <= col);
            var lastRow = Mask.Last(b => b <= row);

            if (lastRow == lastCol)
            {
                var index = Array.IndexOf(Mask, lastRow);
                return GenerateColor(index);
            }

            return Color.Gray;
        }

        public BlockMatrixDecoration(int[] mask)
            : base(mask)
        {
        }
    }
}