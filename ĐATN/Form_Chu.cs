using System;
using System.Windows.Forms;

namespace ĐATN
{
    public partial class Form_Chu : Form
    {
        public Form_Chu()
        {
            InitializeComponent();
            // Thiết lập vị trí bắt đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // xóa tất cả các form có trong panel
            panel_main.Controls.Clear();
            // show form lên
            //tao form moi =))))
            Form1 dulieu = new Form1();
            //Frm_Dulieu dulieu = new Frm_Dulieu();
            // đặt level của form không phải là cao nhất
            dulieu.TopLevel = false;

            dulieu.Dock = DockStyle.Fill;
            // thêm form vào panel
            panel_main.Controls.Add(dulieu);
            // xong là show form lên 
            dulieu.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form
            Form_Vatlieu yourForm = new Form_Vatlieu();

            // Hiển thị form
            yourForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cls_Module.tab.SelectedIndex = 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
