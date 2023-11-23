using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace quanlychungcu
{
    class Connect
    {
        private static string connStr = @"Data Source=DESKTOP-DNGF0KP;Initial Catalog=smallTower_db;Persist Security Info=True;User ID=sa;pwd=123";

        public Connect()
        {
            // Không cần khai báo conn ở đây
        }

        public DataSet LayDuLieu(string truyvan, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();

                    using (SqlDataAdapter da = new SqlDataAdapter(truyvan, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                da.SelectCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        da.Fill(ds);
                    }

                    return ds;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool ThucThi(string truyvan, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(truyvan, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<string> LayDanhSachQuyen()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> quyenList = new List<string>();

                    string query = "SELECT tenquyen FROM quyen";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tenquyen = reader.GetString(0);
                                quyenList.Add(tenquyen);
                            }
                        }
                    }

                    return quyenList;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<string> LayDanhSachPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> phongbanList = new List<string>();

                    string query = "SELECT maphongban FROM phongban";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string maphongban = reader.GetString(0);
                                phongbanList.Add(maphongban);
                            }
                        }
                    }

                    return phongbanList;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<string> LayDanhSachKhu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> khuList = new List<string>();

                    string query = "SELECT makhu FROM khu";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string makhu = reader.GetString(0);
                                khuList.Add(makhu);
                            }
                        }
                    }

                    return khuList;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<string> LayDanhSachHoGiaDinh()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> hogiadinhList = new List<string>();

                    string query = "SELECT mahogd FROM hogiadinh";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string mahogd = reader.GetString(0);
                                hogiadinhList.Add(mahogd);
                            }
                        }
                    }

                    return hogiadinhList;
                }
                catch
                {
                    return null;
                }
            }
        }
        public List<string> LayDanhSachCanHo()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> canhoList = new List<string>();

                    string query = "SELECT macanho FROM canho";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string maphongban = reader.GetString(0);
                                canhoList.Add(maphongban);
                            }
                        }
                    }

                    return canhoList;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<string> LayDanhSachDichVu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> dichvuList = new List<string>();

                    string query = "SELECT madichvu FROM dichvu";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string madichvu = reader.GetString(0);
                                dichvuList.Add(madichvu);
                            }
                        }
                    }

                    return dichvuList;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<string> LayDanhSachIDQuyen()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    List<string> IDList = new List<string>();

                    string query = "SELECT idquyen FROM quyen";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string idquyen = reader.GetString(0);
                                IDList.Add(idquyen);
                            }
                        }
                    }

                    return IDList;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
