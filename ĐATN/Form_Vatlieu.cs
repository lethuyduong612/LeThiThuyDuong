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
    public partial class Form_Vatlieu : Form
    {
        Dictionary<string, string> valueMapping = new Dictionary<string, string>()
            {
                { "B15", "8.5" },
                { "B20", "11.5" },
                { "B25", "14.5" },
                { "B30", "17" },
                { "B35", "19.5" },
                { "B40", "22" },
                { "B45", "25" },
                { "B50", "27.5" },
                { "B55", "30" },
                { "B60", "33" },
                { "B70", "37" },
                { "B80", "41" },
                { "B90", "44" },
                { "B100", "47.5" }
            };
        public Form_Vatlieu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_rb.Text != "" & txt_Rs.Text != "")
            {
                Cls_Module.Rb = Convert.ToDouble(txt_rb.Text);
                Cls_Module.Rs = Convert.ToDouble(txt_Rs.Text);
                Cls_Module.Abv = Convert.ToDouble(txt_Abv.Text);
                this.Close();
                MessageBox.Show("Đã lưu thành công");
            }
            else
            {
                MessageBox.Show("Thiếu dữ liệu");
            }
        }

        private void Form_Vatliue_Load(object sender, EventArgs e)
        {
            // Đặt SelectedIndex thành chỉ mục của mục bạn muốn chọn (ví dụ: chọn mục thứ nhất)
            cbb_macthep.SelectedIndex = 0;
            // Chọn mục đầu tiên (mục có chỉ mục là 0)
            string filePath = Form1.filePath; // Sử dụng đường dẫn tệp từ Form1
            if (!string.IsNullOrEmpty(filePath))
            {
                string cellValue = Cls_Function.ReadExcelCell(filePath);
                textBox1.Text = cellValue;

                if (valueMapping.ContainsKey(cellValue))
                {
                    txt_rb.Text = valueMapping[cellValue];


                }

            }

            

        }


        private void cbb_macthep_SelectedIndexChanged(object sender, EventArgs e)
        {   // Khởi tạo danh sách ánh xạ giữa giá trị của ComboBox và TextBox
            Dictionary<string, string> valueMap2 = new Dictionary<string, string>()
                {
                    { "CB240-V", "210" },
                    { "CB300-T", "260" },
                    { "CB300-V", "260" },
                    { "CB400-V", "350" },
                    { "CB500-V", "435" }
                };

            // Kiểm tra giá trị của ComboBox và thiết lập giá trị của TextBox tương ứng
            if (valueMap2.ContainsKey(cbb_macthep.Text))
            {
                txt_Rs.Text = valueMap2[cbb_macthep.Text];
            }

        }

        private void txt_rb_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Rs_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
