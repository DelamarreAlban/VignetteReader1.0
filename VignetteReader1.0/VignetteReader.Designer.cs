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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.edgesList = new System.Windows.Forms.ListBox();
            this.connectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.vignetteBoxDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // loadVignetteButton
            // 
            this.loadVignetteButton.Location = new System.Drawing.Point(12, 12);
            this.loadVignetteButton.Name = "loadVignetteButton";
            this.loadVignetteButton.Size = new System.Drawing.Size(110, 23);
            this.loadVignetteButton.TabIndex = 0;
            this.loadVignetteButton.Text = "Load vignette";
            this.loadVignetteButton.UseVisualStyleBackColor = true;
            this.loadVignetteButton.Click += new System.EventHandler(this.loadVignetteButton_Click);
            // 
            // vignetteBoxDisplay
            // 
            this.vignetteBoxDisplay.Location = new System.Drawing.Point(12, 41);
            this.vignetteBoxDisplay.Name = "vignetteBoxDisplay";
            this.vignetteBoxDisplay.Size = new System.Drawing.Size(488, 432);
            this.vignetteBoxDisplay.TabIndex = 1;
            this.vignetteBoxDisplay.TabStop = false;
            // 
            // shapeDetectButton
            // 
            this.shapeDetectButton.Location = new System.Drawing.Point(129, 12);
            this.shapeDetectButton.Name = "shapeDetectButton";
            this.shapeDetectButton.Size = new System.Drawing.Size(77, 23);
            this.shapeDetectButton.TabIndex = 3;
            this.shapeDetectButton.Text = "Shape";
            this.shapeDetectButton.UseVisualStyleBackColor = true;
            this.shapeDetectButton.Click += new System.EventHandler(this.detectButton_Click);
            // 
            // colorPictureBox
            // 
            this.colorPictureBox.Location = new System.Drawing.Point(522, 41);
            this.colorPictureBox.Name = "colorPictureBox";
            this.colorPictureBox.Size = new System.Drawing.Size(335, 248);
            this.colorPictureBox.TabIndex = 4;
            this.colorPictureBox.TabStop = false;
            // 
            // nodesList
            // 
            this.nodesList.FormattingEnabled = true;
            this.nodesList.Location = new System.Drawing.Point(871, 41);
            this.nodesList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nodesList.Name = "nodesList";
            this.nodesList.Size = new System.Drawing.Size(132, 407);
            this.nodesList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nb nodes : ";
            // 
            // nodesCount
            // 
            this.nodesCount.Location = new System.Drawing.Point(448, 17);
            this.nodesCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nodesCount.Name = "nodesCount";
            this.nodesCount.Size = new System.Drawing.Size(52, 20);
            this.nodesCount.TabIndex = 8;
            // 
            // dimensions
            // 
            this.dimensions.AutoSize = true;
            this.dimensions.Location = new System.Drawing.Point(320, 17);
            this.dimensions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dimensions.Name = "dimensions";
            this.dimensions.Size = new System.Drawing.Size(35, 13);
            this.dimensions.TabIndex = 9;
            this.dimensions.Text = "label2";
            // 
            // colorGraybutton
            // 
            this.colorGraybutton.Location = new System.Drawing.Point(522, 17);
            this.colorGraybutton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.colorGraybutton.Name = "colorGraybutton";
            this.colorGraybutton.Size = new System.Drawing.Size(190, 18);
            this.colorGraybutton.TabIndex = 10;
            this.colorGraybutton.Text = "Color/Gray";
            this.colorGraybutton.UseVisualStyleBackColor = true;
            this.colorGraybutton.Click += new System.EventHandler(this.colorGraybutton_Click);
            // 
            // arrowDectectbutton
            // 
            this.arrowDectectbutton.Location = new System.Drawing.Point(210, 12);
            this.arrowDectectbutton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.arrowDectectbutton.Name = "arrowDectectbutton";
            this.arrowDectectbutton.Size = new System.Drawing.Size(81, 23);
            this.arrowDectectbutton.TabIndex = 11;
            this.arrowDectectbutton.Text = "Arrows";
            this.arrowDectectbutton.UseVisualStyleBackColor = true;
            this.arrowDectectbutton.Click += new System.EventHandler(this.arrowDectectbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(917, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Nodes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1081, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Edges";
            // 
            // edgesList
            // 
            this.edgesList.FormattingEnabled = true;
            this.edgesList.Location = new System.Drawing.Point(1042, 41);
            this.edgesList.Name = "edgesList";
            this.edgesList.Size = new System.Drawing.Size(120, 407);
            this.edgesList.TabIndex = 14;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(762, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 15;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // vignetteReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 485);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.edgesList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox edgesList;
        private System.Windows.Forms.Button connectButton;
    }
}

