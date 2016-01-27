using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Fishbone.Common.Model;

using Newtonsoft.Json;

namespace Fishbone.Parsing.Parsers
{
    public class JsonEdge
    {
        public int From { get; set; }

        public int To { get; set; }

        public override int GetHashCode()
        {
            return From.GetHashCode() * 17 + To.GetHashCode();
        }
    }

    public class Comparer : IEqualityComparer<JsonEdge>
    {
        public bool Equals(JsonEdge x, JsonEdge y)
        {
            return /*x.From == y.From && x.To == y.To || */x.From == y.To && x.To == y.From;
        }

        public int GetHashCode(JsonEdge obj)
        {
            return obj.GetHashCode();
        }
    }

    public class JsonPortraitMatrixParser : IMatrixParser<int>
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
                    var json = JsonConvert.DeserializeObject<JsonEdge[]>(sr.ReadToEnd());

                    var vertices = json.SelectMany(e => new[] { e.From, e.To }).OrderBy(v => v).Distinct().ToArray();

                    var map = new Dictionary<int, int>();

                    int i = 0;
                    foreach (var vertex in vertices)
                    {
                        map[vertex] = i++;
                    }

                    cols = rows = vertices.Length;

                                                rowIndexes = new int[rows + 1];
//          

                    json =
                        json.Distinct(new Comparer())
                            .Where(e => e.From != e.To)
                            .OrderBy(e => e.From)
                            .ThenBy(e => e.To)
                            .ToArray();

                    colIndexes = new int[json.Length];

                    var row = 0;
                    var col = 0;
                    foreach (var edge in json)
                    {
                        while (map[edge.From] != row)
                        {
                            rowIndexes[row++] = col;
                        }

                        colIndexes[col++] = map[edge.To];
                    }

                    //                    var line = sr.ReadLine();
                    //                    var first = true;
                    //                    while (line != null)
                    //                    {
                    //                        if (line.Contains("%"))
                    //                        {
                    //                            line = sr.ReadLine();
                    //                            continue;
                    //                        }
                    //
                    //                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //                        if (first)
                    //                        {
                    //                            rows = int.Parse(parts[0]);
                    //                            cols = int.Parse(parts[1]);
                    //                            nz = int.Parse(parts[2]);
                    //                            rowIndexes = new int[rows + 1];
                    //                            colIndexes = new int[nz];
                    //                            first = false;
                    //                        }
                    //                        else
                    //                        {
                    //                            var c = int.Parse(parts[0]) - 1;
                    //                            var r = int.Parse(parts[1]) - 1;
                    //
                    //                            colIndexes[++currentPosition] = c;
                    //
                    //                            if (lastRow == r)
                    //                            {
                    //                            }
                    //                            else
                    //                            {
                    //                                lastRow = r;
                    //                                rowIndexes[currentRowPosition++] = currentPosition;
                    //                            }
                    //                        }
                    //                        line = sr.ReadLine();
                    //                    }
                    //
                    //
                    //                    rowIndexes[currentRowPosition++] = nz;
                }
            }

            var matrix = new CrsPortraitMatrix(cols, rows, rowIndexes, colIndexes);
            return matrix;
        }
    }
}
