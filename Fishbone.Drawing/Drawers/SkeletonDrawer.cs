using System;
using log4net;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Fishbone.Common.Model;

namespace Fishbone.Drawing.Drawers
{
    public class SkeletonDrawer : IDrawer<int>
    {
        private static ILog s_logger = LogManager.GetLogger(typeof(SkeletonDrawer));
        public virtual Size AdjustSize(IMatrix<int> mtx)
        {

            var minHeight = TestsConstants.ImageHeight;
            var minWidth = TestsConstants.ImageWidth;

            var maxImageHeight = TestsConstants.MaxImageHeight;
            var maxImageWidth = TestsConstants.MaxImageWidth;
            return AdjustSize(mtx.Cols, mtx.Rows,minHeight,minWidth,maxImageHeight, maxImageWidth);
        }

        public virtual Size AdjustSize(int cols, int rows, int minHeight, int minWidth, int maxHeight, int maxWidth)
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

        public virtual Bitmap DrawPart(IMatrix<int> matrix, out float scale, int col, int row, int cellx, int celly, int height, int width, string fileName)
        {
            Size adjustSize = AdjustSize(cellx, celly, height, width, height, width);

            scale = 1 / GetScaling(cellx, celly, adjustSize.Width, adjustSize.Height);
            Console.WriteLine("Scale {0}", scale);
            Console.WriteLine("Size {0}", adjustSize);
            var bmp = new Bitmap(adjustSize.Width, adjustSize.Height);

            var canvas = Graphics.FromImage(bmp);
            canvas.Clear(Color.Empty);

            var hw = adjustSize.Width / 2;
            var hh = adjustSize.Height / 2;
            var part1 = new Bitmap(adjustSize.Width / 2, adjustSize.Height / 2);
            var part2 = new Bitmap(adjustSize.Width / 2, adjustSize.Height / 2);
            var part3 = new Bitmap(adjustSize.Width / 2, adjustSize.Height / 2);
            var part4 = new Bitmap(adjustSize.Width / 2, adjustSize.Height / 2);

            var canvas1 = Graphics.FromImage(part1);
            canvas1.Clear(Color.Empty);
            var canvas2 = Graphics.FromImage(part2);
            canvas2.Clear(Color.Empty);
            var canvas3 = Graphics.FromImage(part3);
            canvas3.Clear(Color.Empty);
            var canvas4 = Graphics.FromImage(part4);
            canvas4.Clear(Color.Empty);

            var scaleCopy = scale;
            var hx = cellx / 2;
            var hy = celly / 2;
            try
            {
                var task1 = Task.Factory.StartNew(() => DrawBlock(matrix, canvas1, col, row, hx, hy, scaleCopy));
                var task2 = Task.Factory.StartNew(() => DrawBlock(matrix, canvas2, col + hx, row, hx, hy, scaleCopy));
                var task3 = Task.Factory.StartNew(() => DrawBlock(matrix, canvas3, col, row + hy, hx, hy, scaleCopy));
                var task4 = Task.Factory.StartNew(() => DrawBlock(matrix, canvas4, col + hx, row + hy, hx, hy, scaleCopy));

                Task.WaitAll(task1, task2, task3, task4);
            }
            catch (Exception exception)
            {
                s_logger.Error("Drawing was failed", exception);
            }

            canvas.DrawImage(part1, 0, 0);
            canvas.DrawImage(part2, hw, 0);
            canvas.DrawImage(part3, 0, hh);
            canvas.DrawImage(part4, hw, hh);

            //bmp.Save(fileName, ImageFormat.Png);
            //return canvas;
            return bmp;
        }

        protected virtual void DrawBlock(IMatrix<int> matrix, Graphics canvas, int col, int row, int cellx, int celly, float scale)
        {
            for (int r = row; r < row + celly; r++)
            {
                for (int c = col; c < col + cellx; c++)
                {
                    if (matrix[r, c] > 0)
                    {
                        var x = c - col;
                        var y = r - row;
                        canvas.FillRectangle(Brushes.Gray, x * scale, y * scale, scale, scale);
                        canvas.DrawRectangle(new Pen(Color.Black), x * scale, y * scale, scale, scale);
                    }
                }
            }
        }

        public virtual Size ComputeSize(float scale, int cellx, int celly)
        {
            int width = Convert.ToInt32(cellx*scale) + 1;
            int height = Convert.ToInt32(celly*scale) + 1;
            return new Size(width, height);
        }

        public virtual Graphics Draw(IMatrix<int> matrix, string fileName)
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