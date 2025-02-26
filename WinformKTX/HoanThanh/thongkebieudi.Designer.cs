namespace WinformKTX.HoanThanh
{
    partial class thongkebieudi
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            dataGridViewThongTin2 = new DataGridView();
            label2 = new Label();
            chartTien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartGiuong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dataGridViewThongTin = new DataGridView();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartTien).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartGiuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.12531F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 645F));
            tableLayoutPanel1.Controls.Add(dataGridViewThongTin2, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(chartTien, 1, 0);
            tableLayoutPanel1.Controls.Add(chartGiuong, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridViewThongTin, 1, 2);
            tableLayoutPanel1.Controls.Add(label1, 1, 1);
            tableLayoutPanel1.Location = new Point(-8, -1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 93.9890747F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.010929F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.Size = new Size(1301, 607);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridViewThongTin2
            // 
            dataGridViewThongTin2.AllowUserToOrderColumns = true;
            dataGridViewThongTin2.Anchor = AnchorStyles.Bottom;
            dataGridViewThongTin2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThongTin2.BackgroundColor = Color.FromArgb(250, 255, 255);
            dataGridViewThongTin2.BorderStyle = BorderStyle.None;
            dataGridViewThongTin2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewThongTin2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin2.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewThongTin2.Location = new Point(3, 408);
            dataGridViewThongTin2.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin2.Name = "dataGridViewThongTin2";
            dataGridViewThongTin2.ReadOnly = true;
            dataGridViewThongTin2.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewThongTin2.RowHeadersWidth = 51;
            dataGridViewThongTin2.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewThongTin2.Size = new Size(650, 197);
            dataGridViewThongTin2.TabIndex = 53;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(171, 382);
            label2.Name = "label2";
            label2.Size = new Size(313, 21);
            label2.TabIndex = 52;
            label2.Text = "Số Lượng Giường Sử dụng ở mỗi phòng";
            // 
            // chartTien
            // 
            chartArea1.Name = "ChartArea1";
            chartTien.ChartAreas.Add(chartArea1);
            chartTien.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chartTien.Legends.Add(legend1);
            chartTien.Location = new Point(659, 3);
            chartTien.Name = "chartTien";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartTien.Series.Add(series1);
            chartTien.Size = new Size(639, 376);
            chartTien.TabIndex = 1;
            chartTien.Text = "chart1";
            // 
            // chartGiuong
            // 
            chartArea2.Name = "ChartArea1";
            chartGiuong.ChartAreas.Add(chartArea2);
            chartGiuong.Dock = DockStyle.Fill;
            legend2.Name = "Legend1";
            chartGiuong.Legends.Add(legend2);
            chartGiuong.Location = new Point(3, 3);
            chartGiuong.Name = "chartGiuong";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.IsXValueIndexed = true;
            series2.Label = "#VALX: #PERCENT";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartGiuong.Series.Add(series2);
            chartGiuong.Size = new Size(650, 376);
            chartGiuong.TabIndex = 0;
            chartGiuong.Text = "chart1";
            // 
            // dataGridViewThongTin
            // 
            dataGridViewThongTin.AllowUserToOrderColumns = true;
            dataGridViewThongTin.Anchor = AnchorStyles.Bottom;
            dataGridViewThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThongTin.BackgroundColor = Color.FromArgb(250, 255, 255);
            dataGridViewThongTin.BorderStyle = BorderStyle.None;
            dataGridViewThongTin.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewThongTin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewThongTin.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewThongTin.Location = new Point(659, 408);
            dataGridViewThongTin.Margin = new Padding(3, 2, 3, 2);
            dataGridViewThongTin.Name = "dataGridViewThongTin";
            dataGridViewThongTin.ReadOnly = true;
            dataGridViewThongTin.RowHeadersWidth = 51;
            dataGridViewThongTin.ScrollBars = ScrollBars.Vertical;
            dataGridViewThongTin.Size = new Size(639, 197);
            dataGridViewThongTin.TabIndex = 50;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(864, 382);
            label1.Name = "label1";
            label1.Size = new Size(228, 21);
            label1.TabIndex = 51;
            label1.Text = "Top Phòng Có Tiền Cao Nhất";
            // 
            // thongkebieudi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 604);
            Controls.Add(tableLayoutPanel1);
            Name = "thongkebieudi";
            Text = "thongkebieudi";
            Load += thongkebieudi_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin2).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartTien).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartGiuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThongTin).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTien;
        private DataGridView dataGridViewThongTin;
        private Label label1;
        private Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGiuong;
        private DataGridView dataGridViewThongTin2;
    }
}