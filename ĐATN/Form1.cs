using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace ĐATN
{
    public partial class Form1 : Form
    {
        public static string filePath;

        public Form1()
        {
            InitializeComponent();
            // Thiết lập giấy phép cho EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cls_Module.tab = tabControl1;
            // Hiển thị hộp thoại mở tệp để người dùng chọn tệp Excel
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Tất cả các tệp Excel (*.xlsx; *.xls)|*.xlsx; *.xls|Tất cả các tệp (*.*)|*.*",
                Title = "Chọn tệp Excel"
            };
            // nếu người dùng bấm chọn ok để mở file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {// lấy đường dẫn của người dùng vừa chọn file đấy 
                filePath = openFileDialog.FileName; // Lưu đường dẫn tệp vào biến tĩnh

                Cls_Module.filePath = openFileDialog.FileName;
                // Đọc dữ liệu từ file Excel lưu vào trong DataTable

                // Đọc tiết diện
                Cls_Module.dataTable = Cls_Function.ReadExcel(Cls_Module.filePath);

                Cls_Module.dataTable_1 = Cls_Function.ReadExcel_Tietdien(Cls_Module.filePath);

                dgv_dulieu.DataSource = Cls_Module.dataTable_1;

                // Đọc nội lực
                Cls_Module.dataTable2 = Cls_Function.ReadExcel_Noiluc(Cls_Module.filePath);
                dgv_noiluc.DataSource = Cls_Module.dataTable2;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                DataTable dt3 = Cls_Function.Tinh_AS();
                dgv_tinhtoan.DataSource = dt3;
            }
        }
    }
}
