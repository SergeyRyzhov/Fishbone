using Fishbone.Drawing.Drawers;
using Fishbone.Parsing.Parsers;
using Window = System.Console;

namespace Fishbone.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Window.WriteLine("Invalid arguments");
                return;
            }

            Window.WriteLine("Start parsing arguments");
            IParser<int> parser = new MtxPortraitParser();
            var mtx = parser.Parse(args[0]);

            IDrawer<int> drawer = new SkeletonDrawer();
            drawer.Draw(mtx, args[0] + ".png");
        }
    }
}