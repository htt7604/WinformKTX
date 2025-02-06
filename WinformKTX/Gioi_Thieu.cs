using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformKTX
{
    public partial class Gioi_Thieu : Form
    {
        public Gioi_Thieu()
        {
            InitializeComponent();
            pictureBox1.Paint += lammo;
        }



        private void lammo(object sender, PaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(100, Color.Black)))
            {
                e.Graphics.FillRectangle(brush, pictureBox1.ClientRectangle);
            }
            


            string text = "Chào mừng bạn đến ký túc xá NAM CẦN THƠ!" +
                "\r\n " +
                "\r\nKý túc xá Đại học Nam Cần Thơ (KTX ĐH NCT) là nơi lý" +
                "\r\ntưởng cho sinh viên,đặc biệt là những sinh viên xa nhà." +
                "\r\nVới cơ sở vật chất hiện đại và tiện nghi, KTX cung cấp " +
                "\r\nphòng ở đầy đủ trang thiết bị như giường, bàn học, tủ đồ," +
                "\r\nđiều hòa và Internet tốc độ cao. Ngoài ra, sinh viên có" +
                "\r\nthể tham gia các hoạt động thể thao,giải trí tại khu vực " +
                "\r\nsinh hoạt chung." +
                "\r\n" +
                "\r\nKý túc xá tạo môi trường an toàn, gắn kết, với sự hỗ trợ" +
                "\r\n24/7 từ đội ngũ quản lý. Đây là nơi lý tưởng giúp sinh viên" +
                "\r\nphát triển học tập, kỹ năng sống và tham gia cộng đồng sinh" +
                "\r\nviên năng động.";
            using (Font font = new Font("Arial", 24, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                // Tính toán vị trí để căn giữa văn bản
                SizeF textSize = e.Graphics.MeasureString(text, font);
                float x = (pictureBox1.Width - textSize.Width) / 2;
                float y = (pictureBox1.Height - textSize.Height) / 2;

                e.Graphics.DrawString(text, font, textBrush, x, y);
            }
        }

        
    }
}
