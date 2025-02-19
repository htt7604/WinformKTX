using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinformKTX
{
    public partial class ThayDoiPhong : Form
    {
        private string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=WinFormKTX;Integrated Security=True";

        public ThayDoiPhong()
        {
            InitializeComponent();
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        //private void btnExist_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void btnMinisize_Click(object sender, EventArgs e)
        //{
        //    if (this.WindowState == FormWindowState.Maximized)
        //        this.WindowState = FormWindowState.Normal;
        //    else
        //        this.WindowState = FormWindowState.Maximized;
        //}

        //private void btnThumanhinh_Click(object sender, EventArgs e)
        //{
        //    this.WindowState = FormWindowState.Minimized;
        //}

        private void ThayDoiPhong_Load(object sender, EventArgs e)
        {
            this.Location = new Point(335, 130);
        }

        // 1. Tìm kiếm thông tin nội trú của sinh viên theo MSSV
        private void btnTimkiemnoitru_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                string query = @"
                    SELECT SV.MSSV, SV.HOTEN_SV, P.TEN_PHONG, T.TEN_TANG, LP.TEN_LOAI_PHONG, G.TEN_GIUONG
                    FROM NOI_TRU NT
                    INNER JOIN SINH_VIEN SV ON NT.MSSV = SV.MSSV
                    INNER JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG
                    INNER JOIN TANG T ON P.MA_TANG = T.MA_TANG
                    INNER JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                    INNER JOIN GIUONG G ON NT.MA_GIUONG = G.MA_GIUONG
                    WHERE SV.MSSV = @MSSV";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    string loaiPhong = dt.Rows[0]["TEN_LOAI_PHONG"].ToString();
                    txtTang.Items.Clear();
                    if (loaiPhong == "Nam")
                    {
                        txtTang.Items.Add("T3");
                        txtTang.Items.Add("T4");
                    }
                    else if (loaiPhong == "Nữ")
                    {
                        txtTang.Items.Add("T1");
                        txtTang.Items.Add("T2");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nội trú của sinh viên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // 2. Lọc danh sách phòng trống theo tầng đã chọn
        private void txtTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                string query = @"
                    SELECT TEN_PHONG FROM PHONG 
                    WHERE SO_GIUONG_CON_TRONG > 0 
                    AND MA_TANG = (SELECT MA_TANG FROM TANG WHERE TEN_TANG = @TEN_TANG)";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TEN_TANG", txtTang.SelectedItem.ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtTenphong.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    txtTenphong.Items.Add(row["TEN_PHONG"].ToString());
                }
            }
        }

        // 3. Lọc danh sách giường trống theo phòng đã chọn
        private void txtTenphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTenphong.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                string query = @"
                    SELECT TEN_GIUONG FROM GIUONG 
                    WHERE TINH_TRANG_GIUONG = 'trống' 
                    AND MA_PHONG = (SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TEN_PHONG)";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);

                ComboBoxSogiuong.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ComboBoxSogiuong.Items.Add(row["TEN_GIUONG"].ToString());
                    }
                    ComboBoxSogiuong.SelectedIndex = 0; // Chọn mục đầu tiên
                }
                else
                {
                    MessageBox.Show("Không có giường trống trong phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // 4. Lưu thay đổi phòng vào SQL
        private void btnLuuthaydoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Lấy mã phòng mới
                    string queryGetPhong = "SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TEN_PHONG";
                    SqlCommand cmdPhong = new SqlCommand(queryGetPhong, conn, transaction);
                    cmdPhong.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
                    int maPhongMoi = (int)cmdPhong.ExecuteScalar();

                    // Lấy mã giường mới
                    string queryGetGiuong = "SELECT MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG AND MA_PHONG = @MA_PHONG";
                    SqlCommand cmdGiuong = new SqlCommand(queryGetGiuong, conn, transaction);
                    cmdGiuong.Parameters.AddWithValue("@TEN_GIUONG", ComboBoxSogiuong.SelectedItem.ToString());
                    cmdGiuong.Parameters.AddWithValue("@MA_PHONG", maPhongMoi);
                    int maGiuongMoi = (int)cmdGiuong.ExecuteScalar();

                    // Cập nhật thông tin nội trú
                    string queryUpdateNoiTru = @"
                        UPDATE NOI_TRU SET MA_PHONG = @MA_PHONG, MA_GIUONG = @MA_GIUONG 
                        WHERE MSSV = @MSSV";
                    SqlCommand cmdUpdate = new SqlCommand(queryUpdateNoiTru, conn, transaction);
                    cmdUpdate.Parameters.AddWithValue("@MA_PHONG", maPhongMoi);
                    cmdUpdate.Parameters.AddWithValue("@MA_GIUONG", maGiuongMoi);
                    cmdUpdate.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                    cmdUpdate.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnTimkiemnoitru.PerformClick(); // Refresh dữ liệu
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
