using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using Fishbone.Common.Model;
using Fishbone.Common.Utilites;
using Fishbone.Drawing.Drawers;
using log4net;

namespace Fishbone.Drawing.Extended
{
    public class CachedDrawer : SkeletonDrawer
    {
        private static ILog s_logger = LogManager.GetLogger(typeof(CachedDrawer));
        private Cache m_cache;

        public CachedDrawer()
        {
            m_cache = CacheManager.Cache;
        }

        public override Bitmap DrawPart(IMatrix<int> matrix, out float scale, int col, int row, int cellx, int celly, int height, int width,
            string fileName)
        {
            var bitmapKey = string.Format("DrawPart mtx:{0}, col:{1}, row:{2}, cellx:{3}, celly:{4}, height:{5}, width:{6}",
                matrix.GetHashCode(), col, row, cellx, celly, height, width);
            if (m_cache[bitmapKey] != null)
            {
                s_logger.DebugFormat("From cache, key: {0}", bitmapKey);
                dynamic source = m_cache[bitmapKey];

                byte[] cbitmap = source.bitmap;
                float[] cscale = source.scale;
                using (MemoryStream stream = new MemoryStream((byte[])cbitmap))
                {
                    using (Bitmap bmp = new Bitmap(stream))
                    {
                        scale = cscale[0];
                        return bmp;
                    }
                }
            }
            var bitmap = base.DrawPart(matrix, out scale, col, row, cellx, celly, height, width, fileName);
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                dynamic cacheData = new
                {
                    bitmap = ms.ToArray(),
                    scale = new float[1] { scale }
                };
                m_cache.Insert(bitmapKey, cacheData);
                s_logger.DebugFormat("Saveed to cache, key: {0}", bitmapKey);
            }

            return bitmap;
        }

        public override Graphics Draw(IMatrix<int> matrix, string fileName)
        {
            return base.Draw(matrix, fileName);
        }
    }

    //    class CachedDrawer : IDrawer<int>, IExtendedDrawer<int>
    //    {
    //        private readonly IDrawer<int> m_drawer;
    //        private Cache m_cache;
    //
    //        public CachedDrawer(IDrawer<int> drawer)
    //        {
    //            m_drawer = drawer;
    //            m_cache = new Cache();
    //        }
    //
    //        public Graphics Draw(IMatrix<int> matrix, string fileName)
    //        {
    //            return m_drawer.Draw(matrix, fileName);
    //        }
    //
    //        public Bitmap DrawPart(IMatrix<int> matrix, out float scale, int col, int row, int cellx, int celly, int height, int width,
    //            string fileName)
    //        {
    //            string key = string.Format("");
    //
    //            if (m_cache[key] != null)
    //            {
    //                using (MemoryStream ms1 = new MemoryStream((byte[])m_cache[key]))
    //                {
    //                    using (Bitmap bmp = new Bitmap(ms1))
    //                    {
    //                        (m_drawer as SkeletonDrawer).
    //                        return bmp;
    //                    }
    //                }
    //            }
    //
    //            var bitmap = m_drawer.DrawPart(matrix, out scale, col, row, cellx, celly, height, width, fileName);
    //            using (MemoryStream ms = new MemoryStream())
    //            {
    //                bitmap.Save(ms, bitmap.RawFormat);
    //                m_cache.Insert(key, (byte[])ms.ToArray());
    //            }
    //            return bitmap;
    //        }
    //
    //        public
    //        IDrawer<int> Internal
    //        {
    //            get { return m_drawer; }
    //        }
    //    }
}
