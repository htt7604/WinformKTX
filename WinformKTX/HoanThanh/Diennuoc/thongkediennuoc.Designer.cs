namespace WinformKTX.HoanThanh.Diennuoc
{
    partial class thongkediennuoc
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            groupBoxThoiGian = new GroupBox();
            dateTimePickerKetThuc = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            dateTimePickerBatDau = new DateTimePicker();
            groupBoxThongTin = new GroupBox();
            comboBoxLoaiTang = new ComboBox();
            labelLoaiPhong = new Label();
            comboBoxPhong = new ComboBox();
            comboBoxTang = new ComboBox();
            labelTenPhong = new Label();
            label10 = new Label();
            label1 = new Label();
            dataGridViewThongTin = new DataGridView();
            groupBox1 = new GroupBox();
            radioButtonDaTT = new RadioButton();
            radioButtonAll = new RadioButton();
            radioButtonChuaTT = new RadioButton();
            groupBox2 = new GroupBox();
            label5 = new Label();
            textBoxNuoc = new TextBox();
            label4 = new Label();
            textBoxDien = new TextBox();
            groupBox3 = new GroupBox();
            radioButtonTatThoiGian = new RadioButton();
            radioButtonThoiGianSuDung = new RadioButton();
            radioButtonThoiGianThanhToan = new RadioButton();
            groupBox4 = new GroupBox();
            textBoxDemSoDien = new TextBox();
            textBoxDemSoNuoc = new TextBox();
            buttonTroLai = new Button();
            buttonTimKiem = new Button();
            groupBoxThoiGian.SuspendLayout();
            groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxThoiGian
            // 
            groupBoxThoiGian.Controls.Add(dateTimePickerKetThuc);
            groupBoxThoiGian.Controls.Add(label3);
            groupBoxThoiGian.Controls.Add(label2);
            groupBoxThoiGian.Controls.Add(dateTimePickerBatDau);
            groupBoxThoiGian.Location = new Point(12, 52);
            groupBoxThoiGian.Name = "groupBoxThoiGian";
            groupBoxThoiGian.Size = new Size(248, 171);
            groupBoxThoiGian.TabIndex = 67;
            groupBoxThoiGian.TabStop = false;
            groupBoxThoiGian.Text = "Thời Gian";
            // 
            // dateTimePickerKetThuc
            // 
            dateTimePickerKetThuc.Location = new Point(28, 106);
            dateTimePickerKetThuc.Name = "dateTimePickerKetThuc";
            dateTimePickerKetThuc.Size = new Size(200, 23);
            dateTimePickerKetThuc.TabIndex = 52;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 90);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 55;
            label3.Text = "Đến Ngày:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 29);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 54;
            label2.Text = "Từ Ngày:";
            // 
            // dateTimePickerBatDau
            // 
            dateTimePickerBatDau.Location = new Point(28, 47);
            dateTimePickerBatDau.Name = "dateTimePickerBatDau";
            dateTimePickerBatDau.Size = new Size(200, 23);
            dateTimePickerBatDau.TabIndex = 53;
            dateTimePickerBatDau.Value = new DateTime(2025, 2, 20, 9, 41, 33, 0);
            // 
            // groupBoxThongTin
            // 
            groupBoxThongTin.Controls.Add(comboBoxLoaiTang);
            groupBoxThongTin.Controls.Add(labelLoaiPhong);
            groupBoxThongTin.Controls.Add(comboBoxPhong);
            groupBoxThongTin.Controls.Add(comboBoxTang);
            groupBoxThongTin.Controls.Add(labelTenPhong);
            groupBoxThongTin.Controls.Add(label10);
            groupBoxThongTin.Font = new Font("Segoe UI", 10F);
            groupBoxThongTin.Location = new Point(266, 52);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(590, 172);
            groupBoxThongTin.TabIndex = 66;
            groupBoxThongTin.TabStop = false;
            groupBoxThongTin.Text = "Thông tin";
            // 
            // comboBoxLoaiTang
            // 
            comboBoxLoaiTang.DropDownWidth = 223;
            comboBoxLoaiTang.Font = new Font("Segoe UI", 12F);
            comboBoxLoaiTang.FormattingEnabled = true;
            comboBoxLoaiTang.Location = new Point(40, 45);
            comboBoxLoaiTang.Margin = new Padding(3, 2, 3, 2);
            comboBoxLoaiTang.Name = "comboBoxLoaiTang";
            comboBoxLoaiTang.Size = new Size(223, 29);
            comboBoxLoaiTang.TabIndex = 42;
            comboBoxLoaiTang.SelectedIndexChanged += comboBoxLoaiTang_SelectedIndexChanged;
            // 
            // labelLoaiPhong
            // 
            labelLoaiPhong.AutoSize = true;
            labelLoaiPhong.Location = new Point(40, 26);
            labelLoaiPhong.Name = "labelLoaiPhong";
            labelLoaiPhong.Size = new Size(68, 19);
            labelLoaiPhong.TabIndex = 41;
            labelLoaiPhong.Text = "Loại Tầng";
            // 
            // comboBoxPhong
            // 
            comboBoxPhong.Font = new Font("Segoe UI", 12F);
            comboBoxPhong.FormattingEnabled = true;
            comboBoxPhong.Location = new Point(351, 45);
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
            comboBoxTang.Location = new Point(40, 104);
            comboBoxTang.Margin = new Padding(3, 2, 3, 2);
            comboBoxTang.Name = "comboBoxTang";
            comboBoxTang.Size = new Size(223, 29);
            comboBoxTang.TabIndex = 38;
            comboBoxTang.SelectedIndexChanged += comboBoxTang_SelectedIndexChanged;
            // 
            // labelTenPhong
            // 
            labelTenPhong.AutoSize = true;
            labelTenPhong.Location = new Point(351, 26);
            labelTenPhong.Name = "labelTenPhong";
            labelTenPhong.Size = new Size(49, 19);
            labelTenPhong.TabIndex = 30;
            labelTenPhong.Text = "Phòng";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(40, 85);
            label10.Name = "label10";
            label10.Size = new Size(39, 19);
            label10.TabIndex = 29;
            label10.Text = "Tầng";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point(479, 9);
            label1.Name = "label1";
            label1.Size = new Size(292, 37);
            label1.TabIndex = 65;
            label1.Text = "Thống Kê Điện -Nước";
            // 
            // dataGridViewThongTin
            // 
            dataGridViewThongTin.AllowUserToOrderColumns = true;
            dataGridViewThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThongTin.BackgroundColor = Color.FromArgb(250, 255, 255);
            dataGridViewThongTin.BorderStyle = BorderStyle.None;
            dataGridViewThongTin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewThongTin.Location = new Point(-8, 279);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1306, 324);
            dataGridViewThongTin.TabIndex = 69;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonDaTT);
            groupBox1.Controls.Add(radioButtonAll);
            groupBox1.Controls.Add(radioButtonChuaTT);
            groupBox1.Font = new Font("Segoe UI", 10F);
            groupBox1.Location = new Point(492, 229);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(364, 47);
            groupBox1.TabIndex = 70;
            groupBox1.TabStop = false;
            groupBox1.Text = "Trạng Thái Thanh Toán";
            // 
            // radioButtonDaTT
            // 
            radioButtonDaTT.AutoSize = true;
            radioButtonDaTT.Location = new Point(6, 22);
            radioButtonDaTT.Name = "radioButtonDaTT";
            radioButtonDaTT.Size = new Size(119, 23);
            radioButtonDaTT.TabIndex = 60;
            radioButtonDaTT.TabStop = true;
            radioButtonDaTT.Text = "Đã Thanh Toán";
            radioButtonDaTT.UseVisualStyleBackColor = true;
            // 
            // radioButtonAll
            // 
            radioButtonAll.AutoSize = true;
            radioButtonAll.Location = new Point(281, 22);
            radioButtonAll.Name = "radioButtonAll";
            radioButtonAll.Size = new Size(66, 23);
            radioButtonAll.TabIndex = 62;
            radioButtonAll.TabStop = true;
            radioButtonAll.Text = "Tất Cả";
            radioButtonAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonChuaTT
            // 
            radioButtonChuaTT.AutoSize = true;
            radioButtonChuaTT.Location = new Point(140, 22);
            radioButtonChuaTT.Name = "radioButtonChuaTT";
            radioButtonChuaTT.Size = new Size(134, 23);
            radioButtonChuaTT.TabIndex = 61;
            radioButtonChuaTT.TabStop = true;
            radioButtonChuaTT.Text = "Chưa Thanh Toán";
            radioButtonChuaTT.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(textBoxNuoc);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(textBoxDien);
            groupBox2.Location = new Point(862, 52);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(162, 171);
            groupBox2.TabIndex = 71;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tìm";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 95);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 3;
            label5.Text = "Nước >";
            // 
            // textBoxNuoc
            // 
            textBoxNuoc.Font = new Font("Segoe UI", 10F);
            textBoxNuoc.Location = new Point(65, 90);
            textBoxNuoc.Name = "textBoxNuoc";
            textBoxNuoc.Size = new Size(55, 25);
            textBoxNuoc.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 47);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 1;
            label4.Text = "Điện >";
            // 
            // textBoxDien
            // 
            textBoxDien.Font = new Font("Segoe UI", 10F);
            textBoxDien.Location = new Point(65, 42);
            textBoxDien.Name = "textBoxDien";
            textBoxDien.Size = new Size(55, 25);
            textBoxDien.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButtonTatThoiGian);
            groupBox3.Controls.Add(radioButtonThoiGianSuDung);
            groupBox3.Controls.Add(radioButtonThoiGianThanhToan);
            groupBox3.Font = new Font("Segoe UI", 10F);
            groupBox3.Location = new Point(12, 227);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(459, 47);
            groupBox3.TabIndex = 71;
            groupBox3.TabStop = false;
            groupBox3.Text = "Dựa theo";
            // 
            // radioButtonTatThoiGian
            // 
            radioButtonTatThoiGian.AutoSize = true;
            radioButtonTatThoiGian.Location = new Point(331, 22);
            radioButtonTatThoiGian.Name = "radioButtonTatThoiGian";
            radioButtonTatThoiGian.Size = new Size(67, 23);
            radioButtonTatThoiGian.TabIndex = 62;
            radioButtonTatThoiGian.TabStop = true;
            radioButtonTatThoiGian.Text = "Không";
            radioButtonTatThoiGian.UseVisualStyleBackColor = true;
            radioButtonTatThoiGian.CheckedChanged += radioButtonTatThoiGian_CheckedChanged;
            // 
            // radioButtonThoiGianSuDung
            // 
            radioButtonThoiGianSuDung.AutoSize = true;
            radioButtonThoiGianSuDung.Location = new Point(6, 22);
            radioButtonThoiGianSuDung.Name = "radioButtonThoiGianSuDung";
            radioButtonThoiGianSuDung.Size = new Size(141, 23);
            radioButtonThoiGianSuDung.TabIndex = 60;
            radioButtonThoiGianSuDung.TabStop = true;
            radioButtonThoiGianSuDung.Text = "Thời gian sử dụng ";
            radioButtonThoiGianSuDung.UseVisualStyleBackColor = true;
            radioButtonThoiGianSuDung.CheckedChanged += radioButtonThoiGianSuDung_CheckedChanged;
            // 
            // radioButtonThoiGianThanhToan
            // 
            radioButtonThoiGianThanhToan.AutoSize = true;
            radioButtonThoiGianThanhToan.Location = new Point(154, 22);
            radioButtonThoiGianThanhToan.Name = "radioButtonThoiGianThanhToan";
            radioButtonThoiGianThanhToan.Size = new Size(155, 23);
            radioButtonThoiGianThanhToan.TabIndex = 61;
            radioButtonThoiGianThanhToan.TabStop = true;
            radioButtonThoiGianThanhToan.Text = "Thời gian thanh toán";
            radioButtonThoiGianThanhToan.UseVisualStyleBackColor = true;
            radioButtonThoiGianThanhToan.CheckedChanged += radioButtonThoiGianThanhToan_CheckedChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBoxDemSoDien);
            groupBox4.Controls.Add(textBoxDemSoNuoc);
            groupBox4.Location = new Point(1042, 53);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(200, 171);
            groupBox4.TabIndex = 72;
            groupBox4.TabStop = false;
            groupBox4.Text = "Thống kê";
            // 
            // textBoxDemSoDien
            // 
            textBoxDemSoDien.Location = new Point(6, 28);
            textBoxDemSoDien.Name = "textBoxDemSoDien";
            textBoxDemSoDien.ReadOnly = true;
            textBoxDemSoDien.Size = new Size(193, 23);
            textBoxDemSoDien.TabIndex = 65;
            textBoxDemSoDien.WordWrap = false;
            // 
            // textBoxDemSoNuoc
            // 
            textBoxDemSoNuoc.Location = new Point(6, 94);
            textBoxDemSoNuoc.Name = "textBoxDemSoNuoc";
            textBoxDemSoNuoc.ReadOnly = true;
            textBoxDemSoNuoc.Size = new Size(193, 23);
            textBoxDemSoNuoc.TabIndex = 66;
            textBoxDemSoNuoc.WordWrap = false;
            // 
            // buttonTroLai
            // 
            buttonTroLai.BackColor = Color.Gold;
            buttonTroLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonTroLai.ForeColor = SystemColors.ControlLightLight;
            buttonTroLai.Location = new Point(1189, 240);
            buttonTroLai.Name = "buttonTroLai";
            buttonTroLai.Size = new Size(83, 34);
            buttonTroLai.TabIndex = 74;
            buttonTroLai.Text = "Trở Lại";
            buttonTroLai.UseVisualStyleBackColor = false;
            buttonTroLai.Click += buttonTroLai_Click;
            // 
            // buttonTimKiem
            // 
            buttonTimKiem.BackColor = Color.SpringGreen;
            buttonTimKiem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonTimKiem.ForeColor = SystemColors.ControlLightLight;
            buttonTimKiem.Location = new Point(1099, 240);
            buttonTimKiem.Name = "buttonTimKiem";
            buttonTimKiem.Size = new Size(84, 34);
            buttonTimKiem.TabIndex = 73;
            buttonTimKiem.Text = "Tìm Kiếm";
            buttonTimKiem.UseVisualStyleBackColor = false;
            buttonTimKiem.Click += buttonTimKiem_Click;
            // 
            // thongkediennuoc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 594);
            Controls.Add(buttonTroLai);
            Controls.Add(buttonTimKiem);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(groupBoxThoiGian);
            Controls.Add(groupBoxThongTin);
            Controls.Add(label1);
            Name = "thongkediennuoc";
            Text = "thongkediennuoc";
            Load += thongkediennuoc_Load;
            groupBoxThoiGian.ResumeLayout(false);
            groupBoxThoiGian.PerformLayout();
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBoxThoiGian;
        private DateTimePicker dateTimePickerKetThuc;
        private Label label3;
        private Label label2;
        private DateTimePicker dateTimePickerBatDau;
        private GroupBox groupBoxThongTin;
        private ComboBox comboBoxLoaiTang;
        private Label labelLoaiPhong;
        private ComboBox comboBoxPhong;
        private ComboBox comboBoxTang;
        private Label labelTenPhong;
        private Label label10;
        private Label label1;
        private DataGridView dataGridViewThongTin;
        private GroupBox groupBox1;
        private RadioButton radioButtonDaTT;
        private RadioButton radioButtonAll;
        private RadioButton radioButtonChuaTT;
        private GroupBox groupBox2;
        private TextBox textBoxNuoc;
        private Label label4;
        private TextBox textBoxDien;
        private Label label5;
        private GroupBox groupBox3;
        private RadioButton radioButtonThoiGianSuDung;
        private RadioButton radioButtonThoiGianThanhToan;
        private GroupBox groupBox4;
        private TextBox textBoxDemSoDien;
        private TextBox textBoxDemSoNuoc;
        private Button buttonTroLai;
        private Button buttonTimKiem;
        private RadioButton radioButtonTatThoiGian;
    }
}