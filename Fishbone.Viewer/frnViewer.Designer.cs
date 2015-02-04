namespace Fishbone.Viewer
{
    partial class FormViewer
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openMatrix = new System.Windows.Forms.OpenFileDialog();
            this.saveMatrix = new System.Windows.Forms.SaveFileDialog();
            this.pbMainView = new System.Windows.Forms.PictureBox();
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSaveCurrent = new System.Windows.Forms.Button();
            this.btnSaveThumbnail = new System.Windows.Forms.Button();
            this.btnIncrese = new System.Windows.Forms.Button();
            this.btnDecrease = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(559, 146);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(559, 204);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.DimGray;
            // 
            // openMatrix
            // 
            this.openMatrix.FileName = "file.mtx";
            // 
            // pbMainView
            // 
            this.pbMainView.Location = new System.Drawing.Point(12, 12);
            this.pbMainView.Name = "pbMainView";
            this.pbMainView.Size = new System.Drawing.Size(512, 512);
            this.pbMainView.TabIndex = 2;
            this.pbMainView.TabStop = false;
            // 
            // pbThumbnail
            // 
            this.pbThumbnail.Location = new System.Drawing.Point(530, 12);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(128, 128);
            this.pbThumbnail.TabIndex = 3;
            this.pbThumbnail.TabStop = false;
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(600, 175);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(58, 23);
            this.btnRight.TabIndex = 4;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(530, 175);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(64, 23);
            this.btnLeft.TabIndex = 5;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(530, 443);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(128, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSaveCurrent
            // 
            this.btnSaveCurrent.Location = new System.Drawing.Point(530, 472);
            this.btnSaveCurrent.Name = "btnSaveCurrent";
            this.btnSaveCurrent.Size = new System.Drawing.Size(128, 23);
            this.btnSaveCurrent.TabIndex = 7;
            this.btnSaveCurrent.Text = "Save current";
            this.btnSaveCurrent.UseVisualStyleBackColor = true;
            this.btnSaveCurrent.Click += new System.EventHandler(this.btnSaveCurrent_Click);
            // 
            // btnSaveThumbnail
            // 
            this.btnSaveThumbnail.Location = new System.Drawing.Point(530, 501);
            this.btnSaveThumbnail.Name = "btnSaveThumbnail";
            this.btnSaveThumbnail.Size = new System.Drawing.Size(128, 23);
            this.btnSaveThumbnail.TabIndex = 8;
            this.btnSaveThumbnail.Text = "Save thumbnail";
            this.btnSaveThumbnail.UseVisualStyleBackColor = true;
            this.btnSaveThumbnail.Click += new System.EventHandler(this.btnSaveThumbnail_Click);
            // 
            // btnIncrese
            // 
            this.btnIncrese.Location = new System.Drawing.Point(530, 233);
            this.btnIncrese.Name = "btnIncrese";
            this.btnIncrese.Size = new System.Drawing.Size(75, 23);
            this.btnIncrese.TabIndex = 9;
            this.btnIncrese.Text = "Increase";
            this.btnIncrese.UseVisualStyleBackColor = true;
            this.btnIncrese.Click += new System.EventHandler(this.btnIncrese_Click);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Location = new System.Drawing.Point(530, 262);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(75, 23);
            this.btnDecrease.TabIndex = 10;
            this.btnDecrease.Text = "Decrease";
            this.btnDecrease.UseVisualStyleBackColor = true;
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 537);
            this.Controls.Add(this.btnDecrease);
            this.Controls.Add(this.btnIncrese);
            this.Controls.Add(this.btnSaveThumbnail);
            this.Controls.Add(this.btnSaveCurrent);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.pbThumbnail);
            this.Controls.Add(this.pbMainView);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Matrix viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pbMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openMatrix;
        private System.Windows.Forms.SaveFileDialog saveMatrix;
        private System.Windows.Forms.PictureBox pbMainView;
        private System.Windows.Forms.PictureBox pbThumbnail;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSaveCurrent;
        private System.Windows.Forms.Button btnSaveThumbnail;
        private System.Windows.Forms.Button btnIncrese;
        private System.Windows.Forms.Button btnDecrease;
    }
}

