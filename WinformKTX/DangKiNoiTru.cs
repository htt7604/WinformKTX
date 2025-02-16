



//using Microsoft.Data.SqlClient;
//using System;
//using System.Data;
//using System.Windows.Forms;

//namespace WinformKTX
//{
//    public partial class DangKiNoiTru : Form
//    {
//        KetnoiCSDL kn = new KetnoiCSDL();

//        public DangKiNoiTru()
//        {
//            InitializeComponent();
//            comboBoxGioitinh.SelectedIndexChanged += ComboBoxGioitinh_SelectedIndexChanged;
//            txtLoaiphong.SelectedIndexChanged += TxtLoaiphong_SelectedIndexChanged;
//            txtTang.SelectedIndexChanged += TxtTang_SelectedIndexChanged;
//            txtTenphong.SelectedIndexChanged += TxtTenphong_SelectedIndexChanged;
//            txtThoigiannoitru.SelectedIndexChanged += TxtThoigiannoitru_SelectedIndexChanged;
//            btnDangkynoitru.Click += BtnDangkynoitru_Click;
//            dataGridView1Dangkynoitru.CellClick += DataGridView1Dangkynoitru_CellClick;
//        }

//        private void ComboBoxGioitinh_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (comboBoxGioitinh.SelectedItem == null) return;

//            string gender = comboBoxGioitinh.SelectedItem.ToString();
//            txtLoaiphong.Text = gender;
//            txtTang.Items.Clear();

//            if (gender == "Nam")
//            {
//                txtTang.Items.Add("T3");
//                txtTang.Items.Add("T4");
//            }
//            else if (gender == "Nữ")
//            {
//                txtTang.Items.Add("T1");
//                txtTang.Items.Add("T2");
//            }
//        }

//        private void TxtLoaiphong_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtTang.SelectedIndex = -1;
//            txtTenphong.Items.Clear();
//        }

//        private void TxtTang_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (txtTang.SelectedItem == null) return;

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                conn.Open();
//                string query = "SELECT TEN_PHONG FROM PHONG WHERE MA_TANG = (SELECT MA_TANG FROM TANG WHERE TEN_TANG = @Tang) AND TINH_TRANG_PHONG = N'Trống'";
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@Tang", txtTang.SelectedItem.ToString());
//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        txtTenphong.Items.Clear();
//                        while (reader.Read())
//                        {
//                            txtTenphong.Items.Add(reader["TEN_PHONG"].ToString());
//                        }
//                    }
//                }
//            }
//        }

//        private void TxtTenphong_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (txtTenphong.SelectedItem == null) return;

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                conn.Open();
//                string query = "SELECT COUNT(*) FROM GIUONG WHERE MA_PHONG = (SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TenPhong) AND TINH_TRANG_GIUONG = N'Trống'";
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@TenPhong", txtTenphong.SelectedItem.ToString());
//                    object result = cmd.ExecuteScalar();
//                    ComboBoxSogiuong.Text = result != null ? result.ToString() : "Không có dữ liệu";
//                }
//            }
//        }



//        private void TxtThoigiannoitru_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (txtTenphong.SelectedItem == null || txtThoigiannoitru.SelectedItem == null) return;

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                try
//                {
//                    conn.Open();
//                    string query = "SELECT distinct LOAI_PHONG.GIA_PHONG FROM PHONG join LOAI_PHONG on LOAI_PHONG.MA_LOAI_PHONG = PHONG.MA_LOAI_PHONG WHERE PHONG.TEN_PHONG = @TenPhong and PHONG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@TenPhong", txtTenphong.SelectedItem.ToString());

//                        object result = cmd.ExecuteScalar();

//                        if (result != null && result != DBNull.Value)
//                        {
//                            txtGiaphong.Text = result.ToString();
//                        }
//                        else
//                        {
//                            txtGiaphong.Text = "Không có giá";
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi khi truy xuất giá phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }



//        private void BtnDangkynoitru_Click(object sender, EventArgs e)
//        {
//            if (comboBoxGioitinh.SelectedItem == null || txtLoaiphong.SelectedItem == null || txtTang.SelectedItem == null ||
//                txtTenphong.SelectedItem == null || ComboBoxSogiuong.SelectedItem == null || txtThoigiannoitru.SelectedItem == null)
//            {
//                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            using (SqlConnection conn = kn.GetConnection())
//            {
//                conn.Open();

