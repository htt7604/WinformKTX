


//using Microsoft.Data.SqlClient;
//using System;
//using System.Data;
//using System.Windows.Forms;

//namespace WinformKTX
//{
//    public partial class GiaHanNoiTru : Form
//    {
//        public GiaHanNoiTru()
//        {
//            InitializeComponent();
//            txtThoigiangiahan.SelectedIndexChanged += TxtThoigiangiahan_SelectedIndexChanged; // Đăng ký sự kiện SelectedIndexChanged
//        }

//        KetnoiCSDL kn = new KetnoiCSDL();

//        private void btnTracuunoitru_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
//            {
//                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                try
//                {
//                    conn.Open();
//                    string query = @"
//                        SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, 
//                               NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
//                        FROM NOI_TRU 
//                        JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG 
//                        JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
//                        WHERE NOI_TRU.MSSV = @MaSV";

//                    SqlCommand cmd = new SqlCommand(query, conn);
//                    cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

//                    SqlDataAdapter da = new SqlDataAdapter(cmd);
//                    DataTable dt = new DataTable();
//                    da.Fill(dt);

//                    if (dt.Rows.Count == 0)
//                    {
//                        MessageBox.Show("Không tìm thấy thông tin nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    else
//                    {
//                        string trangThai = dt.Rows[0]["TRANG_THAI_NOI_TRU"].ToString();
//                        if (trangThai != "Đang nội trú")
//                        {
//                            MessageBox.Show("Sinh viên chưa nội trú nên không thể gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        }

//                        dataGridView1.DataSource = dt;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void TxtThoigiangiahan_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
//            {
//                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                try
//                {
//                    conn.Open();

//                    string getGiaPhongQuery = @"
//                        SELECT GIA_PHONG 
//                        FROM PHONG 
//                        JOIN LOAI_PHONG ON PHONG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG 
//                        WHERE PHONG.MA_PHONG = (SELECT MA_PHONG FROM NOI_TRU WHERE MSSV = @MaSV)";

//                    SqlCommand cmd = new SqlCommand(getGiaPhongQuery, conn);
//                    cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
//                    object giaPhong = cmd.ExecuteScalar();

//                    if (giaPhong != null && giaPhong != DBNull.Value)
//                    {
//                        txtTienphong.Text = giaPhong.ToString();
//                    }
//                    else
//                    {
//                        MessageBox.Show("Không tìm thấy giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnDangkygiahan_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
//            {
//                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                try
//                {
//                    conn.Open();

//                    // Kiểm tra trạng thái nội trú và ngày kết thúc
//                    string checkStatusQuery = @"
//                        SELECT NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU 
//                        FROM NOI_TRU WHERE MSSV = @MaSV";
//                    SqlCommand cmdCheckStatus = new SqlCommand(checkStatusQuery, conn);
//                    cmdCheckStatus.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
//                    SqlDataReader reader = cmdCheckStatus.ExecuteReader();

//                    if (!reader.Read())
//                    {
//                        MessageBox.Show("Không tìm thấy thông tin nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }

//                    DateTime ngayKetThucHienTai = Convert.ToDateTime(reader["NGAY_KET_THUC_NOI_TRU"]);
//                    string trangThaiNoiTru = reader["TRANG_THAI_NOI_TRU"].ToString();
//                    reader.Close();

//                    if (trangThaiNoiTru != "Đang nội trú")
//                    {
//                        MessageBox.Show("Sinh viên chưa nội trú nên không thể gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    if (ngayKetThucHienTai > DateTime.Now)
//                    {
//                        MessageBox.Show("Sinh viên chỉ có thể gia hạn khi đã hết hạn nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    // Kiểm tra số lần vi phạm trong vòng 1 năm
//                    string checkViolationsQuery = @"
//                        SELECT COUNT(*) 
//                        FROM VI_PHAM 
//                        WHERE MSSV = @MaSV AND DATEDIFF(YEAR, NGAY_VI_PHAM, GETDATE()) = 0";
//                    SqlCommand cmdCheckViolations = new SqlCommand(checkViolationsQuery, conn);
//                    cmdCheckViolations.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
//                    int soLanViPham = (int)cmdCheckViolations.ExecuteScalar();

//                    if (soLanViPham > 3)
//                    {
//                        MessageBox.Show("Sinh viên đã vi phạm quá 3 lần trong năm nay nên không thể gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    // Cập nhật thông tin gia hạn
//                    DateTime ngayBatDauMoi = ngayKetThucHienTai;
//                    DateTime ngayKetThucMoi = ngayKetThucHienTai.AddMonths(3);

