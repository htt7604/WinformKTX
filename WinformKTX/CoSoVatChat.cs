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
            comboBoxMaTang.Enabled = false;
            comboBoxChonPhong.Enabled = false;
            textBoxTenCSVC.Enabled = false;
            textBoxNhapSoLuong.Enabled = false;
            comboBoxChonTinhTrang.Enabled = false;
            textBoxGhiChu.Enabled = false;

            buttonThemCSVC.Enabled = false;
            buttonSuaCSVC.Enabled = false;
            buttonXoaCSVC.Enabled = false;

            comboBoxLocPhong.Enabled = false;

            SqlConnection conn = ketnoi.GetConnection();

            TaiDanhSachTrangThai();
            TaiDanhSachLoaiPhong(conn);
            TaiDanhSachTang(conn);
            TaiDanhSachTenCSVC(conn);

            comboBoxChonHinhThuc.SelectedIndexChanged += comboBoxChonHinhThuc_Click;
            comboBoxLoaiPhong.SelectedIndexChanged += comboBoxChonLoaiPhong_Click;
            comboBoxMaTang.SelectedIndexChanged += comboBoxChonTang_Click;

            comboBoxLocTang.SelectedIndexChanged += comboBoxLocTang_Click;
            comboBoxLocPhong.SelectedIndexChanged += comboBoxLocPhong_Click;
            comboBoxLocTenCSVC.SelectedIndexChanged += comboBoxLocTen_Click;
            comboBoxLocTrangThai.SelectedIndexChanged += comboBoxLocTrangThai_Click;

            LoadCSVC();
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

        private void LoadCSVC()
        {
            try
            {
                using (SqlConnection conn = ketnoi.GetConnection()) // Kết nối CSDL
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    CO_SO_VAT_CHAT.MA_CSVC,
                    CO_SO_VAT_CHAT.MA_PHONG,
                    PHONG.TEN_PHONG, 
                    CO_SO_VAT_CHAT.TEN_CSVC, 
                    CO_SO_VAT_CHAT.SO_LUONG, 
                    CO_SO_VAT_CHAT.TINH_TRANG, 
                    CO_SO_VAT_CHAT.GHI_CHU
                FROM CO_SO_VAT_CHAT 
                INNER JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridViewCSVC.DataSource = dataTable;

                            // Đổi tên cột hiển thị
                            dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                            dataGridViewCSVC.Columns["MA_PHONG"].Visible = false;
                            dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                            dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                            dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                            dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";



                            UpdateLabelChiSo(dataTable.Rows.Count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách CSVC: " + ex.Message);
            }
        }
        private void dataGridViewCSVC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Kiểm tra nếu comboBoxChonThaoTac chưa được chọn
                    if (string.IsNullOrWhiteSpace(comboBoxChonThaoTac.Text))
                    {
                        MessageBox.Show("Vui lòng chọn thao tác trước khi thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Dừng thực hiện nếu chưa chọn thao tác
                    }

                    //ResetInputFields(); // Xóa dữ liệu trước khi hiển thị mới
                    DataGridViewRow row = dataGridViewCSVC.Rows[e.RowIndex];

                    string GetSafeValue(object cellValue)
                    {
                        return cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()) ? cellValue.ToString() : string.Empty;
                    }

                    // Lấy MA_PHONG từ dòng được chọn
                    object maPhongValue = row.Cells["MA_PHONG"].Value;
                    if (maPhongValue != null)
                    {
                        string maPhong = maPhongValue.ToString();

                        // Truy vấn cơ sở dữ liệu để lấy tên Phòng, Loại Phòng và Tầng
                        string query = @"
                                    SELECT P.TEN_PHONG, T.TEN_TANG, LP.TEN_LOAI_PHONG
                                    FROM PHONG P
                                    JOIN TANG T ON P.MA_TANG = T.MA_TANG
                                    JOIN LOAI_PHONG LP ON P.MA_LOAI_PHONG = LP.MA_LOAI_PHONG
                                    WHERE P.MA_PHONG = @MaPhong";

                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        // Gán dữ liệu vào ComboBox theo tên (không phải mã)
                                        if (comboBoxLoaiPhong.Enabled)
                                        {
                                            comboBoxLoaiPhong.Text = reader["TEN_LOAI_PHONG"].ToString();
                                        }

                                        if (comboBoxMaTang.Enabled)
                                        {
                                            comboBoxMaTang.Text = reader["TEN_TANG"].ToString();
                                        }
                                        if (comboBoxChonPhong.Enabled)
                                        {
                                            comboBoxChonPhong.Text = reader["TEN_PHONG"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Gán dữ liệu vào các ô nhập liệu
                    if (comboBoxChonPhong.Enabled)
                    {
                        SetComboBoxValue(comboBoxChonPhong, row.Cells["MA_PHONG"].Value);
                    }
                    textBoxTenCSVC.Text = GetSafeValue(row.Cells["TEN_CSVC"].Value);
                    textBoxNhapSoLuong.Text = GetSafeValue(row.Cells["SO_LUONG"].Value);
                    textBoxGhiChu.Text = GetSafeValue(row.Cells["GHI_CHU"].Value);
                    comboBoxChonTinhTrang.Text = GetSafeValue(row.Cells["TINH_TRANG"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message);
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
        private void ResetInputFields()
        {
            // Xóa dữ liệu trong các TextBox
            //textBoxMaCSVC.Clear();
            textBoxTenCSVC.Text = "";
            textBoxNhapSoLuong.Text = "";
            textBoxGhiChu.Text = "";

            // Đặt lại giá trị mặc định cho ComboBox
            comboBoxLocTrangThai.SelectedIndex = -1;  // Không chọn gì
            comboBoxChonPhong.SelectedIndex = -1;
            comboBoxMaTang.SelectedIndex = -1;
            comboBoxLoaiPhong.SelectedIndex = -1;
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
                return;
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
                    comboBoxChonHinhThuc.Items.AddRange(new string[] { "Xóa CSVC Của Phòng", "Xóa CSVC Của Phòng Trong Tầng", "Xóa CSVC Của Tất Cả Phòng" });
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
            try
            {
                if (comboBoxChonHinhThuc.SelectedItem == null)
                {
                    return;
                }
                ResetInputFields();
                LoadCSVC();
                string selectedHinhThuc = comboBoxChonHinhThuc.SelectedItem.ToString();
                //MessageBox.Show(selectedHinhThuc);
                if (selectedHinhThuc == "Thêm CSVC Vào Phòng")
                {
                    comboBoxLoaiPhong.Enabled = true;
                    comboBoxMaTang.Enabled = true;
                    comboBoxChonPhong.Enabled = true;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = false;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = false;
                    comboBoxChonTinhTrang.Enabled = true;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = false;

                    buttonThemCSVC.Enabled = true;
                    buttonSuaCSVC.Enabled = false;
                    buttonXoaCSVC.Enabled = false;
                }
                else if (selectedHinhThuc == "Thêm CSVC Vào Phòng Trong Tầng")
                {
                    comboBoxLoaiPhong.Enabled = true;
                    comboBoxMaTang.Enabled = true;
                    comboBoxChonPhong.Enabled = false;
                    comboBoxChonPhong.SelectedIndex = -1;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = false;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = false;
                    comboBoxChonTinhTrang.Enabled = true;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = false;

                    buttonThemCSVC.Enabled = true;
                    buttonSuaCSVC.Enabled = false;
                    buttonXoaCSVC.Enabled = false;
                }
                else if (selectedHinhThuc == "Thêm CSVC Vào Tất Cả Phòng")
                {
                    comboBoxLoaiPhong.Enabled = false;
                    comboBoxLoaiPhong.SelectedIndex = -1;
                    comboBoxMaTang.Enabled = false;
                    comboBoxMaTang.SelectedIndex = -1;
                    comboBoxChonPhong.Enabled = false;
                    comboBoxChonPhong.SelectedIndex = -1;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = false;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = false;
                    comboBoxChonTinhTrang.Enabled = true;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = false;

                    buttonThemCSVC.Enabled = true;
                    buttonSuaCSVC.Enabled = false;
                    buttonXoaCSVC.Enabled = false;
                }
                else if (selectedHinhThuc == "Sửa CSVC Của Phòng")
                {
                    comboBoxLoaiPhong.Enabled = true;
                    comboBoxMaTang.Enabled = true;
                    comboBoxChonPhong.Enabled = true;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = true;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = false;
                    comboBoxChonTinhTrang.Enabled = true;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = false;

                    buttonThemCSVC.Enabled = false;
                    buttonSuaCSVC.Enabled = true;
                    buttonXoaCSVC.Enabled = false;
                }
                else if (selectedHinhThuc == "Sửa CSVC Của Phòng Trong Tầng")
                {
                    comboBoxLoaiPhong.Enabled = true;
                    comboBoxMaTang.Enabled = true;
                    comboBoxChonPhong.Enabled = false;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = true;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = false;
                    comboBoxChonTinhTrang.Enabled = true;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = false;

                    buttonThemCSVC.Enabled = false;
                    buttonSuaCSVC.Enabled = true;
                    buttonXoaCSVC.Enabled = false;
                }
                else if (selectedHinhThuc == "Sửa CSVC Của Tất Cả Phòng")
                {
                    comboBoxLoaiPhong.Enabled = false;
                    comboBoxMaTang.Enabled = false;
                    comboBoxChonPhong.Enabled = false;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = false;
                    textBoxNhapSoLuong.Enabled = true;
                    comboBoxChonTinhTrang.Enabled = true;
                    textBoxGhiChu.Enabled = true;

                    buttonThemCSVC.Enabled = false;
                    buttonSuaCSVC.Enabled = true;
                    buttonXoaCSVC.Enabled = false;
                }
                else if (selectedHinhThuc == "Xóa CSVC Của Phòng")
                {
                    comboBoxLoaiPhong.Enabled = true;
                    comboBoxMaTang.Enabled = true;
                    comboBoxChonPhong.Enabled = true;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = true;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = true;
                    comboBoxChonTinhTrang.Enabled = false;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = true;

                    buttonThemCSVC.Enabled = false;
                    buttonSuaCSVC.Enabled = false;
                    buttonXoaCSVC.Enabled = true;
                }
                else if (selectedHinhThuc == "Xóa CSVC Của Phòng Trong Tầng")
                {
                    comboBoxLoaiPhong.Enabled = true;
                    comboBoxMaTang.Enabled = true;
                    comboBoxChonPhong.Enabled = false;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = true;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = true;
                    comboBoxChonTinhTrang.Enabled = false;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = true;

                    buttonThemCSVC.Enabled = false;
                    buttonSuaCSVC.Enabled = false;
                    buttonXoaCSVC.Enabled = true;
                }
                else if (selectedHinhThuc == "Xóa CSVC Của Tất Cả Phòng")
                {
                    comboBoxLoaiPhong.Enabled = false;
                    comboBoxMaTang.Enabled = false;
                    comboBoxChonPhong.Enabled = false;
                    textBoxTenCSVC.Enabled = true;
                    textBoxTenCSVC.ReadOnly = false;
                    textBoxNhapSoLuong.Enabled = true;
                    textBoxNhapSoLuong.ReadOnly = true;
                    comboBoxChonTinhTrang.Enabled = false;
                    textBoxGhiChu.Enabled = true;
                    textBoxGhiChu.ReadOnly = true;

                    buttonThemCSVC.Enabled = false;
                    buttonSuaCSVC.Enabled = false;
                    buttonXoaCSVC.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn hình thức: " + ex.Message);
            }
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

                        // Đặt lại giá trị rỗng cho các ô nhập liệu khi thay đổi tầng
                        textBoxTenCSVC.Text = "";
                        textBoxNhapSoLuong.Text = "";
                        comboBoxChonTinhTrang.SelectedIndex = -1; // Bỏ chọn giá trị
                        textBoxGhiChu.Text = "";
                    }
                }
            }
        }

        private void comboBoxChonPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đặt lại giá trị rỗng cho các ô nhập liệu khi thay đổi phòng
            textBoxTenCSVC.Text = "";
            textBoxNhapSoLuong.Text = "";
            comboBoxChonTinhTrang.SelectedIndex = -1; // Bỏ chọn giá trị
            textBoxGhiChu.Text = "";
        }

        private void comboBoxLocTang_Click(object sender, EventArgs e)
        {
            if(comboBoxLocTang.SelectedValue == null) 
            {
                comboBoxLocTang.DataSource = null;
                return; 
            }
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
                                PHONG.MA_PHONG,
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

                        //// Truyền tham số trạng thái
                        //if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                        //{
                        //    cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                        //}
                        // Truyền tham số trạng thái (đảm bảo kiểu dữ liệu là nvarchar)
                        if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                        {
                            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = selectedTrangThai;
                        }

                        using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable2 = new DataTable();
                            adapter2.Fill(dataTable2);

                            // Hiển thị dữ liệu lên DataGridView
                            dataGridViewCSVC.DataSource = dataTable2;

                            // Đổi tên cột hiển thị trên DataGridView
                            dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                            dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                            dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                            dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                            dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                            dataGridViewCSVC.Columns["MA_PHONG"].Visible = false;

                            // Cập nhật label chỉ số
                            UpdateLabelChiSo(dataTable2.Rows.Count);
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
                                PHONG.MA_PHONG,
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
                        //if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                        //{
                        //    cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                        //}
                        // Truyền tham số trạng thái (đảm bảo kiểu dữ liệu là nvarchar)
                        if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                        {
                            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = selectedTrangThai;
                        }
                        using (SqlDataAdapter adapter1 = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable1 = new DataTable();
                            adapter1.Fill(dataTable1);

                            // Hiển thị dữ liệu lên DataGridView
                            dataGridViewCSVC.DataSource = dataTable1;

                            // Đổi tên cột hiển thị trên DataGridView
                            dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                            dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                            dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                            dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                            dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                            dataGridViewCSVC.Columns["MA_PHONG"].Visible = false;

                            // Cập nhật label chỉ số
                            UpdateLabelChiSo(dataTable1.Rows.Count);
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
            if (comboBoxLocPhong.SelectedValue == null && comboBoxLocPhong.SelectedIndex!=-1) { return; }
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
                PHONG.MA_PHONG,
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

                    //// Thêm tham số Trạng thái
                    //if (string.IsNullOrEmpty(selectedTrangThai) || selectedTrangThai == "Tất Cả Trạng Thái")
                    //{
                    //    cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                    //}
                    // Truyền tham số trạng thái (đảm bảo kiểu dữ liệu là nvarchar)
                    if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                    {
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = selectedTrangThai;
                    }
                    // Đổ dữ liệu vào DataGridView
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridViewCSVC.DataSource = dataTable;

                        // Đổi tên cột hiển thị
                        dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                        dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                        dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                        dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                        dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                        dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                        dataGridViewCSVC.Columns["MA_PHONG"].Visible = false;

                        comboBoxLocTenCSVC.SelectedIndex = 0;

                        // Cập nhật label chỉ số
                        UpdateLabelChiSo(dataTable.Rows.Count);
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

                // Lấy giá trị từ comboBoxLocTrangThai
                string selectedTrangThai = comboBoxLocTrangThai?.SelectedItem?.ToString();

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
                                        AND (@TenCSVC IS NULL OR CO_SO_VAT_CHAT.TEN_CSVC = @TenCSVC)
                                        AND (@TrangThai IS NULL OR CO_SO_VAT_CHAT.TINH_TRANG = @TrangThai)";

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

                            //// Truyền tham số trạng thái
                            //if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                            //{
                            //    cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                            //}
                            // Truyền tham số trạng thái (đảm bảo kiểu dữ liệu là nvarchar)
                            if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                            {
                                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = selectedTrangThai;
                            }
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridViewCSVC.DataSource = dataTable;

                                // Đổi tên cột hiển thị
                                dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                                dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                                dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                                dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                                dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                                dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                                comboBoxLocPhong.SelectedIndex = -1;

                                // Cập nhật label chỉ số
                                UpdateLabelChiSo(dataTable.Rows.Count);
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
                                        (@TenCSVC IS NULL OR CO_SO_VAT_CHAT.TEN_CSVC = @TenCSVC)
                                        AND (@TrangThai IS NULL OR CO_SO_VAT_CHAT.TINH_TRANG = @TrangThai)";

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

                            //// Truyền tham số trạng thái
                            //if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                            //{
                            //    cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                            //}
                            // Truyền tham số trạng thái (đảm bảo kiểu dữ liệu là nvarchar)
                            if (selectedTrangThai == "Tất Cả Trạng Thái" || string.IsNullOrEmpty(selectedTrangThai))
                            {
                                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = selectedTrangThai;
                            }
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridViewCSVC.DataSource = dataTable;

                                // Đổi tên cột hiển thị
                                dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                                dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                                dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                                dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                                dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                                dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                                comboBoxLocPhong.SelectedIndex = -1;

                                // Cập nhật label chỉ số
                                UpdateLabelChiSo(dataTable.Rows.Count);
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

        private void comboBoxLocTrangThai_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    conn.Open();

                    // Lấy giá trị từ comboBoxLocTang
                    object tangValue = comboBoxLocTang.SelectedValue;
                    string selectedMaTang = (tangValue == null || tangValue == DBNull.Value) ? null : tangValue.ToString();

                    // Lấy giá trị từ comboBoxLocPhong
                    object phongValue = comboBoxLocPhong.SelectedValue;
                    string selectedMaPhong = (phongValue == null || phongValue == DBNull.Value) ? null : phongValue.ToString();

                    // Lấy giá trị từ comboBoxLocTenCSVC
                    string selectedTenCSVC = comboBoxLocTenCSVC.SelectedValue?.ToString();
                    if (string.IsNullOrEmpty(selectedTenCSVC) || selectedTenCSVC == "Tất Cả CSVC")
                    {
                        selectedTenCSVC = null;
                    }

                    // Lấy giá trị từ comboBoxLocTrangThai
                    string selectedTrangThai = comboBoxLocTrangThai.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(selectedTrangThai) || selectedTrangThai == "Tất Cả Trạng Thái")
                    {
                        selectedTrangThai = null;
                    }

                    // Xây dựng câu truy vấn động dựa trên các giá trị đã chọn
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
                    (@MaTang IS NULL OR PHONG.MA_TANG = @MaTang)
                    AND (@MaPhong IS NULL OR CO_SO_VAT_CHAT.MA_PHONG = @MaPhong)
                    AND (@TenCSVC IS NULL OR CO_SO_VAT_CHAT.TEN_CSVC = @TenCSVC)
                    AND (@TrangThai IS NULL OR CO_SO_VAT_CHAT.TINH_TRANG = @TrangThai)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Truyền tham số nếu có giá trị, nếu không thì truyền DBNull.Value
                        cmd.Parameters.AddWithValue("@MaTang", (object)selectedMaTang ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaPhong", (object)selectedMaPhong ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TenCSVC", (object)selectedTenCSVC ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", (object)selectedTrangThai ?? DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridViewCSVC.DataSource = dataTable;

                            // Đổi tên cột hiển thị
                            dataGridViewCSVC.Columns["MA_CSVC"].HeaderText = "Mã CSVC";
                            dataGridViewCSVC.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                            dataGridViewCSVC.Columns["TEN_CSVC"].HeaderText = "Tên CSVC";
                            dataGridViewCSVC.Columns["SO_LUONG"].HeaderText = "Số Lượng";
                            dataGridViewCSVC.Columns["TINH_TRANG"].HeaderText = "Tình Trạng";
                            dataGridViewCSVC.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                            // Cập nhật label chỉ số
                            UpdateLabelChiSo(dataTable.Rows.Count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu CSVC: " + ex.Message);
            }
        }
        private void UpdateLabelChiSo(int filteredCount)
        {
            using (SqlConnection conn = ketnoi.GetConnection())
            {
                try
                {
                    conn.Open();
                    // Đếm tổng số csvc
                    string queryTotal = @"SELECT COUNT(*) FROM CO_SO_VAT_CHAT";
                    SqlCommand command = new SqlCommand(queryTotal, conn);
                    int totalCount = (int)command.ExecuteScalar();

                    // Cập nhật vào labelChiSo theo định dạng "filteredCount/totalCount"
                    labelChiSo.Text = $"Số CSVC:{filteredCount}/{totalCount}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đếm csvc: " + ex.Message);
                }
            }
        }

        private void buttonThemCSVC_Click(object sender, EventArgs e)
        {
            if (comboBoxChonHinhThuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hình thức thêm CSVC.");
                return;
            }
            if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Thêm CSVC Vào Phòng")
            {
                if (comboBoxLoaiPhong.SelectedItem == null || comboBoxMaTang.SelectedItem == null || comboBoxChonPhong.SelectedItem == null || textBoxTenCSVC.Text == "" || textBoxNhapSoLuong.Text == "" || comboBoxChonTinhTrang.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm CSVC vào phòng này?", "Xác nhận thêm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = ketnoi.GetConnection())
                    {
                        conn.Open();

                        string tenCSVC = textBoxTenCSVC.Text;

                        // Kiểm tra xem CSVC đã tồn tại trong phòng chưa
                        string queryCheck = @"
                                SELECT COUNT(*) FROM CO_SO_VAT_CHAT 
                                WHERE MA_PHONG = @MaPhong AND TEN_CSVC = @TenCSVC";

                        using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                        {
                            if (int.TryParse(comboBoxChonPhong.SelectedValue?.ToString(), out int maPhong1))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong1);
                            }
                            else
                            {
                                MessageBox.Show("Mã phòng không hợp lệ.");
                                return;
                            };
                            cmdCheck.Parameters.AddWithValue("@TenCSVC", tenCSVC);

                            int count = (int)cmdCheck.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show($"Cơ sở vật chất '{tenCSVC}' đã tồn tại trong phòng này.");
                                return;
                            }
                        }

                        string query = @"
                        INSERT INTO CO_SO_VAT_CHAT(MA_PHONG, TEN_CSVC, SO_LUONG, TINH_TRANG, GHI_CHU)
                        VALUES(@MaPhong, @TenCSVC, @SoLuong, @TinhTrang, @GhiChu)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            if (int.TryParse(comboBoxChonPhong.SelectedValue?.ToString(), out int maPhong))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                            }
                            else
                            {
                                MessageBox.Show("Mã phòng không hợp lệ.");
                                return;
                            }

                            cmd.Parameters.AddWithValue("@TenCSVC", textBoxTenCSVC.Text);
                            cmd.Parameters.AddWithValue("@SoLuong", textBoxNhapSoLuong.Text);
                            cmd.Parameters.AddWithValue("@TinhTrang", comboBoxChonTinhTrang.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@GhiChu", textBoxGhiChu.Text);
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Thêm CSVC vào phòng thành công.");
                                ResetInputFields();
                                LoadCSVC();
                            }
                            else
                            {
                                MessageBox.Show("Thêm CSVC vào phòng thất bại.");
                            }
                        }
                    }
                }
            }
            else if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Thêm CSVC Vào Phòng Trong Tầng")
            {
                if (comboBoxMaTang.SelectedItem == null || textBoxTenCSVC.Text == "" || textBoxNhapSoLuong.Text == "" || comboBoxChonTinhTrang.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm CSVC vào tất cả các phòng trong tầng này?", "Xác nhận thêm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = ketnoi.GetConnection())
                    {
                        conn.Open();

                        // Lấy MaTang từ comboBox
                        if (!int.TryParse(comboBoxMaTang.SelectedValue?.ToString(), out int maTang))
                        {
                            MessageBox.Show("Mã tầng không hợp lệ.");
                            return;
                        }

                        string tenCSVC = textBoxTenCSVC.Text;

                        // Lấy danh sách tất cả phòng thuộc tầng đã chọn
                        string queryLayPhong = "SELECT MA_PHONG FROM PHONG WHERE MA_TANG = @MaTang";
                        List<int> danhSachMaPhong = new List<int>();

                        using (SqlCommand cmdLayPhong = new SqlCommand(queryLayPhong, conn))
                        {
                            cmdLayPhong.Parameters.AddWithValue("@MaTang", maTang);
                            using (SqlDataReader reader = cmdLayPhong.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    danhSachMaPhong.Add(reader.GetInt32(0)); // Lấy MA_PHONG từ kết quả truy vấn
                                }
                            }
                        }

                        if (danhSachMaPhong.Count == 0)
                        {
                            MessageBox.Show("Không có phòng nào trong tầng này.");
                            return;
                        }

                        // Kiểm tra phòng nào đã có CSVC
                        List<int> phongDaCoCSVC = new List<int>();

                        string queryCheckCSVC = @"
        SELECT MA_PHONG FROM CO_SO_VAT_CHAT 
        WHERE MA_PHONG IN (" + string.Join(",", danhSachMaPhong) + @") AND TEN_CSVC = @TenCSVC";

                        using (SqlCommand cmdCheckCSVC = new SqlCommand(queryCheckCSVC, conn))
                        {
                            cmdCheckCSVC.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                            using (SqlDataReader reader = cmdCheckCSVC.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    phongDaCoCSVC.Add(reader.GetInt32(0));
                                }
                            }
                        }

                        // Loại bỏ các phòng đã có CSVC khỏi danh sách thêm mới
                        danhSachMaPhong.RemoveAll(p => phongDaCoCSVC.Contains(p));

                        if (danhSachMaPhong.Count == 0)
                        {
                            MessageBox.Show($"Tất cả các phòng trong tầng đã có '{tenCSVC}'. Không có phòng nào được thêm mới.");
                            return;
                        }

                        // Thêm CSVC vào từng phòng chưa có
                        string queryThemCSVC = @"
                            INSERT INTO CO_SO_VAT_CHAT(MA_PHONG, TEN_CSVC, SO_LUONG, TINH_TRANG, GHI_CHU)
                            VALUES(@MaPhong, @TenCSVC, @SoLuong, @TinhTrang, @GhiChu)";

                        using (SqlCommand cmdThemCSVC = new SqlCommand(queryThemCSVC, conn))
                        {
                            cmdThemCSVC.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                            cmdThemCSVC.Parameters.AddWithValue("@SoLuong", textBoxNhapSoLuong.Text);
                            cmdThemCSVC.Parameters.AddWithValue("@TinhTrang", comboBoxChonTinhTrang.SelectedItem.ToString());
                            cmdThemCSVC.Parameters.AddWithValue("@GhiChu", textBoxGhiChu.Text);

                            int soLuongThem = 0;
                            foreach (int maPhong in danhSachMaPhong)
                            {
                                cmdThemCSVC.Parameters.AddWithValue("@MaPhong", maPhong);
                                soLuongThem += cmdThemCSVC.ExecuteNonQuery();
                                cmdThemCSVC.Parameters.RemoveAt("@MaPhong"); // Xóa tham số để dùng lại
                            }

                            MessageBox.Show($"Thêm CSVC vào {soLuongThem} phòng thành công.");
                            ResetInputFields();
                            LoadCSVC();
                        }

                        //if (phongDaCoCSVC.Count > 0)
                        //{
                        //    MessageBox.Show($"Các phòng sau đã có '{tenCSVC}' và không được thêm: {string.Join(", ", phongDaCoCSVC)}.");
                        //}
                        if (phongDaCoCSVC.Count > 0)
                        {
                            List<string> danhSachTenPhong = new List<string>();

                            string queryLayTenPhong = @"
                            SELECT TEN_PHONG FROM PHONG 
                            WHERE MA_PHONG IN (" + string.Join(",", phongDaCoCSVC) + ")";

                            using (SqlCommand cmdLayTenPhong = new SqlCommand(queryLayTenPhong, conn))
                            {
                                using (SqlDataReader reader = cmdLayTenPhong.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        danhSachTenPhong.Add(reader.GetString(0)); // Lấy TEN_PHONG
                                    }
                                }
                            }

                            MessageBox.Show($"Các phòng sau đã có '{tenCSVC}' và không được thêm: {string.Join(", ", danhSachTenPhong)}.");
                        }

                    }
                }

            }
            else if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Thêm CSVC Vào Tất Cả Phòng")
            {
                if (textBoxTenCSVC.Text == "" || textBoxNhapSoLuong.Text == "" || comboBoxChonTinhTrang.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm CSVC vào tất cả các phòng?", "Xác nhận thêm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = ketnoi.GetConnection())
                    {
                        conn.Open();

                        // Lấy danh sách tất cả phòng trong ký túc xá
                        string queryLayTatCaPhong = "SELECT MA_PHONG FROM PHONG";
                        List<int> danhSachMaPhong = new List<int>();

                        using (SqlCommand cmdLayTatCaPhong = new SqlCommand(queryLayTatCaPhong, conn))
                        {
                            using (SqlDataReader reader = cmdLayTatCaPhong.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    danhSachMaPhong.Add(reader.GetInt32(0)); // Lấy MA_PHONG từ kết quả truy vấn
                                }
                            }
                        }

                        if (danhSachMaPhong.Count == 0)
                        {
                            MessageBox.Show("Không có phòng nào trong ký túc xá.");
                            return;
                        }

                        // Thêm CSVC vào từng phòng
                        string queryThemCSVC = @"
                                        INSERT INTO CO_SO_VAT_CHAT(MA_PHONG, TEN_CSVC, SO_LUONG, TINH_TRANG, GHI_CHU)
                                        VALUES(@MaPhong, @TenCSVC, @SoLuong, @TinhTrang, @GhiChu)";

                        using (SqlCommand cmdThemCSVC = new SqlCommand(queryThemCSVC, conn))
                        {
                            cmdThemCSVC.Parameters.AddWithValue("@TenCSVC", textBoxTenCSVC.Text);
                            cmdThemCSVC.Parameters.AddWithValue("@SoLuong", textBoxNhapSoLuong.Text);
                            cmdThemCSVC.Parameters.AddWithValue("@TinhTrang", comboBoxChonTinhTrang.SelectedItem.ToString());
                            cmdThemCSVC.Parameters.AddWithValue("@GhiChu", textBoxGhiChu.Text);

                            int soLuongThem = 0;
                            foreach (int maPhong in danhSachMaPhong)
                            {
                                cmdThemCSVC.Parameters.AddWithValue("@MaPhong", maPhong);
                                soLuongThem += cmdThemCSVC.ExecuteNonQuery();
                                cmdThemCSVC.Parameters.RemoveAt("@MaPhong"); // Xóa tham số để dùng lại
                            }

                            MessageBox.Show($"Thêm CSVC vào {soLuongThem} phòng thành công.");
                            ResetInputFields();
                            LoadCSVC();
                        }
                    }
                }
            }

        }

        private void buttonXoaCSVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxChonHinhThuc.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn hình thức xóa CSVC.");
                    return;
                }
                if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Xóa CSVC Của Phòng")
                {
                    if (comboBoxLoaiPhong.SelectedItem == null || comboBoxMaTang.SelectedItem == null || comboBoxChonPhong.SelectedItem == null || textBoxTenCSVC.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                        return;
                    }
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa CSVC trong phòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            string tenCSVC = textBoxTenCSVC.Text;
                            // Kiểm tra xem CSVC đã tồn tại trong phòng chưa
                            string queryCheck = @"
                                SELECT COUNT(*) FROM CO_SO_VAT_CHAT 
                                WHERE MA_PHONG = @MaPhong AND TEN_CSVC = @TenCSVC";
                            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                            {
                                if (int.TryParse(comboBoxChonPhong.SelectedValue?.ToString(), out int maPhong1))
                                {
                                    cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong1);
                                }
                                else
                                {
                                    MessageBox.Show("Mã phòng không hợp lệ.");
                                    return;
                                };
                                cmdCheck.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                int count = (int)cmdCheck.ExecuteScalar();
                                if (count == 0)
                                {
                                    MessageBox.Show($"Cơ sở vật chất '{tenCSVC}' không tồn tại trong phòng này.");
                                    return;
                                }
                            }
                            string query = @"
                        DELETE FROM CO_SO_VAT_CHAT
                        WHERE MA_PHONG = @MaPhong AND TEN_CSVC = @TenCSVC";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                if (int.TryParse(comboBoxChonPhong.SelectedValue?.ToString(), out int maPhong))
                                {
                                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                }
                                else
                                {
                                    MessageBox.Show("Mã phòng không hợp lệ.");
                                    return;
                                }
                                cmd.Parameters.AddWithValue("@TenCSVC", textBoxTenCSVC.Text);
                                int result = cmd.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Xóa CSVC trong phòng thành công.");
                                    ResetInputFields();
                                    LoadCSVC();
                                }
                                else
                                {
                                    MessageBox.Show("Xóa CSVC trong phòng thất bại.");
                                }
                            }
                        }
                    }
                }
                else if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Xóa CSVC Của Phòng Trong Tầng")
                {
                    if (comboBoxMaTang.SelectedItem == null || textBoxTenCSVC.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa CSVC trong tất cả các phòng của tầng này?",
                                                                "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            string tenCSVC = textBoxTenCSVC.Text;

                            // Lấy MaTang từ comboBox
                            if (!int.TryParse(comboBoxMaTang.SelectedValue?.ToString(), out int maTang))
                            {
                                MessageBox.Show("Mã tầng không hợp lệ.");
                                return;
                            }

                            // Lấy danh sách các phòng có CSVC cần xóa trong tầng
                            string queryLayPhong = @"
                SELECT MA_PHONG FROM CO_SO_VAT_CHAT 
                WHERE TEN_CSVC = @TenCSVC AND MA_PHONG IN (SELECT MA_PHONG FROM PHONG WHERE MA_TANG = @MaTang)";

                            List<int> danhSachMaPhong = new List<int>();
                            using (SqlCommand cmdLayPhong = new SqlCommand(queryLayPhong, conn))
                            {
                                cmdLayPhong.Parameters.AddWithValue("@MaTang", maTang);
                                cmdLayPhong.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                using (SqlDataReader reader = cmdLayPhong.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        danhSachMaPhong.Add(reader.GetInt32(0)); // Lấy MA_PHONG
                                    }
                                }
                            }

                            if (danhSachMaPhong.Count == 0)
                            {
                                MessageBox.Show($"Không có CSVC '{tenCSVC}' nào trong tầng này.");
                                return;
                            }

                            // Xóa CSVC trong các phòng thuộc tầng
                            string queryXoaCSVC = @"
                DELETE FROM CO_SO_VAT_CHAT 
                WHERE TEN_CSVC = @TenCSVC AND MA_PHONG IN (SELECT MA_PHONG FROM PHONG WHERE MA_TANG = @MaTang)";

                            using (SqlCommand cmdXoaCSVC = new SqlCommand(queryXoaCSVC, conn))
                            {
                                cmdXoaCSVC.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                cmdXoaCSVC.Parameters.AddWithValue("@MaTang", maTang);

                                int soLuongXoa = cmdXoaCSVC.ExecuteNonQuery();

                                MessageBox.Show($"Đã xóa '{tenCSVC}' trong {soLuongXoa} phòng thuộc tầng {maTang}.");
                                ResetInputFields();
                                LoadCSVC();
                            }
                        }
                    }
                }
                else if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Xóa CSVC Của Tất Cả Phòng")
                {
                    if (textBoxTenCSVC.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập tên CSVC cần xóa.");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa CSVC này trong tất cả các phòng?",
                                                                "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            string tenCSVC = textBoxTenCSVC.Text;

                            // Kiểm tra xem CSVC có tồn tại trong bất kỳ phòng nào không
                            string queryCheck = "SELECT COUNT(*) FROM CO_SO_VAT_CHAT WHERE TEN_CSVC = @TenCSVC";
                            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                            {
                                cmdCheck.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                int count = (int)cmdCheck.ExecuteScalar();
                                if (count == 0)
                                {
                                    MessageBox.Show($"Không có CSVC '{tenCSVC}' nào trong hệ thống.");
                                    return;
                                }
                            }

                            // Xóa CSVC trong tất cả các phòng
                            string queryXoaCSVC = "DELETE FROM CO_SO_VAT_CHAT WHERE TEN_CSVC = @TenCSVC";
                            using (SqlCommand cmdXoaCSVC = new SqlCommand(queryXoaCSVC, conn))
                            {
                                cmdXoaCSVC.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                int soLuongXoa = cmdXoaCSVC.ExecuteNonQuery();

                                MessageBox.Show($"Đã xóa '{tenCSVC}' trong {soLuongXoa} phòng.");
                                ResetInputFields();
                                LoadCSVC();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa CSVC: " + ex.Message);
            }
        }

        private void buttonSuaCSVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxChonHinhThuc.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn hình thức sửa CSVC.");
                    return;
                }
                if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Sửa CSVC Của Phòng")
                {
                    if (comboBoxChonPhong.SelectedItem == null || textBoxTenCSVC.Text == "")
                    {
                        MessageBox.Show("Vui lòng chọn phòng và nhập tên CSVC cần chỉnh sửa.");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin CSVC trong phòng này?", "Xác nhận sửa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            string tenCSVC = textBoxTenCSVC.Text;

                            // Lấy mã phòng từ comboBox
                            if (!int.TryParse(comboBoxChonPhong.SelectedValue?.ToString(), out int maPhong))
                            {
                                MessageBox.Show("Mã phòng không hợp lệ.");
                                return;
                            }

                            // Kiểm tra xem CSVC có tồn tại trong phòng không
                            string queryCheck = @"
            SELECT COUNT(*) FROM CO_SO_VAT_CHAT 
            WHERE MA_PHONG = @MaPhong AND TEN_CSVC = @TenCSVC";
                            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmdCheck.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                int count = (int)cmdCheck.ExecuteScalar();
                                if (count == 0)
                                {
                                    MessageBox.Show($"Cơ sở vật chất '{tenCSVC}' không tồn tại trong phòng {maPhong}.");
                                    return;
                                }
                            }

                            // Lấy thông tin mới từ các ô nhập liệu
                            string newTenCSVC = textBoxTenCSVC.Text;
                            int newSoLuong;
                            string newTinhTrang = comboBoxChonTinhTrang.SelectedItem?.ToString() ?? "";
                            string newGhiChu = textBoxGhiChu.Text;

                            // Kiểm tra số lượng nhập vào có hợp lệ không
                            if (!int.TryParse(textBoxNhapSoLuong.Text, out newSoLuong) || newSoLuong < 0)
                            {
                                MessageBox.Show("Số lượng không hợp lệ.");
                                return;
                            }

                            // Cập nhật thông tin CSVC
                            string queryUpdate = @"
            UPDATE CO_SO_VAT_CHAT 
            SET TEN_CSVC = @NewTenCSVC, SO_LUONG = @NewSoLuong, 
                TINH_TRANG = @NewTinhTrang, GHI_CHU = @NewGhiChu
            WHERE MA_PHONG = @MaPhong AND TEN_CSVC = @TenCSVC";
                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@NewTenCSVC", newTenCSVC);
                                cmdUpdate.Parameters.AddWithValue("@NewSoLuong", newSoLuong);
                                cmdUpdate.Parameters.AddWithValue("@NewTinhTrang", newTinhTrang);
                                cmdUpdate.Parameters.AddWithValue("@NewGhiChu", newGhiChu);
                                cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmdUpdate.Parameters.AddWithValue("@TenCSVC", tenCSVC);

                                int result = cmdUpdate.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa CSVC thành công.");
                                    ResetInputFields();
                                    LoadCSVC();
                                }
                                else
                                {
                                    MessageBox.Show("Chỉnh sửa CSVC thất bại.");
                                }
                            }
                        }
                    }
                }
                else if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Sửa CSVC Của Phòng Trong Tầng")
                {
                    if (comboBoxMaTang.SelectedItem == null || textBoxTenCSVC.Text == "")
                    {
                        MessageBox.Show("Vui lòng chọn tầng và nhập tên CSVC cần chỉnh sửa.");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin CSVC trong tất cả các phòng thuộc tầng này?", "Xác nhận sửa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            string tenCSVC = textBoxTenCSVC.Text;

                            // Lấy mã tầng từ comboBox
                            if (!int.TryParse(comboBoxMaTang.SelectedValue?.ToString(), out int maTang))
                            {
                                MessageBox.Show("Mã tầng không hợp lệ.");
                                return;
                            }

                            // Kiểm tra xem CSVC có tồn tại trong bất kỳ phòng nào của tầng không
                            string queryCheck = @"
SELECT COUNT(*) FROM CO_SO_VAT_CHAT csvc
JOIN PHONG p ON csvc.MA_PHONG = p.MA_PHONG
WHERE p.MA_TANG = @MaTang AND csvc.TEN_CSVC = @TenCSVC";

                            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaTang", maTang);
                                cmdCheck.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                int count = (int)cmdCheck.ExecuteScalar();
                                if (count == 0)
                                {
                                    MessageBox.Show($"Cơ sở vật chất '{tenCSVC}' không tồn tại trong bất kỳ phòng nào của tầng {maTang}.");
                                    return;
                                }
                            }

                            // Lấy thông tin mới từ các ô nhập liệu
                            string newTenCSVC = textBoxTenCSVC.Text;
                            int newSoLuong;
                            string newTinhTrang = comboBoxChonTinhTrang.SelectedItem?.ToString() ?? "";
                            string newGhiChu = textBoxGhiChu.Text;

                            // Kiểm tra số lượng nhập vào có hợp lệ không
                            if (!int.TryParse(textBoxNhapSoLuong.Text, out newSoLuong) || newSoLuong < 0)
                            {
                                MessageBox.Show("Số lượng không hợp lệ.");
                                return;
                            }

                            // Cập nhật thông tin CSVC trong tất cả các phòng thuộc tầng
                            string queryUpdate = @"
UPDATE CO_SO_VAT_CHAT 
SET TEN_CSVC = @NewTenCSVC, SO_LUONG = @NewSoLuong, 
    TINH_TRANG = @NewTinhTrang, GHI_CHU = @NewGhiChu
WHERE MA_PHONG IN (SELECT MA_PHONG FROM PHONG WHERE MA_TANG = @MaTang)
AND TEN_CSVC = @TenCSVC";

                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@NewTenCSVC", newTenCSVC);
                                cmdUpdate.Parameters.AddWithValue("@NewSoLuong", newSoLuong);
                                cmdUpdate.Parameters.AddWithValue("@NewTinhTrang", newTinhTrang);
                                cmdUpdate.Parameters.AddWithValue("@NewGhiChu", newGhiChu);
                                cmdUpdate.Parameters.AddWithValue("@MaTang", maTang);
                                cmdUpdate.Parameters.AddWithValue("@TenCSVC", tenCSVC);

                                int result = cmdUpdate.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa CSVC trong tầng thành công.");
                                    ResetInputFields();
                                    LoadCSVC();
                                }
                                else
                                {
                                    MessageBox.Show("Chỉnh sửa CSVC trong tầng thất bại.");
                                }
                            }
                        }
                    }
                }
                else if (comboBoxChonHinhThuc.SelectedItem.ToString() == "Sửa CSVC Của Tất Cả Phòng")
                {
                    if (textBoxTenCSVC.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập tên CSVC cần chỉnh sửa.");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin CSVC trong tất cả các phòng của KTX?", "Xác nhận sửa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = ketnoi.GetConnection())
                        {
                            conn.Open();
                            string tenCSVC = textBoxTenCSVC.Text;

                            // Kiểm tra xem CSVC có tồn tại trong bất kỳ phòng nào không
                            string queryCheck = @"
SELECT COUNT(*) FROM CO_SO_VAT_CHAT
WHERE TEN_CSVC = @TenCSVC";

                            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                            {
                                cmdCheck.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                                int count = (int)cmdCheck.ExecuteScalar();
                                if (count == 0)
                                {
                                    MessageBox.Show($"Cơ sở vật chất '{tenCSVC}' không tồn tại trong bất kỳ phòng nào.");
                                    return;
                                }
                            }

                            // Lấy thông tin mới từ các ô nhập liệu
                            string newTenCSVC = textBoxTenCSVC.Text;
                            int newSoLuong;
                            string newTinhTrang = comboBoxChonTinhTrang.SelectedItem?.ToString() ?? "";
                            string newGhiChu = textBoxGhiChu.Text;

                            // Kiểm tra số lượng nhập vào có hợp lệ không
                            if (!int.TryParse(textBoxNhapSoLuong.Text, out newSoLuong) || newSoLuong < 0)
                            {
                                MessageBox.Show("Số lượng không hợp lệ.");
                                return;
                            }

                            // Cập nhật thông tin CSVC trong tất cả các phòng có tên CSVC đó
                            string queryUpdate = @"
UPDATE CO_SO_VAT_CHAT 
SET TEN_CSVC = @NewTenCSVC, SO_LUONG = @NewSoLuong, 
    TINH_TRANG = @NewTinhTrang, GHI_CHU = @NewGhiChu
WHERE TEN_CSVC = @TenCSVC";

                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@NewTenCSVC", newTenCSVC);
                                cmdUpdate.Parameters.AddWithValue("@NewSoLuong", newSoLuong);
                                cmdUpdate.Parameters.AddWithValue("@NewTinhTrang", newTinhTrang);
                                cmdUpdate.Parameters.AddWithValue("@NewGhiChu", newGhiChu);
                                cmdUpdate.Parameters.AddWithValue("@TenCSVC", tenCSVC);

                                int result = cmdUpdate.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa CSVC trong toàn bộ KTX thành công.");
                                    ResetInputFields();
                                    LoadCSVC();
                                }
                                else
                                {
                                    MessageBox.Show("Chỉnh sửa CSVC trong toàn bộ KTX thất bại.");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa CSVC: " + ex.Message);
            }
        }

        private void buttonXoaAllCSVCHuHong_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả CSVC có tình trạng 'Hư Hỏng'?",
                                                        "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra xem có CSVC nào bị "Hư Hỏng" không
                    string queryCheck = "SELECT COUNT(*) FROM CO_SO_VAT_CHAT WHERE TINH_TRANG = N'Hư Hỏng'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                    {
                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Không có CSVC nào trong hệ thống có tình trạng 'Hư Hỏng'.");
                            return;
                        }
                    }

                    // Xóa tất cả CSVC có tình trạng "Hư Hỏng"
                    string queryXoaCSVC = "DELETE FROM CO_SO_VAT_CHAT WHERE TINH_TRANG = N'Hư Hỏng'";
                    using (SqlCommand cmdXoaCSVC = new SqlCommand(queryXoaCSVC, conn))
                    {
                        int soLuongXoa = cmdXoaCSVC.ExecuteNonQuery();

                        MessageBox.Show($"Đã xóa {soLuongXoa} CSVC có tình trạng 'Hư Hỏng'.");
                        ResetInputFields();
                        LoadCSVC(); // Load lại danh sách CSVC sau khi xóa
                    }
                }
            }
        }


        private void buttonXoaAllCSVC_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa toàn bộ cơ sở vật chất trong KTX? Hành động này không thể hoàn tác!",
                "Xác nhận xóa tất cả CSVC", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                dialogResult = MessageBox.Show(
                    "Bạn có thực sự chắc chắn muốn xóa toàn bộ cơ sở vật chất trong KTX? Đây là lần xác nhận cuối cùng!",
                    "Xác nhận lần cuối", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = ketnoi.GetConnection())
                    {
                        conn.Open();

                        // Kiểm tra xem có dữ liệu CSVC không
                        string queryCheck = "SELECT COUNT(*) FROM CO_SO_VAT_CHAT";
                        using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                        {
                            int count = (int)cmdCheck.ExecuteScalar();
                            if (count == 0)
                            {
                                MessageBox.Show("Không có CSVC nào trong hệ thống để xóa.");
                                return;
                            }
                        }

                        // Xóa tất cả CSVC
                        string queryXoaTatCaCSVC = "DELETE FROM CO_SO_VAT_CHAT";
                        using (SqlCommand cmdXoaTatCa = new SqlCommand(queryXoaTatCaCSVC, conn))
                        {
                            int soLuongXoa = cmdXoaTatCa.ExecuteNonQuery();
                            MessageBox.Show($"Đã xóa toàn bộ {soLuongXoa} CSVC trong KTX.");
                            LoadCSVC(); // Cập nhật lại danh sách CSVC
                        }
                    }
                }
            }
        }

    }
}
