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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

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
                                "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                "NT.TRANG_THAI_NOI_TRU AS 'Trạng thái nội trú' ," +
                                "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán', " +
                                "SV.HOTEN_SV AS 'Họ tên', " +
                                "SV.MSSV AS 'MSSV', " +
                                "P.MA_PHONG AS 'Mã phòng', " +
                                "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng' " +
                                "FROM THANH_TOAN_PHONG TTP " +
                                "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                "JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV " +
                                "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG";
                    }
                    else
                    {
                        query = "SELECT TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', " +
                                "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                "NT.TRANG_THAI_NOI_TRU AS 'Trạng thái nội trú' ," +
                                "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán', " +
                                "SV.HOTEN_SV AS 'Họ tên', " +
                                "SV.MSSV AS 'MSSV', " +
                                "P.MA_PHONG AS 'Mã phòng', " +
                                "(DATEDIFF(MONTH, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU) * LP.GIA_PHONG) AS 'Giá tiền phòng' " +
                                "FROM THANH_TOAN_PHONG TTP " +
                                "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                "JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV " +
                                "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                "WHERE (@hoten IS NULL OR SV.HOTEN_SV = @hoten) " +
                                "AND (@mssv IS NULL OR SV.MSSV = @mssv)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (hoten.ToLower() != "all")
                            cmd.Parameters.AddWithValue("@hoten", string.IsNullOrEmpty(hoten) ? (object)DBNull.Value : hoten);
                        if (mssv.ToLower() != "all")
                            cmd.Parameters.AddWithValue("@mssv", string.IsNullOrEmpty(mssv) ? (object)DBNull.Value : mssv);

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
            string hoTenSinhVien = txthoten.Text.Trim();
            string maSoSinhVien = txtmasosinhvien.Text.Trim();

            if (string.IsNullOrEmpty(hoTenSinhVien) || string.IsNullOrEmpty(maSoSinhVien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Mã số sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string checkQuery = @"
                SELECT TRANG_THAI_NOI_TRU 
                FROM NOI_TRU 
                WHERE MSSV = @mssv AND TRANG_THAI_NOI_TRU = N'Đang nội trú'";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@mssv", maSoSinhVien);
                        object result = checkCmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Sinh viên không có trạng thái 'Đang nội trú', không thể xác nhận thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string updateQuery = @"
                UPDATE THANH_TOAN_PHONG 
                SET TRANG_THAI_THANH_TOAN = N'Đã thanh toán', NGAY_THANH_TOAN = GETDATE() 
                WHERE MA_NOI_TRU = (SELECT MA_NOI_TRU FROM NOI_TRU WHERE MSSV = @mssv)";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@mssv", maSoSinhVien);
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
                    string query = @"
                SELECT 
                    TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', 
                    NT.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    SV.HOTEN_SV AS 'Họ tên', 
                    NT.MSSV AS 'MSSV', 
                    TTP.MA_NOI_TRU AS 'Mã nội trú', 
                    TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', 
                    TTP.GIA_TIEN AS 'Giá tiền phòng', 
                    TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' 
                FROM THANH_TOAN_PHONG TTP
                JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU
                JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV
                JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE LP.TEN_LOAI_PHONG LIKE N'%Nam%'";

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
                    string query = @"
                SELECT 
                    TTP.MA_THANH_TOAN_PHONG AS 'Mã thanh toán phòng', 
                    NT.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    SV.HOTEN_SV AS 'Họ tên', 
                    NT.MSSV AS 'MSSV', 
                    TTP.MA_NOI_TRU AS 'Mã nội trú', 
                    TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', 
                    TTP.GIA_TIEN AS 'Giá tiền phòng', 
                    TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' 
                FROM THANH_TOAN_PHONG TTP
                JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU
                JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV
                JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE LP.TEN_LOAI_PHONG LIKE N'%Nữ%'";

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
                                   "NT.MA_PHONG AS 'Mã phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "SV.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "TTP.GIA_TIEN AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV " +
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
                                   "NT.MA_PHONG AS 'Mã phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "SV.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "TTP.GIA_TIEN AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV " +
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
                                   "NT.MA_PHONG AS 'Mã phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "NT.MSSV AS 'MSSV', " +
                                   "TTP.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "TTP.GIA_TIEN AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán phòng', " +
                                   "DN.TIEN_DIEN AS 'Tiền điện', "+
                                   "DN.TIEN_NUOC AS 'Tiền nước', "+
                                   "DN.TINH_TRANG_TT AS 'Trạng thái thanh toán điện nước', "+
                                   "DN.NGAY_THANH_TOAN_DIEN_NUOC AS 'Ngày thanh toán điện nước', "+
                                   "T.TEN_TANG AS 'Tên tầng' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV " +
                                   "JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG " +
                                   "JOIN DIEN_NUOC DN ON P.MA_PHONG = DN.MA_PHONG "+
                                   "JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG " +
                                   "JOIN TANG T ON P.MA_TANG = T.MA_TANG " +
                                   "WHERE (@tenTang = '' OR T.TEN_TANG = @tenTang) " +
                                   "AND (@maPhong = '' OR NT.MA_PHONG = @maPhong)";

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

        public static string RemoveDiacritics(string input)
        {
            string[] arr1 = new string[] { "a", "e", "i", "o", "u", "d", "A", "E", "I", "O", "U", "D" };
            string[] arr2 = new string[] { "á", "é", "í", "ó", "ú", "đ", "Á", "É", "Í", "Ó", "Ú", "Đ" };

            for (int i = 0; i < arr2.Length; i++)
            {
                input = input.Replace(arr2[i], arr1[i]);
            }
            input = input.Normalize(NormalizationForm.FormD);
            input = Regex.Replace(input, @"[^a-zA-Z0-9\s]", "");

            return input;
        }

        private void btninhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                string maThanhToan = txtmathanhtoan.Text.Trim();

                if (string.IsNullOrEmpty(maThanhToan))
                {
                    MessageBox.Show("Vui lòng nhập mã thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dt = new DataTable();
                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    conn.Open();
                    string query = @"
                     SELECT 
                     tp.MA_THANH_TOAN_PHONG, sv.MSSV, sv.HOTEN_SV, p.TEN_PHONG, 
                     tp.GIA_TIEN, dn.TIEN_DIEN, dn.TIEN_NUOC, tp.NGAY_THANH_TOAN, tp.TRANG_THAI_THANH_TOAN
                     FROM THANH_TOAN_PHONG tp
                     JOIN NOI_TRU nt ON tp.MA_NOI_TRU = nt.MA_NOI_TRU
                     JOIN SINH_VIEN sv ON nt.MSSV = sv.MSSV
                     JOIN PHONG p ON nt.MA_PHONG = p.MA_PHONG
                     JOIN LOAI_PHONG lp ON p.MA_LOAI_PHONG = lp.MA_LOAI_PHONG
                     JOIN DIEN_NUOC dn ON p.MA_PHONG = dn.MA_PHONG
                     WHERE tp.MA_THANH_TOAN_PHONG = @maThanhToan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThanhToan", maThanhToan);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = dt.Rows[0];
                var trangThaiThanhToan = row["TRANG_THAI_THANH_TOAN"].ToString();

                if (trangThaiThanhToan != "Đã thanh toán")
                {
                    MessageBox.Show("Không thể in hóa đơn do sinh viên chưa thực hiện thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string mssv = RemoveDiacritics(row["MSSV"].ToString());
                string hoten = RemoveDiacritics(row["HOTEN_SV"].ToString());
                string tenPhong = RemoveDiacritics(row["TEN_PHONG"].ToString());
                decimal giaTien = Convert.ToDecimal(row["GIA_TIEN"]);
                decimal tienDien = Convert.ToDecimal(row["TIEN_DIEN"]);
                decimal tienNuoc = Convert.ToDecimal(row["TIEN_NUOC"]);
                decimal tongTien = giaTien + tienDien + tienNuoc;
                string ngayThanhToan = Convert.ToDateTime(row["NGAY_THANH_TOAN"]).ToString("dd/MM/yyyy");

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Lưu hóa đơn";
                saveFileDialog.FileName = "PhieuThu.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    document.Open();

                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialuni.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font titleFont = FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 16);
                    iTextSharp.text.Font normalFont = FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12);

                    document.Add(new Paragraph("TRUONG DAI HOC NAM CAN THO", titleFont));
                    document.Add(new Paragraph("168 Nguyen Van Cu noi dai, Quan Ninh Kieu, TP.Can Tho", normalFont));
                    document.Add(new Paragraph("\n                                           PHIEU THU", titleFont));
                    document.Add(new Paragraph($"Ma thanh toan: {maThanhToan}\n", normalFont));
                    document.Add(new Paragraph("_____________________________________________", titleFont));

                    document.Add(new Paragraph($"MSSV: {mssv}", normalFont));
                    document.Add(new Paragraph($"Ho ten SV: {hoten}", normalFont));
                    document.Add(new Paragraph($"Phong: {tenPhong}\n", normalFont));

                    document.Add(new Paragraph($"Gia tien phong: {giaTien:N0} VND", normalFont));
                    document.Add(new Paragraph($"Tien dien: {tienDien:N0} VND", normalFont));
                    document.Add(new Paragraph($"Tien nuoc: {tienNuoc:N0} VND", normalFont));
                    document.Add(new Paragraph("_____________________________________________", titleFont));
                    document.Add(new Paragraph($"\nTong tien: {tongTien:N0} VND", normalFont));
                    document.Add(new Paragraph($"\n                                           Ngay thanh toan: {ngayThanhToan}\n", normalFont));

                    document.Add(new Paragraph("\n                                 ", titleFont));
                    document.Add(new Paragraph("*Chu ky cua:", normalFont));
                    document.Add(new Paragraph("\nNguoi lap phieu\t\t               Nguoi nop tien\t\t           Thu quy", normalFont));
                    document.Add(new Paragraph("\n                                 ", titleFont));
                    document.Add(new Paragraph("\n\n\nKe toan truong\t\t             Hieu truong", normalFont));

                    document.Close();
                    MessageBox.Show("In hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                   "NT.MA_PHONG AS 'Mã phòng', " +
                                   "LP.TEN_LOAI_PHONG AS 'Tên loại phòng', " +
                                   "SV.HOTEN_SV AS 'Họ tên', " +
                                   "SV.MSSV AS 'MSSV', " +
                                   "NT.MA_NOI_TRU AS 'Mã nội trú', " +
                                   "NT.TRANG_THAI_NOI_TRU AS 'Trạng thái nội trú' ,"+
                                   "TTP.NGAY_THANH_TOAN AS 'Ngày thanh toán phòng', " +
                                   "TTP.GIA_TIEN AS 'Giá tiền phòng', " +
                                   "TTP.TRANG_THAI_THANH_TOAN AS 'Trạng thái thanh toán' " +
                                   "FROM THANH_TOAN_PHONG TTP " +
                                   "JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU " +
                                   "JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV " +
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
                    string totalQuery = "SELECT COUNT(*) FROM THANH_TOAN_PHONG";
                    SqlCommand totalCmd = new SqlCommand(totalQuery, conn);
                    int totalRooms = (int)totalCmd.ExecuteScalar();

                    string filterQuery = @"
                SELECT COUNT(*) 
                FROM THANH_TOAN_PHONG TTP
                JOIN NOI_TRU NT ON TTP.MA_NOI_TRU = NT.MA_NOI_TRU
                JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV";

                    if (!string.IsNullOrWhiteSpace(whereCondition))
                    {
                        filterQuery += " WHERE " + whereCondition;
                    }

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










