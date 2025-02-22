namespace abc.HoanThanh.ThongKeSinhVien
{
    partial class ThongTinNoiTru
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
            label1 = new Label();
            dataGridViewThongTin = new DataGridView();
            label2 = new Label();
            groupBoxTrangThai = new GroupBox();
            radioButtonChuaNoiTru = new RadioButton();
            radioButtonChoGianHan = new RadioButton();
            radioButtonDangNoiTru = new RadioButton();
            groupBoxThongTin = new GroupBox();
            labelGiuong = new Label();
            comboBoxGiuong = new ComboBox();
            comboBoxLoaiTang = new ComboBox();
            labelLoaiPhong = new Label();
            comboBoxPhong = new ComboBox();
            comboBoxTang = new ComboBox();
            labelTenPhong = new Label();
            label10 = new Label();
            buttonLoad = new Button();
            buttonTimKiemGiuong = new Button();
            groupBox2 = new GroupBox();
            textBoxDemSV = new TextBox();
            textBoxDemSVNoiTru = new TextBox();
            textBoxDemSVChuaNoiTru = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBoxTrangThai.SuspendLayout();
            groupBoxThongTin.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point(450, 9);
            label1.Name = "label1";
            label1.Size = new Size(443, 37);
            label1.TabIndex = 43;
            label1.Text = "Thông tin Sinh Viên Đang Nội Trú";
            // 
            // dataGridViewThongTin
            // 
            dataGridViewThongTin.AllowUserToOrderColumns = true;
            dataGridViewThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThongTin.BackgroundColor = Color.FromArgb(250, 255, 255);
            dataGridViewThongTin.BorderStyle = BorderStyle.None;
            dataGridViewThongTin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewThongTin.Location = new Point(-5, 258);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1306, 345);
            dataGridViewThongTin.TabIndex = 53;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(7, 231);
            label2.Name = "label2";
            label2.Size = new Size(192, 25);
            label2.TabIndex = 54;
            label2.Text = "Thông Tin Sinh Viên";
            // 
            // groupBoxTrangThai
            // 
            groupBoxTrangThai.Controls.Add(radioButtonChuaNoiTru);
            groupBoxTrangThai.Controls.Add(radioButtonChoGianHan);
            groupBoxTrangThai.Controls.Add(radioButtonDangNoiTru);
            groupBoxTrangThai.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            groupBoxTrangThai.Location = new Point(662, 65);
            groupBoxTrangThai.Name = "groupBoxTrangThai";
            groupBoxTrangThai.Size = new Size(165, 154);
            groupBoxTrangThai.TabIndex = 49;
            groupBoxTrangThai.TabStop = false;
            groupBoxTrangThai.Text = "Trạng Thái ";
            // 
            // radioButtonChuaNoiTru
            // 
            radioButtonChuaNoiTru.AutoSize = true;
            radioButtonChuaNoiTru.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            radioButtonChuaNoiTru.Location = new Point(23, 104);
            radioButtonChuaNoiTru.Name = "radioButtonChuaNoiTru";
            radioButtonChuaNoiTru.Size = new Size(136, 24);
            radioButtonChuaNoiTru.TabIndex = 48;
            radioButtonChuaNoiTru.TabStop = true;
            radioButtonChuaNoiTru.Text = "CHƯA NỘI TRÚ";
            radioButtonChuaNoiTru.UseVisualStyleBackColor = true;
            radioButtonChuaNoiTru.CheckedChanged += radioButtonChuaNoiTru_CheckedChanged;
            // 
            // radioButtonChoGianHan
            // 
            radioButtonChoGianHan.AutoSize = true;
            radioButtonChoGianHan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            radioButtonChoGianHan.Location = new Point(23, 69);
            radioButtonChoGianHan.Name = "radioButtonChoGianHan";
            radioButtonChoGianHan.Size = new Size(140, 24);
            radioButtonChoGianHan.TabIndex = 47;
            radioButtonChoGianHan.TabStop = true;
            radioButtonChoGianHan.Text = "CHỜ GIAN HẠN";
            radioButtonChoGianHan.UseVisualStyleBackColor = true;
            radioButtonChoGianHan.CheckedChanged += radioButtonChoGianHan_CheckedChanged;
            // 
            // radioButtonDangNoiTru
            // 
            radioButtonDangNoiTru.AutoSize = true;
            radioButtonDangNoiTru.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            radioButtonDangNoiTru.Location = new Point(23, 35);
            radioButtonDangNoiTru.Name = "radioButtonDangNoiTru";
            radioButtonDangNoiTru.Size = new Size(138, 24);
            radioButtonDangNoiTru.TabIndex = 46;
            radioButtonDangNoiTru.TabStop = true;
            radioButtonDangNoiTru.Text = "ĐANG NỘI TRÚ";
            radioButtonDangNoiTru.UseVisualStyleBackColor = true;
            radioButtonDangNoiTru.CheckedChanged += radioButtonDangNoiTru_CheckedChanged;
            // 
            // groupBoxThongTin
            // 
            groupBoxThongTin.Controls.Add(labelGiuong);
            groupBoxThongTin.Controls.Add(comboBoxGiuong);
            groupBoxThongTin.Controls.Add(comboBoxLoaiTang);
            groupBoxThongTin.Controls.Add(labelLoaiPhong);
            groupBoxThongTin.Controls.Add(comboBoxPhong);
            groupBoxThongTin.Controls.Add(comboBoxTang);
            groupBoxThongTin.Controls.Add(labelTenPhong);
            groupBoxThongTin.Controls.Add(label10);
            groupBoxThongTin.Font = new Font("Segoe UI", 10F);
            groupBoxThongTin.Location = new Point(7, 65);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(613, 154);
            groupBoxThongTin.TabIndex = 48;
            groupBoxThongTin.TabStop = false;
            groupBoxThongTin.Text = "Thông tin";
            // 
            // labelGiuong
            // 
            labelGiuong.AutoSize = true;
            labelGiuong.Location = new Point(351, 87);
            labelGiuong.Name = "labelGiuong";
            labelGiuong.Size = new Size(54, 19);
            labelGiuong.TabIndex = 51;
            labelGiuong.Text = "Giường";
            // 
            // comboBoxGiuong
            // 
            comboBoxGiuong.Font = new Font("Segoe UI", 12F);
            comboBoxGiuong.FormattingEnabled = true;
            comboBoxGiuong.Location = new Point(351, 104);
            comboBoxGiuong.Margin = new Padding(3, 2, 3, 2);
            comboBoxGiuong.Name = "comboBoxGiuong";
            comboBoxGiuong.Size = new Size(223, 29);
            comboBoxGiuong.TabIndex = 50;
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
            labelLoaiPhong.Location = new Point(40, 28);
            labelLoaiPhong.Name = "labelLoaiPhong";
            labelLoaiPhong.Size = new Size(72, 19);
            labelLoaiPhong.TabIndex = 41;
            labelLoaiPhong.Text = "Loại Tầng ";
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
            comboBoxPhong.SelectedIndexChanged += comboBoxPhong_SelectedIndexChanged;
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
            labelTenPhong.Location = new Point(351, 28);
            labelTenPhong.Name = "labelTenPhong";
            labelTenPhong.Size = new Size(74, 19);
            labelTenPhong.TabIndex = 30;
            labelTenPhong.Text = "Tên Phòng";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(40, 87);
            label10.Name = "label10";
            label10.Size = new Size(39, 19);
            label10.TabIndex = 29;
            label10.Text = "Tầng";
            // 
            // buttonLoad
            // 
            buttonLoad.BackColor = Color.SandyBrown;
            buttonLoad.Font = new Font("Segoe UI", 12F);
            buttonLoad.ForeColor = SystemColors.ButtonHighlight;
            buttonLoad.Location = new Point(896, 141);
            buttonLoad.Margin = new Padding(3, 2, 3, 2);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(104, 32);
            buttonLoad.TabIndex = 43;
            buttonLoad.Text = "Làm Mới";
            buttonLoad.UseVisualStyleBackColor = false;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonTimKiemGiuong
            // 
            buttonTimKiemGiuong.BackColor = Color.Red;
            buttonTimKiemGiuong.Font = new Font("Segoe UI", 12F);
            buttonTimKiemGiuong.ForeColor = SystemColors.ButtonHighlight;
            buttonTimKiemGiuong.Location = new Point(896, 86);
            buttonTimKiemGiuong.Margin = new Padding(3, 2, 3, 2);
            buttonTimKiemGiuong.Name = "buttonTimKiemGiuong";
            buttonTimKiemGiuong.Size = new Size(104, 32);
            buttonTimKiemGiuong.TabIndex = 40;
            buttonTimKiemGiuong.Text = "Tìm Kiếm";
            buttonTimKiemGiuong.UseVisualStyleBackColor = false;
            buttonTimKiemGiuong.Click += buttonTimkiemgiuong_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxDemSV);
            groupBox2.Controls.Add(textBoxDemSVNoiTru);
            groupBox2.Controls.Add(textBoxDemSVChuaNoiTru);
            groupBox2.Location = new Point(1055, 48);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 171);
            groupBox2.TabIndex = 68;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông tin thống kê";
            // 
            // textBoxDemSV
            // 
            textBoxDemSV.Location = new Point(7, 47);
            textBoxDemSV.Name = "textBoxDemSV";
            textBoxDemSV.ReadOnly = true;
            textBoxDemSV.Size = new Size(193, 23);
            textBoxDemSV.TabIndex = 67;
            textBoxDemSV.WordWrap = false;
            // 
            // textBoxDemSVNoiTru
            // 
            textBoxDemSVNoiTru.Location = new Point(7, 86);
            textBoxDemSVNoiTru.Name = "textBoxDemSVNoiTru";
            textBoxDemSVNoiTru.ReadOnly = true;
            textBoxDemSVNoiTru.Size = new Size(193, 23);
            textBoxDemSVNoiTru.TabIndex = 65;
            textBoxDemSVNoiTru.WordWrap = false;
            // 
            // textBoxDemSVChuaNoiTru
            // 
            textBoxDemSVChuaNoiTru.Location = new Point(7, 124);
            textBoxDemSVChuaNoiTru.Name = "textBoxDemSVChuaNoiTru";
            textBoxDemSVChuaNoiTru.ReadOnly = true;
            textBoxDemSVChuaNoiTru.Size = new Size(193, 23);
            textBoxDemSVChuaNoiTru.TabIndex = 66;
            textBoxDemSVChuaNoiTru.WordWrap = false;
            // 
            // ThongTinNoiTru
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 594);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxThongTin);
            Controls.Add(label2);
            Controls.Add(buttonLoad);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(groupBoxTrangThai);
            Controls.Add(label1);
            Controls.Add(buttonTimKiemGiuong);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ThongTinNoiTru";
            Text = "ThongTinNoiTru";
            Load += ThongTinNoiTru_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBoxTrangThai.ResumeLayout(false);
            groupBoxTrangThai.PerformLayout();
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private DataGridView dataGridViewThongTin;
        private Label label2;
        private GroupBox groupBoxTrangThai;
        private RadioButton radioButtonChoGianHan;
        private RadioButton radioButtonDangNoiTru;
        private GroupBox groupBoxThongTin;
        private Label labelGiuong;
        private ComboBox comboBoxGiuong;
        private Button buttonLoad;
        private ComboBox comboBoxLoaiTang;
        private Label labelLoaiPhong;
        private Button buttonTimKiemGiuong;
        private ComboBox comboBoxPhong;
        private ComboBox comboBoxTang;
        private Label labelTenPhong;
        private Label label10;
        private RadioButton radioButtonChuaNoiTru;
        private GroupBox groupBox2;
        private TextBox textBoxDemSVNoiTru;
        private TextBox textBoxDemSVChuaNoiTru;
        private TextBox textBoxDemSV;
    }
}