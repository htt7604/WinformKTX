using abc.HoanThanh.ThanhToan;
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
using WinformKTX.HoanThanh.Diennuoc;
using WinformKTX.HoanThanh.ThongKeCSVC_HuHong;

namespace abc.HoanThanh
{
    public partial class MainThongKe : Form
    {
        public MainThongKe()
        {
            InitializeComponent();
        }


        private void panellMain_Paint(object sender, PaintEventArgs e)
        {

        }


        private void buttonFormGiuongPhong_Click(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void buttonTKThanhToan_Click(object sender, EventArgs e)
        {

        }

        private void hoSoSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ThongtinSV thongtinSV = new ThongtinSV();
            thongtinSV.TopLevel = false;
            thongtinSV.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongtinSV);
            thongtinSV.Show();
        }

        private void sinhVienNoiTruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ThongTinNoiTru thongTinNoiTru = new ThongTinNoiTru();
            thongTinNoiTru.TopLevel = false;
            thongTinNoiTru.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongTinNoiTru);
            thongTinNoiTru.Show();
        }


        private void thanhToanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ThongKeThanhToan thongKeThanhToan = new ThongKeThanhToan();
            thongKeThanhToan.TopLevel = false;
            thongKeThanhToan.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongKeThanhToan);
            thongKeThanhToan.Show();
        }

        private void huHongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ThongKeVatChat thongKeVatChat = new ThongKeVatChat();
            thongKeVatChat.TopLevel = false;
            thongKeVatChat.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongKeVatChat);
            thongKeVatChat.Show();
        }

        private void phongGiuongToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            ThongKeGiuongPhong thongKeGiuongPhong = new ThongKeGiuongPhong();
            thongKeGiuongPhong.Dock = DockStyle.Fill;
            thongKeGiuongPhong.TopLevel = false;
            thongKeGiuongPhong.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongKeGiuongPhong);
            thongKeGiuongPhong.Show();
        }

        private void vIPhamSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            thongkevipham thongKeViPham = new thongkevipham();
            thongKeViPham.Dock = DockStyle.Fill;
            thongKeViPham.TopLevel = false;
            thongKeViPham.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongKeViPham);
            thongKeViPham.Show();
        }

        private void dienNuocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra và đóng tất cả các form đang mở trong panelMain
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form form)
                {
                    // Đóng form đang mở
                    form.Close();  // Hoặc sử dụng form.Hide() nếu bạn chỉ muốn ẩn mà không giải phóng tài nguyên
                }
            }
            thongkediennuoc thongKeDienNuoc = new thongkediennuoc();
            thongKeDienNuoc.Dock = DockStyle.Fill;
            thongKeDienNuoc.TopLevel = false;
            thongKeDienNuoc.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(thongKeDienNuoc);
            thongKeDienNuoc.Show();
        }
    }
}
