using System.Collections.Generic;
using System.IO;

namespace Fishbone.Parsing.Parsers
{
    public class DecorationParser : IDecorationParser
    {
        public MatrixDecoration Parse(string filePath)
        {
            //int n = 0;
            int type = 0;
//            int i = 0;
            List<int> mask = new List<int>();
            using (var file = new FileStream(filePath, FileMode.Open))
            {
                using (var sr = new StreamReader(file))
                {
                    var line = sr.ReadLine();
                    var first = true;
                    while (line != null)
                    {
                        if (first)
                        {
                            var parts = line.Split(' ');
                            //n = int.Parse(parts[0]);
                            type = int.Parse(parts[1]);
                            //mask = new int[n];
                            first = false;
                        }
                        else
                        {
                            mask.Add(int.Parse(line));
                        }

                        line = sr.ReadLine();


                    }
                }
            }

            if (type == 0)
            {
                return new LineMatrixDecoration(mask.ToArray());
            }
            return new BlockMatrixDecoration(mask.ToArray());
        }
    }
}