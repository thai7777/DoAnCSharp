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
    public partial class QuanLyDichVu : Form
    {
        Connect cn = new Connect();
        public QuanLyDichVu()
        {
            InitializeComponent();
        }
        public void getDataHDDV()
        {
            string query = "select * from hoadondv";
            DataSet ds = cn.LayDuLieu(query);
            dgvTTHoaDonDV.DataSource = ds.Tables[0];
        }

        public void getDataDV()
        {
            string query = "select * from dichvu";
            DataSet ds = cn.LayDuLieu(query);
            dgvDichVu.DataSource = ds.Tables[0];
        }

        public void clearTextTTHDDV()
        {
            txtMaHDDichVu.Enabled = true;
            txtTenHDDV.Clear();
            txtMaNhanVien.Clear() ;
            txtDonGiaHDDichVu.Clear();
            cbMaCanHo.Text = "";
            txtTongTien.Clear();
            txtMaHDDichVu.Clear();
            numberSoLuong.Value = 0;
            cbMaHoGD.Text = "";
            cbMaDichVu.Text = "";
            pickerNgayIn.Value = DateTime.Now;
            txtGhiChu.Clear();
        }

        public void clearTextDV()
        {
            txtMaDichVu.Enabled = true;
            txtMaDichVu.Clear();
            txtTenDichVu.Clear();
            txtGiaDichVu.Clear();
        }

        private void QuanLyDichVu_Load(object sender, EventArgs e)
        {
            getDataHDDV();
            getDataDV();
            List<string> maHGDList = cn.LayDanhSachHoGiaDinh();

            if (maHGDList != null)
            {
                foreach (string mahogd in maHGDList)
                {
                    cbMaHoGD.Items.Add(mahogd);
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
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu Căn Hộ từ cơ sở dữ liệu.");
            }

            List<string> dichvuList = cn.LayDanhSachDichVu();

            if (dichvuList != null)
            {
                foreach (string madichvu in dichvuList)
                {
                    cbMaDichVu.Items.Add(madichvu);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu Căn Hộ từ cơ sở dữ liệu.");
            }
        }

        private void btnThemHDDV_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO hoadondv " +
                                   "VALUES (@mahoadondv, @tenhoadon, @manhanvien, @mahogd, @macanho, @ngayin, @soluong,@dongia, @tongtien, @ghichu,@madichvu)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahoadondv", txtMaHDDichVu.Text },
                            { "@tenhoadon", txtTenHDDV.Text },
                            { "@manhanvien", txtMaNhanVien.Text },
                            { "@macanho", cbMaCanHo.SelectedItem.ToString() },
                            { "@dongia", txtDonGiaHDDichVu.Text },
                            { "@tongtien", txtTongTien.Text },
                            { "@soluong", numberSoLuong.Value.ToString() },
                            { "@mahogd", cbMaHoGD.Text },
                            { "@madichvu", cbMaDichVu.SelectedItem.ToString() },
                            { "@ngayin", pickerNgayIn.Value.ToString("yyyy/MM/dd") },
                            { "@ghichu", txtGhiChu.Text }
                        };

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Thêm mới hoá đơn thành công.");
                getDataHDDV();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới hoá đơn.");
            }
        }

        private void dgvTTHoaDonDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvTTHoaDonDV.Rows.Count)
            {
                txtMaHDDichVu.Text = dgvTTHoaDonDV.Rows[r].Cells["mahoadondv"].Value.ToString();
                txtTenHDDV.Text = dgvTTHoaDonDV.Rows[r].Cells["tenhoadon"].Value.ToString();
                txtMaNhanVien.Text = dgvTTHoaDonDV.Rows[r].Cells["manhanvien"].Value.ToString();
                cbMaCanHo.Text = dgvTTHoaDonDV.Rows[r].Cells["macanho"].Value.ToString();
                txtDonGiaHDDichVu.Text = dgvTTHoaDonDV.Rows[r].Cells["dongia"].Value.ToString();
                txtTongTien.Text = dgvTTHoaDonDV.Rows[r].Cells["tongtien"].Value.ToString();
                numberSoLuong.Value = Convert.ToDecimal(dgvTTHoaDonDV.Rows[r].Cells["soluong"].Value);
                cbMaHoGD.Text = dgvTTHoaDonDV.Rows[r].Cells["mahogd"].Value.ToString();
                cbMaDichVu.Text = dgvTTHoaDonDV.Rows[r].Cells["madichvu"].Value.ToString();
                string a = dgvTTHoaDonDV.Rows[r].Cells["ngayin"].Value.ToString();
                if (a == "")
                {
                    return;
                }
                pickerNgayIn.Value = DateTime.Parse(a);
                txtGhiChu.Text = dgvTTHoaDonDV.Rows[r].Cells["ghichu"].Value.ToString();
            }
        }

        private void btnSuaHDDV_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahoadondv", txtMaHDDichVu.Text },
                            { "@tenhoadon", txtTenHDDV.Text },
                            { "@manhanvien", txtMaNhanVien.Text },
                            { "@macanho", cbMaCanHo.SelectedItem.ToString() },
                            { "@dongia", txtDonGiaHDDichVu.Text },
                            { "@tongtien", txtTongTien.Text },
                            { "@soluong", numberSoLuong.Value.ToString() },
                            { "@mahogd", cbMaHoGD.Text },
                            { "@madichvu", cbMaDichVu.SelectedItem.ToString() },
                            { "@ngayin", pickerNgayIn.Value.ToString("yyyy/MM/dd") },
                            { "@ghichu", txtGhiChu.Text }
                        };

            string query = "update hoadondv set  tenhoadon=@tenhoadon, manhanvien=@manhanvien, mahogd=@mahogd, macanho=@macanho,ngayin= @ngayin, soluong=@soluong,dongia=@dongia, tongtien=@tongtien, ghichu=@ghichu,madichvu=@madichvu where mahoadondv=@mahoadondv";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật hoá đơn "+txtMaHDDichVu.Text +" thành công.");
                getDataHDDV();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật hoá đơn.");
            }
        }

        private void btnXoaHDDV_Click(object sender, EventArgs e)
        {
            string query = $"DELETE FROM hoadondv WHERE mahoadondv = '{txtMaHDDichVu.Text}'";

            if (cn.ThucThi(query))
            {
                MessageBox.Show("Xóa thành công!");
                btnHienThiHDDV.PerformClick();
            }
            else
            {
                MessageBox.Show("Xóa không thành công!");
            }
        }

        private void btnTKHDDV_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemHDDV.Text.Trim();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                string query = "SELECT * FROM hoadondv WHERE mahoadondv LIKE @tuKhoa OR tenhoadon LIKE N@tuKhoa OR mahogd LIKE @tuKhoa";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@tuKhoa", $"%{tuKhoa}%" }
                    };

                try
                {
                    DataSet ds = cn.LayDuLieu(query, parameters);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dgvTTHoaDonDV.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin !!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm hóa đơn dịch vụ: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
            }
        }

        private void btnHienThiHDDV_Click(object sender, EventArgs e)
        {
            getDataHDDV();

        }

        //Dịch Vụ
        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO dichvu VALUES (@madichvu, N@tendichvu, @giadichvu)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@madichvu", txtMaDichVu.Text },
                            { "@tendichvu", txtTenDichVu.Text },
                            { "@giadichvu", txtGiaDichVu.Text }
                        };

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Thêm mới dịch vụ thành công.");
                getDataDV();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới dịch vụ.");
            }
        }

        private void btnSuaDichVu_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@madichvu", txtMaDichVu.Text },
                            { "@tendichvu", txtTenDichVu.Text },
                            { "@giadichvu", txtGiaDichVu.Text }
                        };

            string query = "update hogiadinh set  tendichvu=N@tendichvu, giadichvu=@giadichvu where  madichvu=@madichvu";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật dịch vụ " + txtMaDichVu.Text + " thành công.");
                getDataDV();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật dịch vụ.");
            }
        }

        private void btnTKDichVu_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemDichVu.Text.Trim();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                string query = "SELECT * FROM dichvu WHERE madichvu LIKE @tuKhoa OR tendichvu LIKE N@tuKhoa";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@tuKhoa", $"%{tuKhoa}%" }
                    };

                try
                {
                    DataSet ds = cn.LayDuLieu(query, parameters);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dgvDichVu.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin !!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm dịch vụ: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
            }
        }

        private void btnHienThiDichVu_Click(object sender, EventArgs e)
        {
            getDataDV();
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            string maDichVu = txtMaDichVu.Text.Trim();

            if (!string.IsNullOrEmpty(maDichVu))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận xóa",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = $"DELETE FROM dichvu WHERE madichvu = '{maDichVu}'";

                    try
                    {
                        if (cn.ThucThi(query))
                        {
                            MessageBox.Show("Xóa thành công!");
                            btnHienThiDichVu.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa dịch vụ: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.");
            }

        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvTTHoaDonDV.Rows.Count)
            {
                txtMaDichVu.Text = dgvDichVu.Rows[r].Cells["madichvu"].Value.ToString();
                txtTenDichVu.Text = dgvDichVu.Rows[r].Cells["tendichvu"].Value.ToString();
                txtGiaDichVu.Text = dgvDichVu.Rows[r].Cells["giadichvu"].Value.ToString();
            }
        }
    }
}
