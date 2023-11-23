using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlychungcu
{
    public partial class HoGiaDinh : Form
    {
        Connect cn = new Connect();

        string imagePath;
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }

        public void getDataHGD()
        {
            string query = "select * from hogiadinh";
            DataSet ds = cn.LayDuLieu(query);
            dgvHoGiaDinh.DataSource = ds.Tables[0];
        }

        public void getDataQLThanhVien()
        {
            string query = "select * from thanhvienhogd";
            DataSet ds = cn.LayDuLieu(query);
            dgvThanhVienGD.DataSource = ds.Tables[0];
        }

        public void clearTextHGD()
        {
            txtMaHoGiaDinh.Enabled = true;
            txtHoTenChuHo.Clear();
            txtCMNDChuHo.Clear();
            txtSoLuongTV.Clear();
        }

        public void clearTextThanhVien()
        {
            txtMaThanhVien.Enabled = true;
            txtHoTenTV.Clear();
            cbGioitinh.Text = ""; ;
            txtCMNDThanhVien.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            cbMaHoGD.Text = "";
            ptThanhVien.Image = null;
            pickerNgaySinh.Value = DateTime.Now;
            txtMaThanhVien.Clear();
        }
        public HoGiaDinh()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Select();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabQLHoGiaDinh)
            {
                LoadTabQLHoGiaDinh();
            }
            else if (tabControl1.SelectedTab == tabQLThanhVien)
            {
                LoadTab2();
            }
        }

        private void LoadTabQLHoGiaDinh()
        {
            getDataHGD();
        }

        private void LoadTab2()
        {
            getDataQLThanhVien();
        }

        private void btnHienThiHoGD_Click(object sender, EventArgs e)
        {
            getDataHGD();
        }

        private void btnHienThiThanhVienGD_Click(object sender, EventArgs e)
        {
            getDataQLThanhVien();
            clearTextThanhVien();
            imagePath = null;

        }

        private void HoGiaDinh_Load(object sender, EventArgs e)
        {
            getDataQLThanhVien();
            getDataHGD();
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
        }

        private void dgvHoGiaDinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvHoGiaDinh.Rows.Count)
            {
                txtMaHoGiaDinh.Text = dgvHoGiaDinh.Rows[r].Cells["mahogd"].Value.ToString();
                txtHoTenChuHo.Text = dgvHoGiaDinh.Rows[r].Cells["tenchuho"].Value.ToString();
                txtCMNDChuHo.Text = dgvHoGiaDinh.Rows[r].Cells["cmnd"].Value.ToString();
                txtSoLuongTV.Text = dgvHoGiaDinh.Rows[r].Cells["soluongtv"].Value.ToString();
            }
        }

        private void btnThemHoGiaDinh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHoGiaDinh.Text) || string.IsNullOrEmpty(txtHoTenChuHo.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            string query = "INSERT INTO hogiadinh VALUES (@mahogd, @tenchuho, @cmnd, @soluongtv)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@mahogd", txtMaHoGiaDinh.Text },
                    { "@tenchuho", txtHoTenChuHo.Text },
                    { "@cmnd", txtCMNDChuHo.Text },
                    { "@soluongtv", txtSoLuongTV.Text },
                };

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Thêm mới hộ gia đình thành công.");
                getDataHGD();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới hộ gia đình.");
            }
        }

        private void txtMaHoGiaDinh_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSuaNhanVien_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahogd", txtMaHoGiaDinh.Text },
                            { "@tenchuho", txtHoTenChuHo.Text },
                            { "@cmnd", txtCMNDChuHo.Text },
                            { "@soluongtv", txtSoLuongTV.Text },
                        };

            string query = "update hogiadinh set tenchuho=@tenchuho,cmnd=@cmnd,soluongtv=@soluongtv where mahogd=@mahogd";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật hộ gia đình thành công.");
                getDataHGD();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật hộ gia đình.");
            }
        }

        private void btnXoaHoGiaDinh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaHoGiaDinh.Text))
            {
                string query = "DELETE FROM hogiadinh WHERE mahogd = @mahogd";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@mahogd", txtMaHoGiaDinh.Text }
                };

                try
                {
                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Xóa thành công!");
                        btnHienThiHoGD.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa hộ gia đình: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hộ gia đình cần xóa.");
            }
        }

        private void btnTKHoGiaDinh_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from hogiadinh where mahogd like'%{0}%' or tenchuho like N'%{0}%'",
                txtTimKiemHoGiaDinh.Text
                );
            DataSet ds = cn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvHoGiaDinh.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }
        }

        // thành viên hộ gia đình

        private void btnTKThanhVien_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from thanhvienhogd where mathanhvien like'%{0}%' or hoten like N'%{0}%'",
                txtTimKiemTV.Text
                );
            DataSet ds = cn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvThanhVienGD.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }
        }

        private void dgvThanhVienGD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvThanhVienGD.Rows.Count)
            {
                // Lấy giá trị từ cột hình ảnh (thay "ImageColumnName" bằng tên cột chứa hình ảnh)
                if (e.RowIndex >= 0 && e.RowIndex < dgvThanhVienGD.Rows.Count)
                {
                    object cellValue = dgvThanhVienGD.Rows[e.RowIndex].Cells["hinhanh"].Value;

                    if (cellValue != DBNull.Value && cellValue != null)
                    {
                        byte[] imageData = (byte[])cellValue;

                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image originalImage = Image.FromStream(ms);
                            ptThanhVien.Image = ResizeImage(originalImage, ptThanhVien.Width, ptThanhVien.Height);
                        }
                    }
                    else
                    {
                        ptThanhVien.Image = null;
                    }
                }
                else
                {
                    ptThanhVien.Image = null;
                }
                txtMaThanhVien.Text = dgvThanhVienGD.Rows[r].Cells["mathanhvien"].Value.ToString();
                txtHoTenTV.Text = dgvThanhVienGD.Rows[r].Cells["hoten"].Value.ToString();
                cbGioitinh.Text = dgvThanhVienGD.Rows[r].Cells["giotinh"].Value.ToString();
                txtCMNDThanhVien.Text = dgvThanhVienGD.Rows[r].Cells["cmnd"].Value.ToString();
                txtSDT.Text = dgvThanhVienGD.Rows[r].Cells["sdt"].Value.ToString();
                txtEmail.Text = dgvThanhVienGD.Rows[r].Cells["email"].Value.ToString();
                string a = dgvThanhVienGD.Rows[r].Cells["ngaysinh"].Value.ToString();
                if (a == "")
                {
                    return;
                }
                pickerNgaySinh.Value = DateTime.Parse(a);
                cbMaHoGD.Text = dgvThanhVienGD.Rows[r].Cells["mahogd"].Value.ToString();
            }
        }

        private void ptThanhVien_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Đặt các thuộc tính cho hộp thoại OpenFileDialog
            openFileDialog.Title = "Chọn hình ảnh";
            openFileDialog.Filter = "Tất cả các tệp|*.*|Hình ảnh|*.jpg;*.jpeg;*.png;*.gif";

            // Hiển thị hộp thoại và kiểm tra xem người dùng đã chọn file chưa
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName;
                MessageBox.Show("Đường dẫn ảnh: " + imagePath);
                // Đọc hình ảnh từ file
                Image originalImage = Image.FromFile(imagePath);

                // Resize hình ảnh để vừa với PictureBox
                ptThanhVien.Image = ResizeImage(originalImage, ptThanhVien.Width, ptThanhVien.Height);
            }
        }
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Chọn định dạng hình ảnh
                return ms.ToArray();
            }
        }

        private void btnThemThanhVien_Click(object sender, EventArgs e)
        {
            if (imagePath != null)
            {
                try
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, ptThanhVien.Width, ptThanhVien.Height);
                    byte[] imageByteArray = ImageToByteArray(resizedImage);

                    // Sử dụng tham số trong câu lệnh SQL để tránh SQL Injection
                    string query = "INSERT INTO thanhvienhogd (mathanhvien, hoten, giotinh, mahogd, sdt, ngaysinh, email,cmnd, hinhanh) " +
                                   "VALUES (@mathanhvien, @hoten, @giotinh, @mahogd, @sdt, @ngaysinh, @email,@cmnd, @hinhanh)";

                    Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mathanhvien", txtMaThanhVien.Text },
                            { "@hoten", txtHoTenTV.Text },
                            { "@giotinh", cbGioitinh.SelectedItem.ToString() },
                            { "@mahogd", cbMaHoGD.SelectedItem.ToString() },
                            { "@sdt", txtSDT.Text },
                            { "@cmnd", txtCMNDThanhVien.Text },
                            { "@ngaysinh", pickerNgaySinh.Value.ToString("yyyy/MM/dd") },
                            { "@email", txtEmail.Text },
                            { "@hinhanh", imageByteArray }
                        };

                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Thêm mới thành viên thành công.");
                        getDataQLThanhVien();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thêm mới thành viên.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xử lý hình ảnh: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hình ảnh trước khi thêm thành viên.");
            }
        }

        private void btnSuaThanhVien_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mathanhvien", txtMaThanhVien.Text },
                            { "@hoten", txtHoTenTV.Text },
                            { "@giotinh", cbGioitinh.SelectedItem.ToString() },
                            { "@mahogd", cbMaHoGD.SelectedItem.ToString() },
                            { "@sdt", txtSDT.Text },
                            { "@cmnd", txtCMNDThanhVien.Text },
                            { "@ngaysinh", pickerNgaySinh.Value.ToString("yyyy/MM/dd") },
                            { "@email", txtEmail.Text }
                        };

                // Kiểm tra xem người dùng đã chọn ảnh mới hay chưa
                if (!string.IsNullOrEmpty(imagePath))
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, ptThanhVien.Width, ptThanhVien.Height);
                    byte[] imageByteArray = ImageToByteArray(resizedImage);
                    parameters.Add("@hinhanh", imageByteArray);
                }

                // Sử dụng tham số trong câu lệnh SQL để tránh SQL Injection
                string query = "update thanhvienhogd set hoten=@hoten,giotinh=@giotinh,mahogd=@mahogd,sdt=@sdt,ngaysinh=@ngaysinh,email=@email,cmnd=@cmnd";

                // Kiểm tra xem có thêm điều kiện update ảnh không
                if (!string.IsNullOrEmpty(imagePath))
                {
                    query += ", hinhanh=@hinhanh";
                }

                query += " where mathanhvien=@mathanhvien";

                if (cn.ThucThi(query, parameters))
                {
                    MessageBox.Show("Cập nhật thành viên thành công.");
                    getDataQLThanhVien();
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật thành viên.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý hình ảnh: " + ex.Message);
            }
        }

        private void btnXoaThanhVien_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thành viên này?", "Xác nhận xóa",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM thanhviengd WHERE mathanhvien = @mathanhvien";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@mathanhvien", txtMaThanhVien.Text }
                    };

                try
                {
                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Xóa thành công!");
                        btnHienThiThanhVien.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa thành viên: " + ex.Message);
                }
            }
        }
    }
}
