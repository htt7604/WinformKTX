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
    public partial class ThongtinSV : Form
    {
        public ThongtinSV()
        {
            InitializeComponent();
            LoaddataMssv();
            LoaddataHoTen();

            CountStudents();
            CountStudentsNam();
            CountStudentsNu();
        }
        //private SqlConnection conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
        KetnoiCSDL kn = new KetnoiCSDL();
        //load mssv
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
        private void LoaddataHoTen()
        {
            try
            {

                string query = "SELECT MSSV ,SINH_VIEN.HOTEN_SV FROM SINH_VIEN";
                using (var cmd = new SqlCommand(query, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    var table = new DataTable();
                    da.Fill(table);
                    comboBoxHoTen.DataSource = table;
                    comboBoxHoTen.DisplayMember = "HOTEN_SV";
                    comboBoxHoTen.ValueMember = "HOTEN_SV";

                    comboBoxHoTen.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //ham dem so sinh vien 
        private void CountStudents()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM SINH_VIEN";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int studentCount = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSV.Text = "Tổng Số sinh viên: " + studentCount.ToString();
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
        private void CountStudentsNam()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM SINH_VIEN Where SINH_VIEN.GIOI_TINH=N'Nam'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int studentCount = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSVNam.Text = " Số sinh viên nam: " + studentCount.ToString();
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
        private void CountStudentsNu()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(*) FROM SINH_VIEN Where SINH_VIEN.GIOI_TINH=N'Nữ'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int studentCount = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSVNu.Text = "Số sinh viên nữ: " + studentCount.ToString();
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



        ////tim kiem bang ho ten 
        private void comboBoxMSSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSinhVien.SelectedItem != null && comboBoxSinhVien.SelectedItem.ToString() != null && radioButtonMSSV.Checked == true)
            {
                string selectedMSSV = comboBoxSinhVien.SelectedValue.ToString();
                // Tìm kiếm sinh viên theo MSSV đã chọn
                SearchStudentByMSSV(selectedMSSV);
                comboBoxHoTen.Enabled = false;
            }
        }


        private void SearchStudentByMSSV(string mssv)
        {
            SqlConnection conn = kn.GetConnection();

            string query = "SELECT * FROM SINH_VIEN WHERE MSSV = @MSSV";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@MSSV", mssv);  // Sử dụng tham số để tránh SQL Injection

            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adapter.Fill(dt);

                // Hiển thị dữ liệu vào DataGridView
                dataGridViewThongTin.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm sinh viên: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void radioButtonMSSV_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSinhVien.Enabled = true;

            comboBoxHoTen.Enabled = false;
        }

        private void radioButtonHoTen_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxHoTen.Enabled = true;
            comboBoxSinhVien.Enabled = false;
        }

        private void ThongtinSV_Load(object sender, EventArgs e)
        {
            comboBoxHoTen.Enabled = false;
            comboBoxSinhVien.Enabled = false;
            LoadAllSV();
        }



        //tim theo ten 
        private void SearchStudentByHoTen(string selectedHoTen)
        {
            SqlConnection conn = kn.GetConnection();

            string query = "SELECT * FROM SINH_VIEN WHERE SINH_VIEN.HOTEN_SV = N'" + selectedHoTen + "'";
            SqlCommand command = new SqlCommand(query, conn);
            //command.Parameters.AddWithValue("@HoTen", selectedHoTen);  // Sử dụng tham số để tránh SQL Injection

            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adapter.Fill(dt);

                // Hiển thị dữ liệu vào DataGridView
                dataGridViewThongTin.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm sinh viên: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void comboBoxHoTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHoTen.SelectedItem != null && comboBoxHoTen.SelectedItem.ToString() != null && radioButtonHoTen.Checked == true)
            {
                string selectedHoTen = comboBoxHoTen.SelectedValue.ToString();
                // Tìm kiếm sinh viên theo MSSV đã chọn
                SearchStudentByHoTen(selectedHoTen);
                comboBoxSinhVien.Enabled = false;
            }
        }
        //ham loy toan bo sinh vien 
        private void LoadAllSV()
        {
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT   MSSV,HOTEN_SV, CCCD,NGAY_SINH,GIOI_TINH,SDT_SINHVIEN,SDT_NGUOITHAN,QUE_QUAN,EMAIL FROM SINH_VIEN; ";
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
        private void buttonAll_Click(object sender, EventArgs e)
        {
            comboBoxHoTen.SelectedIndex = -1;
            comboBoxSinhVien.SelectedIndex = -1;
            if (radioButtonMSSV.Checked == true)
            {
                radioButtonMSSV.Checked = false;
                comboBoxSinhVien.Enabled=false;
            }
            else
            {
                radioButtonHoTen.Checked = false;
                comboBoxHoTen.Enabled=false;
            }
            LoadAllSV();
        }
    }
}
