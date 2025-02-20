using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
//using static System.ComponentModel.Design.ObjectSelectorEditor;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinformKTX
{
    public partial class QuanLiSinhVien : Form
    {
        // Khởi tạo đối tượng KetnoiCSDL để sử dụng phương thức GetConnection
        KetnoiCSDL ketnoi = new KetnoiCSDL();
        //private string connectionString = "Data Source=LAPTOP-SI5JBDIU\\SQLEXPRESS01;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; // Thay bằng chuỗi kết nối của bạn
        public QuanLiSinhVien()
        {
            InitializeComponent();

            // Các thao tác khởi tạo khác
            comboBoxGioiTinh.SelectedIndexChanged += comboBoxGioiTinh_ThayDoi;
            comboBoxMaLoaiPhong.SelectedIndexChanged += comboBoxMaLoaiPhong_ThayDoi;
            comboBoxMaTang.SelectedIndexChanged += comboBoxMaTang_ThayDoi;
            comboBoxMaPhong.SelectedIndexChanged += comboBoxMaPhong_ThayDoi;

            // Tải dữ liệu lên DataGridView khi form load
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Truy vấn lấy dữ liệu sinh viên + nội trú + tên tầng + tên loại tầng + tên phòng + tên giường
                    string query = @"
                                SELECT 
                                    SINH_VIEN.MSSV,
                                    SINH_VIEN.HOTEN_SV, 
                                    SINH_VIEN.CCCD, 
                                    SINH_VIEN.NGAY_SINH, 
                                    SINH_VIEN.GIOI_TINH, 
                                    SINH_VIEN.SDT_SINHVIEN,
                                    SINH_VIEN.SDT_NGUOITHAN,
                                    SINH_VIEN.QUE_QUAN,
                                    SINH_VIEN.EMAIL,
                                    NOI_TRU.MA_PHONG,    -- Giữ lại để xử lý dữ liệu
                                    NOI_TRU.MA_GIUONG,   -- Giữ lại để xử lý dữ liệu
                                    PHONG.MA_TANG,       -- Giữ lại để xử lý dữ liệu
                                    LOAI_PHONG.MA_LOAI_PHONG,   -- Giữ lại để xử lý dữ liệu
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU,
                                    PHONG.TEN_PHONG,     -- Hiển thị thay vì MA_PHONG
                                    GIUONG.TEN_GIUONG,   -- Hiển thị thay vì MA_GIUONG
                                    TANG.TEN_TANG,       -- Hiển thị thay vì MA_TANG
                                    LOAI_PHONG.TEN_LOAI_PHONG -- Hiển thị thay vì MA_LOAI_PHONG
                                FROM SINH_VIEN
                                INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                                INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                                INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                                INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                                INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false; // Ẩn Mã Loại Tầng nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Tên Loại Tầng";

                    dataGridView1.Columns["MA_TANG"].Visible = false;   // Ẩn Mã Tầng nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tên Tầng";

                    dataGridView1.Columns["MA_PHONG"].Visible = false;  // Ẩn Mã Phòng nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";

                    dataGridView1.Columns["MA_GIUONG"].Visible = false; // Ẩn Mã Giường nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Tên Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);

                    // Tải danh sách loại phòng vào comboBoxMaLoaiTang
                    //TaiDanhSachLoaiPhong(conn);

                    // Tải danh sách tầng vào comboBoxMaTang
                    //TaiDanhSachTang(conn);

                    // Tải danh sách phòng trống vào comboBoxMaPhong
                    //LoadAvailableRooms(conn);

                    // Tải danh sách giường trống vào comboBoxMaGiuong
                    //LoadAvailableBeds(conn);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }
        private void TaiDanhSachTang(SqlConnection conn)
        {
            try
            {
                // Truy vấn danh sách tầng
                string query = @"SELECT MA_TANG, TEN_TANG FROM TANG";
                using (var cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    comboBoxMaTang.DataSource = table;
                    comboBoxMaTang.DisplayMember = "TEN_TANG";  // Hiển thị tên tầng
                    comboBoxMaTang.ValueMember = "MA_TANG";  // Xử lý logic theo mã tầng
                    comboBoxMaTang.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message);
            }
        }

        private void TaiDanhSachLoaiPhong(SqlConnection conn)
        {
            try
            {
                // Truy vấn danh sách loại phòng
                string query = @"SELECT MA_LOAI_PHONG, TEN_LOAI_PHONG FROM LOAI_PHONG";
                using (var cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    comboBoxMaLoaiPhong.DataSource = table;
                    comboBoxMaLoaiPhong.DisplayMember = "TEN_LOAI_PHONG";  // Hiển thị tên loại tầng
                    comboBoxMaLoaiPhong.ValueMember = "MA_LOAI_PHONG";  // Xử lý logic theo mã loại tầng
                    comboBoxMaLoaiPhong.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại phòng: " + ex.Message);
            }
        }

        private void LoadAvailableRooms(SqlConnection conn)
        {
            try
            {
                // Truy vấn danh sách phòng trống
                string roomQuery = @"SELECT MA_PHONG, TEN_PHONG 
                     FROM PHONG 
                     WHERE SO_GIUONG_CON_TRONG > 0";
                using (var cmd = new SqlCommand(roomQuery, conn))
                {
                    SqlDataAdapter roomAdapter = new SqlDataAdapter(cmd);
                    DataTable roomTable = new DataTable();
                    roomAdapter.Fill(roomTable);

                    // Gắn dữ liệu vào ComboBox, hiển thị TÊN_PHÒNG nhưng giữ giá trị là MA_PHONG
                    comboBoxMaPhong.DataSource = roomTable;
                    comboBoxMaPhong.DisplayMember = "TEN_PHONG";  // Hiển thị tên phòng
                    comboBoxMaPhong.ValueMember = "MA_PHONG";  // Xử lý logic theo mã phòng
                    comboBoxMaPhong.SelectedIndex = -1; // Không chọn mặc định
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng trống: " + ex.Message);
            }
        }
        private void comboBoxGioiTinh_ThayDoi(object sender, EventArgs e)
        {
            if (comboBoxGioiTinh.SelectedItem == null) return;

            string gioiTinh = comboBoxGioiTinh.SelectedItem.ToString();

            using (SqlConnection conn = ketnoi.GetConnection())
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

                    comboBoxMaLoaiPhong.DataSource = table;
                    comboBoxMaLoaiPhong.DisplayMember = "TEN_LOAI_PHONG"; // Hiển thị tên loại phòng
                    comboBoxMaLoaiPhong.ValueMember = "MA_LOAI_PHONG";   // Lấy mã loại phòng
                    comboBoxMaLoaiPhong.SelectedIndex = -1;

                    comboBoxMaGiuong.DataSource = null; // Xóa danh sách giường khi thay đổi giới tính
                }
            }
        }

        private void comboBoxMaLoaiPhong_ThayDoi(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu chưa chọn loại phòng
                if (comboBoxMaLoaiPhong.SelectedValue == null)
                {
                    comboBoxMaTang.DataSource = null; // Xóa danh sách tầng nếu chưa chọn loại phòng
                    return;
                }

                // Lấy giá trị thực tế từ SelectedValue
                object selectedValue = comboBoxMaLoaiPhong.SelectedValue;
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

                using (SqlConnection conn = ketnoi.GetConnection())
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
                        comboBoxMaTang.DataSource = table;
                        comboBoxMaTang.DisplayMember = "TEN_TANG"; // Hiển thị tên tầng
                        comboBoxMaTang.ValueMember = "MA_TANG";    // Xử lý logic theo mã tầng
                        comboBoxMaTang.SelectedIndex = -1; // Không chọn sẵn giá trị đầu tiên
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message);
            }
        }
        private void comboBoxMaTang_ThayDoi(object sender, EventArgs e)
        {
            if (comboBoxMaTang.SelectedValue == null) { return; }
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();

                // Lấy phòng hiện tại của sinh viên nếu MSSV có giá trị
                string maPhongHienTai = null;
                if (!string.IsNullOrEmpty(textBoxMSSV.Text.Trim()))
                {
                    string queryPhongHienTai = @"
                SELECT MA_PHONG FROM NOI_TRU WHERE MSSV = @MSSV";

                    using (SqlCommand cmdPhong = new SqlCommand(queryPhongHienTai, conn))
                    {
                        cmdPhong.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text.Trim());
                        var result = cmdPhong.ExecuteScalar();
                        if (result != null)
                        {
                            maPhongHienTai = result.ToString();
                        }
                    }
                }

                string query = @"
                SELECT MA_PHONG, TEN_PHONG 
                FROM PHONG 
                WHERE MA_TANG = @MaTang 
                AND (SO_GIUONG_CON_TRONG > 0 OR MA_PHONG = @MaPhongHienTai)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //if (comboBoxMaTang.SelectedValue == null)
                    //{
                    //    MessageBox.Show("Vui lòng chọn tầng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    // Lấy giá trị thực tế từ DataRowView nếu cần
                    var selectedValue = comboBoxMaTang.SelectedValue;
                    if (selectedValue is DataRowView rowView)
                    {
                        selectedValue = rowView["MA_TANG"];
                    }

                    // Chuyển thành kiểu số nguyên trước khi truyền vào SQL
                    cmd.Parameters.AddWithValue("@MaTang", Convert.ToInt32(selectedValue));
                    cmd.Parameters.AddWithValue("@MaPhongHienTai", maPhongHienTai ?? (object)DBNull.Value); // Xử lý null

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count == 0)
                        {
                            comboBoxMaPhong.DataSource = null;
                            MessageBox.Show("Không có phòng nào còn giường trống trong tầng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        comboBoxMaPhong.DataSource = table;
                        comboBoxMaPhong.DisplayMember = "TEN_PHONG";
                        comboBoxMaPhong.ValueMember = "MA_PHONG";
                        comboBoxMaPhong.SelectedIndex = -1;

                        comboBoxMaGiuong.DataSource = null; // Xóa danh sách giường khi thay đổi tầng
                    }
                }
            }

        }
        private void comboBoxMaPhong_ThayDoi(object sender, EventArgs e)
        {
            if (comboBoxMaPhong.SelectedValue == null) return;

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();

                // Lấy mã phòng được chọn
                object selectedValue = comboBoxMaPhong.SelectedValue;
                string selectedMaPhong = selectedValue is DataRowView dataRowView ? dataRowView["MA_PHONG"].ToString() : selectedValue.ToString();

                if (!int.TryParse(selectedMaPhong, out int maPhong))
                {
                    MessageBox.Show("Mã phòng không hợp lệ.");
                    return;
                }

                // Lấy mã phòng hiện tại của sinh viên
                string currentRoomQuery = "SELECT MA_PHONG FROM NOI_TRU WHERE MSSV = @MSSV";
                int currentRoom = 0;
                using (var currentRoomCmd = new SqlCommand(currentRoomQuery, conn))
                {
                    currentRoomCmd.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text.Trim());
                    var result = currentRoomCmd.ExecuteScalar();
                    currentRoom = result != null ? Convert.ToInt32(result) : 0;
                }

                // Nếu phòng được chọn là phòng hiện tại của sinh viên, lấy mã giường hiện tại
                string maGiuongHienTai = null;
                if (currentRoom == maPhong)
                {
                    string queryGiuongHienTai = "SELECT MA_GIUONG FROM NOI_TRU WHERE MSSV = @MSSV";
                    using (SqlCommand cmdGiuong = new SqlCommand(queryGiuongHienTai, conn))
                    {
                        cmdGiuong.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text.Trim());
                        var result = cmdGiuong.ExecuteScalar();
                        if (result != null)
                        {
                            maGiuongHienTai = result.ToString();
                        }
                    }
                }

                // Load danh sách giường còn trống hoặc giường hiện tại của sinh viên
                string queryGiuong = @"
                        SELECT MA_GIUONG, TEN_GIUONG
                        FROM GIUONG 
                        WHERE MA_PHONG = @MaPhong 
                        AND (TINH_TRANG_GIUONG = 'Trống' OR MA_GIUONG = @MaGiuongHienTai)"; // Giữ giường hiện tại nếu có

                using (SqlCommand cmd = new SqlCommand(queryGiuong, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmd.Parameters.AddWithValue("@MaGiuongHienTai", maGiuongHienTai ?? (object)DBNull.Value); // Xử lý nếu null

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

                        comboBoxMaGiuong.DataSource = table;
                        comboBoxMaGiuong.DisplayMember = "TEN_GIUONG";
                        comboBoxMaGiuong.ValueMember = "MA_GIUONG";

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


        private void dataGridViewQLSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ResetInputFields();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                string GetSafeValue(object cellValue)
                {
                    return cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()) ? cellValue.ToString() : string.Empty;
                }

                // Gán dữ liệu vào TextBox
                textBoxMSSV.Text = GetSafeValue(row.Cells["MSSV"].Value);
                textBoxHoTenSV.Text = GetSafeValue(row.Cells["HOTEN_SV"].Value);
                textBoxCccd.Text = GetSafeValue(row.Cells["CCCD"].Value);
                dateTimePickerNgaySinh.Text = GetSafeValue(row.Cells["NGAY_SINH"].Value);
                comboBoxGioiTinh.Text = GetSafeValue(row.Cells["GIOI_TINH"].Value);
                textBoxSdtSV.Text = GetSafeValue(row.Cells["SDT_SINHVIEN"].Value);
                textBoxSdtNguoiThan.Text = GetSafeValue(row.Cells["SDT_NGUOITHAN"].Value);
                textBoxQueQuan.Text = GetSafeValue(row.Cells["QUE_QUAN"].Value);
                textBoxEmail.Text = GetSafeValue(row.Cells["EMAIL"].Value);
                dateTimePickerNgayBatDauNoiTru.Text = GetSafeValue(row.Cells["NGAY_BAT_DAU_NOI_TRU"].Value);
                dateTimePickerNgayKetThucNoiTru.Text = GetSafeValue(row.Cells["NGAY_KET_THUC_NOI_TRU"].Value);
                comboBoxTrangThaiNoiTru.Text = GetSafeValue(row.Cells["TRANG_THAI_NOI_TRU"].Value);

                //// Lấy mã nhưng hiển thị tên trong ComboBox
                //comboBoxMaLoaiPhong.SelectedValue = GetSafeValue(row.Cells["MA_LOAI_PHONG"].Value);
                //comboBoxMaTang.SelectedValue = GetSafeValue(row.Cells["MA_TANG"].Value);
                //comboBoxMaPhong.SelectedValue = GetSafeValue(row.Cells["MA_PHONG"].Value);
                //comboBoxMaGiuong.SelectedValue = GetSafeValue(row.Cells["MA_GIUONG"].Value);
                SetComboBoxValue(comboBoxMaLoaiPhong, row.Cells["MA_LOAI_PHONG"].Value);
                SetComboBoxValue(comboBoxMaTang, row.Cells["MA_TANG"].Value);
                SetComboBoxValue(comboBoxMaPhong, row.Cells["MA_PHONG"].Value);
                SetComboBoxValue(comboBoxMaGiuong, row.Cells["MA_GIUONG"].Value);

            }
        }
        private void SetComboBoxValue(ComboBox comboBox, object value)
        {
            if (comboBox.DataSource != null && value != null)
            {
                string safeValue = value.ToString();
                if (comboBox.Items.Cast<DataRowView>().Any(item => item[comboBox.ValueMember].ToString() == safeValue))
                {
                    comboBox.SelectedValue = safeValue;
                }
            }
        }

        private void buttonCapNhatSV_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu MSSV không rỗng
            if (string.IsNullOrWhiteSpace(textBoxMSSV.Text))
            {
                MessageBox.Show("Mã số sinh viên không được để trống!");
                return;
            }

            // Thực hiện cập nhật
            UpdateStudentInfo();
        }


        private void UpdateStudentInfo()
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    // Kiểm tra các thuộc tính không được để trống
                    if (string.IsNullOrWhiteSpace(textBoxMSSV.Text))
                    {
                        MessageBox.Show("Mã số sinh viên không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxHoTenSV.Text))
                    {
                        MessageBox.Show("Họ tên sinh viên không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxCccd.Text))
                    {
                        MessageBox.Show("CCCD không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(comboBoxGioiTinh.Text))
                    {
                        MessageBox.Show("Giới tính không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxSdtSV.Text))
                    {
                        MessageBox.Show("Số điện thoại sinh viên không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxSdtNguoiThan.Text))
                    {
                        MessageBox.Show("Số điện thoại người thân không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxQueQuan.Text))
                    {
                        MessageBox.Show("Quê quán không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
                    {
                        MessageBox.Show("Email không được để trống.");
                        return;
                    }
                    if (comboBoxMaLoaiPhong.SelectedValue == null)
                    {
                        MessageBox.Show("Loại phòng không được để trống.");
                        return;
                    }
                    if (comboBoxMaTang.SelectedValue == null)
                    {
                        MessageBox.Show("Tầng không được để trống.");
                        return;
                    }
                    if (comboBoxMaPhong.SelectedValue == null)
                    {
                        MessageBox.Show("Phòng không được để trống.");
                        return;
                    }
                    if (comboBoxMaGiuong.SelectedValue == null)
                    {
                        MessageBox.Show("Giường không được để trống.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(comboBoxTrangThaiNoiTru.Text))
                    {
                        MessageBox.Show("Trạng thái nội trú không được để trống.");
                        return;
                    }

                    try
                    {
                        conn.Open();

                        // Truy vấn lấy dữ liệu cũ từ bảng NOI_TRU và SINH_VIEN
                        string oldDataQuery = @"
                            SELECT 
                                nt.MA_PHONG, nt.MA_GIUONG, nt.NGAY_BAT_DAU_NOI_TRU, nt.NGAY_KET_THUC_NOI_TRU, nt.TRANG_THAI_NOI_TRU,
                                sv.HOTEN_SV, sv.CCCD, sv.GIOI_TINH, sv.SDT_SINHVIEN, sv.EMAIL, sv.QUE_QUAN, sv.SDT_NGUOITHAN, sv.NGAY_SINH
                            FROM NOI_TRU nt
                            INNER JOIN SINH_VIEN sv ON nt.MSSV = sv.MSSV
                            WHERE nt.MSSV = @MSSV";

                        using (SqlCommand oldDataCmd = new SqlCommand(oldDataQuery, conn))
                        {
                            oldDataCmd.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);

                            using (SqlDataReader reader = oldDataCmd.ExecuteReader())
                            {
                                if (reader.Read()) // Nếu có dữ liệu
                                {
                                    // Dữ liệu cũ từ bảng NOI_TRU
                                    int oldMaPhong = Convert.ToInt32(reader["MA_PHONG"]);
                                    int oldMaGiuong = Convert.ToInt32(reader["MA_GIUONG"]);
                                    DateTime? oldNgayBatDau = reader["NGAY_BAT_DAU_NOI_TRU"] as DateTime?;
                                    DateTime? oldNgayKetThuc = reader["NGAY_KET_THUC_NOI_TRU"] as DateTime?;
                                    string oldTrangThai = reader["TRANG_THAI_NOI_TRU"].ToString();

                                    // Dữ liệu cũ từ bảng SINH_VIEN
                                    string oldHoTen = reader["HOTEN_SV"].ToString();
                                    string oldCCCD = reader["CCCD"].ToString();
                                    string oldGioiTinh = reader["GIOI_TINH"].ToString();
                                    string oldSdt = reader["SDT_SINHVIEN"].ToString();
                                    string oldEmail = reader["EMAIL"].ToString();
                                    string oldQueQuan = reader["QUE_QUAN"].ToString();
                                    string oldSdtNguoiThan = reader["SDT_NGUOITHAN"].ToString();
                                    DateTime? oldNgaySinh = reader["NGAY_SINH"] as DateTime?;

                                    // Dữ liệu mới từ giao diện
                                    int newMaPhong = Convert.ToInt32(comboBoxMaPhong.SelectedValue);
                                    int newMaGiuong = Convert.ToInt32(comboBoxMaGiuong.SelectedValue);
                                    DateTime newNgayBatDau = dateTimePickerNgayBatDauNoiTru.Value;
                                    DateTime newNgayKetThuc = dateTimePickerNgayKetThucNoiTru.Value;
                                    string newTrangThai = comboBoxTrangThaiNoiTru.Text;

                                    string newHoTen = textBoxHoTenSV.Text.Trim();
                                    string newCCCD = textBoxCccd.Text.Trim();
                                    string newGioiTinh = comboBoxGioiTinh.Text.Trim();
                                    string newSdt = textBoxSdtSV.Text.Trim();
                                    string newEmail = textBoxEmail.Text.Trim();
                                    string newQueQuan = textBoxQueQuan.Text.Trim();
                                    string newSdtNguoiThan = textBoxSdtNguoiThan.Text.Trim();
                                    DateTime newNgaySinh = dateTimePickerNgaySinh.Value;

                                    // Kiểm tra xem dữ liệu có thay đổi không
                                    if (oldMaPhong == newMaPhong &&
                                        oldMaGiuong == newMaGiuong &&
                                        oldNgayBatDau == newNgayBatDau &&
                                        oldNgayKetThuc == newNgayKetThuc &&
                                        oldTrangThai == newTrangThai &&
                                        oldHoTen == newHoTen &&
                                        oldCCCD == newCCCD &&
                                        oldGioiTinh == newGioiTinh &&
                                        oldSdt == newSdt &&
                                        oldEmail == newEmail &&
                                        oldQueQuan == newQueQuan &&
                                        oldSdtNguoiThan == newSdtNguoiThan &&
                                        oldNgaySinh == newNgaySinh) // Thêm kiểm tra ngày sinh
                                    {
                                        MessageBox.Show("Không có thông tin nào cần cập nhật.");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy thông tin sinh viên trong hệ thống.");
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lấy thông tin: " + ex.Message);
                    }


                    // Kiểm tra ngày sinh
                    if (dateTimePickerNgaySinh.Value > DateTime.Now)
                    {
                        MessageBox.Show("Ngày sinh không hợp lệ.");
                        return;
                    }

                    //conn.Open();

                    // Lấy trạng thái nội trú hiện tại của sinh viên
                    string currentStatusQuery = @"SELECT TRANG_THAI_NOI_TRU FROM NOI_TRU WHERE MSSV = @MSSV";
                    string currentStatus = string.Empty;

                    using (var statusCmd = new SqlCommand(currentStatusQuery, conn))
                    {
                        statusCmd.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);
                        currentStatus = statusCmd.ExecuteScalar()?.ToString();
                    }

                    // Kiểm tra tính hợp lệ của thời gian nội trú
                    if (dateTimePickerNgayBatDauNoiTru.Value > dateTimePickerNgayKetThucNoiTru.Value)
                    {
                        MessageBox.Show("Thời gian bắt đầu nội trú không được lớn hơn thời gian kết thúc nội trú.");
                        return;
                    }

                    // Kiểm tra và cập nhật trạng thái nội trú
                    DateTime ngayKetThucNoiTru = dateTimePickerNgayKetThucNoiTru.Value;
                    string trangThaiMoi = comboBoxTrangThaiNoiTru.Text;

                    if (trangThaiMoi == "Chờ gia hạn" && DateTime.Now <= ngayKetThucNoiTru)
                    {
                        MessageBox.Show("Chỉ có thể đặt trạng thái 'Chờ gia hạn' khi đã qua thời gian kết thúc nội trú.");
                        return;
                    }
                    //if (trangThaiMoi == "Đang Nội Trú" && DateTime.Now > ngayKetThucNoiTru)
                    //{
                    //    MessageBox.Show("Không thể đặt trạng thái 'Đang Nội Trú' khi đã qua thời gian kết thúc nội trú.");
                    //    return;
                    //}
                    // Kiểm tra trạng thái mới và trạng thái hiện tại
                    if (trangThaiMoi == "Chờ gia hạn" && currentStatus == "Đã đăng kí")
                    {
                        MessageBox.Show("Không thể chuyển từ 'Đã đăng kí' sang 'Chờ gia hạn'.");
                        return;
                    }
                    if (trangThaiMoi == "Cần Chú Ý" && currentStatus == "Chờ gia hạn")
                    {
                        MessageBox.Show("Không thể chuyển từ 'Chờ gia hạn' sang 'Cần Chú Ý'.");
                        return;
                    }
                    if (trangThaiMoi == "Cần Chú Ý" && currentStatus == "Đang Nội Trú")
                    {
                        MessageBox.Show("Không thể chuyển từ 'Đang Nội Trú' sang 'Cần Chú Ý'.");
                        return;
                    }
                    if (trangThaiMoi == "Đang Nội Trú" && currentStatus == "Cần Chú Ý")
                    {
                        MessageBox.Show("Không thể chuyển từ 'Cần Chú Ý' sang 'Đang Nội Trú'.");
                        return;
                    }
                    if (trangThaiMoi == "Chờ gia hạn" && currentStatus == "Cần Chú Ý")
                    {
                        MessageBox.Show("Không thể chuyển từ 'Cần Chú Ý' sang 'Chờ gia hạn'.");
                        return;
                    }
                    //// Kiểm tra số lượng giường trống trong phòng
                    //string checkRoomQuery = @"SELECT SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong";
                    //int soGiuongConTrong;

                    //using (var checkCmd = new SqlCommand(checkRoomQuery, conn))
                    //{
                    //    checkCmd.Parameters.AddWithValue("@MaPhong", comboBoxMaPhong.SelectedValue); // Sử dụng ComboBox để chọn phòng
                    //    soGiuongConTrong = Convert.ToInt32(checkCmd.ExecuteScalar());
                    //}

                    //if (soGiuongConTrong <= 0)
                    //{
                    //    MessageBox.Show("Phòng đã đầy. Vui lòng chọn phòng khác.");
                    //    return;
                    //}

                    // Kiểm tra trạng thái giường trước khi cập nhật
                    string checkBedStatusQuery = @"SELECT TINH_TRANG_GIUONG FROM GIUONG WHERE MA_GIUONG = @MaGiuong";
                    string bedStatus = string.Empty;
                    if ((trangThaiMoi == "Đang Nội Trú" || trangThaiMoi == "Chờ gia hạn") &&
                    (currentStatus == "Đã đăng kí" || currentStatus == "Cần Chú Ý"))
                    {
                        using (var checkBedCmd = new SqlCommand(checkBedStatusQuery, conn))
                        {
                            checkBedCmd.Parameters.AddWithValue("@MaGiuong", comboBoxMaGiuong.SelectedValue);
                            bedStatus = checkBedCmd.ExecuteScalar()?.ToString();
                        }

                        // Nếu giường đang được sử dụng, hiển thị thông báo và dừng xử lý
                        if (bedStatus == "Đang Sử Dụng")
                        {
                            MessageBox.Show("Giường đã được sử dụng. Vui lòng chọn giường khác.");
                            return;
                        }
                    }

                    string query = @"   -- Cập nhật thông tin sinh viên
                                   UPDATE SINH_VIEN 
                                   SET HOTEN_SV = @HoTenSV,
                                       CCCD = @CCCD,
                                       NGAY_SINH = @NgaySinh,
                                       GIOI_TINH = @GioiTinh,
                                       SDT_SINHVIEN = @SdtSV,
                                       SDT_NGUOITHAN = @SdtNguoiThan,
                                       QUE_QUAN = @QueQuan,
                                       EMAIL = @Email
                                   WHERE MSSV = @MSSV;

                                   -- Lấy thông tin phòng cũ
                                   DECLARE @OldMaPhong INT, @OldMaGiuong NVARCHAR(50);
                                   SELECT @OldMaPhong = MA_PHONG, @OldMaGiuong = MA_GIUONG FROM NOI_TRU WHERE MSSV = @MSSV;

                                   -- Cập nhật thông tin nội trú
                                   UPDATE NOI_TRU
                                   SET MA_PHONG = @MaPhong,
                                       MA_GIUONG = @MaGiuong,
                                       NGAY_BAT_DAU_NOI_TRU = @NgayBatDau,
                                       NGAY_KET_THUC_NOI_TRU = @NgayKetThuc,
                                       TRANG_THAI_NOI_TRU = @TrangThai
                                   WHERE MSSV = @MSSV;

                                   -- Kiểm tra nếu có thay đổi giường hoặc thay đổi phòng
                                    IF @OldMaGiuong IS NOT NULL AND @OldMaGiuong != @MaGiuong
                                    BEGIN
                                        -- Cập nhật giường cũ thành ""Trống""
                                        UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Trống' WHERE MA_GIUONG = @OldMaGiuong;

                                        -- Nếu thay đổi giường và chuyển sang phòng khác 
                                        IF @OldMaPhong != @MaPhong
                                        BEGIN
                                            -- Nếu trạng thái cũ là ""Đang Nội Trú"" hoặc ""Chờ gia hạn"", tăng số giường trống của phòng cũ
                                            IF @CurrentStatus IN (N'Đang Nội Trú', N'Chờ gia hạn')
                                            BEGIN
                                                UPDATE PHONG SET SO_GIUONG_CON_TRONG += 1 WHERE MA_PHONG = @OldMaPhong;
                                            END

                                            -- Nếu trạng thái mới là ""Đang Nội Trú"" hoặc ""Chờ gia hạn"", giảm số giường trống của phòng mới
                                            IF @TrangThai IN (N'Đang Nội Trú', N'Chờ gia hạn')
                                            BEGIN
                                                UPDATE PHONG SET SO_GIUONG_CON_TRONG -= 1 WHERE MA_PHONG = @MaPhong;
                                                UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Đang Sử Dụng' WHERE MA_GIUONG = @MaGiuong;
                                            END

                                            -- Kiểm tra nếu trạng thái thay đổi từ ""Đang Nội Trú"" hoặc ""Chờ gia hạn"" -> ""Đã đăng kí"" hoặc ""Cần Chú Ý""
                                            IF @CurrentStatus IN (N'Đang Nội Trú', N'Chờ gia hạn') AND @TrangThai IN (N'Đã đăng kí', N'Cần Chú Ý')
                                            BEGIN
                                                -- UPDATE PHONG SET SO_GIUONG_CON_TRONG += 1 WHERE MA_PHONG = @OldMaPhong;
                                                UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Trống' WHERE MA_GIUONG = @MaGiuong;
                                            END

                                            -- Kiểm tra nếu trạng thái thay đổi từ ""Đã đăng kí"" hoặc ""Cần Chú Ý"" -> ""Đang Nội Trú"" hoặc ""Chờ gia hạn""
                                            IF @CurrentStatus IN (N'Đã đăng kí', N'Cần Chú Ý') AND @TrangThai IN (N'Đang Nội Trú', N'Chờ gia hạn')
                                            BEGIN
                                                -- UPDATE PHONG SET SO_GIUONG_CON_TRONG -= 1 WHERE MA_PHONG = @MaPhong;
                                                UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Đang Sử Dụng' WHERE MA_GIUONG = @MaGiuong;
                                            END
                                        END
                                        ELSE -- Nếu sinh viên đổi giường nhưng vẫn ở cùng phòng
                                        BEGIN
                                            IF @CurrentStatus IN (N'Đang Nội Trú', N'Chờ gia hạn') AND @TrangThai IN (N'Đã đăng kí', N'Cần Chú Ý')
                                            BEGIN
                                                -- Trạng thái cũ là ""Đang Nội Trú"" hoặc ""Chờ gia hạn"", trạng thái mới là ""Đã đăng kí"" hoặc ""Cần Chú Ý"" => tăng số giường trống
                                                UPDATE PHONG SET SO_GIUONG_CON_TRONG += 1 WHERE MA_PHONG = @MaPhong;
                                            END
                                            ELSE IF @CurrentStatus IN (N'Đã đăng kí', N'Cần Chú Ý') AND @TrangThai IN (N'Đang Nội Trú', N'Chờ gia hạn')
                                            BEGIN
                                                -- Trạng thái cũ là ""Đã đăng kí"" hoặc ""Cần Chú Ý"", trạng thái mới là ""Đang Nội Trú"" hoặc ""Chờ gia hạn"" => giảm số giường trống
                                                UPDATE PHONG SET SO_GIUONG_CON_TRONG -= 1 WHERE MA_PHONG = @MaPhong;
                                            END

                                            -- Chỉ cần cập nhật giường mới nếu trạng thái mới là ""Đang Nội Trú"" hoặc ""Chờ gia hạn""
                                            IF @TrangThai IN (N'Đang Nội Trú', N'Chờ gia hạn')
                                            BEGIN
                                                UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Đang Sử Dụng' WHERE MA_GIUONG = @MaGiuong;
                                            END
                                        END
                                        -- Cập nhật trạng thái phòng cũ
                                        DECLARE @OldSoGiuongCon INT;
                                        SELECT @OldSoGiuongCon = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @OldMaPhong;
                                        UPDATE PHONG SET TINH_TRANG_PHONG = CASE
                                            WHEN @OldSoGiuongCon = 0 THEN N'Đầy'
                                            WHEN @OldSoGiuongCon = SO_GIUONG_TOI_DA THEN N'Trống'
                                            ELSE N'Đang Sử Dụng'
                                        END WHERE MA_PHONG = @OldMaPhong;

                                        -- Cập nhật trạng thái phòng mới
                                        DECLARE @NewSoGiuongCon INT;
                                        SELECT @NewSoGiuongCon = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong;
                                        UPDATE PHONG SET TINH_TRANG_PHONG = CASE
                                            WHEN @NewSoGiuongCon = 0 THEN N'Đầy'
                                            WHEN @NewSoGiuongCon = SO_GIUONG_TOI_DA THEN N'Trống'
                                            ELSE N'Đang Sử Dụng'
                                        END WHERE MA_PHONG = @MaPhong;
                                    END;
                                   -- Nếu không thay đổi phòng hoặc giường, chỉ xử lý thay đổi trạng thái nội trú
                                   IF @OldMaPhong = @MaPhong AND @OldMaGiuong = @MaGiuong
                                   BEGIN
                                       DECLARE @SoGiuongConLai INT;
                                       -- Kiểm tra trạng thái cũ và mới để tránh cập nhật không hợp lệ
                                       IF (@TrangThai = N'Đã đăng kí' OR @TrangThai = N'Cần Chú Ý') AND (@CurrentStatus = N'Đang Nội Trú')
                                       BEGIN
                                           UPDATE PHONG SET SO_GIUONG_CON_TRONG += 1 WHERE MA_PHONG = @OldMaPhong;
                                           UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Trống' WHERE MA_GIUONG = @OldMaGiuong;
                                           SELECT @SoGiuongConLai = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @OldMaPhong;
                                           UPDATE PHONG SET TINH_TRANG_PHONG = CASE
                                               WHEN @SoGiuongConLai = 0 THEN N'Đầy'
                                               WHEN @SoGiuongConLai = SO_GIUONG_TOI_DA THEN N'Trống'
                                               ELSE N'Đang Sử Dụng'
                                           END WHERE MA_PHONG = @OldMaPhong;
                                       END;

                                       ELSE IF @TrangThai = N'Chờ gia hạn' AND @CurrentStatus = N'Đang Nội Trú'
                                           UPDATE NOI_TRU SET TRANG_THAI_NOI_TRU = N'Chờ gia hạn' WHERE MSSV = @MSSV;

                                       ELSE IF @TrangThai = N'Đang Nội Trú' AND @CurrentStatus = N'Chờ gia hạn'
                                           UPDATE NOI_TRU SET TRANG_THAI_NOI_TRU = N'Đang Nội Trú' WHERE MSSV = @MSSV;

                                       ELSE IF @TrangThai = N'Đã đăng kí' AND @CurrentStatus = N'Chờ gia hạn'
                                           PRINT N'Không thể chuyển từ Chờ gia hạn sang Đã đăng kí';

                                       ELSE IF @TrangThai = N'Đang Nội Trú' AND (@CurrentStatus = N'Đã đăng kí' OR @CurrentStatus = N'Cần Chú Ý')
                                       BEGIN
                                           DECLARE @SoGiuongHienTai INT;
                                           SELECT @SoGiuongHienTai = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong;
                                           IF @SoGiuongHienTai > 0
                                           BEGIN
                                               UPDATE PHONG SET SO_GIUONG_CON_TRONG -= 1 WHERE MA_PHONG = @MaPhong;
                                               UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Đang Sử Dụng' WHERE MA_GIUONG = @MaGiuong;
                                               UPDATE PHONG SET TINH_TRANG_PHONG = CASE
                                                   WHEN (@SoGiuongHienTai - 1) = 0 THEN N'Đầy'
                                                   ELSE N'Đang Sử Dụng'
                                               END WHERE MA_PHONG = @MaPhong;
                                           END;
                                       END;

                                       -- Nếu chuyển đổi giữa Đã đăng kí và Cần Chú Ý thì chỉ cập nhật trạng thái
                                       ELSE IF(@TrangThai = N'Cần Chú Ý' AND @CurrentStatus = N'Đã đăng kí') 
                                           OR(@TrangThai = N'Đã đăng kí' AND @CurrentStatus = N'Cần Chú Ý')
                                       BEGIN
                                           UPDATE NOI_TRU SET TRANG_THAI_NOI_TRU = @TrangThai WHERE MSSV = @MSSV;
                                       END;
                                   END;
                                ";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        // Gắn giá trị từ TextBox vào tham số
                        cmd.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);
                        cmd.Parameters.AddWithValue("@HoTenSV", textBoxHoTenSV.Text);
                        cmd.Parameters.AddWithValue("@CCCD", textBoxCccd.Text);
                        cmd.Parameters.AddWithValue("@NgaySinh", dateTimePickerNgaySinh.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", comboBoxGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@SdtSV", textBoxSdtSV.Text);
                        cmd.Parameters.AddWithValue("@SdtNguoiThan", textBoxSdtNguoiThan.Text);
                        cmd.Parameters.AddWithValue("@QueQuan", textBoxQueQuan.Text);
                        cmd.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                        cmd.Parameters.AddWithValue("@MaTang", comboBoxMaTang.SelectedValue); // Thêm mã tầng
                        cmd.Parameters.AddWithValue("@MaLoaiPhong", comboBoxMaLoaiPhong.SelectedValue); // Thêm mã loại phòng
                        cmd.Parameters.AddWithValue("@MaPhong", comboBoxMaPhong.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaGiuong", comboBoxMaGiuong.SelectedValue); // Sử dụng ComboBox để chọn giường
                        cmd.Parameters.AddWithValue("@NgayBatDau", dateTimePickerNgayBatDauNoiTru.Value);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", dateTimePickerNgayKetThucNoiTru.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", comboBoxTrangThaiNoiTru.Text);
                        cmd.Parameters.AddWithValue("@CurrentStatus", currentStatus);

                        // Thực thi lệnh SQL
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin sinh viên thành công!");
                            ResetInputFields();
                            LoadData(); // Tải lại dữ liệu để cập nhật DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để cập nhật.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message);
                }
            }
        }


        private void buttonXoaSV_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu MSSV không rỗng
            string mssv = textBoxMSSV.Text.Trim();
            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Mã số sinh viên không được để trống!");
                return;
            }

            // Xác nhận người dùng trước khi xóa
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sinh viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        string query = @"
                                    -- Lưu thông tin phòng và giường của sinh viên trước khi xóa
                                    DECLARE @MaPhong INT, @MaGiuong NVARCHAR(50), @CurrentStatus NVARCHAR(50);

                                    SELECT @MaPhong = MA_PHONG, @MaGiuong = MA_GIUONG, @CurrentStatus = TRANG_THAI_NOI_TRU
                                    FROM NOI_TRU
                                    WHERE MSSV = @MSSV;

                                    -- Xóa sinh viên khỏi NOI_TRU
                                    DELETE FROM NOI_TRU WHERE MSSV = @MSSV;

                                    -- Xóa sinh viên khỏi SINH_VIEN
                                    DELETE FROM SINH_VIEN WHERE MSSV = @MSSV;

                                    -- Nếu sinh viên đang trong trạng thái Đang Nội Trú hoặc Chờ gia hạn, cập nhật giường và phòng
                                    IF @CurrentStatus IN(N'Đang Nội Trú', N'Chờ gia hạn')
                                    BEGIN
                                        -- Cập nhật trạng thái giường của sinh viên bị xóa thành Trống
                                        IF @MaGiuong IS NOT NULL
                                        BEGIN
                                            UPDATE GIUONG
                                            SET TINH_TRANG_GIUONG = N'Trống'
                                            WHERE MA_GIUONG = @MaGiuong;
                                                            END

                                                            -- Cập nhật số giường trống của phòng liên quan
                                                            IF @MaPhong IS NOT NULL
                                    BEGIN
                                    UPDATE PHONG
                                    SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG + 1
                                    WHERE MA_PHONG = @MaPhong;

                                    --Cập nhật trạng thái phòng dựa trên số giường còn trống
                                    DECLARE @SoGiuongCon INT;
                                        SELECT @SoGiuongCon = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong;

                                    UPDATE PHONG
                                    SET TINH_TRANG_PHONG =
                                        CASE
                                            WHEN @SoGiuongCon = 0 THEN N'Đầy'
                                            WHEN @SoGiuongCon = SO_GIUONG_TOI_DA THEN N'Trống'
                                            ELSE N'Đang Sử Dụng'
                                        END
                                    WHERE MA_PHONG = @MaPhong;
                                    END
                                END;";


                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@MSSV", mssv);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã xóa sinh viên và cập nhật giường, phòng thành công.");

                            // 🔹 Reset các ô nhập liệu sau khi xóa thành công
                            ResetInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để xóa.");
                        }

                        // Load lại dữ liệu sau khi xóa
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
                    }
                }
            }
        }
        // Tạo hàm Reset các ô nhập liệu để dễ tái sử dụng
        private void ResetInputFields()
        {
            textBoxMSSV.Text = "";
            textBoxHoTenSV.Text = "";
            textBoxCccd.Text = "";
            dateTimePickerNgaySinh.Value = DateTime.Now;
            comboBoxGioiTinh.SelectedIndex = -1;
            textBoxSdtSV.Text = "";
            textBoxSdtNguoiThan.Text = "";
            textBoxQueQuan.Text = "";
            textBoxEmail.Text = "";
            dateTimePickerNgayBatDauNoiTru.Value = DateTime.Now;
            dateTimePickerNgayKetThucNoiTru.Value = DateTime.Now;
            comboBoxTrangThaiNoiTru.SelectedIndex = -1;
            comboBoxMaLoaiPhong.SelectedIndex = -1;
            comboBoxMaTang.SelectedIndex = -1;
            comboBoxMaPhong.SelectedIndex = -1;
            comboBoxMaGiuong.SelectedIndex = -1;
        }
        private void buttonAllSV_Click(object sender, EventArgs e)
        {
            // Gọi phương thức LoadData() để tải tất cả sinh viên
            LoadData();
        }

        private void buttonDangNoiTru_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                                    SELECT 
                                        SINH_VIEN.MSSV,
                                        SINH_VIEN.HOTEN_SV, 
                                        SINH_VIEN.CCCD, 
                                        SINH_VIEN.NGAY_SINH, 
                                        SINH_VIEN.GIOI_TINH, 
                                        SINH_VIEN.SDT_SINHVIEN,
                                        SINH_VIEN.SDT_NGUOITHAN,
                                        SINH_VIEN.QUE_QUAN,
                                        SINH_VIEN.EMAIL,
                                        NOI_TRU.MA_PHONG,       -- Giữ nguyên mã để xử lý dữ liệu
                                        NOI_TRU.MA_GIUONG,      -- Giữ nguyên mã để xử lý dữ liệu
                                        PHONG.MA_TANG,          -- Giữ nguyên mã để xử lý dữ liệu
                                        LOAI_PHONG.MA_LOAI_PHONG, -- Giữ nguyên mã để xử lý dữ liệu
                                        NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                        NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                        NOI_TRU.TRANG_THAI_NOI_TRU,
                                        PHONG.TEN_PHONG,      -- Hiển thị thay vì MA_PHONG
                                        GIUONG.TEN_GIUONG,    -- Hiển thị thay vì MA_GIUONG
                                        TANG.TEN_TANG,        -- Hiển thị thay vì MA_TANG
                                        LOAI_PHONG.TEN_LOAI_PHONG -- Hiển thị thay vì MA_LOAI_PHONG
                                    FROM SINH_VIEN
                                    INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                                    INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                                    INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                                    INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                                    INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG
                                    WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Đang nội trú'"; // Điều kiện lọc sinh viên đang nội trú
                    // Tạo DataAdapter và DataTable để lấy dữ liệu từ SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    // Ẩn mã nhưng giữ lại để xử lý
                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_TANG"].Visible = false;
                    dataGridView1.Columns["MA_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_GIUONG"].Visible = false;

                    // Hiển thị tên thay thế
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Loại Phòng";
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tầng";
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Phòng";
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }


        private void buttonChuaNoiTru_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                            SELECT 
                                SINH_VIEN.MSSV,
                                SINH_VIEN.HOTEN_SV, 
                                SINH_VIEN.CCCD, 
                                SINH_VIEN.NGAY_SINH, 
                                SINH_VIEN.GIOI_TINH, 
                                SINH_VIEN.SDT_SINHVIEN,
                                SINH_VIEN.SDT_NGUOITHAN,
                                SINH_VIEN.QUE_QUAN,
                                SINH_VIEN.EMAIL,
                                NOI_TRU.MA_PHONG,
                                NOI_TRU.MA_GIUONG,
                                PHONG.MA_TANG,
                                LOAI_PHONG.MA_LOAI_PHONG,
                                NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                NOI_TRU.TRANG_THAI_NOI_TRU,
                                PHONG.TEN_PHONG,
                                GIUONG.TEN_GIUONG,
                                TANG.TEN_TANG,
                                LOAI_PHONG.TEN_LOAI_PHONG
                            FROM SINH_VIEN
                            INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                            INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                            INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                            INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                            INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG
                            WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Đã đăng kí'"; // Lọc sinh viên Đã đăng kí

                    // Tạo DataAdapter và DataTable để lấy dữ liệu từ SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    // Ẩn mã nhưng giữ lại để xử lý
                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_TANG"].Visible = false;
                    dataGridView1.Columns["MA_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_GIUONG"].Visible = false;

                    // Hiển thị tên thay thế
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Loại Phòng";
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tầng";
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Phòng";
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }


        private void buttonCanChuY_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                            SELECT 
                                SINH_VIEN.MSSV,
                                SINH_VIEN.HOTEN_SV, 
                                SINH_VIEN.CCCD, 
                                SINH_VIEN.NGAY_SINH, 
                                SINH_VIEN.GIOI_TINH, 
                                SINH_VIEN.SDT_SINHVIEN,
                                SINH_VIEN.SDT_NGUOITHAN,
                                SINH_VIEN.QUE_QUAN,
                                SINH_VIEN.EMAIL,
                                NOI_TRU.MA_PHONG,
                                NOI_TRU.MA_GIUONG,
                                PHONG.MA_TANG,
                                LOAI_PHONG.MA_LOAI_PHONG,
                                NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                NOI_TRU.TRANG_THAI_NOI_TRU,
                                PHONG.TEN_PHONG,
                                GIUONG.TEN_GIUONG,
                                TANG.TEN_TANG,
                                LOAI_PHONG.TEN_LOAI_PHONG
                            FROM SINH_VIEN
                            INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                            INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                            INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                            INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                            INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG
                            WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Cần Chú Ý'";

                    // Tạo DataAdapter và DataTable để lấy dữ liệu từ SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    // Ẩn mã nhưng giữ lại để xử lý
                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_TANG"].Visible = false;
                    dataGridView1.Columns["MA_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_GIUONG"].Visible = false;

                    // Hiển thị tên thay thế
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Loại Phòng";
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tầng";
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Phòng";
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }


        private void buttonChoGiaHan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                            SELECT 
                                SINH_VIEN.MSSV,
                                SINH_VIEN.HOTEN_SV, 
                                SINH_VIEN.CCCD, 
                                SINH_VIEN.NGAY_SINH, 
                                SINH_VIEN.GIOI_TINH, 
                                SINH_VIEN.SDT_SINHVIEN,
                                SINH_VIEN.SDT_NGUOITHAN,
                                SINH_VIEN.QUE_QUAN,
                                SINH_VIEN.EMAIL,
                                NOI_TRU.MA_PHONG,
                                NOI_TRU.MA_GIUONG,
                                PHONG.MA_TANG,
                                LOAI_PHONG.MA_LOAI_PHONG,
                                NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                NOI_TRU.TRANG_THAI_NOI_TRU,
                                PHONG.TEN_PHONG,
                                GIUONG.TEN_GIUONG,
                                TANG.TEN_TANG,
                                LOAI_PHONG.TEN_LOAI_PHONG
                            FROM SINH_VIEN
                            INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                            INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                            INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                            INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                            INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG
                            WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Chờ gia hạn'"; // Lọc sinh viên Chờ gia hạn

                    // Tạo DataAdapter và DataTable để lấy dữ liệu từ SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    // Ẩn mã nhưng giữ lại để xử lý
                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_TANG"].Visible = false;
                    dataGridView1.Columns["MA_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_GIUONG"].Visible = false;

                    // Hiển thị tên thay thế
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Loại Phòng";
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tầng";
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Phòng";
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }


        private void buttonHetThoiGianNoiTru_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT 
                        SINH_VIEN.MSSV,
                        SINH_VIEN.HOTEN_SV, 
                        SINH_VIEN.CCCD, 
                        SINH_VIEN.NGAY_SINH, 
                        SINH_VIEN.GIOI_TINH, 
                        SINH_VIEN.SDT_SINHVIEN,
                        SINH_VIEN.SDT_NGUOITHAN,
                        SINH_VIEN.QUE_QUAN,
                        SINH_VIEN.EMAIL,
                        NOI_TRU.MA_PHONG,
                        NOI_TRU.MA_GIUONG,
                        PHONG.MA_TANG,
                        LOAI_PHONG.MA_LOAI_PHONG,
                        NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                        NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                        NOI_TRU.TRANG_THAI_NOI_TRU,
                        PHONG.TEN_PHONG,
                        GIUONG.TEN_GIUONG,
                        TANG.TEN_TANG,
                        LOAI_PHONG.TEN_LOAI_PHONG
                    FROM SINH_VIEN
                    INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                    INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                    INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                    INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                    INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG
                    WHERE NOI_TRU.NGAY_KET_THUC_NOI_TRU < GETDATE()"; // Lọc sinh viên hết thời gian nội trú

                    // Tạo DataAdapter và DataTable để lấy dữ liệu từ SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    // Ẩn mã nhưng giữ lại để xử lý
                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_TANG"].Visible = false;
                    dataGridView1.Columns["MA_PHONG"].Visible = false;
                    dataGridView1.Columns["MA_GIUONG"].Visible = false;

                    // Hiển thị tên thay thế
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Loại Phòng";
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tầng";
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Phòng";
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }



        private void buttonTimTheoMSSV_Click(object sender, EventArgs e)
        {
            string maSinhVien = textBoxTimKiemMSSV.Text.Trim(); // Lấy MSSV từ ô tìm kiếm

            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Vui lòng nhập MSSV để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
        SELECT SV.MSSV, SV.HOTEN_SV, SV.CCCD, SV.NGAY_SINH, SV.GIOI_TINH, 
               SV.SDT_SINHVIEN, SV.SDT_NGUOITHAN, SV.QUE_QUAN, SV.EMAIL, 
               NT.MA_PHONG, NT.MA_GIUONG, NT.NGAY_BAT_DAU_NOI_TRU, NT.NGAY_KET_THUC_NOI_TRU, NT.TRANG_THAI_NOI_TRU, 
               P.MA_TANG, P.MA_LOAI_PHONG
        FROM SINH_VIEN SV
        LEFT JOIN NOI_TRU NT ON SV.MSSV = NT.MSSV
        LEFT JOIN PHONG P ON NT.MA_PHONG = P.MA_PHONG
        WHERE SV.MSSV = @MSSV";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@MSSV", maSinhVien);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy sinh viên với MSSV: " + maSinhVien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //// Xóa dữ liệu các ô nhập liệu
                            //textBoxMSSV.Text = "";
                            //textBoxHoTenSV.Text = "";
                            //textBoxCccd.Text = "";
                            //dateTimePickerNgaySinh.Value = DateTime.Now;
                            //comboBoxGioiTinh.SelectedIndex = -1;
                            //textBoxSdtSV.Text = "";
                            //textBoxSdtNguoiThan.Text = "";
                            //textBoxQueQuan.Text = "";
                            //textBoxEmail.Text = "";
                            //dateTimePickerNgayBatDauNoiTru.Value = DateTime.Now;
                            //dateTimePickerNgayKetThucNoiTru.Value = DateTime.Now;
                            //comboBoxTrangThaiNoiTru.SelectedIndex = -1;
                            //comboBoxMaLoaiPhong.SelectedIndex = -1;
                            //comboBoxMaTang.SelectedIndex = -1;
                            //comboBoxMaPhong.SelectedIndex = -1;
                            //comboBoxMaGiuong.SelectedIndex = -1;

                            ResetInputFields();

                            return;
                        }

                        // Lấy dòng dữ liệu đầu tiên
                        DataRow row = dataTable.Rows[0];

                        string GetSafeValue(object cellValue) =>
                            cellValue != DBNull.Value && cellValue != null ? cellValue.ToString() : string.Empty;

                        // Gán dữ liệu vào các TextBox
                        textBoxMSSV.Text = GetSafeValue(row["MSSV"]);
                        textBoxHoTenSV.Text = GetSafeValue(row["HOTEN_SV"]);
                        textBoxCccd.Text = GetSafeValue(row["CCCD"]);
                        dateTimePickerNgaySinh.Text = GetSafeValue(row["NGAY_SINH"]);
                        comboBoxGioiTinh.Text = GetSafeValue(row["GIOI_TINH"]);
                        textBoxSdtSV.Text = GetSafeValue(row["SDT_SINHVIEN"]);
                        textBoxSdtNguoiThan.Text = GetSafeValue(row["SDT_NGUOITHAN"]);
                        textBoxQueQuan.Text = GetSafeValue(row["QUE_QUAN"]);
                        textBoxEmail.Text = GetSafeValue(row["EMAIL"]);
                        dateTimePickerNgayBatDauNoiTru.Text = GetSafeValue(row["NGAY_BAT_DAU_NOI_TRU"]);
                        dateTimePickerNgayKetThucNoiTru.Text = GetSafeValue(row["NGAY_KET_THUC_NOI_TRU"]);
                        comboBoxTrangThaiNoiTru.Text = GetSafeValue(row["TRANG_THAI_NOI_TRU"]);

                        // Gán dữ liệu vào ComboBox (dùng hàm SetComboBoxValue)
                        SetComboBoxValue(comboBoxMaLoaiPhong, row["MA_LOAI_PHONG"]);
                        SetComboBoxValue(comboBoxMaTang, row["MA_TANG"]);
                        SetComboBoxValue(comboBoxMaPhong, row["MA_PHONG"]);
                        SetComboBoxValue(comboBoxMaGiuong, row["MA_GIUONG"]);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void UpdateLabelChiSo(int filteredCount)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    // Đếm tổng số sinh viên
                    string queryTotal = @"SELECT COUNT(*) FROM SINH_VIEN";
                    SqlCommand command = new SqlCommand(queryTotal, conn);
                    int totalCount = (int)command.ExecuteScalar();

                    // Cập nhật vào labelChiSo theo định dạng "filteredCount/totalCount"
                    labelChiSo.Text = $"Số Sinh Viên:{filteredCount}/{totalCount}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đếm sinh viên: " + ex.Message);
                }
            }
        }

        private void buttonXacNhanNoiTruAll_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xác nhận nội trú cho tất cả sinh viên Đã đăng kí không?",
                                                  "Xác Nhận Nội Trú",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Bước 1: Kiểm tra nếu có sinh viên quá hạn nội trú
                    string checkOverdueQuery = @"
                    SELECT COUNT(*) FROM NOI_TRU
                    WHERE NGAY_KET_THUC_NOI_TRU < GETDATE()";

                    SqlCommand overdueCmd = new SqlCommand(checkOverdueQuery, conn);
                    int overdueCount = Convert.ToInt32(overdueCmd.ExecuteScalar());

                    if (overdueCount > 0)
                    {
                        MessageBox.Show("Có sinh viên đã quá hạn nội trú. Không thể xác nhận tất cả!");
                        return;
                    }

                    // Bước 2: Kiểm tra số lần xuất hiện nhiều nhất của một MA_GIUONG
                    string checkDuplicateQuery = @"
                                                SELECT MAX(SoLuong) AS SoLuongLonNhat
                                                FROM (
                                                    SELECT COUNT(NOI_TRU.MA_GIUONG) AS SoLuong
                                                    FROM NOI_TRU
                                                    GROUP BY NOI_TRU.MA_GIUONG
                                                ) AS BangTam;";

                    SqlCommand checkCmd = new SqlCommand(checkDuplicateQuery, conn);
                    int maxCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (maxCount > 1) // Nếu có giường bị trùng
                    {
                        MessageBox.Show($"Có giường bị đăng ký trùng. Vui lòng kiểm tra lại.");
                        return;
                    }


                    // Xác nhận nội trú cho tất cả sinh viên chưa được xác nhận
                    string updateQuery = @"
                                    -- Tạo bảng tạm để lưu danh sách sinh viên vừa được xác nhận nội trú
                                    DECLARE @DanhSachSinhVienMoi TABLE (MA_PHONG INT, MA_GIUONG NVARCHAR(50));

                                    -- Lưu danh sách sinh viên từ 'Đã đăng kí' sang 'Đang Nội Trú'
                                    INSERT INTO @DanhSachSinhVienMoi (MA_PHONG, MA_GIUONG)
                                    SELECT MA_PHONG, MA_GIUONG
                                    FROM NOI_TRU
                                    WHERE TRANG_THAI_NOI_TRU = N'Đã đăng kí';

                                    -- Cập nhật trạng thái nội trú cho sinh viên vừa được xác nhận
                                    UPDATE NOI_TRU
                                    SET TRANG_THAI_NOI_TRU = N'Đang Nội Trú'
                                    WHERE TRANG_THAI_NOI_TRU = N'Đã đăng kí';

                                    -- Cập nhật trạng thái giường thành 'Đang Sử Dụng' cho giường của sinh viên vừa xác nhận
                                    UPDATE GIUONG
                                    SET TINH_TRANG_GIUONG = N'Đang Sử Dụng'
                                    WHERE MA_GIUONG IN (SELECT MA_GIUONG FROM @DanhSachSinhVienMoi);

                                    -- Giảm số giường còn trống trong từng phòng tương ứng với số sinh viên vừa được xác nhận
                                    UPDATE PHONG
                                    SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG - 
                                        (SELECT COUNT(*) FROM @DanhSachSinhVienMoi DS WHERE DS.MA_PHONG = PHONG.MA_PHONG)
                                    WHERE MA_PHONG IN (SELECT DISTINCT MA_PHONG FROM @DanhSachSinhVienMoi);

                                    -- Cập nhật trạng thái phòng dựa trên số giường còn trống
                                    UPDATE PHONG
                                    SET TINH_TRANG_PHONG =
                                        CASE
                                            WHEN SO_GIUONG_CON_TRONG = 0 THEN N'Đầy'
                                            WHEN SO_GIUONG_CON_TRONG = SO_GIUONG_TOI_DA THEN N'Trống'
                                            ELSE N'Đang Sử Dụng'
                                        END
                                    WHERE MA_PHONG IN (SELECT DISTINCT MA_PHONG FROM @DanhSachSinhVienMoi);";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xác nhận nội trú thành công!");
                        LoadData(); // Tải lại dữ liệu để cập nhật DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào cần xác nhận nội trú.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xác nhận nội trú: " + ex.Message);
                }
            }
        }

        private void buttonChoGiaHanNoiTruAll_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn chuyển trạng thái 'Chờ gia hạn' cho tất cả sinh viên đã quá hạn nội trú không?",
                                                  "Xác Nhận Chờ gia hạn",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            // Nếu người dùng chọn "No", dừng thao tác
            if (result == DialogResult.No)
            {
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Lấy danh sách sinh viên đã quá hạn nội trú
                    string query = @"SELECT MSSV 
                             FROM NOI_TRU 
                             WHERE TRANG_THAI_NOI_TRU = N'Đang Nội Trú' 
                             AND NGAY_KET_THUC_NOI_TRU < GETDATE()";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        // Chuyển trạng thái Chờ gia hạn cho tất cả sinh viên đã quá hạn nội trú
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string mssv = row["MSSV"].ToString();
                            string updateQuery = @"UPDATE NOI_TRU 
                                           SET TRANG_THAI_NOI_TRU = N'Chờ gia hạn' 
                                           WHERE MSSV = @MSSV";

                            using (var command = new SqlCommand(updateQuery, conn))
                            {
                                command.Parameters.AddWithValue("@MSSV", mssv);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Đã chuyển trạng thái 'Chờ gia hạn' cho tất cả sinh viên đã quá hạn nội trú.");
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào đã quá hạn nội trú cần chuyển trạng thái.");
                    }

                    // Cập nhật lại dữ liệu trong DataGridView
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái Chờ gia hạn: " + ex.Message);
                }
            }
        }

        private void buttonChuaNoiTruAll_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn chuyển trạng thái 'Đã đăng kí' cho tất cả sinh viên không?",
                                                  "Xác Nhận Đã đăng kí",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            // Nếu người dùng chọn "No", dừng thao tác
            if (result == DialogResult.No)
            {
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Lấy danh sách sinh viên có trạng thái nội trú khác 'Đã đăng kí'
                    string query = @"SELECT MSSV, MA_PHONG, MA_GIUONG 
                             FROM NOI_TRU 
                             WHERE TRANG_THAI_NOI_TRU <> N'Đã đăng kí'";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string mssv = row["MSSV"].ToString();
                            int maPhong = row["MA_PHONG"] != DBNull.Value ? Convert.ToInt32(row["MA_PHONG"]) : 0;
                            string maGiuong = row["MA_GIUONG"]?.ToString();

                            // Cập nhật trạng thái sinh viên về 'Đã đăng kí'
                            string updateQuery = @"UPDATE NOI_TRU 
                                           SET TRANG_THAI_NOI_TRU = N'Đã đăng kí' 
                                           WHERE MSSV = @MSSV";

                            using (var command = new SqlCommand(updateQuery, conn))
                            {
                                command.Parameters.AddWithValue("@MSSV", mssv);
                                command.ExecuteNonQuery();
                            }

                            // Nếu có phòng cũ, cập nhật lại số giường trống
                            if (maPhong > 0)
                            {
                                string updatePhongQuery = @"UPDATE PHONG 
                                                    SET SO_GIUONG_CON_TRONG += 1 
                                                    WHERE MA_PHONG = @MaPhong";

                                using (var cmdPhong = new SqlCommand(updatePhongQuery, conn))
                                {
                                    cmdPhong.Parameters.AddWithValue("@MaPhong", maPhong);
                                    cmdPhong.ExecuteNonQuery();
                                }

                                // Cập nhật tình trạng phòng
                                string updateTinhTrangPhong = @"
                                                                DECLARE @SoGiuongConTrong INT;
                                                                SELECT @SoGiuongConTrong = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong;
                                                                UPDATE PHONG 
                                                                SET TINH_TRANG_PHONG = 
                                                                    CASE 
                                                                        WHEN @SoGiuongConTrong = 0 THEN N'Đầy'
                                                                        WHEN @SoGiuongConTrong = SO_GIUONG_TOI_DA THEN N'Trống'
                                                                        ELSE N'Đang Sử Dụng'
                                                                    END
                                                                WHERE MA_PHONG = @MaPhong;";

                                using (var cmdTinhTrang = new SqlCommand(updateTinhTrangPhong, conn))
                                {
                                    cmdTinhTrang.Parameters.AddWithValue("@MaPhong", maPhong);
                                    cmdTinhTrang.ExecuteNonQuery();
                                }
                            }

                            // Nếu có giường cũ, cập nhật trạng thái giường
                            if (!string.IsNullOrEmpty(maGiuong))
                            {
                                string updateGiuongQuery = @"UPDATE GIUONG 
                                                     SET TINH_TRANG_GIUONG = N'Trống' 
                                                     WHERE MA_GIUONG = @MaGiuong";

                                using (var cmdGiuong = new SqlCommand(updateGiuongQuery, conn))
                                {
                                    cmdGiuong.Parameters.AddWithValue("@MaGiuong", maGiuong);
                                    cmdGiuong.ExecuteNonQuery();
                                }
                            }
                        }

                        MessageBox.Show("Đã chuyển trạng thái 'Đã đăng kí' cho tất cả sinh viên.");
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào để cập nhật trạng thái.");
                    }

                    // Cập nhật lại dữ liệu trên DataGridView
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái 'Đã đăng kí': " + ex.Message);
                }
            }
        }

        private void buttonXoaAllSvChoGiaHan_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tất cả sinh viên có trạng thái 'Chờ gia hạn' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Nếu người dùng chọn "No", thoát khỏi phương thức
            if (result == DialogResult.No)
            {
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                                   -- Tạo biến bảng để lưu danh sách phòng và giường của sinh viên ""Gia hạn""
                                    DECLARE @PhongGiuong TABLE (MSSV NVARCHAR(50), MA_PHONG INT, MA_GIUONG NVARCHAR(50));

                                    -- Lưu lại thông tin trước khi xóa
                                    INSERT INTO @PhongGiuong (MSSV, MA_PHONG, MA_GIUONG)
                                    SELECT MSSV, MA_PHONG, MA_GIUONG
                                    FROM NOI_TRU
                                    WHERE TRANG_THAI_NOI_TRU = N'Chờ gia hạn';

                                    -- Xóa dữ liệu sinh viên ""Gia hạn"" khỏi NOI_TRU trước
                                    DELETE NT
                                    FROM NOI_TRU NT
                                    JOIN @PhongGiuong PG ON NT.MSSV = PG.MSSV;

                                    -- Xóa dữ liệu sinh viên ""Gia hạn"" khỏi SINH_VIEN
                                    DELETE SV
                                    FROM SINH_VIEN SV
                                    JOIN @PhongGiuong PG ON SV.MSSV = PG.MSSV;

                                    -- Cập nhật số giường trống của các phòng liên quan
                                    UPDATE P
                                    SET P.SO_GIUONG_CON_TRONG = P.SO_GIUONG_CON_TRONG + 
                                        (SELECT COUNT(*) FROM @PhongGiuong PG WHERE P.MA_PHONG = PG.MA_PHONG)
                                    FROM PHONG P
                                    JOIN @PhongGiuong PG ON P.MA_PHONG = PG.MA_PHONG;

                                    -- Cập nhật trạng thái phòng
                                    UPDATE P
                                    SET P.TINH_TRANG_PHONG = 
                                        CASE 
                                            WHEN P.SO_GIUONG_CON_TRONG = 0 THEN N'Đầy'
                                            WHEN P.SO_GIUONG_CON_TRONG = P.SO_GIUONG_TOI_DA THEN N'Trống'
                                            ELSE N'Đang Sử Dụng'
                                        END
                                    FROM PHONG P
                                    JOIN @PhongGiuong PG ON P.MA_PHONG = PG.MA_PHONG;

                                    -- Cập nhật trạng thái giường của các sinh viên bị xóa
                                    UPDATE G
                                    SET G.TINH_TRANG_GIUONG = N'Trống'
                                    FROM GIUONG G
                                    JOIN @PhongGiuong PG ON G.MA_GIUONG = PG.MA_GIUONG;
                                    ";

                    SqlCommand command = new SqlCommand(query, conn);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa tất cả sinh viên có trạng thái 'Chờ gia hạn' và cập nhật giường, phòng thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào có trạng thái 'Chờ gia hạn' để xóa.");
                    }

                    // Load lại dữ liệu sau khi xóa
                    LoadData();
                    ResetInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
                }
            }
        }

        private void buttonXoaAllSvChuaNoiTru_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tất cả sinh viên có trạng thái 'Đã đăng kí' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Nếu người dùng chọn "No", thoát khỏi phương thức
            if (result == DialogResult.No)
            {
                return;
            }
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                            -- Xóa sinh viên khỏi NOI_TRU trước
                            DELETE FROM NOI_TRU WHERE TRANG_THAI_NOI_TRU = N'Đã đăng kí';

                            -- Xóa sinh viên khỏi SINH_VIEN
                            DELETE FROM SINH_VIEN 
                            WHERE MSSV NOT IN (SELECT MSSV FROM NOI_TRU);
                        ";

                    SqlCommand command = new SqlCommand(query, conn);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa tất cả sinh viên có trạng thái 'Đã đăng kí' thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào có trạng thái 'Đã đăng kí' để xóa.");
                    }

                    // Load lại dữ liệu sau khi xóa
                    LoadData();
                    ResetInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
                }
            }
        }
        private void buttonXoaAllSvCanChuY_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tất cả sinh viên có trạng thái 'Cần Chú Ý' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Nếu người dùng chọn "No", thoát khỏi phương thức
            if (result == DialogResult.No)
            {
                return;
            }
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                            -- Xóa sinh viên khỏi NOI_TRU trước
                            DELETE FROM NOI_TRU WHERE TRANG_THAI_NOI_TRU = N'Cần Chú Ý';

                            -- Xóa sinh viên khỏi SINH_VIEN
                            DELETE FROM SINH_VIEN 
                            WHERE MSSV NOT IN (SELECT MSSV FROM NOI_TRU);
                        ";

                    SqlCommand command = new SqlCommand(query, conn);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa tất cả sinh viên có trạng thái 'Cần Chú Ý' thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào có trạng thái 'Cần Chú Ý' để xóa.");
                    }

                    // Load lại dữ liệu sau khi xóa
                    LoadData();
                    ResetInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
                }
            }
        }


        private void buttonXoaAllSinhVien_Click(object sender, EventArgs e)
        {
            // Hộp thoại xác nhận lần 1
            DialogResult confirm1 = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả sinh viên không?",
                                                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm1 != DialogResult.Yes)
            {
                return;
            }

            // Hộp thoại xác nhận lần 2
            DialogResult confirm2 = MessageBox.Show("Hành động này sẽ xóa tất cả sinh viên. Bạn có chắc chắn tiếp tục không?",
                                                    "Xác nhận lần cuối", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm2 != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                            -- Lưu thông tin phòng và giường của tất cả sinh viên
                            DECLARE @PhongGiuong TABLE (MSSV NVARCHAR(50), MA_PHONG INT, MA_GIUONG NVARCHAR(50));

                            INSERT INTO @PhongGiuong (MSSV, MA_PHONG, MA_GIUONG)
                            SELECT MSSV, MA_PHONG, MA_GIUONG FROM NOI_TRU;

                            -- Xóa sinh viên khỏi NOI_TRU
                            DELETE FROM NOI_TRU;

                            -- Xóa sinh viên khỏi SINH_VIEN
                            DELETE FROM SINH_VIEN;

                            -- Cập nhật số giường trống của các phòng liên quan
                            UPDATE P
                            SET P.SO_GIUONG_CON_TRONG = P.SO_GIUONG_TOI_DA
                            FROM PHONG P
                            WHERE P.MA_PHONG IN (SELECT DISTINCT MA_PHONG FROM @PhongGiuong);

                            -- Cập nhật trạng thái phòng
                            UPDATE P
                            SET P.TINH_TRANG_PHONG = N'Trống'
                            FROM PHONG P
                            WHERE P.MA_PHONG IN (SELECT DISTINCT MA_PHONG FROM @PhongGiuong);

                            -- Cập nhật trạng thái giường của các sinh viên bị xóa
                            UPDATE G
                            SET G.TINH_TRANG_GIUONG = N'Trống'
                            FROM GIUONG G
                            WHERE G.MA_GIUONG IN (SELECT DISTINCT MA_GIUONG FROM @PhongGiuong);
                        ";

                    SqlCommand command = new SqlCommand(query, conn);
                    int rowsAffected = command.ExecuteNonQuery();

                    MessageBox.Show("Đã xóa tất cả sinh viên và cập nhật giường, phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Load lại dữ liệu sau khi xóa
                    LoadData();
                    ResetInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSvDangKiTrungGiuong_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Bước 1: Truy vấn danh sách các giường bị trùng (xuất hiện trên 2 lần)
                    string queryTrungGiuong = @"
                SELECT MA_GIUONG
                FROM NOI_TRU
                GROUP BY MA_GIUONG
                HAVING COUNT(*) > 1";

                    SqlCommand cmdTrungGiuong = new SqlCommand(queryTrungGiuong, conn);
                    SqlDataAdapter adapterTrungGiuong = new SqlDataAdapter(cmdTrungGiuong);
                    DataTable dtTrungGiuong = new DataTable();
                    adapterTrungGiuong.Fill(dtTrungGiuong);

                    // Nếu không có giường nào bị trùng, hiển thị thông báo và thoát
                    if (dtTrungGiuong.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có sinh viên nào đăng ký trùng giường.");
                        dataGridView1.DataSource = null; // Xóa dữ liệu cũ
                        return;
                    }

                    // Tạo danh sách các mã giường bị trùng để sử dụng trong truy vấn tiếp theo
                    List<string> danhSachMaGiuong = dtTrungGiuong.AsEnumerable()
                        .Select(row => $"'{row["MA_GIUONG"]}'")
                        .ToList();
                    string maGiuongTrungStr = string.Join(",", danhSachMaGiuong);

                    // Bước 2: Lấy danh sách sinh viên có MA_GIUONG nằm trong danh sách bị trùng
                    string query = $@"
                SELECT 
                    SINH_VIEN.MSSV,
                    SINH_VIEN.HOTEN_SV, 
                    SINH_VIEN.CCCD, 
                    SINH_VIEN.NGAY_SINH, 
                    SINH_VIEN.GIOI_TINH, 
                    SINH_VIEN.SDT_SINHVIEN,
                    SINH_VIEN.SDT_NGUOITHAN,
                    SINH_VIEN.QUE_QUAN,
                    SINH_VIEN.EMAIL,
                    NOI_TRU.MA_PHONG,    -- Giữ lại để xử lý dữ liệu
                    NOI_TRU.MA_GIUONG,   -- Giữ lại để xử lý dữ liệu
                    PHONG.MA_TANG,       -- Giữ lại để xử lý dữ liệu
                    LOAI_PHONG.MA_LOAI_PHONG,   -- Giữ lại để xử lý dữ liệu
                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                    NOI_TRU.TRANG_THAI_NOI_TRU,
                    PHONG.TEN_PHONG,     -- Hiển thị thay vì MA_PHONG
                    GIUONG.TEN_GIUONG,   -- Hiển thị thay vì MA_GIUONG
                    TANG.TEN_TANG,       -- Hiển thị thay vì MA_TANG
                    LOAI_PHONG.TEN_LOAI_PHONG -- Hiển thị thay vì MA_LOAI_PHONG
                FROM SINH_VIEN
                INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                INNER JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG
                INNER JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG
                INNER JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG
                INNER JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG
                WHERE NOI_TRU.MA_GIUONG IN ({maGiuongTrungStr})";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột hiển thị trên DataGridView
                    dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                    dataGridView1.Columns["HOTEN_SV"].HeaderText = "Họ và Tên";
                    dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
                    dataGridView1.Columns["NGAY_SINH"].HeaderText = "Ngày Sinh";
                    dataGridView1.Columns["GIOI_TINH"].HeaderText = "Giới Tính";
                    dataGridView1.Columns["SDT_SINHVIEN"].HeaderText = "SĐT Sinh Viên";
                    dataGridView1.Columns["SDT_NGUOITHAN"].HeaderText = "SĐT Người Thân";
                    dataGridView1.Columns["QUE_QUAN"].HeaderText = "Quê Quán";
                    dataGridView1.Columns["EMAIL"].HeaderText = "Email";

                    dataGridView1.Columns["MA_LOAI_PHONG"].Visible = false; // Ẩn Mã Loại Tầng nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_LOAI_PHONG"].HeaderText = "Tên Loại Tầng";

                    dataGridView1.Columns["MA_TANG"].Visible = false;   // Ẩn Mã Tầng nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_TANG"].HeaderText = "Tên Tầng";

                    dataGridView1.Columns["MA_PHONG"].Visible = false;  // Ẩn Mã Phòng nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";

                    dataGridView1.Columns["MA_GIUONG"].Visible = false; // Ẩn Mã Giường nhưng vẫn giữ giá trị xử lý
                    dataGridView1.Columns["TEN_GIUONG"].HeaderText = "Tên Giường";

                    dataGridView1.Columns["NGAY_BAT_DAU_NOI_TRU"].HeaderText = "Ngày Bắt Đầu Nội Trú";
                    dataGridView1.Columns["NGAY_KET_THUC_NOI_TRU"].HeaderText = "Ngày Kết Thúc Nội Trú";
                    dataGridView1.Columns["TRANG_THAI_NOI_TRU"].HeaderText = "Trạng Thái Nội Trú";
                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);

                    MessageBox.Show($"Có {dataTable.Rows.Count} sinh viên đăng ký trùng giường.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy danh sách sinh viên đăng ký trùng giường: " + ex.Message);
                }
            }
        }
    }
}
