namespace WinformKTX.Vi_Pham
{
    partial class MainVipham
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
            menuStrip1 = new MenuStrip();
            viPhamToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            panelMainvipham = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { viPhamToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1308, 27);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // viPhamToolStripMenuItem
            // 
            viPhamToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            viPhamToolStripMenuItem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            viPhamToolStripMenuItem.Name = "viPhamToolStripMenuItem";
            viPhamToolStripMenuItem.Size = new Size(76, 23);
            viPhamToolStripMenuItem.Text = "Vi Pham";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(198, 24);
            toolStripMenuItem1.Text = "Sinh Viên Vi Phạm";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(198, 24);
            toolStripMenuItem2.Text = "Xử Lý Vi Phạm";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // panelMainvipham
            // 
            panelMainvipham.Location = new Point(0, 39);
            panelMainvipham.Name = "panelMainvipham";
            panelMainvipham.Size = new Size(1308, 643);
            panelMainvipham.TabIndex = 10;
            // 
            // MainVipham
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 672);
            Controls.Add(panelMainvipham);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainVipham";
            Text = "MainVipham";
            Load += MainVipham_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem viPhamToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private Panel panelMainvipham;
    }
}