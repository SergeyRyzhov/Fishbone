using System;
using Fishbone.Common.Model;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fishbone.Drawing.Drawers
{
    public interface IDrawer<in T>
    {
        Graphics Draw(IMatrix<T> matrix, string fileName);
    }

    public class TestsConstants
    {
        public static int Scale = 20;

        public static int ImageHeight = 1024;

        public static int ImageWidth = 1024;

        public static int MaxImageHeight = 2048;

        public static int MaxImageWidth = 2048;
    }

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

        public Graphics Draw(IMatrix<int> matrix, string fileName)
        {
            int integerScale = TestsConstants.Scale;
            float scale = integerScale;


            Size adjustSize = AdjustSize(matrix);
            var bmp = new Bitmap(adjustSize.Width, adjustSize.Height);

            var canvas = Graphics.FromImage(bmp);
            canvas.Clear(Color.White);

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