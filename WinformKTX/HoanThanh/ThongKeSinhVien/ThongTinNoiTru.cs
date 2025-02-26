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


namespace abc.HoanThanh.ThongKeSinhVien
{
    public partial class ThongTinNoiTru : Form
    {
        public ThongTinNoiTru()
        {
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            InitializeComponent();
            SqlConnection conn = kn.GetConnection();
            TaidanhsachPhong(conn);
            DemSVChuaNoiTru();
            DemSVNoiTru();
            DemSV();
        }
        KetnoiCSDL kn = new KetnoiCSDL();
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

                        comboBoxGiuong.SelectedIndex = -1;
                    }
                }
            }
        }



        //khi mo form thi thuc hien 
        private void ThongTinNoiTru_Load(object sender, EventArgs e)
        {
            //dat gia tri mac dinh cho radioButtonChuaNoiTru
            radioButtonChuaNoiTru.Checked = true;
            //dat gia tri mac dinh cho radioButtonDangNoiTru
            radioButtonDangNoiTru.Checked = false;
            //dat gia tri mac dinh cho radioButtonChoGianHan
            radioButtonChoGianHan.Checked = false;
            //dat gia tri mac dinh cho groupBoxThongTin
            groupBoxThongTin.Enabled = false;
        }


        //ham khi check vao radioButtonDangNoiTru
        private void radioButtonDangNoiTru_CheckedChanged(object sender, EventArgs e)
        {
            //khi check vao radioButtonDangNoiTru thi se hien thi groupBoxThongTin
            groupBoxThongTin.Enabled = true;

        }


        //ham khi check vao radioButtonChuaNoiTru
        private void radioButtonChuaNoiTru_CheckedChanged(object sender, EventArgs e)
        {
            //khi check vao radioButtonChuaNoiTru thi se an groupBoxThongTin
            groupBoxThongTin.Enabled = false;
        }



        //ham check vao radioButtonChoGianHan
        private void radioButtonChoGianHan_CheckedChanged(object sender, EventArgs e)
        {
            //khi check vao radioButtonChoGianHan thi se hien thi groupBoxThongTin
            groupBoxThongTin.Enabled = false;
        }




        //ham tim kiem thong tin sinh vien  noi tru
        private void TimkiemSinhVienDangNoiTru()
        {

            try
            {
                if (comboBoxLoaiTang.SelectedValue == null && comboBoxTang.SelectedValue == null && comboBoxPhong.SelectedValue == null)
                {
                    using (SqlConnection conn = kn.GetConnection())
                    {
                        conn.Open();

                        // Kiểm tra sự tồn tại của bảng GIUONG
                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GIUONG'", conn);
                        int tableCount = (int)cmd.ExecuteScalar();
                        string query = " select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU,LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,NOI_TRU.TRANG_THAI_NOI_TRU,LOAI_PHONG.GIA_PHONG from SINH_VIEN join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG =LOAI_PHONG.MA_LOAI_PHONG  WHERE  NOI_TRU.TRANG_THAI_NOI_TRU=N'Đang nội trú'";
                        SqlCommand command = new SqlCommand(query, conn);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet set = new DataSet();
                        adapter.Fill(set, "WinFormKTX");
                        dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                    }
                        //MessageBox.Show("Vui lòng chọn ít nhất một tiêu chí để tìm kiếm.");
                    return;
                }

                // Mở kết nối cơ sở dữ liệu
                using (var con = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True"))
                {
                    con.Open();

                    // Kiểm tra sự tồn tại của bảng GIUONG
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GIUONG'", con);
                    int tableCount = (int)cmd.ExecuteScalar();
                    if (tableCount != 1)
                    {
                        MessageBox.Show("Table 'GIUONG' không tồn tại.");
                        return;
                    }

                    // Kiểm tra nếu chọn theo loại phòng
                    if (comboBoxTang.SelectedValue == null)
                    {
                        if (comboBoxLoaiTang.SelectedValue != null)
                        {
                            string MaLoai = comboBoxLoaiTang.SelectedValue.ToString();

                            string query = " select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU,LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,NOI_TRU.TRANG_THAI_NOI_TRU,LOAI_PHONG.GIA_PHONG from SINH_VIEN join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG =LOAI_PHONG.MA_LOAI_PHONG  WHERE LOAI_PHONG.MA_LOAI_PHONG = @MaLoai And NOI_TRU.TRANG_THAI_NOI_TRU=N'Đang nội trú'";
                            SqlCommand command = new SqlCommand(query, con);
                            command.Parameters.AddWithValue("@MaLoai", MaLoai);
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataSet set = new DataSet();
                            adapter.Fill(set, "WinFormKTX");
                            dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];

                        }
                        else
                        {
                            MessageBox.Show("Vui lòng chọn loại phòng.");
                        }
                    }
                    // Kiểm tra nếu chọn theo tầng
                    else
                    {
                        if (comboBoxPhong.SelectedValue == null)
                        {
                            string MaTang = comboBoxTang.SelectedValue.ToString();

                            string query = "select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU,LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,NOI_TRU.TRANG_THAI_NOI_TRU,LOAI_PHONG.GIA_PHONG from SINH_VIEN join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG " +
                                           "WHERE TANG.MA_TANG = @MaTang And NOI_TRU.TRANG_THAI_NOI_TRU=N'Đang Nội Trú'";

                            SqlCommand command = new SqlCommand(query, con);
                            command.Parameters.AddWithValue("@MaTang", MaTang);
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataSet set = new DataSet();
                            adapter.Fill(set, "WinFormKTX");
                            dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                        }
                        else
                        {
                            if (comboBoxGiuong.SelectedValue == null)
                            {

                                // Kiểm tra nếu chọn theo phòng
                                string MaPhong = comboBoxPhong.SelectedValue.ToString();

                                string query = "select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU,LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,NOI_TRU.TRANG_THAI_NOI_TRU,LOAI_PHONG.GIA_PHONG from SINH_VIEN join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG " +
                                               "WHERE PHONG.MA_PHONG = @MaPhong And NOI_TRU.TRANG_THAI_NOI_TRU=N'Đang Nội Trú'";

                                SqlCommand command = new SqlCommand(query, con);
                                command.Parameters.AddWithValue("@MaPhong", MaPhong);
                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataSet set = new DataSet();
                                adapter.Fill(set, "WinFormKTX");
                                dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                            }
                            else
                            {
                                string MaGiuong = comboBoxGiuong.SelectedValue.ToString();
                                string query = "select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU,LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,NOI_TRU.TRANG_THAI_NOI_TRU,LOAI_PHONG.GIA_PHONG from SINH_VIEN join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG " +
                                               "WHERE GIUONG.MA_GIUONG = @MaGiuong And NOI_TRU.TRANG_THAI_NOI_TRU=N'Đang Nội Trú'";
                                SqlCommand command = new SqlCommand(query, con);
                                command.Parameters.AddWithValue("@MaGiuong", MaGiuong);
                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataSet set = new DataSet();
                                adapter.Fill(set, "WinFormKTX");
                                dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        //ham tim kiem thong tin sinh vien chua noi tru
        private void TimkiemSinhVienChuaNoiTru()
        {
            //chuoi ket noi
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            //chuoi query
            string query = "select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU from SINH_VIEN left join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV  where NOI_TRU.TRANG_THAI_NOI_TRU is null";
            //tao ket noi
            using (SqlConnection connection = kn.GetConnection())
            {
                //mo ket noi
                connection.Open();
                //tao command
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //tao dataAdapter
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        //tao dataset
                        DataSet dataset = new DataSet();
                        //nap du lieu tu dataAdapter vao dataset
                        adapter.Fill(dataset);
                        //hien thi du lieu len dataGridView
                        dataGridViewThongTin.DataSource = dataset.Tables[0];
                    }
                }
            }
        }

        //ham tim kiem thong tin sinh vien cho gian han
        private void TimkiemSinhVienChoGianHan()
        {
            //chuoi ket noi
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            //chuoi query
            string query = "select SINH_VIEN.MSSV, SINH_VIEN.HOTEN_SV,SINH_VIEN.NGAY_SINH,SINH_VIEN.GIOI_TINH,SINH_VIEN.SDT_SINHVIEN,SINH_VIEN.SDT_NGUOITHAN,SINH_VIEN.QUE_QUAN,SINH_VIEN.EMAIL,NOI_TRU.NGAY_BAT_DAU_NOI_TRU,NOI_TRU.NGAY_KET_THUC_NOI_TRU,LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,GIUONG.TEN_GIUONG,NOI_TRU.TRANG_THAI_NOI_TRU,LOAI_PHONG.GIA_PHONG from SINH_VIEN join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV join PHONG on NOI_TRU.MA_PHONG=PHONG.MA_PHONG join GIUONG on NOI_TRU.MA_GIUONG=GIUONG.MA_GIUONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG where NOI_TRU.TRANG_THAI_NOI_TRU= N'Chờ gia hạn'";
            //tao ket noi
            using (SqlConnection connection = kn.GetConnection())
            {
                //mo ket noi
                connection.Open();
                //tao command
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //tao dataAdapter
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        //tao dataset
                        DataSet dataset = new DataSet();
                        //nap du lieu tu dataAdapter vao dataset
                        adapter.Fill(dataset);
                        //hien thi du lieu len dataGridView
                        dataGridViewThongTin.DataSource = dataset.Tables[0];
                    }
                }
            }

        }





        private void buttonTimkiemgiuong_Click(object sender, EventArgs e)
        {
            //neu check vao radioButtonDangNoiTru
            if (radioButtonDangNoiTru.Checked)
            {
                //thuc hien ham TimKiemGiuongDangNoiTru
                TimkiemSinhVienDangNoiTru();
            }
            //neu check vao radioButtonChuaNoiTru
            else if (radioButtonChuaNoiTru.Checked)
            {
                //thuc hien ham TimKiemGiuongChuaNoiTru
                TimkiemSinhVienChuaNoiTru();
            }
            //neu check vao radioButtonChoGianHan
            else if (radioButtonChoGianHan.Checked)
            {
                //thuc hien ham TimKiemGiuongChoGianHan
                TimkiemSinhVienChoGianHan();
            }
        }





        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //dat lai cac gia tri cua cac field
            ClearFields();
        }

        //ham set lai toan bo gia tri cua field
        private void ClearFields()
        {
            comboBoxGiuong.SelectedValue = -1;
            comboBoxTang.SelectedValue = -1;
            comboBoxPhong.SelectedValue = -1;
            comboBoxLoaiTang.SelectedValue = -1;
            radioButtonChuaNoiTru.Checked = true;
        }


        //dem
        private void DemSV()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn SQL để đếm số sinh viên
            string query = "select COUNT(SINH_VIEN.MSSV) from SINH_VIEN  ";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSV.Text = "Tong Số sinh viên : " + Count.ToString();
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
        private void DemSVChuaNoiTru()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "select COUNT(SINH_VIEN.MSSV) from SINH_VIEN left join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV where NOI_TRU.MSSV is null";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSVChuaNoiTru.Text = "Số sinh viên không nội trú: " + Count.ToString();
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

        private void DemSVNoiTru()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "select COUNT(SINH_VIEN.MSSV) from SINH_VIEN left join NOI_TRU on SINH_VIEN.MSSV=NOI_TRU.MSSV where NOI_TRU.MSSV is not null\r\n";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSVNoiTru.Text = "Số sinh viên đang nội trú: " + Count.ToString();
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
