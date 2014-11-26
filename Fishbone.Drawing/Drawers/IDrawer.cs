using Fishbone.Common.Model;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fishbone.Drawing.Drawers
{
    public interface IDrawer<T>
    {
        Graphics Draw(Matrix<T> matrix, string fileName);
    }

    public class SkeletonDrawer : IDrawer<int>
    {
        public Graphics Draw(Matrix<int> matrix, string fileName)
        {
            const int integerScale = 20;
            const float scale = integerScale;

            var bmp = new Bitmap(matrix.Width * integerScale, matrix.Height * integerScale);
            var canvas = Graphics.FromImage(bmp);
            canvas.Clear(Color.White);
            for (int r = 0; r < matrix.Height; r++)
            {
                for (int c = 0; c < matrix.Width; c++)
                {
                    if (matrix[r][c] > 0)
                    {
                        canvas.FillRectangle(Brushes.Gray, c * scale, r * scale, scale, scale);
                        canvas.DrawRectangle(new Pen(Color.Black), c * scale, r * scale, scale, scale);
                    }
                }
            }
            bmp.Save(fileName, ImageFormat.Png);
            return canvas;
        }
    }
}