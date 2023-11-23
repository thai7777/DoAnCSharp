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
    public partial class KiemTraCapNhat : Form
    {
        public KiemTraCapNhat()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            lbversion.Text = "Phiên bản mới nhất : 1.0 ";
            MessageBox.Show("Phiên bản hiện tại đã là phiên bản mới nhất ");
        }
    }
}
