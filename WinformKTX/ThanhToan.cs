using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsAppKTX;
using Microsoft.Data.SqlClient;

namespace WinformKTX
{
    public partial class ThanhToan : Form
    {
        KetnoiCSDL ketnoi = new KetnoiCSDL();
        public ThanhToan()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpthang_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtmasosinhvien_TextChanged(object sender, EventArgs e)
        {

        }

        private void btntracuu_Click(object sender, EventArgs e)
        {
            string hoten = txthoten.Text.Trim();
            string mssv = txtmasosinhvien.Text.Trim();

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query;

                    if (hoten.ToLower() == "all" || mssv.ToLower() == "all")
                    {
                        query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                "TTP.MA_PHONG AS 'Mã phòng', " +
                                "SV.HOTEN_SV AS 'Họ tên', " +
                                "TTP.MSSV AS 'MSSV', " +
                                "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                "FROM THANH_TOAN_PHONG TTP " +
                                "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG";
                    }
                    else
                    {
                        query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                "TTP.MA_PHONG AS 'Mã phòng', " +
                                "SV.HOTEN_SV AS 'Họ tên', " +
                                "TTP.MSSV AS 'MSSV', " +
                                "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                "FROM THANH_TOAN_PHONG TTP " +
                                "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                "WHERE SV.HOTEN_SV = @hoten AND TTP.MSSV = @mssv";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (hoten.ToLower() != "all" && mssv.ToLower() != "all")
                        {
                            cmd.Parameters.AddWithValue("@hoten", hoten);
                            cmd.Parameters.AddWithValue("@mssv", mssv);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            guna2DataGridView2.DataSource = dt;
                            foreach (DataRow row in dt.Rows)
                            {
                                string maThanhToan = row["Mã thanh toán phòng"].ToString();
                                decimal giaTien = Convert.ToDecimal(row["Giá tiền phòng"]);

                                string updateQuery = "UPDATE THANH_TOAN_PHONG SET GIA_TIEN = @giaTien WHERE MA_THANH_TOAN_PHONG = @maThanhToan";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@giaTien", giaTien);
                                    updateCmd.Parameters.AddWithValue("@maThanhToan", maThanhToan);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    txthoten.Clear();
                    txtmasosinhvien.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void ThanhToan_Load(object sender, EventArgs e)
        {

        }

        private void btnxacnhanthanhtoan_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count > 0)
            {
                string maThanhToan = guna2DataGridView2.SelectedRows[0].Cells["Mã thanh toán phòng"].Value.ToString();
                string maSoSinhVien = guna2DataGridView2.SelectedRows[0].Cells["MSSV"].Value.ToString();

                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        // Kiểm tra TRANG_THAI_NOI_TRU của sinh viên
                        string checkQuery = "SELECT TRANG_THAI_NOI_TRU FROM SINH_VIEN WHERE MSSV = @mssv";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@mssv", maSoSinhVien);
                            string trangThaiNoiTru = (string)checkCmd.ExecuteScalar();

                            if (trangThaiNoiTru != "Đang nội trú")
                            {
                                MessageBox.Show("Sinh viên chưa nội trú, không thể xác nhận thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Cập nhật trạng thái thanh toán
                        string updateQuery = "UPDATE THANH_TOAN_PHONG SET TRANG_THAI_THANH_TOAN = N'Đã thanh toán', NGAY_THANH_TOAN = GETDATE() WHERE MA_THANH_TOAN_PHONG = @maThanhToan";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@maThanhToan", maThanhToan);
                            updateCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btntracuu_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xác nhận thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txthoten.Clear();
            txtmasosinhvien.Clear();
        }


        private void btnloaiphongnam_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                   "TTP.MA_PHONG AS 'Mã phòng', " +
                                   "LP.TEN_LOAI_PHONG AS 'Tên loại phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "TTP.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                   "WHERE LP.TEN_LOAI_PHONG LIKE N'%Nam%'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        guna2DataGridView2.DataSource = dt;
                    }
                    UpdateSoluong2("LP.TEN_LOAI_PHONG LIKE N'%Nam%'");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnloaiphongnu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                   "TTP.MA_PHONG AS 'Mã phòng', " +
                                   "LP.TEN_LOAI_PHONG AS 'Tên loại phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "TTP.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                   "WHERE LP.TEN_LOAI_PHONG LIKE N'%Nữ%'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        guna2DataGridView2.DataSource = dt;
                    }
                    UpdateSoluong2("LP.TEN_LOAI_PHONG LIKE N'%Nữ%'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnchuathanhtoan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                   "TTP.MA_PHONG AS 'Mã phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "TTP.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                   "WHERE TTP.TRANG_THAI_THANH_TOAN = N'Chưa thanh toán'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        guna2DataGridView2.DataSource = dt;
                    }
                    UpdateSoluong2("TTP.TRANG_THAI_THANH_TOAN LIKE N'%Chưa thanh toán%'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btndathanhtoan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                   "TTP.MA_PHONG AS 'Mã phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "TTP.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                   "WHERE TTP.TRANG_THAI_THANH_TOAN = N'Đã thanh toán'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        guna2DataGridView2.DataSource = dt;
                    }
                    UpdateSoluong2("TTP.TRANG_THAI_THANH_TOAN LIKE N'%Đã thanh toán%'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btntracuu1_Click(object sender, EventArgs e)
        {
            string tenTang = txttang.Text.Trim();
            string maPhong = txtmaphong.Text.Trim();
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                   "TTP.MA_PHONG AS 'Mã phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "TTP.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán', " +
                                   "T.TEN_TANG AS 'Tên tầng' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                   "JOIN TANG T ON P.MA_TANG = T.MA_TANG " +
                                   "WHERE T.TEN_TANG = @tenTang OR TTP.MA_PHONG = @maPhong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenTang", tenTang);
                        cmd.Parameters.AddWithValue("@maPhong", maPhong);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            guna2DataGridView2.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    txtmaphong.Clear();
                    txttang.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btninhoadon_Click(object sender, EventArgs e)
        {
            string maThanhToan = txtmathanhtoan.Text.Trim();

            if (string.IsNullOrEmpty(maThanhToan))
            {
                MessageBox.Show("Vui lòng nhập mã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (KiemTraDaThanhToan(maThanhToan))
            {
                InHoaDon(maThanhToan);
            }
            else
            {
                MessageBox.Show("Mã thanh toán chưa được thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtmathanhtoan.Clear();
        }

        private bool KiemTraDaThanhToan(string maThanhToan)
        {
            bool daThanhToan = false;

            string query = "SELECT TRANG_THAI_THANH_TOAN FROM THANH_TOAN_PHONG WHERE MA_THANH_TOAN_PHONG = @MaThanhToan";
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaThanhToan", maThanhToan);
                    object result = cmd.ExecuteScalar();
                    daThanhToan = result != null && result.ToString() == "Đã thanh toán";
                }
            }

            return daThanhToan;
        }

        private void InHoaDon(string maThanhToan)
        {
            string query = "SELECT T.MA_THANH_TOAN_PHONG, S.HOTEN_SV, P.TEN_PHONG, T.NGAY_THANH_TOAN, T.GIA_TIEN FROM THANH_TOAN_PHONG T " +
                           "JOIN SINH_VIEN S ON T.MSSV = S.MSSV " +
                           "JOIN PHONG P ON T.MA_PHONG = P.MA_PHONG " +
                           "WHERE T.MA_THANH_TOAN_PHONG = @MaThanhToan";

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaThanhToan", maThanhToan);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hoaDon = "Hóa đơn thanh toán\n" +
                                            "Mã thanh toán: " + reader["MA_THANH_TOAN_PHONG"] + "\n" +
                                            "Họ tên sinh viên: " + reader["HOTEN_SV"] + "\n" +
                                            "Tên phòng: " + reader["TEN_PHONG"] + "\n" +
                                            "Ngày thanh toán: " + reader["NGAY_THANH_TOAN"] + "\n" +
                                            "Giá tiền phòng: " + reader["GIA_TIEN"];

                            MessageBox.Show(hoaDon, "Hóa Đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btntatcasinhvien_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                   "TTP.MA_PHONG AS 'Mã phòng', " +
                                   "LP.TEN_LOAI_PHONG AS 'Tên loại phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "TTP.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        guna2DataGridView2.DataSource = dt;
                    }
                    UpdateSoluong2("1=1");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }



        private void Labelsoluong2_Click(object sender, EventArgs e)
        {
            UpdateSoluong2();
        }

        private void UpdateSoluong2(string whereCondition = "")
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Tính tổng số phòng
                    string totalQuery = "SELECT COUNT(*) FROM THANH_TOAN_PHONG";
                    SqlCommand totalCmd = new SqlCommand(totalQuery, conn);
                    int totalRooms = (int)totalCmd.ExecuteScalar();

                    // Tính số phòng thỏa mãn điều kiện lọc
                    string filterQuery = $@"
                SELECT COUNT(*) 
                FROM THANH_TOAN_PHONG TTP
                JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU
                JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                JOIN SINH_VIEN SV ON TTP.MSSV = SV.MSSV
                WHERE {whereCondition}";

                    SqlCommand filterCmd = new SqlCommand(filterQuery, conn);
                    int filteredRooms = (int)filterCmd.ExecuteScalar();

                    // Cập nhật Labelsoluong2
                    Labelsoluong2.Text = $"Số lượng: {filteredRooms}/{totalRooms}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật số lượng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btntracuu2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dn.MA_PHONG AS 'Mã phòng', p.TEN_PHONG AS 'Tên phòng', 
                                     dn.TIEN_DIEN AS 'Tiền điện', dn.TIEN_NUOC AS 'Tiền nước', 
                                     dn.TONG_TIEN AS 'Tổng tiền', 
                                     dn.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước', 
                                     dn.TINH_TRANG_TT AS 'Trạng thái thanh toán'
                              FROM DIEN_NUOC dn
                              JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG
                              WHERE dn.MA_PHONG = @maPhong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPhong", txtmaphongdiennuoc.Text.Trim());
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không tìm thấy phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtmaphongdiennuoc.Clear();
                                return;
                            }
                            guna2DataGridView2.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnxacnhanthanhtoan2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count > 0)
            {
                string maPhong = guna2DataGridView2.SelectedRows[0].Cells["Mã phòng"].Value.ToString();

                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        string updateQuery = "UPDATE DIEN_NUOC SET TINH_TRANG_TT = N'Đã thanh toán', NGAY_THANH_TOAN_DIEN_NUOC = GETDATE() WHERE MA_PHONG = @maPhong";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@maPhong", maPhong);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btntracuu2_Click(sender, e);
                        txtmaphongdiennuoc.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xác nhận thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnloaiphongnam2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dn.MA_PHONG AS 'Mã phòng', p.TEN_PHONG AS 'Tên phòng', lp.TEN_LOAI_PHONG AS 'Tên loại phòng',
                                     dn.TIEN_DIEN AS 'Tiền điện', dn.TIEN_NUOC AS 'Tiền nước', 
                                     dn.TONG_TIEN AS 'Tổng tiền', 
                                     dn.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước',
                                     dn.TINH_TRANG_TT AS 'Trạng thái thanh toán'
                              FROM DIEN_NUOC dn
                              JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG
                              JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG
                              WHERE lp.TEN_LOAI_PHONG = N'Nam'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            guna2DataGridView2.DataSource = dt;
                        }
                    }
                    string condition = "lp.TEN_LOAI_PHONG = N'Nam'";
                    UpdateSoluong3(condition);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnloaiphongnu2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dn.MA_PHONG AS 'Mã phòng', p.TEN_PHONG AS 'Tên phòng', lp.TEN_LOAI_PHONG AS 'Tên loại phòng',
                                     dn.TIEN_DIEN AS 'Tiền điện', dn.TIEN_NUOC AS 'Tiền nước', 
                                     dn.TONG_TIEN AS 'Tổng tiền', 
                                     dn.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước',
                                     dn.TINH_TRANG_TT AS 'Trạng thái thanh toán'
                              FROM DIEN_NUOC dn
                              JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG
                              JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG
                              WHERE lp.TEN_LOAI_PHONG = N'Nữ'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            guna2DataGridView2.DataSource = dt;
                        }
                    }
                    string condition = "lp.TEN_LOAI_PHONG = N'Nữ'";
                    UpdateSoluong3(condition);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btndathanhtoan2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dn.MA_PHONG AS 'Mã phòng', p.TEN_PHONG AS 'Tên phòng', lp.TEN_LOAI_PHONG AS 'Tên loại phòng',
                                     dn.TIEN_DIEN AS 'Tiền điện', dn.TIEN_NUOC AS 'Tiền nước', 
                                     dn.TONG_TIEN AS 'Tổng tiền', 
                                     dn.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước',
                                     dn.TINH_TRANG_TT AS 'Trạng thái thanh toán'
                              FROM DIEN_NUOC dn
                              JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG
                              JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG
                              WHERE dn.TINH_TRANG_TT = N'Đã thanh toán'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            guna2DataGridView2.DataSource = dt;
                        }
                        string condition = "dn.TINH_TRANG_TT = N'Đã thanh toán'";
                        UpdateSoluong3(condition);
                    }  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnchuathanhtoan2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dn.MA_PHONG AS 'Mã phòng', p.TEN_PHONG AS 'Tên phòng', lp.TEN_LOAI_PHONG AS 'Tên loại phòng',
                                     dn.TIEN_DIEN AS 'Tiền điện', dn.TIEN_NUOC AS 'Tiền nước', 
                                     dn.TONG_TIEN AS 'Tổng tiền', 
                                     dn.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước',
                                     dn.TINH_TRANG_TT AS 'Trạng thái thanh toán'
                              FROM DIEN_NUOC dn
                              JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG
                              JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG
                              WHERE dn.TINH_TRANG_TT = N'Chưa thanh toán'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            guna2DataGridView2.DataSource = dt;
                        }
                        string condition = "dn.TINH_TRANG_TT = N'Chưa thanh toán'";
                        UpdateSoluong3(condition);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btntatcaphong2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dn.MA_PHONG AS 'Mã phòng', p.TEN_PHONG AS 'Tên phòng', lp.TEN_LOAI_PHONG AS 'Tên loại phòng',
                                     dn.TIEN_DIEN AS 'Tiền điện', dn.TIEN_NUOC AS 'Tiền nước', 
                                     dn.TONG_TIEN AS 'Tổng tiền', 
                                     dn.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước',
                                     dn.TINH_TRANG_TT AS 'Trạng thái thanh toán'
                              FROM DIEN_NUOC dn
                              JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG
                              JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            guna2DataGridView2.DataSource = dt;
                        }
                        
                    }
                    UpdateSoluong3("1=1");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateSoluong3(string whereCondition2 = "")
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Tính tổng số phòng
                    string totalQuery2 = "SELECT COUNT(*) FROM DIEN_NUOC";
                    SqlCommand totalCmd2 = new SqlCommand(totalQuery2, conn);
                    int totalRooms2 = (int)totalCmd2.ExecuteScalar();

                    // Tính số phòng thỏa mãn điều kiện lọc
                    string filterQuery2 = $@"
            SELECT COUNT(*) 
            FROM DIEN_NUOC dn 
            JOIN PHONG p ON dn.MA_PHONG = p.MA_PHONG               
            JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG
            WHERE {whereCondition2}";

                    SqlCommand filterCmd2 = new SqlCommand(filterQuery2, conn);
                    int filteredRooms2 = (int)filterCmd2.ExecuteScalar();

                    // Cập nhật Labelsoluong3
                    Labelsoluong3.Text = $"Số lượng: {filteredRooms2}/{totalRooms2}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật số lượng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Labelsoluong3_Click(object sender, EventArgs e)
        {
            UpdateSoluong3();
        }
    }
}










