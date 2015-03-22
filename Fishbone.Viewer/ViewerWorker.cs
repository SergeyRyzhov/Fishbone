using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using log4net;
using Fishbone.Common.Model;
using Fishbone.Drawing.Drawers;
using Fishbone.Drawing.Extended;
using Fishbone.Parsing.Parsers;

namespace Fishbone.Viewer
{
    public class ViewerWorker
    {
        private static ILog s_logger = LogManager.GetLogger(typeof(ViewerWorker));
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
            s_logger.DebugFormat("Parsing was elapsed: {0}", timer.Elapsed);
            cols = m_matrix.Cols;
            rows = m_matrix.Rows;
        }

        public string DrawPart(int col, int row, int cellx, int celly, int height, int width, string name)
        {
            IDrawer<int> drawer = new CachedDrawer();
            var fileName = name + ".png";
            float scale;
            var timer = new Stopwatch();
            timer.Start();
            Bitmap bmp = drawer.DrawPart(m_matrix, out scale, col, row, cellx, celly, height, width, fileName);
            timer.Stop();
            s_logger.DebugFormat("Drawing was elapsed: {0}", timer.Elapsed);

            timer.Restart();
            bmp.Save(fileName, ImageFormat.Png);
            timer.Stop();
            s_logger.DebugFormat("Saving was elapsed: {0}", timer.Elapsed);
            return fileName;
        }

        public string DrawThumbnail(int col, int row, int cellx, int celly, int height, int width, string name)
        {
            IDrawer<int> drawer = new CachedDrawer();
            var fileName = name + ".png";

            Stopwatch timer = new Stopwatch();
            timer.Start();
            float scale;
            Bitmap bmp = drawer.Draw(m_matrix, fileName, height, width, out scale);
            int x = Convert.ToInt32(col*scale);
            int y = Convert.ToInt32(row*scale);
            int w = Convert.ToInt32(cellx*scale);
            int h = Convert.ToInt32(celly*scale);
            var canvas = Graphics.FromImage(bmp);
            var pen = new Pen(Color.Red) {Width = 2};
            canvas.DrawRectangle(pen, x, y, w, h);
            timer.Stop();
            s_logger.DebugFormat("Drawing thumbnail was elapsed: {0}", timer.Elapsed);

            timer.Restart();
            bmp.Save(fileName, ImageFormat.Png);
            timer.Stop();
            s_logger.DebugFormat("Saving thumbnail was elapsed: {0}", timer.Elapsed);
            return fileName;
        }

    }
}