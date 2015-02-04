using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Fishbone.Common.Model;

namespace Fishbone.Drawing.Drawers
{
    public class SkeletonDrawer : IDrawer<int>
    {
        public static Size AdjustSize(IMatrix<int> mtx)
        {

            var minHeight = TestsConstants.ImageHeight;
            var minWidth = TestsConstants.ImageWidth;

            var maxImageHeight = TestsConstants.MaxImageHeight;
            var maxImageWidth = TestsConstants.MaxImageWidth;
            return AdjustSize(mtx.Cols, mtx.Rows,minHeight,minWidth,maxImageHeight, maxImageWidth);
        }

        public static Size AdjustSize(int cols, int rows, int minHeight, int minWidth, int maxHeight, int maxWidth)
        {
            float width = cols*TestsConstants.Scale;
            float height = rows*TestsConstants.Scale;
            float scaleHeight = height < minHeight ? minHeight/height : 1;
            float scaleWidth = width < minWidth ? minWidth/width : 1;

            float scale = Math.Max(scaleHeight, scaleWidth);

            height *= scale;
            width *= scale;
            scaleHeight = height > maxHeight ? maxHeight / height : 1;
            scaleWidth = width > maxWidth ? maxWidth / width : 1;

            scale = Math.Min(scaleHeight, scaleWidth);

            height *= scale;
            width *= scale;

            return new Size((int) width, (int) height);
        }

        protected static float GetScaling(float fieldWidth, float fieldHeight, float width, float height)
        {
            float scaling = fieldWidth / width < fieldHeight / height ? fieldWidth / width : fieldHeight / height;
            return scaling;
        }

        public Bitmap DrawPart(IMatrix<int> matrix, out float scale, int col, int row, int cellx, int celly, int height, int width, string fileName)
        {
            Size adjustSize = AdjustSize(cellx, celly, height, width, height, width);

            scale = 1 / GetScaling(cellx, celly, adjustSize.Width, adjustSize.Height);
            Console.WriteLine("Scale {0}", scale);
            Console.WriteLine("Size {0}", adjustSize);
            var bmp = new Bitmap(adjustSize.Width, adjustSize.Height);

            var canvas = Graphics.FromImage(bmp);
            canvas.Clear(Color.Empty);

            for (int r = row; r < row + celly; r++)
            {
                for (int c = col; c < col + cellx; c++)
                {
                    if (matrix[r, c] > 0)
                    {
                        var x = c - col;
                        var y = r - row;
                        canvas.FillRectangle(Brushes.Gray, x*scale, y*scale, scale, scale);
                        canvas.DrawRectangle(new Pen(Color.Black), x*scale, y*scale, scale, scale);
                    }
                }
            }

            //bmp.Save(fileName, ImageFormat.Png);
            //return canvas;
            return bmp;
        }

        public Size ComputeSize(float scale, int cellx, int celly)
        {
            int width = Convert.ToInt32(cellx*scale) + 1;
            int height = Convert.ToInt32(celly*scale) + 1;
            return new Size(width, height);
        }

        public Graphics Draw(IMatrix<int> matrix, string fileName)
        {
            Size adjustSize = AdjustSize(matrix);

            float scale = 1 / GetScaling(matrix.Cols, matrix.Rows, adjustSize.Width, adjustSize.Height);
            Console.WriteLine("Scale {0}", scale);
            Console.WriteLine("Size {0}", adjustSize);
            var bmp = new Bitmap(adjustSize.Width, adjustSize.Height);

            var canvas = Graphics.FromImage(bmp);
            canvas.Clear(Color.Empty);

            foreach (var cell in matrix)
            {
                var c = cell.Col;
                var r = cell.Row;
                canvas.FillRectangle(Brushes.Gray, c * scale, r * scale, scale, scale);
                canvas.DrawRectangle(new Pen(Color.Black), c * scale, r * scale, scale, scale);

                c = cell.Row;
                r = cell.Col;
                canvas.FillRectangle(Brushes.Gray, c * scale, r * scale, scale, scale);
                canvas.DrawRectangle(new Pen(Color.Black), c * scale, r * scale, scale, scale);
            }
            bmp.Save(fileName, ImageFormat.Png);
            return canvas;
        }
    }
}