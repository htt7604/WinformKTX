using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformKTX.HoanThanh.Diennuoc
{
    public partial class thongkediennuoc : Form
    {
        public thongkediennuoc()
        {
            var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            InitializeComponent();
            LoadDienNuocData();
            TaidanhsachPhong(conn);
            DemSoDien();
            DemSoNuoc();
        }
        KetnoiCSDL kn = new KetnoiCSDL();


        //ham du lieu vao data
        private void LoadDienNuocData()
        {
            //DemSVDaThanhToan();
            //DemSVChuaThanhToan();
            //string connectionString = "Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "select LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,DIEN_NUOC.CHI_SO_DIEN_CU,CHI_SO_DIEN_MOI,DIEN_NUOC.CHI_SO_NUOC_CU,DIEN_NUOC.CHI_SO_NUOC_MOI,TU_NGAY,DEN_NGAY,SO_DIEN_DA_SU_DUNG,SO_NUOC_DA_SU_DUNG,DIEN_NUOC.TIEN_DIEN,TIEN_NUOC,TONG_TIEN,NGAY_THANH_TOAN_DIEN_NUOC,TINH_TRANG_TT  from DIEN_NUOC join PHONG on DIEN_NUOC.MA_PHONG=PHONG.MA_PHONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG ";
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

        private void thongkediennuoc_Load(object sender, EventArgs e)
        {
            //ham load du lieu 
            //XoaThongtin();
            LoadDienNuocData();
            groupBoxThoiGian.Enabled = false;
            radioButtonThoiGianThanhToan.Checked = false;
            radioButtonThoiGianSuDung.Checked = false;
            radioButtonTatThoiGian.Checked = true;
            radioButtonAll.Checked = true;
            comboBoxTang.Enabled = false;
            comboBoxPhong.Enabled = false;

            textBoxDien.Text = "";
            textBoxNuoc.Text = "";
        }

        //ham luu thong tin ng dung nhap

        private (string sodien, string sonuoc, string phong, string tang, string loaiTang, DateTime? ngayStart, DateTime? ngayEnd) GetUserInputs()
        {
            // Lấy giá trị từ các điều khiển giao diện người dùng
            bool thoigianthanhtoan = radioButtonThoiGianThanhToan.Checked;
            bool thoigiansudung = radioButtonThoiGianSuDung.Checked;
            bool tatthoigian = radioButtonTatThoiGian.Checked;
            if (tatthoigian == false)
            {
                if (radioButtonThoiGianSuDung.Checked == true)
                {

                    thoigiansudung = true;
                }
                else
                {

                    thoigianthanhtoan = false;
                }
            }
            string sodien = textBoxDien.Text != null ? textBoxDien.Text.ToString() : null;
            string sonuoc = textBoxNuoc.Text != null ? textBoxNuoc.Text.ToString() : null;
            string phong = comboBoxPhong.SelectedValue != null ? comboBoxPhong.SelectedValue.ToString() : null; // Phòng
            string tang = comboBoxTang.SelectedValue != null ? comboBoxTang.SelectedValue.ToString() : null; // Tầng
            string loaiTang = comboBoxLoaiTang.SelectedValue != null ? comboBoxLoaiTang.SelectedValue.ToString() : null; // Loại tầng
            //DateTime? ngayStart = dateTimePickerBatDau.Checked ? dateTimePickerBatDau.Value : (DateTime?)null; // Ngày bắt đầu
            //DateTime? ngayEnd = dateTimePickerKetThuc.Checked ? dateTimePickerKetThuc.Value : (DateTime?)null; // Ngày kết thúc
            DateTime ngayStart = dateTimePickerBatDau.Value;
            DateTime ngayEnd = dateTimePickerKetThuc.Value;

            return (sodien, sonuoc, phong, tang, loaiTang, ngayStart, ngayEnd);
        }

        //cau sql
        private string CreateSqlQuery(string sodien, string sonuoc, string phong, string tang, string loaiTang, DateTime? ngayStart, DateTime? ngayEnd)
        {
            string query = "select LOAI_PHONG.TEN_LOAI_PHONG,TANG.TEN_TANG,PHONG.TEN_PHONG,DIEN_NUOC.CHI_SO_DIEN_CU,CHI_SO_DIEN_MOI,DIEN_NUOC.CHI_SO_NUOC_CU,DIEN_NUOC.CHI_SO_NUOC_MOI,TU_NGAY,DEN_NGAY,SO_DIEN_DA_SU_DUNG,SO_NUOC_DA_SU_DUNG,DIEN_NUOC.TIEN_DIEN,TIEN_NUOC,TONG_TIEN,NGAY_THANH_TOAN_DIEN_NUOC,TINH_TRANG_TT  from DIEN_NUOC join PHONG on DIEN_NUOC.MA_PHONG=PHONG.MA_PHONG join TANG on PHONG.MA_TANG=TANG.MA_TANG join LOAI_PHONG on TANG.MA_LOAI_PHONG=LOAI_PHONG.MA_LOAI_PHONG " +
                           "WHERE 1=1 ";  
            // Thêm điều kiện cơ bản (dễ dàng thêm các điều kiện khác sau này)
            //co khi co du lieu 
            bool hasCondition = false;
            if (radioButtonAll.Checked)
            {
                query += "AND ( DIEN_NUOC.TINH_TRANG_TT=N'Đã thanh toán' or DIEN_NUOC.TINH_TRANG_TT=N'Chưa thanh toán') ";
                hasCondition = false;
            }
            else if (radioButtonDaTT.Checked)
            {
                query += "AND DIEN_NUOC.TINH_TRANG_TT=N'Đã thanh toán' ";
                hasCondition = false;
            }
            else if (radioButtonChuaTT.Checked)
            {
                query += "AND DIEN_NUOC.TINH_TRANG_TT=N'Chưa thanh toán' ";
                hasCondition = false;
            }

            // Kiểm tra ComboBox phòng
            if (comboBoxPhong.SelectedIndex != -1)
            {
                query += " AND PHONG.MA_PHONG = @Phong ";
                hasCondition = true;
            }

            // Kiểm tra ComboBox tầng
            if (comboBoxTang.SelectedIndex != -1)
            {
                query += " AND TANG.MA_TANG = @Tang ";
                hasCondition = true;
            }

            // Kiểm tra ComboBox loại tầng
            if (comboBoxLoaiTang.SelectedIndex != -1)
            {
                query += " AND LOAI_PHONG.MA_LOAI_PHONG = @LoaiTang ";
                hasCondition = true;
            }

            //la so dien > hon
            if (textBoxDien.Text != null)
            {
                query += "AND ((CHI_SO_DIEN_MOI-CHI_SO_DIEN_CU)>= @SoDien ) ";
                hasCondition = true;
            }
            else if (textBoxDien.Text == null)
            {
                query += "AND ((CHI_SO_DIEN_MOI-CHI_SO_DIEN_CU)>= 0 )  ";
            }
            //lay so nuoc lon hon 
            if (textBoxNuoc.Text != null)
            {
                query += "AND ((CHI_SO_NUOC_MOI-CHI_SO_NUOC_CU)>= @SoNuoc ) ";
                hasCondition = true;
            }
            else
            if (textBoxNuoc.Text == null)
            {
                query += "AND ((CHI_SO_NUOC_MOI-CHI_SO_NUOC_CU)>= 0 )  ";
            }




            // Kiểm tra điều kiện ngày tháng neu ngay tu > ngay den 1h 
            //la thoi gian su dung 
            if (radioButtonTatThoiGian.Checked == false)
            {
                if (radioButtonThoiGianSuDung.Checked == true)
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
                            query += " AND ((DIEN_NUOC.TU_NGAY BETWEEN @NgayStart AND @NgayEnd ) or  (DIEN_NUOC.DEN_NGAY BETWEEN @NgayStart AND @NgayEnd ))  ";
                        }
                        else
                        {
                            // Nếu có ngày bắt đầu và ngày kết thúc, thêm điều kiện ngày
                            if (ngayStart.Value < ngayEnd.Value.AddHours(1))
                            {
                                query += " AND DIEN_NUOC.TU_NGAY = @NgayStart And DIEN_NUOC.DEN_NGAY = @NgayEnd ";
                            }
                        }
                    }
                }
                //la thoi gian thanh toan 
                else if (radioButtonThoiGianThanhToan.Checked == true)
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
                            query += " AND DIEN_NUOC.NGAY_THANH_TOAN_DIEN_NUOC ='@NgayStart' ";
                        }
                        else
                        {
                            // Nếu có ngày bắt đầu và ngày kết thúc, thêm điều kiện ngày
                            if (ngayStart.Value < ngayEnd.Value.AddHours(1))
                            {
                                query += " AND DIEN_NUOC.NGAY_THANH_TOAN_DIEN_NUOC BETWEEN @NgayStart AND @NgayEnd";
                            }
                        }
                    }
                }

            }




            // Nếu không có điều kiện tìm kiếm nào, thông báo lỗi
            if (!hasCondition)
            {
                query += "";
                //MessageBox.Show("Vui lòng chọn ít nhất một điều kiện tìm kiếm.");
            }

            MessageBox.Show(query);
            return query;
        }

        private void AddParametersToSqlCommand(SqlCommand cmd, string sodien, string sonuoc, string phong, string tang, string loaiTang, DateTime? ngayStart, DateTime? ngayEnd)
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
            //neu co so dien
            if (!string.IsNullOrEmpty(sodien))
            {
                cmd.Parameters.AddWithValue("@SoDien", sodien);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SoDien", "0");
            }
            //neu co so nuoc 
            if (!string.IsNullOrEmpty(sonuoc))
            {
                cmd.Parameters.AddWithValue("@SoNuoc", sonuoc);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SoNuoc", "0");
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


        private void GetThongTinDienNuoc()
        {
            // Lấy các tham số tìm kiếm từ giao diện người dùng
            var (sonuoc, sodien, phong, tang, loaiTang, ngayStart, ngayEnd) = GetUserInputs();

            // Xây dựng câu lệnh SQL với các điều kiện động
            string query = CreateSqlQuery(sonuoc, sodien, phong, tang, loaiTang, ngayStart, ngayEnd);

            using (SqlConnection conn = kn.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Khởi tạo đối tượng SqlCommand với câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        AddParametersToSqlCommand(cmd, sonuoc, sodien, phong, tang, loaiTang, ngayStart, ngayEnd);
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
            GetThongTinDienNuoc();
        }

        private void radioButtonTatThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxThoiGian.Enabled = false;
        }

        private void radioButtonThoiGianSuDung_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxThoiGian.Enabled = true;

        }

        private void radioButtonThoiGianThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxThoiGian.Enabled = true;
        }

        private void buttonTroLai_Click(object sender, EventArgs e)
        {
            radioButtonTatThoiGian.Checked = true;
            radioButtonAll.Checked = true;
            comboBoxLoaiTang.SelectedIndex = -1;
            comboBoxTang.SelectedIndex = -1;
            comboBoxPhong.SelectedIndex = -1;          
            LoadDienNuocData();
            thongkediennuoc_Load(sender, e);
        }

        //ham tinh tong so dien da su dung 
        private void DemSoDien()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");

            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT SUM(DIEN_NUOC.SO_DIEN_DA_SU_DUNG )FROM DIEN_NUOC";
            SqlConnection conn = kn.GetConnection();
            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSoDien.Text = "Tổng số điện đã sử dụng: " + Count.ToString() +" KW";
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
        private void DemSoNuoc()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            //var conn = new SqlConnection("Data Source=Win_byTai;Initial Catalog=WinFormKTX;Integrated Security=True;Trust Server Certificate=True");
            SqlConnection conn = kn.GetConnection();
            // Truy vấn SQL để đếm số sinh viên
            string query = "SELECT SUM(DIEN_NUOC.SO_NUOC_DA_SU_DUNG )FROM DIEN_NUOC";

            // Tạo lệnh SQL
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                // Mở kết nối
                conn.Open();

                // Thực hiện truy vấn và lấy kết quả đếm
                int Count = (int)command.ExecuteScalar();

                // Hiển thị số sinh viên lên form (ví dụ, gán vào một Label)
                textBoxDemSoNuoc.Text = "Tổng số nước đã sử dụng: " + Count.ToString() + " M3";
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