//                // Kiểm tra xem MSSV đã tồn tại trong bảng NOI_TRU chưa
//                string checkNoiTruQuery = "SELECT COUNT(*) FROM NOI_TRU WHERE MSSV = @MSSV";
//                using (SqlCommand checkCmd = new SqlCommand(checkNoiTruQuery, conn))
//                {
//                    checkCmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
//                    int count = (int)checkCmd.ExecuteScalar();
//                    if (count > 0)
//                    {
//                        MessageBox.Show("Sinh viên này đã đăng ký nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }
//                }

//                // Insert into SINH_VIEN if MSSV does not exist
//                string checkQuery = "SELECT COUNT(*) FROM SINH_VIEN WHERE MSSV = @MSSV";
//                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
//                {
//                    checkCmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
//                    int count = (int)checkCmd.ExecuteScalar();
//                    if (count == 0)
//                    {
//                        string insertSinhVienQuery = "INSERT INTO SINH_VIEN (MSSV, HOTEN_SV, CCCD, NGAY_SINH, GIOI_TINH, SDT_SINHVIEN, SDT_NGUOITHAN, QUE_QUAN, EMAIL) " +
//                                                     "VALUES (@MSSV, @HoTenSV, @CCCD, @NgaySinh, @GioiTinh, @SdtSV, @SdtNguoiThan, @QueQuan, @Email)";
//                        using (SqlCommand insertCmd = new SqlCommand(insertSinhVienQuery, conn))
//                        {
//                            insertCmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
//                            insertCmd.Parameters.AddWithValue("@HoTenSV", txtHoten.Text);
//                            insertCmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
//                            insertCmd.Parameters.AddWithValue("@NgaySinh", DateTimeNgaySinh.Value);
//                            insertCmd.Parameters.AddWithValue("@GioiTinh", comboBoxGioitinh.SelectedItem.ToString());
//                            insertCmd.Parameters.AddWithValue("@SdtSV", txtSDT.Text);
//                            insertCmd.Parameters.AddWithValue("@SdtNguoiThan", txtSdtNguoiThan.Text);
//                            insertCmd.Parameters.AddWithValue("@QueQuan", txtQuequan.Text);
//                            insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
//                            insertCmd.ExecuteNonQuery();
//                        }
//                    }
//                }

//                // Insert into NOI_TRU
//                string query = "INSERT INTO NOI_TRU (MSSV, MA_PHONG, MA_GIUONG, NGAY_DANG_KY_NOI_TRU, NGAY_BAT_DAU_NOI_TRU, TRANG_THAI_NOI_TRU) " +
//                               "VALUES (@MSSV, (SELECT TOP 1 MA_PHONG FROM PHONG WHERE TEN_PHONG = @TENPHONG), " +
//                               "(SELECT TOP 1 MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG), @NGAYDANGKY, @NGAYBATDAU, 'Đã đăng ký')";
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
//                    cmd.Parameters.AddWithValue("@TENPHONG", txtTenphong.SelectedItem.ToString());
//                    cmd.Parameters.AddWithValue("@TEN_GIUONG", ComboBoxSogiuong.SelectedItem.ToString());
//                    cmd.Parameters.AddWithValue("@NGAYDANGKY", DateTime.Now);
//                    cmd.Parameters.AddWithValue("@NGAYBATDAU", DateTime.Now);
//                    cmd.ExecuteNonQuery();
//                }
//            }

//            LoadData();
//            MessageBox.Show("Đăng ký nội trú thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            ClearFields(); // Xóa dữ liệu trong các ô
//        }

//        private void LoadData()
//        {
//            using (SqlConnection conn = kn.GetConnection())
//            {
//                conn.Open();
//                string query = "SELECT * FROM NOI_TRU";
//                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
//                {
//                    DataTable dt = new DataTable();
//                    adapter.Fill(dt);
//                    dataGridView1Dangkynoitru.DataSource = dt;

