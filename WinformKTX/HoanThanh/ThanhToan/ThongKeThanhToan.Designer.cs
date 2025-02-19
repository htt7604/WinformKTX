namespace abc.HoanThanh.ThanhToan
{
    partial class ThongKeThanhToan
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
            dataGridViewThongTin = new DataGridView();
            label1 = new Label();
            groupBoxThongTin = new GroupBox();
            labelGiuong = new Label();
            comboBoxGiuong = new ComboBox();
            comboBoxLoaiTang = new ComboBox();
            labelLoaiPhong = new Label();
            comboBoxPhong = new ComboBox();
            comboBoxTang = new ComboBox();
            labelTenPhong = new Label();
            label10 = new Label();
            dateTimePickerKetThuc = new DateTimePicker();
            dateTimePickerBatDau = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            buttonTimKiem = new Button();
            buttonTroLai = new Button();
            groupBoxThoiGian = new GroupBox();
            label4 = new Label();
            comboBoxMSSV = new ComboBox();
            radioButtonDaTT = new RadioButton();
            radioButtonChuaTT = new RadioButton();
            radioButtonAll = new RadioButton();
            groupBox1 = new GroupBox();
            checkBoxThoigian = new CheckBox();
            textBoxDemSVDaTT = new TextBox();
            textBoxDemSVChuTT = new TextBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBoxThongTin.SuspendLayout();
            groupBoxThoiGian.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
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
            dataGridViewThongTin.Location = new Point(-6, 289);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1306, 309);
            dataGridViewThongTin.TabIndex = 54;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point(479, 23);
            label1.Name = "label1";
            label1.Size = new Size(298, 37);
            label1.TabIndex = 55;
            label1.Text = "Thong ke Thanh Toan ";
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
            groupBoxThongTin.Location = new Point(266, 66);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(590, 171);
            groupBoxThongTin.TabIndex = 56;
            groupBoxThongTin.TabStop = false;
            groupBoxThongTin.Text = "Thông tin";
            // 
            // labelGiuong
            // 
            labelGiuong.AutoSize = true;
            labelGiuong.Location = new Point(351, 86);
            labelGiuong.Name = "labelGiuong";
            labelGiuong.Size = new Size(79, 19);
            labelGiuong.TabIndex = 51;
            labelGiuong.Text = "Ten Giuong";
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
            labelLoaiPhong.Location = new Point(40, 27);
            labelLoaiPhong.Name = "labelLoaiPhong";
            labelLoaiPhong.Size = new Size(66, 19);
            labelLoaiPhong.TabIndex = 41;
            labelLoaiPhong.Text = "Loai Tang";
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
            labelTenPhong.Location = new Point(351, 27);
            labelTenPhong.Name = "labelTenPhong";
            labelTenPhong.Size = new Size(74, 19);
            labelTenPhong.TabIndex = 30;
            labelTenPhong.Text = "Ten Phong";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(40, 86);
            label10.Name = "label10";
            label10.Size = new Size(39, 19);
            label10.TabIndex = 29;
            label10.Text = "Tầng";
            // 
            // dateTimePickerKetThuc
            // 
            dateTimePickerKetThuc.Location = new Point(28, 106);
            dateTimePickerKetThuc.Name = "dateTimePickerKetThuc";
            dateTimePickerKetThuc.Size = new Size(200, 23);
            dateTimePickerKetThuc.TabIndex = 52;
            // 
            // dateTimePickerBatDau
            // 
            dateTimePickerBatDau.Location = new Point(28, 47);
            dateTimePickerBatDau.Name = "dateTimePickerBatDau";
            dateTimePickerBatDau.Size = new Size(200, 23);
            dateTimePickerBatDau.TabIndex = 53;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 29);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 54;
            label2.Text = "Tu Ngay:";
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
            // buttonTimKiem
            // 
            buttonTimKiem.BackColor = Color.SpringGreen;
            buttonTimKiem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonTimKiem.ForeColor = SystemColors.ControlLightLight;
            buttonTimKiem.Location = new Point(879, 167);
            buttonTimKiem.Name = "buttonTimKiem";
            buttonTimKiem.Size = new Size(84, 34);
            buttonTimKiem.TabIndex = 56;
            buttonTimKiem.Text = "Tim Kiem";
            buttonTimKiem.UseVisualStyleBackColor = false;
            buttonTimKiem.Click += button1_Click;
            // 
            // buttonTroLai
            // 
            buttonTroLai.BackColor = Color.Gold;
            buttonTroLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonTroLai.ForeColor = SystemColors.ControlLightLight;
            buttonTroLai.Location = new Point(969, 167);
            buttonTroLai.Name = "buttonTroLai";
            buttonTroLai.Size = new Size(83, 34);
            buttonTroLai.TabIndex = 57;
            buttonTroLai.Text = "Tro Lai";
            buttonTroLai.UseVisualStyleBackColor = false;
            buttonTroLai.Click += buttonTroLai_Click;
            // 
            // groupBoxThoiGian
            // 
            groupBoxThoiGian.Controls.Add(dateTimePickerKetThuc);
            groupBoxThoiGian.Controls.Add(label3);
            groupBoxThoiGian.Controls.Add(label2);
            groupBoxThoiGian.Controls.Add(dateTimePickerBatDau);
            groupBoxThoiGian.Location = new Point(12, 66);
            groupBoxThoiGian.Name = "groupBoxThoiGian";
            groupBoxThoiGian.Size = new Size(248, 171);
            groupBoxThoiGian.TabIndex = 58;
            groupBoxThoiGian.TabStop = false;
            groupBoxThoiGian.Text = "Thoi Gian";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(951, 111);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 59;
            label4.Text = "MSSV";
            // 
            // comboBoxMSSV
            // 
            comboBoxMSSV.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxMSSV.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxMSSV.Font = new Font("Segoe UI", 12F);
            comboBoxMSSV.FormattingEnabled = true;
            comboBoxMSSV.Location = new Point(892, 128);
            comboBoxMSSV.Margin = new Padding(3, 2, 3, 2);
            comboBoxMSSV.Name = "comboBoxMSSV";
            comboBoxMSSV.Size = new Size(160, 29);
            comboBoxMSSV.TabIndex = 52;
            // 
            // radioButtonDaTT
            // 
            radioButtonDaTT.AutoSize = true;
            radioButtonDaTT.Location = new Point(6, 22);
            radioButtonDaTT.Name = "radioButtonDaTT";
            radioButtonDaTT.Size = new Size(119, 23);
            radioButtonDaTT.TabIndex = 60;
            radioButtonDaTT.TabStop = true;
            radioButtonDaTT.Text = "Da Thanh Toan";
            radioButtonDaTT.UseVisualStyleBackColor = true;
            // 
            // radioButtonChuaTT
            // 
            radioButtonChuaTT.AutoSize = true;
            radioButtonChuaTT.Location = new Point(140, 22);
            radioButtonChuaTT.Name = "radioButtonChuaTT";
            radioButtonChuaTT.Size = new Size(134, 23);
            radioButtonChuaTT.TabIndex = 61;
            radioButtonChuaTT.TabStop = true;
            radioButtonChuaTT.Text = "Chua Thanh Toan";
            radioButtonChuaTT.UseVisualStyleBackColor = true;
            // 
            // radioButtonAll
            // 
            radioButtonAll.AutoSize = true;
            radioButtonAll.Location = new Point(281, 22);
            radioButtonAll.Name = "radioButtonAll";
            radioButtonAll.Size = new Size(64, 23);
            radioButtonAll.TabIndex = 62;
            radioButtonAll.TabStop = true;
            radioButtonAll.Text = "Tat Ca";
            radioButtonAll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonDaTT);
            groupBox1.Controls.Add(radioButtonAll);
            groupBox1.Controls.Add(radioButtonChuaTT);
            groupBox1.Font = new Font("Segoe UI", 10F);
            groupBox1.Location = new Point(916, 237);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(364, 47);
            groupBox1.TabIndex = 63;
            groupBox1.TabStop = false;
            groupBox1.Text = "Trang Thai";
            // 
            // checkBoxThoigian
            // 
            checkBoxThoigian.AutoSize = true;
            checkBoxThoigian.Location = new Point(51, 243);
            checkBoxThoigian.Name = "checkBoxThoigian";
            checkBoxThoigian.Size = new Size(161, 19);
            checkBoxThoigian.TabIndex = 64;
            checkBoxThoigian.Text = "Tim Kiem Theo Thoi Gian";
            checkBoxThoigian.UseVisualStyleBackColor = true;
            checkBoxThoigian.CheckedChanged += checkBoxThoigian_CheckedChanged;
            // 
            // textBoxDemSVDaTT
            // 
            textBoxDemSVDaTT.Location = new Point(7, 51);
            textBoxDemSVDaTT.Name = "textBoxDemSVDaTT";
            textBoxDemSVDaTT.ReadOnly = true;
            textBoxDemSVDaTT.Size = new Size(193, 23);
            textBoxDemSVDaTT.TabIndex = 65;
            textBoxDemSVDaTT.WordWrap = false;
            // 
            // textBoxDemSVChuTT
            // 
            textBoxDemSVChuTT.Location = new Point(6, 104);
            textBoxDemSVChuTT.Name = "textBoxDemSVChuTT";
            textBoxDemSVChuTT.ReadOnly = true;
            textBoxDemSVChuTT.Size = new Size(193, 23);
            textBoxDemSVChuTT.TabIndex = 66;
            textBoxDemSVChuTT.WordWrap = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxDemSVDaTT);
            groupBox2.Controls.Add(textBoxDemSVChuTT);
            groupBox2.Location = new Point(1080, 66);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 171);
            groupBox2.TabIndex = 67;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thong ke";
            // 
            // ThongKeThanhToan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 594);
            Controls.Add(groupBox2);
            Controls.Add(checkBoxThoigian);
            Controls.Add(groupBox1);
            Controls.Add(comboBoxMSSV);
            Controls.Add(label4);
            Controls.Add(groupBoxThoiGian);
            Controls.Add(buttonTroLai);
            Controls.Add(buttonTimKiem);
            Controls.Add(groupBoxThongTin);
            Controls.Add(label1);
            Controls.Add(dataGridViewThongTin);
            Name = "ThongKeThanhToan";
            Text = "ThongKeThanhToan";
            Load += ThongKeThanhToan_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            groupBoxThoiGian.ResumeLayout(false);
            groupBoxThoiGian.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewThongTin;
        private Label label1;
        private GroupBox groupBoxThongTin;
        private Label label3;
        private Label label2;
        private DateTimePicker dateTimePickerBatDau;
        private DateTimePicker dateTimePickerKetThuc;
        private Label labelGiuong;
        private ComboBox comboBoxGiuong;
        private ComboBox comboBoxLoaiTang;
        private Label labelLoaiPhong;
        private ComboBox comboBoxPhong;
        private ComboBox comboBoxTang;
        private Label labelTenPhong;
        private Label label10;
        private Button buttonTimKiem;
        private Button buttonTroLai;
        private GroupBox groupBoxThoiGian;
        private Label label4;
        private ComboBox comboBoxMSSV;
        private RadioButton radioButtonDaTT;
        private RadioButton radioButtonChuaTT;
        private RadioButton radioButtonAll;
        private GroupBox groupBox1;
        private CheckBox checkBoxThoigian;
        private TextBox textBoxDemSVDaTT;
        private TextBox textBoxDemSVChuTT;
        private GroupBox groupBox2;
    }
}