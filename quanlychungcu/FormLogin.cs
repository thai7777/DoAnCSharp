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
    public partial class Login : Form
    {
        Connect cn = new Connect();
        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

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

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (cbMangLamViec.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn mảng làm việc trước khi đăng nhập.");
                return;
            }

            string selectedQuyenText = cbMangLamViec.SelectedItem.ToString();
            string matKhauMaHoa = MaHoaMatKhau(txtMK.Text);

            string query = string.Format(
                "SELECT * FROM dangxuat, quyen WHERE dangxuat.tendangnhap='{0}' AND dangxuat.matkhau ='{1}' AND quyen.tenquyen='{2}' AND quyen.idquyen=dangxuat.idquyen",
                txtTK.Text,
                matKhauMaHoa,
                selectedQuyenText);

            DataSet ds = cn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count == 1)
            {
                MessageBox.Show("Xin chào " + txtTK.Text + " !!!");
                this.Hide();
                if (selectedQuyenText == "Admin")
                {
                    ChuongTrinhAdmin admin = new ChuongTrinhAdmin();
                    admin.Show();
                }
                if (selectedQuyenText == "Manager")
                {
                    ChuongTrinh ct = new ChuongTrinh();
                    ct.Show();
                }
            }
            else
            {
                MessageBox.Show("Tài Khoản hoặc Mật Khẩu không đúng hoặc không có quyền hạn !!!");
            }
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            List<string> quyenList = cn.LayDanhSachQuyen();

            if (quyenList != null)
            {
                foreach (string quyen in quyenList)
                {
                    cbMangLamViec.Items.Add(quyen);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu quyền từ cơ sở dữ liệu.");
            }
        }
    }
}