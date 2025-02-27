using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformKTX;

namespace abc.HoanThanh.ThanhToan
{
    public partial class ThongKeThanhToan : Form
    {
        public ThongKeThanhToan()
        {
            SqlConnection conn = kn.GetConnection();
            InitializeComponent();
            TaidanhsachPhong(conn);
            LoadcomboBoxMSSV();
        }
        //ham load mssv
        KetnoiCSDL kn = new KetnoiCSDL();
        private void LoadcomboBoxMSSV()
        {
            try
            {
                radioButtonAll.Checked = true;
                SqlConnection conn = kn.GetConnection();
                //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
                string query = "select DISTINCT NOI_TRU.MSSV from THANH_TOAN_PHONG join NOI_TRU on THANH_TOAN_PHONG.MA_NOI_TRU=NOI_TRU.MA_NOI_TRU";
                using (var cmd = new SqlCommand(query, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxMSSV.DataSource = table;
                    comboBoxMSSV.DisplayMember = "MSSV";
                    comboBoxMSSV.ValueMember = "MSSV";

                    comboBoxMSSV.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }







        //ham du lieu vao data
        private void LoadThanhToanData()
        {
            DemSVDaThanhToan();
            DemSVChuaThanhToan();
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "select THANH_TOAN_PHONG.MA_THANH_TOAN_PHONG, SINH_VIEN.MSSV,SINH_VIEN.HOTEN_SV, LOAI_PHONG.TEN_LOAI_PHONG, TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,THANH_TOAN_PHONG.NGAY_THANH_TOAN,THANH_TOAN_PHONG.GIA_TIEN,THANH_TOAN_PHONG.TRANG_THAI_THANH_TOAN from THANH_TOAN_PHONG join NOI_TRU on THANH_TOAN_PHONG.MA_NOI_TRU=NOI_TRU.MA_NOI_TRU join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join SINH_VIEN on NOI_TRU.MSSV=SINH_VIEN.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG";
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

        private void TaidanhsachPhong(SqlConnection conn)
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
            if (comboBoxLoaiTang.SelectedItem == null)
            {
                return;
            }
            var selectedRow = comboBoxLoaiTang.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string loaitang = selectedRow["MA_LOAI_PHONG"].ToString();
                using (SqlConnection conn = kn.GetConnection())
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
                        comboBoxTang.Enabled = true;
                        comboBoxTang.SelectedIndex = -1;
                    }
                }
            }
        }
        private void comboBoxTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTang.SelectedItem == null)
            {
                return;
            }
            var selectedRow = comboBoxTang.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string tang = selectedRow["MA_TANG"].ToString();
                using (SqlConnection conn = kn.GetConnection())
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
                        comboBoxPhong.Enabled = true;
                        comboBoxPhong.SelectedIndex = -1;
                    }
                }
            }
        }

        private void comboBoxPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPhong.SelectedItem == null)
            {
                return;
            }
            var selectedRow = comboBoxPhong.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string MaPhong = selectedRow["MA_PHONG"].ToString();
                using (SqlConnection conn = kn.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MA_GIUONG,TEN_GIUONG FROM GIUONG where GIUONG.MA_PHONG= @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", MaPhong);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBoxGiuong.DataSource = dt;
                        comboBoxGiuong.DisplayMember = "TEN_GIUONG";
                        comboBoxGiuong.ValueMember = "MA_GIUONG";
                        comboBoxGiuong.Enabled = true;
                        comboBoxGiuong.SelectedIndex = -1;
                    }
                }
            }
        }


        //ham clean
        private void XoaThongtin()
        {
        }
        private void ThongKeThanhToan_Load(object sender, EventArgs e)
        {
            //ham load du lieu 
            XoaThongtin();
            LoadThanhToanData();
            groupBoxThoiGian.Enabled = false;
            checkBoxThoigian.Checked = false;
            comboBoxTang.Enabled = false;
            comboBoxPhong.Enabled = false;
            comboBoxGiuong.Enabled = false;

        }

        //ham luu thong tin ng dung nhap

        private (string mssv, string loaiTangGiuong, string phong, string tang, string loaiTang, DateTime? ngayStart, DateTime? ngayEnd) GetUserInputs()
        {
            // Lấy giá trị từ các điều khiển giao diện người dùng
            string mssv = comboBoxMSSV.SelectedValue != null ? comboBoxMSSV.SelectedValue.ToString() : null; // Mã sinh viên
            string loaiTangGiuong = comboBoxGiuong.SelectedValue != null ? comboBoxGiuong.SelectedValue.ToString() : null; // Loại giường
            string phong = comboBoxPhong.SelectedValue != null ? comboBoxPhong.SelectedValue.ToString() : null; // Phòng
            string tang = comboBoxTang.SelectedValue != null ? comboBoxTang.SelectedValue.ToString() : null; // Tầng
            string loaiTang = comboBoxLoaiTang.SelectedValue != null ? comboBoxLoaiTang.SelectedValue.ToString() : null; // Loại tầng
            //DateTime? ngayStart = dateTimePickerBatDau.Checked ? dateTimePickerBatDau.Value : (DateTime?)null; // Ngày bắt đầu
            //DateTime? ngayEnd = dateTimePickerKetThuc.Checked ? dateTimePickerKetThuc.Value : (DateTime?)null; // Ngày kết thúc
            DateTime ngayStart = dateTimePickerBatDau.Value;
            DateTime ngayEnd = dateTimePickerKetThuc.Value;

            return (mssv, loaiTangGiuong, phong, tang, loaiTang, ngayStart, ngayEnd);
        }

        //cau sql
        private string CreateSqlQuery(string mssv, string loaiTangGiuong, string phong, string tang, string loaiTang, DateTime? ngayStart, DateTime? ngayEnd)
        {
            string query = "SELECT THANH_TOAN_PHONG.MA_THANH_TOAN_PHONG, SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV, LOAI_PHONG.TEN_LOAI_PHONG, TANG.TEN_TANG, PHONG.TEN_PHONG, GIUONG.TEN_GIUONG, THANH_TOAN_PHONG.NGAY_THANH_TOAN, THANH_TOAN_PHONG.GIA_TIEN, THANH_TOAN_PHONG.TRANG_THAI_THANH_TOAN FROM THANH_TOAN_PHONG " +
                           "JOIN NOI_TRU ON THANH_TOAN_PHONG.MA_NOI_TRU = NOI_TRU.MA_NOI_TRU " +
                           "JOIN GIUONG ON NOI_TRU.MA_GIUONG = GIUONG.MA_GIUONG " +
                           "JOIN SINH_VIEN ON NOI_TRU.MSSV = SINH_VIEN.MSSV " +
                           "JOIN PHONG ON NOI_TRU.MA_PHONG = PHONG.MA_PHONG " +
                           "JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG " +
                           "JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG " +
                           "WHERE 1=1 ";  // Thêm điều kiện cơ bản (dễ dàng thêm các điều kiện khác sau này)

            bool hasCondition = false;
            if (radioButtonAll.Checked)
            {
                query += "AND ( THANH_TOAN_PHONG.TRANG_THAI_THANH_TOAN=N'Đã thanh toán' or THANH_TOAN_PHONG.TRANG_THAI_THANH_TOAN=N'Chưa thanh toán') ";
                hasCondition = false;
            }
            else if (radioButtonDaTT.Checked)
            {
                query += "AND THANH_TOAN_PHONG.TRANG_THAI_THANH_TOAN=N'Đã thanh toán'";
                hasCondition = false;
            }
            else if (radioButtonChuaTT.Checked)
            {
                query += "AND THANH_TOAN_PHONG.TRANG_THAI_THANH_TOAN=N'Chưa thanh toán'";
                hasCondition = false;
            }
            // Kiểm tra ComboBox loại giường
            if (comboBoxGiuong.SelectedIndex != -1)
            {
                query += " AND GIUONG.MA_GIUONG = @LoaiTangGiuong";
                hasCondition = true;
            }

            // Kiểm tra ComboBox phòng
            if (comboBoxPhong.SelectedIndex != -1)
            {
                query += " AND PHONG.MA_PHONG = @Phong";
                hasCondition = true;
            }

            // Kiểm tra ComboBox tầng
            if (comboBoxTang.SelectedIndex != -1)
            {
                query += " AND TANG.MA_TANG = @Tang";
                hasCondition = true;
            }

            // Kiểm tra ComboBox loại tầng
            if (comboBoxLoaiTang.SelectedIndex != -1)
            {
                query += " AND LOAI_PHONG.MA_LOAI_PHONG = @LoaiTang";
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
                        query += " AND THANH_TOAN_PHONG.NGAY_THANH_TOAN ='@NgayStart'";
                    }
                    else
                    {
                        // Nếu có ngày bắt đầu và ngày kết thúc, thêm điều kiện ngày
                        if (ngayStart.Value < ngayEnd.Value.AddHours(1))
                        {
                            query += " AND THANH_TOAN_PHONG.NGAY_THANH_TOAN BETWEEN @NgayStart AND @NgayEnd";
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
            if (comboBoxMSSV.SelectedIndex != -1)
            {
                query += " AND NOI_TRU.MSSV = @MSSV";
            }

            return query;
        }


        private void AddParametersToSqlCommand(SqlCommand cmd, string mssv, string loaiTangGiuong, string phong, string tang, string loaiTang, DateTime? ngayStart, DateTime? ngayEnd)
        {
            // Nếu có loại giường, thêm tham số giường
            if (!string.IsNullOrEmpty(loaiTangGiuong))
            {
                cmd.Parameters.AddWithValue("@LoaiTangGiuong", loaiTangGiuong);
            }

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



        private void GetThongTinThanhToan()
        {
            // Lấy các tham số tìm kiếm từ giao diện người dùng
            var (mssv, loaiTangGiuong, phong, tang, loaiTang, ngayStart, ngayEnd) = GetUserInputs();

            // Xây dựng câu lệnh SQL với các điều kiện động
            string query = CreateSqlQuery(mssv, loaiTangGiuong, phong, tang, loaiTang, ngayStart, ngayEnd);

            using (SqlConnection conn = kn.GetConnection())
                try
                {
                    conn.Open();

                    // Khởi tạo đối tượng SqlCommand với câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        AddParametersToSqlCommand(cmd, mssv, loaiTangGiuong, phong, tang, loaiTang, ngayStart, ngayEnd);
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



        private void button1_Click(object sender, EventArgs e)
        {

            GetThongTinThanhToan();
        }

        private void checkBoxThoigian_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxThoigian.Checked)
            {
                groupBoxThoiGian.Enabled = true;
            }
            else { 
                groupBoxThoiGian.Enabled = false;
            }
        }

        private void buttonTroLai_Click(object sender, EventArgs e)
        {
            checkBoxThoigian.Checked = false;
            radioButtonAll.Checked = true;
            comboBoxLoaiTang.SelectedIndex = -1;
            comboBoxTang.SelectedIndex = -1;
            comboBoxPhong.SelectedIndex = -1;
            comboBoxMSSV.SelectedIndex = -1;
            comboBoxGiuong.SelectedIndex = -1;
            LoadThanhToanData();
            ThongKeThanhToan_Load(sender, e);
        }




        //dem 
        private void DemSVDaThanhToan()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM THANH_TOAN_PHONG WHERE TRANG_THAI_THANH_TOAN=N'Đã thanh toán'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSVDaTT.Text = "Số sinh viên đã thanh toán: " + Count.ToString();
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
        private void DemSVChuaThanhToan()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM THANH_TOAN_PHONG WHERE TRANG_THAI_THANH_TOAN=N'Chưa thanh toán'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSVChuTT.Text = "Số sinh viên chưa thanh toán: " + Count.ToString();
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







