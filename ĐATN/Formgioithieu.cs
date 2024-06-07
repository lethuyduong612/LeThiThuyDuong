using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ĐATN
{
    public partial class Formgioithieu : Form
    {
        public Formgioithieu()
        {
            InitializeComponent();
            // Thiết lập vị trí bắt đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form
            Form_Chu yourForm = new Form_Chu();

            // Hiển thị form
            yourForm.Show();
        }
    }
}
