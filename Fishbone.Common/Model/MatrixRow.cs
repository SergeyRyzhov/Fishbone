namespace Fishbone.Common.Model
{
    public class MatrixRow<T>
    {
        private readonly T[] m_rowValues;

        public MatrixRow(T[] rowValues)
        {
            m_rowValues = rowValues;
            Size = m_rowValues.Length;
        }

        public int Size { get; private set; }

        public T this[int col]
        {
            get { return m_rowValues[col]; }
            private set
            {
                /* set the specified col to value here */
            }
        }
    }
}