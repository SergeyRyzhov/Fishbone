using Fishbone.Common.Model;

namespace Fishbone.Parsing.Parsers
{
    public interface IParser<T>
    {
        IMatrix<T> Parse(string fileName);
    }
}