using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using log4net;
using Window = System.Console;

namespace Fishbone.Viewer
{
    public partial class FormViewer : Form
    {
        private static readonly ILog s_logger = LogManager.GetLogger(typeof(FormViewer));
        private readonly ViewerWorker m_worker;
        private int m_cellx;
        private int m_celly;
        private int m_col;
        private int m_cols;
        private int m_row;
        private int m_rows;

        public FormViewer()
        {
            InitializeComponent();
            m_worker = new ViewerWorker();
            m_col = 0;
            m_row = 0;
            m_cellx = 10;
            m_celly = 10;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var res = openMatrix.ShowDialog();
            if (res == DialogResult.OK)
            {
                m_worker.Open(openMatrix.FileName, out m_cols, out m_rows);

                UpdateCurrent();
            }
        }

        private void ToggleState(bool state)
        {
            btnLeft.Enabled = state;
            btnRight.Enabled = state;
            btnUp.Enabled = state;
            btnDown.Enabled = state;

            btnUpLeft.Enabled = state;
            btnUpRight.Enabled = state;
            btnDownRight.Enabled = state;
            btnDownLeft.Enabled = state;

            btnIncrese.Enabled = state;
            btnDecrease.Enabled = state;
            btnOpen.Enabled = state;

            btnSaveCurrent.Enabled = state;
            btnSaveThumbnail.Enabled = state;

            btnLoadColoring.Enabled = state;
        }

        private void UpdateCurrent()
        {
            try
            {
                ToggleState(false);
                m_worker.DrawPart(m_col, m_row, m_cellx, m_celly, pbMainView.Height, pbMainView.Width, "current");

                using (var fs = new FileStream("current.png", FileMode.Open, FileAccess.Read))
                {
                    pbMainView.Image = Image.FromStream(fs);
                    pbMainView.Refresh();
                    fs.Close();
                }

                m_worker.DrawThumbnail(m_col, m_row, m_cellx, m_celly, pbThumbnail.Height, pbThumbnail.Width,
                    "thumbnail");

                if (File.Exists("thumbnail.png"))
                {
                    using (var fs = new FileStream("thumbnail.png", FileMode.Open, FileAccess.Read))
                    {
                        pbThumbnail.Image = Image.FromStream(fs);
                        pbThumbnail.Refresh();
                        fs.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                s_logger.Error("Updating was failed", exception);
            }
            finally
            {
                ToggleState(true);
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleState(false);

                if (saveMatrix.ShowDialog() == DialogResult.OK)
                {
                    m_worker.DrawPart(m_col, m_row, m_cellx, m_celly, 4096, 4096, "snapshot");
                    File.Move("snapshot.png", saveMatrix.FileName);
                }
            }
            catch (Exception exception)
            {
                s_logger.Error("Updating was failed", exception);
            }
            finally
            {
                ToggleState(true);
            }
        }

        private void btnSaveThumbnail_Click(object sender, EventArgs e)
        {

            try
            {
                ToggleState(false);
                if (saveMatrix.ShowDialog() == DialogResult.OK)
                {
                    ToggleState(false);
                    m_worker.DrawThumbnail(m_col, m_row, m_cellx, m_celly, 4096, 4096, "snapshot");
                    File.Move("snapshot.png", saveMatrix.FileName);
                }
            }
            catch (Exception exception)
            {
                s_logger.Error("Updating was failed", exception);
            }
            finally
            {
                ToggleState(true);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveUp();
            UpdateCurrent();
        }

        private void MoveUp()
        {
            if (m_row > 0)
            {
                m_row -= m_celly / 2;
            }

            if (m_row < 0)
            {
                m_row = 0;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveDown();

            UpdateCurrent();
        }

        private void MoveDown()
        {
            if (m_row < m_rows - m_celly)
            {
                m_row += m_celly / 2;
            }

            if (m_row > m_rows - m_celly)
            {
                m_row = m_rows - m_celly;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveLeft();

            UpdateCurrent();
        }

        private void MoveLeft()
        {
            if (m_col > 0)
            {
                m_col -= m_cellx / 2;
            }

            if (m_col < 0)
            {
                m_col = 0;
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveRight();

            UpdateCurrent();
        }

        private void MoveRight()
        {
            if (m_col < m_cols - m_cellx)
            {
                m_col += m_cellx / 2;
            }

            if (m_col > m_cols - m_cellx)
            {
                m_col = m_cols - m_cellx;
            }
        }

        private void btnIncrese_Click(object sender, EventArgs e)
        {
            Increase();
            UpdateCurrent();
        }

        private void Increase()
        {
            if (this.m_cellx > 1)
            {
                this.m_cellx /= 2;
                this.m_celly /= 2;
            }
            if (this.m_cellx < 1)
            {
                this.m_cellx = 1;
                this.m_celly = 1;
            }
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            Decrease();
            UpdateCurrent();
        }

        private void Decrease()
        {
            if (this.m_cellx < this.m_cols)
            {
                this.m_cellx *= 2;

                this.m_celly *= 2;
            }

            if (this.m_cellx > this.m_cols)
            {
                this.m_cellx = this.m_cols;
                this.m_celly = this.m_rows;
            }
        }

        private void btnUpLeft_Click(object sender, EventArgs e)
        {
            MoveUp();
            MoveLeft();
            UpdateCurrent();
        }

        private void btnUpRight_Click(object sender, EventArgs e)
        {
            MoveUp();
            MoveRight();
            UpdateCurrent();
        }

        private void btnDownLeft_Click(object sender, EventArgs e)
        {
            MoveDown();
            MoveLeft();
            UpdateCurrent();
        }

        private void btnDownRight_Click(object sender, EventArgs e)
        {
            MoveDown();
            MoveRight();
            UpdateCurrent();
        }

        private void btnLoadColoring_Click(object sender, EventArgs e)
        {
            var res = openColor.ShowDialog();
            if (res == DialogResult.OK)
            {
                m_worker.LoadColoring(openColor.FileName);

                UpdateCurrent();
            }
        }

        private void FormViewer_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.NumPad8:
                    {
                        MoveUp();
                        UpdateCurrent();
                        return;
                    }

                case Keys.NumPad2:
                case Keys.S:
                    {
                        MoveDown();
                        UpdateCurrent();
                        return;
                    }

                case Keys.NumPad6:
                case Keys.D:
                    {
                        MoveRight();
                        UpdateCurrent();
                        return;
                    }

                case Keys.NumPad4:
                case Keys.A:
                    {
                        MoveLeft();
                        UpdateCurrent();
                        return;
                    }


                case Keys.NumPad1:
                    {
                        MoveLeft();
                        MoveDown();
                        UpdateCurrent();
                        return;
                    }


                case Keys.NumPad7:
                    {
                        MoveLeft();
                        MoveUp();
                        UpdateCurrent();
                        return;
                    }


                case Keys.NumPad9:
                    {
                        MoveRight();
                        MoveUp();
                        UpdateCurrent();
                        return;
                    }


                case Keys.NumPad3:
                    {
                        MoveRight();
                        MoveDown();
                        UpdateCurrent();
                        return;
                    }

                case Keys.Subtract:
                    {
                        Decrease();
                        UpdateCurrent();
                        return;
                    }

                case Keys.Add:
                    {
                        Increase();
                        UpdateCurrent();
                        return;
                    }
            }
        }
    }
}