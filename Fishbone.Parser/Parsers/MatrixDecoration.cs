using System;
using System.Drawing;

namespace Fishbone.Parsing.Parsers
{
    public class MatrixDecoration
    {
        protected readonly int[] Mask;
        private readonly Random m_rnd = new Random();
        private readonly int[] m_colors;

        public MatrixDecoration(int[] mask)
        {
            Mask = mask;
            m_colors = new int[mask.Length];
            for (int i = 0; i < m_colors.Length; i++)
            {
                m_colors[i] = m_rnd.Next(256 * 256 * 256);
            }
        }

        public virtual Color GetCellColor(int col, int row)
        {
            return Color.Gray;
        }

        protected virtual Color GenerateColor(int number)
        {
            var cIndex = m_colors[number];
            var r = (cIndex) % 256;
            cIndex /= 256;
            var g = (cIndex) % 256;
            cIndex /= 256;
            var b = (cIndex);
            return Color.FromArgb(r, g, b);
        }
    }
}