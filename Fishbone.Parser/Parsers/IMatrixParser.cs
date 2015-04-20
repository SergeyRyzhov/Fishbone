using Fishbone.Common.Model;

namespace Fishbone.Parsing.Parsers
{
    public interface IMatrixParser<out T>
    {
        IMatrix<T> Parse(string fileName);
    }
}