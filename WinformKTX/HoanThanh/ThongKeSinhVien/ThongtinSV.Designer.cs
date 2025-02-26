namespace abc.HoanThanh.ThongKeSinhVien
{
    partial class ThongtinSV
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
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            radioButtonHoTen = new RadioButton();
            radioButtonMSSV = new RadioButton();
            comboBoxHoTen = new ComboBox();
            comboBoxSinhVien = new ComboBox();
            labelHoTen = new Label();
            label = new Label();
            dataGridViewThongTin = new DataGridView();
            groupBox1 = new GroupBox();
            textBoxDemSVNu = new TextBox();
            textBoxDemSVNam = new TextBox();
            textBoxDemSV = new TextBox();
            label1 = new Label();
            buttonAll = new Button();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(comboBoxHoTen);
            groupBox2.Controls.Add(comboBoxSinhVien);
            groupBox2.Controls.Add(labelHoTen);
            groupBox2.Controls.Add(label);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(354, 134);
            groupBox2.TabIndex = 56;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông Tin";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButtonHoTen);
            groupBox3.Controls.Add(radioButtonMSSV);
            groupBox3.Location = new Point(257, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(97, 134);
            groupBox3.TabIndex = 60;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tìm Bằng";
            // 
            // radioButtonHoTen
            // 
            radioButtonHoTen.AutoSize = true;
            radioButtonHoTen.Location = new Point(6, 84);
            radioButtonHoTen.Name = "radioButtonHoTen";
            radioButtonHoTen.Size = new Size(44, 19);
            radioButtonHoTen.TabIndex = 1;
            radioButtonHoTen.TabStop = true;
            radioButtonHoTen.Text = "Tên";
            radioButtonHoTen.UseVisualStyleBackColor = true;
            radioButtonHoTen.CheckedChanged += radioButtonHoTen_CheckedChanged;
            // 
            // radioButtonMSSV
            // 
            radioButtonMSSV.AutoSize = true;
            radioButtonMSSV.Location = new Point(6, 37);
            radioButtonMSSV.Name = "radioButtonMSSV";
            radioButtonMSSV.Size = new Size(55, 19);
            radioButtonMSSV.TabIndex = 0;
            radioButtonMSSV.TabStop = true;
            radioButtonMSSV.Text = "MSSV";
            radioButtonMSSV.UseVisualStyleBackColor = true;
            radioButtonMSSV.CheckedChanged += radioButtonMSSV_CheckedChanged;
            // 
            // comboBoxHoTen
            // 
            comboBoxHoTen.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxHoTen.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxHoTen.FormattingEnabled = true;
            comboBoxHoTen.Location = new Point(104, 78);
            comboBoxHoTen.Name = "comboBoxHoTen";
            comboBoxHoTen.Size = new Size(133, 23);
            comboBoxHoTen.TabIndex = 60;
            comboBoxHoTen.SelectedIndexChanged += comboBoxHoTen_SelectedIndexChanged;
            // 
            // comboBoxSinhVien
            // 
            comboBoxSinhVien.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxSinhVien.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxSinhVien.FormattingEnabled = true;
            comboBoxSinhVien.Location = new Point(105, 36);
            comboBoxSinhVien.Name = "comboBoxSinhVien";
            comboBoxSinhVien.Size = new Size(133, 23);
            comboBoxSinhVien.TabIndex = 59;
            comboBoxSinhVien.SelectedIndexChanged += comboBoxMSSV_SelectedIndexChanged;
            // 
            // labelHoTen
            // 
            labelHoTen.AutoSize = true;
            labelHoTen.Location = new Point(6, 86);
            labelHoTen.Name = "labelHoTen";
            labelHoTen.Size = new Size(97, 15);
            labelHoTen.TabIndex = 46;
            labelHoTen.Text = "Họ Tên Sinh Viên";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(37, 39);
            label.Name = "label";
            label.Size = new Size(37, 15);
            label.TabIndex = 44;
            label.Text = "MSSV";
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
            dataGridViewThongTin.Location = new Point(-4, 151);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(1306, 447);
            dataGridViewThongTin.TabIndex = 57;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxDemSVNu);
            groupBox1.Controls.Add(textBoxDemSVNam);
            groupBox1.Controls.Add(textBoxDemSV);
            groupBox1.Location = new Point(978, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(219, 134);
            groupBox1.TabIndex = 58;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin thống kê";
            // 
            // textBoxDemSVNu
            // 
            textBoxDemSVNu.Location = new Point(6, 69);
            textBoxDemSVNu.Name = "textBoxDemSVNu";
            textBoxDemSVNu.ReadOnly = true;
            textBoxDemSVNu.Size = new Size(197, 23);
            textBoxDemSVNu.TabIndex = 3;
            textBoxDemSVNu.WordWrap = false;
            // 
            // textBoxDemSVNam
            // 
            textBoxDemSVNam.Location = new Point(6, 105);
            textBoxDemSVNam.Name = "textBoxDemSVNam";
            textBoxDemSVNam.ReadOnly = true;
            textBoxDemSVNam.Size = new Size(197, 23);
            textBoxDemSVNam.TabIndex = 2;
            textBoxDemSVNam.WordWrap = false;
            // 
            // textBoxDemSV
            // 
            textBoxDemSV.Location = new Point(6, 31);
            textBoxDemSV.Name = "textBoxDemSV";
            textBoxDemSV.ReadOnly = true;
            textBoxDemSV.Size = new Size(197, 23);
            textBoxDemSV.TabIndex = 1;
            textBoxDemSV.WordWrap = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            label1.Location = new Point(551, 43);
            label1.Name = "label1";
            label1.Size = new Size(323, 54);
            label1.TabIndex = 59;
            label1.Text = "Hồ Sơ Sinh Viên";
            // 
            // buttonAll
            // 
            buttonAll.Location = new Point(1205, 51);
            buttonAll.Name = "buttonAll";
            buttonAll.Size = new Size(75, 46);
            buttonAll.TabIndex = 60;
            buttonAll.Text = "Hủy ";
            buttonAll.UseVisualStyleBackColor = true;
            buttonAll.Click += buttonAll_Click;
            // 
            // ThongtinSV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 594);
            Controls.Add(buttonAll);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(dataGridViewThongTin);
            Controls.Add(groupBox2);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ThongtinSV";
            Text = "ThongtinSV";
            Load += ThongtinSV_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private Label labelHoTen;
        private Label label;
        private DataGridView dataGridViewThongTin;
        private GroupBox groupBox1;
        private ComboBox comboBoxSinhVien;
        private ComboBox comboBoxHoTen;
        private TextBox textBoxDemSV;
        private TextBox textBoxDemSVNu;
        private TextBox textBoxDemSVNam;
        private Label label1;
        private GroupBox groupBox3;
        private RadioButton radioButtonHoTen;
        private RadioButton radioButtonMSSV;
        private Button buttonAll;
    }
}