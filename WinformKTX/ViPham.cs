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

namespace abc.HoanThanh.ThongKeViPham
{
    public partial class ViPham : Form
    {
        public ViPham()
        {
            InitializeComponent();
            LoadViPhamData();
            LoadViewForm();
        }
        private SqlConnection conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
        //giao dien ban dau
        private void LoadViewForm()
        {
            comboBoxSinhVien.Enabled = false;
            comboBoxMucViPham.Enabled = false;
            dateTimePickerNgayViPham.Enabled = false;
            textBoxGhiChu.Enabled = false;
            textBoxNoiDung.Enabled = false;
            radioButtonChuaxuly.Enabled = false;
            radioButtonDaxuly.Enabled = false;
            buttonHuy.Enabled = false;
            buttonHuy.BackColor = Color.Gray;
                        buttonTroLai.Enabled = false;
            buttonTroLai.BackColor = Color.Gray;
            buttonLuu.Enabled = false;
            buttonLuu.BackColor = Color.Gray;
            buttonSua.Enabled = false;
            buttonXoa.Enabled = false;
            buttonThem.Enabled = false;
        }
        //giao dien Them
        private void GiaodienThem()
        {
            groupBoxTrangThai.Enabled = true;
            groupBoxThongTin.Enabled = true;
            groupBoxSuaXoa.Enabled = true;
            comboBoxSinhVien.Enabled = true;
            comboBoxMucViPham.Enabled = true;
            dateTimePickerNgayViPham.Enabled = true;
            textBoxGhiChu.Enabled = true;
            textBoxNoiDung.Enabled = true;
            radioButtonChuaxuly.Enabled = true;
            radioButtonDaxuly.Enabled = true;
            buttonHuy.Enabled = true;
            buttonHuy.BackColor = Color.Red;
            buttonTroLai.Enabled = false;
            buttonTroLai.BackColor = Color.Gray;
            buttonLuu.Enabled = false;
            buttonLuu.BackColor = Color.Gray;
            buttonSua.Enabled = false;
            buttonXoa.Enabled = false;
            buttonThem.Enabled = true;
        }


