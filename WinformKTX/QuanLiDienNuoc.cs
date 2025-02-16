
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WinformKTX
{
    public partial class QuanLiDienNuoc : Form
    {
        public QuanLiDienNuoc()
        {
            InitializeComponent();
            dataGridView1Dien_nuoc.CellClick += dataGridView1Dien_nuoc_CellClick;
        
        }

        KetnoiCSDL kn = new KetnoiCSDL();

        // Hàm kiểm tra phòng có tồn tại hay không
        private bool KiemTraPhongTonTai(int maPhong)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong";
                SqlCommand cmdCheck = new SqlCommand(checkQuery, conn);
                cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong);

                int count = (int)cmdCheck.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnTracuudien_nuoc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTenphong.Text, out int maPhong))
            {
                MessageBox.Show("Mã phòng phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!KiemTraPhongTonTai(maPhong))
            {
                MessageBox.Show("Không tìm thấy phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MA_DIEN_NUOC, MA_PHONG, CHI_SO_DIEN_CU, CHI_SO_DIEN_MOI, CHI_SO_NUOC_CU, CHI_SO_NUOC_MOI, TIEN_DIEN, TIEN_NUOC, TONG_TIEN, TU_NGAY, DEN_NGAY, TINH_TRANG_TT " +
                           "FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong";



                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1Dien_nuoc.DataSource = dt;

                        // Xóa cột THOI_GIAN nếu tồn tại
                        if (dataGridView1Dien_nuoc.Columns.Contains("THOI_GIAN"))
                        {
                            dataGridView1Dien_nuoc.Columns.Remove("THOI_GIAN");
                        }

                        // Thêm cột nút THANH TOÁN nếu chưa có
                        if (!dataGridView1Dien_nuoc.Columns.Contains("ThanhToan"))
                        {
                            DataGridViewButtonColumn thanhToanButtonColumn = new DataGridViewButtonColumn();
                            thanhToanButtonColumn.Name = "ThanhToan";
                            thanhToanButtonColumn.HeaderText = "Thanh Toán";
                            thanhToanButtonColumn.Text = "Thanh Toán";
                            thanhToanButtonColumn.UseColumnTextForButtonValue = true;
                            dataGridView1Dien_nuoc.Columns.Add(thanhToanButtonColumn);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //private void btnCapnhatdien_nuoc_Click(object sender, EventArgs e)
        //{
        //    if (!int.TryParse(txtTenphong.Text, out int maPhong))
        //    {
        //        MessageBox.Show("Mã phòng phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (!KiemTraPhongTonTai(maPhong))
        //    {
        //        MessageBox.Show("Không tìm thấy phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    if (!int.TryParse(txtChisodien.Text, out int chiSoDienMoi))
        //    {
        //        MessageBox.Show("Chỉ số điện phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (!int.TryParse(txtChisonuoc.Text, out int chiSoNuocMoi))
        //    {
        //        MessageBox.Show("Chỉ số nước phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    using (SqlConnection conn = kn.GetConnection())
        //    {
        //        try
        //        {
        //            conn.Open();

        //            // Lấy giá điện, giá nước từ bảng GIA_DIEN_NUOC
        //            string getGiaQuery = "SELECT TOP 1 GIA_DIEN, GIA_NUOC FROM GIA_DIEN_NUOC ORDER BY MA_GIA_DN DESC";
        //            SqlCommand cmdGia = new SqlCommand(getGiaQuery, conn);
        //            SqlDataReader reader = cmdGia.ExecuteReader();

        //            double giaDien = 0, giaNuoc = 0;
        //            if (reader.Read())
        //            {
        //                giaDien = Convert.ToDouble(reader["GIA_DIEN"]);
        //                giaNuoc = Convert.ToDouble(reader["GIA_NUOC"]);
        //            }
        //            reader.Close();

        //            // Lấy chỉ số cũ từ bảng DIEN_NUOC
        //            string getChiSoCuQuery = "SELECT CHI_SO_DIEN_MOI, CHI_SO_NUOC_MOI FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong";
        //            SqlCommand cmdChiSo = new SqlCommand(getChiSoCuQuery, conn);
        //            cmdChiSo.Parameters.AddWithValue("@MaPhong", maPhong);
        //            reader = cmdChiSo.ExecuteReader();

        //            int chiSoDienCu = 0, chiSoNuocCu = 0;
        //            if (reader.Read())
        //            {
        //                chiSoDienCu = reader["CHI_SO_DIEN_MOI"] != DBNull.Value ? Convert.ToInt32(reader["CHI_SO_DIEN_MOI"]) : 0;
        //                chiSoNuocCu = reader["CHI_SO_NUOC_MOI"] != DBNull.Value ? Convert.ToInt32(reader["CHI_SO_NUOC_MOI"]) : 0;
        //            }
        //            reader.Close();

        //            // Tính tiền điện và nước
        //            double tienDien = (chiSoDienMoi - chiSoDienCu) * giaDien;
        //            double tienNuoc = (chiSoNuocMoi - chiSoNuocCu) * giaNuoc;
        //            double tongTien = tienDien + tienNuoc;

        //            // Cập nhật lại bảng DIEN_NUOC
        //            string updateQuery = "UPDATE DIEN_NUOC " +
        //                                 "SET CHI_SO_DIEN_CU = CHI_SO_DIEN_MOI, CHI_SO_DIEN_MOI = @ChiSoDienMoi, " +
        //                                 "CHI_SO_NUOC_CU = CHI_SO_NUOC_MOI, CHI_SO_NUOC_MOI = @ChiSoNuocMoi, " +
        //                                 "TIEN_DIEN = @TienDien, TIEN_NUOC = @TienNuoc, TONG_TIEN = @TongTien " +
        //                                 "WHERE MA_PHONG = @MaPhong";

        //            SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
        //            cmdUpdate.Parameters.AddWithValue("@ChiSoDienMoi", chiSoDienMoi);
        //            cmdUpdate.Parameters.AddWithValue("@ChiSoNuocMoi", chiSoNuocMoi);
        //            cmdUpdate.Parameters.AddWithValue("@TienDien", tienDien);
        //            cmdUpdate.Parameters.AddWithValue("@TienNuoc", tienNuoc);
        //            cmdUpdate.Parameters.AddWithValue("@TongTien", tongTien);
        //            cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);

        //            int rowsAffected = cmdUpdate.ExecuteNonQuery();
        //            if (rowsAffected > 0)
        //            {
        //                MessageBox.Show($"Cập nhật thành công!\nTiền điện: {tienDien} VND\nTiền nước: {tienNuoc} VND\nTổng tiền: {tongTien} VND",
        //                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                btnTracuudien_nuoc_Click(sender, e); // Cập nhật lại danh sách
        //            }
        //            else
        //            {
        //                MessageBox.Show("Không tìm thấy phòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}



        private void btnCapnhatdien_nuoc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTenphong.Text, out int maPhong))
            {
                MessageBox.Show("Mã phòng phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!KiemTraPhongTonTai(maPhong))
            {
                MessageBox.Show("Không tìm thấy phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtChisodien.Text, out int chiSoDienMoi))
            {
                MessageBox.Show("Chỉ số điện phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtChisonuoc.Text, out int chiSoNuocMoi))
            {
                MessageBox.Show("Chỉ số nước phải là số nguyên hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Lấy giá điện, giá nước từ bảng GIA_DIEN_NUOC
                    string getGiaQuery = "SELECT TOP 1 GIA_DIEN, GIA_NUOC FROM GIA_DIEN_NUOC ORDER BY MA_GIA_DN DESC";
                    SqlCommand cmdGia = new SqlCommand(getGiaQuery, conn);
                    SqlDataReader reader = cmdGia.ExecuteReader();

                    double giaDien = 0, giaNuoc = 0;
                    if (reader.Read())
                    {
                        giaDien = Convert.ToDouble(reader["GIA_DIEN"]);
                        giaNuoc = Convert.ToDouble(reader["GIA_NUOC"]);
                    }
                    reader.Close();

                    // Lấy chỉ số cũ từ bảng DIEN_NUOC
                    string getChiSoCuQuery = "SELECT CHI_SO_DIEN_MOI, CHI_SO_NUOC_MOI FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong";
                    SqlCommand cmdChiSo = new SqlCommand(getChiSoCuQuery, conn);
                    cmdChiSo.Parameters.AddWithValue("@MaPhong", maPhong);
                    reader = cmdChiSo.ExecuteReader();

                    int chiSoDienCu = 0, chiSoNuocCu = 0;
                    if (reader.Read())
                    {
                        chiSoDienCu = reader["CHI_SO_DIEN_MOI"] != DBNull.Value ? Convert.ToInt32(reader["CHI_SO_DIEN_MOI"]) : 0;
                        chiSoNuocCu = reader["CHI_SO_NUOC_MOI"] != DBNull.Value ? Convert.ToInt32(reader["CHI_SO_NUOC_MOI"]) : 0;
                    }
                    reader.Close();

                    // Kiểm tra nếu chỉ số điện mới hoặc nước mới nhỏ hơn chỉ số cũ
                    if (chiSoDienMoi <= chiSoDienCu)
                    {
                        MessageBox.Show("Vui lòng nhập lại! Chỉ số điện mới phải lớn hơn chỉ số điện cũ (" + chiSoDienCu + ").", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (chiSoNuocMoi <= chiSoNuocCu)
                    {
                        MessageBox.Show("Vui lòng nhập lại! Chỉ số nước mới phải lớn hơn chỉ số nước cũ (" + chiSoNuocCu + ").", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tính tiền điện và nước
                    double tienDien = (chiSoDienMoi - chiSoDienCu) * giaDien;
                    double tienNuoc = (chiSoNuocMoi - chiSoNuocCu) * giaNuoc;
                    double tongTien = tienDien + tienNuoc;

                    // Cập nhật lại bảng DIEN_NUOC
                    string updateQuery = "UPDATE DIEN_NUOC " +
                                 "SET CHI_SO_DIEN_CU = CHI_SO_DIEN_MOI, CHI_SO_DIEN_MOI = @ChiSoDienMoi, " +
                                 "CHI_SO_NUOC_CU = CHI_SO_NUOC_MOI, CHI_SO_NUOC_MOI = @ChiSoNuocMoi, " +
                                 "TIEN_DIEN = @TienDien, TIEN_NUOC = @TienNuoc, TONG_TIEN = @TongTien, " +
                                 "TU_NGAY = @TuNgay, DEN_NGAY = @DenNgay, TINH_TRANG_TT = N'Chưa thanh toán' " +
                                 "WHERE MA_PHONG = @MaPhong";

                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
                    cmdUpdate.Parameters.AddWithValue("@ChiSoDienMoi", chiSoDienMoi);
                    cmdUpdate.Parameters.AddWithValue("@ChiSoNuocMoi", chiSoNuocMoi);
                    cmdUpdate.Parameters.AddWithValue("@TienDien", tienDien);
                    cmdUpdate.Parameters.AddWithValue("@TienNuoc", tienNuoc);
                    cmdUpdate.Parameters.AddWithValue("@TongTien", tongTien);
                    cmdUpdate.Parameters.AddWithValue("@TuNgay", txtTungay.Value);
                    cmdUpdate.Parameters.AddWithValue("@DenNgay", txtDenngay.Value);
                    cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Cập nhật thành công!\nTiền điện: {tienDien} VND\nTiền nước: {tienNuoc} VND\nTổng tiền: {tongTien} VND",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnTracuudien_nuoc_Click(sender, e); // Cập nhật lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1Dien_nuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1Dien_nuoc.Columns["ThanhToan"].Index && e.RowIndex >= 0)
            {
                // Lấy MA_DIEN_NUOC của hàng được chọn
                int maDienNuoc = Convert.ToInt32(dataGridView1Dien_nuoc.Rows[e.RowIndex].Cells["MA_DIEN_NUOC"].Value);

                using (SqlConnection conn = kn.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        string updateQuery = "UPDATE DIEN_NUOC SET TINH_TRANG_TT = N'Đã thanh toán' WHERE MA_DIEN_NUOC = @MaDienNuoc";
                        SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
                        cmdUpdate.Parameters.AddWithValue("@MaDienNuoc", maDienNuoc);

                        int rowsAffected = cmdUpdate.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnTracuudien_nuoc_Click(sender, e); // Cập nhật lại danh sách
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtTungay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDenngay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
