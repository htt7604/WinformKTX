using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinformKTX.HoanThanh
{
    public partial class thongkebieudi : Form
    {
        public thongkebieudi()
        {
            InitializeComponent();
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        private void BieudoTronGiuong()
        {
            // Chuỗi kết nối
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn dữ liệu
            string query = "SELECT COUNT(CASE WHEN GIUONG.TINH_TRANG_GIUONG = N'Trống' THEN 1 END) AS Controng, " +
                           "COUNT(CASE WHEN GIUONG.TINH_TRANG_GIUONG = N'Đang sử dụng' THEN 1 END) AS Dangsudung " +
                           "FROM GIUONG";

            // Khai báo biến
            int ConTrong = 0;
            int Dangsudung = 0;

            // Tính toán tỷ lệ phần trăm
            int tong = ConTrong + Dangsudung;
            double tiletrong = 0;
            double tinlesudung = 0;

            // Lấy dữ liệu từ CSDL
            using (var cmd = new SqlCommand(query, conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Lấy số giường còn trống và giường đang sử dụng từ CSDL
                    ConTrong = reader.GetInt32(reader.GetOrdinal("Controng"));
                    Dangsudung = reader.GetInt32(reader.GetOrdinal("Dangsudung"));
                }
            }

            // Tính lại tổng số giường và tỷ lệ phần trăm
            tong = ConTrong + Dangsudung;
            if (tong > 0)  // Đảm bảo không chia cho 0
            {
                tiletrong = (double)ConTrong / tong * 100;
                tinlesudung = (double)Dangsudung / tong * 100;
            }

            // Cấu hình biểu đồ
            chartGiuong.Series.Clear();  // Xóa các Series hiện tại

            // Thêm Series vào biểu đồ
            Series series = new Series();
            series.Name = "PieChartSeries";
            series.ChartType = SeriesChartType.Pie; // Biểu đồ hình tròn (Pie)

            // Thêm dữ liệu vào Series
            series.Points.AddXY("Trống", tiletrong);  // Tỷ lệ giường còn trống
            series.Points.AddXY("Đang sử dụng", tinlesudung);  // Tỷ lệ giường đang sử dụng
            series.Label = "#VALX: #PERCENT";

            // Tùy chỉnh màu sắc cho từng phần trong biểu đồ
            series.Points[0].Color = System.Drawing.Color.BlueViolet;  // Màu cho phần giường còn trống
            series.Points[1].Color = System.Drawing.Color.Gray;    // Màu cho phần giường đang sử dụng

            // Hiển thị tỷ lệ phần trăm trên các phần
            series["PieLabelStyle"] = "Outside";  // Đặt nhãn ra ngoài
            series["PieLineColor"] = "Black";    // Màu đường viền
            series["Label"] = "#PERCENT{P0}";    // Hiển thị phần trăm

            // Thêm Series vào biểu đồ
            chartGiuong.Series.Add(series);

            // Thêm Tiêu đề cho biểu đồ
            chartGiuong.Titles.Clear();
            chartGiuong.Titles.Add("Tỷ lệ giường trong KTX");
            chartGiuong.Titles[0].Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            chartGiuong.Titles[0].ForeColor = System.Drawing.Color.Blue;

            // Tùy chỉnh Legend (Chú giải)
            chartGiuong.Legends.Clear();
            chartGiuong.Legends.Add(new Legend());
            chartGiuong.Legends[0].Position = new ElementPosition(70, 0, 30, 10); // Vị trí chú giải
            chartGiuong.Legends[0].Docking = Docking.Bottom;
            chartGiuong.Legends[0].Alignment = StringAlignment.Center;
        }

        private void BieudoCotDien_Nuoc()
        {
            // Chuỗi kết nối cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn dữ liệu (tổng tiền điện và nước theo tháng)
            string query = "SELECT YEAR(DIEN_NUOC.TU_NGAY) AS Year, " +
                "MONTH(DIEN_NUOC.TU_NGAY) AS Month, " +
                "CAST(SUM(DIEN_NUOC.TIEN_DIEN) AS DECIMAL(18, 2)) AS TongTienDien, " +
                "CAST(SUM(DIEN_NUOC.TIEN_NUOC) AS DECIMAL(18, 2)) AS TongTienNuoc " +
                "FROM DIEN_NUOC " +
                "GROUP BY YEAR(DIEN_NUOC.TU_NGAY), MONTH(DIEN_NUOC.TU_NGAY) " +
                "ORDER BY YEAR(DIEN_NUOC.TU_NGAY), MONTH(DIEN_NUOC.TU_NGAY)";
            // Lấy dữ liệu từ CSDL và thêm vào biểu đồ
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Xóa các series hiện tại của biểu đồ
                chartTien.Series.Clear();

                // Thêm series cho tiền điện
                Series seriesDien = new Series
                {
                    Name = "Tiền Điện",
                    ChartType = SeriesChartType.Column,  // Biểu đồ cột
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,  // Hiển thị giá trị trên các cột
                    Color = System.Drawing.Color.Blue  // Màu cho tiền điện
                };

                // Thêm series cho tiền nước
                Series seriesNuoc = new Series
                {
                    Name = "Tiền Nước",
                    ChartType = SeriesChartType.Column,  // Biểu đồ cột
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,  // Hiển thị giá trị trên các cột
                    Color = System.Drawing.Color.Green  // Màu cho tiền nước
                };

                // Đọc dữ liệu và thêm vào biểu đồ
                while (reader.Read())
                {
                    int month = reader.GetInt32(reader.GetOrdinal("Month"));
                    decimal totalTienDien = reader.GetDecimal(reader.GetOrdinal("TongTienDien"));
                    decimal totalTienNuoc = reader.GetDecimal(reader.GetOrdinal("TongTienNuoc"));

                    // Thêm điểm vào Series (month là nhãn trên trục X, tổng tiền là giá trị trên trục Y)
                    seriesDien.Points.AddXY(month, totalTienDien);
                    seriesNuoc.Points.AddXY(month, totalTienNuoc);
                }

                // Thêm series vào biểu đồ
                chartTien.Series.Add(seriesDien);
                chartTien.Series.Add(seriesNuoc);

                // Thiết lập các thuộc tính cho trục X và Y
                chartTien.ChartAreas[0].AxisX.Title = "Tháng";
                chartTien.ChartAreas[0].AxisY.Title = "Tổng Tiền (VND)";
                chartTien.ChartAreas[0].AxisX.Interval = 1;  // Khoảng cách giữa các tháng trên trục X
                chartTien.ChartAreas[0].AxisY.LabelStyle.Format = "C0";  // Định dạng số tiền là dạng tiền tệ

                // Thêm tiêu đề cho biểu đồ
                chartTien.Titles.Clear();
                chartTien.Titles.Add("Tổng Tiền Điện và Nước Theo Tháng");
            }
        }
        private void thongketopphongdiennuoc()
        {
            // Chuỗi kết nối cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn SQL để lấy top phòng sử dụng nhiều điện hoặc nước nhất
            string query = @"
        SELECT TOP 10
            PHONG.TEN_PHONG,
            SUM(DIEN_NUOC.TIEN_DIEN) AS TongTienDien,
            SUM(DIEN_NUOC.TIEN_NUOC) AS TongTienNuoc
        FROM 
            PHONG
        JOIN 
            DIEN_NUOC ON PHONG.MA_PHONG = DIEN_NUOC.MA_PHONG
        GROUP BY 
            PHONG.TEN_PHONG
        ORDER BY 
            SUM(DIEN_NUOC.TIEN_DIEN) DESC, 
            SUM(DIEN_NUOC.TIEN_NUOC) DESC";

            // Lấy dữ liệu từ CSDL và hiển thị lên DataGridView
            using (var cmd = new SqlCommand(query, conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dt = new System.Data.DataTable();
                conn.Open();
                da.Fill(dt);
                conn.Close();

                // Gán dữ liệu vào DataGridView
                dataGridViewThongTin.DataSource = dt;
            }
            dataGridViewThongTin.AutoGenerateColumns = true;
            dataGridViewThongTin.ColumnHeadersVisible = true;
            dataGridViewThongTin.Columns[1].DefaultCellStyle.Format = "C0";  // Định dạng tiền tệ
            dataGridViewThongTin.Columns[2].DefaultCellStyle.Format = "C0";  // Định dạng tiền tệ
        }

        private void thongkesoluonggiuong()
        {
            // Kết nối đến cơ sở dữ liệu
            //string connString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để lấy số lượng giường trống và đang sử dụng theo phòng
            string query = "SELECT PHONG.TEN_PHONG, " +
       " COUNT(CASE WHEN TINH_TRANG_GIUONG = N'Trống' THEN 1 END) AS Controng," +
      " COUNT(CASE WHEN TINH_TRANG_GIUONG = N'Đang sử dụng' THEN 1 END) AS Dangsudung,PHONG.SO_GIUONG_TOI_DA  " +
        " FROM GIUONG join PHONG on GIUONG.MA_PHONG=PHONG.MA_PHONG " +
        " GROUP BY PHONG.TEN_PHONG,GIUONG.MA_PHONG,PHONG.SO_GIUONG_TOI_DA ";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Hiển thị kết quả vào DataGridView
            dataGridViewThongTin2.DataSource = dt;
        }
        private void thongkebieudi_Load(object sender, EventArgs e)
        {
            BieudoCotDien_Nuoc();
            BieudoTronGiuong();
            thongketopphongdiennuoc();
            thongkesoluonggiuong();
        }
            
    }
}
