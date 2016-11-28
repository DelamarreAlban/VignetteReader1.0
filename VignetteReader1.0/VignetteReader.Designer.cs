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
            this.vignetteBoxDisplay = new System.Windows.Forms.PictureBox();
            this.nodesList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nodesCount = new System.Windows.Forms.TextBox();
            this.dimensions = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.edgesList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadVignetteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadXMIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMIIntegrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmiCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.vignetteBoxDisplay)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vignetteBoxDisplay
            // 
            this.vignetteBoxDisplay.Location = new System.Drawing.Point(15, 111);
            this.vignetteBoxDisplay.Margin = new System.Windows.Forms.Padding(6);
            this.vignetteBoxDisplay.Name = "vignetteBoxDisplay";
            this.vignetteBoxDisplay.Size = new System.Drawing.Size(976, 831);
            this.vignetteBoxDisplay.TabIndex = 1;
            this.vignetteBoxDisplay.TabStop = false;
            // 
            // nodesList
            // 
            this.nodesList.FormattingEnabled = true;
            this.nodesList.ItemHeight = 25;
            this.nodesList.Location = new System.Drawing.Point(1096, 111);
            this.nodesList.Margin = new System.Windows.Forms.Padding(4);
            this.nodesList.Name = "nodesList";
            this.nodesList.Size = new System.Drawing.Size(260, 829);
            this.nodesList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(770, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nb nodes : ";
            // 
            // nodesCount
            // 
            this.nodesCount.Location = new System.Drawing.Point(891, 65);
            this.nodesCount.Margin = new System.Windows.Forms.Padding(4);
            this.nodesCount.Name = "nodesCount";
            this.nodesCount.Size = new System.Drawing.Size(100, 31);
            this.nodesCount.TabIndex = 8;
            // 
            // dimensions
            // 
            this.dimensions.AutoSize = true;
            this.dimensions.Location = new System.Drawing.Point(13, 68);
            this.dimensions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dimensions.Name = "dimensions";
            this.dimensions.Size = new System.Drawing.Size(70, 25);
            this.dimensions.TabIndex = 9;
            this.dimensions.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1188, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Nodes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1499, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Edges";
            // 
            // edgesList
            // 
            this.edgesList.FormattingEnabled = true;
            this.edgesList.ItemHeight = 25;
            this.edgesList.Location = new System.Drawing.Point(1418, 111);
            this.edgesList.Margin = new System.Windows.Forms.Padding(6);
            this.edgesList.Name = "edgesList";
            this.edgesList.Size = new System.Drawing.Size(236, 829);
            this.edgesList.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.detectToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.outputToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1699, 40);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadVignetteToolStripMenuItem,
            this.loadXMIToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(90, 38);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // loadVignetteToolStripMenuItem
            // 
            this.loadVignetteToolStripMenuItem.Name = "loadVignetteToolStripMenuItem";
            this.loadVignetteToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.loadVignetteToolStripMenuItem.Text = "Load vignette";
            this.loadVignetteToolStripMenuItem.Click += new System.EventHandler(this.loadVignetteButton_Click);
            // 
            // loadXMIToolStripMenuItem
            // 
            this.loadXMIToolStripMenuItem.Name = "loadXMIToolStripMenuItem";
            this.loadXMIToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.loadXMIToolStripMenuItem.Text = "Load XMI";
            this.loadXMIToolStripMenuItem.Click += new System.EventHandler(this.loadXMIToolStripMenuItem_Click);
            // 
            // detectToolStripMenuItem
            // 
            this.detectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shapesToolStripMenuItem,
            this.arrowsToolStripMenuItem,
            this.textToolStripMenuItem});
            this.detectToolStripMenuItem.Name = "detectToolStripMenuItem";
            this.detectToolStripMenuItem.Size = new System.Drawing.Size(97, 36);
            this.detectToolStripMenuItem.Text = "Detect";
            // 
            // shapesToolStripMenuItem
            // 
            this.shapesToolStripMenuItem.Name = "shapesToolStripMenuItem";
            this.shapesToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.shapesToolStripMenuItem.Text = "Shapes";
            this.shapesToolStripMenuItem.Click += new System.EventHandler(this.shapeDetectButton_Click);
            // 
            // arrowsToolStripMenuItem
            // 
            this.arrowsToolStripMenuItem.Name = "arrowsToolStripMenuItem";
            this.arrowsToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.arrowsToolStripMenuItem.Text = "Arrows";
            this.arrowsToolStripMenuItem.Click += new System.EventHandler(this.arrowDectectbutton_Click);
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.textToolStripMenuItem.Text = "Text";
            this.textToolStripMenuItem.Click += new System.EventHandler(this.textDetectButton_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(91, 36);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(204, 38);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateXMLToolStripMenuItem,
            this.xMIIntegrationToolStripMenuItem});
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(103, 38);
            this.outputToolStripMenuItem.Text = "Output";
            // 
            // generateXMLToolStripMenuItem
            // 
            this.generateXMLToolStripMenuItem.Name = "generateXMLToolStripMenuItem";
            this.generateXMLToolStripMenuItem.Size = new System.Drawing.Size(281, 38);
            this.generateXMLToolStripMenuItem.Text = "Generate XML";
            this.generateXMLToolStripMenuItem.Click += new System.EventHandler(this.xmlbutton_Click);
            // 
            // xMIIntegrationToolStripMenuItem
            // 
            this.xMIIntegrationToolStripMenuItem.Name = "xMIIntegrationToolStripMenuItem";
            this.xMIIntegrationToolStripMenuItem.Size = new System.Drawing.Size(281, 38);
            this.xMIIntegrationToolStripMenuItem.Text = "XMI integration";
            this.xMIIntegrationToolStripMenuItem.Click += new System.EventHandler(this.xMIIntegrationToolStripMenuItem_Click);
            // 
            // xmiCheckBox
            // 
            this.xmiCheckBox.AutoSize = true;
            this.xmiCheckBox.Location = new System.Drawing.Point(377, 67);
            this.xmiCheckBox.Name = "xmiCheckBox";
            this.xmiCheckBox.Size = new System.Drawing.Size(81, 29);
            this.xmiCheckBox.TabIndex = 19;
            this.xmiCheckBox.Text = "XMI";
            this.xmiCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(159, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 37);
            this.button1.TabIndex = 20;
            this.button1.Text = "Detect&Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // vignetteReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1699, 1013);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xmiCheckBox);
            this.Controls.Add(this.edgesList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dimensions);
            this.Controls.Add(this.nodesCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nodesList);
            this.Controls.Add(this.vignetteBoxDisplay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "vignetteReader";
            this.Text = "VignetteReader1.0";
            ((System.ComponentModel.ISupportInitialize)(this.vignetteBoxDisplay)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox vignetteBoxDisplay;
        private System.Windows.Forms.ListBox nodesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nodesCount;
        private System.Windows.Forms.Label dimensions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox edgesList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadVignetteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadXMIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMIIntegrationToolStripMenuItem;
        private System.Windows.Forms.CheckBox xmiCheckBox;
        private System.Windows.Forms.Button button1;
    }
}

