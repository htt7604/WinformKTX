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

            

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    //// Kiểm tra xem phòng có sinh viên nội trú hay không
                    //string checkNoiTruQuery = "SELECT COUNT(*) FROM NOI_TRU WHERE MA_PHONG = @MaPhong";
                    //SqlCommand cmdCheckNoiTru = new SqlCommand(checkNoiTruQuery, conn);
                    //cmdCheckNoiTru.Parameters.AddWithValue("@MaPhong", maPhong);

                    //int countNoiTru = (int)cmdCheckNoiTru.ExecuteScalar();
                    //if (countNoiTru == 0)
                    //{
                    //    MessageBox.Show("Phòng chưa được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}


                    // Truy vấn thông tin điện nước của phòng
                    //string query = "SELECT MA_DIEN_NUOC, MA_PHONG, CHI_SO_DIEN_CU, CHI_SO_DIEN_MOI, SO_DIEN_DA_SU_DUNG, CHI_SO_NUOC_CU, CHI_SO_NUOC_MOI, SO_NUOC_DA_SU_DUNG, TIEN_DIEN, TIEN_NUOC, TONG_TIEN, TU_NGAY, DEN_NGAY, NGAY_THANH_TOAN_DIEN_NUOC, TINH_TRANG_TT " +
                    //       "FROM DIEN_NUOC WHERE MA_PHONG = @MaPhong";
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

                        // Xóa cột "Thanh Toán" nếu đã tồn tại để tránh bị trùng lặp
                        if (dataGridView1Dien_nuoc.Columns.Contains("ThanhToan"))
                        {
                            dataGridView1Dien_nuoc.Columns.Remove("ThanhToan");
                        }

                        // Thêm cột nút "Thanh Toán"
                        DataGridViewButtonColumn thanhToanButtonColumn = new DataGridViewButtonColumn
                        {
                            Name = "ThanhToan",
                            HeaderText = "Thanh Toán",
                            Text = "Thanh Toán",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1Dien_nuoc.Columns.Add(thanhToanButtonColumn);

                        // Kiểm tra từng hàng trong DataGridView
                        foreach (DataGridViewRow row in dataGridView1Dien_nuoc.Rows)
                        {
                            if (row.Cells["TINH_TRANG_TT"].Value != null && row.Cells["TINH_TRANG_TT"].Value.ToString() == "Đã thanh toán")
                            {
                                row.Cells["ThanhToan"].Value = ""; // Ẩn nút bằng cách không hiển thị text
                                row.Cells["ThanhToan"].ReadOnly = true; // Không cho bấm
                            }
                        }

                        // Đổi tên cột hiển thị trên DataGridView
                        dataGridView1Dien_nuoc.Columns["MA_DIEN_NUOC"].HeaderText = "Mã Điện - Nước";
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
                        dataGridView1Dien_nuoc.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                          
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ResetInputFields(); // Reset các ô nhập liệu sau khi tìm kiếm
        }

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

                    //Tính số điện và nước tiêu thụ
                    double soDienTieuThu = chiSoDienMoi - chiSoDienCu;
                    double soNuocTieuThu = chiSoNuocMoi - chiSoNuocCu;

                    // Cập nhật lại bảng DIEN_NUOC
                    string updateQuery = "UPDATE DIEN_NUOC " +
                                 "SET CHI_SO_DIEN_CU = CHI_SO_DIEN_MOI, CHI_SO_DIEN_MOI = @ChiSoDienMoi, " +
                                 "CHI_SO_NUOC_CU = CHI_SO_NUOC_MOI, CHI_SO_NUOC_MOI = @ChiSoNuocMoi, " +
                                 "SO_DIEN_DA_SU_DUNG = @SoDienDaSuDung," +
                                 "TIEN_DIEN = @TienDien, TIEN_NUOC = @TienNuoc," +
                                 "SO_NUOC_DA_SU_DUNG = @SoNuocDaSuDung, TONG_TIEN = @TongTien, " +
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
                    cmdUpdate.Parameters.AddWithValue("@SoDienDaSuDung", soDienTieuThu);
                    cmdUpdate.Parameters.AddWithValue("@SoNuocDaSuDung", soNuocTieuThu);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Cập nhật thành công!\nSố điện đã sử dụng: {soDienTieuThu} kWh\nTiền điện: {tienDien} VND\nSố nước đã sử dụng: {soNuocTieuThu} m³\nTiền nước: {tienNuoc} VND\nTổng tiền: {tongTien} VND",
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
            ResetInputFields(); // Reset các ô nhập liệu sau khi tìm kiếm
        }

        //private void dataGridView1Dien_nuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == dataGridView1Dien_nuoc.Columns["ThanhToan"].Index && e.RowIndex >= 0)
        //    {
        //        // Kiểm tra sự tồn tại của cột "TINH_TRANG_TT"
        //        if (dataGridView1Dien_nuoc.Columns.Contains("TINH_TRANG_TT"))
        //        {


        //            //mới thêm chỗ này
        //            string tinhTrangTT = dataGridView1Dien_nuoc.Rows[e.RowIndex].Cells["TINH_TRANG_TT"].Value.ToString();
        //            if (tinhTrangTT == "Đã thanh toán")
        //            {
        //                MessageBox.Show("Hóa đơn này đã được thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }

        //            // Lấy MA_DIEN_NUOC của hàng được chọn
        //            int maDienNuoc = Convert.ToInt32(dataGridView1Dien_nuoc.Rows[e.RowIndex].Cells["MA_DIEN_NUOC"].Value);

        //            using (SqlConnection conn = kn.GetConnection())
        //            {
        //                try
        //                {
        //                    conn.Open();
        //                    string updateQuery = "UPDATE DIEN_NUOC SET TINH_TRANG_TT = N'Đã thanh toán' WHERE MA_DIEN_NUOC = @MaDienNuoc";
        //                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
        //                    cmdUpdate.Parameters.AddWithValue("@MaDienNuoc", maDienNuoc);

        //                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
        //                    if (rowsAffected > 0)
        //                    {
        //                        MessageBox.Show("Cập nhật trạng thái thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        btnTracuudien_nuoc_Click(sender, e); // Cập nhật lại danh sách
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Không tìm thấy dữ liệu để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Cột 'TINH_TRANG_TT' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
        private void dataGridView1Dien_nuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1Dien_nuoc.Columns["ThanhToan"] != null && dataGridView1Dien_nuoc.Rows[e.RowIndex] != null)
            {
                if (e.ColumnIndex == dataGridView1Dien_nuoc.Columns["ThanhToan"].Index)
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
                    else
                    {
                        MessageBox.Show("Cột 'TINH_TRANG_TT' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }




        private void ResetInputFields()
        {
            // Đặt lại TextBox về trống
            txtTenphong.Text = "";
            txtChisodien.Text = "";
            txtChisonuoc.Text = "";

            // Đặt lại ComboBox về giá trị mặc định
            txtTungay.Value = DateTime.Now;  // Nếu muốn giá trị đầu tiên thì dùng cmbThang.SelectedIndex = 0;
            txtDenngay.Value = DateTime.Now;
        }

        private void btnThongkediennuoc_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DN.MA_DIEN_NUOC, P.TEN_PHONG, DN.TIEN_DIEN, DN.TIEN_NUOC, DN.TONG_TIEN, DN.TINH_TRANG_TT " +
                        "FROM DIEN_NUOC DN " +
                        "JOIN PHONG P ON DN.MA_PHONG = P.MA_PHONG "; // Thêm dấu cách ở cuối dòng này
               
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1Dien_nuoc.DataSource = dt;
                        // Đặt tên cột hiển thị trong DataGridView
                        dataGridView1Dien_nuoc.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                        dataGridView1Dien_nuoc.Columns["TIEN_DIEN"].HeaderText = "Tiền Điện";
                        dataGridView1Dien_nuoc.Columns["TIEN_NUOC"].HeaderText = "Tiền Nước";
                        dataGridView1Dien_nuoc.Columns["TONG_TIEN"].HeaderText = "Tổng Tiền";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
