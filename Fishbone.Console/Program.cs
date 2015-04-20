using System.Diagnostics;
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
            IMatrixParser<int> matrixParser = new MtxPortraitMatrixParser();
            var timer = new Stopwatch();
            timer.Start();
            var mtx = matrixParser.Parse(args[0]);
            timer.Stop();
            Window.WriteLine("Parsing: {0}", timer.Elapsed);
            timer.Reset();
            timer.Start();

            IDrawer<int> drawer = new SkeletonDrawer(new BlockMatrixDecoration(new int[0]));
            float scale;
            drawer.Draw(mtx, args[0] + ".png", 256, 256, out scale);
            timer.Stop();

            Window.WriteLine("Drawing: {0}", timer.Elapsed);
        }
    }
}