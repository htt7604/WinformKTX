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
using Guna.UI2.WinForms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinformKTX
{
    public partial class QuanLiPhong : Form
    {

        //private string connectionString = "Data Source=LAPTOP-5VTLAM86\\SQLEXPRESS;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        KetnoiCSDL ketnoi = new KetnoiCSDL();



        public QuanLiPhong()
        {
            InitializeComponent();

        //private string connectionString = "Data Source=LAPTOP-5VTLAM86\\SQLEXPRESS;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        KetnoiCSDL ketnoi = new KetnoiCSDL();
        public QuanLiPhong()
        {
            InitializeComponent();

        private string connectionString = "Data Source=LAPTOP-5VTLAM86\\SQLEXPRESS;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


        public QuanLiPhong()
        {
            InitializeComponent();

            LoadData();
        }

        private void btntimkiemphongg_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void QuanLiPhong_Load(object sender, EventArgs e)
        {

        }


        private void btnthemphong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmaphong.Text) ||
                string.IsNullOrWhiteSpace(txtsotang.Text) ||
                string.IsNullOrWhiteSpace(txtsogiuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int maPhong;
            if (!int.TryParse(txtmaphong.Text, out maPhong))
            {
                MessageBox.Show("Mã phòng phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int soTang = int.Parse(txtsotang.Text);
            int soGiuong = int.Parse(txtsogiuong.Text);
            string tinhTrangPhong = "Chưa cập nhật";
            string tenPhong = $"{maPhong}T{soTang}";


            using (SqlConnection conn = ketnoi.GetConnection())
            {
                string getLoaiPhongQuery = "SELECT MA_LOAI_PHONG FROM TANG WHERE MA_TANG = @MA_TANG";
                SqlCommand getLoaiCmd = new SqlCommand(getLoaiPhongQuery, conn);
                getLoaiCmd.Parameters.AddWithValue("@MA_TANG", soTang);

                try
                {
                    conn.Open();
                    object result = getLoaiCmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Không tìm thấy loại phòng cho tầng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int maLoaiPhong = Convert.ToInt32(result);

                    string checkMaPhongQuery = "SELECT COUNT(*) FROM PHONG WHERE MA_PHONG = @MA_PHONG";
                    SqlCommand checkMaPhongCmd = new SqlCommand(checkMaPhongQuery, conn);
                    checkMaPhongCmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                    int maPhongCount = (int)checkMaPhongCmd.ExecuteScalar();

                    if (maPhongCount > 0)
                    {
                        MessageBox.Show("Mã phòng đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string insertQuery = "INSERT INTO PHONG (MA_PHONG, MA_TANG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, MA_LOAI_PHONG, TINH_TRANG_PHONG, TEN_PHONG) " +
                                         "VALUES (@MA_PHONG, @MA_TANG, @SO_GIUONG_TOI_DA, @SO_GIUONG_CON_TRONG, @MA_LOAI_PHONG, @TINH_TRANG_PHONG, @TEN_PHONG)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                    insertCmd.Parameters.AddWithValue("@MA_TANG", soTang);
                    insertCmd.Parameters.AddWithValue("@SO_GIUONG_TOI_DA", soGiuong);
                    insertCmd.Parameters.AddWithValue("@SO_GIUONG_CON_TRONG", soGiuong);
                    insertCmd.Parameters.AddWithValue("@MA_LOAI_PHONG", maLoaiPhong);
                    insertCmd.Parameters.AddWithValue("@TINH_TRANG_PHONG", tinhTrangPhong);
                    insertCmd.Parameters.AddWithValue("@TEN_PHONG", tenPhong);

                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Đã thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                    txtmaphong.Clear();
                    txtsotang.Clear();
                    txtsogiuong.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadData()
        {

            using (SqlConnection conn = ketnoi.GetConnection())


            using (SqlConnection conn = ketnoi.GetConnection())



            {
                string query = "SELECT MA_PHONG, MA_LOAI_PHONG, MA_TANG, TEN_PHONG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, TINH_TRANG_PHONG FROM PHONG";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGridView1.DataSource = dt;

                DataGridView1.Columns["MA_PHONG"].HeaderText = "Mã phòng";
                DataGridView1.Columns["MA_LOAI_PHONG"].HeaderText = "Mã loại phòng";
                DataGridView1.Columns["MA_TANG"].HeaderText = "Mã tầng";
                DataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên phòng";
                DataGridView1.Columns["SO_GIUONG_TOI_DA"].HeaderText = "Số giường tối đa";
                DataGridView1.Columns["SO_GIUONG_CON_TRONG"].HeaderText = "Số giường còn trống";
                DataGridView1.Columns["TINH_TRANG_PHONG"].HeaderText = "Tình trạng phòng";
            }
        }




        private void btncapnhat_Click(object sender, EventArgs e)
        {
            string tenPhong = txtmasophong2.Text.Trim();
            if (string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Vui lòng nhập tên phòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tinhTrangPhong = radkichhoat.Checked ? "Trống" : "Không thể sử dụng";
            int soGiuongConTrong;
            if (!int.TryParse(txtgiuongcontrong.Text, out soGiuongConTrong) || soGiuongConTrong < 0)
            {
                MessageBox.Show("Vui lòng nhập số giường còn trống hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            using (SqlConnection conn = ketnoi.GetConnection())


            using (SqlConnection conn = ketnoi.GetConnection())




            {
                string updateQuery = "UPDATE PHONG SET TINH_TRANG_PHONG = @TinhTrang, SO_GIUONG_CON_TRONG = @SoGiuongConTrong WHERE TEN_PHONG = @TenPhong";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TenPhong", tenPhong);
                    cmd.Parameters.AddWithValue("@TinhTrang", tinhTrangPhong);
                    cmd.Parameters.AddWithValue("@SoGiuongConTrong", soGiuongConTrong);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Phòng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        txtmasophong2.Clear();
                        txtgiuongcontrong.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string tenPhong = txtmasophong1.Text.Trim();
            string maTang = txtsotang1.Text.Trim();


            using (SqlConnection conn = ketnoi.GetConnection())
            using (SqlConnection conn = ketnoi.GetConnection())


            {
                string query;

                if (tenPhong.ToLower() == "all" && string.IsNullOrEmpty(maTang))
                {
                    query = "SELECT MA_PHONG, MA_LOAI_PHONG, MA_TANG, TEN_PHONG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, TINH_TRANG_PHONG FROM PHONG";
                }
                else if (!string.IsNullOrEmpty(maTang)) // Nếu nhập số tầng, tìm theo tầng
                {
                    query = "SELECT MA_PHONG, MA_LOAI_PHONG, MA_TANG, TEN_PHONG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, TINH_TRANG_PHONG " +
                            "FROM PHONG WHERE MA_TANG = @MaTang";
                }
                else // Nếu nhập tên phòng, tìm theo tên phòng
                {
                    query = "SELECT MA_PHONG, MA_LOAI_PHONG, MA_TANG, TEN_PHONG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, TINH_TRANG_PHONG " +
                            "FROM PHONG WHERE TEN_PHONG = @TenPhong";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(maTang))
                    {
                        cmd.Parameters.AddWithValue("@MaTang", maTang);
                    }
                    else if (tenPhong.ToLower() != "all")
                    {
                        cmd.Parameters.AddWithValue("@TenPhong", tenPhong);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    txtmasophong1.Clear();
                    txtsotang1.Clear();

                    try
                    {
                        conn.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btnxoaphong_Click(object sender, EventArgs e)
        {
            string tenPhong = txtmasophong1.Text.Trim();
            if (string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Vui lòng nhập tên phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            using (SqlConnection conn = ketnoi.GetConnection())


            using (SqlConnection conn = ketnoi.GetConnection())



            {
                string checkQuery = "SELECT COUNT(*) FROM PHONG WHERE TEN_PHONG = @TenPhong";
                string deleteQuery = "DELETE FROM PHONG WHERE TEN_PHONG = @TenPhong";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@TenPhong", tenPhong);

                    try
                    {
                        conn.Open();
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                            {
                                deleteCmd.Parameters.AddWithValue("@TenPhong", tenPhong);
                                deleteCmd.ExecuteNonQuery();
                                MessageBox.Show("Đã xóa phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DataGridView1.DataSource = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Phòng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        txtmasophong1.Clear();
                        txtsotang1.Clear();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void txtsotang_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnloaiphongnam2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    P.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình trạng phòng'
                FROM PHONG P
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE LP.TEN_LOAI_PHONG LIKE N'%Nam%'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có loại phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateSoluong("AND LP.TEN_LOAI_PHONG LIKE N'%Nam%'");
            }
        }

        private void btnloaiphongnu2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    P.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'Số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình trạng phòng'
                FROM PHONG P
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE LP.TEN_LOAI_PHONG LIKE N'%Nữ%'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có loại phòng nam nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateSoluong("AND LP.TEN_LOAI_PHONG LIKE N'%Nữ%'");
            }
        }

        private void btngiuongcontrong_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    P.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'Số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình trạng phòng'
                FROM PHONG P
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE P.SO_GIUONG_CON_TRONG IS NOT NULL AND P.SO_GIUONG_CON_TRONG > 0";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có phòng nào còn giường trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Cập nhật số lượng sau khi lọc
            UpdateSoluong("AND P.SO_GIUONG_CON_TRONG > 0");
        }


        private void btnphongtrong_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection()) // Thay bằng phương thức kết nối thực tế của bạn
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                   P.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'Số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình Trạng Phòng'
                FROM PHONG P
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE P.TINH_TRANG_PHONG = N'Trống'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có phòng nào đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateSoluong("AND P.TINH_TRANG_PHONG LIKE N'%Trống%'");
            }
        }


        private void btnphongchuacapnhat_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection()) // Thay bằng phương thức kết nối thực tế của bạn
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                   P.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'Số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình Trạng Phòng'
                FROM PHONG P
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE P.TINH_TRANG_PHONG = N'Chưa cập nhật'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có phòng nào đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateSoluong("AND P.TINH_TRANG_PHONG LIKE N'%Chưa cập nhật%'");
            }
        }

        private void btnphongkhongthesudung_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection()) // Thay bằng phương thức kết nối thực tế của bạn
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                   P.MA_PHONG AS 'Mã phòng', 
                    LP.TEN_LOAI_PHONG AS 'Tên loại phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'Số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình Trạng Phòng'
                FROM PHONG P
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                WHERE P.TINH_TRANG_PHONG = N'Không thể sử dụng'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có phòng nào đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateSoluong("AND P.TINH_TRANG_PHONG LIKE N'%Không thể sử dụng%'");
            }
        }

        private void Labelsoluong_Click(object sender, EventArgs e)
        {
            UpdateSoluong();
        }

        private void btntatcaphong_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection()) // Thay bằng phương thức kết nối thực tế của bạn
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                   P.MA_PHONG AS 'Mã phòng', 
                    P.MA_LOAI_PHONG AS 'Mã loại phòng', 
                    P.MA_TANG AS 'Mã tầng', 
                    P.TEN_PHONG AS 'Tên phòng', 
                    P.SO_GIUONG_TOI_DA AS 'Số giường tối đa', 
                    P.SO_GIUONG_CON_TRONG AS 'Số giường còn trống', 
                    P.TINH_TRANG_PHONG AS 'Tình Trạng Phòng'
                FROM PHONG P";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có phòng nào đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateSoluong();
            }
        }
        private void UpdateSoluong(string filterCondition = "")
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Đếm tổng số phòng trong bảng PHONG
                    string totalQuery = "SELECT COUNT(*) FROM PHONG";

                    // Đếm số phòng theo điều kiện lọc (nếu có)
                    string filteredQuery = $@"
                SELECT COUNT(*) 
                FROM PHONG P 
                JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG 
                WHERE 1=1 {filterCondition}";

                    int totalRooms = 0;
                    int filteredRooms = 0;

                    using (SqlCommand cmd = new SqlCommand(totalQuery, conn))
                    {
                        totalRooms = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    using (SqlCommand cmd = new SqlCommand(filteredQuery, conn))
                    {
                        filteredRooms = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    Labelsoluong.Text = $"Số lượng: {filteredRooms}/{totalRooms}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật số lượng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
  

   
