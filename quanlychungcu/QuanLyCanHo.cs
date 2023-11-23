using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlychungcu
{
    public partial class QuanLyCanHo : Form
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
        public QuanLyCanHo()
        {
            InitializeComponent();
        }

        public void getData()
        {
            string query = "select * from canho";
            DataSet ds = cn.LayDuLieu(query);
            dgvCanHo.DataSource = ds.Tables[0];
            imagePath = null;
        }

        public void clearText()
        {
            txtMaCanHo.Enabled = true;
            txtMaCanHo.Clear();
            txtTenCanHo.Clear();
            txtLoaiCanHo.Clear();
            txtGiaCanHo.Clear();
            txtTrangThai.Clear();
            txtGhiChu.Clear();
            cbMaHoGD.Text = "";
            cbMaKhu.Text = "";
            ptQLCanHo.Image = null;
        }


        private void QuanLyCanHo_Load(object sender, EventArgs e)
        {
            getData();
            clearText();
            List<string> makhuList = cn.LayDanhSachKhu();

            if (makhuList != null)
            {
                foreach (string makhu in makhuList)
                {
                    cbMaKhu.Items.Add(makhu);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu mã khu từ cơ sở dữ liệu.");
            }

            List<string> mahogdList = cn.LayDanhSachHoGiaDinh();

            if (mahogdList != null)
            {
                foreach (string mahogd in mahogdList)
                {
                    cbMaHoGD.Items.Add(mahogd);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu mã phòng ban từ cơ sở dữ liệu.");
            }
        }


        private void dgvCanHo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvCanHo.Rows.Count)
            {
                // Lấy giá trị từ cột hình ảnh (thay "ImageColumnName" bằng tên cột chứa hình ảnh)
                if (e.RowIndex >= 0 && e.RowIndex < dgvCanHo.Rows.Count)
                {
                    object cellValue = dgvCanHo.Rows[e.RowIndex].Cells["hinhanh"].Value;

                    if (cellValue != DBNull.Value && cellValue != null)
                    {
                        byte[] imageData = (byte[])cellValue;

                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image originalImage = Image.FromStream(ms);
                            ptQLCanHo.Image = ResizeImage(originalImage, ptQLCanHo.Width, ptQLCanHo.Height);
                        }
                    }
                    else
                    {
                        ptQLCanHo.Image = null;
                    }
                }
                else
                {
                    ptQLCanHo.Image = null;
                }
                txtMaCanHo.Text = dgvCanHo.Rows[r].Cells["macanho"].Value.ToString();
                txtTenCanHo.Text = dgvCanHo.Rows[r].Cells["tencanho"].Value.ToString();
                txtLoaiCanHo.Text = dgvCanHo.Rows[r].Cells["loaicanho"].Value.ToString();
                txtGiaCanHo.Text = dgvCanHo.Rows[r].Cells["giacanho"].Value.ToString();
                txtTrangThai.Text = dgvCanHo.Rows[r].Cells["trangthai"].Value.ToString();
                txtGhiChu.Text = dgvCanHo.Rows[r].Cells["ghichu"].Value.ToString();
                cbMaKhu.Text = dgvCanHo.Rows[r].Cells["makhu"].Value.ToString();
                cbMaHoGD.Text = dgvCanHo.Rows[r].Cells["mahogd"].Value.ToString();
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

        private void btnThemCanHo_Click(object sender, EventArgs e)
        {
            if (imagePath != null)
            {
                try
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, ptQLCanHo.Width, ptQLCanHo.Height);
                    byte[] imageByteArray = ImageToByteArray(resizedImage);

                    // Sử dụng tham số trong câu lệnh SQL để tránh SQL Injection
                    string query = "INSERT INTO canho (macanho, tencanho, loaicanho, hinhanh, giacanho, trangthai, ghichu, makhu, mahogd) " +
                                   "VALUES (@macanho, @tencanho, @loaicanho, @hinhanhCanHo, @giacanho, @trangthai, @ghichu, @makhu, @mahogd)";

                    Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@macanho", txtMaCanHo.Text },
                            { "@tencanho", txtTenCanHo.Text },
                            { "@loaicanho", txtLoaiCanHo.Text },
                            { "@hinhanhCanHo", imageByteArray },
                            { "@giacanho", txtGiaCanHo.Text },
                            { "@trangthai", txtTrangThai.Text },
                            { "@ghichu", txtGhiChu.Text },
                            { "@makhu", cbMaKhu.SelectedItem.ToString() },
                            { "@mahogd", cbMaHoGD.SelectedItem.ToString() }
                        };

                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Thêm mới thông tin căn hộ thành công.");
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thêm mới thông tin căn hộ.");
                    }
                }
                catch (SqlException sqlex)
                {
                    MessageBox.Show("Lỗi SQL: " + sqlex.Message);
                    // Log lỗi vào file hoặc console
                    // Logger.Log("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xử lý hình ảnh: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hình ảnh trước khi thêm thông tin căn hộ.");
            }
        }

        private void ptHinhanhQLCanHo_Click_1(object sender, EventArgs e)
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
                ptQLCanHo.Image = ResizeImage(originalImage, ptQLCanHo.Width, ptQLCanHo.Height);
            }
        }

        private void btnSuaCanHo_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@macanho", txtMaCanHo.Text },
                            { "@tencanho", txtTenCanHo.Text },
                            { "@mahogd", cbMaHoGD.SelectedItem.ToString() },
                            { "@loaicanho", txtLoaiCanHo.Text },
                            { "@giacanho", txtGiaCanHo.Text },
                            { "@trangthai", txtTrangThai.Text },
                            { "@ghichu", txtGhiChu.Text },
                            { "@makhu", cbMaKhu.SelectedItem.ToString() }
                        };

                // Kiểm tra xem người dùng đã chọn ảnh mới hay chưa
                if (!string.IsNullOrEmpty(imagePath))
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, ptQLCanHo.Width, ptQLCanHo.Height);
                    byte[] imageByteArray = ImageToByteArray(resizedImage);
                    parameters.Add("@hinhanhCanHo", imageByteArray);
                }

                // Sử dụng tham số trong câu lệnh SQL để tránh SQL Injection
                string query = "update canho set tencanho=@tencanho,mahogd=@mahogd,loaicanho=@loaicanho,giacanho=@giacanho,trangthai=@trangthai,ghichu=@ghichu,makhu=@makhu";

                // Kiểm tra xem có thêm điều kiện update ảnh không
                if (!string.IsNullOrEmpty(imagePath))
                {
                    query += ", hinhanh=@hinhanhCanHo";
                }

                query += " where macanho=@macanho";

                if (cn.ThucThi(query, parameters))
                {
                    MessageBox.Show("Cập nhật căn hộ thành công.");
                    getData();
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật căn hộ.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý hình ảnh: " + ex.Message);
            }
        }

        private void btnHienThiCanHo_Click(object sender, EventArgs e)
        {
            getData();
            clearText();
        }

        private void btnXoaCanHo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa căn hộ này?", "Xác nhận xóa",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM canho WHERE macanho = @macanho";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@macanho", txtMaCanHo.Text }
                    };

                try
                {
                    if (cn.ThucThi(query, parameters))
                    {
                        MessageBox.Show("Xóa thành công!");
                        btnHienThiCanHo.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa căn hộ: " + ex.Message);
                }
            }
        }

        private void btnTKCanHo_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM canho WHERE macanho LIKE @tuKhoa OR tencanho LIKE @tuKhoa";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@tuKhoa", $"%{txtTimKiemCanHo.Text}%" }
                };

            try
            {
                DataSet ds = cn.LayDuLieu(query, parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvCanHo.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin !!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm căn hộ: " + ex.Message);
            }
        }
    }
}