        //ham du lieu vao data
        private void LoadViPhamData()
        {
            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MA_VI_PHAM, MSSV, MUC_VI_PHAM.TEN_MUC_VI_PHAM, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, TRANG_THAI_XU_LY, GHI_CHU_VP " +
                                   "FROM VI_PHAM JOIN MUC_VI_PHAM ON VI_PHAM.MA_MUC_VP = MUC_VI_PHAM.MA_MUC_VP";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGridView
                    dataGridViewThongTin.DataSource = dataTable;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
        private void LoaddataMssv()
        {
            try
            {
                string query = "SELECT MSSV FROM SINH_VIEN";
                using (var cmd = new SqlCommand(query, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxSinhVien.DataSource = table;
                    comboBoxSinhVien.DisplayMember = "MSSV";
                    comboBoxSinhVien.ValueMember = "MSSV";

                    comboBoxSinhVien.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //load loai vi pham
        private void LoadMucViPham()
        {
            try
            {
                string query = "SELECT MUC_VI_PHAM.MA_MUC_VP,MUC_VI_PHAM.TEN_MUC_VI_PHAM from MUC_VI_PHAM";
                using (var cmd = new SqlCommand(query, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxMucViPham.DataSource = table;
                    comboBoxMucViPham.DisplayMember = "TEN_MUC_VI_PHAM";
                    comboBoxMucViPham.ValueMember = "MA_MUC_VP";

                    comboBoxMucViPham.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioThem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonThem.Checked)
            {
                GroupBoxChucNang.Enabled = false;
                LoadViPhamData();
                GiaodienThem();
                //load datavao combobox
                LoaddataMssv();
                LoadMucViPham();

            }
        }
        //ham an nut them thi them du lieu vao 
        private void AddViPham()
        {
            // Kiểm tra các trường đầu vào có đầy đủ dữ liệu hay không
            if (string.IsNullOrEmpty(comboBoxSinhVien.SelectedValue?.ToString()) || string.IsNullOrEmpty(comboBoxMucViPham.SelectedValue?.ToString()) || !radioButtonDaxuly.Checked && !radioButtonChuaxuly.Checked)
            {
                MessageBox.Show("Vui lòng Nhap Du thong tin");
                return;
            }

            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO VI_PHAM (MSSV, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, MA_MUC_VP, TRANG_THAI_XU_LY, GHI_CHU_VP) " +
                                   "VALUES (@MSSV, @NgayViPham, @NoiDungViPham, @MaMucVp, @TrangThaiXuLy, @GhiChu)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        string mssv = comboBoxSinhVien.SelectedValue.ToString();
                        cmd.Parameters.AddWithValue("@MSSV", mssv);
                        DateTime ngayViPham = dateTimePickerNgayViPham.Value;
                        cmd.Parameters.AddWithValue("@NgayViPham", ngayViPham);
                        string noiDungViPham = textBoxNoiDung.Text;
                        cmd.Parameters.AddWithValue("@NoiDungViPham", noiDungViPham);
                        string maMucVp = comboBoxMucViPham.SelectedValue.ToString();
                        cmd.Parameters.AddWithValue("@MaMucVp", maMucVp);

                        // Kiểm tra trạng thái xử lý và lưu vào cơ sở dữ liệu
                        string trangThaiXuLy = radioButtonDaxuly.Checked ? "true" : "false";
                        cmd.Parameters.AddWithValue("@TrangThaiXuLy", trangThaiXuLy);

                        string ghiChu = textBoxGhiChu.Text;
                        cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                        // Thực thi câu lệnh SQL để thêm dữ liệu
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm vi phạm thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm vi phạm!");
                        }
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        private void buttonThem_Click(object sender, EventArgs e)
        {
            AddViPham();
            ClearAllValues();
            LoadViPhamData();
        }


        private void ClearAllValues()
        {
                textBoxNoiDung.Clear();
                textBoxGhiChu.Clear();
                // Clear ComboBox
                comboBoxSinhVien.SelectedIndex = -1;  // Hủy chọn sinh viên
                comboBoxMucViPham.SelectedIndex = -1; // Hủy chọn mức vi phạm

                // Reset DateTimePicker về ngày hiện tại (hoặc bạn có thể đặt ngày mặc định khác)
                dateTimePickerNgayViPham.Value = DateTime.Now;

                // Clear các RadioButton (bỏ chọn tất cả)
                radioButtonDaxuly.Checked = false;
                radioButtonChuaxuly.Checked = false;

                // Nếu bạn có thêm các control khác, xử lý chúng tương tự.

            // Clear các TextBox
        }

        private void LoadGiaTriCu()
        {
            textBoxNoiDung.Text = initialNoiDung;
            textBoxGhiChu.Text = initialGhiChu;
            comboBoxMucViPham.SelectedValue = initialMaMucViPham;
            comboBoxSinhVien.SelectedValue = initialMSSV;

            dateTimePickerNgayViPham.Value = initialNgayViPham;

            if (initialTrangThaiXuLy == true)
            {
                radioButtonDaxuly.Checked = true;
            }
            else
            {
                radioButtonChuaxuly.Checked = true;
            }
        }

        //xoa


        //giao dien xoa
        private void GiaodienXoa()
        {
            groupBoxTrangThai.Enabled = true;
            groupBoxThongTin.Enabled = true;
            groupBoxSuaXoa.Enabled = true;
            comboBoxSinhVien.Enabled = true;
            comboBoxMucViPham.Enabled = false;
            dateTimePickerNgayViPham.Enabled = false;
            textBoxGhiChu.Enabled = false;
            textBoxNoiDung.Enabled = false;
            radioButtonChuaxuly.Enabled = false;
            radioButtonDaxuly.Enabled = false;
            buttonHuy.Enabled = true;
            buttonHuy.BackColor = Color.Red;
            buttonTroLai.Enabled = false;
            buttonTroLai.BackColor = Color.Gray;
            buttonLuu.Enabled = false;
            buttonLuu.BackColor = Color.Gray;
            buttonSua.Enabled = false;
            buttonXoa.Enabled = true;
            buttonThem.Enabled = false;
        }


        //lay MSSV tu comboboxSinhVien

        private void comboBoxSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSinhVien.SelectedIndex != -1) // Kiểm tra có chọn MSSV không
            {
                if (radioButtonSua.Checked)
                {
                    // Lấy MSSV từ ComboBox
                    string selectedMSSV = comboBoxSinhVien.SelectedValue.ToString();

                    // Hiển thị thông tin vi phạm của sinh viên này trong DataGridView
                    LoadViPhamByMSSV(selectedMSSV);
                }
                if (radioButtonXoa.Checked)
                {
                    // Lấy MSSV từ ComboBox
                    string selectedMSSV = comboBoxSinhVien.SelectedValue.ToString();

                    // Hiển thị thông tin vi phạm của sinh viên này trong DataGridView
                    LoadViPhamByMSSV(selectedMSSV);
                }
            }
        }
        //dua thong tin vi pham can xoa
        private void LoadViPhamByMSSV(string mssv)
        {
            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";

            // Truy vấn dữ liệu vi phạm theo MSSV
            string query = "SELECT MA_VI_PHAM, MSSV, MUC_VI_PHAM.TEN_MUC_VI_PHAM, NGAY_VI_PHAM, NOI_DUNG_VI_PHAM, TRANG_THAI_XU_LY, GHI_CHU_VP " +
                                   "FROM VI_PHAM JOIN MUC_VI_PHAM ON VI_PHAM.MA_MUC_VP = MUC_VI_PHAM.MA_MUC_VP" +
                                   " WHERE MSSV = @MSSV";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số MSSV
                        cmd.Parameters.AddWithValue("@MSSV", mssv);

                        // Sử dụng DataReader để lấy dữ liệu
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Tạo một DataTable để lưu kết quả
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Đưa dữ liệu vào DataGridView
                        dataGridViewThongTin.DataSource = dt;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }







        //xoa



        private void radioButtonXoa_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonXoa.Checked)
            {
                GroupBoxChucNang.Enabled = false;
                LoaddataMssv();
                LoadViPhamData();
                //giao dien xoa
                GiaodienXoa();
            }
        }

        private void XoaViPham()
        {
            // Kiểm tra xem có dòng nào trong DataGridView được chọn không
            if (dataGridViewThongTin.SelectedRows.Count > 0)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa vi phạm này?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn Yes, tiến hành xóa
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Lấy MSSV và ID vi phạm từ dòng được chọn trong DataGridView
                        string mssv = dataGridViewThongTin.SelectedRows[0].Cells["MSSV"].Value.ToString();
                        int viPhamId = Convert.ToInt32(dataGridViewThongTin.SelectedRows[0].Cells["MA_VI_PHAM"].Value);  // Giả sử có cột ViPhamID là khóa chính của vi phạm

                        // Câu lệnh SQL để xóa vi phạm theo ID
                        string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
                        string query = "DELETE FROM VI_PHAM WHERE MA_VI_PHAM = @ViPhamID And MSSV=@mssv";

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                // Thêm tham số vào câu lệnh SQL
                                cmd.Parameters.AddWithValue("@ViPhamID", viPhamId);
                                cmd.Parameters.AddWithValue("@mssv", mssv);

                                // Thực thi câu lệnh xóa
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Xóa vi phạm thành công!");
                                    // Sau khi xóa, cần load lại danh sách vi phạm trong DataGridView
                                    LoadViPhamByMSSV(mssv);
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa vi phạm!");
                                }
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
                else
                {
                    // Nếu người dùng chọn No, không làm gì
                    MessageBox.Show("Hủy bỏ xóa vi phạm.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vi phạm để xóa.");
            }
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            XoaViPham();
            comboBoxSinhVien.SelectedIndex = -1;
            LoadViPhamData();
        }



        //giao dien huy
        private void GiaodienHuy()
        {
            groupBoxTrangThai.Enabled = false;
            groupBoxThongTin.Enabled = false;
            groupBoxSuaXoa.Enabled = false;
            buttonHuy.Enabled = false;
            buttonHuy.BackColor = Color.Gray;
            buttonTroLai.Enabled = false;
            buttonTroLai.BackColor = Color.Gray;
            buttonLuu.Enabled = false;
            buttonLuu.BackColor = Color.Gray;
            GroupBoxChucNang.Enabled = true;
            radioButtonSua.Checked = false;
            radioButtonThem.Checked = false;
            radioButtonXoa.Checked = false;

        }


        //button huy sai chung 
        private void buttonHuy_Click(object sender, EventArgs e)
        {
            //ClearAllValues();
            comboBoxSinhVien.SelectedIndex = -1;
            comboBoxMucViPham.SelectedIndex = 0;
            textBoxGhiChu.Text = "";
            textBoxNoiDung.Text = "";
            radioButtonChuaxuly.Checked = false;
            radioButtonDaxuly.Checked = false;
            dateTimePickerNgayViPham.Value = DateTime.Now;

            LoadViPhamData();
            GiaodienHuy();
        }














        private void GiaoDienChonSua()
        {
            GroupBoxChucNang.Enabled = false;
            groupBoxTrangThai.Enabled = false;
            groupBoxThongTin.Enabled = true;
            groupBoxSuaXoa.Enabled = true;
            comboBoxSinhVien.Enabled = true;
            comboBoxMucViPham.Enabled = false;
            dateTimePickerNgayViPham.Enabled = false;
            textBoxGhiChu.Enabled = false;
            textBoxNoiDung.Enabled = false;
            radioButtonChuaxuly.Enabled = false;
            radioButtonDaxuly.Enabled = false;
            buttonHuy.Enabled = true;
            buttonHuy.BackColor = Color.Red;
            buttonTroLai.Enabled = false;
            buttonTroLai.BackColor = Color.Gray;
            buttonLuu.Enabled = false;
            buttonLuu.BackColor = Color.Gray;
            buttonSua.Enabled = true;
            buttonXoa.Enabled = false;
            buttonThem.Enabled = false;
        }

        private void radioButtonSua_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSua.Checked == true)
            {
                //giao dien sua               
                GiaoDienChonSua();
                LoaddataMssv();
                LoadMucViPham();
                LoadViPhamData();
            }
        }




