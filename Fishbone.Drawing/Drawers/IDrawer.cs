using Fishbone.Common.Model;
using System.Drawing;

namespace Fishbone.Drawing.Drawers
{
    public interface IDrawer<in T>
    {
        Graphics Draw(IMatrix<T> matrix, string fileName);
        Bitmap DrawPart(IMatrix<int> matrix, out float scale, int col, int row, int cellx, int celly, int height, int width, string fileName);
    }
}