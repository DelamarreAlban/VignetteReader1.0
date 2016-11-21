namespace VignetteReader1._0
{
    partial class vignetteReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadVignetteButton = new System.Windows.Forms.Button();
            this.vignetteBoxDisplay = new System.Windows.Forms.PictureBox();
            this.shapeDetectButton = new System.Windows.Forms.Button();
            this.colorPictureBox = new System.Windows.Forms.PictureBox();
            this.nodesList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nodesCount = new System.Windows.Forms.TextBox();
            this.dimensions = new System.Windows.Forms.Label();
            this.colorGraybutton = new System.Windows.Forms.Button();
            this.arrowDectectbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.vignetteBoxDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // loadVignetteButton
            // 
            this.loadVignetteButton.Location = new System.Drawing.Point(24, 23);
            this.loadVignetteButton.Margin = new System.Windows.Forms.Padding(6);
            this.loadVignetteButton.Name = "loadVignetteButton";
            this.loadVignetteButton.Size = new System.Drawing.Size(220, 44);
            this.loadVignetteButton.TabIndex = 0;
            this.loadVignetteButton.Text = "Load vignette";
            this.loadVignetteButton.UseVisualStyleBackColor = true;
            this.loadVignetteButton.Click += new System.EventHandler(this.loadVignetteButton_Click);
            // 
            // vignetteBoxDisplay
            // 
            this.vignetteBoxDisplay.Location = new System.Drawing.Point(24, 79);
            this.vignetteBoxDisplay.Margin = new System.Windows.Forms.Padding(6);
            this.vignetteBoxDisplay.Name = "vignetteBoxDisplay";
            this.vignetteBoxDisplay.Size = new System.Drawing.Size(976, 831);
            this.vignetteBoxDisplay.TabIndex = 1;
            this.vignetteBoxDisplay.TabStop = false;
            // 
            // shapeDetectButton
            // 
            this.shapeDetectButton.Location = new System.Drawing.Point(258, 23);
            this.shapeDetectButton.Margin = new System.Windows.Forms.Padding(6);
            this.shapeDetectButton.Name = "shapeDetectButton";
            this.shapeDetectButton.Size = new System.Drawing.Size(154, 44);
            this.shapeDetectButton.TabIndex = 3;
            this.shapeDetectButton.Text = "Shape";
            this.shapeDetectButton.UseVisualStyleBackColor = true;
            this.shapeDetectButton.Click += new System.EventHandler(this.detectButton_Click);
            // 
            // colorPictureBox
            // 
            this.colorPictureBox.Location = new System.Drawing.Point(1045, 79);
            this.colorPictureBox.Margin = new System.Windows.Forms.Padding(6);
            this.colorPictureBox.Name = "colorPictureBox";
            this.colorPictureBox.Size = new System.Drawing.Size(722, 683);
            this.colorPictureBox.TabIndex = 4;
            this.colorPictureBox.TabStop = false;
            // 
            // nodesList
            // 
            this.nodesList.FormattingEnabled = true;
            this.nodesList.ItemHeight = 25;
            this.nodesList.Location = new System.Drawing.Point(1791, 79);
            this.nodesList.Name = "nodesList";
            this.nodesList.Size = new System.Drawing.Size(417, 779);
            this.nodesList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(766, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nb nodes : ";
            // 
            // nodesCount
            // 
            this.nodesCount.Location = new System.Drawing.Point(895, 33);
            this.nodesCount.Name = "nodesCount";
            this.nodesCount.Size = new System.Drawing.Size(100, 31);
            this.nodesCount.TabIndex = 8;
            // 
            // dimensions
            // 
            this.dimensions.AutoSize = true;
            this.dimensions.Location = new System.Drawing.Point(641, 33);
            this.dimensions.Name = "dimensions";
            this.dimensions.Size = new System.Drawing.Size(70, 25);
            this.dimensions.TabIndex = 9;
            this.dimensions.Text = "label2";
            // 
            // colorGraybutton
            // 
            this.colorGraybutton.Location = new System.Drawing.Point(1233, 28);
            this.colorGraybutton.Name = "colorGraybutton";
            this.colorGraybutton.Size = new System.Drawing.Size(381, 34);
            this.colorGraybutton.TabIndex = 10;
            this.colorGraybutton.Text = "Color/Gray";
            this.colorGraybutton.UseVisualStyleBackColor = true;
            this.colorGraybutton.Click += new System.EventHandler(this.colorGraybutton_Click);
            // 
            // arrowDectectbutton
            // 
            this.arrowDectectbutton.Location = new System.Drawing.Point(421, 23);
            this.arrowDectectbutton.Name = "arrowDectectbutton";
            this.arrowDectectbutton.Size = new System.Drawing.Size(162, 44);
            this.arrowDectectbutton.TabIndex = 11;
            this.arrowDectectbutton.Text = "Arrows";
            this.arrowDectectbutton.UseVisualStyleBackColor = true;
            this.arrowDectectbutton.Click += new System.EventHandler(this.arrowDectectbutton_Click);
            // 
            // vignetteReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2220, 933);
            this.Controls.Add(this.arrowDectectbutton);
            this.Controls.Add(this.colorGraybutton);
            this.Controls.Add(this.dimensions);
            this.Controls.Add(this.nodesCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nodesList);
            this.Controls.Add(this.colorPictureBox);
            this.Controls.Add(this.shapeDetectButton);
            this.Controls.Add(this.vignetteBoxDisplay);
            this.Controls.Add(this.loadVignetteButton);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "vignetteReader";
            this.Text = "VignetteReader1.0";
            ((System.ComponentModel.ISupportInitialize)(this.vignetteBoxDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadVignetteButton;
        private System.Windows.Forms.PictureBox vignetteBoxDisplay;
        private System.Windows.Forms.Button shapeDetectButton;
        private System.Windows.Forms.PictureBox colorPictureBox;
        private System.Windows.Forms.ListBox nodesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nodesCount;
        private System.Windows.Forms.Label dimensions;
        private System.Windows.Forms.Button colorGraybutton;
        private System.Windows.Forms.Button arrowDectectbutton;
    }
}