        // Các biến để lưu giá trị ban đầu
        private string initialMaViPham;
        private string initialMSSV;
        private string initialMaMucViPham;
        private DateTime initialNgayViPham;
        private string initialNoiDung;
        private string initialGhiChu;
        private bool initialTrangThaiXuLy;
        private bool CoDangSua;


        private string GetMaMucViPham(string tenMucViPham)
        {
            string maMucViPham = string.Empty;
            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MA_MUC_VP FROM MUC_VI_PHAM  WHERE  TEN_MUC_VI_PHAM= @TenMucViPham";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số
                        cmd.Parameters.AddWithValue("@TenMucViPham", tenMucViPham);

                        // Lấy kết quả
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            maMucViPham = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Tên mức vi phạm không tồn tại.");
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            return maMucViPham;
        }

        //luu gia tri ban dau 
        private void SaveData()
        {
            // Lấy thông tin từ DataGridView và lưu lại các giá trị ban đầu
            if (dataGridViewThongTin.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewThongTin.SelectedRows[0];
                initialMaViPham = row.Cells["MA_VI_PHAM"].Value.ToString();
                initialMSSV = row.Cells["MSSV"].Value.ToString();
                initialMaMucViPham = GetMaMucViPham(row.Cells["TEN_MUC_VI_PHAM"].Value.ToString());//luu ma muc vi pham 
                initialNgayViPham = Convert.ToDateTime(row.Cells["NGAY_VI_PHAM"].Value);
                initialNoiDung = row.Cells["NOI_DUNG_VI_PHAM"].Value.ToString();
                initialGhiChu = row.Cells["GHI_CHU_VP"].Value.ToString();
                if (row.Cells["TRANG_THAI_XU_LY"].Value.ToString() == "True")
                {
                    initialTrangThaiXuLy = true;
                }
                else
                {
                    initialTrangThaiXuLy = false;
                }

            }
            else
            {
                MessageBox.Show("Vi long chon Sinh vien de sua");
            }
        }
        private void dataGridViewThongTin_SelectionChanged()
        {
            //neu o trang thai sua
            if (radioButtonSua.Checked == true && CoDangSua == true)
            {
                //dua thong tin len 
                if (dataGridViewThongTin.SelectedRows.Count > 0)
                {
                    // Gọi hàm để lưu lại thông tin ban đầu khi người dùng chọn dòng mới
                    SaveData();

                    //// Sau khi lưu dữ liệu ban đầu, bạn có thể tải dữ liệu vào các trường nhập liệu (giống như đã làm trước đây)
                    //// Cập nhật các trường nhập liệu theo dòng được chọn từ DataGridView
                    DataGridViewRow row = dataGridViewThongTin.SelectedRows[0];
                    comboBoxSinhVien.Text = row.Cells["MSSV"].Value.ToString();
                    comboBoxMucViPham.SelectedValue = GetMaMucViPham(row.Cells["TEN_MUC_VI_PHAM"].Value.ToString());
                    dateTimePickerNgayViPham.Value = Convert.ToDateTime(row.Cells["NGAY_VI_PHAM"].Value);
                    textBoxNoiDung.Text = row.Cells["NOI_DUNG_VI_PHAM"].Value.ToString();
                    textBoxGhiChu.Text = row.Cells["GHI_CHU_VP"].Value.ToString();
                    if (row.Cells["TRANG_THAI_XU_LY"].Value.ToString() == "True")
                    {
                        radioButtonDaxuly.Checked = true;
                    }
                    else
                    {
                        radioButtonChuaxuly.Checked = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vi long chon Sinh vien de sua");
            }
        }

        //giao dien su 
        private void GiaodienSua()
        {
            groupBoxTrangThai.Enabled = true;
            groupBoxSuaXoa.Enabled = true;
            comboBoxSinhVien.Enabled = true;
            comboBoxMucViPham.Enabled = true;
            dateTimePickerNgayViPham.Enabled = true;
            textBoxGhiChu.Enabled = true;
            textBoxNoiDung.Enabled = true;
            radioButtonChuaxuly.Enabled = true;
            radioButtonDaxuly.Enabled = true;
            buttonHuy.Enabled = true;
            buttonHuy.BackColor = Color.Red;
            buttonTroLai.Enabled = false;
            buttonTroLai.BackColor = Color.Gray;
            buttonLuu.Enabled = false;
            buttonLuu.BackColor = Color.Gray;
            buttonSua.Enabled = true;
            buttonXoa.Enabled = false;
            buttonThem.Enabled = false;
        }
        private void buttonSua_Click(object sender, EventArgs e)
        {
            SaveData();
            LoadViPhamByMSSV(initialMSSV);
            comboBoxSinhVien.Text = initialMSSV;
            if (radioButtonSua.Checked == true && comboBoxSinhVien.SelectedValue!=null)
            {
                CoDangSua = true;
                //khoa mss lai 
                GiaodienSua();
                dataGridViewThongTin_SelectionChanged();
            }
            else
            {
                MessageBox.Show("Vi long chon Sinh vien de sua");
            }
        }



        // Hàm kiểm tra sự thay đổi của dữ liệu
        private bool IsDataChanged()
        {
            // Kiểm tra các trường có thay đổi so với giá trị ban đầu hay không
            bool isTenMucViPhamChanged = comboBoxMucViPham.SelectedValue.ToString() != initialMaMucViPham;
            bool isNgayViPhamChanged = dateTimePickerNgayViPham.Value != initialNgayViPham;
            bool isNoiDungChanged = textBoxNoiDung.Text != initialNoiDung;
            bool isGhiChuChanged = textBoxGhiChu.Text != initialGhiChu;
            bool isTrangThaiXuLyChanged = radioButtonDaxuly.Checked != initialTrangThaiXuLy;

            // Nếu có ít nhất một sự thay đổi, trả về true
            return isTenMucViPhamChanged || isNgayViPhamChanged || isNoiDungChanged || isGhiChuChanged || isTrangThaiXuLyChanged;
        }

        // Sự kiện Ketqua để kiểm tra và bật/tắt nút Lưu
        private void Ketqua(object sender, EventArgs e)
        {
            if (CoDangSua==true &&radioButtonSua.Checked) {           
                // Kiểm tra xem dữ liệu có thay đổi hay không
                bool isChanged = IsDataChanged();

                // Nếu có sự thay đổi, bật nút Lưu và thay đổi màu sắc
                if (isChanged)
                {
                    buttonLuu.Enabled = true;      // Bật nút Lưu
                    buttonLuu.BackColor = Color.Green;  // Đổi màu nút Lưu thành xanh
                    buttonTroLai.Enabled = true;
                    buttonTroLai.BackColor = Color.Gold;
                }
                else
                {
                    buttonTroLai.Enabled = false;
                    buttonTroLai.BackColor = Color.Gray;
                    buttonLuu.Enabled = false;     // Tắt nút Lưu
                    buttonLuu.BackColor = Color.Gray; // Đổi màu nút Lưu thành xám
                }
            }
        }




















        private void ViPham_Load(object sender, EventArgs e)
        {
        }


        //ham luu vi pham 

        private void SaveViPham()
        {
            //// Kiểm tra nếu thông tin chưa được điền đầy đủ
            //if (string.IsNullOrEmpty(comboBoxSinhVien.Text) || string.IsNullOrEmpty(textBoxNoiDung.Text) ||
            //    string.IsNullOrEmpty(comboBoxMucViPham.Text) || string.IsNullOrEmpty(textBoxGhiChu.Text))
            //{
            //    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return; // Dừng hàm nếu thiếu thông tin
            //}

            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Câu lệnh cập nhật vi phạm trong cơ sở dữ liệu
                    string query = "UPDATE VI_PHAM " +
                                   "SET NGAY_VI_PHAM = @NgayViPham, " +
                                       "NOI_DUNG_VI_PHAM = @NoiDungViPham, " +
                                       "MA_MUC_VP = @MaMucVp, " +
                                       "TRANG_THAI_XU_LY = @TrangThaiXuLy, " +
                                       "GHI_CHU_VP = @GhiChu " +
                                   "WHERE MSSV = @MSSV AND MA_VI_PHAM = @MaViPham"; // Cập nhật dựa trên MSSV và mã vi phạm (có thể điều chỉnh cho phù hợp)

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string mavipham = initialMaViPham;
                        cmd.Parameters.AddWithValue("@MaViPham", mavipham);
                        // Lấy giá trị các tham số từ các control
                        string mssv = comboBoxSinhVien.SelectedValue.ToString();
                        cmd.Parameters.AddWithValue("@MSSV", mssv);

                        DateTime ngayViPham = dateTimePickerNgayViPham.Value;
                        cmd.Parameters.AddWithValue("@NgayViPham", ngayViPham);

                        string noiDungViPham = textBoxNoiDung.Text;
                        cmd.Parameters.AddWithValue("@NoiDungViPham", noiDungViPham);

                        string maMucVp = comboBoxMucViPham.SelectedValue.ToString();
                        cmd.Parameters.AddWithValue("@MaMucVp", maMucVp);

                        string trangThaiXuLy = radioButtonDaxuly.Checked ? "true" : "false";
                        cmd.Parameters.AddWithValue("@TrangThaiXuLy", trangThaiXuLy);

                        string ghiChu = textBoxGhiChu.Text;
                        cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                        // Lưu vi phạm vào cơ sở dữ liệu
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật vi phạm thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật vi phạm!");
                        }
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }




        private void buttonLuu_Click(object sender, EventArgs e)
        {
            if (radioButtonSua.Checked == true && CoDangSua == true)
            {
                //ham luu
                SaveViPham();
                //load thong tin vi pham lai 
                dataGridViewThongTin_SelectionChanged();
                LoadViPhamData();
            }
        }

        private void buttonTroLai_Click(object sender, EventArgs e)
        {
            LoadGiaTriCu();
            GiaodienSua();
        }
    }
}
