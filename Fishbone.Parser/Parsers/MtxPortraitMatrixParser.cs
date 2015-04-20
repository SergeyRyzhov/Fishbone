using Fishbone.Common.Model;
using System;
using System.IO;

namespace Fishbone.Parsing.Parsers
{
    public class MtxPortraitMatrixParser : IMatrixParser<int>
    {
        public IMatrix<int> Parse(string fileName)
        {
            int rows = 0;
            int cols = 0;
            int[] rowIndexes = null;
            int[] colIndexes = null;

            int currentPosition = -1;
            int currentRowPosition = 1;
            int lastRow = 0;
            int nz = 0;

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
                            nz = int.Parse(parts[2]);
                            rowIndexes = new int[rows + 1];
                            colIndexes = new int[nz];
                            first = false;
                        }
                        else
                        {
                            var c = int.Parse(parts[0]) - 1;
                            var r = int.Parse(parts[1]) - 1;

                            colIndexes[++currentPosition] = c;

                            if (lastRow == r)
                            {
                            }
                            else
                            {
                                lastRow = r;
                                rowIndexes[currentRowPosition++] = currentPosition;
                            }
                        }
                        line = sr.ReadLine();
                    }


                    rowIndexes[currentRowPosition++] = nz;
                }
            }

            var matrix = new CrsPortraitMatrix(cols, rows, rowIndexes, colIndexes);
            return matrix;
        }
    }
}