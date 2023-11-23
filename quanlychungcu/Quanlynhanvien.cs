using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace quanlychungcu
{
    public partial class Quanlynhanvien : Form
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
        public Quanlynhanvien()
        {
            InitializeComponent();
        }
        public void getData()
        {
            string query = "select * from nhanvien";
            DataSet ds = cn.LayDuLieu(query);
            dgvNhanVien.DataSource = ds.Tables[0];
            imagePath = null;
        }

        public void clearText()
        {
            txtMaNhanVien.Enabled = true;
            txtHoTenNhanVien.Clear();
            cbGioitinh.Text=""; ;
            txtSDTNhanVien.Clear();
            txtEmailNhanVien.Clear();
            txtDiaChiNhanVien.Clear();
            cbmaphongban.Text = "";
            ptHinhanh.Image = null;
            pickerNgaySinh.Value = DateTime.Now;
            txtMaNhanVien.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void Quanlynhanvien_Load(object sender, EventArgs e)
        {
            getData();
            List<string> maphongbanList = cn.LayDanhSachPhongBan();

            if (maphongbanList != null)
            {
                foreach (string maphongban in maphongbanList)
                {
                    cbmaphongban.Items.Add(maphongban);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu mã phòng ban từ cơ sở dữ liệu.");
            }

        }

        private void lvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnHienThiNhanVien_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                // Lấy giá trị từ cột hình ảnh
                if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
                {
                    object cellValue = dgvNhanVien.Rows[e.RowIndex].Cells["hinhanh"].Value;

                    if (cellValue != DBNull.Value && cellValue != null)
                    {
                        byte[] imageData = (byte[])cellValue;

                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image originalImage = Image.FromStream(ms);
                            ptHinhanh.Image = ResizeImage(originalImage, ptHinhanh.Width, ptHinhanh.Height);
                        }
                    }
                    else
                    {
                        ptHinhanh.Image = null;
                    }
                }
                else
                {
                    ptHinhanh.Image = null;
                }
                txtMaNhanVien.Text = dgvNhanVien.Rows[r].Cells["manhanvien"].Value.ToString();
                txtHoTenNhanVien.Text = dgvNhanVien.Rows[r].Cells["hoten"].Value.ToString();
                cbGioitinh.Text = dgvNhanVien.Rows[r].Cells["gioitinh"].Value.ToString();
                txtDiaChiNhanVien.Text = dgvNhanVien.Rows[r].Cells["diachi"].Value.ToString();
                txtSDTNhanVien.Text = dgvNhanVien.Rows[r].Cells["sdt"].Value.ToString();
                txtEmailNhanVien.Text = dgvNhanVien.Rows[r].Cells["email"].Value.ToString();
                txtCMNDNhanVien.Text = dgvNhanVien.Rows[r].Cells["cmnd"].Value.ToString();
                string a = dgvNhanVien.Rows[r].Cells["ngaysinh"].Value.ToString();
                if (!string.IsNullOrEmpty(a))
                {
                    pickerNgaySinh.Value = DateTime.Parse(a);
                }
                else
                {
                    pickerNgaySinh.Value = DateTime.Now; // hoặc giá trị mặc định khác
                }
                cbmaphongban.Text = dgvNhanVien.Rows[r].Cells["maphongban"].Value.ToString();
            }
        }

        private void ptHinhanh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Đặt các thuộc tính cho hộp thoại OpenFileDialog
            openFileDialog.Title = "Chọn hình ảnh";
            openFileDialog.Filter = "Tất cả các tệp|*.*|Hình ảnh|*.jpg;*.jpeg;*.png;*.gif";

            // Hiển thị hộp thoại và kiểm tra xem người dùng đã chọn file chưa
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName;
                // Đọc hình ảnh từ file
                Image originalImage = Image.FromFile(imagePath);

                // Resize hình ảnh để vừa với PictureBox
               ptHinhanh.Image = ResizeImage(originalImage, ptHinhanh.Width, ptHinhanh.Height);
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

        private void btnThemNhanVien_Click_1(object sender, EventArgs e)
        {
            if (imagePath != null)
            {
                try
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, ptHinhanh.Width, ptHinhanh.Height);
                    byte[] imageByteArray = ImageToByteArray(resizedImage);

                    // Sử dụng tham số trong câu lệnh SQL để tránh SQL Injection
                    string query = "INSERT INTO nhanvien (manhanvien, hoten, gioitinh, diachi, sdt, ngaysinh, email,cmnd, maphongban, hinhanh) " +
                                   "VALUES (@manhanvien, @hoten, @gioitinh, @diachi, @sdt, @ngaysinh, @email,@cmnd, @maphongban, @hinhanh)";

                    Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@manhanvien", txtMaNhanVien.Text },
                            { "@hoten", txtHoTenNhanVien.Text },
                            { "@gioitinh", cbGioitinh.SelectedItem.ToString() },
                            { "@diachi", txtDiaChiNhanVien.Text },
                            { "@sdt", txtSDTNhanVien.Text },
                            { "@cmnd", txtCMNDNhanVien.Text },
                            { "@ngaysinh", pickerNgaySinh.Value.ToString("yyyy/MM/dd") },
                            { "@email", txtEmailNhanVien.Text },
                            { "@maphongban", cbmaphongban.SelectedItem.ToString() },
                            { "@hinhanh", imageByteArray }
                        };

                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Thêm mới nhân viên thành công.");
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thêm mới nhân viên.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xử lý hình ảnh: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hình ảnh trước khi thêm nhân viên.");
            }
        }

        private void btnSuaNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@manhanvien", txtMaNhanVien.Text },
            { "@hoten", txtHoTenNhanVien.Text },
            { "@gioitinh", cbGioitinh.SelectedItem.ToString() },
            { "@diachi", txtDiaChiNhanVien.Text },
            { "@cmnd", txtCMNDNhanVien.Text },
            { "@sdt", txtSDTNhanVien.Text },
            { "@ngaysinh", pickerNgaySinh.Value.ToString("yyyy/MM/dd") },
            { "@email", txtEmailNhanVien.Text },
            { "@maphongban", cbmaphongban.SelectedItem.ToString() },
        };

                // Kiểm tra xem người dùng đã chọn ảnh mới hay chưa
                if (!string.IsNullOrEmpty(imagePath))
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, ptHinhanh.Width, ptHinhanh.Height);
                    byte[] imageByteArray = ImageToByteArray(resizedImage);
                    parameters.Add("@hinhanh", imageByteArray);
                }

                // Sử dụng tham số trong câu lệnh SQL để tránh SQL Injection
                string query = "update nhanvien set hoten=@hoten,gioitinh=@gioitinh,diachi=@diachi,sdt=@sdt,ngaysinh=@ngaysinh,email=@email,cmnd=@cmnd,maphongban=@maphongban";

                // Kiểm tra xem có thêm điều kiện update ảnh không
                if (!string.IsNullOrEmpty(imagePath))
                {
                    query += ", hinhanh=@hinhanh";
                }

                query += " where manhanvien=@manhanvien";

                if (cn.ThucThi(query, parameters))
                {
                    MessageBox.Show("Cập nhật nhân viên thành công.");
                    getData();
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật nhân viên.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý hình ảnh: " + ex.Message);
            }
        }

        private void btnHienThiNhanVien_Click_1(object sender, EventArgs e)
        {
            getData();
            clearText();
        }

        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Xóa nhân viên
                    string query = "DELETE FROM nhanvien WHERE manhanvien=@manhanvien";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@manhanvien", txtMaNhanVien.Text }
                        };

                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Xóa nhân viên thành công.");
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa nhân viên.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }

        private void btnTKNhanVien_Click(object sender, EventArgs e)
        {
            string timKiemValue = txtTimKiemNV.Text.Trim();
            string query = $"SELECT * FROM nhanvien WHERE manhanvien LIKE '%{timKiemValue}%' OR hoten LIKE N'%{timKiemValue}%'";

            DataSet ds = cn.LayDuLieu(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvNhanVien.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin !!!");
            }

        }
    }
}