//                    // Thêm cột nút "Xóa" nếu chưa có
//                    if (!dataGridView1Dangkynoitru.Columns.Contains("Delete"))
//                    {
//                        DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
//                        deleteButtonColumn.Name = "Delete";
//                        deleteButtonColumn.HeaderText = "Xóa";
//                        deleteButtonColumn.Text = "Xóa";
//                        deleteButtonColumn.UseColumnTextForButtonValue = true;
//                        dataGridView1Dangkynoitru.Columns.Add(deleteButtonColumn);
//                    }
//                }
//            }
//        }

//        private void DataGridView1Dangkynoitru_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Delete"].Index && e.RowIndex >= 0)
//            {
//                // Lấy MSSV của hàng được chọn
//                string mssv = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MSSV"].Value.ToString();

//                // Xác nhận xóa
//                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đăng ký nội trú này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//                if (result == DialogResult.Yes)
//                {
//                    // Xóa đăng ký nội trú từ cơ sở dữ liệu
//                    using (SqlConnection conn = kn.GetConnection())
//                    {
//                        conn.Open();
//                        string deleteQuery = "DELETE FROM NOI_TRU WHERE MSSV = @MSSV";
//                        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
//                        {
//                            cmd.Parameters.AddWithValue("@MSSV", mssv);
//                            cmd.ExecuteNonQuery();
//                        }
//                    }

//                    // Tải lại dữ liệu
//                    LoadData();
//                }
//            }
//        }


//        private void ClearFields()
//        {
//            txtMasinhvien.Clear();
//            txtHoten.Clear();
//            txtCCCD.Clear();
//            DateTimeNgaySinh.Value = DateTime.Now;
//            comboBoxGioitinh.SelectedIndex = -1;
//            txtSDT.Clear();
//            txtSdtNguoiThan.Clear();
//            txtQuequan.Clear();
//            txtEmail.Clear();
//            txtLoaiphong.SelectedIndex = -1;
//            txtTang.SelectedIndex = -1;
//            txtTenphong.SelectedIndex = -1;
//            ComboBoxSogiuong.SelectedIndex = -1;
//            txtThoigiannoitru.SelectedIndex = -1;
//            txtGiaphong.Clear();
//        }
//    }
//}





