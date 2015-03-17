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
            IDrawer<int> drawer = new SkeletonDrawer();
            var fileName = name + ".png";
            float scale;
            var timer = new Stopwatch();
            timer.Start();
            Bitmap bmp = drawer.DrawPart(m_matrix, out scale, row, col, cellx, celly, height, width, fileName);
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
            IDrawer<int> drawer = new SkeletonDrawer();
            var fileName = name + ".png";
            if (m_matrix.Cols > 2000 || m_matrix.Rows > 2000)
            {
                s_logger.WarnFormat("Drawing thumbnail was skipped. Matrix very big");
                return fileName;
            }

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