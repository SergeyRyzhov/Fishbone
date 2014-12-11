using System;
using System.Drawing;
using System.Drawing.Imaging;
using Fishbone.Common.Model;

namespace Fishbone.Drawing.Drawers
{
    public class SkeletonDrawer : IDrawer<int>
    {
        public static Size AdjustSize(IMatrix<int> mtx)
        {
            float width = mtx.Cols * TestsConstants.Scale;
            float height = mtx.Rows * TestsConstants.Scale;

            float scaleHeight = height < TestsConstants.ImageHeight ? TestsConstants.ImageHeight / height : 1;
            float scaleWidth = width < TestsConstants.ImageWidth ? TestsConstants.ImageWidth / width : 1;

            float scale = Math.Max(scaleHeight, scaleWidth);

            height *= scale;
            width *= scale;

            scaleHeight = height > TestsConstants.MaxImageHeight ? TestsConstants.MaxImageHeight / height : 1;
            scaleWidth = width > TestsConstants.MaxImageWidth ? TestsConstants.MaxImageWidth / width : 1;

            scale = Math.Min(scaleHeight, scaleWidth);

            height *= scale;
            width *= scale;

            return new Size((int)width, (int)height);
        }

        protected static float GetScaling(float fieldWidth, float fieldHeight, float width, float height)
        {
            float scaling = fieldWidth / width < fieldHeight / height ? fieldWidth / width : fieldHeight / height;
            return scaling;
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