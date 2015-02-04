using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Window = System.Console;

namespace Fishbone.Viewer
{
    public partial class FormViewer : Form
    {
        private ViewerWorker m_worker;
        private int col;
        private int row;
        private int cellx;
        private int celly;

        private int cols;
        private int rows;

        public FormViewer()
        {
            InitializeComponent();
            m_worker = new ViewerWorker();
            col = 0;
            row = 0;
            cellx = 10;
            celly = 10;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var res = openMatrix.ShowDialog();
            if (res == DialogResult.OK)
            {
                m_worker.Open(openMatrix.FileName, out cols, out rows);


                UpdateCurrent();
            }
        }

        private void UpdateCurrent()
        {
            m_worker.DrawPart(col, row, cellx, celly, pbMainView.Height, pbMainView.Width, "current");

            using (var fs = new FileStream("current.png", FileMode.Open, FileAccess.Read))
            {
                pbMainView.Image = Image.FromStream(fs);
                pbMainView.Refresh();
                fs.Close();
            }

            m_worker.DrawThumbnail(col, row, cellx, celly, pbThumbnail.Height, pbThumbnail.Width, "thumbnail");
            using (var fs = new FileStream("thumbnail.png", FileMode.Open, FileAccess.Read))
            {
                pbThumbnail.Image = Image.FromStream(fs);
                pbThumbnail.Refresh();
                fs.Close();
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveThumbnail_Click(object sender, EventArgs e)
        {
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (row > 0)
            {
                row -= celly/2;
            }

            if (row < 0)
            {
                row = 0;
            }
            UpdateCurrent();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {

            if (row < rows - celly)
            {
                row += celly/2;
            }

            if (row > rows - celly)
            {
                row = rows - celly;
            }

            UpdateCurrent();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (col > 0)
            {
                col -= cellx/2;
            }

            if (col < 0)
            {
                col = 0;
            }

            UpdateCurrent();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {

            if (col < cols - cellx)
            {
                col += cellx/2;
            }

            if (col > cols - cellx)
            {
                col = cols - cellx;
            }

            UpdateCurrent();

        }

        private void btnIncrese_Click(object sender, EventArgs e)
        {
            if (cellx > 1)
            {
                cellx /= 2;

                celly /= 2;
            }
            if (cellx < 1)
            {
                cellx = 1;
                celly = 1;
            }

            UpdateCurrent();
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {

            if (cellx < cols)
            {
                cellx *= 2;

                celly *= 2;
            }

            if (cellx > cols)
            {
                cellx = cols;
                celly = rows;
            }

            UpdateCurrent();
            UpdateCurrent();
        }
    }
}
