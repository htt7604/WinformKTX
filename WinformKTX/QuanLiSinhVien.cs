using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinformKTX
{
    public partial class QuanLiSinhVien : Form
    {
        //private string connectionString = "Data Source=LAPTOP-SI5JBDIU\\SQLEXPRESS01;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; // Thay bằng chuỗi kết nối của bạn
        KetnoiCSDL ketnoi = new KetnoiCSDL();
        public QuanLiSinhVien()
        {
            InitializeComponent();

            // Các thao tác khởi tạo khác
            comboBoxMaPhong.SelectedIndexChanged += comboBoxMaPhong_ThayDoiMaPhong;

            // Tải dữ liệu lên DataGridView khi form load
            LoadData();
        }

        //private void LoadData()
        //{
        //    using (var conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string query = @"SELECT SINH_VIEN.MSSV,
        //                                   SINH_VIEN.HOTEN_SV, 
        //                                   SINH_VIEN.CCCD, 
        //                                   SINH_VIEN.NGAY_SINH, 
        //                                   SINH_VIEN.GIOI_TINH, 
        //                                   SINH_VIEN.SDT_SINHVIEN,
        //                                   SINH_VIEN.SDT_NGUOITHAN,
        //                                   SINH_VIEN.QUE_QUAN,
        //                                   SINH_VIEN.EMAIL,
        //                                   NOI_TRU.MA_PHONG,
        //                                   NOI_TRU.MA_GIUONG,
        //                                   NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
        //                                   NOI_TRU.NGAY_KET_THUC_NOI_TRU,
        //                                   NOI_TRU.TRANG_THAI_NOI_TRU
        //                            FROM SINH_VIEN 
        //                            INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV";// Thay bằng tên bảng và cột thực tế
        //            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        //            DataTable dataTable = new DataTable();
        //            adapter.Fill(dataTable);

        //            // Hiển thị dữ liệu lên DataGridView
        //            dataGridView1.DataSource = dataTable;

        //            // Gọi hàm cập nhật labelChiSo
        //            UpdateLabelChiSo(dataTable.Rows.Count);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
        //        }
        //    }
        //}
        private void LoadData()
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Tải dữ liệu sinh viên và nội trú
                    string query = @"SELECT SINH_VIEN.MSSV,
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
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU
                             FROM SINH_VIEN 
                             INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Cập nhật label chỉ số
                    UpdateLabelChiSo(dataTable.Rows.Count);

                    // Tải danh sách phòng trống vào comboBoxMaPhong
                    LoadAvailableRooms(conn);

                    // Tải danh sách giường trống vào comboBoxMaGiuong
                    LoadAvailableBeds(conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void LoadAvailableRooms(SqlConnection conn)
        {
            try
            {
                // Truy vấn danh sách phòng trống
                string roomQuery = @"SELECT MA_PHONG 
                             FROM PHONG 
                             WHERE SO_GIUONG_CON_TRONG > 0";
                using (var cmd = new SqlCommand(roomQuery, conn))
                {
                    SqlDataAdapter roomAdapter = new SqlDataAdapter(cmd);
                    DataTable roomTable = new DataTable();
                    roomAdapter.Fill(roomTable);

                    // Gắn dữ liệu vào ComboBox
                    comboBoxMaPhong.DataSource = roomTable;
                    comboBoxMaPhong.DisplayMember = "MA_PHONG";
                    comboBoxMaPhong.ValueMember = "MA_PHONG";
                    comboBoxMaPhong.SelectedIndex = -1; // Không chọn mặc định
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng trống: " + ex.Message);
            }
        }

        private void LoadAvailableBeds(SqlConnection conn)
        {
            try
            {
                // Kiểm tra nếu mã phòng chưa được chọn hoặc ComboBox chưa có giá trị
                if (comboBoxMaPhong.SelectedValue == null)
                {
                    comboBoxMaGiuong.DataSource = null; // Xóa danh sách giường nếu phòng chưa được chọn
                    return;
                }

                // Lấy giá trị thực tế từ SelectedValue
                object selectedValue = comboBoxMaPhong.SelectedValue;
                string selectedMaPhong;

                if (selectedValue is DataRowView dataRowView)
                {
                    selectedMaPhong = dataRowView["MA_PHONG"].ToString();
                }
                else
                {
                    selectedMaPhong = selectedValue.ToString();
                }

                // Chuyển đổi mã phòng sang kiểu số nguyên
                int maPhong;
                if (!int.TryParse(selectedMaPhong, out maPhong))
                {
                    MessageBox.Show("Mã phòng không hợp lệ.");
                    return;
                }

                // Lấy mã phòng hiện tại của sinh viên
                string currentRoomQuery = @"
                                            SELECT MA_PHONG 
                                            FROM NOI_TRU 
                                            WHERE MSSV = @MSSV";

                int currentRoom = 0;
                using (var currentRoomCmd = new SqlCommand(currentRoomQuery, conn))
                {
                    currentRoomCmd.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);
                    var result = currentRoomCmd.ExecuteScalar();
                    currentRoom = result != null ? Convert.ToInt32(result) : 0;
                }

                // Nếu mã phòng được chọn khác với mã phòng hiện tại của sinh viên, không hiển thị giường hiện tại
                bool isCurrentRoom = (currentRoom == maPhong);

                // Truy vấn danh sách giường theo mã phòng được chọn
                string bedQuery = @"
                                    SELECT MA_GIUONG, TINH_TRANG_GIUONG 
                                    FROM GIUONG 
                                    WHERE MA_PHONG = @MaPhong
                                      AND TINH_TRANG_GIUONG = N'Trống'";

                using (var cmd = new SqlCommand(bedQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                    SqlDataAdapter bedAdapter = new SqlDataAdapter(cmd);
                    DataTable bedTable = new DataTable();
                    bedAdapter.Fill(bedTable);

                    // Nếu phòng được chọn là phòng hiện tại, lấy mã giường hiện tại của sinh viên
                    if (isCurrentRoom)
                    {
                        string currentBedQuery = @"
                                                SELECT MA_GIUONG 
                                                FROM NOI_TRU 
                                                WHERE MSSV = @MSSV";

                        string currentBed = string.Empty;
                        using (var currentBedCmd = new SqlCommand(currentBedQuery, conn))
                        {
                            currentBedCmd.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);
                            var result = currentBedCmd.ExecuteScalar();
                            currentBed = result?.ToString();
                        }

                        // Nếu giường hiện tại của sinh viên không có trong danh sách, thêm vào
                        if (!string.IsNullOrEmpty(currentBed) && !bedTable.AsEnumerable().Any(row => row["MA_GIUONG"].ToString() == currentBed))
                        {
                            DataRow newRow = bedTable.NewRow();
                            newRow["MA_GIUONG"] = currentBed;
                            newRow["TINH_TRANG_GIUONG"] = "Đang Sử Dụng";
                            bedTable.Rows.InsertAt(newRow, 0);
                        }

                        // Chọn giường hiện tại của sinh viên nếu có
                        if (!string.IsNullOrEmpty(currentBed))
                        {
                            comboBoxMaGiuong.SelectedValue = currentBed;
                        }
                        else
                        {
                            comboBoxMaGiuong.SelectedIndex = -1; // Không chọn mặc định
                        }
                    }
                    else
                    {
                        comboBoxMaGiuong.SelectedIndex = -1; // Không chọn mặc định
                    }

                    // Gắn dữ liệu vào ComboBox giường
                    comboBoxMaGiuong.DataSource = bedTable;
                    comboBoxMaGiuong.DisplayMember = "MA_GIUONG";
                    comboBoxMaGiuong.ValueMember = "MA_GIUONG";

                    // Kiểm tra nếu không có giường trống
                    if (bedTable.Rows.Count == 0)
                    {
                        comboBoxMaGiuong.DataSource = null; // Xóa dữ liệu trong ComboBox
                        MessageBox.Show("Phòng này không còn giường trống.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu xảy ra ngoại lệ
                MessageBox.Show("Lỗi khi tải danh sách giường trống: " + ex.Message);
            }
        }


        private void comboBoxMaPhong_ThayDoiMaPhong(object sender, EventArgs e)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
                try
                {
                    conn.Open();
                    LoadAvailableBeds(conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thay đổi mã phòng: " + ex.Message);
                }
        }

        // Sự kiện khi click vào một ô trong DataGridView
        private void dataGridViewQLSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được click
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Hàm kiểm tra giá trị không rỗng hoặc null
                string GetSafeValue(object cellValue)
                {
                    return cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()) ? cellValue.ToString() : string.Empty;
                }

                // Gán giá trị từ các ô vào TextBox
                textBoxMSSV.Text = GetSafeValue(row.Cells["MSSV"].Value);
                textBoxHoTenSV.Text = GetSafeValue(row.Cells["HOTEN_SV"].Value);
                textBoxCccd.Text = GetSafeValue(row.Cells["CCCD"].Value);
                dateTimePickerNgaySinh.Text = GetSafeValue(row.Cells["NGAY_SINH"].Value);
                comboBoxGioiTinh.Text = GetSafeValue(row.Cells["GIOI_TINH"].Value);
                textBoxSdtSV.Text = GetSafeValue(row.Cells["SDT_SINHVIEN"].Value);
                textBoxSdtNguoiThan.Text = GetSafeValue(row.Cells["SDT_NGUOITHAN"].Value);
                textBoxQueQuan.Text = GetSafeValue(row.Cells["QUE_QUAN"].Value);
                textBoxEmail.Text = GetSafeValue(row.Cells["EMAIL"].Value);
                comboBoxMaPhong.Text = GetSafeValue(row.Cells["MA_PHONG"].Value);
                comboBoxMaGiuong.Text = GetSafeValue(row.Cells["MA_GIUONG"].Value);
                dateTimePickerNgayBatDauNoiTru.Text = GetSafeValue(row.Cells["NGAY_BAT_DAU_NOI_TRU"].Value);
                dateTimePickerNgayKetThucNoiTru.Text = GetSafeValue(row.Cells["NGAY_KET_THUC_NOI_TRU"].Value);
                comboBoxTrangThaiNoiTru.Text = GetSafeValue(row.Cells["TRANG_THAI_NOI_TRU"].Value);
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

                    // Kiểm tra ngày sinh
                    if (dateTimePickerNgaySinh.Value > DateTime.Now)
                    {
                        MessageBox.Show("Ngày sinh không hợp lệ.");
                        return;
                    }

                    conn.Open();

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

                    if (trangThaiMoi == "Chờ Gia Hạn" && DateTime.Now <= ngayKetThucNoiTru)
                    {
                        MessageBox.Show("Chỉ có thể đặt trạng thái 'Chờ Gia Hạn' khi đã qua thời gian kết thúc nội trú.");
                        return;
                    }
                    if (trangThaiMoi == "Đang Nội Trú" && DateTime.Now > ngayKetThucNoiTru)
                    {
                        MessageBox.Show("Không thể đặt trạng thái 'Đang Nội Trú' khi đã qua thời gian kết thúc nội trú.");
                        return;
                    }
                    // Kiểm tra trạng thái mới và trạng thái hiện tại
                    if (trangThaiMoi == "Chờ Gia Hạn" && currentStatus == "Chưa Nội Trú")
                    {
                        MessageBox.Show("Không thể chuyển từ 'Chưa Nội Trú' sang 'Chờ Gia Hạn'.");
                        return;
                    }

                    // Kiểm tra số lượng giường trống trong phòng
                    string checkRoomQuery = @"SELECT SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong";
                    int soGiuongConTrong;

                    using (var checkCmd = new SqlCommand(checkRoomQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaPhong", comboBoxMaPhong.SelectedValue); // Sử dụng ComboBox để chọn phòng
                        soGiuongConTrong = Convert.ToInt32(checkCmd.ExecuteScalar());
                    }

                    if (soGiuongConTrong <= 0)
                    {
                        MessageBox.Show("Phòng đã đầy. Vui lòng chọn phòng khác.");
                        return;
                    }

                    //string query = @"
                    //                -- Cập nhật thông tin sinh viên
                    //                UPDATE SINH_VIEN 
                    //                SET HOTEN_SV = @HoTenSV,
                    //                    CCCD = @CCCD,
                    //                    NGAY_SINH = @NgaySinh,
                    //                    GIOI_TINH = @GioiTinh,
                    //                    SDT_SINHVIEN = @SdtSV,
                    //                    SDT_NGUOITHAN = @SdtNguoiThan,
                    //                    QUE_QUAN = @QueQuan,
                    //                    EMAIL = @Email
                    //                WHERE MSSV = @MSSV;

                    //                -- Cập nhật thông tin nội trú
                    //                DECLARE @OldMaPhong INT, @OldMaGiuong NVARCHAR(50);

                    //                SELECT @OldMaPhong = MA_PHONG, @OldMaGiuong = MA_GIUONG 
                    //                FROM NOI_TRU 
                    //                WHERE MSSV = @MSSV;

                    //                UPDATE NOI_TRU
                    //                SET MA_PHONG = @MaPhong,
                    //                    MA_GIUONG = @MaGiuong,
                    //                    NGAY_BAT_DAU_NOI_TRU = @NgayBatDau,
                    //                    NGAY_KET_THUC_NOI_TRU = @NgayKetThuc,
                    //                    TRANG_THAI_NOI_TRU = @TrangThai
                    //                WHERE MSSV = @MSSV;

                    //                -- Nếu Mã Phòng thay đổi, cập nhật số giường còn trống và tình trạng phòng
                    //                IF @OldMaPhong IS NOT NULL AND @OldMaPhong != @MaPhong
                    //                BEGIN
                    //                    -- Tăng số giường trống cho phòng cũ
                    //                    UPDATE PHONG
                    //                    SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG + 1
                    //                    WHERE MA_PHONG = @OldMaPhong;

                    //                    -- Kiểm tra và cập nhật tình trạng phòng cũ
                    //                    DECLARE @OldSoGiuongConTrong INT, @OldSoGiuongToiDa INT;
                    //                    SELECT @OldSoGiuongConTrong = SO_GIUONG_CON_TRONG, @OldSoGiuongToiDa = SO_GIUONG_TOI_DA 
                    //                    FROM PHONG 
                    //                    WHERE MA_PHONG = @OldMaPhong;

                    //                    IF @OldSoGiuongConTrong = @OldSoGiuongToiDa
                    //                    BEGIN
                    //                        UPDATE PHONG
                    //                        SET TINH_TRANG_PHONG = N'Trống'
                    //                        WHERE MA_PHONG = @OldMaPhong;
                    //                    END
                    //                    ELSE IF @OldSoGiuongConTrong < @OldSoGiuongToiDa
                    //                    BEGIN
                    //                        UPDATE PHONG
                    //                        SET TINH_TRANG_PHONG = N'Đang Sử Dụng'
                    //                        WHERE MA_PHONG = @OldMaPhong;
                    //                    END
                    //                    ELSE IF @OldSoGiuongConTrong = 0
                    //                    BEGIN
                    //                        UPDATE PHONG
                    //                        SET TINH_TRANG_PHONG = N'Đầy'
                    //                        WHERE MA_PHONG = @OldMaPhong;
                    //                    END;

                    //                    -- Giảm số giường trống cho phòng mới
                    //                    UPDATE PHONG
                    //                    SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG - 1
                    //                    WHERE MA_PHONG = @MaPhong;
                    //                END;

                    //                -- Kiểm tra và cập nhật tình trạng phòng mới
                    //                DECLARE @NewSoGiuongConTrong INT, @NewSoGiuongToiDa INT;
                    //                SELECT @NewSoGiuongConTrong = SO_GIUONG_CON_TRONG, @NewSoGiuongToiDa = SO_GIUONG_TOI_DA 
                    //                FROM PHONG 
                    //                WHERE MA_PHONG = @MaPhong;

                    //                IF @NewSoGiuongConTrong = @NewSoGiuongToiDa
                    //                BEGIN
                    //                    UPDATE PHONG
                    //                    SET TINH_TRANG_PHONG = N'Trống'
                    //                    WHERE MA_PHONG = @MaPhong;
                    //                END
                    //                ELSE IF @NewSoGiuongConTrong < @NewSoGiuongToiDa
                    //                BEGIN
                    //                    UPDATE PHONG
                    //                    SET TINH_TRANG_PHONG = N'Đang Sử Dụng'
                    //                    WHERE MA_PHONG = @MaPhong;
                    //                END
                    //                ELSE IF @NewSoGiuongConTrong = 0
                    //                BEGIN
                    //                    UPDATE PHONG
                    //                    SET TINH_TRANG_PHONG = N'Đầy'
                    //                    WHERE MA_PHONG = @MaPhong;
                    //                END;

                    //                -- Nếu Mã Giường thay đổi, cập nhật trạng thái giường
                    //                IF @OldMaGiuong IS NOT NULL AND @OldMaGiuong != @MaGiuong
                    //                BEGIN
                    //                    -- Đặt trạng thái giường cũ là 'Trống'
                    //                    UPDATE GIUONG
                    //                    SET TINH_TRANG_GIUONG = N'Trống'
                    //                    WHERE MA_GIUONG = @OldMaGiuong;

                    //                    -- Đặt trạng thái giường mới là 'Đang Sử Dụng'
                    //                    UPDATE GIUONG
                    //                    SET TINH_TRANG_GIUONG = N'Đang Sử Dụng'
                    //                    WHERE MA_GIUONG = @MaGiuong;
                    //                END;

                    //               -- Khai báo biến trước khi sử dụng
                    //                DECLARE @SoGiuongConLai INT;

                    //                -- Nếu sinh viên chuyển từ 'Đang Nội Trú' sang 'Chưa Nội Trú'
                    //                IF @TrangThai = N'Chưa Nội Trú' AND @CurrentStatus = N'Đang Nội Trú'
                    //                BEGIN
                    //                    -- Tăng số giường trống cho phòng cũ
                    //                    UPDATE PHONG
                    //                    SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG + 1
                    //                    WHERE MA_PHONG = @OldMaPhong;

                    //                    -- Cập nhật trạng thái giường cũ thành 'Trống'
                    //                    UPDATE GIUONG
                    //                    SET TINH_TRANG_GIUONG = N'Trống'
                    //                    WHERE MA_GIUONG = @OldMaGiuong;

                    //                    -- Lấy số giường còn lại sau khi cập nhật
                    //                    SELECT @SoGiuongConLai = SO_GIUONG_CON_TRONG
                    //                    FROM PHONG WHERE MA_PHONG = @OldMaPhong;

                    //                    -- Cập nhật tình trạng phòng cũ
                    //                    UPDATE PHONG 
                    //                    SET TINH_TRANG_PHONG = 
                    //                        CASE 
                    //                            WHEN @SoGiuongConLai = 0 THEN N'Đầy'
                    //                            WHEN @SoGiuongConLai = @OldSoGiuongToiDa THEN N'Trống'
                    //                            ELSE N'Đang Sử Dụng'
                    //                        END
                    //                    WHERE MA_PHONG = @OldMaPhong;
                    //                END;

                    //                -- Nếu sinh viên chuyển từ 'Đang Nội Trú' sang 'Chờ Gia Hạn'
                    //                ELSE IF @TrangThai = N'Chờ Gia Hạn' AND @CurrentStatus = N'Đang Nội Trú'
                    //                BEGIN
                    //                    UPDATE NOI_TRU
                    //                    SET TRANG_THAI_NOI_TRU = N'Chờ Gia Hạn'
                    //                    WHERE MSSV = @MSSV;
                    //                END;

                    //                -- Nếu sinh viên chuyển từ 'Chờ Gia Hạn' sang 'Đang Nội Trú'
                    //                ELSE IF @TrangThai = N'Đang Nội Trú' AND @CurrentStatus = N'Chờ Gia Hạn'
                    //                BEGIN
                    //                    UPDATE NOI_TRU
                    //                    SET TRANG_THAI_NOI_TRU = N'Đang Nội Trú'
                    //                    WHERE MSSV = @MSSV;
                    //                END;

                    //                -- Nếu sinh viên chuyển từ 'Chờ Gia Hạn' sang 'Chưa Nội Trú' (Không được phép)
                    //                ELSE IF @TrangThai = N'Chưa Nội Trú' AND @CurrentStatus = N'Chờ Gia Hạn'
                    //                BEGIN
                    //                    PRINT N'Không thể chuyển từ Chờ Gia Hạn sang Chưa Nội Trú';
                    //                END;

                    //                -- Nếu sinh viên chuyển từ 'Chưa Nội Trú' sang 'Đang Nội Trú'
                    //                ELSE IF @TrangThai = N'Đang Nội Trú' AND @CurrentStatus = N'Chưa Nội Trú'
                    //                BEGIN
                    //                    -- Kiểm tra nếu phòng đầy thì không cho phép
                    //                    DECLARE @SoGiuongHienTai INT, @SoGiuongToiDa INT;
                    //                    SELECT @SoGiuongHienTai = SO_GIUONG_CON_TRONG, @SoGiuongToiDa = SO_GIUONG_TOI_DA FROM PHONG WHERE MA_PHONG = @MaPhong;

                    //                    IF @SoGiuongHienTai > 0
                    //                    BEGIN
                    //                        -- Giảm số giường trống cho phòng mới
                    //                        UPDATE PHONG
                    //                        SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG - 1
                    //                        WHERE MA_PHONG = @MaPhong;

                    //                        -- Cập nhật trạng thái giường mới thành 'Đang Sử Dụng'
                    //                        UPDATE GIUONG
                    //                        SET TINH_TRANG_GIUONG = N'Đang Sử Dụng'
                    //                        WHERE MA_GIUONG = @MaGiuong;

                    //                        -- Cập nhật trạng thái phòng sau khi thêm sinh viên
                    //                        UPDATE PHONG 
                    //                        SET TINH_TRANG_PHONG = 
                    //                            CASE 
                    //                                WHEN (@SoGiuongHienTai - 1) = 0 THEN N'Đầy'
                    //                                ELSE N'Đang Sử Dụng'
                    //                            END
                    //                        WHERE MA_PHONG = @MaPhong;
                    //                    END;
                    //                END;";

                    string query = @"
                                    -- Cập nhật thông tin sinh viên
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

                                    -- Nếu đổi phòng, cập nhật số giường và trạng thái phòng
                                    IF @OldMaPhong IS NOT NULL AND @OldMaPhong != @MaPhong
                                    BEGIN
                                        UPDATE PHONG SET SO_GIUONG_CON_TRONG += 1 WHERE MA_PHONG = @OldMaPhong;
                                        UPDATE PHONG SET SO_GIUONG_CON_TRONG -= 1 WHERE MA_PHONG = @MaPhong;

                                        -- Cập nhật trạng thái phòng cũ
                                        DECLARE @OldSoGiuongConTrong INT;
                                        SELECT @OldSoGiuongConTrong = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @OldMaPhong;
                                        UPDATE PHONG SET TINH_TRANG_PHONG = CASE
                                            WHEN @OldSoGiuongConTrong = 0 THEN N'Đầy'
                                            WHEN @OldSoGiuongConTrong = SO_GIUONG_TOI_DA THEN N'Trống'
                                            ELSE N'Đang Sử Dụng'
                                        END WHERE MA_PHONG = @OldMaPhong; 
                                    END;

                                    -- Cập nhật trạng thái phòng mới
                                    DECLARE @NewSoGiuongConTrong INT;
                                    SELECT @NewSoGiuongConTrong = SO_GIUONG_CON_TRONG FROM PHONG WHERE MA_PHONG = @MaPhong;
                                    UPDATE PHONG SET TINH_TRANG_PHONG = CASE
                                        WHEN @NewSoGiuongConTrong = 0 THEN N'Đầy'
                                        WHEN @NewSoGiuongConTrong = SO_GIUONG_TOI_DA THEN N'Trống'
                                        ELSE N'Đang Sử Dụng'
                                    END WHERE MA_PHONG = @MaPhong;

                                    -- Nếu đổi giường, cập nhật trạng thái giường
                                    IF @OldMaGiuong IS NOT NULL AND @OldMaGiuong != @MaGiuong
                                    BEGIN
                                        UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Trống' WHERE MA_GIUONG = @OldMaGiuong;
                                        UPDATE GIUONG SET TINH_TRANG_GIUONG = N'Đang Sử Dụng' WHERE MA_GIUONG = @MaGiuong;
                                    END;

                                    -- Xử lý thay đổi trạng thái nội trú
                                    DECLARE @SoGiuongConLai INT;
                                    IF @TrangThai = N'Chưa Nội Trú' AND @CurrentStatus = N'Đang Nội Trú'
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

                                    ELSE IF @TrangThai = N'Chờ Gia Hạn' AND @CurrentStatus = N'Đang Nội Trú'
                                        UPDATE NOI_TRU SET TRANG_THAI_NOI_TRU = N'Chờ Gia Hạn' WHERE MSSV = @MSSV;

                                    ELSE IF @TrangThai = N'Đang Nội Trú' AND @CurrentStatus = N'Chờ Gia Hạn'
                                        UPDATE NOI_TRU SET TRANG_THAI_NOI_TRU = N'Đang Nội Trú' WHERE MSSV = @MSSV;

                                    ELSE IF @TrangThai = N'Chưa Nội Trú' AND @CurrentStatus = N'Chờ Gia Hạn'
                                        PRINT N'Không thể chuyển từ Chờ Gia Hạn sang Chưa Nội Trú';

                                    ELSE IF @TrangThai = N'Đang Nội Trú' AND @CurrentStatus = N'Chưa Nội Trú'
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
            if (string.IsNullOrWhiteSpace(textBoxMSSV.Text))
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
                DeleteStudent();
            }
        }

        private void DeleteStudent()
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Lấy thông tin phòng và giường trước khi xóa
                    string getRoomAndBedQuery = @"
                                                SELECT MA_PHONG, MA_GIUONG 
                                                FROM NOI_TRU 
                                                WHERE MSSV = @MSSV;";

                    int? maPhong = null;
                    int? maGiuong = null;

                    using (var cmdGetInfo = new SqlCommand(getRoomAndBedQuery, conn))
                    {
                        cmdGetInfo.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);

                        using (var reader = cmdGetInfo.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                maPhong = reader["MA_PHONG"] as int?;
                                maGiuong = reader["MA_GIUONG"] as int?;
                            }
                        }
                    }

                    // Xóa thông tin sinh viên và nội trú
                    string deleteQuery = @"
                                        DELETE FROM NOI_TRU WHERE MSSV = @MSSV;
                                        DELETE FROM SINH_VIEN WHERE MSSV = @MSSV;";

                    using (var cmdDelete = new SqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MSSV", textBoxMSSV.Text);

                        // Thực thi câu lệnh SQL
                        int rowsAffected = cmdDelete.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa sinh viên thành công!");

                            // Cập nhật trạng thái giường và phòng nếu thông tin tồn tại
                            if (maPhong.HasValue && maGiuong.HasValue)
                            {
                                UpdateRoomAndBedStatus(conn, maPhong.Value, maGiuong.Value);
                            }

                            LoadData(); // Tải lại dữ liệu để cập nhật DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để xóa.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
                }
            }
        }

        private void UpdateRoomAndBedStatus(SqlConnection conn, int maPhong, int maGiuong)
        {
            try
            {
                // Đặt trạng thái giường là 'Trống'
                string updateBedQuery = @"
                                        UPDATE GIUONG
                                        SET TINH_TRANG_GIUONG = N'Trống'
                                        WHERE MA_GIUONG = @MaGiuong;";

                using (var cmdUpdateBed = new SqlCommand(updateBedQuery, conn))
                {
                    cmdUpdateBed.Parameters.AddWithValue("@MaGiuong", maGiuong);
                    cmdUpdateBed.ExecuteNonQuery();
                }

                // Tăng số giường trống trong phòng
                string updateRoomQuery = @"
                                        UPDATE PHONG
                                        SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG + 1
                                        WHERE MA_PHONG = @MaPhong;

                                        -- Cập nhật trạng thái phòng
                                        DECLARE @SoGiuongConTrong INT, @SoGiuongToiDa INT;
                                        SELECT @SoGiuongConTrong = SO_GIUONG_CON_TRONG, @SoGiuongToiDa = SO_GIUONG_TOI_DA
                                        FROM PHONG
                                        WHERE MA_PHONG = @MaPhong;

                                        IF @SoGiuongConTrong = @SoGiuongToiDa
                                        BEGIN
                                            UPDATE PHONG
                                            SET TINH_TRANG_PHONG = N'Trống'
                                            WHERE MA_PHONG = @MaPhong;
                                        END
                                        ELSE IF @SoGiuongConTrong = 0
                                        BEGIN
                                            UPDATE PHONG
                                            SET TINH_TRANG_PHONG = N'Đầy'
                                            WHERE MA_PHONG = @MaPhong;
                                        END
                                        ELSE
                                        BEGIN
                                            UPDATE PHONG
                                            SET TINH_TRANG_PHONG = N'Đang Sử Dụng'
                                            WHERE MA_PHONG = @MaPhong;
                                        END;";

                using (var cmdUpdateRoom = new SqlCommand(updateRoomQuery, conn))
                {
                    cmdUpdateRoom.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmdUpdateRoom.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái giường và phòng: " + ex.Message);
            }
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
                    string query = @"SELECT SINH_VIEN.MSSV,
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
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU
                             FROM SINH_VIEN 
                             INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                             WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Đang nội trú'"; // Điều kiện lọc sinh viên đang nội trú
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Gọi hàm cập nhật labelChiSo
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
                    string query = @"SELECT SINH_VIEN.MSSV,
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
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU
                             FROM SINH_VIEN 
                             INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                             WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Chưa nội trú'"; // Điều kiện lọc sinh viên chưa nội trú
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Gọi hàm cập nhật labelChiSo
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
                    string query = @"SELECT SINH_VIEN.MSSV,
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
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU
                             FROM SINH_VIEN 
                             INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                             WHERE NOI_TRU.TRANG_THAI_NOI_TRU = N'Chờ Gia Hạn'"; // Điều kiện lọc sinh viên chờ gia hạn
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Gọi hàm cập nhật labelChiSo
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
                    string query = @"SELECT SINH_VIEN.MSSV,
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
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU
                             FROM SINH_VIEN
                             INNER JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                             WHERE NOI_TRU.NGAY_KET_THUC_NOI_TRU < GETDATE()"; // Điều kiện lọc sinh viên hết thời gian nội trú
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Gọi hàm cập nhật labelChiSo
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
            string maSinhVien = textBoxTimKiemMSSV.Text.Trim(); // Lấy mã sinh viên từ TextBox

            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT SINH_VIEN.MSSV,
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
                                    NOI_TRU.NGAY_BAT_DAU_NOI_TRU,
                                    NOI_TRU.NGAY_KET_THUC_NOI_TRU,
                                    NOI_TRU.TRANG_THAI_NOI_TRU
                             FROM SINH_VIEN
                             LEFT JOIN NOI_TRU ON SINH_VIEN.MSSV = NOI_TRU.MSSV
                             WHERE SINH_VIEN.MSSV = @MSSV"; // Tìm sinh viên theo mã
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@MSSV", maSinhVien); // Thêm tham số mã sinh viên
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Kiểm tra nếu không có kết quả
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy sinh viên với mã: " + maSinhVien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Gọi hàm cập nhật labelChiSo
                    UpdateLabelChiSo(dataTable.Rows.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xác nhận nội trú cho tất cả sinh viên chưa nội trú không?",
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

                    // Lấy danh sách sinh viên chưa nội trú cùng với thông tin phòng và giường
                    string query = @"
                SELECT MSSV, MA_PHONG, MA_GIUONG 
                FROM NOI_TRU 
                WHERE TRANG_THAI_NOI_TRU = N'Chưa Nội Trú'";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string mssv = row["MSSV"].ToString();
                            int maPhong = Convert.ToInt32(row["MA_PHONG"]);
                            int maGiuong = Convert.ToInt32(row["MA_GIUONG"]);

                            // Cập nhật trạng thái nội trú của sinh viên (không thay đổi NGAY_BAT_DAU_NOI_TRU)
                            string updateQuery = @"
                                                    UPDATE NOI_TRU 
                                                    SET TRANG_THAI_NOI_TRU = N'Đang Nội Trú' 
                                                    WHERE MSSV = @MSSV";

                            using (var command = new SqlCommand(updateQuery, conn))
                            {
                                command.Parameters.AddWithValue("@MSSV", mssv);
                                command.ExecuteNonQuery();
                            }

                            // Giảm số giường trống trong phòng
                            string updatePhongQuery = @"
                                                        UPDATE PHONG
                                                        SET SO_GIUONG_CON_TRONG = SO_GIUONG_CON_TRONG - 1
                                                        WHERE MA_PHONG = @MaPhong";

                            using (var cmdUpdatePhong = new SqlCommand(updatePhongQuery, conn))
                            {
                                cmdUpdatePhong.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmdUpdatePhong.ExecuteNonQuery();
                            }

                            // Cập nhật trạng thái giường thành "Đang Sử Dụng"
                            string updateGiuongQuery = @"
                                                        UPDATE GIUONG
                                                        SET TINH_TRANG_GIUONG = N'Đang Sử Dụng'
                                                        WHERE MA_GIUONG = @MaGiuong";

                            using (var cmdUpdateGiuong = new SqlCommand(updateGiuongQuery, conn))
                            {
                                cmdUpdateGiuong.Parameters.AddWithValue("@MaGiuong", maGiuong);
                                cmdUpdateGiuong.ExecuteNonQuery();
                            }

                            // Cập nhật trạng thái phòng dựa trên số giường trống
                            string updateTinhTrangPhongQuery = @"
                                                                DECLARE @SoGiuongConTrong INT, @SoGiuongToiDa INT;
                                                                SELECT @SoGiuongConTrong = SO_GIUONG_CON_TRONG, @SoGiuongToiDa = SO_GIUONG_TOI_DA 
                                                                FROM PHONG 
                                                                WHERE MA_PHONG = @MaPhong;

                                                                IF @SoGiuongConTrong = 0
                                                                BEGIN
                                                                    UPDATE PHONG
                                                                    SET TINH_TRANG_PHONG = N'Đầy'
                                                                    WHERE MA_PHONG = @MaPhong;
                                                                END
                                                                ELSE IF @SoGiuongConTrong < @SoGiuongToiDa
                                                                BEGIN
                                                                    UPDATE PHONG
                                                                    SET TINH_TRANG_PHONG = N'Đang Sử Dụng'
                                                                    WHERE MA_PHONG = @MaPhong;
                                                                END;";

                            using (var cmdUpdateTinhTrangPhong = new SqlCommand(updateTinhTrangPhongQuery, conn))
                            {
                                cmdUpdateTinhTrangPhong.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmdUpdateTinhTrangPhong.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Đã xác nhận nội trú cho tất cả sinh viên chưa nội trú.");
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào trong trạng thái 'Chưa Nội Trú' cần xác nhận.");
                    }

                    // Cập nhật lại dữ liệu trong DataGridView
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xác nhận nội trú: " + ex.Message);
                }
            }
        }


        // Kiểm tra trạng thái nút bấm trước khi Form hiển thị
        private void QuanLiSinhVien_Load(object sender, EventArgs e)
        {
            CheckButtonXacNhanNoiTruAll();
        }

        // Hàm kiểm tra và bật/tắt nút Xác nhận nội trú
        private void CheckButtonXacNhanNoiTruAll()
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) FROM SINH_VIEN WHERE MSSV NOT IN (SELECT MSSV FROM NOI_TRU)";
                    SqlCommand command = new SqlCommand(query, conn);
                    int count = (int)command.ExecuteScalar();

                    // Bật nút nếu có sinh viên chưa nội trú, tắt nút nếu không có
                    btnXacNhanNoiTruAll.Enabled = count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra sinh viên chưa nội trú: " + ex.Message);
                }
            }
        }

        private void buttonChoGiaHanNoiTruAll_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn chuyển trạng thái 'Chờ Gia Hạn' cho tất cả sinh viên đã quá hạn nội trú không?",
                                                  "Xác Nhận Chờ Gia Hạn",
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
                        // Chuyển trạng thái Chờ Gia Hạn cho tất cả sinh viên đã quá hạn nội trú
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string mssv = row["MSSV"].ToString();
                            string updateQuery = @"UPDATE NOI_TRU 
                                           SET TRANG_THAI_NOI_TRU = N'Chờ Gia Hạn' 
                                           WHERE MSSV = @MSSV";

                            using (var command = new SqlCommand(updateQuery, conn))
                            {
                                command.Parameters.AddWithValue("@MSSV", mssv);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Đã chuyển trạng thái 'Chờ Gia Hạn' cho tất cả sinh viên đã quá hạn nội trú.");
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
                    MessageBox.Show("Lỗi khi cập nhật trạng thái chờ gia hạn: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