//                    string updateQuery = @"
//                        UPDATE NOI_TRU 
//                        SET NGAY_BAT_DAU_NOI_TRU = @NgayBatDauMoi, 
//                            NGAY_KET_THUC_NOI_TRU = @NgayKetThucMoi,
//                            TRANG_THAI_NOI_TRU = N'Chờ gia hạn'
//                        WHERE MSSV = @MaSV";

//                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
//                    cmdUpdate.Parameters.AddWithValue("@NgayBatDauMoi", ngayBatDauMoi);
//                    cmdUpdate.Parameters.AddWithValue("@NgayKetThucMoi", ngayKetThucMoi);
//                    cmdUpdate.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

//                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
//                    if (rowsAffected > 0)
//                    {
//                        // Cập nhật trạng thái thanh toán
//                        string updateThanhToanQuery = @"
//                            UPDATE THANH_TOAN_PHONG
//                            SET TRANG_THAI_THANH_TOAN = N'Chưa thanh toán'
//                            WHERE MA_NOI_TRU IN (SELECT MA_NOI_TRU FROM NOI_TRU WHERE MSSV = @MaSV)";

//                        SqlCommand cmdUpdateThanhToan = new SqlCommand(updateThanhToanQuery, conn);
//                        cmdUpdateThanhToan.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
//                        cmdUpdateThanhToan.ExecuteNonQuery();

//                        MessageBox.Show($"Gia hạn thành công!\nNgày mới bắt đầu: {ngayBatDauMoi:dd/MM/yyyy}\nNgày hết hạn mới: {ngayKetThucMoi:dd/MM/yyyy}",
//                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                        LoadData();
//                    }
//                    else
//                    {
//                        MessageBox.Show("Không thể gia hạn nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void LoadData()
//        {
//            btnTracuunoitru_Click(null, null);
//        }
//    }
//}



