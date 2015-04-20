using System.Drawing;

namespace Fishbone.Parsing.Parsers
{
    public class LineMatrixDecoration : MatrixDecoration
    {
        public override Color GetCellColor(int col, int row)
        {
            if (col >= Mask.Length || row >= Mask.Length || col < 0 || row < 0)
            {
                return Color.Gray;
            }

            int number;

            if (row >= col)
            {
                number = Mask[row];
            }
            else
            {
                number = Mask[col];
            }
            return number == 0 ? Color.Gray : GenerateColor(number);

        }

        public LineMatrixDecoration(int[] mask)
            : base(mask)
        {
        }
    }
}