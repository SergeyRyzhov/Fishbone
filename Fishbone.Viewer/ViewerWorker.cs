using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Fishbone.Common.Model;
using Fishbone.Common.Utilites;
using Fishbone.Drawing.Drawers;
using Fishbone.Drawing.Extended;
using Fishbone.Parsing.Parsers;

using log4net;

namespace Fishbone.Viewer
{
    public class ViewerWorker
    {
        private static readonly ILog s_logger = LogManager.GetLogger(typeof(ViewerWorker));
        private readonly Dictionary<string,IMatrixParser<int>> m_matrixParser;
        private IMatrix<int> m_matrix;
        private readonly Random m_rnd = new Random();
        private CachedDrawer m_drawer;
        private IDecorationParser m_decoretionParser;

        public ViewerWorker()
        {
            m_matrixParser = new Dictionary<string, IMatrixParser<int>>()
                                 {
                                     { ".mtx", new MtxPortraitMatrixParser() },
                                     { ".json", new JsonPortraitMatrixParser() }
                                 };

            m_decoretionParser = new DecorationParser();
        }

        public void Open(string filePath, out int cols, out int rows)
        {
            //            m_name = Path.GetFileNameWithoutExtension(filePath);
            var timer = new Stopwatch();
            timer.Start();


            m_matrix = m_matrixParser[Path.GetExtension(filePath)].Parse(filePath);
            timer.Stop();
            s_logger.DebugFormat("Parsing was elapsed: {0}", timer.Elapsed);
            cols = m_matrix.Cols;
            rows = m_matrix.Rows;

            m_drawer = new CachedDrawer(new MatrixDecoration(new int[0]));
        }

        public void LoadColoring(string filePath)
        {
            var dec = m_decoretionParser.Parse(filePath);
            m_drawer = new CachedDrawer(dec);
            Cache.Instance.Clear();
        }

        public string DrawPart(int col, int row, int cellx, int celly, int height, int width, string name)
        {
            var fileName = name + ".png";
            float scale;
            var timer = new Stopwatch();
            timer.Start();
            Bitmap bmp = m_drawer.DrawPart(m_matrix, out scale, col, row, cellx, celly, height, width, fileName);
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
            var fileName = name + ".png";

            Stopwatch timer = new Stopwatch();
            timer.Start();
            float scale;
            Bitmap bmp = m_drawer.Draw(m_matrix, fileName, height, width, out scale);
            int x = Convert.ToInt32(col * scale);
            int y = Convert.ToInt32(row * scale);
            int w = Convert.ToInt32(cellx * scale);
            int h = Convert.ToInt32(celly * scale);
            var canvas = Graphics.FromImage(bmp);
            var pen = new Pen(Color.Red) { Width = 2 };
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