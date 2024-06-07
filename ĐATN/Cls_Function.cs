using System.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using static Autodesk.Revit.DB.SpecTypeId;
using System.Collections.Generic;
using System.IO.Compression;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;

namespace ĐATN
{
    public class Cls_Function
    {
        //  đọc file excel
        public static DataTable ReadExcel(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Frame Sec Def - Conc Rect"];
                int lastRow = worksheet.Dimension.End.Row;

                // Thêm cột vào DataTable
                dataTable.Columns.Add("Loại cột");
                dataTable.Columns.Add("Bề rộng");
                dataTable.Columns.Add("Chiều cao");

                // Đọc toàn bộ dữ liệu một lần
                var cells = worksheet.Cells;

                // Đọc dữ liệu từ các cột đã chọn và thêm vào DataTable
                for (int row = 4; row <= lastRow; row++)
                {
                    if (cells[row, 9].Text == "Column")
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = cells[row, 1].Text;
                        dataRow[1] = cells[row, 4].Text;
                        dataRow[2] = cells[row, 5].Text;

                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            return dataTable;
        }

        // Đọc và chọn giá trị nội lực từ file Excel với hiệu năng cao hơn
        public static DataTable ReadExcel_Noiluc(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Element Forces - Columns"];
                int lastRow = worksheet.Dimension.End.Row;

                // Thêm cột vào DataTable
                dataTable.Columns.Add("Tầng");
                dataTable.Columns.Add("Tên cột");
                dataTable.Columns.Add("Vị trí");
                dataTable.Columns.Add("Lực dọc(kN)");
                dataTable.Columns.Add("MomenX(kN.m)");
                dataTable.Columns.Add("MomenY(kN.m)");

                // Đọc toàn bộ dữ liệu một lần
                var cells = worksheet.Cells;

                // Đọc dữ liệu từ các cột đã chọn và thêm vào DataTable
                for (int row = 4; row <= lastRow; row++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = cells[row, 1].Text;
                    dataRow[1] = cells[row, 3].Text;
                    dataRow[2] = cells[row, 7].Text;
                    dataRow[3] = Math.Round(double.Parse(cells[row, 8].Text), 3);
                    dataRow[4] = Math.Round(double.Parse(cells[row, 12].Text), 3);
                    dataRow[5] = Math.Round(double.Parse(cells[row, 13].Text), 3);

                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }

        public static DataTable ReadExcel_Tietdien(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Frame Assigns - Summary"];
                int lastRow = worksheet.Dimension.End.Row;

                // Thêm cột vào DataTable
                dataTable.Columns.Add("Tên cột");
                dataTable.Columns.Add("Tiết diện");
                dataTable.Columns.Add("Bề rộng(mm)");
                dataTable.Columns.Add("Chiều cao(mm)");

                // Đọc toàn bộ dữ liệu một lần
                var cells = worksheet.Cells;

                // Đọc dữ liệu từ các cột đã chọn và thêm vào DataTable
                for (int row = 4; row <= lastRow; row++)
                {
                    if (cells[row, 4].Text == "Column")
                    {
                        var Berong = (from row2 in Cls_Module.dataTable.AsEnumerable()
                                      where row2.Field<string>(0) == cells[row, 7].Text
                                      select row2.Field<string>(1)).FirstOrDefault();

                        var Chieucao = (from row2 in Cls_Module.dataTable.AsEnumerable()
                                        where row2.Field<string>(0) == cells[row, 7].Text
                                        select row2.Field<string>(2)).FirstOrDefault();

                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = cells[row, 3].Text;
                        dataRow[1] = cells[row, 7].Text;
                        dataRow[2] = Berong;
                        dataRow[3] = Chieucao;

                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            return dataTable;
        }
        public static DataTable Loctietdien (string filePath)
        {
            DataTable dataTable = new DataTable();
            int[] columnsToRead = { 2, 5,6 };
            Microsoft.Office.Interop.Excel.Application mExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = mExcel.Workbooks.Open(filePath);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item("Frame Assigns - Summary");
            int lastRow = xlWorkSheet.Cells[xlWorkSheet.Rows.Count, 1].End[Microsoft.Office.Interop.Excel.XlDirection.xlUp].Row;

            dataTable.Columns.Add("Tencot");
            dataTable.Columns.Add("Chieucaotang");
            dataTable.Columns.Add("Tietdien");
            xlWorkBook.Close();

            return dataTable;
        }
        //tes
        public static DataTable Tinh_AS()
        {
            DataTable dataTable = new DataTable();
            if (dataTable.Rows.Count < 4) 
            {
                dataTable.Columns.Add("Tầng");
                dataTable.Columns.Add("Tên cột");
                dataTable.Columns.Add("Bề rộng");
                dataTable.Columns.Add("Chiều cao");
                dataTable.Columns.Add("Vị trí");
                dataTable.Columns.Add("Lực dọc");
                dataTable.Columns.Add("MomenX");
                dataTable.Columns.Add("MomenY");
                dataTable.Columns.Add("As (mm2)");
                dataTable.Columns.Add("Gợi ý chọn thép");
                dataTable.Columns.Add("As mới (mm2)");
            }
           
            for (int i = 0; i < Cls_Module.dataTable2.Rows.Count; i++)
            {
                var Berong = (from row2 in Cls_Module.dataTable_1.AsEnumerable()
                              where row2.Field<string>(0) == Cls_Module.dataTable2.Rows[i].Field<string>(1)
                              select row2.Field<string>(2)).FirstOrDefault();

                var Chieucao = (from row2 in Cls_Module.dataTable_1.AsEnumerable()
                                where row2.Field<string>(0) == Cls_Module.dataTable2.Rows[i].Field<string>(1)
                                select row2.Field<string>(3)).FirstOrDefault();
                DataRow dataRow = dataTable.NewRow();

                // Use colIndex instead of hard-coded column indices
                dataRow[0] = Cls_Module.dataTable2.Rows[i][0].ToString();
                dataRow[1] = Cls_Module.dataTable2.Rows[i][1].ToString();
                dataRow[2] = Berong;
                dataRow[3] = Chieucao;
                dataRow[4] = Cls_Module.dataTable2.Rows[i][2].ToString();
                dataRow[5] = Cls_Module.dataTable2.Rows[i][3].ToString();
                dataRow[6] = Cls_Module.dataTable2.Rows[i][4].ToString();
                dataRow[7] = Cls_Module.dataTable2.Rows[i][5].ToString();

               
                //dataRow[8] = Math.Round(Value.As(),3);
                dataTable.Rows.Add(dataRow);
            }
            for (int i = 0;  i < dataTable.Rows.Count;i++)
            {
                string Cx1 = dataTable.Rows[i]["Chiều cao"].ToString();
                string Cy1 = dataTable.Rows[i]["Bề rộng"].ToString();
                double l = 3600;
                double l0 = l * 0.7;
                string N1 = dataTable.Rows[i]["Lực dọc"].ToString();
                string Mx1 = dataTable.Rows[i]["MomenX"].ToString();
                string My1 = dataTable.Rows[i]["MomenY"].ToString();
                double a = Cls_Module.Abv*10;
                double Rb = Cls_Module.Rb;
                double Rsc = Cls_Module.Rs;
                

                if (double.TryParse(Cx1, out double Cx)
                    && double.TryParse(Cy1, out double Cy)
                    && double.TryParse(Mx1, out double Mx)
                    && double.TryParse(My1, out double My) && double.TryParse(N1, out double N))
                {
                    double h = Cx;
                    double b = Cy;
                    double h0 = h - a;
                    double za = h0 - a;
                    double M1 = Mx;
                    double M2 = My;
                    double ea = Math.Max(l / 600, h / 30);
                    double eax = Math.Max(l / 600, Cx / 30);
                    double eay = Math.Max(l / 600, Cy / 30);
                    N = Math.Abs(N);
                    Mx = Math.Abs(Mx);
                    My = Math.Abs(My);

                    double lamda = l0 / (0.0288* b);

                    if (Mx/Cx < My/Cy)
                    {
                         h = Cy;
                         b = Cx;
                         h0 = h -a;
                         za = h0 - a;
                         M1 = My;
                         M2 = Mx;
                         ea = Math.Max(l / 600, h / 30);
                         eax = Math.Max(l / 600, Cx / 30);
                         eay = Math.Max(l / 600, Cy / 30);
                        ea = eay + 0.2 * eax;
                    }   
                    else
                    {
                         h = Cx;
                         b = Cy;
                         h0 = h - a;
                         za = h0 - a;
                         M1 = Mx;
                         M2 = My;
                         ea = Math.Max(l / 600, h / 30);
                         eax = Math.Max(l / 600, Cx / 30);
                         eay = Math.Max(l / 600, Cy / 30);
                        ea = eax + 0.2 * eay;
                    }
                    double x1 = N / Rb * b;
                    double m0 = (1 - 0.6 * x1) / h0;
                    if (x1 <= h0)
                    {
                         m0 = (1 - 0.6 * x1) / h0;
                    }
                    else
                    {
                         m0 = 0.4;
                    }
                    double M = M1 + m0 * M2 * (h / b);
                    double e1 = M / N;
                    double e0 = Math.Max(ea, e1);
                    double ee = e0 / h0;

                    double ye = 1 / ((0.5 - ee) * (2 + ee));
                    double phi = 1.028 - 0.0000288 * lamda * lamda - 0.0016 * lamda;
                    double phie = phi + ((1 - phi) * ee) / 0.3;
                    double As = (((ye * N) / phie) - Rb * b * h) / (Rsc - Rb);
                    if (ee <= 0.3)
                    {
                         ye = 1 / ((0.5 - ee) * (2 + ee));
                         phi = 1.028 - 0.0000288 * lamda * lamda - 0.0016 * lamda;
                         phie = phi + ((1 - phi) * ee) / 0.3;
                         As = (((ye * N*10) / phie) - Rb*10 * b/10 * h/10) / (Rsc*10 - Rb*10);
                    }    
                    else
                    {
                        double eee = e0 + (h / 2) - a;
                         As = (N*10 * (eee + 0.5 * x1 - h0/10)) / (0.4 * Rsc*10 * za/10);
                    }
                    dataTable.Rows[i]["As (mm2)"] = Math.Round(As, 2);

                    string As_str = dataTable.Rows[i]["As (mm2)"].ToString();
                    string b_str = dataTable.Rows[i]["Bề rộng"].ToString();
                    string h_str = dataTable.Rows[i]["Chiều cao"].ToString();

                    if (double.TryParse(As_str, out double As1)
                        && double.TryParse(b_str, out double b1)
                        && double.TryParse(h_str, out double h1))
                    {
                        bool conditionMet = false;
                        for (int k1 = 1; k1 <= 10; k1++)//duong kinh thep
                        {
                            double As_thanhthep_doc = 0;
                            for (int k = 2; k <= 30; k++)//sl thanh
                            {
                                string d = "";
                                if (k1 == 1) { As_thanhthep_doc = 5 * 5 * 3.14; d = "f10"; }
                                else if (k1 == 2) { As_thanhthep_doc = 6 * 6 * 3.14; d = "f12"; }
                                else if (k1 == 3) { As_thanhthep_doc = 7 * 7 * 3.14; d = "f14"; }
                                else if (k1 == 4) { As_thanhthep_doc = 8 * 8 * 3.14; d = "f16"; }
                                else if (k1 == 5) { As_thanhthep_doc = 9 * 9 * 3.14; d = "f18"; }
                                else if (k1 == 6) { As_thanhthep_doc = 10 * 10 * 3.14; d = "f20"; }
                                else if (k1 == 7) { As_thanhthep_doc = 11 * 11 * 3.14; d = "f22"; }
                                else if (k1 == 8) { As_thanhthep_doc = 12 * 12 * 3.14; d = "f24"; }
                                else if (k1 == 9) { As_thanhthep_doc = 13 * 13 * 3.14; d = "f26"; }
                                else if (k1 == 10) { As_thanhthep_doc = 14 * 14 * 3.14; d = "f28"; }
                                //Tổng diện tích cốt thép dọc phải lớn hơn hoặc bằng một phần diện tích mặt cắt của cột (b1 * h1), với hệ số 0.2%
                                if (As_thanhthep_doc * k > As && As_thanhthep_doc*k >= (0.2 / 100) * b1 * h1)
                                {
                                    dataTable.Rows[i]["Đề xuất"] = k + " " + d;
                                    dataTable.Rows[i]["As đề xuất (mm2)"] = Math.Round(k * As_thanhthep_doc, 2);
                                    conditionMet = true; //true khi điều kiện đáp ứngS
                                    break;
                                }
                            }
                            if (conditionMet)
                            {
                                break; 
                            }
                        }
                    }



                }

            }

            return dataTable;
        }

        public static string ReadExcelCell(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Frame Sec Def - Conc Rect"];
                return worksheet.Cells[4, 2].Text; // Đọc giá trị từ dòng 4, cột 2
            }
        }

    }
}
