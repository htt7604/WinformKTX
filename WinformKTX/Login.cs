using Microsoft.Data.SqlClient;
using WinformKTX;

namespace WinFormsAppKTX
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    // Kết nối đến cơ sở dữ liệu
        //    string connectionString = "Data Source=TRONG\\SQLEXPRESS03;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; // Cập nhật chuỗi kết nối
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            // Mở kết nối đến cơ sở dữ liệu
        //            conn.Open();

        //            // Tạo câu lệnh SQL để kiểm tra tên đăng nhập và mật khẩu
        //            string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TEN_TK = @username AND MAT_KHAU = @password";
        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                // Thêm tham số vào câu lệnh SQL để tránh SQL Injection
        //                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
        //                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

        //                // Thực thi câu lệnh SQL và lấy kết quả
        //                int result = (int)cmd.ExecuteScalar(); // Nếu có ít nhất một tài khoản khớp thì result sẽ > 0

        //                // Kiểm tra nếu đăng nhập thành công
        //                if (result > 0)
        //                {
        //                    Main dbs = new Main(); // Hiển thị form chính
        //                    dbs.Show();
        //                    this.Hide(); // Ẩn form đăng nhập
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi kết nối: " + ex.Message);
        //        }
        //    }
        //}


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo đối tượng KetnoiCSDL để sử dụng phương thức GetConnection
                KetnoiCSDL ketnoi = new KetnoiCSDL();

                // Sử dụng phương thức GetConnection() để lấy kết nối
                using (SqlConnection conn = ketnoi.GetConnection())
                {
                    // Mở kết nối đến cơ sở dữ liệu
                    conn.Open();

                    // Tạo câu lệnh SQL để kiểm tra tên đăng nhập và mật khẩu
                    string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TEN_TK = @username AND MAT_KHAU = @password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số vào câu lệnh SQL để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        // Thực thi câu lệnh SQL và lấy kết quả
                        int result = (int)cmd.ExecuteScalar(); // Nếu có ít nhất một tài khoản khớp thì result sẽ > 0

                        // Kiểm tra nếu đăng nhập thành công
                        if (result > 0)
                        {
                            Main dbs = new Main(); // Hiển thị form chính
                            dbs.Show();
                            this.Hide(); // Ẩn form đăng nhập
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }



        private void LogoutDN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
