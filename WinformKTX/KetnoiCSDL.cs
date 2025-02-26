using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinformKTX
{
    internal class KetnoiCSDL
    {
        public SqlConnection GetConnection() //
        {
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Data Source=TRONG\\SQLEXPRESS03;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; 
            //con.ConnectionString = "Data Source=LAPTOP-5VTLAM86\\SQLEXPRESS;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            con.ConnectionString = "Data Source = Win_byTai; Initial Catalog = WinFormKTX; Integrated Security = True; Trust Server Certificate = True";

            //con.ConnectionString = "Data Source=TRONG\\SQLEXPRESS03;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            //con.ConnectionString = "Data Source=TRONG\\SQLEXPRESS03;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            //con.ConnectionString = "Data Source=LAPTOP-SI5JBDIU\\SQLEXPRESS01;Initial Catalog=WinFormKTX;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            return con;
        }

        internal SqlDataReader GetDataReader(string query) //
        {
            throw new NotImplementedException();
        }

        public SqlDataReader getForCombox(string query) //
        {
            // Kết nối SQL và thực thi query
            SqlConnection conn = new SqlConnection("Chuỗi_kết_nối");
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            return cmd.ExecuteReader(); // Trả về SqlDataReader
        }
    }
}
