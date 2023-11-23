using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlychungcu
{
    public partial class QuanLyTaiKhoan : Form
    {
        Connect cn = new Connect();

        public QuanLyTaiKhoan()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            GetDataTK();
            LoadIDQuyen();
        }
        private void GetDataTK()
        {
            string query = "SELECT * FROM dangxuat";
            DataSet ds = cn.LayDuLieu(query);
            dgvTaiKhoan.DataSource = ds.Tables[0];
        }

        private void LoadIDQuyen()
        {
            List<string> IDList = cn.LayDanhSachIDQuyen();

            if (IDList != null)
            {
                cbIDQuyen.Items.AddRange(IDList.ToArray());
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu quyền từ cơ sở dữ liệu.");
            }
        }

        public void clearTextTK()
        {
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cbIDQuyen.Enabled = true;
            cbIDQuyen.Text = "";
        }

        private void QuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvTaiKhoan.Rows.Count)
            {
                txtMaNhanVien.Text = dgvTaiKhoan.Rows[r].Cells["manhanvien"].Value.ToString();
                txtTenDangNhap.Text = dgvTaiKhoan.Rows[r].Cells["tendangnhap"].Value.ToString();
                txtMatKhau.Text = dgvTaiKhoan.Rows[r].Cells["matkhau"].Value.ToString();
                cbIDQuyen.Text = dgvTaiKhoan.Rows[r].Cells["idquyen"].Value.ToString();
            }
        }

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO dangxuat (manhanvien, tendangnhap, matkhau, idquyen) " +
                        "VALUES (@manhanvien, @tendangnhap, @matkhau, @idquyen)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@manhanvien", txtMaNhanVien.Text },
            { "@tendangnhap", txtTenDangNhap.Text },
            { "@matkhau", MaHoaMatKhau(txtMatKhau.Text) },
            { "@idquyen", cbIDQuyen.Text },
        };

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Thêm mới tài khoản thành công.");
                GetDataTK();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới tài khoản.");
            }
        }

        private void btnSuaTaiKhoan_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@manhanvien", txtMaNhanVien.Text },
                    { "@tendangnhap", txtTenDangNhap.Text },
                    { "@matkhau", MaHoaMatKhau(txtMatKhau.Text) },
                    { "@idquyen", cbIDQuyen.Text },
                };

            string query = "UPDATE dangxuat SET tendangnhap=@tendangnhap, matkhau=@matkhau, idquyen=@idquyen WHERE manhanvien=@manhanvien";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật tài khoản thành công.");
                GetDataTK();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật tài khoản.");
            }
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM dangxuat WHERE manhanvien=@manhanvien";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@manhanvien", txtMaNhanVien.Text }
                };

                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Xóa tài khoản thành công.");
                        GetDataTK();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa tài khoản.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.");
            }
        }

        private void btnHienTaiKhoan_Click(object sender, EventArgs e)
        {
            GetDataTK();
        }

        private void btnTKTaiKhoan_Click(object sender, EventArgs e)
        {
            string query = string.Format("SELECT * FROM dangxuat WHERE tendangnhap LIKE '%{0}%' OR manhanvien LIKE N'%{0}%'", txtTimKiemTaiKhoan.Text);
            DataSet ds = cn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvTaiKhoan.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }
        }

        private string MaHoaMatKhau(string matKhau)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(matKhau));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
