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

            // Thiết lập giá trị của txtNgaydangkynoitru bằng ngày hiện tại
            txtNgaydangkynoitru.Value = DateTime.Now;
            txtNgaydangkynoitru.Enabled = false;


            // Tải lại dữ liệu
            LoadData();
        }
       
        private void ComboBoxGioitinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGioitinh.SelectedItem == null)
            {
                return;
            }

            string gioiTinh = comboBoxGioitinh.SelectedItem.ToString();

            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT DISTINCT MA_LOAI_PHONG, TEN_LOAI_PHONG
            FROM LOAI_PHONG
            WHERE TEN_LOAI_PHONG = @GioiTinh";  // Chỉ lọc theo tên loại phòng

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    txtLoaiphong.DataSource = table;
                    txtLoaiphong.DisplayMember = "TEN_LOAI_PHONG"; // Hiển thị tên loại phòng
                    txtLoaiphong.ValueMember = "MA_LOAI_PHONG";   // Lấy mã loại phòng
                    txtLoaiphong.SelectedIndex = -1;

                    ComboBoxSogiuong.DataSource = null; // Xóa danh sách giường khi thay đổi giới tính
                }
            }
        }
   
        private void TxtLoaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu chưa chọn loại phòng
                if (txtLoaiphong.SelectedValue == null)
                {
                    return;
                }

                // Lấy giá trị thực tế từ SelectedValue
                object selectedValue = txtLoaiphong.SelectedValue;
                string selectedMaLoaiPhong;

                if (selectedValue is DataRowView dataRowView)
                {
                    selectedMaLoaiPhong = dataRowView["MA_LOAI_PHONG"].ToString();
                }
                else
                {
                    selectedMaLoaiPhong = selectedValue.ToString();
                }

                // Chuyển đổi mã loại phòng sang kiểu số nguyên
                int maLoaiPhong;
                if (!int.TryParse(selectedMaLoaiPhong, out maLoaiPhong))
                {
                    MessageBox.Show("Mã loại phòng không hợp lệ.");
                    return;
                }

                using (SqlConnection conn = kn.GetConnection())
                {
                    conn.Open();

                    // Truy vấn danh sách tầng theo mã loại phòng được chọn
                    string query = @"
                SELECT DISTINCT T.MA_TANG, T.TEN_TANG
                FROM TANG T
                JOIN PHONG P ON T.MA_TANG = P.MA_TANG
                WHERE T.MA_LOAI_PHONG = @MaLoaiPhong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoaiPhong", maLoaiPhong);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Gán dữ liệu cho combobox tầng
                        txtTang.DataSource = table;
                        txtTang.DisplayMember = "TEN_TANG"; // Hiển thị tên tầng
                        txtTang.ValueMember = "MA_TANG";    // Xử lý logic theo mã tầng
                        txtTang.SelectedIndex = -1; // Không chọn sẵn giá trị đầu tiên
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message);
            }
        }

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
                            AND SO_GIUONG_CON_TRONG > 0 
                            AND TINH_TRANG_PHONG IN (N'Trống', N'Đang sử dụng')";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    

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

                        ComboBoxSogiuong.DataSource = table;
                        ComboBoxSogiuong.DisplayMember = "TEN_GIUONG";
                        ComboBoxSogiuong.ValueMember = "MA_GIUONG";
                    }
                }
            }
        }

        private void TxtThoigiannoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTenphong.SelectedItem == null || txtThoigiannoitru.SelectedItem == null) return;

            // Lấy giá trị thực tế từ SelectedValue
            object selectedValue = txtLoaiphong.SelectedValue;
            string selectedMaLoaiPhong;

            if (selectedValue is DataRowView dataRowView)
            {
                selectedMaLoaiPhong = dataRowView["MA_LOAI_PHONG"].ToString();
            }
            else
            {
                selectedMaLoaiPhong = selectedValue.ToString();
            }

            // Chuyển đổi mã loại phòng sang kiểu số nguyên
            int maLoaiPhong;
            if (!int.TryParse(selectedMaLoaiPhong, out maLoaiPhong))
            {
                MessageBox.Show("Mã loại phòng không hợp lệ.");
                return;
            }

            // Lấy mã phòng được chọn
            object selectedValue1 = txtTenphong.SelectedValue;
            string selectedMaPhong = selectedValue is DataRowView dataRowView1 ? dataRowView1["MA_PHONG"].ToString() : selectedValue1.ToString();

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
                    string query = "SELECT distinct LOAI_PHONG.GIA_PHONG FROM PHONG " +
                        "join LOAI_PHONG on LOAI_PHONG.MA_LOAI_PHONG = PHONG.MA_LOAI_PHONG " +
                        "WHERE PHONG.MA_PHONG = @MaPhong and PHONG.MA_LOAI_PHONG = @MaLoaiPhong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@MaLoaiPhong", maLoaiPhong);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtGiaphong.Text = reader["GIA_PHONG"].ToString();
                            }
                            else
                            {
                                txtGiaphong.Text = "Không có dữ liệu";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy xuất giá phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetValidMaGiaDn()
        {
            // Giả sử bạn có một logic để lấy giá trị MA_GIA_DN hợp lệ từ cơ sở dữ liệu hoặc một nguồn khác
            // Ví dụ: Lấy giá trị MA_GIA_DN từ bảng GIA_DN
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string query = "SELECT TOP 1 MA_GIA_DN FROM GIA_DIEN_NUOC"; // Thay đổi tên bảng thành GIA_DIEN_NUOC
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy giá trị MA_GIA_DN hợp lệ.");
                    }
                }
            }
        }

        private void BtnDangkynoitru_Click(object sender, EventArgs e)
        {
            // Kiểm tra các thuộc tính không được để trống
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text))
            {
                MessageBox.Show("Mã số sinh viên không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHoten.Text))
            {
                MessageBox.Show("Họ tên sinh viên không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show("CCCD không được để trống.");
                return;
            }
            // Kiểm tra CCCD phải có đúng 12 số
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCCCD.Text, @"^\d{12}$"))
            {
                MessageBox.Show("CCCD phải có đúng 12 số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBoxGioitinh.Text))
            {
                MessageBox.Show("Giới tính không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Số điện thoại sinh viên không được để trống.");
                return;
            }
            // Kiểm tra SĐT SV phải có đúng 10 số và bắt đầu bằng số 0
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại sinh viên phải có 10 số và bắt đầu bằng số 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSdtNguoiThan.Text))
            {
                MessageBox.Show("Số điện thoại người thân không được để trống.");
                return;
            }
            // Kiểm tra SĐT người thân phải có đúng 10 số và bắt đầu bằng số 0
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSdtNguoiThan.Text, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại người thân phải có 10 số và bắt đầu bằng số 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtQuequan.Text))
            {
                MessageBox.Show("Quê quán không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống.");
                return;
            }
            if (txtLoaiphong.SelectedValue == null)
            {
                MessageBox.Show("Loại phòng không được để trống.");
                return;
            }
            if (txtTang.SelectedValue == null)
            {
                MessageBox.Show("Tầng không được để trống.");
                return;
            }
            if (txtTenphong.SelectedValue == null)
            {
                MessageBox.Show("Phòng không được để trống.");
                return;
            }
            if (ComboBoxSogiuong.SelectedValue == null)
            {
                MessageBox.Show("Giường không được để trống.");
                return;
            }

            // Tính toán tuổi của sinh viên
            DateTime ngaySinh = DateTimeNgaySinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (ngaySinh > DateTime.Now.AddYears(-tuoi)) tuoi--;

            // Kiểm tra xem sinh viên có đủ 16 tuổi hay không
            if (tuoi < 16)
            {
                MessageBox.Show("Sinh viên chưa đủ 16 tuổi, không thể đăng ký nội trú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra xem Ngày bắt đầu nội trú có nhỏ hơn hoặc bằng Ngày đăng ký nội trú hay không
            if (txtNgaybatdaunoitru.Value <= txtNgaydangkynoitru.Value)
            {
                DateTime ngayHopLe = txtNgaydangkynoitru.Value.AddDays(1);
                MessageBox.Show("Ngày bắt đầu nội trú phải từ ngày " + ngayHopLe.ToString("dd/MM/yyyy") + " !",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    // Lấy giá trị thực tế từ SelectedValue
                    object selectedValue = ComboBoxSogiuong.SelectedValue;
                    string selectedSOGIUONG;

                    if (selectedValue is DataRowView dataRowView)
                    {
                        selectedSOGIUONG = dataRowView["MA_GIUONG"].ToString();
                    }
                    else
                    {
                        selectedSOGIUONG = selectedValue.ToString();
                    }

                    // Chuyển đổi mã loại phòng sang kiểu số nguyên
                    int maGIUONG;
                    if (!int.TryParse(selectedSOGIUONG, out maGIUONG))
                    {
                        MessageBox.Show("Mã giường không hợp lệ.");
                        return;
                    }
                    
                }
            
                string query = @"
    INSERT INTO NOI_TRU (MSSV, MA_PHONG, MA_GIUONG, NGAY_DANG_KY_NOI_TRU, NGAY_BAT_DAU_NOI_TRU, NGAY_KET_THUC_NOI_TRU, TRANG_THAI_NOI_TRU) 
    OUTPUT INSERTED.MA_NOI_TRU
    VALUES (@MSSV, @MaPhong, @MaGiuong, @NGAYDANGKY, @NgayBatDau, @NgayKetThuc, N'Đã đăng ký');";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MSSV", txtMasinhvien.Text);
                    cmd.Parameters.AddWithValue("@MaPhong", txtTenphong.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaGiuong", ComboBoxSogiuong.SelectedValue);
                    cmd.Parameters.AddWithValue("@NGAYDANGKY", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NgayBatDau", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);

                    // Lấy giá trị MA_NOI_TRU vừa được tạo
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int maNoiTru = Convert.ToInt32(result);

                        // Thêm dữ liệu vào bảng THANH_TOAN_PHONG
                        string insertThanhToanQuery = @"
            INSERT INTO THANH_TOAN_PHONG (MA_NOI_TRU, TRANG_THAI_THANH_TOAN) 
            VALUES (@MA_NOI_TRU, N'Chưa thanh toán');";

                        using (SqlCommand cmdThanhToan = new SqlCommand(insertThanhToanQuery, conn))
                        {
                            cmdThanhToan.Parameters.AddWithValue("@MA_NOI_TRU", maNoiTru);
                            cmdThanhToan.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể đăng ký nội trú. Vui lòng kiểm tra lại dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Cập nhật số giường còn trống
                string queryUpdateSoGiuong = "UPDATE PHONG SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG - 1 WHERE TEN_PHONG = @TEN_PHONG";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateSoGiuong, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
                    cmdUpdate.ExecuteNonQuery();
                }

                // Kiểm tra điều kiện SO_GIUONG_CON_TRONG < SO_GIUONG_TOI_DA và cập nhật bảng DIEN_NUOC
                string checkSoGiuongQuery = "SELECT SO_GIUONG_CON_TRONG, SO_GIUONG_TOI_DA FROM PHONG WHERE TEN_PHONG = @TEN_PHONG";
                using (SqlCommand checkCmd = new SqlCommand(checkSoGiuongQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int soGiuongConTrong = reader.GetInt32(0);
                            int soGiuongToiDa = reader.GetInt32(1);

                            if (soGiuongConTrong < soGiuongToiDa)
                            {
                                reader.Close(); // Đóng reader trước khi thực hiện truy vấn khác
                                int maGiaDn = GetValidMaGiaDn();
                                // Cập nhật thông tin vào bảng DIEN_NUOC
                                string insertDienNuocQuery = @"
                                            INSERT INTO DIEN_NUOC (MA_GIA_DN, MA_PHONG, CHI_SO_DIEN_CU, CHI_SO_DIEN_MOI, SO_DIEN_DA_SU_DUNG, 
                                                        CHI_SO_NUOC_CU, CHI_SO_NUOC_MOI, SO_NUOC_DA_SU_DUNG, 
                                                        TIEN_DIEN, TIEN_NUOC, TONG_TIEN, TU_NGAY, DEN_NGAY, TINH_TRANG_TT) 
                                            VALUES (@MA_GIA_DN, (SELECT MA_PHONG FROM PHONG WHERE TEN_PHONG = @TEN_PHONG), 0, 0, 0, 0, 0, 0, 0, 0, 0, @TU_NGAY, @DEN_NGAY, N'Chưa thanh toán')";

                                using (SqlCommand cmdDienNuoc = new SqlCommand(insertDienNuocQuery, conn))
                                {
                                    cmdDienNuoc.Parameters.AddWithValue("@TEN_PHONG", txtTenphong.SelectedItem.ToString());
                                    cmdDienNuoc.Parameters.AddWithValue("@MA_GIA_DN", maGiaDn); // Đảm bảo maGiaDn là giá trị hợp lệ không phải NULL

                                    cmdDienNuoc.Parameters.AddWithValue("@TU_NGAY", DateTime.Now);
                                    cmdDienNuoc.Parameters.AddWithValue("@DEN_NGAY", DateTime.Now.AddMonths(3));
                                    cmdDienNuoc.ExecuteNonQuery();
                                }
                            }
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
                string query = "SELECT NOI_TRU.*, PHONG.TEN_PHONG, SINH_VIEN.HOTEN_SV, SINH_VIEN.CCCD, SINH_VIEN.NGAY_SINH, SINH_VIEN.GIOI_TINH, " +
                    "SINH_VIEN.SDT_SINHVIEN, SINH_VIEN.SDT_NGUOITHAN, SINH_VIEN.QUE_QUAN, SINH_VIEN.EMAIL FROM NOI_TRU  " +
                    "JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG " +
                    "join SINH_VIEN on SINH_VIEN.MSSV = NOI_TRU.MSSV";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1Dangkynoitru.DataSource = dt;

                    // Hiển thị dữ liệu lên DataGridView

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1Dangkynoitru.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1Dangkynoitru.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1Dangkynoitru.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1Dangkynoitru.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1Dangkynoitru.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1Dangkynoitru.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1Dangkynoitru.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1Dangkynoitru.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1Dangkynoitru.Columns["EMAIL"].HeaderText = "Email";
                    dataGridView1Dangkynoitru.Columns["MA_PHONG"].Visible = false;  // Ẩn Mã Phòng nhưng vẫn giữ giá trị xử lý
                    dataGridView1Dangkynoitru.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                    dataGridView1Dangkynoitru.Columns["MA_GIUONG"].Visible = false; // Ẩn Mã Giường nhưng vẫn giữ giá trị xử lý
                    dataGridView1Dangkynoitru.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1Dangkynoitru.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1Dangkynoitru.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";
                    dataGridView1Dangkynoitru.Columns["MA_NOI_TRU"].HeaderText = "Mã Nội Trú";
                    dataGridView1Dangkynoitru.Columns["NGAY_DANG_KY_NOI_TRU"].HeaderText = " Ngày Đăng Ký Nội Trú";

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
                }
            }

            isLoadingData = false;
        }

        private void DataGridView1Dangkynoitru_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1Dangkynoitru.Columns["Delete"].Index)
                {
                    var cell = dataGridView1Dangkynoitru.Rows[e.RowIndex].Cells["MSSV"];
                    if (cell != null && cell.Value != null)
                    {
                        string mssv = cell.Value.ToString();

                        // Xác nhận xóa
                        DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đăng ký nội trú này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            using (SqlConnection conn = kn.GetConnection())
                            {
                                conn.Open();

                                // Lấy MA_NOI_TRU từ MSSV
                                string getMaNoiTruQuery = "SELECT MA_NOI_TRU FROM NOI_TRU WHERE MSSV = @MSSV";
                                int maNoiTru;
                                using (SqlCommand cmdGetMaNoiTru = new SqlCommand(getMaNoiTruQuery, conn))
                                {
                                    cmdGetMaNoiTru.Parameters.AddWithValue("@MSSV", mssv);
                                    maNoiTru = (int)cmdGetMaNoiTru.ExecuteScalar();
                                }

                                // Xóa các bản ghi liên quan trong bảng THANH_TOAN_PHONG
                                string deleteThanhToanQuery = "DELETE FROM THANH_TOAN_PHONG WHERE MA_NOI_TRU = @MaNoiTru";
                                using (SqlCommand cmdThanhToan = new SqlCommand(deleteThanhToanQuery, conn))
                                {
                                    cmdThanhToan.Parameters.AddWithValue("@MaNoiTru", maNoiTru);
                                    cmdThanhToan.ExecuteNonQuery();
                                }

                                // Xóa đăng ký nội trú từ cơ sở dữ liệu
                                string deleteNoiTruQuery = "DELETE FROM NOI_TRU WHERE MSSV = @MSSV";
                                string deleteSinhVienQuery = "DELETE FROM SINH_VIEN WHERE MSSV = @MSSV";

                                using (SqlTransaction transaction = conn.BeginTransaction())
                                {
                                    try
                                    {
                                        // Xóa thông tin nội trú
                                        using (SqlCommand cmdNoiTru = new SqlCommand(deleteNoiTruQuery, conn, transaction))
                                        {
                                            cmdNoiTru.Parameters.AddWithValue("@MSSV", mssv);
                                            cmdNoiTru.ExecuteNonQuery();
                                        }

                                        // Xóa thông tin sinh viên
                                        using (SqlCommand cmdSinhVien = new SqlCommand(deleteSinhVienQuery, conn, transaction))
                                        {
                                            cmdSinhVien.Parameters.AddWithValue("@MSSV", mssv);
                                            cmdSinhVien.ExecuteNonQuery();
                                        }

                                        // Commit nếu cả hai câu lệnh đều thành công
                                        transaction.Commit();
                                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Rollback nếu có lỗi
                                        transaction.Rollback();
                                        MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                // Tải lại dữ liệu
                                LoadData();
                            }
                        }
                    }
                }
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

            txtNgaydangkynoitru.Value = DateTime.Now; // Thiết lập lại giá trị của txtNgaydangkynoitru bằng ngày hiện tại
            txtNgaydangkynoitru.Enabled = false; // Không cho phép người dùng thay đổi giá trị

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
