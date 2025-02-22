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

            loadPhong();

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


        //private void btnCapnhatdien_nuoc_Click(object sender, EventArgs e)
        //{
        //    // Lấy mã phòng được chọn
        //    object selectedValue = comboBoxphong.SelectedValue;
        //    string selectedMaPhong = selectedValue is DataRowView dataRowView ? dataRowView["MA_PHONG"].ToString() : selectedValue.ToString();

        //    if (!int.TryParse(selectedMaPhong, out int maPhong))
        //    {
        //        MessageBox.Show("Mã phòng không hợp lệ.");
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
        //            string getChiSoCuQuery = "SELECT TOP 1 CHI_SO_DIEN_MOI, CHI_SO_NUOC_MOI FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong ORDER BY MA_DIEN_NUOC DESC";
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

        //            // Kiểm tra nếu chỉ số điện mới hoặc nước mới nhỏ hơn chỉ số cũ
        //            if (chiSoDienMoi <= chiSoDienCu)
        //            {
        //                MessageBox.Show("Vui lòng nhập lại! Chỉ số điện mới phải lớn hơn chỉ số điện cũ (" + chiSoDienCu + ").", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            if (chiSoNuocMoi <= chiSoNuocCu)
        //            {
        //                MessageBox.Show("Vui lòng nhập lại! Chỉ số nước mới phải lớn hơn chỉ số nước cũ (" + chiSoNuocCu + ").", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            // Tính tiền điện và nước
        //            double tienDien = (chiSoDienMoi - chiSoDienCu) * giaDien;
        //            double tienNuoc = (chiSoNuocMoi - chiSoNuocCu) * giaNuoc;
        //            double tongTien = tienDien + tienNuoc;

        //            // Tính số điện và nước tiêu thụ
        //            double soDienTieuThu = chiSoDienMoi - chiSoDienCu;
        //            double soNuocTieuThu = chiSoNuocMoi - chiSoNuocCu;

        //            // Thêm một mã điện nước mới vào bảng DIEN_NUOC
        //            string insertQuery = @"INSERT INTO DIEN_NUOC 
        //        (MA_PHONG, CHI_SO_DIEN_CU, CHI_SO_DIEN_MOI, CHI_SO_NUOC_CU, CHI_SO_NUOC_MOI, 
        //        SO_DIEN_DA_SU_DUNG, TIEN_DIEN, TIEN_NUOC, SO_NUOC_DA_SU_DUNG, TONG_TIEN, TU_NGAY, DEN_NGAY, TINH_TRANG_TT) 
        //        VALUES 
        //        (@MaPhong, @ChiSoDienCu, @ChiSoDienMoi, @ChiSoNuocCu, @ChiSoNuocMoi, 
        //        @SoDienDaSuDung, @TienDien, @TienNuoc, @SoNuocDaSuDung, @TongTien, @TuNgay, @DenNgay, N'Chưa thanh toán')";

        //            SqlCommand cmdInsert = new SqlCommand(insertQuery, conn);
        //            cmdInsert.Parameters.AddWithValue("@MaPhong", maPhong);
        //            cmdInsert.Parameters.AddWithValue("@ChiSoDienCu", chiSoDienCu);
        //            cmdInsert.Parameters.AddWithValue("@ChiSoDienMoi", chiSoDienMoi);
        //            cmdInsert.Parameters.AddWithValue("@ChiSoNuocCu", chiSoNuocCu);
        //            cmdInsert.Parameters.AddWithValue("@ChiSoNuocMoi", chiSoNuocMoi);
        //            cmdInsert.Parameters.AddWithValue("@SoDienDaSuDung", soDienTieuThu);
        //            cmdInsert.Parameters.AddWithValue("@TienDien", tienDien);
        //            cmdInsert.Parameters.AddWithValue("@TienNuoc", tienNuoc);
        //            cmdInsert.Parameters.AddWithValue("@SoNuocDaSuDung", soNuocTieuThu);
        //            cmdInsert.Parameters.AddWithValue("@TongTien", tongTien);
        //            cmdInsert.Parameters.AddWithValue("@TuNgay", txtTungay.Value);
        //            cmdInsert.Parameters.AddWithValue("@DenNgay", txtDenngay.Value);

        //            int rowsAffected = cmdInsert.ExecuteNonQuery();
        //            if (rowsAffected > 0)
        //            {
        //                MessageBox.Show($"Cập nhật thành công!\nSố điện đã sử dụng: {soDienTieuThu} kWh\nTiền điện: {tienDien} VND\nSố nước đã sử dụng: {soNuocTieuThu} m³\nTiền nước: {tienNuoc} VND\nTổng tiền: {tongTien} VND",
        //                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                loadPhong(); // Cập nhật lại danh sách phòng
        //            }
        //            else
        //            {
        //                MessageBox.Show("Không thể thêm dữ liệu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    ResetInputFields(); // Reset các ô nhập liệu sau khi cập nhật
        //}

        private void btnCapnhatdien_nuoc_Click(object sender, EventArgs e)
        {
            object selectedValue = comboBoxphong.SelectedValue;
            string selectedMaPhong = selectedValue is DataRowView dataRowView ? dataRowView["MA_PHONG"].ToString() : selectedValue.ToString();

            if (!int.TryParse(selectedMaPhong, out int maPhong))
            {
                MessageBox.Show("Mã phòng không hợp lệ.");
                return;
            }

            //if (!KiemTraPhongTonTai(maPhong))
            //{
            //    MessageBox.Show(maPhong.ToString());
            //    MessageBox.Show("Không tìm thấy phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (!int.TryParse(txtChisodien.Text, out int chiSoDienMoi) || chiSoDienMoi <= 0)
            {
                MessageBox.Show("Chỉ số điện phải là số nguyên dương!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtChisonuoc.Text, out int chiSoNuocMoi) || chiSoNuocMoi <= 0)
            {
                MessageBox.Show("Chỉ số nước phải là số nguyên dương!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // 1. Lấy `MA_GIA_DN` và giá điện, giá nước mới nhất
                    string getGiaQuery = "SELECT TOP 1 MA_GIA_DN, GIA_DIEN, GIA_NUOC FROM GIA_DIEN_NUOC ORDER BY MA_GIA_DN DESC";
                    SqlCommand cmdGia = new SqlCommand(getGiaQuery, conn);
                    SqlDataReader reader = cmdGia.ExecuteReader();

                    int maGiaDN = 0;
                    double giaDien = 0, giaNuoc = 0;
                    if (reader.Read())
                    {
                        maGiaDN = Convert.ToInt32(reader["MA_GIA_DN"]);
                        giaDien = Convert.ToDouble(reader["GIA_DIEN"]);
                        giaNuoc = Convert.ToDouble(reader["GIA_NUOC"]);
                    }
                    reader.Close();

                    // 2. Lấy chỉ số điện, nước cũ từ `DIEN_NUOC`
                    string getChiSoCuQuery = "SELECT TOP 1 CHI_SO_DIEN_MOI, CHI_SO_NUOC_MOI FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong ORDER BY MA_DIEN_NUOC DESC";
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

                    // 3. Kiểm tra nếu chỉ số mới nhỏ hơn chỉ số cũ
                    if (chiSoDienMoi <= chiSoDienCu)
                    {
                        MessageBox.Show($"Chỉ số điện mới phải lớn hơn chỉ số điện cũ ({chiSoDienCu}).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (chiSoNuocMoi <= chiSoNuocCu)
                    {
                        MessageBox.Show($"Chỉ số nước mới phải lớn hơn chỉ số nước cũ ({chiSoNuocCu}).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 4. Tính tiền điện và nước
                    double soDienTieuThu = chiSoDienMoi - chiSoDienCu;
                    double soNuocTieuThu = chiSoNuocMoi - chiSoNuocCu;
                    double tienDien = soDienTieuThu * giaDien;
                    double tienNuoc = soNuocTieuThu * giaNuoc;
                    double tongTien = tienDien + tienNuoc;

                    // 5. Thêm dữ liệu mới vào bảng `DIEN_NUOC`
                    string insertQuery = @"INSERT INTO DIEN_NUOC 
                (MA_GIA_DN, MA_PHONG, CHI_SO_DIEN_CU, CHI_SO_DIEN_MOI, CHI_SO_NUOC_CU, CHI_SO_NUOC_MOI, 
                SO_DIEN_DA_SU_DUNG, TIEN_DIEN, TIEN_NUOC, SO_NUOC_DA_SU_DUNG, TONG_TIEN, TU_NGAY, DEN_NGAY, TINH_TRANG_TT, NGAY_THANH_TOAN_DIEN_NUOC) 
                VALUES 
                (@MaGiaDN, @MaPhong, @ChiSoDienCu, @ChiSoDienMoi, @ChiSoNuocCu, @ChiSoNuocMoi, 
                @SoDienDaSuDung, @TienDien, @TienNuoc, @SoNuocDaSuDung, @TongTien, @TuNgay, @DenNgay, N'Chưa thanh toán', NULL)";

                    SqlCommand cmdInsert = new SqlCommand(insertQuery, conn);
                    cmdInsert.Parameters.AddWithValue("@MaGiaDN", maGiaDN);
                    cmdInsert.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmdInsert.Parameters.AddWithValue("@ChiSoDienCu", chiSoDienCu);
                    cmdInsert.Parameters.AddWithValue("@ChiSoDienMoi", chiSoDienMoi);
                    cmdInsert.Parameters.AddWithValue("@ChiSoNuocCu", chiSoNuocCu);
                    cmdInsert.Parameters.AddWithValue("@ChiSoNuocMoi", chiSoNuocMoi);
                    cmdInsert.Parameters.AddWithValue("@SoDienDaSuDung", soDienTieuThu);
                    cmdInsert.Parameters.AddWithValue("@TienDien", tienDien);
                    cmdInsert.Parameters.AddWithValue("@TienNuoc", tienNuoc);
                    cmdInsert.Parameters.AddWithValue("@SoNuocDaSuDung", soNuocTieuThu);
                    cmdInsert.Parameters.AddWithValue("@TongTien", tongTien);
                    cmdInsert.Parameters.AddWithValue("@TuNgay", txtTungay.Value);
                    cmdInsert.Parameters.AddWithValue("@DenNgay", txtDenngay.Value);

                    int rowsAffected = cmdInsert.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Cập nhật thành công!\nSố điện tiêu thụ: {soDienTieuThu} kWh\nTiền điện: {tienDien} VND\nSố nước tiêu thụ: {soNuocTieuThu} m³\nTiền nước: {tienNuoc} VND\nTổng tiền: {tongTien} VND",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadPhong(); // Cập nhật danh sách phòng
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm dữ liệu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ResetInputFields(); // Reset các ô nhập liệu sau khi cập nhật
        }

        //private void dataGridView1Dien_nuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && dataGridView1Dien_nuoc.Columns["ThanhToan"] != null && dataGridView1Dien_nuoc.Rows[e.RowIndex] != null)
        //    {
        //        if (e.ColumnIndex == dataGridView1Dien_nuoc.Columns["ThanhToan"].Index)
        //        {
        //            // Kiểm tra sự tồn tại của cột "TINH_TRANG_TT"
        //            if (dataGridView1Dien_nuoc.Columns.Contains("TINH_TRANG_TT"))
        //            {
        //                string tinhTrangTT = dataGridView1Dien_nuoc.Rows[e.RowIndex].Cells["TINH_TRANG_TT"].Value?.ToString();
        //                if (tinhTrangTT == "Đã thanh toán")
        //                {
        //                    MessageBox.Show("Hóa đơn này đã được thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return;
        //                }

        //                // Lấy MA_DIEN_NUOC của hàng được chọn
        //                int maDienNuoc = Convert.ToInt32(dataGridView1Dien_nuoc.Rows[e.RowIndex].Cells["MA_DIEN_NUOC"].Value);

        //                using (SqlConnection conn = kn.GetConnection())
        //                {
        //                    try
        //                    {
        //                        conn.Open();
        //                        string updateQuery = "UPDATE DIEN_NUOC SET TINH_TRANG_TT = N'Đã thanh toán' WHERE MA_DIEN_NUOC = @MaDienNuoc";
        //                        SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
        //                        cmdUpdate.Parameters.AddWithValue("@MaDienNuoc", maDienNuoc);

        //                        int rowsAffected = cmdUpdate.ExecuteNonQuery();
        //                        if (rowsAffected > 0)
        //                        {
        //                            MessageBox.Show("Cập nhật trạng thái thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                           // btnTracuudien_nuoc_Click(sender, e); // Cập nhật lại danh sách
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Không tìm thấy dữ liệu để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Cột 'TINH_TRANG_TT' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //}

        private void dataGridView1Dien_nuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1Dien_nuoc.Rows[e.RowIndex] != null)
            {
                

                // Kiểm tra sự tồn tại của cột "TINH_TRANG_TT"
                if (dataGridView1Dien_nuoc.Columns.Contains("TINH_TRANG_TT"))
                {
                    string tinhTrangTT = dataGridView1Dien_nuoc.Rows[e.RowIndex].Cells["TINH_TRANG_TT"].Value?.ToString();
                    if (tinhTrangTT == "Đã thanh toán")
                    {
                        MessageBox.Show("Hóa đơn này đã được thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

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
                                // btnTracuudien_nuoc_Click(sender, e); // Cập nhật lại danh sách nếu cần
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
                else
                {
                    MessageBox.Show("Cột 'TINH_TRANG_TT' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void ResetInputFields()
        {
            // Đặt lại TextBox về trống
            //txtTenphong.Text = "";
            txtChisodien.Text = "";
            txtChisonuoc.Text = "";

            // Đặt lại ComboBox về giá trị mặc định
            txtTungay.Value = DateTime.Now;  // Nếu muốn giá trị đầu tiên thì dùng cmbThang.SelectedIndex = 0;
            txtDenngay.Value = DateTime.Now;
        }

        //private void btnThongkediennuoc_Click(object sender, EventArgs e)
        //{
        //    using (SqlConnection conn = kn.GetConnection())
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string query = "SELECT DN.MA_DIEN_NUOC, P.TEN_PHONG, T.TEN_TANG, DN.TIEN_DIEN, DN.TIEN_NUOC, DN.TONG_TIEN, DN.TINH_TRANG_TT " +
        //                "FROM DIEN_NUOC DN " +
        //                "JOIN PHONG P ON DN.MA_PHONG = P.MA_PHONG" +
        //                "JOIN TANG T ON P.MA_TANG = T.MA_TANG "; // Thêm dấu cách ở cuối dòng này

        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            if (dt.Rows.Count == 0)
        //            {
        //                MessageBox.Show("Không có dữ liệu thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                dataGridView1Dien_nuoc.DataSource = dt;
        //                // Đặt tên cột hiển thị trong DataGridView
        //                dataGridView1Dien_nuoc.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
        //                dataGridView1Dien_nuoc.Columns["TIEN_DIEN"].HeaderText = "Tiền Điện";
        //                dataGridView1Dien_nuoc.Columns["TIEN_NUOC"].HeaderText = "Tiền Nước";
        //                dataGridView1Dien_nuoc.Columns["TONG_TIEN"].HeaderText = "Tổng Tiền";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi khi truy vấn dữ liệu thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void btnThongkediennuoc_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT DN.MA_DIEN_NUOC, P.TEN_PHONG, T.TEN_TANG, DN.TU_NGAY, DN.DEN_NGAY,
                                    DN.TIEN_DIEN, DN.TIEN_NUOC, DN.TONG_TIEN, DN.TINH_TRANG_TT 
                             FROM DIEN_NUOC DN 
                             JOIN PHONG P ON DN.MA_PHONG = P.MA_PHONG 
                             JOIN TANG T ON P.MA_TANG = T.MA_TANG 
                             ORDER BY DN.MA_DIEN_NUOC DESC"; // Hiển thị dữ liệu mới nhất trước

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Không có dữ liệu thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dataGridView1Dien_nuoc.DataSource = dt;

                            // Thiết lập tiêu đề cột hiển thị trong DataGridView
                            dataGridView1Dien_nuoc.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridView1Dien_nuoc.Columns["TEN_TANG"].HeaderText = "Tầng";
                            dataGridView1Dien_nuoc.Columns["TIEN_DIEN"].HeaderText = "Tiền Điện";
                            dataGridView1Dien_nuoc.Columns["TIEN_NUOC"].HeaderText = "Tiền Nước";
                            dataGridView1Dien_nuoc.Columns["TONG_TIEN"].HeaderText = "Tổng Tiền";
                            dataGridView1Dien_nuoc.Columns["TINH_TRANG_TT"].HeaderText = "Tình Trạng Thanh Toán";
                            dataGridView1Dien_nuoc.Columns["TU_NGAY"].HeaderText = "Từ Ngày";
                            dataGridView1Dien_nuoc.Columns["DEN_NGAY"].HeaderText = "Đến Ngày";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void loadPhong()
        {
            using (SqlConnection conn = kn.GetConnection()) // Giả sử `kn` là đối tượng kết nối SQL
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT MA_PHONG, TEN_PHONG
                FROM PHONG
                WHERE SO_GIUONG_CON_TRONG < SO_GIUONG_TOI_DA";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán dữ liệu vào ComboBox hoặc DataGridView
                    comboBoxphong.DataSource = dt;
                    comboBoxphong.DisplayMember = "TEN_PHONG"; // Hiển thị tên phòng
                    comboBoxphong.ValueMember = "MA_PHONG";   // Lưu mã phòng

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy mã phòng được chọn
            object selectedValue = comboBoxphong.SelectedValue;
            string selectedMaPhong = selectedValue is DataRowView dataRowView ? dataRowView["MA_PHONG"].ToString() : selectedValue.ToString();

            if (!int.TryParse(selectedMaPhong, out int maPhong))
            {
                MessageBox.Show("Mã phòng không hợp lệ.");
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Kiểm tra xem phòng có sinh viên nội trú hay không
                    string checkNoiTruQuery = "SELECT COUNT(*) FROM NOI_TRU WHERE MA_PHONG = @MaPhong";
                    SqlCommand cmdCheckNoiTru = new SqlCommand(checkNoiTruQuery, conn);
                    cmdCheckNoiTru.Parameters.AddWithValue("@MaPhong", maPhong);

                    int countNoiTru = (int)cmdCheckNoiTru.ExecuteScalar();
                    if (countNoiTru == 0)
                    {
                        MessageBox.Show("Phòng chưa được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Truy vấn thông tin điện nước của phòng
                    string query = @"
                                SELECT 
                                    p.MA_PHONG, 
                                    p.TEN_PHONG,
                                    dn.MA_DIEN_NUOC,
	                                dn.TU_NGAY,
	                                dn.DEN_NGAY,
                                    dn.CHI_SO_DIEN_CU,
                                    dn.CHI_SO_DIEN_MOI,
	                                dn.CHI_SO_NUOC_CU,
                                    dn.CHI_SO_NUOC_MOI,
	                                dn.SO_DIEN_DA_SU_DUNG,
	                                dn.SO_NUOC_DA_SU_DUNG,
                                    dn.TIEN_DIEN, 
                                    dn.TIEN_NUOC, 
                                    dn.TONG_TIEN,
                                    dn.NGAY_THANH_TOAN_DIEN_NUOC,
                                    dn.TINH_TRANG_TT
                                FROM PHONG p
                                OUTER APPLY (
                                    SELECT TOP 1 
                                        d.CHI_SO_DIEN_MOI, 
                                        d.CHI_SO_NUOC_MOI, 
                                        d.TIEN_DIEN, 
                                        d.TIEN_NUOC, 
                                        d.TONG_TIEN,
		                                d.TU_NGAY,
		                                d.DEN_NGAY,
		                                d.CHI_SO_DIEN_CU,
		                                d.CHI_SO_NUOC_CU,
		                                d.SO_DIEN_DA_SU_DUNG,
		                                d.SO_NUOC_DA_SU_DUNG,
                                        d.NGAY_THANH_TOAN_DIEN_NUOC,
                                        d.TINH_TRANG_TT,
                                        d.MA_DIEN_NUOC
                                    FROM DIEN_NUOC d
                                    WHERE d.MA_PHONG = p.MA_PHONG
                                    ORDER BY d.TU_NGAY DESC
                                    ) dn
                                WHERE p.MA_PHONG = @MaPhong AND p.SO_GIUONG_CON_TRONG < p.SO_GIUONG_TOI_DA;"; // Chỉ lấy phòng có sinh viên ở

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

                        

                        
                        

                        // Đổi tên cột hiển thị trên DataGridView
                        dataGridView1Dien_nuoc.Columns["MA_PHONG"].HeaderText = "Mã Phòng";
                        dataGridView1Dien_nuoc.Columns["TU_NGAY"].HeaderText = "Từ Ngày";
                        dataGridView1Dien_nuoc.Columns["DEN_NGAY"].HeaderText = "Đến Ngày";
                        dataGridView1Dien_nuoc.Columns["CHI_SO_DIEN_CU"].HeaderText = "Chỉ Số Điện Cũ";
                        dataGridView1Dien_nuoc.Columns["CHI_SO_DIEN_MOI"].HeaderText = "Chỉ Số Điện Mới";
                        dataGridView1Dien_nuoc.Columns["SO_DIEN_DA_SU_DUNG"].HeaderText = "Số Điện Đã Sử Dụng";
                        dataGridView1Dien_nuoc.Columns["CHI_SO_NUOC_CU"].HeaderText = "Chí Số Nước Cũ";
                        dataGridView1Dien_nuoc.Columns["CHI_SO_NUOC_MOI"].HeaderText = "Chỉ Số Nước Mới";
                        dataGridView1Dien_nuoc.Columns["SO_NUOC_DA_SU_DUNG"].HeaderText = "Số Nước Đã Sử Dụng";
                        dataGridView1Dien_nuoc.Columns["TIEN_DIEN"].HeaderText = "Tiền Điện";
                        dataGridView1Dien_nuoc.Columns["TIEN_NUOC"].HeaderText = "Tiền Nước";
                        dataGridView1Dien_nuoc.Columns["TONG_TIEN"].HeaderText = "Tổng Tiền";
                        dataGridView1Dien_nuoc.Columns["NGAY_THANH_TOAN_DIEN_NUOC"].HeaderText = "Ngày Thanh Toán Điện - Nước";
                        dataGridView1Dien_nuoc.Columns["TINH_TRANG_TT"].HeaderText = "Tình Trạng Thanh Toán";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ResetInputFields(); // Reset các ô nhập liệu sau khi tìm kiếm
        }
    }
}
