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

namespace WinFormsAppKTX
{
    public partial class Main : Form
    {
        private string connectionString = "Data Source=LAPTOP-SI5JBDIU\\SQLEXPRESS01;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; // Thay bằng chuỗi kết nối của bạn
        public Main()
        {
            InitializeComponent();
        }

        private void btnExist_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login fn = new Login();
            fn.Show();
            this.Close();
        }

        private void QuanLiPhong_Click(object sender, EventArgs e)
        {
            QuanLiPhong quanLiPhong = new QuanLiPhong();
            quanLiPhong.TopLevel = false;
            quanLiPhong.FormBorderStyle = FormBorderStyle.None;
            quanLiPhong.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(quanLiPhong);
            quanLiPhong.Show();
        }

        private void DangKiNoiTru_Click(object sender, EventArgs e)
        {
            DangKiNoiTru dangKiNoiTru = new DangKiNoiTru();
            dangKiNoiTru.TopLevel = false;
            dangKiNoiTru.FormBorderStyle = FormBorderStyle.None;
            dangKiNoiTru.Dock = DockStyle.Fill;

            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(dangKiNoiTru);
            dangKiNoiTru.Show();
        }

        private void GiaHanNoiTru_Click(object sender, EventArgs e)
        {
            GiaHanNoiTru giaHanNoiTru = new GiaHanNoiTru();
            giaHanNoiTru.TopLevel = false;
            giaHanNoiTru.FormBorderStyle = FormBorderStyle.None;
            giaHanNoiTru.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();

            panelDisplay.Controls.Add(giaHanNoiTru);
            giaHanNoiTru.Show();

        }

        private void QuanLySinhVien_Click(object sender, EventArgs e)
        {
            QuanLiSinhVien quanLiSinhVien = new QuanLiSinhVien();
            quanLiSinhVien.TopLevel = false;
            quanLiSinhVien.FormBorderStyle = FormBorderStyle.None;
            quanLiSinhVien.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(quanLiSinhVien);
            quanLiSinhVien.Show();
        }

        private void CoSoVatChat_Click(object sender, EventArgs e)
        {
            CoSoVatChat coSoVatChat = new CoSoVatChat();
            coSoVatChat.TopLevel = false;
            coSoVatChat.FormBorderStyle = FormBorderStyle.None;
            coSoVatChat.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(coSoVatChat);
            coSoVatChat.Show();
        }

        private void QuanLiDienNuoc_Click(object sender, EventArgs e)
        {
            QuanLiDienNuoc quanLiDienNuoc = new QuanLiDienNuoc();
            quanLiDienNuoc.TopLevel = false;
            quanLiDienNuoc.FormBorderStyle = FormBorderStyle.None;
            quanLiDienNuoc.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(quanLiDienNuoc);
            quanLiDienNuoc.Show();
        }

        private void QuanLiViPham_Click(object sender, EventArgs e)
        {
            QuanLiViPham quanLiViPham = new QuanLiViPham();
            quanLiViPham.TopLevel = false;
            quanLiViPham.FormBorderStyle = FormBorderStyle.None;
            quanLiViPham.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(quanLiViPham);
            quanLiViPham.Show();
        }

        private void ThanhToan_Click(object sender, EventArgs e)
        {
            ThanhToan thanhToan = new ThanhToan();
            thanhToan.TopLevel = false;
            thanhToan.FormBorderStyle = FormBorderStyle.None;
            thanhToan.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(thanhToan);
            thanhToan.Show();
        }

        private void ThongKe_Click(object sender, EventArgs e)
        {
            ThongKe thongKe = new ThongKe();
            thongKe.TopLevel = false;
            thongKe.FormBorderStyle = FormBorderStyle.None;
            thongKe.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(thongKe);
            thongKe.Show();
        }

        private void btnMinisize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal; // Quay lại kích thước bình thường
            }
            else
            {
                this.WindowState = FormWindowState.Maximized; // Phóng to màn hình
            }
        }

        private void btnThumanhinh_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Thu nhỏ cửa sổ xuống Taskbar

        }

        private void Gioi_Thieu_Click(object sender, EventArgs e)
        {
            Gioi_Thieu gioithieu = new Gioi_Thieu();
            gioithieu.TopLevel = false;
            gioithieu.FormBorderStyle = FormBorderStyle.None;
            gioithieu.Dock = DockStyle.Fill;
            panelDisplay.Controls.Clear();
            panelDisplay.Controls.Add(gioithieu);
            gioithieu.Show();
        }
    }
}
