using Microsoft.CSharp.RuntimeBinder;
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
    public partial class ChiTietHopDong : Form
    {
        private string _receivedData;
        Connect cn = new Connect();
        public ChiTietHopDong(string data)
        {
            InitializeComponent();
            _receivedData = data;
        }

        public string ReceivedData
        {
            get { return _receivedData; }
        }

        public void getData()
        {
            string query = string.Format("select * from chitiethopdonggd where mahopdong = '{0}'",ReceivedData);
            DataSet ds = cn.LayDuLieu(query);
            dgvHopDong.DataSource = ds.Tables[0];
        }

        private void ChiTietHopDong_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void btnSuaHDDien_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "@mahopdong", ReceivedData },
                            { "@macthopdong", txtMaCTHopDong.Text },
                            { "@giatri", txtGiaTriHopDong.Text },
                            { "@thoihan", cbThoiHan.SelectedItem.ToString()},
                            { "@noidung", txtNoiDung.Text},
                            { "@ngaykihd", pickerNgayKiHD.Value.ToString() }
                        };
            string query = "update chitiethopdonggd set giatri=@giatri, thoihan=@thoihan, noidung=@noidung,ngaykihd= @ngaykihd where mahopdong=@mahopdong";

            if (cn.ThucThi(query, parameters))
            {
                MessageBox.Show("Cập nhật chi tiết hợp đồng " + ReceivedData + " thành công.");
                getData();
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật chi tiết hợp đồng");
            }
        }

        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvHopDong.Rows.Count)
            {
                txtMaCTHopDong.Enabled = false;
                txtMaHopDong.Enabled = false;
                dgvHopDong.Rows[r].Selected = true;
                txtMaHopDong.Text = dgvHopDong.Rows[r].Cells["mahopdong"].Value.ToString();
                txtMaCTHopDong.Text = dgvHopDong.Rows[r].Cells["macthopdong"].Value.ToString();
                cbThoiHan.Text = dgvHopDong.Rows[r].Cells["thoihan"].Value.ToString();
                txtGiaTriHopDong.Text = dgvHopDong.Rows[r].Cells["giatri"].Value.ToString();
                txtNoiDung.Text = dgvHopDong.Rows[r].Cells["noidung"].Value.ToString();
                string a = dgvHopDong.Rows[r].Cells["ngaykihd"].Value.ToString();
                if (a == "")
                {
                    return;
                }
                pickerNgayKiHD.Value = DateTime.Parse(a);
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
