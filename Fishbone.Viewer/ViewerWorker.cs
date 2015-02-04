using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Fishbone.Common.Model;
using Fishbone.Drawing.Drawers;
using Fishbone.Parsing.Parsers;

namespace Fishbone.Viewer
{
    public class ViewerWorker
    {
        private readonly IParser<int> m_parser;
        private IMatrix<int> m_matrix;

        public ViewerWorker()
        {
            m_parser = new MtxPortraitParser();
        }

        public void Open(string filePath, out int cols, out int rows)
        {
//            m_name = Path.GetFileNameWithoutExtension(filePath);
            var timer = new Stopwatch();
            timer.Start();
            m_matrix = m_parser.Parse(filePath);
            timer.Stop();
            cols = m_matrix.Cols;
            rows = m_matrix.Rows;
        }

        public string DrawPart(int col, int row, int cellx, int celly, int height, int width, string name)
        {
            IDrawer<int> drawer = new SkeletonDrawer();
            var fileName = name + ".png";
            float scale;
            Bitmap bmp = drawer.DrawPart(m_matrix, out scale, row, col, cellx, celly, height, width, fileName);
            bmp.Save(fileName, ImageFormat.Png);
            return fileName;
        }

        public string DrawThumbnail(int col, int row, int cellx, int celly, int height, int width, string name)
        {
            IDrawer<int> drawer = new SkeletonDrawer();
            var fileName = name + ".png";
            float scale;
            Bitmap bmp = drawer.DrawPart(m_matrix, out scale, 0, 0, m_matrix.Cols, m_matrix.Rows, height, width,
                fileName);
            int x = Convert.ToInt32(col*scale);
            int y = Convert.ToInt32(row*scale);
            int w = Convert.ToInt32(cellx*scale);
            int h = Convert.ToInt32(celly*scale);
            var canvas = Graphics.FromImage(bmp);
            canvas.DrawRectangle(new Pen(Color.Red), x, y, w, h);

            bmp.Save(fileName, ImageFormat.Png);
            return fileName;
        }

    }
}