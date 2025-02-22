namespace abc.HoanThanh
{
    partial class MainThongKe
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
            panelMain = new Panel();
            menuStrip2 = new MenuStrip();
            chonMucToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            hoSoSinhVienToolStripMenuItem = new ToolStripMenuItem();
            sinhVienNoiTruToolStripMenuItem = new ToolStripMenuItem();
            vIPhamSinhVienToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            coSoVatChatToolStripMenuItem = new ToolStripMenuItem();
            huHongToolStripMenuItem = new ToolStripMenuItem();
            dienNuocToolStripMenuItem = new ToolStripMenuItem();
            phongGiuongToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            thanhToanToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Location = new Point(0, 32);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1308, 638);
            panelMain.TabIndex = 9;
            // 
            // menuStrip2
            // 
            menuStrip2.Items.AddRange(new ToolStripItem[] { chonMucToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(1308, 29);
            menuStrip2.TabIndex = 10;
            menuStrip2.Text = "menuStrip2";
            // 
            // chonMucToolStripMenuItem
            // 
            chonMucToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem4, toolStripSeparator1, coSoVatChatToolStripMenuItem, toolStripSeparator3, thanhToanToolStripMenuItem, toolStripSeparator4 });
            chonMucToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            chonMucToolStripMenuItem.Name = "chonMucToolStripMenuItem";
            chonMucToolStripMenuItem.Size = new Size(98, 25);
            chonMucToolStripMenuItem.Text = "Thống Kê ";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.DropDownItems.AddRange(new ToolStripItem[] { hoSoSinhVienToolStripMenuItem, sinhVienNoiTruToolStripMenuItem, vIPhamSinhVienToolStripMenuItem });
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(192, 26);
            toolStripMenuItem4.Text = "Sinh Viên";
            // 
            // hoSoSinhVienToolStripMenuItem
            // 
            hoSoSinhVienToolStripMenuItem.Name = "hoSoSinhVienToolStripMenuItem";
            hoSoSinhVienToolStripMenuItem.Size = new Size(221, 26);
            hoSoSinhVienToolStripMenuItem.Text = "Hồ Sơ Sinh Viên";
            hoSoSinhVienToolStripMenuItem.Click += hoSoSinhVienToolStripMenuItem_Click;
            // 
            // sinhVienNoiTruToolStripMenuItem
            // 
            sinhVienNoiTruToolStripMenuItem.Name = "sinhVienNoiTruToolStripMenuItem";
            sinhVienNoiTruToolStripMenuItem.Size = new Size(221, 26);
            sinhVienNoiTruToolStripMenuItem.Text = "Sinh Viên Nội Trú";
            sinhVienNoiTruToolStripMenuItem.Click += sinhVienNoiTruToolStripMenuItem_Click;
            // 
            // vIPhamSinhVienToolStripMenuItem
            // 
            vIPhamSinhVienToolStripMenuItem.Name = "vIPhamSinhVienToolStripMenuItem";
            vIPhamSinhVienToolStripMenuItem.Size = new Size(221, 26);
            vIPhamSinhVienToolStripMenuItem.Text = "Vi Phạm Sinh Viên";
            vIPhamSinhVienToolStripMenuItem.Click += vIPhamSinhVienToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(189, 6);
            // 
            // coSoVatChatToolStripMenuItem
            // 
            coSoVatChatToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { huHongToolStripMenuItem, dienNuocToolStripMenuItem, phongGiuongToolStripMenuItem });
            coSoVatChatToolStripMenuItem.Name = "coSoVatChatToolStripMenuItem";
            coSoVatChatToolStripMenuItem.Size = new Size(192, 26);
            coSoVatChatToolStripMenuItem.Text = "Cơ Sở Vật Chất";
            // 
            // huHongToolStripMenuItem
            // 
            huHongToolStripMenuItem.Name = "huHongToolStripMenuItem";
            huHongToolStripMenuItem.Size = new Size(196, 26);
            huHongToolStripMenuItem.Text = "Cơ Sở Vật Chất ";
            huHongToolStripMenuItem.Click += huHongToolStripMenuItem_Click;
            // 
            // dienNuocToolStripMenuItem
            // 
            dienNuocToolStripMenuItem.Name = "dienNuocToolStripMenuItem";
            dienNuocToolStripMenuItem.Size = new Size(196, 26);
            dienNuocToolStripMenuItem.Text = "Điện Nước";
            dienNuocToolStripMenuItem.Click += dienNuocToolStripMenuItem_Click;
            // 
            // phongGiuongToolStripMenuItem
            // 
            phongGiuongToolStripMenuItem.Name = "phongGiuongToolStripMenuItem";
            phongGiuongToolStripMenuItem.Size = new Size(196, 26);
            phongGiuongToolStripMenuItem.Text = "Phòng";
            phongGiuongToolStripMenuItem.Click += phongGiuongToolStripMenuItem_Click_1;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(189, 6);
            // 
            // thanhToanToolStripMenuItem
            // 
            thanhToanToolStripMenuItem.Name = "thanhToanToolStripMenuItem";
            thanhToanToolStripMenuItem.Size = new Size(192, 26);
            thanhToanToolStripMenuItem.Text = "Thanh Toán";
            thanhToanToolStripMenuItem.Click += thanhToanToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(189, 6);
            // 
            // MainThongKe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 672);
            Controls.Add(panelMain);
            Controls.Add(menuStrip2);
            Name = "MainThongKe";
            Text = "MainThongKe";
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panelMain;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem chonMucToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem hoSoSinhVienToolStripMenuItem;
        private ToolStripMenuItem sinhVienNoiTruToolStripMenuItem;
        private ToolStripMenuItem vIPhamSinhVienToolStripMenuItem;
        private ToolStripMenuItem thanhToanToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem coSoVatChatToolStripMenuItem;
        private ToolStripMenuItem huHongToolStripMenuItem;
        private ToolStripMenuItem dienNuocToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem phongGiuongToolStripMenuItem;
    }
}