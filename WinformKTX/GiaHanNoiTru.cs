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
            guna2Button1Doiphong.Click += guna2Button1Doiphong_Click; // Thêm sự kiện Click

        }

        KetnoiCSDL kn = new KetnoiCSDL();

        // 1. Tra cứu thông tin nội trú
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
        //                SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
        //                FROM NOI_TRU JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
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
        //                if (trangThai == "Đã nội trú")
        //                {
        //                    dataGridView1.DataSource = dt;
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Sinh viên chưa nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }

        //                // Đổi tên cột hiển thị trên DataGridView////


        //                dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
        //                dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ Và Tên";
        //                dataGridView1.Columns["MA_PHONG"].HeaderText = "Mã Phòng";
        //                dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
        //                dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
        //                dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
        //                dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";


        //                //dataGridView1Dien_nuoc.Columns["CHI_SO_NUOC_CU"].HeaderText = "Chí Số Nước Cũ";
        //                //dataGridView1Dien_nuoc.Columns["CHI_SO_NUOC_MOI"].HeaderText = "Chỉ Số Nước Mới";
        //                //dataGridView1Dien_nuoc.Columns["SO_NUOC_DA_SU_DUNG"].HeaderText = "Số Nước Đã Sử Dụng";
        //                //dataGridView1Dien_nuoc.Columns["TIEN_DIEN"].HeaderText = "Tiền Điện";
        //                //dataGridView1Dien_nuoc.Columns["TIEN_NUOC"].HeaderText = "Tiền Nước";
        //                //dataGridView1Dien_nuoc.Columns["TONG_TIEN"].HeaderText = "Tổng Tiền";
        //                //dataGridView1Dien_nuoc.Columns["NGAY_THANH_TOAN_DIEN_NUOC"].HeaderText = "Ngày Thanh Toán Điện - Nước";
        //                //dataGridView1Dien_nuoc.Columns["TINH_TRANG_TT"].HeaderText = "Tình Trạng Thanh Toán";
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
                    string query = @"
                SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
                FROM NOI_TRU JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                WHERE NOI_TRU.MSSV = @MaSV";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string trangThai = dt.Rows[0]["TRANG_THAI_NOI_TRU"].ToString();
                        if (trangThai != "Đã nội trú")
                        {
                            MessageBox.Show("Sinh viên chưa nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dataGridView1.DataSource = dt;

                        // Đổi tên cột hiển thị trên DataGridView
                        dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                        dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ Và Tên";
                        dataGridView1.Columns["MA_PHONG"].HeaderText = "Mã Phòng";
                        dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                        dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                        dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                        dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        // 2. Hiển thị giá phòng khi chọn thời gian gia hạn
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
                        WHERE PHONG.MA_PHONG = ( SELECT MA_PHONG FROM NOI_TRU WHERE MSSV = @MaSV )";

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

        // 3. Đăng ký gia hạn
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

                    // Lấy ngày kết thúc nội trú hiện tại
                    string getDateQuery = "SELECT NGAY_KET_THUC_NOI_TRU FROM NOI_TRU WHERE MSSV = @MaSV";
                    SqlCommand cmdGetDate = new SqlCommand(getDateQuery, conn);
                    cmdGetDate.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
                    object result = cmdGetDate.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DateTime ngayKetThucHienTai = Convert.ToDateTime(result);
                    DateTime ngayBatDauMoi = ngayKetThucHienTai; // Ngày bắt đầu mới là ngày kết thúc cũ
                    DateTime ngayKetThucMoi = ngayKetThucHienTai.AddMonths(3); // Cộng thêm 3 tháng

                    // DEBUG: Kiểm tra giá trị ngày tháng
                    MessageBox.Show($"Ngày bắt đầu mới: {ngayBatDauMoi:dd/MM/yyyy}\nNgày kết thúc mới: {ngayKetThucMoi:dd/MM/yyyy}",
                                    "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật ngày mới vào bảng NOI_TRU
                    string updateQuery = @"
                                          UPDATE NOI_TRU 
                                          SET NGAY_BAT_DAU_NOI_TRU = @NgayBatDauMoi, 
                                          NGAY_KET_THUC_NOI_TRU = @NgayKetThucMoi
                                          WHERE MSSV = @MaSV";

                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
                    cmdUpdate.Parameters.AddWithValue("@NgayBatDauMoi", ngayBatDauMoi);
                    cmdUpdate.Parameters.AddWithValue("@NgayKetThucMoi", ngayKetThucMoi);
                    cmdUpdate.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Cập nhật trạng thái thanh toán
                        string updateThanhToanQuery = @"
    UPDATE THANH_TOAN_PHONG
    SET TRANG_THAI_THANH_TOAN = N'Chưa thanh toán'
    WHERE MA_NOI_TRU IN (SELECT MA_NOI_TRU FROM NOI_TRU WHERE MSSV = @MaSV)";
                        SqlCommand cmdUpdateThanhToan = new SqlCommand(updateThanhToanQuery, conn);
                        cmdUpdateThanhToan.Parameters.AddWithValue("@MaSV", txtMasinhvien.Text);
                        cmdUpdateThanhToan.ExecuteNonQuery();

                        MessageBox.Show($"Gia hạn thành công!\nNgày mới bắt đầu: {ngayBatDauMoi:dd/MM/yyyy}\nNgày hết hạn mới: {ngayKetThucMoi:dd/MM/yyyy}",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Gọi lại hàm load dữ liệu
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể gia hạn nội trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT NOI_TRU.MSSV, SINH_VIEN.HOTEN_SV, NOI_TRU.MA_PHONG, PHONG.TEN_PHONG, NOI_TRU.NGAY_BAT_DAU_NOI_TRU, NOI_TRU.NGAY_KET_THUC_NOI_TRU, NOI_TRU.TRANG_THAI_NOI_TRU 
                        FROM NOI_TRU JOIN PHONG ON PHONG.MA_PHONG = NOI_TRU.MA_PHONG JOIN SINH_VIEN ON SINH_VIEN.MSSV = NOI_TRU.MSSV
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



        /// <summary>
        /// /// 4. Đổi phòng

        private void guna2Button1Doiphong_Click(object sender, EventArgs e)
        {
            ThayDoiPhong formDoiPhong = new ThayDoiPhong();
            formDoiPhong.ShowDialog(); // Hoặc dùng Show() nếu muốn mở không khóa form chính
        }



    }
}


