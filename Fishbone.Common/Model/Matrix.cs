using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fishbone.Common.Model
{
    public interface IMatrix<out T>: IEnumerable<Cell>
    {
        T this[int row, int col] { get;}

        int Cols { get; }

        int Rows { get; }
    }

    public class Cell
    {
        public int Col;
        public int Row;
    }

    public class CrsPortraitMatrix : IMatrix<int>
    {
        public override int GetHashCode()
        {
            return 17 * Cols + Rows;
        }

        private readonly int[] m_rowIndex;
        private readonly int[] m_colIndex;

        public CrsPortraitMatrix(int cols, int rows, int nz)
        {
            Cols = cols;
            Rows = rows;

            m_rowIndex = new int[rows + 1];
            m_colIndex = new int[nz];
        }

        public CrsPortraitMatrix(int rows, int cols, int[] rowIndex, int[] colIndex)
        {
            Rows = rows;
            Cols = cols;
            m_rowIndex = rowIndex;
            m_colIndex = colIndex;
        }

        public int this[int row, int col]
        {
            get
            {
                try
                {
                    var @from = m_rowIndex[row];
                    var @to = m_rowIndex[row + 1];

                    var @symFrom = m_rowIndex[col];
                    var @symTo = m_rowIndex[col + 1];
                    return m_colIndex.Skip(@from).Take(@to - @from).Contains(col)
                        ? 1
                        : m_colIndex.Skip(@symFrom).Take(@symTo - @symFrom).Contains(row) ? 1 : 0;
                }
                catch (IndexOutOfRangeException exception)
                {
                    //Console.WriteLine("Matrix are not symmetric");
                    return 0;
                }
            }
        }

        public int Cols { get; private set; }
        public int Rows { get; private set; }
        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 0; i < Rows; i++)
            {
                var row = m_rowIndex[i];
                var nrow = m_rowIndex[i+1];

                for (int j = row; j < nrow; j++)
                {
                    yield return new Cell
                    {
                        Row = i,
                        Col = m_colIndex[j]
                    };
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}