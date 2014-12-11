using Fishbone.Common.Model;
using System.Drawing;

namespace Fishbone.Drawing.Drawers
{
    public interface IDrawer<in T>
    {
        Graphics Draw(IMatrix<T> matrix, string fileName);
    }
}