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
using WinformKTX;

namespace abc.HoanThanh.ThongKeViPham
{
    public partial class thongkevipham : Form
    {
        public thongkevipham()
        {
            InitializeComponent();
        }

        private void thongkevipham_Load(object sender, EventArgs e)
        {
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            LoadDataViPham();
            SqlConnection conn = kn.GetConnection();
            LoadcomboBoxMSSV(conn);
            LoadcomboBoxMucViPham(conn);
            DemSVvipham();
            DemSVviphamchuxuly();
            DemSVviphamdaxuly();
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        //ham du lieu vao data
        private void LoadDataViPham()
        {
            groupBoxThoiGian.Enabled = false;
            checkBoxThoigian.Checked = false;
            radioButtonAll.Checked = true;
            //DemSVDaThanhToan();
            //DemSVChuaThanhToan();
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "select SINH_VIEN.MSSV,SINH_VIEN.HOTEN_SV,MUC_VI_PHAM.TEN_VI_PHAM,VI_PHAM.NGAY_VI_PHAM,VI_PHAM.NOI_DUNG_VI_PHAM,VI_PHAM.GHI_CHU_VP,VI_PHAM.TRANG_THAI_XU_LY from VI_PHAM join SINH_VIEN on VI_PHAM.MSSV =SINH_VIEN.MSSV join MUC_VI_PHAM on VI_PHAM.MA_MUC_VP=MUC_VI_PHAM.MA_MUC_VP";
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

        //tai muc vi pham 
        private void LoadcomboBoxMucViPham(SqlConnection conn)
        {
            try
            {
                string query = "select MUC_VI_PHAM.TEN_VI_PHAM , MUC_VI_PHAM.MA_MUC_VP from MUC_VI_PHAM";
                using (var cmd = new SqlCommand(query, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxMucViPham.DataSource = table;
                    comboBoxMucViPham.DisplayMember = "TEN_VI_PHAM";
                    comboBoxMucViPham.ValueMember = "MA_MUC_VP";

                    comboBoxMucViPham.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ///tai mssv
        private void LoadcomboBoxMSSV(SqlConnection conn)
        {
            try
            {
                string query = "select MSSV from SINH_VIEN";
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


        //ham lay tt

        private (string mssv, string mucvipham, DateTime? ngayStart, DateTime? ngayEnd, string trangthai) GetUserInputs()
        {
            // Lấy giá trị từ các điều khiển giao diện người dùng
            string mssv = comboBoxSinhVien.SelectedValue != null ? comboBoxSinhVien.SelectedValue.ToString() : null; // Mã sinh viên
            string mucvipham = comboBoxMucViPham.SelectedValue != null ? comboBoxMucViPham.SelectedValue.ToString() : null; // Loại giường

            //DateTime? ngayStart = dateTimePickerBatDau.Checked ? dateTimePickerBatDau.Value : (DateTime?)null; // Ngày bắt đầu
            //DateTime? ngayEnd = dateTimePickerKetThuc.Checked ? dateTimePickerKetThuc.Value : (DateTime?)null; // Ngày kết thúc
            DateTime ngayStart = dateTimePickerBatDau.Value;
            DateTime ngayEnd = dateTimePickerKetThuc.Value;
            string trangthai;
            if (radioButtonDaxuly.Checked)
            {
                trangthai = "True";
            }
            else
            {
                trangthai = "False";
            }

            return (mssv, mucvipham, ngayStart, ngayEnd, trangthai);
        }


        //cau sql
        private string CreateSqlQuery(string mssv, string mucvipham, DateTime? ngayStart, DateTime? ngayEnd, string trangthai)
        {
            string query = "select SINH_VIEN.MSSV,SINH_VIEN.HOTEN_SV,MUC_VI_PHAM.TEN_VI_PHAM,VI_PHAM.NGAY_VI_PHAM,VI_PHAM.NOI_DUNG_VI_PHAM,VI_PHAM.GHI_CHU_VP,VI_PHAM.TRANG_THAI_XU_LY from VI_PHAM join SINH_VIEN on VI_PHAM.MSSV =SINH_VIEN.MSSV join MUC_VI_PHAM on VI_PHAM.MA_MUC_VP=MUC_VI_PHAM.MA_MUC_VP " +
                           "WHERE 1=1 ";  // Thêm điều kiện cơ bản (dễ dàng thêm các điều kiện khác sau này)

            bool hasCondition = false;
            if (radioButtonAll.Checked)
            {
                query += "AND ( VI_PHAM.TRANG_THAI_XU_LY=N'Đã xử lý' or VI_PHAM.TRANG_THAI_XU_LY =N'Chưa xử lý') ";
                hasCondition = false;
            }
            else if (radioButtonDaxuly.Checked)
            {
                query += "AND VI_PHAM.TRANG_THAI_XU_LY=N'Đã xử lý' ";
                hasCondition = false;
            }
            else if (radioButtonChuaxuly.Checked)
            {
                query += "AND VI_PHAM.TRANG_THAI_XU_LY=N'Chưa xử lý' ";
                hasCondition = false;
            }
            // Kiểm tra ComboBox loại giường
            if (comboBoxMucViPham.SelectedIndex != -1)
            {
                query += " AND VI_PHAM.MA_MUC_VP= @mucvipham ";
                hasCondition = true;
            }


            // Kiểm tra điều kiện ngày tháng
            if (checkBoxThoigian.Checked == true)
            {
                if ((ngayStart.Value > ngayEnd.Value.AddHours(1)))
                {
                    dateTimePickerBatDau.Value = DateTime.Now;
                    dateTimePickerKetThuc.Value = DateTime.Now;
                    MessageBox.Show("Vui Long Chon Khoang Thoi gian hop le");

                }
                else
                {
                    if (ngayStart.Value == ngayEnd.Value.AddHours(1))
                    {
                        query += " AND VI_PHAM.NGAY_VI_PHAM ='@NgayStart'";
                    }
                    else
                    {
                        // Nếu có ngày bắt đầu và ngày kết thúc, thêm điều kiện ngày
                        if (ngayStart.Value < ngayEnd.Value.AddHours(1))
                        {
                            query += " AND VI_PHAM.NGAY_VI_PHAM BETWEEN @NgayStart AND @NgayEnd";
                        }
                    }
                }
            }
            else
            {
                checkBoxThoigian.Checked = false;
                query += "";
            }


            // Nếu không có điều kiện tìm kiếm nào, thông báo lỗi
            if (!hasCondition)
            {
                query += "";
                //MessageBox.Show("Vui lòng chọn ít nhất một điều kiện tìm kiếm.");
            }
            // Nếu có MSSV, thêm điều kiện MSSV
            if (comboBoxSinhVien.SelectedIndex != -1)
            {
                query += " AND VI_PHAM.MSSV = @MSSV";
            }
            //MessageBox.Show(query);
            return query;
        }


        private void AddParametersToSqlCommand(SqlCommand cmd, string mssv, string mucvipham, DateTime? ngayStart, DateTime? ngayEnd, string trangthai)
        {
            // Nếu có loại giường, thêm tham số giường
            if (!string.IsNullOrEmpty(mucvipham))
            {
                cmd.Parameters.AddWithValue("@mucvipham", mucvipham);
            }


            // Nếu có ngày bắt đầu, thêm tham số ngày bắt đầu
            if (ngayStart.HasValue)
            {
                cmd.Parameters.AddWithValue("@NgayStart", ngayStart.Value);
            }

            // Nếu có ngày kết thúc, thêm tham số ngày kết thúc
            if (ngayEnd.HasValue)
            {
                cmd.Parameters.AddWithValue("@NgayEnd", ngayEnd.Value);
            }

            // Nếu có MSSV, thêm tham số MSSV
            if (!string.IsNullOrEmpty(mssv))
            {
                cmd.Parameters.AddWithValue("@MSSV", mssv);
            }
            if (radioButtonChuaxuly.Checked || radioButtonDaxuly.Checked)
            {
                cmd.Parameters.AddWithValue("@trangthai", trangthai);
            }
        }


        private void ExecuteSqlAndBindToGridView(SqlCommand cmd)
        {
            // Tạo một DataTable để lưu kết quả truy vấn
            DataTable dataTable = new DataTable();

            try
            {
                // Kết nối đến cơ sở dữ liệu
                //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conn = kn.GetConnection())
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
        private void GetThongTinvipham()
        {
            // Lấy các tham số tìm kiếm từ giao diện người dùng
            var (mssv, mucvipham, ngayStart, ngayEnd, trangthai) = GetUserInputs();

            // Xây dựng câu lệnh SQL với các điều kiện động
            string query = CreateSqlQuery(mssv, mucvipham, ngayStart, ngayEnd, trangthai);

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Khởi tạo đối tượng SqlCommand với câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        AddParametersToSqlCommand(cmd, mssv, mucvipham, ngayStart, ngayEnd, trangthai);
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
            GetThongTinvipham();
        }


        private void checkBoxThoigian_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxThoigian.Checked)
            {
                groupBoxThoiGian.Enabled = true;
            }
            else { 
                groupBoxThoiGian.Enabled=false;
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            checkBoxThoigian.Checked = false;
            radioButtonAll.Checked = true;
            comboBoxSinhVien.SelectedIndex = -1;
            comboBoxMucViPham.SelectedIndex = -1;      
            LoadDataViPham();
            thongkevipham_Load(sender, e);
        }

        private void DemSVvipham()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            var conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM VI_PHAM  ";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemViPham.Text = "Số vi phạm: " + Count.ToString();
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

        private void DemSVviphamchuxuly()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            var conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM VI_PHAM  where VI_PHAM.TRANG_THAI_XU_LY=N'Chưa xử lý'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemChuaXL.Text = "Số vi phạm chưa xử lý: " + Count.ToString();
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
        private void DemSVviphamdaxuly()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            var conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM VI_PHAM  where VI_PHAM.TRANG_THAI_XU_LY=N'Đã xử lý' ";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemDaXL.Text = "Số vi phạm đã xử lý: " + Count.ToString();
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
