using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace quanlychungcu
{
    public partial class Quanlyhopdong : Form
    {
        Connect cn = new Connect();
        public Quanlyhopdong()
        {
            InitializeComponent();
        }

        public void getData()
        {
            string query = "select * from hopdonggd";
            DataSet ds = cn.LayDuLieu(query);
            dgvHopDong.DataSource = ds.Tables[0];
        }

        private void Quanlyhopdong_Load(object sender, EventArgs e)
        {
            getData();
            List<string> maCanHoList = cn.LayDanhSachCanHo();

            if (maCanHoList != null)
            {
                foreach (string macanho in maCanHoList)
                {
                    cbMaCanHo.Items.Add(macanho);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu Căn Hộ từ cơ sở dữ liệu.");
            }
        }

        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvHopDong.Rows.Count)
            {
                txtMaHopDong.Text = dgvHopDong.Rows[r].Cells["mahopdong"].Value.ToString();
                txtTenHopDong.Text = dgvHopDong.Rows[r].Cells["tenhopdong"].Value.ToString();
                txtLoaiHopDong.Text = dgvHopDong.Rows[r].Cells["loaihopdong"].Value.ToString();
                cbMaCanHo.Text = dgvHopDong.Rows[r].Cells["macanho"].Value.ToString();
                txtMaNhanVien.Text = dgvHopDong.Rows[r].Cells["manhanvien"].Value.ToString();
            }
        }

        private void btnThemHopDong_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO hopdonggd (mahopdong, tenhopdong, loaihopdong, macanho, manhanvien) " +
               "VALUES (@mahopdong, @tenhopdong, @loaihopdong, @macanho, @manhanvien)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@mahopdong", txtMaHopDong.Text.Trim() },
                    { "@tenhopdong", txtTenHopDong.Text.Trim() },
                    { "@loaihopdong", txtLoaiHopDong.Text.Trim() },
                    { "@macanho", cbMaCanHo.SelectedItem?.ToString()?.Trim() },
                    { "@manhanvien", txtMaNhanVien.Text.Trim() }
                };

            try
            {
                if (cn.ThucThi(query, parameters))
                {
                    MessageBox.Show("Thêm mới hợp đồng thành công.");
                    getData();
                }
                else
                {
                    MessageBox.Show("Lỗi thêm mới hợp đồng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện truy vấn: " + ex.Message);
            }

        }

        private void btnSuaHopDong_Click(object sender, EventArgs e)
        {
            string query = "UPDATE hopdonggd SET tenhopdong = @tenhopdong, loaihopdong = @loaihopdong, macanho = @macanho, manhanvien = @manhanvien WHERE mahopdong = @mahopdong";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@mahopdong", txtMaHopDong.Text.Trim() },
                    { "@tenhopdong", txtTenHopDong.Text.Trim() },
                    { "@loaihopdong", txtLoaiHopDong.Text.Trim() },
                    { "@macanho", cbMaCanHo.SelectedItem?.ToString()?.Trim() },
                    { "@manhanvien", txtMaNhanVien.Text.Trim() }
                };

            try
            {
                if (cn.ThucThi(query, parameters))
                {
                    MessageBox.Show($"Cập nhật hợp đồng {txtMaHopDong.Text.Trim()} thành công.");
                    getData();
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật hợp đồng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện truy vấn: " + ex.Message);
            }

        }

        private void btnXoaHopDong_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete from hopdonggd where mahopdong='{0}' ",
               txtMaHopDong.Text
               );
            if (cn.ThucThi(query) == true)
            {
                MessageBox.Show("Xoa Thanh Cong !!");
                btnHienThiHopDong.PerformClick();
            }
            else { MessageBox.Show("Xoa khong Thanh Cong !!"); }
        }

        private void btnHienThiHopDong_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void btnTKHopDong_Click(object sender, EventArgs e)
        {
            string timKiemValue = txtTimKiemHD.Text.Trim();
            string query = $"SELECT * FROM hopdonggd WHERE mahopdong LIKE '%{timKiemValue}%' OR macanho LIKE '%{timKiemValue}%'";

            DataSet ds = cn.LayDuLieu(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvHopDong.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string dataToPass = txtMaHopDong.Text;
            ChiTietHopDong cthd = new ChiTietHopDong(dataToPass);
            cthd.Show();
        }

        
    }
}
