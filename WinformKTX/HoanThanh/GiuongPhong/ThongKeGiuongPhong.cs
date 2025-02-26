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

namespace abc
{


    public partial class ThongKeGiuongPhong : Form
    {
        public ThongKeGiuongPhong()
        {
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            InitializeComponent();
            SqlConnection conn = kn.GetConnection();
            taidanhsachPhong(conn);
            LoadData();
            LoadcomboBoxTinhTrang();
            comboBoxTang.Enabled = false;
            comboBoxPhong.Enabled = false;
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        private void LoadData()
        {
            DemALLPhong();
            DemPhongDay();
            DemPhongTrong();
            // Khởi tạo kết nối đến cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn dữ liệu
            string query = "SELECT DISTINCT LOAI_PHONG.TEN_LOAI_PHONG, TANG.TEN_TANG, PHONG.TEN_PHONG, PHONG.TINH_TRANG_PHONG " +
               " FROM GIUONG " +
               " JOIN PHONG ON GIUONG.MA_PHONG = PHONG.MA_PHONG " +
               " JOIN TANG ON PHONG.MA_TANG = TANG.MA_TANG " +
               " JOIN LOAI_PHONG ON TANG.MA_LOAI_PHONG = LOAI_PHONG.MA_LOAI_PHONG ";
            // Thực hiện truy vấn và lấy dữ liệu
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();

            // Đổ dữ liệu vào DataSet
            adapter.Fill(set, "WinFormKTX");

            // Gán nguồn dữ liệu cho DataGridView
            dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];

            // Thêm cột số thứ tự vào vị trí đầu tiên nếu chưa có


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
            comboBoxPhong.Enabled = true;
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

        //tai combobox TinhTrang
        private void LoadcomboBoxTinhTrang()
        {
            using (SqlConnection conn = kn.GetConnection())
            {
                conn.Open();
                string query = "SELECT DISTINCT PHONG.TINH_TRANG_PHONG FROM PHONG ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBoxTinhTrang.DataSource = dt;
                    comboBoxTinhTrang.DisplayMember = "TINH_TRANG_PHONG";
                    comboBoxTinhTrang.ValueMember = "TINH_TRANG_PHONG";
                    comboBoxTinhTrang.SelectedIndex = -1;
                }
            }
        }
        private void buttonTimkiemgiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxLoaiTang.SelectedValue == null && comboBoxTang.SelectedValue == null && comboBoxPhong.SelectedValue == null && comboBoxTinhTrang==null) 
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một tiêu chí để tìm kiếm.");
                    return;
                }

                // Mở kết nối cơ sở dữ liệu
                using (SqlConnection conn = kn.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT  LOAI_PHONG.TEN_LOAI_PHONG, TANG. TEN_TANG, PHONG.TEN_PHONG, PHONG. TINH_TRANG_PHONG FROM PHONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG " +
                                               "WHERE  1=1  And  ";
                    // Kiểm tra sự tồn tại của bảng GIUONG

                        if (comboBoxTinhTrang.SelectedValue != null) { 
                        query += " PHONG.TINH_TRANG_PHONG= N'" + comboBoxTinhTrang.SelectedValue.ToString() +"' And ";
                        }

                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GIUONG'", conn);
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

                                query += " PHONG.MA_LOAI_PHONG = @MaLoai";

                                SqlCommand command = new SqlCommand(query, conn);
                                command.Parameters.AddWithValue("@MaLoai", MaLoai);
                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataSet set = new DataSet();
                                adapter.Fill(set, "WinFormKTX");
                                dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                            }
                            else
                            {
                            //MessageBox.Show("Vui lòng chọn loại phòng.");
                            string TinhTrang = comboBoxTinhTrang.SelectedValue != null ? comboBoxTinhTrang.SelectedValue.ToString() : string.Empty;
                            if (TinhTrang == null)
                                {
                                    query += " ";
                                }
                                else
                                {                              
                                    query += "  PHONG.TINH_TRANG_PHONG= @TinhTrang";

                                        SqlCommand command = new SqlCommand(query, conn);
                                        command.Parameters.AddWithValue("@TinhTrang", TinhTrang);
                                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                                        DataSet set = new DataSet();
                                        adapter.Fill(set, "WinFormKTX");
                                        dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                                }

                            }
                        }
                        // Kiểm tra nếu chọn theo tầng
                        else
                        {
                            if (comboBoxPhong.SelectedValue == null)
                            {
                                string MaTang = comboBoxTang.SelectedValue.ToString();

                                query += " PHONG.MA_TANG = @MaTang";

                                SqlCommand command = new SqlCommand(query, conn);
                                command.Parameters.AddWithValue("@MaTang", MaTang);
                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataSet set = new DataSet();
                                adapter.Fill(set, "WinFormKTX");
                                dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                            }
                            else
                            {
                                // Kiểm tra nếu chọn theo phòng
                                string MaPhong = comboBoxPhong.SelectedValue.ToString();

                                query += " PHONG.MA_PHONG = @MaPhong";

                                SqlCommand command = new SqlCommand(query, conn);
                                command.Parameters.AddWithValue("@MaPhong", MaPhong);
                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataSet set = new DataSet();
                                adapter.Fill(set, "WinFormKTX");
                                dataGridViewThongTin.DataSource = set.Tables["WinFormKTX"];
                            }
                        }
                    //////kiem tra cau truy van
                    //MessageBox.Show(query);
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

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //dat lai cac gia tri cua cac field
            ClearFields();
            LoadData();
        }

        //ham set lai toan bo gia tri cua field
        private void ClearFields()
        {
            comboBoxTinhTrang.SelectedValue = -1;
            comboBoxLoaiTang.SelectedValue = -1;
            comboBoxTang.SelectedValue = -1;
            comboBoxTang.Enabled = false;
            comboBoxPhong.SelectedValue = -1;
            comboBoxPhong.Enabled = false;
        }





        //dem 
        private void DemALLPhong()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(PHONG.MA_PHONG) FROM PHONG  ";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemTongPhong.Text = "Tổng số phòng: " + Count.ToString();
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
        private void DemPhongTrong()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(PHONG.MA_PHONG) FROM PHONG WHERE PHONG.TINH_TRANG_PHONG=N'Trống'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemPhongTrong.Text = "Số phòng còn trống: " + Count.ToString();
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
        //dem 
        private void DemPhongDay()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            SqlConnection conn = kn.GetConnection();

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT COUNT(PHONG.MA_PHONG) FROM PHONG WHERE PHONG.TINH_TRANG_PHONG=N'Đầy'";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemPhongDay.Text = "Số Phòng Đầy  : " + Count.ToString();
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
