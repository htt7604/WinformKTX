namespace abc
{
    partial class ThongKeGiuongPhong
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            buttonLoad = new Button();
            groupBox1 = new GroupBox();
            comboBoxLoaiTang = new ComboBox();
            labelLoaiPhong = new Label();
            comboBoxPhong = new ComboBox();
            comboBoxTang = new ComboBox();
            labelTenPhong = new Label();
            label10 = new Label();
            buttonTimKiemGiuong = new Button();
            dataGridViewThongTin = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            comboBoxTinhTrang = new ComboBox();
            groupBox3 = new GroupBox();
            textBoxDemPhongTrong = new TextBox();
            textBoxDemPhongDay = new TextBox();
            textBoxDemTongPhong = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // buttonLoad
            // 
            buttonLoad.BackColor = Color.SandyBrown;
            buttonLoad.Font = new Font("Segoe UI", 12F);
            buttonLoad.ForeColor = SystemColors.ButtonHighlight;
            buttonLoad.Location = new Point(1085, 251);
            buttonLoad.Margin = new Padding(3, 2, 3, 2);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(87, 32);
            buttonLoad.TabIndex = 43;
            buttonLoad.Text = "Làm mới";
            buttonLoad.UseVisualStyleBackColor = false;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBoxLoaiTang);
            groupBox1.Controls.Add(labelLoaiPhong);
            groupBox1.Controls.Add(comboBoxPhong);
            groupBox1.Controls.Add(comboBoxTang);
            groupBox1.Controls.Add(labelTenPhong);
            groupBox1.Controls.Add(label10);
            groupBox1.Font = new Font("Segoe UI", 10F);
            groupBox1.Location = new Point(75, 71);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(570, 166);
            groupBox1.TabIndex = 46;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm Kiếm Theo";
            // 
            // comboBoxLoaiTang
            // 
            comboBoxLoaiTang.DropDownWidth = 223;
            comboBoxLoaiTang.Font = new Font("Segoe UI", 12F);
            comboBoxLoaiTang.FormattingEnabled = true;
            comboBoxLoaiTang.Location = new Point(40, 54);
            comboBoxLoaiTang.Margin = new Padding(3, 2, 3, 2);
            comboBoxLoaiTang.Name = "comboBoxLoaiTang";
            comboBoxLoaiTang.Size = new Size(223, 29);
            comboBoxLoaiTang.TabIndex = 42;
            comboBoxLoaiTang.SelectedIndexChanged += comboBoxLoaiTang_SelectedIndexChanged;
            // 
            // labelLoaiPhong
            // 
            labelLoaiPhong.AutoSize = true;
            labelLoaiPhong.Location = new Point(40, 33);
            labelLoaiPhong.Name = "labelLoaiPhong";
            labelLoaiPhong.Size = new Size(68, 19);
            labelLoaiPhong.TabIndex = 41;
            labelLoaiPhong.Text = "Loại Tầng";
            // 
            // comboBoxPhong
            // 
            comboBoxPhong.Font = new Font("Segoe UI", 12F);
            comboBoxPhong.FormattingEnabled = true;
            comboBoxPhong.Location = new Point(324, 54);
            comboBoxPhong.Margin = new Padding(3, 2, 3, 2);
            comboBoxPhong.Name = "comboBoxPhong";
            comboBoxPhong.Size = new Size(223, 29);
            comboBoxPhong.TabIndex = 39;
            // 
            // comboBoxTang
            // 
            comboBoxTang.DropDownWidth = 223;
            comboBoxTang.Font = new Font("Segoe UI", 12F);
            comboBoxTang.FormattingEnabled = true;
            comboBoxTang.Location = new Point(40, 115);
            comboBoxTang.Margin = new Padding(3, 2, 3, 2);
            comboBoxTang.Name = "comboBoxTang";
            comboBoxTang.Size = new Size(223, 29);
            comboBoxTang.TabIndex = 38;
            comboBoxTang.SelectedIndexChanged += comboBoxTang_SelectedIndexChanged;
            // 
            // labelTenPhong
            // 
            labelTenPhong.AutoSize = true;
            labelTenPhong.Location = new Point(324, 33);
            labelTenPhong.Name = "labelTenPhong";
            labelTenPhong.Size = new Size(49, 19);
            labelTenPhong.TabIndex = 30;
            labelTenPhong.Text = "Phòng";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(40, 96);
            label10.Name = "label10";
            label10.Size = new Size(39, 19);
            label10.TabIndex = 29;
            label10.Text = "Tầng";
            // 
            // buttonTimKiemGiuong
            // 
            buttonTimKiemGiuong.BackColor = Color.Red;
            buttonTimKiemGiuong.Font = new Font("Segoe UI", 12F);
            buttonTimKiemGiuong.ForeColor = SystemColors.ButtonHighlight;
            buttonTimKiemGiuong.Location = new Point(983, 251);
            buttonTimKiemGiuong.Margin = new Padding(3, 2, 3, 2);
            buttonTimKiemGiuong.Name = "buttonTimKiemGiuong";
            buttonTimKiemGiuong.Size = new Size(87, 32);
            buttonTimKiemGiuong.TabIndex = 40;
            buttonTimKiemGiuong.Text = "Tìm Kiếm";
            buttonTimKiemGiuong.UseVisualStyleBackColor = false;
            buttonTimKiemGiuong.Click += buttonTimkiemgiuong_Click;
            // 
            // dataGridViewThongTin
            // 
            dataGridViewThongTin.AllowUserToOrderColumns = true;
            dataGridViewThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThongTin.BackgroundColor = Color.FromArgb(250, 255, 255);
            dataGridViewThongTin.BorderStyle = BorderStyle.None;
            dataGridViewThongTin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewThongTin.Location = new Point(-4, 291);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1306, 309);
            dataGridViewThongTin.TabIndex = 49;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(22, 251);
            label1.Name = "label1";
            label1.Size = new Size(163, 25);
            label1.TabIndex = 48;
            label1.Text = "Thông tin phòng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label2.Location = new Point(545, 9);
            label2.Name = "label2";
            label2.Size = new Size(229, 37);
            label2.TabIndex = 47;
            label2.Text = "Thống Kê Phòng";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(comboBoxTinhTrang);
            groupBox2.Location = new Point(747, 71);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(268, 166);
            groupBox2.TabIndex = 50;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tìm Theo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 33);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 44;
            label3.Text = "Tình Trạng";
            // 
            // comboBoxTinhTrang
            // 
            comboBoxTinhTrang.Font = new Font("Segoe UI", 12F);
            comboBoxTinhTrang.FormattingEnabled = true;
            comboBoxTinhTrang.Location = new Point(17, 54);
            comboBoxTinhTrang.Margin = new Padding(3, 2, 3, 2);
            comboBoxTinhTrang.Name = "comboBoxTinhTrang";
            comboBoxTinhTrang.Size = new Size(223, 29);
            comboBoxTinhTrang.TabIndex = 44;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBoxDemPhongTrong);
            groupBox3.Controls.Add(textBoxDemPhongDay);
            groupBox3.Controls.Add(textBoxDemTongPhong);
            groupBox3.Location = new Point(1050, 81);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(219, 134);
            groupBox3.TabIndex = 59;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin thống kê";
            // 
            // textBoxDemPhongTrong
            // 
            textBoxDemPhongTrong.Location = new Point(6, 69);
            textBoxDemPhongTrong.Name = "textBoxDemPhongTrong";
            textBoxDemPhongTrong.ReadOnly = true;
            textBoxDemPhongTrong.Size = new Size(197, 23);
            textBoxDemPhongTrong.TabIndex = 3;
            textBoxDemPhongTrong.WordWrap = false;
            // 
            // textBoxDemPhongDay
            // 
            textBoxDemPhongDay.Location = new Point(6, 105);
            textBoxDemPhongDay.Name = "textBoxDemPhongDay";
            textBoxDemPhongDay.ReadOnly = true;
            textBoxDemPhongDay.Size = new Size(197, 23);
            textBoxDemPhongDay.TabIndex = 2;
            textBoxDemPhongDay.WordWrap = false;
            // 
            // textBoxDemTongPhong
            // 
            textBoxDemTongPhong.Location = new Point(6, 31);
            textBoxDemTongPhong.Name = "textBoxDemTongPhong";
            textBoxDemTongPhong.ReadOnly = true;
            textBoxDemTongPhong.Size = new Size(197, 23);
            textBoxDemTongPhong.TabIndex = 1;
            textBoxDemTongPhong.WordWrap = false;
            // 
            // ThongKeGiuongPhong
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 594);
            Controls.Add(groupBox3);
            Controls.Add(buttonLoad);
            Controls.Add(groupBox2);
            Controls.Add(buttonTimKiemGiuong);
            Controls.Add(groupBox1);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(label1);
            Controls.Add(label2);
            Name = "ThongKeGiuongPhong";
            Text = "ThongKe";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonLoad;
        private GroupBox groupBox1;
        private ComboBox comboBoxLoaiTang;
        private Label labelLoaiPhong;
        private Button buttonTimKiemGiuong;
        private ComboBox comboBoxPhong;
        private ComboBox comboBoxTang;
        private Label labelTenPhong;
        private Label label10;
        private DataGridView dataGridViewThongTin;
        private Label label1;
        private Label label2;
        private GroupBox groupBox2;
        private Label label3;
        private ComboBox comboBoxTinhTrang;
        private GroupBox groupBox3;
        private TextBox textBoxDemPhongTrong;
        private TextBox textBoxDemPhongDay;
        private TextBox textBoxDemTongPhong;
    }
}