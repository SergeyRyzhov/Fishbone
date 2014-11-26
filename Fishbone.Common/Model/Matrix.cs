namespace Fishbone.Common.Model
{
    public abstract class Matrix<T>
    {
        private readonly MatrixRow<T>[] m_rows;

        protected Matrix(T[][] values)
        {
            Height = values.Length;
            Width = values[0].Length;

            m_rows = new MatrixRow<T>[Height];

            for (int i = 0; i < Height; i++)
            {
                m_rows[i] = new MatrixRow<T>(values[i]);
            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public MatrixRow<T> this[int row]
        {
            get { return m_rows[row]; }
            private set
            {
                /* set the specified col to value here */
            }
        }
    }
}