using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WinformKTX
{
    public partial class GiaHanNoiTru : Form
    {
        public GiaHanNoiTru()
        {
            InitializeComponent();
            txtThoigiangiahan.SelectedIndexChanged += TxtThoigiangiahan_SelectedIndexChanged;
        }

        KetnoiCSDL kn = new KetnoiCSDL();

        //private void btnTracuunoitru_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
        //    {
        //        MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    using (SqlConnection conn = kn.GetConnection())
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string query = @"
        //                SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, 
        //                       NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
        //                FROM NOI_TRU 
        //                JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG 
        //                JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
        //                WHERE NOI_TRU.MSSV = @MaSV";

        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            if (dt.Rows.Count == 0)
        //            {
        //                MessageBox.Show("Không tìm thấy thông tin nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                string trangThai = dt.Rows[0]["TRANG_THAI_NOI_TRU"].ToString();
        //                if (trangThai != "Chờ gia hạn")
        //                {
        //                    MessageBox.Show("Sinh viên chưa nội trú nên không thể gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }

        //                dataGridView1.DataSource = dt;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void btnTracuunoitru_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Truy vấn trạng thái nội trú trước khi lấy dữ liệu đầy đủ
                    string checkStatusQuery = @"
                SELECT TRANG_THAI_NOI_TRU 
                FROM NOI_TRU 
                WHERE MSSV = @MaSV";

                    SqlCommand checkStatusCmd = new SqlCommand(checkStatusQuery, conn);
                    checkStatusCmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

                    object trangThaiObj = checkStatusCmd.ExecuteScalar();
                    if (trangThaiObj == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nội trú của sinh viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // Xóa dữ liệu cũ nếu có
                        return;
                    }

                    string trangThai = trangThaiObj.ToString();
                    if (trangThai != "Chờ gia hạn")
                    {
                        MessageBox.Show("Sinh viên chưa đến hạn gia hạn nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // Không hiển thị dữ liệu nếu không hợp lệ
                        return;
                    }

                    // Nếu trạng thái hợp lệ, truy vấn lấy thông tin đầy đủ để hiển thị
                    string query = @"
                SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, 
                       NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
                FROM NOI_TRU 
                JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG 
                JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                WHERE NOI_TRU.MSSV = @MaSV";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtThoigiangiahan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    string getGiaPhongQuery = @"
                                SELECT GIA_PHONG 
                                FROM PHONG 
                                JOIN LOAI_PHONG ON PHONG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG 
                                WHERE PHONG.MA_PHONG = (SELECT MA_PHONG FROM NOI_TRU WHERE MSSV = @MaSV)";

                    SqlCommand cmd = new SqlCommand(getGiaPhongQuery, conn);
                    cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
                    object giaPhong = cmd.ExecuteScalar();

                    if (giaPhong != null && giaPhong != DBNull.Value)
                    {
                        txtTienphong.Text = giaPhong.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //private void btnDangkygiahan_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
        //    {
        //        MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    using (SqlConnection conn = kn.GetConnection())
        //    {
        //        try
        //        {
        //            conn.Open();

        //            // Kiểm tra trạng thái nội trú và ngày kết thúc
        //            string checkStatusQuery = @"
        //        SELECT NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU 
        //        FROM NOI_TRU WHERE MSSV = @MaSV";
        //            SqlCommand cmdCheckStatus = new SqlCommand(checkStatusQuery, conn);
        //            cmdCheckStatus.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
        //            SqlDataReader reader = cmdCheckStatus.ExecuteReader();

        //            if (!reader.Read())
        //            {
        //                MessageBox.Show("Không tìm thấy thông tin nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }

        //            DateTime ngayKetThucHienTai = Convert.ToDateTime(reader["NGAY_KET_THUC_NOI_TRU"]);
        //            string trangThaiNoiTru = reader["TRANG_THAI_NOI_TRU"].ToString();
        //            reader.Close();

        //            if (ngayKetThucHienTai > DateTime.Now)
        //            {
        //                MessageBox.Show("Sinh viên chỉ có thể gia hạn khi đã hết hạn nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            // Nếu trạng thái là "Chờ gia hạn" hoặc "Đang nội trú" thì tiếp tục xử lý
        //            if (trangThaiNoiTru != "Đang nội trú" && trangThaiNoiTru != "Chờ gia hạn")
        //            {
        //                MessageBox.Show("Sinh viên không ở trạng thái hợp lệ để gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            // Kiểm tra số lần vi phạm trong vòng 1 năm
        //            string checkViolationsQuery = @"
        //        SELECT COUNT(*) 
        //        FROM VI_PHAM 
        //        WHERE MSSV = @MaSV AND DATEDIFF(YEAR, NGAY_VI_PHAM, GETDATE()) = 0";
        //            SqlCommand cmdCheckViolations = new SqlCommand(checkViolationsQuery, conn);
        //            cmdCheckViolations.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
        //            int soLanViPham = (int)cmdCheckViolations.ExecuteScalar();

        //            if (soLanViPham > 3)
        //            {
        //                MessageBox.Show("Sinh viên đã vi phạm quá 3 lần trong năm nay nên không thể gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            // Cập nhật thông tin gia hạn
        //            DateTime ngayBatDauMoi = ngayKetThucHienTai;
        //            DateTime ngayKetThucMoi = ngayKetThucHienTai.AddMonths(3);

        //            string updateQuery = @"
        //        UPDATE NOI_TRU 
        //        SET NGAY_BAT_DAU_NOI_TRU = @NgayBatDauMoi, 
        //            NGAY_KET_THUC_NOI_TRU = @NgayKetThucMoi,
        //            TRANG_THAI_NOI_TRU = N'Đang nội trú'
        //        WHERE MSSV = @MaSV";

        //            SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
        //            cmdUpdate.Parameters.AddWithValue("@NgayBatDauMoi", ngayBatDauMoi);
        //            cmdUpdate.Parameters.AddWithValue("@NgayKetThucMoi", ngayKetThucMoi);
        //            cmdUpdate.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

        //            int rowsAffected = cmdUpdate.ExecuteNonQuery();
        //            if (rowsAffected > 0)
        //            {
        //                // Lấy mã nội trú để tạo mã thanh toán mới
        //                string getMaNoiTruQuery = "SELECT MA_NOI_TRU FROM NOI_TRU WHERE MSSV = @MaSV";
        //                SqlCommand cmdGetMaNoiTru = new SqlCommand(getMaNoiTruQuery, conn);
        //                cmdGetMaNoiTru.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
        //                object maNoiTru = cmdGetMaNoiTru.ExecuteScalar();

        //                if (maNoiTru != null)
        //                {
        //                    // Thêm một bản ghi mới vào THANH_TOAN_PHONG
        //                    string insertThanhToanQuery = @"
        //                INSERT INTO THANH_TOAN_PHONG (MA_NOI_TRU, TRANG_THAI_THANH_TOAN)
        //                VALUES (@MaNoiTru, N'Chưa thanh toán')";

        //                    SqlCommand cmdInsertThanhToan = new SqlCommand(insertThanhToanQuery, conn);
        //                    cmdInsertThanhToan.Parameters.AddWithValue("@MaNoiTru", maNoiTru);
        //                    cmdInsertThanhToan.ExecuteNonQuery();
        //                }

        //                MessageBox.Show($"Gia hạn thành công!\nNgày mới bắt đầu: {ngayBatDauMoi:dd/MM/yyyy}\nNgày hết hạn mới: {ngayKetThucMoi:dd/MM/yyyy}",
        //                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                LoadData();
        //                //txtMasinhvien.Text = "";
        //                //txtTienphong.Text = "";
        //                //txtThoigiangiahan.SelectedIndex = -1;
        //            }
        //            else
        //            {
        //                MessageBox.Show("Không thể gia hạn nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
        private void btnDangkygiahan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Kiểm tra trạng thái nội trú và ngày kết thúc
                    string checkStatusQuery = @"
            SELECT MA_PHONG, MA_GIUONG, NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU 
            FROM NOI_TRU WHERE MSSV = @MaSV ORDER BY NGAY_KET_THUC_NOI_TRU DESC";
                    SqlCommand cmdCheckStatus = new SqlCommand(checkStatusQuery, conn);
                    cmdCheckStatus.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
                    SqlDataReader reader = cmdCheckStatus.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("Không tìm thấy thông tin nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int maPhong = Convert.ToInt32(reader["MA_PHONG"]);
                    int maGiuong = Convert.ToInt32(reader["MA_GIUONG"]);
                    DateTime ngayKetThucHienTai = Convert.ToDateTime(reader["NGAY_KET_THUC_NOI_TRU"]);
                    string trangThaiNoiTru = reader["TRANG_THAI_NOI_TRU"].ToString();
                    reader.Close();

                    if (ngayKetThucHienTai > DateTime.Now)
                    {
                        MessageBox.Show("Sinh viên chỉ có thể gia hạn khi đã hết hạn nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (trangThaiNoiTru != "Đang nội trú" && trangThaiNoiTru != "Chờ gia hạn")
                    {
                        MessageBox.Show("Sinh viên không ở trạng thái hợp lệ để gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra số lần vi phạm trong vòng 1 năm
                    string checkViolationsQuery = @"
            SELECT COUNT(*) FROM VI_PHAM 
            WHERE MSSV = @MaSV AND DATEDIFF(YEAR, NGAY_VI_PHAM, GETDATE()) = 0";
                    SqlCommand cmdCheckViolations = new SqlCommand(checkViolationsQuery, conn);
                    cmdCheckViolations.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
                    int soLanViPham = (int)cmdCheckViolations.ExecuteScalar();

                    if (soLanViPham > 3)
                    {
                        MessageBox.Show("Sinh viên đã vi phạm quá 3 lần trong năm nay nên không thể gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tạo mã nội trú mới
                    DateTime ngayBatDauMoi = DateTime.Now;
                    DateTime ngayKetThucMoi = ngayBatDauMoi.AddMonths(3);

                    string insertQuery = @"
            INSERT INTO NOI_TRU (MSSV, MA_PHONG, MA_GIUONG, NGAY_BAT_DAU_NOI_TRU, NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU, NGAY_DANG_KY_NOI_TRU)
            VALUES (@MaSV, @MaPhong, @MaGiuong, @NgayBatDauMoi, @NgayKetThucMoi, N'Đang nội trú', GETDATE());
            SELECT SCOPE_IDENTITY();"; // Lấy ID mới vừa tạo

                    SqlCommand cmdInsert = new SqlCommand(insertQuery, conn);
                    cmdInsert.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
                    cmdInsert.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmdInsert.Parameters.AddWithValue("@MaGiuong", maGiuong);
                    cmdInsert.Parameters.AddWithValue("@NgayBatDauMoi", ngayBatDauMoi);
                    cmdInsert.Parameters.AddWithValue("@NgayKetThucMoi", ngayKetThucMoi);

                    object maNoiTruMoi = cmdInsert.ExecuteScalar();

                    if (maNoiTruMoi != null)
                    {
                        // Thêm một bản ghi mới vào THANH_TOAN_PHONG
                        string insertThanhToanQuery = @"
                INSERT INTO THANH_TOAN_PHONG (MA_NOI_TRU, TRANG_THAI_THANH_TOAN)
                VALUES (@MaNoiTru, N'Chưa thanh toán')";

                        SqlCommand cmdInsertThanhToan = new SqlCommand(insertThanhToanQuery, conn);
                        cmdInsertThanhToan.Parameters.AddWithValue("@MaNoiTru", maNoiTruMoi);
                        cmdInsertThanhToan.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Gia hạn thành công!\nNgày mới bắt đầu: {ngayBatDauMoi:dd/MM/yyyy}\nNgày hết hạn mới: {ngayKetThucMoi:dd/MM/yyyy}",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadData()
        {
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, 
                       NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
                FROM NOI_TRU 
                JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG 
                JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                WHERE NOI_TRU.MSSV = @MaSV";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nội trú của sinh viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // Xóa dữ liệu cũ nếu có
                    }
                    else
                    {
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
