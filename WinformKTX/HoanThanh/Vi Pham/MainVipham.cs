using abc.HoanThanh.ThongKeSinhVien;
using abc.HoanThanh.ThongKeViPham;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformKTX.Vi_Pham
{
    public partial class MainVipham : Form
    {
        public MainVipham()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMainvipham.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ViPham viPham = new ViPham();
            viPham.TopLevel = false;
            viPham.FormBorderStyle = FormBorderStyle.None;
            panelMainvipham.Controls.Add(viPham);
            viPham.Show();
        }

        private void MainVipham_Load(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMainvipham.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ViPham viPham = new ViPham();
            viPham.TopLevel = false;
            viPham.FormBorderStyle = FormBorderStyle.None;
            panelMainvipham.Controls.Add(viPham);
            viPham.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMainvipham.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            xulyvipham xulyvipham = new xulyvipham();
            xulyvipham.TopLevel = false;
            xulyvipham.FormBorderStyle = FormBorderStyle.None;
            panelMainvipham.Controls.Add(xulyvipham);
            xulyvipham.Show();
        }
    }
}
