using abc.HoanThanh.ThongKeViPham;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformKTX.Vi_Pham
{
    public partial class xulyvipham : Form
    {
        public xulyvipham()
        {
            InitializeComponent();
            LoadViPhamData();
            LoaddataMssv();
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        //private SqlConnection conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
        //load vi pham vao 
        private void LoadViPhamData()
        {
            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MA_VI_PHAM, MSSV, MUC_VI_PHAM.TEN_VI_PHAM, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, TRANG_THAI_XU_LY, GHI_CHU_VP,MUC_VI_PHAM.HINH_THUC_XU_LI " +
                                  "FROM VI_PHAM JOIN MUC_VI_PHAM ON VI_PHAM.MA_MUC_VP = MUC_VI_PHAM.MA_MUC_VP where TRANG_THAI_XU_LY = N'Chưa xử lý'";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGridView
                    dataGridViewThongTin.DataSource = dataTable;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
        private void LoaddataMssv()
        {
            try
            {
                SqlConnection conn = kn.GetConnection();
                string query = "SELECT MSSV FROM Vi_PHAM where TRANG_THAI_XU_LY =N'Chưa xử lý'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxSinhVien.DataSource = table;
                    comboBoxSinhVien.DisplayMember = "MSSV";
                    comboBoxSinhVien.ValueMember = "MSSV";

                    comboBoxSinhVien.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //dua thong tin vi pham can xoa
        private void LoadViPhamByMSSV(string mssv)
        {
            string query;
            if (!string.IsNullOrEmpty(mssv))
            {
                // Truy vấn dữ liệu vi phạm theo MSSV
                query = "SELECT MA_VI_PHAM, MSSV, MUC_VI_PHAM.TEN_VI_PHAM, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, TRANG_THAI_XU_LY, GHI_CHU_VP " +
                                      "FROM VI_PHAM JOIN MUC_VI_PHAM ON VI_PHAM.MA_MUC_VP = MUC_VI_PHAM.MA_MUC_VP" +
                                      " WHERE TRANG_THAI_XU_LY=N'Chưa xử lý' and  MSSV = @MSSV";
            }
            else
            {
                query = "SELECT MA_VI_PHAM, MSSV, MUC_VI_PHAM.TEN_VI_PHAM, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, TRANG_THAI_XU_LY, GHI_CHU_VP FROM VI_PHAM JOIN MUC_VI_PHAM ON VI_PHAM.MA_MUC_VP = MUC_VI_PHAM.MA_MUC_VP where TRANG_THAI_XU_LY=N'Chưa xử lý' ";
            }
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số MSSV
                        cmd.Parameters.AddWithValue("@MSSV", mssv);

                        // Sử dụng DataReader để lấy dữ liệu
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Tạo một DataTable để lưu kết quả
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Đưa dữ liệu vào DataGridView
                        dataGridViewThongTin.DataSource = dt;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            if (comboBoxSinhVien.SelectedValue != null)
            {
                LoadViPhamByMSSV(comboBoxSinhVien.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Vui Lòng chọn MSSV để tìm kiếm ");
            }
        }


        private void buttonHuyBo_Click(object sender, EventArgs e)
        {
            comboBoxSinhVien.SelectedIndex = -1;
            LoadViPhamData();
        }
        // Khai báo các biến toàn cục cần thiết
        private string initialMaViPham;
        private string MSSV;
        private DateTime ngayVipham;
        private DateTime ngayxuly;
        private string noidungvipham;

        // Hàm lưu trạng thái xử lý vi phạm
        private void SaveViPham()
        {
            if (dataGridViewThongTin.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewThongTin.SelectedRows[0];

                // Lưu giá trị ban đầu của MA_VI_PHAM
                initialMaViPham = row.Cells["MA_VI_PHAM"].Value.ToString();

                // Kiểm tra điều kiện để lưu vi phạm
                //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conn = kn.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        string query = "UPDATE VI_PHAM SET TRANG_THAI_XU_LY = @TrangThaiXuLy WHERE MA_VI_PHAM = @MaViPham";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaViPham", initialMaViPham);
                            cmd.Parameters.AddWithValue("@TrangThaiXuLy", "Đã xử lý");

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xử lý vi phạm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không thể xử lý vi phạm!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên để sửa");
            }
        }

        // Hàm lấy hình thức xử lý vi phạm từ MA_VI_PHAM
        public string GetHinhThucXuLybyMA_VI_PHAM(string MA_VI_PHAM)
        {
            string Hinh_thuc_xu_ly = " ";

            // Kiểm tra nếu MA_VI_PHAM không hợp lệ
            if (string.IsNullOrEmpty(MA_VI_PHAM))
            {
                Console.WriteLine("Mã vi phạm không hợp lệ.");
                return Hinh_thuc_xu_ly;
            }

            string query = "SELECT HINH_THUC_XU_LI FROM MUC_VI_PHAM " +
                           "JOIN VI_PHAM ON MUC_VI_PHAM.MA_MUC_VP = VI_PHAM.MA_MUC_VP " +
                           "WHERE VI_PHAM.MA_VI_PHAM = @MA_VI_PHAM";

            SqlConnection conn = kn.GetConnection(); // Đảm bảo rằng kết nối là hợp lệ
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@MA_VI_PHAM", MA_VI_PHAM);
                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        Hinh_thuc_xu_ly = result.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy hình thức xử lý.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return Hinh_thuc_xu_ly;
        }




        // Hàm đếm số lần vi phạm và hình thức xử lý
        public int DemKiemTraViPham(string mssv)
        {
            int result = 0;
            string query = "SELECT MSSV, " +
               "COUNT(CASE WHEN HINH_THUC_XU_LY = N'Cảnh cáo' THEN 1 END) AS SoLanCanhCao, " +
                " COUNT(CASE WHEN HINH_THUC_XU_LY = N'Hủy nội trú' THEN 1 END) AS SoLanHuyNoiTru, " +
                "COUNT(CASE WHEN HINH_THUC_XU_LY = N'Hủy gia hạn nội trú' THEN 1 END) AS SoLanHuyGiaHanNoiTru " +
                "FROM LICH_SU_VI_PHAM  where MSSV = @MSSV " +
                "GROUP BY MSSV ";
            try
            {
                SqlConnection conn = kn.GetConnection();
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MSSV", mssv);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Kiểm tra và lấy dữ liệu nếu có
                            int soLanCanhCao = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            int soLanhuynoitru = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            int soLanhuygianhannoitru = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                            // Kiểm tra số lần cảnh cáo và các hình thức xử lý
                            if (soLanCanhCao >= 2)
                            {
                                result = 1; // 2 lần cảnh cáo
                            }
                            else if (soLanhuynoitru > 0)
                            {
                                result = 1; // "Hủy nội trú"
                            }
                            else if (soLanhuygianhannoitru > 0)
                            {
                                result = 2; // "Hủy gia hạn nội trú"
                            }
                            else
                            {
                                result = 0; // Các trường hợp khác
                            }
                        }
                    }
                }
                conn.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Lỗi kết nối SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã có lỗi xảy ra: " + ex.Message);
            }
            return result; // Trả về kết quả
        }


        // Hàm lưu thông tin vi phạm vào lịch sử vi phạm
        private void Luuviphamvaolichsu(string Hinhthucxuly)
        {
            try
            {
                SqlConnection conn = kn.GetConnection();
                conn.Open();
                string query = "INSERT INTO LICH_SU_VI_PHAM(MSSV, NGAY_VI_PHAM, HINH_THUC_XU_LY, NGAY_XU_LY, NOI_DUNG_VI_PHAM) " +
                               "VALUES (@MSSV, @NgayViPham, @Hinhthucxuly, @Ngayxuly, @NoiDungViPham)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    DataGridViewRow row = dataGridViewThongTin.SelectedRows[0];
                    MSSV = row.Cells["MSSV"].Value.ToString();
                    cmd.Parameters.AddWithValue("@MSSV", MSSV);
                    ngayVipham = Convert.ToDateTime(row.Cells["NGAY_VI_PHAM"].Value);
                    cmd.Parameters.AddWithValue("@NgayViPham", ngayVipham);

                    cmd.Parameters.AddWithValue("@Hinhthucxuly", Hinhthucxuly);

                    ngayxuly = DateTime.Now;
                    cmd.Parameters.AddWithValue("@Ngayxuly", ngayxuly);

                    noidungvipham = row.Cells["NOI_DUNG_VI_PHAM"].Value.ToString();
                    cmd.Parameters.AddWithValue("@NoiDungViPham", noidungvipham);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã thêm vi phạm vào lịch sử!");
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm vi phạm!");
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Hàm xử lý khi nhấn nút xử lý vi phạm
        private void buttonXuLy_Click(object sender, EventArgs e)
        {
            if (dataGridViewThongTin.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewThongTin.SelectedRows[0];
                initialMaViPham = row.Cells["MA_VI_PHAM"].Value.ToString();
                string mssv = row.Cells["MSSV"].Value.ToString();
                int kt = DemKiemTraViPham(mssv);

                if (row.Cells["TRANG_THAI_XU_LY"].Value.ToString() != "Đã xử lý")
                {
                    SaveViPham();
                    string hinhThucXuLya = GetHinhThucXuLybyMA_VI_PHAM(initialMaViPham);

                    if (hinhThucXuLya == "Cảnh cáo" && kt == 0)
                    {
                        Luuviphamvaolichsu("Cảnh cáo");
                    }
                    else
                    {
                        if (hinhThucXuLya == "Hủy nội trú" || kt == 1)
                        {
                            Luuviphamvaolichsu("Cần chú ý");
                        }
                        else if (hinhThucXuLya == "Hủy gia hạn nội trú" || kt == 2)
                        {
                            Luuviphamvaolichsu("Hủy gia hạn nội trú");
                        }
                    }
                    LoadViPhamData();
                }
                else
                {
                    MessageBox.Show("Vi phạm đã được xử lý từ trước");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xử lý.");
            }
        }







        private void buttonXem_Click(object sender, EventArgs e)
        {
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MA_VI_PHAM, MSSV, MUC_VI_PHAM.TEN_VI_PHAM, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, TRANG_THAI_XU_LY, GHI_CHU_VP,MUC_VI_PHAM.HINH_THUC_XU_LI " +
                                   " FROM VI_PHAM JOIN MUC_VI_PHAM ON VI_PHAM.MA_MUC_VP = MUC_VI_PHAM.MA_MUC_VP where TRANG_THAI_XU_LY = N'Đã xử lý'";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGridView
                    dataGridViewThongTin.DataSource = dataTable;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
    }
}
