using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;

namespace quanlychungcu
{
    public partial class ChuongTrinhAdmin : Form
    {
        Connect cn = new Connect();
        private List<GunaButton> buttons = new List<GunaButton>();
        public ChuongTrinhAdmin()
        {
            InitializeComponent();
        }

        private Form currentFormChill;
        private void OpenChillForm(Form childForm)
        {
            if (currentFormChill != null)
            {
                currentFormChill.Close();
            }
            currentFormChill = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.AutoScroll = true;
            childForm.AutoScrollMargin = new Size(0, 0);
            childForm.Dock = DockStyle.Fill;
            panelBody.Controls.Add(childForm);
            panelBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ChuongTrinhAdmin_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn đang Thoát chương trình" + "\n" + "Bạn có muốn tiếp tục không ?", "Thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn đang đăng xuất" + "\n" + "Bạn có muốn đăng xuất không ?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void btnFormQLNhanVien_Click(object sender, EventArgs e)
        {
            OpenChillForm(new QuanLyTaiKhoan());
            lbTitle.Text = btnFormQLNhanVien.Text;
        }

        private void btnFormQLCanHo_Click(object sender, EventArgs e)
        {
            OpenChillForm(new KiemTraCapNhat());
            lbTitle.Text = btnFormKTCapNhat.Text;
        }
    }
}
