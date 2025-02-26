namespace WinformKTX.Vi_Pham
{
    partial class xulyvipham
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
            dataGridViewThongTin = new DataGridView();
            labelViPham = new Label();
            buttonXuLy = new Button();
            label1 = new Label();
            groupBoxThongTin = new GroupBox();
            buttonHuyBo = new Button();
            buttonTimKiem = new Button();
            comboBoxSinhVien = new ComboBox();
            labelMSSV = new Label();
            buttonXem = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            groupBoxThongTin.SuspendLayout();
            SuspendLayout();
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
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewThongTin.Location = new Point(-9, 213);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewThongTin.Size = new Size(1324, 460);
            dataGridViewThongTin.TabIndex = 64;
            // 
            // labelViPham
            // 
            labelViPham.AutoSize = true;
            labelViPham.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            labelViPham.Location = new Point(534, 9);
            labelViPham.Name = "labelViPham";
            labelViPham.Size = new Size(208, 37);
            labelViPham.TabIndex = 66;
            labelViPham.Text = "Xử Lý Vi Phạm ";
            // 
            // buttonXuLy
            // 
            buttonXuLy.BackColor = Color.DarkViolet;
            buttonXuLy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonXuLy.ForeColor = SystemColors.ButtonHighlight;
            buttonXuLy.Location = new Point(654, 97);
            buttonXuLy.Name = "buttonXuLy";
            buttonXuLy.Size = new Size(126, 67);
            buttonXuLy.TabIndex = 75;
            buttonXuLy.Text = "Xác Nhận Xử Lý";
            buttonXuLy.UseVisualStyleBackColor = false;
            buttonXuLy.Click += buttonXuLy_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.ForeColor = Color.MediumBlue;
            label1.Location = new Point(12, 172);
            label1.Name = "label1";
            label1.Size = new Size(192, 28);
            label1.TabIndex = 76;
            label1.Text = "Thông Tin Vi Phạm";
            // 
            // groupBoxThongTin
            // 
            groupBoxThongTin.Controls.Add(buttonHuyBo);
            groupBoxThongTin.Controls.Add(buttonTimKiem);
            groupBoxThongTin.Controls.Add(comboBoxSinhVien);
            groupBoxThongTin.Controls.Add(labelMSSV);
            groupBoxThongTin.Font = new Font("Segoe UI", 10F);
            groupBoxThongTin.Location = new Point(17, 84);
            groupBoxThongTin.Margin = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Name = "groupBoxThongTin";
            groupBoxThongTin.Padding = new Padding(3, 2, 3, 2);
            groupBoxThongTin.Size = new Size(536, 82);
            groupBoxThongTin.TabIndex = 77;
            groupBoxThongTin.TabStop = false;
            groupBoxThongTin.Text = "Thông tin";
            // 
            // buttonHuyBo
            // 
            buttonHuyBo.BackColor = Color.Magenta;
            buttonHuyBo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonHuyBo.ForeColor = SystemColors.ButtonHighlight;
            buttonHuyBo.Location = new Point(412, 36);
            buttonHuyBo.Name = "buttonHuyBo";
            buttonHuyBo.Size = new Size(101, 26);
            buttonHuyBo.TabIndex = 74;
            buttonHuyBo.Text = "Tải tại";
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
            // labelMSSV
            // 
            labelMSSV.AutoSize = true;
            labelMSSV.Location = new Point(5, 37);
            labelMSSV.Name = "labelMSSV";
            labelMSSV.Size = new Size(105, 19);
            labelMSSV.TabIndex = 41;
            labelMSSV.Text = "MSSV (nội trú) :";
            labelMSSV.UseMnemonic = false;
            // 
            // buttonXem
            // 
            buttonXem.BackColor = Color.Honeydew;
            buttonXem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonXem.ForeColor = SystemColors.ActiveCaptionText;
            buttonXem.Location = new Point(1096, 97);
            buttonXem.Name = "buttonXem";
            buttonXem.Size = new Size(141, 64);
            buttonXem.TabIndex = 80;
            buttonXem.Text = "Xem Vi Phạm đã xử lý ";
            buttonXem.UseVisualStyleBackColor = false;
            buttonXem.Click += buttonXem_Click;
            // 
            // xulyvipham
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 672);
            Controls.Add(buttonXem);
            Controls.Add(groupBoxThongTin);
            Controls.Add(buttonXuLy);
            Controls.Add(label1);
            Controls.Add(labelViPham);
            Controls.Add(dataGridViewThongTin);
            Name = "xulyvipham";
            Text = "xulyvipham";
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            groupBoxThongTin.ResumeLayout(false);
            groupBoxThongTin.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridViewThongTin;
        private Label labelViPham;
        private Button buttonXuLy;
        private Label label1;
        private GroupBox groupBoxThongTin;
        private Button buttonHuyBo;
        private Button buttonTimKiem;
        private ComboBox comboBoxSinhVien;
        private Label labelMSSV;
        private Button buttonXem;
    }
}