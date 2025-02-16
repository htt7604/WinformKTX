using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformKTX
{
    public partial class CoSoVatChat : Form
    {
        // Khởi tạo đối tượng KetnoiCSDL để sử dụng phương thức GetConnection
        KetnoiCSDL ketnoi = new KetnoiCSDL();
        public CoSoVatChat()
        {
            InitializeComponent();

            comboBoxLoaiPhong.Enabled = false;
            comboBoxLocPhong.Enabled = false;

            SqlConnection conn = ketnoi.GetConnection();

            TaiDanhSachTrangThai();
            TaiDanhSachLoaiPhong(conn);
            TaiDanhSachTang(conn);
            TaiDanhSachTenCSVC(conn);

            comboBoxLoaiPhong.SelectedIndexChanged += comboBoxChonLoaiPhong_Click;
            comboBoxMaTang.SelectedIndexChanged += comboBoxChonTang_Click;

            comboBoxLocTang.SelectedIndexChanged += comboBoxLocTang_Click;
            comboBoxLocPhong.SelectedIndexChanged += comboBoxLocPhong_Click;
            comboBoxLocTenCSVC.SelectedIndexChanged += comboBoxLocTen_Click;
        }
        // Hàm để vô hiệu hóa tất cả các nút
        private void VoHieuHoaTatCaNut()
        {
            //buttonThem.Enabled = false;
            //buttonSua.Enabled = false;
            //buttonXoa.Enabled = false;
            //buttonLuu.Enabled = false;
            //buttonHuy.Enabled = false;
            //comboBoxChonHinhThuc.Enabled = false;
            comboBoxLoaiPhong.Enabled = false;
        }

        // Hàm để kích hoạt tất cả các nút
        private void KichHoatTatCaNut()
        {
            //buttonThem.Enabled = true;
            //buttonSua.Enabled = true;
            //buttonXoa.Enabled = true;
            //buttonLuu.Enabled = true;
            //buttonHuy.Enabled = true;
            //comboBoxChonHinhThuc.Enabled = true;
            comboBoxLoaiPhong.Enabled = true;
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

                    // Thêm dòng "Tất Cả Tầng" vào DataTable
                    DataRow newRow = table.NewRow();
                    newRow["MA_TANG"] = DBNull.Value;
                    newRow["TEN_TANG"] = "Tất Cả Tầng";
                    table.Rows.InsertAt(newRow, 0); // Chèn vào vị trí đầu tiên

                    comboBoxLocTang.DataSource = table;
                    comboBoxLocTang.DisplayMember = "TEN_TANG";  // Hiển thị tên tầng
                    comboBoxLocTang.ValueMember = "MA_TANG";  // Xử lý logic theo mã tầng
                    comboBoxLocTang.SelectedIndex = 0;
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

                    comboBoxLoaiPhong.DataSource = table;
                    comboBoxLoaiPhong.DisplayMember = "TEN_LOAI_PHONG";  // Hiển thị tên loại tầng
                    comboBoxLoaiPhong.ValueMember = "MA_LOAI_PHONG";  // Xử lý logic theo mã loại tầng
                    comboBoxLoaiPhong.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại phòng: " + ex.Message);
            }
        }

        private void TaiDanhSachTenCSVC(SqlConnection conn)
        {
            try
            {
                // Truy vấn danh sách CSVC
                string query = @"SELECT DISTINCT TEN_CSVC FROM CO_SO_VAT_CHAT";
                using (var cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Thêm tùy chọn "Tất Cả CSVC"
                    DataRow newRow = table.NewRow();
                    newRow["TEN_CSVC"] = "Tất Cả CSVC";
                    table.Rows.InsertAt(newRow, 0);

                    // Gán dữ liệu vào comboBox
                    comboBoxLocTenCSVC.DataSource = table;
                    comboBoxLocTenCSVC.DisplayMember = "TEN_CSVC";  // Hiển thị tên CSVC
                    comboBoxLocTenCSVC.ValueMember = "TEN_CSVC";    // Xử lý logic cũng theo tên CSVC
                    comboBoxLocTenCSVC.SelectedIndex = 0; // Mặc định chọn "Tất Cả CSVC"
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách CSVC: " + ex.Message);
            }
        }


        private void TaiDanhSachTrangThai()
        {
            try
            {
                comboBoxLocTrangThai.Items.Clear();
                comboBoxLocTrangThai.Items.AddRange(new string[] { "Tất Cả Trạng Thái", "Hư Hỏng", "Có Thể Sử Dụng" });
                comboBoxLocTrangThai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message);
            }
        }

        private void comboBoxChonThaoTac_Click(object sender, EventArgs e)
        {
            // Nếu chưa chọn thao tác, vô hiệu hóa tất cả nút
            if (comboBoxChonThaoTac.SelectedItem == null)
            {
                VoHieuHoaTatCaNut();
                return;
            }
            else
            {
                KichHoatTatCaNut();
            }

            string thaotac = comboBoxChonThaoTac.SelectedItem.ToString();

            using (SqlConnection con = ketnoi.GetConnection())
            {
                con.Open();

                // Xóa các mục cũ trong comboBoxChonHinhThuc trước khi thêm mới
                comboBoxChonHinhThuc.Items.Clear();

                if (thaotac == "Thêm")
                {
                    comboBoxChonHinhThuc.Items.AddRange(new string[] { "Thêm CSVC Vào Phòng", "Thêm CSVC Vào Phòng Trong Tầng", "Thêm CSVC Vào Tất Cả Phòng" });
                }
                else if (thaotac == "Sửa")
                {
                    comboBoxChonHinhThuc.Items.AddRange(new string[] { "Sửa CSVC Của Phòng", "Sửa CSVC Của Phòng Trong Tầng", "Sửa CSVC Của Tất Cả Phòng" });
                }
                else if (thaotac == "Xóa")
                {
                    comboBoxChonHinhThuc.Items.AddRange(new string[] { "Xóa Của Phòng", "Xóa CSVC Của Phòng Trong Tầng", "Xóa CSVC Của Tất Cả Phòng" });
                }

                // Chọn mặc định mục đầu tiên nếu có
                if (comboBoxChonHinhThuc.Items.Count > 0)
                {
                    comboBoxChonHinhThuc.SelectedIndex = 0;
                }
            }
        }


        private void comboBoxChonHinhThuc_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxChonLoaiPhong_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu chưa chọn loại phòng
                if (comboBoxLoaiPhong.SelectedValue == null)
                {
                    return;
                }

                // Lấy giá trị thực tế từ SelectedValue
                object selectedValue = comboBoxLoaiPhong.SelectedValue;
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

                    // Truy vấn danh sách tầng theo mã loại phòng
                    string query = @"
                SELECT DISTINCT T.MA_TANG, T.TEN_TANG
                FROM TANG T
                JOIN PHONG P ON T.MA_TANG = P.MA_TANG
                WHERE P.MA_LOAI_PHONG = @MaLoaiPhong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoaiPhong", maLoaiPhong);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Gán dữ liệu cho combobox tầng
                        comboBoxMaTang.DataSource = table;
                        comboBoxMaTang.DisplayMember = "TEN_TANG";
                        comboBoxMaTang.ValueMember = "MA_TANG";
                        comboBoxMaTang.SelectedIndex = -1; // Không chọn sẵn

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message);
            }
        }



        private void comboBoxChonTang_Click(object sender, EventArgs e)
        {
            if (comboBoxMaTang.SelectedValue == null) { return; }
            // Lấy giá trị thực tế từ comboBoxMaTang
            object tangValue = comboBoxMaTang.SelectedValue;
            string selectedMaTang;

            if (tangValue is DataRowView tangRowView)
            {
                selectedMaTang = tangRowView["MA_TANG"].ToString();
            }
            else
            {
                selectedMaTang = tangValue?.ToString();
            }

            // Lấy giá trị thực tế từ comboBoxMaLoaiPhong
            object loaiPhongValue = comboBoxLoaiPhong.SelectedValue;
            string selectedMaLoaiPhong;

            if (loaiPhongValue is DataRowView loaiPhongRowView)
            {
                selectedMaLoaiPhong = loaiPhongRowView["MA_LOAI_PHONG"].ToString();
            }
            else
            {
                selectedMaLoaiPhong = loaiPhongValue?.ToString();
            }

            // Kiểm tra chuyển đổi mã tầng
            if (!int.TryParse(selectedMaTang, out int maTang))
            {
                MessageBox.Show("Mã tầng không hợp lệ.");
                return;
            }

            // Kiểm tra chuyển đổi mã loại phòng
            if (!int.TryParse(selectedMaLoaiPhong, out int maLoaiPhong))
            {
                MessageBox.Show("Mã loại phòng không hợp lệ.");
                return;
            }
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();

                string query = @"
                                SELECT MA_PHONG, TEN_PHONG 
                                FROM PHONG 
                                WHERE MA_TANG = @MaTang 
                                  AND MA_LOAI_PHONG = @MaLoaiPhong";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Truyền tham số từ comboBox
                    cmd.Parameters.AddWithValue("@MaTang", maTang);
                    cmd.Parameters.AddWithValue("@MaLoaiPhong", maLoaiPhong);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        comboBoxChonPhong.DataSource = table;
                        comboBoxChonPhong.DisplayMember = "TEN_PHONG";
                        comboBoxChonPhong.ValueMember = "MA_PHONG";
                        comboBoxChonPhong.SelectedIndex = -1;
                    }
                }
            }
        }
        private void comboBoxLocTang_Click(object sender, EventArgs e)
        {
            // Lấy giá trị thực tế từ comboBoxLocTang
            object tangValue = comboBoxLocTang.SelectedValue;
            string selectedMaTang;

            // Lấy giá trị từ comboBoxLocTrangThai
            string selectedTrangThai = comboBoxLocTrangThai?.SelectedItem?.ToString();
            
            // Xử lý khi giá trị là DBNull hoặc null
            if (tangValue == DBNull.Value || tangValue == null)
            {
                selectedMaTang = null;
            }
            else if (tangValue is DataRowView tangRowView)
            {
                selectedMaTang = tangRowView["MA_TANG"].ToString();
            }
            else
            {
                selectedMaTang = tangValue?.ToString();
            }

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();
                // Truy vấn khi khác null và khác "Tất Cả Tầng" (theo mã tầng)
                if (!string.IsNullOrEmpty(selectedMaTang) && selectedMaTang != "Tất Cả Tầng")
                {
                    comboBoxLocPhong.Enabled = true;

                    string query2 = @"
                            SELECT 
                                CO_SO_VAT_CHAT.MA_CSVC,
                                PHONG.TEN_PHONG, 
                                CO_SO_VAT_CHAT.TEN_CSVC, 
                                CO_SO_VAT_CHAT.SO_LUONG, 
                                CO_SO_VAT_CHAT.TINH_TRANG, 
                                CO_SO_VAT_CHAT.GHI_CHU
                            FROM CO_SO_VAT_CHAT 
                            INNER JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG
                            WHERE 
                                PHONG.MA_TANG = @MaTang 
                                AND (@TrangThai IS NULL OR CO_SO_VAT_CHAT.TINH_TRANG = @TrangThai)";

                    using (SqlCommand cmd = new SqlCommand(query2, conn))
                    {
                        // Truyền tham số mã tầng
                        cmd.Parameters.AddWithValue("@MaTang", selectedMaTang);

                        // Truyền tham số trạng thái
                        if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                        }

                        using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable2 = new DataTable();
                            adapter2.Fill(dataTable2);

                            // Hiển thị dữ liệu lên DataGridView
                            dataGridView1.DataSource = dataTable2;

                            // Đổi tên cột hiển thị trên DataGridView
                            dataGridView1.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                            dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridView1.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                            dataGridView1.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                            dataGridView1.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                            dataGridView1.Columns["GHI_CHU"].HeaderText = "Ghi Chú";
                        }
                    }
                    // Kiểm tra chuyển đổi mã tầng
                    if (!int.TryParse(selectedMaTang, out int maTang))
                    {
                        MessageBox.Show("Mã tầng không hợp lệ.");
                        return;
                    }

                    // Truy vấn danh sách phòng theo tầng
                    string query = @"
                        SELECT MA_PHONG, TEN_PHONG 
                        FROM PHONG 
                        WHERE MA_TANG = @MaTang";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Truyền tham số từ comboBox
                        cmd.Parameters.AddWithValue("@MaTang", maTang);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            comboBoxLocPhong.DataSource = table;
                            comboBoxLocPhong.DisplayMember = "TEN_PHONG";
                            comboBoxLocPhong.ValueMember = "MA_PHONG";
                            comboBoxLocPhong.SelectedIndex = -1;
                        }
                    }

                    // Thoát khỏi hàm
                    return;
                }


                // Truy vấn theo tầng và trạng thái
                if (selectedMaTang == null || selectedMaTang == "Tất Cả Tầng")
                {
                    comboBoxLocPhong.Enabled = false;
                    string query1 = @"
                            SELECT 
                                CO_SO_VAT_CHAT.MA_CSVC,
                                PHONG.TEN_PHONG, 
                                CO_SO_VAT_CHAT.TEN_CSVC, 
                                CO_SO_VAT_CHAT.SO_LUONG, 
                                CO_SO_VAT_CHAT.TINH_TRANG, 
                                CO_SO_VAT_CHAT.GHI_CHU
                            FROM CO_SO_VAT_CHAT 
                            INNER JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG
                            WHERE 
                                (@TrangThai IS NULL OR CO_SO_VAT_CHAT.TINH_TRANG = @TrangThai)";

                    using (SqlCommand cmd = new SqlCommand(query1, conn))
                    {
                        if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                        }
                        using (SqlDataAdapter adapter1 = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable1 = new DataTable();
                            adapter1.Fill(dataTable1);

                            // Hiển thị dữ liệu lên DataGridView
                            dataGridView1.DataSource = dataTable1;

                            // Đổi tên cột hiển thị trên DataGridView
                            dataGridView1.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                            dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridView1.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                            dataGridView1.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                            dataGridView1.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                            dataGridView1.Columns["GHI_CHU"].HeaderText = "Ghi Chú";
                        }
                    }

                    // Thoát khỏi hàm để không tiếp tục xử lý mã tầng
                    return;
                }

                // Nếu chọn "Tất Cả Tầng" hoặc không có mã tầng thì thoát
                if (string.IsNullOrEmpty(selectedMaTang))
                {
                    // Xóa dữ liệu phòng khi chọn "Tất Cả Tầng"
                    comboBoxLocPhong.DataSource = null;
                    comboBoxLocPhong.Items.Clear();
                    return;
                }


            }
        }
        private void comboBoxLocPhong_Click(object sender, EventArgs e)
        {
            if (comboBoxLocPhong.SelectedValue == null) { return; }
            // Lấy giá trị mã tầng từ comboBoxLocTang (kiểu int?, có thể null)
            int? selectedMaTang = comboBoxLocTang?.SelectedValue as int?;

            // Lấy giá trị mã phòng từ comboBoxLocPhong (kiểu int?, có thể null)
            int? selectedMaPhong = comboBoxLocPhong?.SelectedValue as int?;

            // Lấy giá trị trạng thái từ comboBoxLocTrangThai
            string selectedTrangThai = comboBoxLocTrangThai?.SelectedItem?.ToString();

            using (SqlConnection conn = ketnoi.GetConnection())
            {
                conn.Open();

                // Truy vấn danh sách CSVC theo Mã tầng, Mã phòng (có thể null) và Trạng thái
                string query = @"
            SELECT 
                CO_SO_VAT_CHAT.MA_CSVC,
                PHONG.TEN_PHONG, 
                CO_SO_VAT_CHAT.TEN_CSVC, 
                CO_SO_VAT_CHAT.SO_LUONG, 
                CO_SO_VAT_CHAT.TINH_TRANG, 
                CO_SO_VAT_CHAT.GHI_CHU
            FROM CO_SO_VAT_CHAT
            LEFT JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG
            WHERE 
                (@MaTang IS NULL OR PHONG.MA_TANG = @MaTang) AND
                (@MaPhong IS NULL OR CO_SO_VAT_CHAT.MA_PHONG = @MaPhong OR CO_SO_VAT_CHAT.MA_PHONG IS NULL) AND
                (@TrangThai IS NULL OR CO_SO_VAT_CHAT.TINH_TRANG = @TrangThai)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số Mã tầng (int?)
                    if (selectedMaTang.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@MaTang", selectedMaTang.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MaTang", DBNull.Value);
                    }

                    // Thêm tham số Mã phòng (int?)
                    if (selectedMaPhong.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", selectedMaPhong.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", DBNull.Value);
                    }

                    // Thêm tham số Trạng thái
                    if (string.IsNullOrEmpty(selectedTrangThai) || selectedTrangThai == "Tất Cả Trạng Thái")
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                    }

                    // Đổ dữ liệu vào DataGridView
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

                        // Đổi tên cột hiển thị
                        dataGridView1.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                        dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                        dataGridView1.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                        dataGridView1.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                        dataGridView1.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                        dataGridView1.Columns["GHI_CHU"].HeaderText = "Ghi Chú";
                    }
                }
            }
        }

        private void comboBoxLocTen_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxLocTenCSVC.SelectedValue == null) { return; }
                // Lấy giá trị từ comboBoxLocTenCSVC
                string selectedTenCSVC = comboBoxLocTenCSVC.SelectedValue.ToString();
                MessageBox.Show("Tên CSVC được chọn: " + selectedTenCSVC);

                // Lấy giá trị từ comboBoxLocTang
                object tangValue = comboBoxLocTang.SelectedValue;
                string selectedMaTang;

                // Xử lý khi giá trị là DBNull hoặc null
                if (tangValue == DBNull.Value || tangValue == null)
                {
                    selectedMaTang = null;
                }
                else if (tangValue is DataRowView tangRowView)
                {
                    selectedMaTang = tangRowView["MA_TANG"].ToString();
                }
                else
                {
                    selectedMaTang = tangValue?.ToString();
                }
                // Kiểm tra chuyển đổi mã tầng
                
                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    conn.Open();

                    // Trường hợp chọn 1 tầng cụ thể
                    if (!string.IsNullOrEmpty(selectedMaTang) && selectedMaTang != "Tất Cả Tầng")
                    {
                        
                        string query = @"
                                    SELECT 
                                        CO_SO_VAT_CHAT.MA_CSVC,
                                        PHONG.TEN_PHONG, 
                                        CO_SO_VAT_CHAT.TEN_CSVC, 
                                        CO_SO_VAT_CHAT.SO_LUONG, 
                                        CO_SO_VAT_CHAT.TINH_TRANG, 
                                        CO_SO_VAT_CHAT.GHI_CHU
                                    FROM CO_SO_VAT_CHAT 
                                    INNER JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG
                                    WHERE 
                                        PHONG.MA_TANG = @MaTang 
                                        AND (@TenCSVC IS NULL OR CO_SO_VAT_CHAT.TEN_CSVC = @TenCSVC)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaTang", selectedMaTang);

                            if (string.IsNullOrEmpty(selectedTenCSVC) || selectedTenCSVC == "Tất Cả CSVC")
                            {
                                cmd.Parameters.AddWithValue("@TenCSVC", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TenCSVC", selectedTenCSVC);
                            }

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridView1.DataSource = dataTable;

                                // Đổi tên cột hiển thị
                                dataGridView1.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                                dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                                dataGridView1.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                                dataGridView1.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                                dataGridView1.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                                dataGridView1.Columns["GHI_CHU"].HeaderText = "Ghi Chú";
                            }
                        }
                    }
                    // Trường hợp "Tất Cả Tầng"
                    else
                    {
                        string query = @"
                SELECT 
                    CO_SO_VAT_CHAT.MA_CSVC,
                    PHONG.TEN_PHONG, 
                    CO_SO_VAT_CHAT.TEN_CSVC, 
                    CO_SO_VAT_CHAT.SO_LUONG, 
                    CO_SO_VAT_CHAT.TINH_TRANG, 
                    CO_SO_VAT_CHAT.GHI_CHU
                FROM CO_SO_VAT_CHAT 
                INNER JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG
                WHERE 
                    (@TenCSVC IS NULL OR CO_SO_VAT_CHAT.TEN_CSVC = @TenCSVC)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            if (string.IsNullOrEmpty(selectedTenCSVC) || selectedTenCSVC == "Tất Cả CSVC")
                            {
                                cmd.Parameters.AddWithValue("@TenCSVC", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TenCSVC", selectedTenCSVC);
                            }

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridView1.DataSource = dataTable;

                                // Đổi tên cột hiển thị
                                dataGridView1.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                                dataGridView1.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                                dataGridView1.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                                dataGridView1.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                                dataGridView1.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                                dataGridView1.Columns["GHI_CHU"].HeaderText = "Ghi Chú";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách CSVC: " + ex.Message);
            }
        }

    }
}
