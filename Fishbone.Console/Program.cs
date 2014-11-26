using Window = System.Console;

namespace Fishbone.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Window.WriteLine("Invalid arguments");
                return;
            }

            Window.WriteLine("Start parsing arguments");

        }
    }

    interface IParser
    {
         
    }

    class MtxParser : IParser
    {
        
    }
}
