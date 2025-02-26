namespace abc.HoanThanh.ThongKeViPham
{
    partial class ViPham
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
            buttonXoa = new Button();
            radioButtonXoa = new RadioButton();
            radioButtonSua = new RadioButton();
            buttonSua = new Button();
            GroupBoxChucNang = new GroupBox();
            radioButtonThem = new RadioButton();
            dataGridViewThongTin = new DataGridView();
            groupBoxThongTin = new GroupBox();
            buttonHuyBo = new Button();
            buttonTimKiem = new Button();
            textBoxNoiDung = new TextBox();
            labelGhichu = new Label();
            textBoxGhiChu = new TextBox();
            comboBoxSinhVien = new ComboBox();
            dateTimePickerNgayViPham = new DateTimePicker();
            comboBoxMucViPham = new ComboBox();
            labelMucvipham = new Label();
            labelMSSV = new Label();
            labelNoiDung = new Label();
            labelNgay = new Label();
            buttonLuu = new Button();
            buttonHuy = new Button();
            labelViPham = new Label();
            label1 = new Label();
            groupBoxSuaXoa = new GroupBox();
            buttonThem = new Button();
            buttonTroLai = new Button();
            GroupBoxChucNang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBoxThongTin.SuspendLayout();
            groupBoxSuaXoa.SuspendLayout();
            SuspendLayout();
            // 
            // buttonXoa
            // 
            buttonXoa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonXoa.Location = new Point(26, 91);
            buttonXoa.Name = "buttonXoa";
            buttonXoa.Size = new Size(75, 29);
            buttonXoa.TabIndex = 68;
            buttonXoa.Text = "Xóa";
            buttonXoa.UseVisualStyleBackColor = true;
            buttonXoa.Click += buttonXoa_Click;
            // 
            // radioButtonXoa
            // 
            radioButtonXoa.AutoSize = true;
            radioButtonXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonXoa.Location = new Point(163, 19);
            radioButtonXoa.Margin = new Padding(3, 2, 3, 2);
            radioButtonXoa.Name = "radioButtonXoa";
            radioButtonXoa.Size = new Size(46, 19);
            radioButtonXoa.TabIndex = 2;
            radioButtonXoa.TabStop = true;
            radioButtonXoa.Text = "Xóa";
            radioButtonXoa.UseVisualStyleBackColor = true;
            radioButtonXoa.CheckedChanged += radioButtonXoa_CheckedChanged;
            // 
            // radioButtonSua
            // 
            radioButtonSua.AutoSize = true;
            radioButtonSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonSua.Location = new Point(101, 18);
            radioButtonSua.Margin = new Padding(3, 2, 3, 2);
            radioButtonSua.Name = "radioButtonSua";
            radioButtonSua.Size = new Size(46, 19);
            radioButtonSua.TabIndex = 1;
            radioButtonSua.TabStop = true;
            radioButtonSua.Text = "Sửa";
            radioButtonSua.UseVisualStyleBackColor = true;
            radioButtonSua.CheckedChanged += radioButtonSua_CheckedChanged;
            // 
            // buttonSua
            // 
            buttonSua.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonSua.Location = new Point(26, 53);
            buttonSua.Name = "buttonSua";
            buttonSua.Size = new Size(75, 29);
            buttonSua.TabIndex = 66;
            buttonSua.Text = "Sửa";
            buttonSua.UseVisualStyleBackColor = true;
            buttonSua.Click += buttonSua_Click;
            // 
            // GroupBoxChucNang
            // 
            GroupBoxChucNang.Controls.Add(radioButtonXoa);
            GroupBoxChucNang.Controls.Add(radioButtonSua);
            GroupBoxChucNang.Controls.Add(radioButtonThem);
            GroupBoxChucNang.Location = new Point(35, 24);
            GroupBoxChucNang.Margin = new Padding(3, 2, 3, 2);
            GroupBoxChucNang.Name = "GroupBoxChucNang";
            GroupBoxChucNang.Padding = new Padding(3, 2, 3, 2);
            GroupBoxChucNang.Size = new Size(236, 55);
            GroupBoxChucNang.TabIndex = 67;
            GroupBoxChucNang.TabStop = false;
            GroupBoxChucNang.Text = "Chức Năng";
            // 
            // radioButtonThem
            // 
            radioButtonThem.AutoSize = true;
            radioButtonThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radioButtonThem.Location = new Point(30, 18);
            radioButtonThem.Margin = new Padding(3, 2, 3, 2);
            radioButtonThem.Name = "radioButtonThem";
            radioButtonThem.Size = new Size(57, 19);
            radioButtonThem.TabIndex = 0;
            radioButtonThem.TabStop = true;
            radioButtonThem.Text = "Thêm";
            radioButtonThem.UseVisualStyleBackColor = true;
            radioButtonThem.CheckedChanged += radioThem_CheckedChanged;
            // 
            // dataGridViewThongTin
            // 
            dataGridViewThongTin.AllowUserToAddRows = false;
            dataGridViewThongTin.AllowUserToOrderColumns = true;
            dataGridViewThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThongTin.BackgroundColor = SystemColors.ButtonHighlight;
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
            dataGridViewThongTin.Location = new Point(-7, 274);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewThongTin.Size = new Size(1324, 411);
            dataGridViewThongTin.TabIndex = 61;
            // 
            // groupBoxThongTin
            // 
            groupBoxThongTin.Controls.Add(buttonHuyBo);
            groupBoxThongTin.Controls.Add(buttonTimKiem);
            groupBoxThongTin.Controls.Add(textBoxNoiDung);
            groupBoxThongTin.Controls.Add(labelGhichu);
            groupBoxThongTin.Controls.Add(textBoxGhiChu);
            groupBoxThongTin.Controls.Add(comboBoxSinhVien);
            groupBoxThongTin.Controls.Add(dateTimePickerNgayViPham);
            groupBoxThongTin.Controls.Add(comboBoxMucViPham);
            groupBoxThongTin.Controls.Add(labelMucvipham);
            groupBoxThongTin.Controls.Add(labelMSSV);
            groupBoxThongTin.Controls.Add(labelNoiDung);
            groupBoxThongTin.Controls.Add(labelNgay);
            groupBoxThongTin.Font = new Font("Segoe UI", 10F);
            groupBoxThongTin.Location = new Point(24, 83);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(1022, 127);
            groupBoxThongTin.TabIndex = 62;
            groupBoxThongTin.TabStop = false;
            groupBoxThongTin.Text = "Thông tin";
            // 
            // buttonHuyBo
            // 
            buttonHuyBo.BackColor = Color.Magenta;
            buttonHuyBo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonHuyBo.ForeColor = SystemColors.ButtonHighlight;
            buttonHuyBo.Location = new Point(398, 36);
            buttonHuyBo.Name = "buttonHuyBo";
            buttonHuyBo.Size = new Size(101, 26);
            buttonHuyBo.TabIndex = 74;
            buttonHuyBo.Text = "Hủy Bỏ ";
            buttonHuyBo.UseVisualStyleBackColor = false;
            buttonHuyBo.Click += buttonHuyBo_Click;
            // 
            // buttonTimKiem
            // 
            buttonTimKiem.BackColor = Color.CadetBlue;
            buttonTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonTimKiem.ForeColor = SystemColors.ButtonHighlight;
            buttonTimKiem.Location = new Point(278, 36);
            buttonTimKiem.Name = "buttonTimKiem";
            buttonTimKiem.Size = new Size(101, 26);
            buttonTimKiem.TabIndex = 73;
            buttonTimKiem.Text = "Tìm Kiếm ";
            buttonTimKiem.UseVisualStyleBackColor = false;
            buttonTimKiem.Click += buttonTimKiem_Click;
            // 
            // textBoxNoiDung
            // 
            textBoxNoiDung.Font = new Font("Segoe UI", 10F);
            textBoxNoiDung.Location = new Point(631, 36);
            textBoxNoiDung.Multiline = true;
            textBoxNoiDung.Name = "textBoxNoiDung";
            textBoxNoiDung.ScrollBars = ScrollBars.Vertical;
            textBoxNoiDung.Size = new Size(178, 69);
            textBoxNoiDung.TabIndex = 61;
            textBoxNoiDung.TextChanged += Ketqua;
            // 
            // labelGhichu
            // 
            labelGhichu.AutoSize = true;
            labelGhichu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelGhichu.Location = new Point(869, 20);
            labelGhichu.Name = "labelGhichu";
            labelGhichu.Size = new Size(68, 15);
            labelGhichu.TabIndex = 60;
            labelGhichu.Text = "Ghi Chú VP";
            // 
            // textBoxGhiChu
            // 
            textBoxGhiChu.Font = new Font("Segoe UI", 10F);
            textBoxGhiChu.Location = new Point(818, 38);
            textBoxGhiChu.Multiline = true;
            textBoxGhiChu.Name = "textBoxGhiChu";
            textBoxGhiChu.ScrollBars = ScrollBars.Vertical;
            textBoxGhiChu.Size = new Size(189, 69);
            textBoxGhiChu.TabIndex = 59;
            textBoxGhiChu.TextChanged += Ketqua;
            // 
            // comboBoxSinhVien
            // 
            comboBoxSinhVien.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxSinhVien.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxSinhVien.FormattingEnabled = true;
            comboBoxSinhVien.Location = new Point(111, 36);
            comboBoxSinhVien.Name = "comboBoxSinhVien";
            comboBoxSinhVien.Size = new Size(132, 25);
            comboBoxSinhVien.TabIndex = 58;
            // 
            // dateTimePickerNgayViPham
            // 
            dateTimePickerNgayViPham.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgayViPham.Location = new Point(107, 79);
            dateTimePickerNgayViPham.MinDate = new DateTime(1998, 6, 23, 0, 0, 0, 0);
            dateTimePickerNgayViPham.Name = "dateTimePickerNgayViPham";
            dateTimePickerNgayViPham.Size = new Size(197, 25);
            dateTimePickerNgayViPham.TabIndex = 57;
            dateTimePickerNgayViPham.Value = new DateTime(2025, 2, 25, 0, 0, 0, 0);
            dateTimePickerNgayViPham.ValueChanged += Ketqua;
            // 
            // comboBoxMucViPham
            // 
            comboBoxMucViPham.FormattingEnabled = true;
            comboBoxMucViPham.Location = new Point(411, 79);
            comboBoxMucViPham.Name = "comboBoxMucViPham";
            comboBoxMucViPham.Size = new Size(202, 25);
            comboBoxMucViPham.TabIndex = 50;
            comboBoxMucViPham.SelectedIndexChanged += Ketqua;
            // 
            // labelMucvipham
            // 
            labelMucvipham.AutoSize = true;
            labelMucvipham.Location = new Point(314, 85);
            labelMucvipham.Name = "labelMucvipham";
            labelMucvipham.Size = new Size(91, 19);
            labelMucvipham.TabIndex = 49;
            labelMucvipham.Text = "Mục Vi Phạm";
            // 
            // labelMSSV
            // 
            labelMSSV.AutoSize = true;
            labelMSSV.Location = new Point(5, 38);
            labelMSSV.Name = "labelMSSV";
            labelMSSV.Size = new Size(105, 19);
            labelMSSV.TabIndex = 41;
            labelMSSV.Text = "MSSV (nội trú) :";
            labelMSSV.UseMnemonic = false;
            // 
            // labelNoiDung
            // 
            labelNoiDung.AutoSize = true;
            labelNoiDung.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNoiDung.Location = new Point(675, 18);
            labelNoiDung.Name = "labelNoiDung";
            labelNoiDung.Size = new Size(72, 19);
            labelNoiDung.TabIndex = 30;
            labelNoiDung.Text = "Nội Dung";
            // 
            // labelNgay
            // 
            labelNgay.AutoSize = true;
            labelNgay.Location = new Point(5, 85);
            labelNgay.Name = "labelNgay";
            labelNgay.Size = new Size(96, 19);
            labelNgay.TabIndex = 29;
            labelNgay.Text = "Ngày Vi Phạm";
            // 
            // buttonLuu
            // 
            buttonLuu.BackColor = Color.Lime;
            buttonLuu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonLuu.ForeColor = SystemColors.ButtonHighlight;
            buttonLuu.Location = new Point(864, 230);
            buttonLuu.Name = "buttonLuu";
            buttonLuu.Size = new Size(75, 39);
            buttonLuu.TabIndex = 64;
            buttonLuu.Text = "Lưu";
            buttonLuu.UseVisualStyleBackColor = false;
            buttonLuu.Click += buttonLuu_Click;
            // 
            // buttonHuy
            // 
            buttonHuy.BackColor = Color.Salmon;
            buttonHuy.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonHuy.ForeColor = SystemColors.ButtonHighlight;
            buttonHuy.Location = new Point(1078, 230);
            buttonHuy.Name = "buttonHuy";
            buttonHuy.Size = new Size(75, 39);
            buttonHuy.TabIndex = 63;
            buttonHuy.Text = "Hủy";
            buttonHuy.UseVisualStyleBackColor = false;
            buttonHuy.Click += buttonHuy_Click;
            // 
            // labelViPham
            // 
            labelViPham.AutoSize = true;
            labelViPham.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            labelViPham.Location = new Point(501, 24);
            labelViPham.Name = "labelViPham";
            labelViPham.Size = new Size(256, 37);
            labelViPham.TabIndex = 65;
            labelViPham.Text = "Sinh Viên Vi Phạm ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.ForeColor = Color.MediumBlue;
            label1.Location = new Point(12, 244);
            label1.Name = "label1";
            label1.Size = new Size(192, 28);
            label1.TabIndex = 69;
            label1.Text = "Thông Tin Vi Phạm";
            // 
            // groupBoxSuaXoa
            // 
            groupBoxSuaXoa.Controls.Add(buttonThem);
            groupBoxSuaXoa.Controls.Add(buttonXoa);
            groupBoxSuaXoa.Controls.Add(buttonSua);
            groupBoxSuaXoa.Location = new Point(1052, 84);
            groupBoxSuaXoa.Name = "groupBoxSuaXoa";
            groupBoxSuaXoa.Size = new Size(131, 126);
            groupBoxSuaXoa.TabIndex = 70;
            groupBoxSuaXoa.TabStop = false;
            // 
            // buttonThem
            // 
            buttonThem.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonThem.Location = new Point(26, 18);
            buttonThem.Name = "buttonThem";
            buttonThem.Size = new Size(75, 29);
            buttonThem.TabIndex = 69;
            buttonThem.Text = "Thêm";
            buttonThem.UseVisualStyleBackColor = true;
            buttonThem.Click += buttonThem_Click;
            // 
            // buttonTroLai
            // 
            buttonTroLai.BackColor = Color.Gold;
            buttonTroLai.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonTroLai.ForeColor = SystemColors.ButtonHighlight;
            buttonTroLai.Location = new Point(945, 230);
            buttonTroLai.Name = "buttonTroLai";
            buttonTroLai.Size = new Size(86, 39);
            buttonTroLai.TabIndex = 71;
            buttonTroLai.Text = "Hoàn Tác";
            buttonTroLai.UseVisualStyleBackColor = false;
            buttonTroLai.Click += buttonTroLai_Click;
            // 
            // ViPham
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 672);
            Controls.Add(buttonTroLai);
            Controls.Add(groupBoxSuaXoa);
            Controls.Add(label1);
            Controls.Add(GroupBoxChucNang);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(groupBoxThongTin);
            Controls.Add(buttonLuu);
            Controls.Add(buttonHuy);
            Controls.Add(labelViPham);
            Name = "ViPham";
            Text = " ";
            Load += ViPham_Load;
            GroupBoxChucNang.ResumeLayout(false);
            GroupBoxChucNang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            groupBoxSuaXoa.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonXoa;
        private RadioButton radioButtonXoa;
        private RadioButton radioButtonSua;
        private Button buttonSua;
        private GroupBox GroupBoxChucNang;
        private RadioButton radioButtonThem;
        private DataGridView dataGridViewThongTin;
        private GroupBox groupBoxThongTin;
        private Label labelGhichu;
        private TextBox textBoxGhiChu;
        private ComboBox comboBoxSinhVien;
        private DateTimePicker dateTimePickerNgayViPham;
        private ComboBox comboBoxMucViPham;
        private Label labelMucvipham;
        private Label labelMSSV;
        private Label labelNoiDung;
        private Label labelNgay;
        private Button buttonLuu;
        private Button buttonHuy;
        private Label labelViPham;
        private Label label1;
        private GroupBox groupBoxSuaXoa;
        private Button buttonThem;
        private Button buttonTroLai;
        private TextBox textBoxNoiDung;
        private Button buttonTimKiem;
        private Button buttonHuyBo;
    }
}