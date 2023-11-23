using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlychungcu
{
    public partial class Quanlydiennuoc : Form
    {
        Connect cn = new Connect();
        public Quanlydiennuoc()
        {
            InitializeComponent();
        }

        public void getDataDien()
        {
            string query = "select * from hoadondien";
            DataSet ds = cn.LayDuLieu(query);
            dgvDien.DataSource = ds.Tables[0];
        }

        public void getDataNuoc()
        {
            string query = "select * from hoadonnuoc";
            DataSet ds = cn.LayDuLieu(query);
            dgvNuoc.DataSource = ds.Tables[0];
        }

        public void clearTextDien()
        {
            txtMahoadonDien.Enabled = true;
            txtMahoadonDien.Clear();
            txtTenHoaDonDien.Clear();
            txtManvDien.Clear();
            cbMaHoGD.Text = "";
            cbMaCanHo.Text = "";
            pickerNgayInDien.Value=DateTime.Now;
            txtTongTienDien.Clear();
            txtGhiChuHDDien.Clear();
            numberSoLuongDien.Value = 0;
            txtDonGiaDien.Clear();
        }

        public void clearTextNuoc()
        {
            txtMaHDNuoc.Enabled = true;
            txtMaHDNuoc.Clear();
            txtTenHDNuoc.Clear();
            txtMaNhanVienNuoc.Clear();
            cbMaHoGDNuoc.Text = "";
            cbMaCanHoNuoc.Text = "";
            pickerNgayInNuoc.Value = DateTime.Now;
            txtTongTienNuoc.Clear();
            txtGhiChuNuoc.Clear();
            numberSoLuongNuoc.Value = 0;
            txtDonGiaNuoc.Clear();
        }


        private void Quanlydiennuoc_Load(object sender, EventArgs e)
        {
            getDataDien();
            getDataNuoc();

            List<string> maHGDList = cn.LayDanhSachHoGiaDinh();

            if (maHGDList != null)
            {
                foreach (string mahogd in maHGDList)
                {
                    cbMaHoGD.Items.Add(mahogd);
                    cbMaHoGDNuoc.Items.Add(mahogd);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu mã hộ gia đình từ cơ sở dữ liệu.");
            }

            List<string> maCanHoList = cn.LayDanhSachCanHo();

            if (maHGDList != null)
            {
                foreach (string macanho in maCanHoList)
                {
                    cbMaCanHo.Items.Add(macanho);
                    cbMaCanHoNuoc.Items.Add(macanho);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu Căn Hộ từ cơ sở dữ liệu.");
            }
        }

        private void btnThemHDDien_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO hoadondien " +
                                  "VALUES (@mahddien, @tenhoadon, @manhanvien, @mahogd, @macanho, @ngayin, @soluong,@dongia, @tongtien, @ghichu)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahddien", txtMahoadonDien.Text },
                            { "@tenhoadon", txtTenHoaDonDien.Text },
                            { "@manhanvien", txtManvDien.Text },
                            { "@mahogd", cbMaHoGD.SelectedItem.ToString() },
                            { "@macanho", cbMaCanHo.SelectedItem.ToString()},
                            { "@ngayin",pickerNgayInDien.Value.ToString("yyyy/MM/dd") },
                            { "@soluong", numberSoLuongDien.Value.ToString() },
                            { "@dongia", txtDonGiaDien.Text },
                            { "@tongtien",txtTongTienDien.Text  },
                            { "@ghichu", txtGhiChuHDDien.Text }
                        };

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Thêm mới hoá đơn điện thành công.");
                getDataDien();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới hoá đơn điện .");
            }
        }

        private void btnSuaHDDien_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahddien", txtMahoadonDien.Text },
                            { "@tenhoadon", txtTenHoaDonDien.Text },
                            { "@manhanvien", txtManvDien.Text },
                            { "@mahogd", cbMaHoGD.SelectedItem.ToString() },
                            { "@macanho", cbMaCanHo.SelectedItem.ToString()},
                            { "@ngayin",pickerNgayInDien.Value.ToString("yyyy/MM/dd") },
                            { "@soluong", numberSoLuongDien.Value.ToString() },
                            { "@dongia", txtDonGiaDien.Text },
                            { "@tongtien",txtTongTienDien.Text  },
                            { "@ghichu", txtGhiChuHDDien.Text }
                        };
            string query = "update hoadondien set tenhoadon=@tenhoadon, manhanvien=@manhanvien, mahogd=@mahogd, macanho=@macanho,ngayin= @ngayin, soluong=@soluong,dongia=@dongia, tongtien=@tongtien, ghichu=@ghichu where mahddien=@mahddien";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật hoá đơn " + txtMahoadonDien.Text + " thành công.");
                getDataDien();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật hoá đơn.");
            }
        }

        private void btnXoaHDDien_Click(object sender, EventArgs e)
        {
            string maHDDien = txtMahoadonDien.Text.Trim();

            if (!string.IsNullOrEmpty(maHDDien))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn điện này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = $"DELETE FROM hoadondien WHERE mahddien = '{maHDDien}'";

                    try
                    {
                        if (cn.ThucThi(query))
                        {
                            MessageBox.Show("Xóa thành công!");
                            btnHienThiHDDien.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa hóa đơn điện: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn điện cần xóa.");
            }

        }

        private void btnHienThiHDDien_Click(object sender, EventArgs e)
        {
            getDataDien();
        }

        private void btnTKHDDien_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from hoadondien where mahddien like'%{0}%' or tenhoadon like N'%{0}%' or mahogd like '%{0}%'",
                txtTimKiemHDDien.Text
                );
            DataSet ds = cn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvDien.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }
        }

        private void btnThemHDNuoc_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO hoadonnuoc " +
                                  "VALUES (@mahdnuoc, @tenhoadon, @manhanvien, @mahogd, @macanho, @ngayin, @soluong,@dongia, @tongtien, @ghichu)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahdnuoc", txtMaHDNuoc.Text },
                            { "@tenhoadon", txtTenHDNuoc.Text },
                            { "@manhanvien", txtMaNhanVienNuoc.Text },
                            { "@mahogd", cbMaHoGDNuoc.SelectedItem.ToString() },
                            { "@macanho", cbMaCanHoNuoc.SelectedItem.ToString()},
                            { "@ngayin",pickerNgayInNuoc.Value.ToString("yyyy/MM/dd") },
                            { "@soluong", numberSoLuongNuoc.Value.ToString() },
                            { "@dongia", txtDonGiaNuoc.Text },
                            { "@tongtien",txtTongTienNuoc.Text  },
                            { "@ghichu", txtGhiChuNuoc.Text }
                        };

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Thêm mới hoá đơn nước thành công.");
                getDataNuoc();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới hoá đơn nước .");
            }
        }

        private void btnSuaHDNuoc_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahdnuoc", txtMaHDNuoc.Text },
                            { "@tenhoadon", txtTenHDNuoc.Text },
                            { "@manhanvien", txtMaNhanVienNuoc.Text },
                            { "@mahogd", cbMaHoGDNuoc.SelectedItem.ToString() },
                            { "@macanho", cbMaCanHoNuoc.SelectedItem.ToString()},
                            { "@ngayin",pickerNgayInNuoc.Value.ToString("yyyy/MM/dd") },
                            { "@soluong", numberSoLuongNuoc.Value.ToString() },
                            { "@dongia", txtDonGiaNuoc.Text },
                            { "@tongtien",txtTongTienNuoc.Text  },
                            { "@ghichu", txtGhiChuNuoc.Text }
                        };
            string query = "update hoadonnuoc set tenhoadon @tenhoadon, manhanvien=@manhanvien, mahogd=@mahogd, macanho=@macanho,ngayin= @ngayin, soluong=@soluong,dongia=@dongia, tongtien=@tongtien, ghichu=@ghichu where mahdnuoc=@mahdnuoc";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật hoá đơn " + txtMaHDNuoc.Text + " thành công.");
                getDataNuoc();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật hoá đơn.");
            }
        }

        private void btnXoaHDNuoc_Click(object sender, EventArgs e)
        {
            string maHDNuoc = txtMaHDNuoc.Text.Trim();

            if (!string.IsNullOrEmpty(maHDNuoc))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn nước này?", "Xác nhận xóa",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = $"DELETE FROM hoadonnuoc WHERE mahdnuoc = '{maHDNuoc}'";

                    try
                    {
                        if (cn.ThucThi(query))
                        {
                            MessageBox.Show("Xóa thành công!");
                            btnHienThiHDNuoc.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa hóa đơn nước: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn nước cần xóa.");
            }

        }

        private void btnHienThiHDNuoc_Click(object sender, EventArgs e)
        {
            getDataNuoc();
        }

        private void btnTimKiemHDNuoc_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from hoadonnuoc where mahdnuoc like'%{0}%' or tenhoadon like N'%{0}%' or mahogd like '%{0}%'",
                txtTKHDNuoc.Text
                );
            DataSet ds = cn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvNuoc.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }
        }

        private void dgvDien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvDien.Rows.Count)
            {
                txtMahoadonDien.Text = dgvDien.Rows[r].Cells["mahddien"].Value.ToString();
                txtTenHoaDonDien.Text = dgvDien.Rows[r].Cells["tenhoadon"].Value.ToString();
                txtManvDien.Text = dgvDien.Rows[r].Cells["manhanvien"].Value.ToString();
                cbMaCanHo.Text = dgvDien.Rows[r].Cells["macanho"].Value.ToString();
                txtDonGiaDien.Text = dgvDien.Rows[r].Cells["dongia"].Value.ToString();
                txtTongTienDien.Text = dgvDien.Rows[r].Cells["tongtien"].Value.ToString();
                numberSoLuongDien.Value = Convert.ToDecimal(dgvDien.Rows[r].Cells["soluong"].Value);
                cbMaHoGD.Text = dgvDien.Rows[r].Cells["mahogd"].Value.ToString();
                string a = dgvDien.Rows[r].Cells["ngayin"].Value.ToString();
                if (a == "")
                {
                    return;
                }
                pickerNgayInDien.Value = DateTime.Parse(a);
                txtGhiChuHDDien.Text = dgvDien.Rows[r].Cells["ghichu"].Value.ToString();
            }
        }

        private void dgvNuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvNuoc.Rows.Count)
            {
                txtMaHDNuoc.Text = dgvNuoc.Rows[r].Cells["mahdnuoc"].Value.ToString();
                txtTenHDNuoc.Text = dgvNuoc.Rows[r].Cells["tenhoadon"].Value.ToString();
                txtMaNhanVienNuoc.Text = dgvNuoc.Rows[r].Cells["manhanvien"].Value.ToString();
                cbMaCanHoNuoc.Text = dgvNuoc.Rows[r].Cells["macanho"].Value.ToString();
                txtDonGiaNuoc.Text = dgvNuoc.Rows[r].Cells["dongia"].Value.ToString();
                txtTongTienNuoc.Text = dgvNuoc.Rows[r].Cells["tongtien"].Value.ToString();
                numberSoLuongNuoc.Value = Convert.ToDecimal(dgvNuoc.Rows[r].Cells["soluong"].Value);
                cbMaHoGDNuoc.Text = dgvNuoc.Rows[r].Cells["mahogd"].Value.ToString();
                string a = dgvNuoc.Rows[r].Cells["ngayin"].Value.ToString();
                if (a == "")
                {
                    return;
                }
                pickerNgayInNuoc.Value = DateTime.Parse(a);
                txtGhiChuNuoc.Text = dgvNuoc.Rows[r].Cells["ghichu"].Value.ToString();
            }
        }
    }
}
