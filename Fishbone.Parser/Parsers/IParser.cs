using Fishbone.Common.Model;

namespace Fishbone.Parsing.Parsers
{
    public interface IParser<T>
    {
        Matrix<T> Parse(string fileName);
    }
}