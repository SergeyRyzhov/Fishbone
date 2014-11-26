using Fishbone.Common.Model;
using System;
using System.IO;

namespace Fishbone.Parsing.Parsers
{
    public class MtxPortraitParser : IParser<int>
    {
        public Matrix<int> Parse(string fileName)
        {
            int[][] mtx = null;
            int rows = 0;
            int cols = 0;

            using (var file = new FileStream(fileName, FileMode.Open))
            {
                using (var sr = new StreamReader(file))
                {
                    var line = sr.ReadLine();
                    var first = true;
                    while (line != null)
                    {
                        if (line.Contains("%"))
                        {
                            line = sr.ReadLine();
                            continue;
                        }

                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (first)
                        {
                            rows = int.Parse(parts[0]);
                            cols = int.Parse(parts[1]);

                            mtx = new int[rows][];
                            for (int i = 0; i < rows; i++)
                            {
                                mtx[i] = new int[cols];
                            }

                            first = false;
                        }
                        else
                        {
                            var r = int.Parse(parts[0]) - 1;
                            var c = int.Parse(parts[1]) - 1;

                            mtx[r][c] = 1;
                            if (rows == cols)
                            {
                                mtx[c][r] = 1;
                            }
                        }
                        line = sr.ReadLine();
                    }
                }
            }

            var matrix = new IntMatrix(mtx);
            return matrix;
        }
    }
}