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
    public partial class QuanLiPhong : Form
    {


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
            if (string.IsNullOrWhiteSpace(txtsotang.Text) ||
                string.IsNullOrWhiteSpace(txtsogiuong.Text) ||
                (!radnam.Checked && !radnu.Checked))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int soTang = int.Parse(txtsotang.Text);
            int soGiuong = int.Parse(txtsogiuong.Text);
            int maLoaiPhong = radnam.Checked ? 1 : 2;
            string tinhTrangPhong = "Chưa cập nhật"; 


            using (SqlConnection conn = ketnoi.GetConnection())
            {
                string checkLoaiPhongQuery = "SELECT COUNT(*) FROM LOAI_PHONG WHERE MA_LOAI_PHONG = @MA_LOAI_PHONG";
                SqlCommand checkCmd = new SqlCommand(checkLoaiPhongQuery, conn);
                checkCmd.Parameters.AddWithValue("@MA_LOAI_PHONG", maLoaiPhong);

                try
                {
                    conn.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Loại phòng không tồn tại trong bảng LOAI_PHONG!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string insertQuery = "INSERT INTO PHONG (MA_TANG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, MA_LOAI_PHONG, TINH_TRANG_PHONG) " +
                                         "VALUES (@MA_TANG, @SO_GIUONG_TOI_DA, @SO_GIUONG_CON_TRONG, @MA_LOAI_PHONG, @TINH_TRANG_PHONG); " +
                                         "SELECT SCOPE_IDENTITY();";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@MA_TANG", soTang);
                    insertCmd.Parameters.AddWithValue("@SO_GIUONG_TOI_DA", soGiuong);
                    insertCmd.Parameters.AddWithValue("@SO_GIUONG_CON_TRONG", soGiuong); // Bằng SO_GIUONG_TOI_DA
                    insertCmd.Parameters.AddWithValue("@MA_LOAI_PHONG", maLoaiPhong);
                    insertCmd.Parameters.AddWithValue("@TINH_TRANG_PHONG", tinhTrangPhong); // Gán tình trạng

                    int maPhong = Convert.ToInt32(insertCmd.ExecuteScalar());

                    string tenPhong = $"{maPhong}T{soTang}";
                    string updateQuery = "UPDATE PHONG SET TEN_PHONG = @TEN_PHONG WHERE MA_PHONG = @MA_PHONG";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@TEN_PHONG", tenPhong);
                    updateCmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Đã thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); 
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

            string tinhTrangPhong = radkichhoat.Checked ? "Có thể sử dụng" : "Không thể sử dụng";


            using (SqlConnection conn = ketnoi.GetConnection())



            {
                string updateQuery = "UPDATE PHONG SET TINH_TRANG_PHONG = @TinhTrang WHERE TEN_PHONG = @TenPhong";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TenPhong", tenPhong);
                    cmd.Parameters.AddWithValue("@TinhTrang", tinhTrangPhong);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Phòng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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


            using (SqlConnection conn = ketnoi.GetConnection())


            {
                string query;
                if (tenPhong.ToLower() == "all")
                {
                    query = "SELECT MA_PHONG, MA_LOAI_PHONG, MA_TANG, TEN_PHONG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, TINH_TRANG_PHONG FROM PHONG";
                }
                else
                {
                    query = "SELECT MA_PHONG, MA_LOAI_PHONG, MA_TANG, TEN_PHONG, SO_GIUONG_TOI_DA, SO_GIUONG_CON_TRONG, TINH_TRANG_PHONG " +
                            "FROM PHONG WHERE TEN_PHONG = @TenPhong";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (tenPhong.ToLower() != "all")
                    {
                        cmd.Parameters.AddWithValue("@TenPhong", tenPhong);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

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
                            MessageBox.Show("Phòng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

   
