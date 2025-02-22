namespace WinformKTX.HoanThanh.ThongKeCSVC_HuHong
{
    partial class ThongKeVatChat
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
            groupBoxThongTin = new GroupBox();
            comboBoxLoaiTang = new ComboBox();
            labelLoaiPhong = new Label();
            comboBoxPhong = new ComboBox();
            comboBoxTang = new ComboBox();
            labelTenPhong = new Label();
            label10 = new Label();
            dataGridViewThongTin = new DataGridView();
            groupBox1 = new GroupBox();
            textBoxDemHuHong = new TextBox();
            textBoxDemConTot = new TextBox();
            textBoxDemCSVC = new TextBox();
            label1 = new Label();
            buttonTimKiem = new Button();
            groupBox2 = new GroupBox();
            comboBoxTenVatChat = new ComboBox();
            groupBox3 = new GroupBox();
            comboBoxTinhTrang = new ComboBox();
            buttonDatLai = new Button();
            groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
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
            groupBoxThongTin.Location = new Point(12, 62);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(613, 154);
            groupBoxThongTin.TabIndex = 49;
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
            labelLoaiPhong.Location = new Point(40, 27);
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
            labelTenPhong.Location = new Point(351, 27);
            labelTenPhong.Name = "labelTenPhong";
            labelTenPhong.Size = new Size(49, 19);
            labelTenPhong.TabIndex = 30;
            labelTenPhong.Text = "Phòng";
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
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewThongTin.Location = new Point(-8, 220);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1306, 378);
            dataGridViewThongTin.TabIndex = 54;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxDemHuHong);
            groupBox1.Controls.Add(textBoxDemConTot);
            groupBox1.Controls.Add(textBoxDemCSVC);
            groupBox1.Location = new Point(837, 62);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(217, 154);
            groupBox1.TabIndex = 59;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin thống kê";
            // 
            // textBoxDemHuHong
            // 
            textBoxDemHuHong.Location = new Point(6, 80);
            textBoxDemHuHong.Name = "textBoxDemHuHong";
            textBoxDemHuHong.ReadOnly = true;
            textBoxDemHuHong.Size = new Size(185, 23);
            textBoxDemHuHong.TabIndex = 3;
            textBoxDemHuHong.WordWrap = false;
            // 
            // textBoxDemConTot
            // 
            textBoxDemConTot.Location = new Point(6, 125);
            textBoxDemConTot.Name = "textBoxDemConTot";
            textBoxDemConTot.ReadOnly = true;
            textBoxDemConTot.Size = new Size(185, 23);
            textBoxDemConTot.TabIndex = 2;
            textBoxDemConTot.WordWrap = false;
            // 
            // textBoxDemCSVC
            // 
            textBoxDemCSVC.Location = new Point(6, 31);
            textBoxDemCSVC.Name = "textBoxDemCSVC";
            textBoxDemCSVC.ReadOnly = true;
            textBoxDemCSVC.Size = new Size(185, 23);
            textBoxDemCSVC.TabIndex = 1;
            textBoxDemCSVC.WordWrap = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point(435, 9);
            label1.Name = "label1";
            label1.Size = new Size(333, 37);
            label1.TabIndex = 60;
            label1.Text = "Thống kê Cơ Sở Vật Chất";
            // 
            // buttonTimKiem
            // 
            buttonTimKiem.Location = new Point(1109, 93);
            buttonTimKiem.Name = "buttonTimKiem";
            buttonTimKiem.Size = new Size(75, 102);
            buttonTimKiem.TabIndex = 61;
            buttonTimKiem.Text = "Tìm Kiếm";
            buttonTimKiem.UseVisualStyleBackColor = true;
            buttonTimKiem.Click += buttonTimKiem_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(comboBoxTenVatChat);
            groupBox2.Location = new Point(631, 62);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 74);
            groupBox2.TabIndex = 62;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tên cơ sở vật chất ";
            // 
            // comboBoxTenVatChat
            // 
            comboBoxTenVatChat.Font = new Font("Segoe UI", 12F);
            comboBoxTenVatChat.FormattingEnabled = true;
            comboBoxTenVatChat.Location = new Point(6, 30);
            comboBoxTenVatChat.Margin = new Padding(3, 2, 3, 2);
            comboBoxTenVatChat.Name = "comboBoxTenVatChat";
            comboBoxTenVatChat.Size = new Size(177, 29);
            comboBoxTenVatChat.TabIndex = 52;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(comboBoxTinhTrang);
            groupBox3.Location = new Point(631, 142);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 74);
            groupBox3.TabIndex = 63;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tình Trạng";
            // 
            // comboBoxTinhTrang
            // 
            comboBoxTinhTrang.Font = new Font("Segoe UI", 12F);
            comboBoxTinhTrang.FormattingEnabled = true;
            comboBoxTinhTrang.Location = new Point(6, 30);
            comboBoxTinhTrang.Margin = new Padding(3, 2, 3, 2);
            comboBoxTinhTrang.Name = "comboBoxTinhTrang";
            comboBoxTinhTrang.Size = new Size(177, 29);
            comboBoxTinhTrang.TabIndex = 52;
            // 
            // buttonDatLai
            // 
            buttonDatLai.Location = new Point(1205, 89);
            buttonDatLai.Name = "buttonDatLai";
            buttonDatLai.Size = new Size(75, 102);
            buttonDatLai.TabIndex = 64;
            buttonDatLai.Text = "Đặt lại ";
            buttonDatLai.UseVisualStyleBackColor = true;
            buttonDatLai.Click += buttonDatLai_Click;
            // 
            // ThongKeVatChat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 594);
            Controls.Add(buttonDatLai);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(buttonTimKiem);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(groupBoxThongTin);
            Name = "ThongKeVatChat";
            Text = "ThongKeVatChat";
            Load += ThongKeVatChat_Load;
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxThongTin;
        private ComboBox comboBoxLoaiTang;
        private Label labelLoaiPhong;
        private ComboBox comboBoxPhong;
        private ComboBox comboBoxTang;
        private Label labelTenPhong;
        private Label label10;
        private DataGridView dataGridViewThongTin;
        private GroupBox groupBox1;
        private TextBox textBoxDemHuHong;
        private TextBox textBoxDemConTot;
        private TextBox textBoxDemCSVC;
        private Label label1;
        private Button buttonTimKiem;
        private GroupBox groupBox2;
        private ComboBox comboBoxTenVatChat;
        private GroupBox groupBox3;
        private ComboBox comboBoxTinhTrang;
        private Button buttonDatLai;
    }
}