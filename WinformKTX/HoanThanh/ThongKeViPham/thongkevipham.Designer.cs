namespace abc.HoanThanh.ThongKeViPham
{
    partial class thongkevipham
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
            groupBoxThongTin = new GroupBox();
            comboBoxSinhVien = new ComboBox();
            comboBoxMucViPham = new ComboBox();
            labelMucvipham = new Label();
            labelMSSV = new Label();
            dataGridViewThongTin = new DataGridView();
            groupBoxTrangThai = new GroupBox();
            radioButtonAll = new RadioButton();
            radioButtonChuaxuly = new RadioButton();
            radioButtonDaxuly = new RadioButton();
            checkBoxThoigian = new CheckBox();
            groupBoxThoiGian = new GroupBox();
            dateTimePickerKetThuc = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            dateTimePickerBatDau = new DateTimePicker();
            label1 = new Label();
            buttonTimKiem = new Button();
            buttonLoad = new Button();
            groupBox2 = new GroupBox();
            textBoxDemViPham = new TextBox();
            textBoxDemDaXL = new TextBox();
            textBoxDemChuaXL = new TextBox();
            groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBoxTrangThai.SuspendLayout();
            groupBoxThoiGian.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxThongTin
            // 
            groupBoxThongTin.Controls.Add(comboBoxSinhVien);
            groupBoxThongTin.Controls.Add(comboBoxMucViPham);
            groupBoxThongTin.Controls.Add(labelMucvipham);
            groupBoxThongTin.Controls.Add(labelMSSV);
            groupBoxThongTin.Font = new Font("Segoe UI", 10F);
            groupBoxThongTin.Location = new Point(266, 136);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(515, 85);
            groupBoxThongTin.TabIndex = 63;
            groupBoxThongTin.TabStop = false;
            groupBoxThongTin.Text = "Thông tin";
            // 
            // comboBoxSinhVien
            // 
            comboBoxSinhVien.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxSinhVien.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxSinhVien.FormattingEnabled = true;
            comboBoxSinhVien.Location = new Point(88, 36);
            comboBoxSinhVien.Name = "comboBoxSinhVien";
            comboBoxSinhVien.Size = new Size(132, 25);
            comboBoxSinhVien.TabIndex = 58;
            // 
            // comboBoxMucViPham
            // 
            comboBoxMucViPham.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxMucViPham.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxMucViPham.FormattingEnabled = true;
            comboBoxMucViPham.Location = new Point(357, 36);
            comboBoxMucViPham.Name = "comboBoxMucViPham";
            comboBoxMucViPham.Size = new Size(132, 25);
            comboBoxMucViPham.TabIndex = 50;
            // 
            // labelMucvipham
            // 
            labelMucvipham.AutoSize = true;
            labelMucvipham.Location = new Point(260, 39);
            labelMucvipham.Name = "labelMucvipham";
            labelMucvipham.Size = new Size(91, 19);
            labelMucvipham.TabIndex = 49;
            labelMucvipham.Text = "Mục Vi Phạm";
            // 
            // labelMSSV
            // 
            labelMSSV.AutoSize = true;
            labelMSSV.Location = new Point(30, 41);
            labelMSSV.Name = "labelMSSV";
            labelMSSV.Size = new Size(52, 19);
            labelMSSV.TabIndex = 41;
            labelMSSV.Text = "MSSV :";
            labelMSSV.UseMnemonic = false;
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
            dataGridViewThongTin.Location = new Point(-4, 236);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1295, 366);
            dataGridViewThongTin.TabIndex = 64;
            // 
            // groupBoxTrangThai
            // 
            groupBoxTrangThai.Controls.Add(radioButtonAll);
            groupBoxTrangThai.Controls.Add(radioButtonChuaxuly);
            groupBoxTrangThai.Controls.Add(radioButtonDaxuly);
            groupBoxTrangThai.Font = new Font("Segoe UI", 10F);
            groupBoxTrangThai.Location = new Point(787, 136);
            groupBoxTrangThai.Name = "groupBoxTrangThai";
            groupBoxTrangThai.Size = new Size(321, 85);
            groupBoxTrangThai.TabIndex = 61;
            groupBoxTrangThai.TabStop = false;
            groupBoxTrangThai.Text = "Trạng Thái Xử Lý";
            // 
            // radioButtonAll
            // 
            radioButtonAll.AutoSize = true;
            radioButtonAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonAll.Location = new Point(218, 38);
            radioButtonAll.Name = "radioButtonAll";
            radioButtonAll.Size = new Size(59, 19);
            radioButtonAll.TabIndex = 48;
            radioButtonAll.TabStop = true;
            radioButtonAll.Text = "Tất Cả";
            radioButtonAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonChuaxuly
            // 
            radioButtonChuaxuly.AutoSize = true;
            radioButtonChuaxuly.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonChuaxuly.Location = new Point(102, 39);
            radioButtonChuaxuly.Name = "radioButtonChuaxuly";
            radioButtonChuaxuly.Size = new Size(87, 19);
            radioButtonChuaxuly.TabIndex = 47;
            radioButtonChuaxuly.TabStop = true;
            radioButtonChuaxuly.Text = "Chưa Xử Lý";
            radioButtonChuaxuly.UseVisualStyleBackColor = true;
            // 
            // radioButtonDaxuly
            // 
            radioButtonDaxuly.AutoSize = true;
            radioButtonDaxuly.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonDaxuly.Location = new Point(6, 38);
            radioButtonDaxuly.Name = "radioButtonDaxuly";
            radioButtonDaxuly.Size = new Size(74, 19);
            radioButtonDaxuly.TabIndex = 46;
            radioButtonDaxuly.TabStop = true;
            radioButtonDaxuly.Text = "Đã Xử Lý";
            radioButtonDaxuly.UseVisualStyleBackColor = true;
            // 
            // checkBoxThoigian
            // 
            checkBoxThoigian.AutoSize = true;
            checkBoxThoigian.Location = new Point(58, 212);
            checkBoxThoigian.Name = "checkBoxThoigian";
            checkBoxThoigian.Size = new Size(161, 19);
            checkBoxThoigian.TabIndex = 66;
            checkBoxThoigian.Text = "Tìm Kiếm Theo Thời Gian";
            checkBoxThoigian.UseVisualStyleBackColor = true;
            checkBoxThoigian.CheckedChanged += checkBoxThoigian_CheckedChanged;
            // 
            // groupBoxThoiGian
            // 
            groupBoxThoiGian.Controls.Add(dateTimePickerKetThuc);
            groupBoxThoiGian.Controls.Add(label3);
            groupBoxThoiGian.Controls.Add(label2);
            groupBoxThoiGian.Controls.Add(dateTimePickerBatDau);
            groupBoxThoiGian.Location = new Point(12, 46);
            groupBoxThoiGian.Name = "groupBoxThoiGian";
            groupBoxThoiGian.Size = new Size(248, 160);
            groupBoxThoiGian.TabIndex = 65;
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
            label2.Size = new Size(52, 15);
            label2.TabIndex = 54;
            label2.Text = "Từ Ngày";
            // 
            // dateTimePickerBatDau
            // 
            dateTimePickerBatDau.Location = new Point(28, 47);
            dateTimePickerBatDau.Name = "dateTimePickerBatDau";
            dateTimePickerBatDau.Size = new Size(200, 23);
            dateTimePickerBatDau.TabIndex = 53;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point(416, 46);
            label1.Name = "label1";
            label1.Size = new Size(252, 37);
            label1.TabIndex = 67;
            label1.Text = "Thống Kê Vi Phạm";
            // 
            // buttonTimKiem
            // 
            buttonTimKiem.Location = new Point(1163, 136);
            buttonTimKiem.Name = "buttonTimKiem";
            buttonTimKiem.Size = new Size(83, 36);
            buttonTimKiem.TabIndex = 68;
            buttonTimKiem.Text = "Tìm Kiếm";
            buttonTimKiem.UseVisualStyleBackColor = true;
            buttonTimKiem.Click += buttonTimKiem_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(1163, 185);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(83, 36);
            buttonLoad.TabIndex = 69;
            buttonLoad.Text = "Tải Lại";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxDemViPham);
            groupBox2.Controls.Add(textBoxDemDaXL);
            groupBox2.Controls.Add(textBoxDemChuaXL);
            groupBox2.Location = new Point(813, 32);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(467, 99);
            groupBox2.TabIndex = 70;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông Tin Thống kê";
            // 
            // textBoxDemViPham
            // 
            textBoxDemViPham.Location = new Point(16, 29);
            textBoxDemViPham.Name = "textBoxDemViPham";
            textBoxDemViPham.ReadOnly = true;
            textBoxDemViPham.Size = new Size(193, 23);
            textBoxDemViPham.TabIndex = 67;
            textBoxDemViPham.WordWrap = false;
            // 
            // textBoxDemDaXL
            // 
            textBoxDemDaXL.Location = new Point(248, 22);
            textBoxDemDaXL.Name = "textBoxDemDaXL";
            textBoxDemDaXL.ReadOnly = true;
            textBoxDemDaXL.Size = new Size(193, 23);
            textBoxDemDaXL.TabIndex = 65;
            textBoxDemDaXL.WordWrap = false;
            // 
            // textBoxDemChuaXL
            // 
            textBoxDemChuaXL.Location = new Point(248, 59);
            textBoxDemChuaXL.Name = "textBoxDemChuaXL";
            textBoxDemChuaXL.ReadOnly = true;
            textBoxDemChuaXL.Size = new Size(193, 23);
            textBoxDemChuaXL.TabIndex = 66;
            textBoxDemChuaXL.WordWrap = false;
            // 
            // thongkevipham
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 599);
            Controls.Add(groupBox2);
            Controls.Add(buttonLoad);
            Controls.Add(buttonTimKiem);
            Controls.Add(label1);
            Controls.Add(checkBoxThoigian);
            Controls.Add(groupBoxThoiGian);
            Controls.Add(groupBoxTrangThai);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(groupBoxThongTin);
            Name = "thongkevipham";
            Text = "thongkevipham";
            Load += thongkevipham_Load;
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBoxTrangThai.ResumeLayout(false);
            groupBoxTrangThai.PerformLayout();
            groupBoxThoiGian.ResumeLayout(false);
            groupBoxThoiGian.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxThongTin;
        private ComboBox comboBoxSinhVien;
        private ComboBox comboBoxMucViPham;
        private Label labelMucvipham;
        private Label labelMSSV;
        private DataGridView dataGridViewThongTin;
        private GroupBox groupBoxTrangThai;
        private RadioButton radioButtonChuaxuly;
        private RadioButton radioButtonDaxuly;
        private CheckBox checkBoxThoigian;
        private GroupBox groupBoxThoiGian;
        private DateTimePicker dateTimePickerKetThuc;
        private Label label3;
        private Label label2;
        private DateTimePicker dateTimePickerBatDau;
        private Label label1;
        private Button buttonTimKiem;
        private Button buttonLoad;
        private GroupBox groupBox2;
        private TextBox textBoxDemViPham;
        private TextBox textBoxDemDaXL;
        private TextBox textBoxDemChuaXL;
        private RadioButton radioButtonAll;
    }
}