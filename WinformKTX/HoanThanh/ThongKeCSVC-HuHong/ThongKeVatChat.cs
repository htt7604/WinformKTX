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

namespace WinformKTX.HoanThanh.ThongKeCSVC_HuHong
{
    public partial class ThongKeVatChat : Form
    {
        public ThongKeVatChat()
        {
            InitializeComponent();
            var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            taidanhsachPhong(conn);
            LoadcomboBoxTinhTrang();
            LoadcomboBoxTenCSVC();
            LoadVatChatData();
        }



        private void taidanhsachPhong(SqlConnection conn)
        {
            try
            {
                string query = "SELECT MA_LOAI_PHONG,TEN_LOAI_PHONG FROM LOAI_PHONG";
                using (var cmd = new SqlCommand(query, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxLoaiTang.DataSource = table;
                    comboBoxLoaiTang.DisplayMember = "TEN_LOAI_PHONG";
                    comboBoxLoaiTang.ValueMember = "MA_LOAI_PHONG";

                    comboBoxLoaiTang.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxLoaiTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxTang.Enabled = true;
            if (comboBoxLoaiTang.SelectedItem == null)
            {
                return;
            }
            var selectedRow = comboBoxLoaiTang.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string loaitang = selectedRow["MA_LOAI_PHONG"].ToString();
                using (var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = "SELECT MA_TANG,TEN_TANG FROM LOAI_PHONG join TANG on TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG WHERE tang.MA_LOAI_PHONG = @loaitang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@loaitang", loaitang);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBoxTang.DataSource = dt;
                        comboBoxTang.DisplayMember = "TEN_TANG";
                        comboBoxTang.ValueMember = "MA_TANG";

                        comboBoxTang.SelectedIndex = -1;
                    }
                }
            }
        }
        private void comboBoxTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPhong.Enabled = true;
            if (comboBoxTang.SelectedItem == null)
            {
                return;
            }
            var selectedRow = comboBoxTang.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string tang = selectedRow["MA_TANG"].ToString();
                using (var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = "SELECT MA_PHONG,TEN_PHONG FROM PHONG WHERE MA_TANG = @tang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tang", tang);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBoxPhong.DataSource = dt;
                        comboBoxPhong.DisplayMember = "TEN_PHONG";
                        comboBoxPhong.ValueMember = "MA_PHONG";
                        comboBoxPhong.SelectedIndex = -1;
                    }
                }
            }
        }


        //tai combobox TinhTrang
        private void LoadcomboBoxTinhTrang()
        {
            using (var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True"))
            {
                conn.Open();
                string query = "SELECT DISTINCT CO_SO_VAT_CHAT.TINH_TRANG FROM CO_SO_VAT_CHAT where TINH_TRANG is not null ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBoxTinhTrang.DataSource = dt;
                    comboBoxTinhTrang.DisplayMember = "TINH_TRANG";
                    comboBoxTinhTrang.ValueMember = "TINH_TRANG";
                    comboBoxTinhTrang.SelectedIndex = -1;
                }
            }
        }

        //tai ten csvc
        //tai combobox TinhTrang
        private void LoadcomboBoxTenCSVC()
        {
            using (var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True"))
            {
                conn.Open();
                string query = "SELECT DISTINCT CO_SO_VAT_CHAT.TEN_CSVC FROM CO_SO_VAT_CHAT where TEN_CSVC is not null ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBoxTenVatChat.DataSource = dt;
                    comboBoxTenVatChat.DisplayMember = "TEN_CSVC";
                    comboBoxTenVatChat.ValueMember = "TEN_CSVC";
                    comboBoxTenVatChat.SelectedIndex = -1;
                }
            }
        }

        //ham load datat vao 
        //ham du lieu vao data
        private void LoadVatChatData()
        {
            //DemSVDaThanhToan();
            //DemSVChuaThanhToan();
            string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select CO_SO_VAT_CHAT.TEN_CSVC, LOAI_PHONG.TEN_LOAI_PHONG, TANG.TEN_TANG, PHONG.TEN_PHONG,CO_SO_VAT_CHAT.SO_LUONG,CO_SO_VAT_CHAT.TINH_TRANG,ISNULL(CO_SO_VAT_CHAT.GHI_CHU,'Khong co') from CO_SO_VAT_CHAT join PHONG on CO_SO_VAT_CHAT.MA_PHONG=PHONG.MA_PHONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG \r\n";
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
        private void ThongKeVatChat_Load(object sender, EventArgs e)
        {
            //ham load du lieu 
            //XoaThongtin();
            LoadVatChatData();
        }


        //ham luu thong tin ng dung nhap

        private (string phong, string tang, string loaiTang, string TenCSVC, string TinhTrang) GetUserInputs()
        {
            // Lấy giá trị từ các điều khiển giao diện người dùng
            string phong = comboBoxPhong.SelectedValue != null ? comboBoxPhong.SelectedValue.ToString() : null; // Phòng
            string tang = comboBoxTang.SelectedValue != null ? comboBoxTang.SelectedValue.ToString() : null; // Tầng
            string loaiTang = comboBoxLoaiTang.SelectedValue != null ? comboBoxLoaiTang.SelectedValue.ToString() : null; // Loại tầng
            string TenCSVC = comboBoxTenVatChat.SelectedValue != null ? comboBoxTenVatChat.SelectedValue.ToString() : null;
            string TinhTrang = comboBoxTinhTrang.SelectedValue != null ? comboBoxTinhTrang.SelectedValue.ToString() : null;

            return (phong, tang, loaiTang, TenCSVC, TinhTrang);
        }

        private string CreateSqlQuery(string phong, string tang, string loaiTang, string TenCSVC, string TinhTrang)
        {
            string query = "SELECT CO_SO_VAT_CHAT.TEN_CSVC, LOAI_PHONG.TEN_LOAI_PHONG, TANG.TEN_TANG, PHONG.TEN_PHONG, CO_SO_VAT_CHAT.SO_LUONG, CO_SO_VAT_CHAT.TINH_TRANG, ISNULL(CO_SO_VAT_CHAT.GHI_CHU, 'Khong co') " +
                           "FROM CO_SO_VAT_CHAT " +
                           "JOIN PHONG ON CO_SO_VAT_CHAT.MA_PHONG = PHONG.MA_PHONG " +
                           "JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG " +
                           "JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG " +
                           "WHERE 1 = 1 ";

            bool hasCondition = false;

            // Kiểm tra ComboBox phòng
            if (!string.IsNullOrEmpty(phong))
            {
                query += " AND PHONG.MA_PHONG = @Phong ";
                hasCondition = true;
            }

            // Kiểm tra ComboBox tầng
            if (!string.IsNullOrEmpty(tang))
            {
                query += " AND TANG.MA_TANG = @Tang ";
                hasCondition = true;
            }

            // Kiểm tra ComboBox loại tầng
            if (!string.IsNullOrEmpty(loaiTang))
            {
                query += " AND LOAI_PHONG.MA_LOAI_PHONG = @LoaiTang ";
                hasCondition = true;
            }

            // Kiểm tra TenCSVC
            if (!string.IsNullOrEmpty(TenCSVC))
            {
                query += " AND CO_SO_VAT_CHAT.TEN_CSVC = @TenCSVC ";
                hasCondition = true;
            }

            // Kiểm tra TinhTrang
            if (!string.IsNullOrEmpty(TinhTrang))
            {
                query += " AND CO_SO_VAT_CHAT.TINH_TRANG = @TinhTrang ";
                hasCondition = true;
            }

            // Nếu không có điều kiện tìm kiếm nào, thông báo lỗi
            if (!hasCondition)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một điều kiện tìm kiếm.");
            }

            return query;
        }

        private void AddParametersToSqlCommand(SqlCommand cmd, string phong, string tang, string loaiTang, string TenCSVC, string TinhTrang)
        {
            // Nếu có phòng, thêm tham số phòng
            if (!string.IsNullOrEmpty(phong))
            {
                cmd.Parameters.AddWithValue("@Phong", phong);
            }

            // Nếu có tầng, thêm tham số tầng
            if (!string.IsNullOrEmpty(tang))
            {
                cmd.Parameters.AddWithValue("@Tang", tang);
            }

            // Nếu có loại tầng, thêm tham số loại tầng
            if (!string.IsNullOrEmpty(loaiTang))
            {
                cmd.Parameters.AddWithValue("@LoaiTang", loaiTang);
            }

            // Nếu có TenCSVC
            if (!string.IsNullOrEmpty(TenCSVC))
            {
                DemCSVC(TenCSVC);
                cmd.Parameters.AddWithValue("@TenCSVC", TenCSVC);
            }

            // Nếu có TinhTrang
            if (!string.IsNullOrEmpty(TinhTrang))
            {
                cmd.Parameters.AddWithValue("@TinhTrang", TinhTrang);
            }
        }


        private void ExecuteSqlAndBindToGridView(SqlCommand cmd)
        {
            // Tạo một DataTable để lưu kết quả truy vấn
            DataTable dataTable = new DataTable();

            try
            {
                // Kết nối đến cơ sở dữ liệu
                string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Thiết lập đối tượng DataAdapter với câu lệnh SQL
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        // Điền dữ liệu vào DataTable từ câu lệnh SQL
                        dataAdapter.Fill(dataTable);
                    }

                    // Gán DataTable cho DataGridView để hiển thị kết quả
                    dataGridViewThongTin.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message);
            }
        }


        private void GetThongTinThanhToan()
        {
            // Lấy các tham số tìm kiếm từ giao diện người dùng
            var (phong, tang, loaiTang, TenCSVC, TinhTrang) = GetUserInputs();

            // Xây dựng câu lệnh SQL với các điều kiện động
            string query = CreateSqlQuery(phong, tang, loaiTang, TenCSVC, TinhTrang);

            using (SqlConnection conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True"))
            {
                try
                {
                    conn.Open();

                    // Khởi tạo đối tượng SqlCommand với câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        AddParametersToSqlCommand(cmd, phong, tang, loaiTang, TenCSVC, TinhTrang);
                        ////kiem tra cau truy van
                        //MessageBox.Show(query);
                        // Thực thi câu lệnh và hiển thị kết quả lên DataGridView
                        ExecuteSqlAndBindToGridView(cmd);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            GetThongTinThanhToan();
        }
        //ham clear
        private void Clear()
        {
            comboBoxLoaiTang.SelectedIndex = -1;
            comboBoxTang.SelectedIndex = -1;
            comboBoxPhong.SelectedIndex = -1;
            comboBoxTenVatChat.SelectedIndex = -1;
            comboBoxTinhTrang.SelectedIndex = -1;
        }

        private void buttonDatLai_Click(object sender, EventArgs e)
        {
            Clear();
            LoadVatChatData();
        }


        //dem tong so luong CSVC dua tren ten+soluong
        private void DemCSVC(string TenCSVC)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");

            // Truy vấn SQL để đếm số sinh viên
            string query = "select Sum(CO_SO_VAT_CHAT.SO_LUONG) from CO_SO_VAT_CHAT where CO_SO_VAT_CHAT.TEN_CSVC =@TenCSVC";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@TenCSVC", TenCSVC);
            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemCSVC.Text = "Tong SL " + TenCSVC +" :"+ Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối
                conn.Close();
            }
        }
    }
}