using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WinformKTX
{
    public partial class DangKiNoiTru : Form
    {
        KetnoiCSDL kn = new KetnoiCSDL();

        public DangKiNoiTru()
        {
            InitializeComponent();
            comboBoxGioitinh.SelectedIndexChanged += ComboBoxGioitinh_SelectedIndexChanged;
            txtLoaiphong.SelectedIndexChanged += TxtLoaiphong_SelectedIndexChanged;
            txtTang.SelectedIndexChanged += TxtTang_SelectedIndexChanged;
            txtTenphong.SelectedIndexChanged += TxtTenphong_SelectedIndexChanged;
            txtThoigiannoitru.SelectedIndexChanged += TxtThoigiannoitru_SelectedIndexChanged;
            btnDangkynoitru.Click += BtnDangkynoitru_Click;
            dataGridView1Dangkynoitru.CellClick += DataGridView1Dangkynoitru_CellClick;
            btnTimkiemnoitru.Click += btnTimkiemnoitru_Click; // Đăng ký sự kiện Click
            dataGridView1Dangkynoitru.CellEndEdit += dataGridView1Dangkynoitru_CellEndEdit; // Đăng ký sự kiện CellEndEdit
                                                                                            // Tải lại dữ liệu
            LoadData();
        }

        private void ComboBoxGioitinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGioitinh.SelectedItem == null) return;

            string gender = comboBoxGioitinh.SelectedItem.ToString();
            txtLoaiphong.Text = gender;
            txtTang.Items.Clear();

            if (gender == "Nam")
            {
                txtTang.Items.Add("T3");
                txtTang.Items.Add("T4");
            }
            else if (gender == "Nữ")
            {
                txtTang.Items.Add("T1");
                txtTang.Items.Add("T2");
            }
        }

        private int selectedRowIndex = -1;
        private void TxtLoaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTang.SelectedIndex = -1;
            txtTenphong.Items.Clear();
        }

        private void TxtTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTang.SelectedItem == null) return;

            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string query = "SELECT TEN_PHONG FROM PHONG WHERE MA_TANG = (SELECT MA_TANG FROM TANG WHERE TEN_TANG = @Tang) AND TINH_TRANG_PHONG = N'Trống'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Tang", txtTang.SelectedItem.ToString());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        txtTenphong.Items.Clear();
                        while (reader.Read())
                        {
                            txtTenphong.Items.Add(reader["TEN_PHONG"].ToString());
                        }
                    }
                }
            }
        }

        private void TxtTenphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTenphong.SelectedItem == null) return;

            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM GIUONG WHERE MA_PHONG = (SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TenPhong) AND TINH_TRANG_GIUONG = N'Trống'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenPhong", txtTenphong.SelectedItem.ToString());
                    object result = cmd.ExecuteScalar();
                    ComboBoxSogiuong.Text = result != null ? result.ToString() : "Không có dữ liệu";
                }
            }
        }



        private void TxtThoigiannoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTenphong.SelectedItem == null || txtThoigiannoitru.SelectedItem == null) return;

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT distinct LOAI_PHONG.GIA_PHONG FROM PHONG join LOAI_PHONG on LOAI_PHONG.MA_LOAI_PHONG = PHONG.MA_LOAI_PHONG WHERE PHONG.TEN_PHONG = @TenPhong and PHONG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenPhong", txtTenphong.SelectedItem.ToString());

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            txtGiaphong.Text = result.ToString();
                        }
                        else
                        {
                            txtGiaphong.Text = "Không có giá";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy xuất giá phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void BtnDangkynoitru_Click(object sender, EventArgs e)
        {
            if (comboBoxGioitinh.SelectedItem == null || txtLoaiphong.SelectedItem == null || txtTang.SelectedItem == null ||
                txtTenphong.SelectedItem == null || ComboBoxSogiuong.SelectedItem == null || txtThoigiannoitru.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();

                // Kiểm tra xem MSSV đã tồn tại trong bảng NOI_TRU chưa
                string checkNoiTruQuery = "SELECT COUNT(*) FROM NOI_TRU WHERE MSSV = @MSSV";
                using (SqlCommand checkCmd = new SqlCommand(checkNoiTruQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Sinh viên này đã đăng ký nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insert into SINH_VIEN if MSSV does not exist
                string checkQuery = "SELECT COUNT(*) FROM SINH_VIEN WHERE MSSV = @MSSV";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertSinhVienQuery = "INSERT INTO SINH_VIEN (MSSV, HOTEN_SV, CCCD, NGAY_SINH, GIOI_TINH, SDT_SINHVIEN, SDT_NGUOITHAN, QUE_QUAN, EMAIL) " +
                                                     "VALUES (@MSSV, @HoTenSV, @CCCD, @NgaySinh, @GioiTinh, @SdtSV, @SdtNguoiThan, @QueQuan, @Email)";
                        using (SqlCommand insertCmd = new SqlCommand(insertSinhVienQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                            insertCmd.Parameters.AddWithValue("@HoTenSV", txtHoten.Text);
                            insertCmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                            insertCmd.Parameters.AddWithValue("@NgaySinh", DateTimeNgaySinh.Value);
                            insertCmd.Parameters.AddWithValue("@GioiTinh", comboBoxGioitinh.SelectedItem.ToString());
                            insertCmd.Parameters.AddWithValue("@SdtSV", txtSDT.Text);
                            insertCmd.Parameters.AddWithValue("@SdtNguoiThan", txtSdtNguoiThan.Text);
                            insertCmd.Parameters.AddWithValue("@QueQuan", txtQuequan.Text);
                            insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                // Tính NGAY_KET_THUC_NOI_TRU
                DateTime ngayBatDau = DateTime.Now; // Hoặc lấy từ DateTimePicker nếu có
                DateTime ngayKetThuc = ngayBatDau.AddMonths(3);


                string giuongQuery = "SELECT TOP 1 MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG";
                using (SqlCommand giuongCmd = new SqlCommand(giuongQuery, conn))
                {
                    giuongCmd.Parameters.AddWithValue("@TEN_GIUONG", ComboBoxSogiuong.SelectedItem.ToString());
                    var maGiuong = giuongCmd.ExecuteScalar();
                    if (maGiuong == null)
                    {
                        MessageBox.Show("Không tìm thấy giường!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // Insert into NOI_TRU
                //string query = "INSERT INTO NOI_TRU (MSSV, MA_PHONG, MA_GIUONG, NGAY_DANG_KY_NOI_TRU, NGAY_BAT_DAU_NOI_TRU, NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU) " +
                //           "VALUES (@MSSV, (SELECT TOP 1 MA_PHONG FROM PHONG WHERE TEN_PHONG = @TENPHONG), " +
                //           "(SELECT TOP 1 MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG), @NGAYDANGKY, @NGAYBATDAU, @NGAYKETTHUC, 'Đã đăng ký')";
                //using (SqlCommand cmd = new SqlCommand(query, conn))
                //{
                //    cmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                //    cmd.Parameters.AddWithValue("@TENPHONG", txtTenphong.SelectedItem.ToString());
                //    cmd.Parameters.AddWithValue("@TEN_GIUONG", ComboBoxSogiuong.SelectedItem.ToString());
                //    cmd.Parameters.AddWithValue("@NGAYDANGKY", DateTime.Now);
                //    cmd.Parameters.AddWithValue("@NGAYBATDAU", DateTime.Now);
                //    cmd.Parameters.AddWithValue("@NGAYKETTHUC", ngayKetThuc);
                //    cmd.ExecuteNonQuery();

                //    int maNoiTru = Convert.ToInt32(cmd.ExecuteScalar()); // Lấy mã nội trú vừa chèn
                //    if (maNoiTru > 0)
                //    {
                //        using (SqlCommand cmdThanhToan = new SqlCommand(@"INSERT INTO THANH_TOAN_PHONG (MSSV, MA_PHONG, MA_NOI_TRU, TRANG_THAI_THANH_TOAN) 
                //                                            VALUES (@MSSV, (SELECT TOP 1 MA_PHONG FROM PHONG WHERE TEN_PHONG = @TENPHONG), @MA_NOI_TRU, N'Chưa thanh toán')", conn))
                //        {
                //            cmdThanhToan.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                //            cmdThanhToan.Parameters.AddWithValue("@TENPHONG", txtTenphong.SelectedItem.ToString());
                //            cmdThanhToan.Parameters.AddWithValue("@MA_NOI_TRU", maNoiTru);

                //            cmdThanhToan.ExecuteNonQuery();
                //        }
                //    }
                //}

                string query = @"
    INSERT INTO NOI_TRU (MSSV, MA_PHONG, MA_GIUONG, NGAY_DANG_KY_NOI_TRU, NGAY_BAT_DAU_NOI_TRU, NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU) 
    OUTPUT INSERTED.MA_NOI_TRU
    VALUES (@MSSV, 
            (SELECT TOP 1 MA_PHONG FROM PHONG WHERE TEN_PHONG = @TENPHONG), 
            (SELECT TOP 1 MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG), 
            @NGAYDANGKY, @NGAYBATDAU, @NGAYKETTHUC, N'Đã đăng ký')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                    cmd.Parameters.AddWithValue("@TENPHONG", txtTenphong.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TEN_GIUONG", ComboBoxSogiuong.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@NGAYDANGKY", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NGAYBATDAU", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NGAYKETTHUC", ngayKetThuc);

                    int maNoiTru = Convert.ToInt32(cmd.ExecuteScalar()); // Lấy MA_NOI_TRU mới nhất từ câu lệnh OUTPUT INSERTED

                    if (maNoiTru > 0)
                    {
                        string insertThanhToanQuery = @"
            INSERT INTO THANH_TOAN_PHONG (MSSV, MA_PHONG, MA_NOI_TRU, TRANG_THAI_THANH_TOAN) 
            VALUES (@MSSV, (SELECT TOP 1 MA_PHONG FROM PHONG WHERE TEN_PHONG = @TENPHONG), @MA_NOI_TRU, N'Chưa thanh toán')";

                        using (SqlCommand cmdThanhToan = new SqlCommand(insertThanhToanQuery, conn))
                        {
                            cmdThanhToan.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                            cmdThanhToan.Parameters.AddWithValue("@TENPHONG", txtTenphong.SelectedItem.ToString());
                            cmdThanhToan.Parameters.AddWithValue("@MA_NOI_TRU", maNoiTru);

                            cmdThanhToan.ExecuteNonQuery();
                        }
                    }
                }

            }

            LoadData();
            MessageBox.Show("Đăng ký nội trú thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields(); // Xóa dữ liệu trong các ô
        }

        private bool isLoadingData = false;

        private void LoadData()
        {
            if (isLoadingData) return;
            isLoadingData = true;

            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM NOI_TRU";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1Dangkynoitru.CellEndEdit -= dataGridView1Dangkynoitru_CellEndEdit;
                    dataGridView1Dangkynoitru.DataSource = dt;
                    dataGridView1Dangkynoitru.CellEndEdit += dataGridView1Dangkynoitru_CellEndEdit;

                    // Thêm cột nút nếu chưa có
                    if (!dataGridView1Dangkynoitru.Columns.Contains("Delete"))
                    {
                        DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                        {
                            Name = "Delete",
                            HeaderText = "Xóa",
                            Text = "Xóa",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1Dangkynoitru.Columns.Add(deleteButtonColumn);
                    }

                    if (!dataGridView1Dangkynoitru.Columns.Contains("Edit"))
                    {
                        DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                        {
                            Name = "Edit",
                            HeaderText = "Sửa",
                            Text = "Sửa",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1Dangkynoitru.Columns.Add(editButtonColumn);
                    }

                    if (!dataGridView1Dangkynoitru.Columns.Contains("Save"))
                    {
                        DataGridViewButtonColumn saveButtonColumn = new DataGridViewButtonColumn
                        {
                            Name = "Save",
                            HeaderText = "Lưu",
                            Text = "Lưu",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1Dangkynoitru.Columns.Add(saveButtonColumn);
                    }
                }
            }

            isLoadingData = false;
        }


        //private void DataGridView1Dangkynoitru_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Delete"].Index && e.RowIndex >= 0)
        //    {
        //        // Lấy MSSV của hàng được chọn
        //        string mssv = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MSSV"].Value.ToString();

        //        // Xác nhận xóa
        //        DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đăng ký nội trú này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //        if (result == DialogResult.Yes)
        //        {
        //            // Xóa đăng ký nội trú từ cơ sở dữ liệu
        //            using (SqlConnection conn = kn.GetConnection())
        //            {
        //                conn.Open();
        //                string deleteQuery = "DELETE FROM NOI_TRU WHERE MSSV = @MSSV";
        //                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
        //                {
        //                    cmd.Parameters.AddWithValue("@MSSV", mssv);
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }

        //            // Tải lại dữ liệu
        //            LoadData();
        //        }
        //    }


        //}


        //private void DataGridView1Dangkynoitru_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Delete"].Index)
        //        {
        //            // Lấy MSSV của hàng được chọn
        //            string mssv = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MSSV"].Value.ToString();

        //            // Xác nhận xóa
        //            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đăng ký nội trú này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //            if (result == DialogResult.Yes)
        //            {
        //                using (SqlConnection conn = kn.GetConnection())
        //                {
        //                    conn.Open();

        //                    // Xóa các bản ghi liên quan trong bảng THANH_TOAN_PHONG
        //                    string deleteThanhToanQuery = "DELETE FROM THANH_TOAN_PHONG WHERE MSSV = @MSSV";
        //                    using (SqlCommand cmdThanhToan = new SqlCommand(deleteThanhToanQuery, conn))
        //                    {
        //                        cmdThanhToan.Parameters.AddWithValue("@MSSV", mssv);
        //                        cmdThanhToan.ExecuteNonQuery();
        //                    }

        //                    // Xóa đăng ký nội trú từ cơ sở dữ liệu
        //                    string deleteNoiTruQuery = "DELETE FROM NOI_TRU WHERE MSSV = @MSSV";
        //                    using (SqlCommand cmdNoiTru = new SqlCommand(deleteNoiTruQuery, conn))
        //                    {
        //                        cmdNoiTru.Parameters.AddWithValue("@MSSV", mssv);
        //                        cmdNoiTru.ExecuteNonQuery();
        //                    }
        //                }

        //                // Tải lại dữ liệu
        //                LoadData();
        //            }
        //        }
        //        else if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Edit"].Index)
        //        {
        //            // Cho phép chỉnh sửa hàng được chọn
        //            dataGridView1Dangkynoitru.ReadOnly = false;
        //            dataGridView1Dangkynoitru.Rows[e.RowIndex].ReadOnly = false;
        //            dataGridView1Dangkynoitru.BeginEdit(true);
        //        }
        //        else if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Save"].Index)
        //        {
        //            // Lưu thông tin đã chỉnh sửa
        //            dataGridView1Dangkynoitru.EndEdit();
        //            SaveData(e.RowIndex);
        //        }
        //    }
        //}


        private void DataGridView1Dangkynoitru_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Edit"].Index)
                {
                    // Lấy MSSV từ hàng được chọn
                    string mssv = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MSSV"].Value.ToString();

                    string tenPhong = txtTenphong.SelectedItem?.ToString();
                    if (tenPhong == null)
                    {
                        MessageBox.Show("Vui lòng chọn phòng.");
                        return;
                    }

                    string tenGiuong = ComboBoxSogiuong.SelectedItem?.ToString();
                    if (tenGiuong == null)
                    {
                        MessageBox.Show("Vui lòng chọn giường.");
                        return;
                    }

                    // Truy vấn cơ sở dữ liệu dựa trên MSSV
                    using (SqlConnection conn = kn.GetConnection())
                    {
                        conn.Open();
                        string query = @"
        INSERT INTO NOI_TRU (MSSV, MA_PHONG, MA_GIUONG, NGAY_DANG_KY_NOI_TRU, NGAY_BAT_DAU_NOI_TRU, NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU) 
        OUTPUT INSERTED.MA_NOI_TRU
        VALUES (@MSSV, 
                (SELECT TOP 1 MA_PHONG FROM PHONG WHERE TEN_PHONG = @TENPHONG), 
                (SELECT TOP 1 MA_GIUONG FROM GIUONG WHERE TEN_GIUONG = @TEN_GIUONG), 
                @NGAYDANGKY, @NGAYBATDAU, @NGAYKETTHUC, N'Đã đăng ký')";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MSSV", mssv);
                            cmd.Parameters.AddWithValue("@TENPHONG", tenPhong);
                            cmd.Parameters.AddWithValue("@TEN_GIUONG", tenGiuong);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtMasinhvien.Text = reader["MSSV"].ToString();
                                    txtHoten.Text = reader["HOTEN_SV"].ToString();
                                    txtCCCD.Text = reader["CCCD"].ToString();
                                    DateTimeNgaySinh.Value = Convert.ToDateTime(reader["NGAY_SINH"]);
                                    comboBoxGioitinh.SelectedItem = reader["GIOI_TINH"].ToString();
                                    txtSDT.Text = reader["SDT_SINHVIEN"].ToString();
                                    txtSdtNguoiThan.Text = reader["SDT_NGUOITHAN"].ToString();
                                    txtQuequan.Text = reader["QUE_QUAN"].ToString();
                                    txtEmail.Text = reader["EMAIL"].ToString();
                                    txtLoaiphong.SelectedItem = reader["LOAI_PHONG"].ToString();
                                    txtTang.SelectedItem = reader["TANG"].ToString();
                                    txtTenphong.SelectedItem = reader["TEN_PHONG"].ToString();
                                    ComboBoxSogiuong.SelectedItem = reader["TEN_GIUONG"].ToString();
                                    txtThoigiannoitru.SelectedItem = reader["THOIGIAN_NOI_TRU"].ToString();
                                    txtGiaphong.Text = reader["GIA_PHONG"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }



        private bool isEditing = false;
        private void dataGridView1Dangkynoitru_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing) return;
            isEditing = true;

            if (e.RowIndex >= 0)
            {
                string mssv = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MSSV"].Value.ToString();
                string maPhong = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MA_PHONG"].Value.ToString();
                string maGiuong = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MA_GIUONG"].Value.ToString();
                DateTime ngayDangKy = Convert.ToDateTime(dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["NGAY_DANG_KY_NOI_TRU"].Value);
                DateTime ngayBatDau = Convert.ToDateTime(dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["NGAY_BAT_DAU_NOI_TRU"].Value);
                DateTime ngayKetThuc = Convert.ToDateTime(dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["NGAY_KET_THUC_NOI_TRU"].Value);
                string trangThai = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["TRANG_THAI_NOI_TRU"].Value.ToString();

                using (SqlConnection conn = kn.GetConnection())
                {
                    conn.Open();
                    string updateQuery = "UPDATE NOI_TRU SET MA_PHONG = @MaPhong, MA_GIUONG = @MaGiuong, NGAY_DANG_KY_NOI_TRU = @NgayDangKy, NGAY_BAT_DAU_NOI_TRU = @NgayBatDau, NGAY_KET_THUC_NOI_TRU = @NgayKetThuc, TRANG_THAI_NOI_TRU = @TrangThai WHERE MSSV = @MSSV";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MSSV", mssv);
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@MaGiuong", maGiuong);
                        cmd.Parameters.AddWithValue("@NgayDangKy", ngayDangKy);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Đặt lại chế độ chỉ đọc cho DataGridView
                dataGridView1Dangkynoitru.ReadOnly = true;
                LoadData();
            }
            isEditing = false;
        }

        private void SaveData(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                string mssv = dataGridView1Dangkynoitru.Rows[rowIndex].Cells["MSSV"].Value.ToString();
                string maPhong = dataGridView1Dangkynoitru.Rows[rowIndex].Cells["MA_PHONG"].Value.ToString();
                string maGiuong = dataGridView1Dangkynoitru.Rows[rowIndex].Cells["MA_GIUONG"].Value.ToString();
                DateTime ngayDangKy = Convert.ToDateTime(dataGridView1Dangkynoitru.Rows[rowIndex].Cells["NGAY_DANG_KY_NOI_TRU"].Value);
                DateTime ngayBatDau = Convert.ToDateTime(dataGridView1Dangkynoitru.Rows[rowIndex].Cells["NGAY_BAT_DAU_NOI_TRU"].Value);
                DateTime ngayKetThuc = Convert.ToDateTime(dataGridView1Dangkynoitru.Rows[rowIndex].Cells["NGAY_KET_THUC_NOI_TRU"].Value);
                string trangThai = dataGridView1Dangkynoitru.Rows[rowIndex].Cells["TRANG_THAI_NOI_TRU"].Value.ToString();

                using (SqlConnection conn = kn.GetConnection())
                {
                    conn.Open();
                    string updateQuery = "UPDATE NOI_TRU SET MA_PHONG = @MaPhong, MA_GIUONG = @MaGiuong, NGAY_DANG_KY_NOI_TRU = @NgayDangKy, NGAY_BAT_DAU_NOI_TRU = @NgayBatDau, NGAY_KET_THUC_NOI_TRU = @NgayKetThuc, TRANG_THAI_NOI_TRU = @TrangThai WHERE MSSV = @MSSV";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MSSV", mssv);
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@MaGiuong", maGiuong);
                        cmd.Parameters.AddWithValue("@NgayDangKy", ngayDangKy);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Đặt lại chế độ chỉ đọc cho DataGridView
                dataGridView1Dangkynoitru.ReadOnly = true;
                LoadData();
            }
        }



        private void ClearFields()
        {
            txtMasinhvien.Clear();
            txtHoten.Clear();
            txtCCCD.Clear();
            DateTimeNgaySinh.Value = DateTime.Now;
            comboBoxGioitinh.SelectedIndex = -1;
            txtSDT.Clear();
            txtSdtNguoiThan.Clear();
            txtQuequan.Clear();
            txtEmail.Clear();
            txtLoaiphong.SelectedIndex = -1;
            txtTang.SelectedIndex = -1;
            txtTenphong.SelectedIndex = -1;
            ComboBoxSogiuong.SelectedIndex = -1;
            txtThoigiannoitru.SelectedIndex = -1;
            txtGiaphong.Clear();
        }

        private void btnDangkynoitru_Click_1(object sender, EventArgs e)
        {

        }

        private void btnTimkiemnoitru_Click(object sender, EventArgs e)
        {
            string mssv = txtMasinhvien.Text.Trim();

            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Vui lòng nhập MSSV!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Kiểm tra xem MSSV đã tồn tại trong bảng NOI_TRU chưa
                    string checkNoiTruQuery = "SELECT COUNT(*) FROM NOI_TRU WHERE MSSV = @MSSV";
                    using (SqlCommand checkCmd = new SqlCommand(checkNoiTruQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MSSV", mssv);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Sinh viên này đã đăng ký nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Sinh viên này chưa đăng ký nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}









