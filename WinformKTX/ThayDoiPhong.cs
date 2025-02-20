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

            txtTang.SelectedIndexChanged += TxtTang_SelectedIndexChanged;
            txtTenphong.SelectedIndexChanged += TxtTenphong_SelectedIndexChanged;
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

        //thêm
        private void TxtTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTang.SelectedValue == null) { return; }
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT MA_PHONG, TEN_PHONG 
                FROM PHONG 
                WHERE MA_TANG = @MaTang 
                AND SO_GIUONG_CON_TRONG > 0";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //if (comboBoxMaTang.SelectedValue == null)
                    //{
                    //    MessageBox.Show("Vui lòng chọn tầng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    // Lấy giá trị thực tế từ DataRowView nếu cần
                    var selectedValue = txtTang.SelectedValue;
                    if (selectedValue is DataRowView rowView)
                    {
                        selectedValue = rowView["MA_TANG"];
                    }

                    // Chuyển thành kiểu số nguyên trước khi truyền vào SQL
                    cmd.Parameters.AddWithValue("@MaTang", Convert.ToInt32(selectedValue));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count == 0)
                        {
                            txtTenphong.DataSource = null;
                            MessageBox.Show("Không có phòng nào còn giường trống trong tầng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        txtTenphong.DataSource = table;
                        txtTenphong.DisplayMember = "TEN_PHONG";
                        txtTenphong.ValueMember = "MA_PHONG";
                        txtTenphong.SelectedIndex = -1;

                        ComboBoxSogiuong.DataSource = null; // Xóa danh sách giường khi thay đổi tầng
                    }
                }
            }

        }

        private void TxtTenphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTenphong.SelectedValue == null)
            {
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();

                // Lấy mã phòng được chọn
                object selectedValue = txtTenphong.SelectedValue;
                string selectedMaPhong = selectedValue is DataRowView dataRowView ? dataRowView["MA_PHONG"].ToString() : selectedValue.ToString();

                if (!int.TryParse(selectedMaPhong, out int maPhong))
                {
                    MessageBox.Show("Mã phòng không hợp lệ.");
                    return;
                }



                // Load danh sách giường còn trống hoặc giường hiện tại của sinh viên
                string queryGiuong = @"
                        SELECT MA_GIUONG, TEN_GIUONG
                        FROM GIUONG 
                        WHERE MA_PHONG = @MaPhong 
                        AND TINH_TRANG_GIUONG = N'Trống'"; // Giữ giường hiện tại nếu có

                using (SqlCommand cmd = new SqlCommand(queryGiuong, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        //if (table.Rows.Count == 0)
                        //{
                        //    comboBoxMaGiuong.DataSource = null;
                        //    MessageBox.Show("Không có giường nào trống trong phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}

                        ComboBoxSogiuong.DataSource = table;
                        ComboBoxSogiuong.DisplayMember = "TEN_GIUONG";
                        ComboBoxSogiuong.ValueMember = "MA_GIUONG";

                        //// Nếu sinh viên đã có giường, đặt lại giá trị
                        //if (!string.IsNullOrEmpty(maGiuongHienTai))
                        //{
                        //    comboBoxMaGiuong.SelectedValue = maGiuongHienTai;
                        //}
                        //else
                        //{
                        //    comboBoxMaGiuong.SelectedIndex = -1;
                        //}
                    }
                }
            }
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
                        //txtTang.Items.Add("T3");
                        //txtTang.Items.Add("T4");
                        LoadTangFromMasinhvien();
                    }
                    else if (loaiPhong == "Nữ")
                    {
                        //txtTang.Items.Add("T1");
                        //txtTang.Items.Add("T2");
                        LoadTangFromMasinhvien();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nội trú của sinh viên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadTangFromMasinhvien()
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                string query = @"
            SELECT DISTINCT TANG.MA_TANG, TANG.TEN_TANG
            FROM NOI_TRU
            JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
            JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
            WHERE NOI_TRU.MSSV = @MSSV";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtTang.DataSource = dt;
                txtTang.DisplayMember = "TEN_TANG"; // Hiển thị TEN_TANG
                txtTang.ValueMember = "MA_TANG";    // Giá trị thực tế là MA_TANG
            }
        }



        // 2. Lọc danh sách phòng trống theo tầng đã chọn
        private void txtTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //using (SqlConnection conn = kn.GetConnection())
            //{
            //    string query = @"
            //        SELECT TEN_PHONG FROM PHONG
            //        WHERE SO_GIUONG_CON_TRONG > 0
            //        AND MA_TANG = (SELECT MA_TANG FROM TANG WHERE TEN_TANG = @TEN_TANG)";

            //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
            //    da.SelectCommand.Parameters.AddWithValue("@TEN_TANG", txtTang.SelectedItem.ToString());
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    txtTenphong.Items.Clear();
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        txtTenphong.Items.Add(row["TEN_PHONG"].ToString());

            //    }
            //}

            if (txtTang.SelectedValue == null) { return; }
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT MA_PHONG, TEN_PHONG 
                FROM PHONG 
                WHERE MA_TANG = @MaTang 
                AND SO_GIUONG_CON_TRONG > 0";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //if (comboBoxMaTang.SelectedValue == null)
                    //{
                    //    MessageBox.Show("Vui lòng chọn tầng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    // Lấy giá trị thực tế từ DataRowView nếu cần
                    var selectedValue = txtTang.SelectedValue;
                    if (selectedValue is DataRowView rowView)
                    {
                        selectedValue = rowView["MA_TANG"];
                    }

                    // Chuyển thành kiểu số nguyên trước khi truyền vào SQL
                    cmd.Parameters.AddWithValue("@MaTang", Convert.ToInt32(selectedValue));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count == 0)
                        {
                            txtTenphong.DataSource = null;
                            MessageBox.Show("Không có phòng nào còn giường trống trong tầng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        txtTenphong.DataSource = table;
                        txtTenphong.DisplayMember = "TEN_PHONG";
                        txtTenphong.ValueMember = "MA_PHONG";
                        txtTenphong.SelectedIndex = -1;

                        ComboBoxSogiuong.DataSource = null; // Xóa danh sách giường khi thay đổi tầng
                    }
                }
            }
        }

        // 3. Lọc danh sách giường trống theo phòng đã chọn
        //private void txtTenphong_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (txtTenphong.SelectedItem == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn một phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    using (SqlConnection conn = kn.GetConnection())
        //    {
        //        string query = @"
        //            SELECT TEN_GIUONG FROM GIUONG 
        //            WHERE TINH_TRANG_GIUONG = 'trống' 
        //            AND MA_PHONG = (SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TEN_PHONG)";

        //        SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //        da.SelectCommand.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        ComboBoxSogiuong.Items.Clear();
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                ComboBoxSogiuong.Items.Add(row["TEN_GIUONG"].ToString());
        //            }
        //            ComboBoxSogiuong.SelectedIndex = 0; // Chọn mục đầu tiên
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không có giường trống trong phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //}

        // 4. Lưu thay đổi phòng vào SQL
        //private void btnLuuthaydoi_Click(object sender, EventArgs e)
        //{
        //    using (SqlConnection conn = kn.GetConnection())
        //    {
        //        conn.Open();
        //        SqlTransaction transaction = conn.BeginTransaction();

        //        try
        //        {
        //            // Lấy mã phòng mới
        //            string queryGetPhong = "SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TEN_PHONG";
        //            SqlCommand cmdPhong = new SqlCommand(queryGetPhong, conn, transaction);
        //            cmdPhong.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
        //            int maPhongMoi = (int)cmdPhong.ExecuteScalar();

        //            // Lấy mã giường mới
        //            string queryGetGiuong = "SELECT MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG AND MA_PHONG = @MA_PHONG";
        //            SqlCommand cmdGiuong = new SqlCommand(queryGetGiuong, conn, transaction);
        //            cmdGiuong.Parameters.AddWithValue("@TEN_GIUONG", ComboBoxSogiuong.SelectedItem.ToString());
        //            cmdGiuong.Parameters.AddWithValue("@MA_PHONG", maPhongMoi);
        //            int maGiuongMoi = (int)cmdGiuong.ExecuteScalar();

        //            // Cập nhật thông tin nội trú
        //            string queryUpdateNoiTru = @"
        //                UPDATE NOI_TRU SET MA_PHONG = @MA_PHONG, MA_GIUONG = @MA_GIUONG 
        //                WHERE MSSV = @MSSV";
        //            SqlCommand cmdUpdate = new SqlCommand(queryUpdateNoiTru, conn, transaction);
        //            cmdUpdate.Parameters.AddWithValue("@MA_PHONG", maPhongMoi);
        //            cmdUpdate.Parameters.AddWithValue("@MA_GIUONG", maGiuongMoi);
        //            cmdUpdate.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
        //            cmdUpdate.ExecuteNonQuery();

        //            transaction.Commit();
        //            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            btnTimkiemnoitru.PerformClick(); // Refresh dữ liệu
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void btnLuuthaydoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Kiểm tra dữ liệu đầu vào
                    if (txtTenphong.SelectedValue == null || ComboBoxSogiuong.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lấy mã phòng mới từ combobox (đã bind sẵn giá trị là MA_PHONG)
                    int maPhongMoi = Convert.ToInt32(txtTenphong.SelectedValue);

                    // Lấy mã giường mới từ combobox (đã bind sẵn giá trị là MA_GIUONG)
                    int maGiuongMoi = Convert.ToInt32(ComboBoxSogiuong.SelectedValue);

                    // Cập nhật thông tin nội trú
                    string queryUpdateNoiTru = @"
                UPDATE NOI_TRU 
                SET MA_PHONG = @MA_PHONG, MA_GIUONG = @MA_GIUONG 
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
                    //ansaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}

