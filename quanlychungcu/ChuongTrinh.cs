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
using Excel = Microsoft.Office.Interop.Excel;

namespace quanlychungcu
{
    public partial class ChuongTrinh : Form
    {
        Connect cn = new Connect();
        private List<GunaButton> buttons = new List<GunaButton>();

        public ChuongTrinh()
        {
            InitializeComponent();
        }

        private void panelHeader_Resize(object sender, EventArgs e)
        {
            // Đặt vị trí giữa cho lbTitle khi panelHeader thay đổi kích thước
            lbTitle.Location = new Point((panel2.Width - lbTitle.Width) / 2, (panel2.Height - lbTitle.Height) / 2);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Login lg = new Login();
            lg.Show();
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

        private void btnFormQLNhanVien_Click(object sender, EventArgs e)
        {
            OpenChillForm(new Quanlynhanvien());
            lbTitle.Text = btnFormQLNhanVien.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentFormChill != null)
            {
                currentFormChill.Close();
                lbTitle.Text = "Home";
            }
        }

        private void btnFormQLCanHo_Click(object sender, EventArgs e)
        {

            OpenChillForm(new QuanLyCanHo());
            lbTitle.Text =btnFormQLCanHo.Text;
        }

        private void btnFormQLHopDong_Click(object sender, EventArgs e)
        {
            OpenChillForm(new Quanlyhopdong());
            lbTitle.Text = btnFormQLHopDong.Text;
        }

        private void btnFormHoGiaDinh_Click(object sender, EventArgs e)
        {
            OpenChillForm(new HoGiaDinh());
            lbTitle.Text = btnFormHoGiaDinh.Text;
        }

        private void btnFormDichVu_Click(object sender, EventArgs e)
        {
            OpenChillForm(new QuanLyDichVu());
            lbTitle.Text = btnFormDichVu.Text;
        }

        private void btnFormQLBaiXe_Click(object sender, EventArgs e)
        {
            OpenChillForm(new Quanlybaiguixe());
            lbTitle.Text = btnFormQLBaiXe.Text;
        }

        private void btnFormDienNuoc_Click(object sender, EventArgs e)
        {
            OpenChillForm(new Quanlydiennuoc());
            lbTitle.Text = btnFormDienNuoc.Text;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn đang đăng xuất" + "\n" + "Bạn có muốn tiếp tục không ?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn đang Thoát chương trình" + "\n" + "Bạn có muốn tiếp tục không ?", "Thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void ChuongTrinh_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.Resize += new EventHandler(panelHeader_Resize);
        }


        //Thống kê

        string loaihoadon;
        private void btnThongKeThang_Click(object sender, EventArgs e)
        {
            if (cbChonThongKe.SelectedItem.ToString()=="Theo Tháng")
            {
                try {
                    string query = string.Format("select tenhoadon,count(tenhoadon) as soluonghoadon,sum(soluong)as tongsoluong,sum(tongtien)  as doanhthu  from {0} where month(ngayin)='{1}' group by tenhoadon", loaihoadon, cbMonth.SelectedItem.ToString());
                    DataSet ds = cn.LayDuLieu(query);
                    dgvThongKe.DataSource = ds.Tables[0];
                    lbNgayTao.Text ="Ngày tạo báo cáo "+ DateTime.Now.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi"+ex);
                }
                
            }
            else
            {
                try {
                    string query = string.Format("select tenhoadon,count(tenhoadon) as soluonghoadon,sum(soluong)as tongsoluong,sum(tongtien)  as doanhthu  from {0} where year(ngayin)='{1}' group by tenhoadon",loaihoadon, txtYear.Text);
                    DataSet ds = cn.LayDuLieu(query);
                    dgvThongKe.DataSource = ds.Tables[0];
                    lbNgayTao.Text = "Ngày tạo báo cáo " + DateTime.Now.ToString();
                }
                catch(Exception ex) { MessageBox.Show("Lỗi" + ex); }
            }
        }

        private void cbChonLoaiHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChonLoaiHD.SelectedItem.ToString() == "Hoá đơn điện")
            {
                loaihoadon = "hoadondien";
            }
            else if (cbChonLoaiHD.SelectedItem.ToString() == "Hoá đơn nước")
            {
                loaihoadon = "hoadonnuoc";
            }
            else
            {
                loaihoadon = "hoadondv";
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";
                saveFileDialog.Title = "Lưu file Excel";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Workbooks.Add();

                    Excel.Worksheet worksheet = excelApp.ActiveSheet;
                    worksheet.Name = "ThongKe"+cbChonLoaiHD.SelectedItem.ToString()+cbChonThongKe.SelectedItem.ToString();

                    DataTable dataTable = (DataTable)dgvThongKe.DataSource;

                    if (dataTable != null)
                    {
                        for (int i = 1; i <= dataTable.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
                        }

                        for (int i = 1; i <= dataTable.Rows.Count; i++)
                        {
                            for (int j = 1; j <= dataTable.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 1, j] = dataTable.Rows[i - 1][j - 1];
                            }
                        }

                        excelApp.ActiveWorkbook.SaveAs(saveFileDialog.FileName);
                        excelApp.ActiveWorkbook.Saved = true;
                        excelApp.Quit();

                        MessageBox.Show("Xuất Excel thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để xuất!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void cbChonThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbChonThongKe.SelectedItem.ToString()== "Theo Tháng")
            {
                cbMonth.Enabled = true;
                txtYear.Enabled = false;
            }else
            {
                txtYear.Enabled = true;
                cbMonth.Enabled = false;
            }
        }
    }
}
