using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Web.UI.WebControls;
using System.Web;
using DevExpress.Persistent.Base;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using ImageSizeMode = DevExpress.XtraPrinting.ImageSizeMode;
using System.Net.Http;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using System.IO;
using System.Reflection;
using System.Text;
using DevExpress.XtraReports.Configuration;
using System.Text.RegularExpressions;
using System.Linq;
using DevExpress.XtraPrinting.Native;
using DevExpress.DocumentView;
using System.Data;
using DevExpress.DataAccess.Sql.DataApi;
using DevExpress.DataAccess.Sql;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Configuration;
using System.Web.Mvc;
using System.Globalization;
using DevExpress.XtraPrinting.NativeBricks;

namespace DemoProject.WebUI.Reports
{
    public partial class BalanceListReport : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public BalanceListReport()
        {
            //BalanceListReport balanceListReport = new BalanceListReport();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            InitializeComponent();
            xrTableCell6.Text = PaperKind.ToString();
            xrTable3 = new XRTable { LocationF = new System.Drawing.PointF(0F, 0F) };
            ///int number1 = 0;

            // Create a calculated field 
            // and add it to the report's collection.
            //CalculatedField calcField = new CalculatedField();
            //this.CalculatedFields.Add(calcField);

            //// Specify the calculated field's properties.
            //calcField.DataSource = this.DataSource;
            //calcField.DataMember = this.DataMember;
            //calcField.FieldType = FieldType.Int32;
            //calcField.DisplayName = "Calculated Field";
            //calcField.Name = "myField";
            //calcField.Expression = "[SrNo]";
            //calcField.GetValue += new GetValueEventHandler(calculatedField4_GetValue);
            //for (int i = 0; i < srno.Count(); i++)
            //{

            //    //  

            //string result = new String("1"
            //.Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c)
            //.ToArray());
            //char ch = char.Parse(result);
            //int intVal = (int)Char.GetNumericValue(ch);

            //int intValue = Convert.ToInt32(result.Replace(@"\x", "0x"), 16);
            //int code = int.Parse(result, System.Globalization.NumberStyles.HexNumber);
            // int numVal = Int32.Parse(t.ToString());
            //string val = "F0005";
            //var no = Int16.Parse(result);
            //result = (int.Parse(result) + 1).ToString();
            //int i1 = (int.Parse(str_Val) + 1).ToString();
            //int i = int.TryParse(result, out int x) ? x : 0;
            // Bind the label's Text property to the calculated field.
            //this.FindControl("xrlabel5", true).ExpressionBindings
            //        .Add(new ExpressionBinding()
            //        {
            //            EventName = "BeforePrint",
            //            PropertyName = "Text",
            //            Expression = "FormatString('{0:0}', sumRecordNumber('" + number1 + 1 + "'))"
            //        });
            ////}
            //number1++;
            //for (int i = 0; i < srno.Count(); i++)
            //{
            //    Report.FindControl("xrlabel2", true).ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", srno[i]));
            //}
            //Summary="Group"


            //Report.FindControl("xrlabel2", true).ExpressionBindings
            //        .Add(new ExpressionBinding()
            //        {
            //            EventName = "BeforePrint",
            //            PropertyName = "Text",
            //            Expression = "FormatString('{0}', sumRecordNumber['" + result + "'])",
            //            //Summary="Group"

            //        });


            //string result = new String(DateTime.Now.ToString("dd-MM-yyyy hh:mm tt")
            //    .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c)
            //    .ToArray());
            //xrRichText23.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "Accounte"));



            //        // Create a report with the page info.
            //        BalanceListReport report = new BalanceListReport()
            //        {
            //            Bands = {
            //          new DetailBand() {
            //        HeightF = 15,
            //        Controls = {pageInfo}
            //    }
            //}
            //        };


        }

        int PageNumber = 0;
        int numval =0;
        List<double> PageTotal = new List<double>();
        List<decimal> PageTotalAmount = new List<decimal>();
        List<string> Total = new List<string>();
        List<string> srno = new List<string>();
        private void BalanceList_BeforePrint(object sender, PrintEventArgs e)
        {
            xrTableCell31.Visible = false;
            xrLabel3.CanPublish = false;
            CreateDataGroupingReport();
        }

        public BalanceListReport CreateDataGroupingReport()
        {

            // Create a report.
            BalanceListReport report = new BalanceListReport();
            var GroupBy = Convert.ToString(Parameters["GroupBy"].Value) ?? string.Empty;
            var SortBy = Convert.ToString(Parameters["SortBy"].Value) ?? string.Empty;
            bool OrderByIs = Convert.ToBoolean(Parameters["IsAscending"].Value);
            // Create the Detail band and add it to the report.
            GroupField groupFieldSortBy = new GroupField();
            groupFieldSortBy.FieldName = SortBy;
            XRRichText xRRichText = new XRRichText();
            //xrRichText23.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "Accounte"));
            if (OrderByIs == true)
            {
                groupFieldSortBy.SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
            }
            else
            {
                groupFieldSortBy.SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
            }
            Detail.SortFields.Add(groupFieldSortBy);
            // Set for dynamic group by for each GroupField 
            GroupField groupField = new GroupField(GroupBy);
            GroupHeader1.GroupFields.Add(groupField);

            // Specify label bindings.
            xrRichText27 = new XRRichText { TopF=50F,WidthF = 200F, Font = new Font("Segoe UI", 12f, FontStyle.Bold | FontStyle.Bold), ForeColor = System.Drawing.Color.Black, LocationF = new System.Drawing.PointF(20F, 10F) };
            
            if (GroupBy == "MemberTypePrmkey")
            {
                xrRichText27.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[MemberType]"));
            }
            if (GroupBy == "GenderPrmKey")
            {
                xrRichText27.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[Gender]"));
            }

            if (GroupBy == "SchemePrmKey")
            {
                xrRichText27.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[Scheme]"));
            }
            if (GroupBy == "")
            {
                //xrTable3.ProcessHiddenCellMode = ProcessHiddenCellMode.ResizeCellsProportionally;
                xrTable3.ProcessHiddenCellMode = ProcessHiddenCellMode.StretchNextCell;
                xrTableCell31.Visible = true;

            }
            else
            {

                GroupHeader1.Controls.Add(xrRichText27);
            }
            return report;
        }

        private void total_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            
            if (e.Value != null)
            {
                PageTotal.Add(PageNumber);

                PageTotalAmount.Add((decimal)e.Value);
                
            }
            PageNumber++;
        }

        private void ReportFooter_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            //XRPageInfo pageInfo = new XRPageInfo()
            //{


            //    PageInfo= PageInfo.NumberOfTotal,
            //    StartPageNumber = 1,
            //    Format = "Page {0} of {1}",
            //    //PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime,

            //    WidthF = 250,
            //    // Specify a format string for the control's page info.
            //    //TextFormatString = "{0:MMMM d, yyyy}"
            //};



            //xrRichText23.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", ));
            //ReportFooter.Controls.Add(CreateXRPageInfo());
            ReportFooter.Controls.Add(CreateXRTable());
        }

        public XRPageInfo CreateXRPageInfo(int num,int num2)
        {
            //XRPageInfo pageInfo1 = new XRPageInfo
            //{
            //    SizeF = new SizeF(200F, 50F),
            //    BackColor = Color.PaleGreen,
            //    PageInfo = PageInfo.DateTime,
            //    Format = "{0:MM/dd/yyyy}",
            //    StartPageNumber = 2
            //};

            XRPageInfo pageInfo = new XRPageInfo()
            {


                PageInfo = PageInfo.NumberOfTotal,
                StartPageNumber = 1,
                Format = "Page {0} of {1}",
                //PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime,

                WidthF = 250,
                // Specify a format string for the control's page info.
                //TextFormatString = "{0:MMMM d, yyyy}"
            };
            return pageInfo;
        }

        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(198F, 100F);
        }

        public XRTable CreateXRTable()
        {

            int pnumber = 0;
            // Create an empty table and set its size.
            XRTable table = new XRTable();
            table.Size = new Size(100, 100);
            table.Borders = BorderSide.All;
            table.Borders = BorderSide.Bottom;
            table.KeepTogether = true;
            float rowsHeightF = 37F;
             //this.ParagraphPropertiesBase
            //// Enable table borders to see its boundaries.
            table.BorderWidth = 2;
            //table.Borders = BorderSide.All;
            int numRows = PageTotal.Count - 1;
            int rowNumber = 0;
            table.BeginInit();
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            for (int i = -1; i <= numRows; i++)
            {
                rowNumber = i + 1;

                XRTableRow row = new XRTableRow();
                table.Rows.Add(row);
                // Create table cells.
                XRTableCell cell1 = new XRTableCell();
                XRTableCell cell2 = new XRTableCell();
                cell1.KeepTogether = true;
                cell2.KeepTogether = true;
                if (i >= 0)
                {
                    if (LanguagePrmkey == 1)
                    {
                        pnumber = Convert.ToInt32(PageTotal[i]) + 1;
                        cell1.Text = pnumber.ToString();
                        cell2.Text = PageTotalAmount[i].ToString();
                        cell1.Font = new Font("Segoe UI", 12f, FontStyle.Bold | FontStyle.Bold);
                        cell2.Font = new Font("Segoe UI", 12f, FontStyle.Bold | FontStyle.Bold);
                        cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                        cell2.Borders = BorderSide.Top;
                        cell1.Width = 220;
                        cell2.Width = 220;

                        table.Rows[rowNumber].Cells.Add(cell1);
                        table.Rows[rowNumber].Cells.Add(cell2);
                        row.HeightF = rowsHeightF * 1;
                        row.BorderWidth = 2;
                        row.Borders = BorderSide.All;
                        table.Rows.Add(row);
                       
                    }
                    else
                    {
                        pnumber = Convert.ToInt32(PageTotal[i]) + 1;
                        var pnumbermarathi = configurationDetailRepository.TranslateNumberInRegionalLanguage(pnumber, LanguagePrmkey).ToString();
                        var Totalmarathi = configurationDetailRepository.TranslateNumberInRegionalLanguage(PageTotalAmount[i], LanguagePrmkey).ToString();
                        string[] number = pnumbermarathi.Split('.');
                        cell1.Text = number[0].ToString();
                        cell2.Text = Totalmarathi.ToString();
                        cell1.Font = new Font("Segoe UI", 12f, FontStyle.Bold | FontStyle.Bold);
                        cell2.Font = new Font("Segoe UI", 12f, FontStyle.Bold | FontStyle.Bold);
                        cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                        cell1.Width = 220;
                        cell2.Width = 220;
                        table.Rows[rowNumber].Cells.Add(cell1);
                        table.Rows[rowNumber].Cells.Add(cell2);
                        row.HeightF = rowsHeightF * 1;
                        row.BorderWidth = 2;
                        row.Borders = BorderSide.All;
                        table.Rows.Add(row);
                        
                    }
                }
                else
                {
                    if (LanguagePrmkey == 1)
                    {
                        
                        cell1.Text = "Page Number";
                        cell2.Text = "Page Total Amount";
                        cell1.Font = new Font("Segoe UI", 14f, FontStyle.Bold | FontStyle.Bold);
                        cell2.Font = new Font("Segoe UI", 14f, FontStyle.Bold | FontStyle.Bold);
                        cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell1.Width = 220;
                        cell2.Width = 220;
                        table.Rows[rowNumber].Cells.Add(cell1);
                        table.Rows[rowNumber].Cells.Add(cell2);
                        cell1.BackColor = Color.WhiteSmoke;
                        cell2.BackColor = Color.WhiteSmoke;
                        row.HeightF = rowsHeightF * 1;
                        row.BorderWidth = 2;
                        row.Borders = BorderSide.All;
                        table.Rows.Add(row);
                        
                    }
                    else
                    {
                        cell1.Text = "पान क्रमांक";
                        var xrTableCellText = cell1.Text;
                        cell1.Controls.Add(new XRRichText
                        {
                            Html = xrTableCellText,
                            Borders = DevExpress.XtraPrinting.BorderSide.None,
                            Font = new Font("Segoe UI", 14f, FontStyle.Bold | FontStyle.Bold),
                            //WidthF = cell1.WidthF - cell1.Padding.Left - cell1.Padding.Right,
                            //HeightF = xrTableCell.HeightF,
                            Width = 220,
                            TopF = 12F,
                            LeftF = 50F,
                            CanGrow = true,
                            CanPublish = true
                        });
                        

                        cell2.Text = "पान एकूण रक्कम";
                        var xrTableCellText2 = cell2.Text;
                        cell2.Controls.Add(new XRRichText
                        {

                            Html = xrTableCellText2,
                            Borders = DevExpress.XtraPrinting.BorderSide.None,
                            WidthF = cell2.WidthF - cell2.Padding.Left - cell2.Padding.Right,
                            //HeightF = xrTableCell.HeightF,
                            Font = new Font("Segoe UI", 14f, FontStyle.Bold | FontStyle.Bold),
                            TopF = 12F,
                            Width = 220,
                            LeftF =50F,
                            CanGrow = true,
                            CanPublish = true
                        });
                        //xrTableCell.Text = null;
                        //XRRichText richtext = new XRRichText();
                        //richtext.Text = "पान क्रमांक";
                        //richtext.Location = new Point(0, 0);
                        //richtext.Size = cell1.Size;
                        //cell1.Controls.Add(richtext);
                        table.Rows[rowNumber].Cells.Add(cell1);
                        //XRRichText richtext1 = new XRRichText();
                        //richtext1.Text = "पान एकूण रक्कम";
                        //richtext1.Location = new Point(0, 0);
                        //richtext1.Size = cell2.Size;
                        //cell2.Controls.Add(richtext1);
                        //xrTableRow1.InsertCell(cell1, 3);
                        table.Rows[rowNumber].Cells.Add(cell2);
                        //cell1.Text = "पान क्रमांक";
                        //cell2.Text = "पान एकूण रक्कम";
                        cell1.Font = new Font("Segoe UI", 14f, FontStyle.Bold | FontStyle.Bold);
                        cell2.Font = new Font("Segoe UI", 14f, FontStyle.Bold | FontStyle.Bold);
                        cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        cell1.Width = 220;
                        cell2.Width = 220;
                     
                        //table.Rows[rowNumber].Cells.Add(cell2);
                        cell1.BackColor = Color.WhiteSmoke;
                        cell2.BackColor = Color.WhiteSmoke;
                        row.HeightF = rowsHeightF * 1;
                        row.BorderWidth = 2;
                        row.Borders = BorderSide.All;
                        table.Rows.Add(row);
                    }
                }
                

            }
            // Set table size.
            //table.HeightF = 50;
            //table.WidthF = 500;
            //table.BeforePrint += new PrintEventHandler(table_BeforePrint);
            table.AdjustSize();
            // Finish table initialization.
            //table.EndInit();
            table.Borders = BorderSide.All;
            //XRTable table = new XRTable();
            //detailBand.Controls.Add(table);

            //table.BeginInit();
            //XRTableRow row = new XRTableRow();
            //table.Rows.Add(row);
            //XRTableCell categoryName = new XRTableCell();
            //XRTableCell productName = new XRTableCell();
            //XRTableCell supplierID = new XRTableCell();

            //row.Cells.Add(categoryName);
            //row.Cells.Add(productName);
            //row.Cells.Add(supplierID);

            //categoryName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[CategoryName]"));
            //productName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[ProductName]"));
            //supplierID.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[SupplierID]"));
            //private SqlDataSource BindToData()
            //{
            //    SqlDataSource ds = new SqlDataSource("nwind");
            //    SelectQuery query = SelectQueryFluentBuilder
            //        .AddTable("CategoryProducts")
            //        .SelectColumns(new string[] { "CategoryName", "ProductName", "SupplierID" })
            //        .Filter("[CategoryName] = '" + comboBoxEdit1.EditValue.ToString() + "'")
            //        .Build("MyQuery");
            //    ds.Queries.Add(query);
            //    ds.Fill();
            //    return ds;
            //}
            table.EndInit();
            return table;
        }
        
        private void xrTableCell1_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            xrLabel6.Value = e.PageCount;
            int PageCount = e.PageCount - PageNumber;
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (e.PageCount - PageCount == PageNumber)
            {

                xrTableCell1.Text = Convert.ToString(PageNumber);
                if (LanguagePrmkey==2)
                {
                    var result = configurationDetailRepository.TranslateNumberInRegionalLanguage(PageNumber, LanguagePrmkey).ToString();
                    string[] pagenumber = result.Split('.');
                    xrTableCell1.Text = Convert.ToString(pagenumber[0]);
                }
                

            }

        }

        private void xrTableCell3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

            int PageCount = e.PageCount - PageNumber;
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (e.PageCount - PageCount == PageNumber)
            {
                xrTableCell3.Text = PageTotalAmount[PageTotalAmount.Count - 1].ToString();

                if (LanguagePrmkey == 2)
                {
                    var result = configurationDetailRepository.TranslateNumberInRegionalLanguage(PageTotalAmount[PageTotalAmount.Count - 1], LanguagePrmkey).ToString();
                    xrTableCell3.Text = Convert.ToString(result);
                }
            }
        }
        
        private void xrTableCell4_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value != null)
            {
                Total.Add(e.Value.ToString());
            }
        }

        private void xrTableCell4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
          
            if (LanguagePrmkey == 2)
            {
                string result = new String(Total[0].ToString()
                .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c)
                .ToArray());
                xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                ((XRTableCell)sender).Text = result;
            }
            
        }

        private void xrTableCell1_BeforePrint(object sender, PrintEventArgs e)
        {
            xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        }

        private void xrTableCell2_BeforePrint(object sender, PrintEventArgs e)
        {
            xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

        }

        private void xrTableCell3_BeforePrint(object sender, PrintEventArgs e)
        {
            xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

        }

        private void xrRichText21_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            string amountInWords = "";
            if (PageTotalAmount.Count > 0)
            {
                decimal amount = PageTotalAmount[e.PageIndex];
                double amt = (double)amount;
                if (LanguagePrmkey == 1)
                {
                     amountInWords = NumberToWords(amt);
                }
                else
                {
                    amountInWords = funNumToWordConvert(amt);
                }
            }
                ((XRRichText)sender).Font = new Font("Segoe UI", 9f, FontStyle.Bold | FontStyle.Bold);


                ((XRRichText)sender).Text = amountInWords;
            }

        public string NumberToWords(double doubleNumber)
        {

            int index = doubleNumber.ToString().IndexOf(".");
            string returnvalue;
            var beforeFloatingPoint = (int)Math.Floor(doubleNumber);
            var beforeFloatingPointWord = $"{convert_number(beforeFloatingPoint)} Ruppes";
            var afterFloatingPointWord = $"{SmallNumberToWord((int)((doubleNumber - beforeFloatingPoint) * 100), "")} Paise";

            if (index <= 0)
            {
                afterFloatingPointWord = "";
                returnvalue = $"₹-({beforeFloatingPointWord} Only)";
            }
            else
            {
                returnvalue = $"₹-({beforeFloatingPointWord} And {afterFloatingPointWord} Only)";
            }

            return returnvalue;
        }

        public  string funNumToWordConvert(double number)
        {
            
            //number = decimal.Round(number, 2);
            string wordNumber = string.Empty;
            //number = 556.23;
            string[] arrNumber = number.ToString().Split('.');

            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = funConvert(wholePart);

            if (number == wholePart)
            {
                return $"₹-( {strWholePart} रुपये )";
            }
            else
            {
                wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? "" : " रुपये आणि ");

                // If the array has more than one element then there is a fractional part otherwise there isn't
                // just add 'No Cents' to the end
                if (arrNumber.Length > 1)
                {
                    // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,
                    // 'One', 'Two', etc but 'Ten', 'Twenty', etc.
                    long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
                    string strFarctionPart = funConvert(fractionPart);

                    wordNumber += (fractionPart == 0 ? " No" : strFarctionPart) + (fractionPart == 1 ? " Cent" : " पैसे");
                    wordNumber = $"₹-( {wordNumber} )";
                }
                else
                    wordNumber += "No Cents";
            }

            return wordNumber;
        }

        public string funConvert(double num)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            List<string> str = configurationDetailRepository.NumberInWordsDropdownList(LanguagePrmkey);
            string[] HigherDigitHindiNumberArray = str[0].Split(',').Select(a => a.Trim()).ToArray();
            string[] HundredHindiDigitArray = str[1].Split(',').Select(a => a.Trim()).ToArray();
            string[] SouthAsianCodeArray = str[2].Split(',').Select(a => a.Trim()).ToArray();
            string amt = num.ToString();

            if (amt == "0")
            {
                return "शून्य";
            }

            int[] amountArray;
            amountArray = new int[amt.Length];
            for (int i = amountArray.Length; i >= 1; i += -1)
            {
                amountArray[i - 1] = int.Parse(amt.Substring(i - 1, 1));
            }

            int j = 0;
            int digit = 0;
            string result = "";
            string separator = "";
            string higherDigitHindiString = "";
            string codeIndex = "";

            for (int i = amountArray.Length; i >= 1; i += -1)
            {
                j = amountArray.Length - i;
                digit = amountArray[j];

                codeIndex = SouthAsianCodeArray[i - 1];

                higherDigitHindiString = HigherDigitHindiNumberArray[Int32.Parse((codeIndex.Substring(0, 1))) - 1];

                if (codeIndex == "1")
                {
                    result = result + separator + HundredHindiDigitArray[digit];

                    //Number in tenth place and skip if digit is 0
                }

                else if (codeIndex.Length == 2 & digit != 0)
                {
                    int suffixDigit = amountArray[j + 1];
                    int wholeTenthPlaceDigit = digit * 10 + suffixDigit;

                    result = result + separator + HundredHindiDigitArray[wholeTenthPlaceDigit] + higherDigitHindiString;
                    i -= 1;

                    //Standard Number like 100, 1000, 1000000 and skip if digit is 0
                }

                else if (digit != 0)
               {
                    result = result + separator + HundredHindiDigitArray[digit] + higherDigitHindiString.TrimStart();
                }
                separator = " ";
            }

            return result;
        }

        private string SmallNumberToWord(int number, string words)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            BalanceListReport balanceListReport = new BalanceListReport();
            if (number <= 0) return words;
            if (words != "")
                words += " ";
            string[] unitsMap;
            string[] tensMap;
               unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
               tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            //unitsMap = new[] { "शून्य", "एक", "दोन", "तीन", "चार", "पाच", "सहा", "सात", "आठ", "नऊ", "दहा", "अकरा", "बारा", "तेरा", "चौदा", "पंधरा", "सोळा", "सतरा", "अठरा", "एकोणीस" };
                //tensMap = new[] { "शून्य", "दहा", "वीस","तीस", "चाळीस", "पन्नास", "साठ", "सत्तर", "ऐंशी", "नव्वद" };
                //tensMap = new[] { "शून्य", "दहा", "वीस", "तीस", "चाळीस", "पन्नास", "साठ", "सत्तर", "ऐंशी", "नव्वद" };
            

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words;
        }

        public string convert_number(int number)
        {
            
                var res = "";
                if ((number < 0) || (number > 999999999))
                {
                    return "NUMBER OUT OF RANGE!";
                }
                var Gn = Math.Abs(number / 10000000);  /* Crore */
                number -= Gn * 10000000;
                var kn = Math.Abs(number / 100000);     /* lakhs */
                number -= kn * 100000;
                var Hn = Math.Abs(number / 1000);      /* thousand */
                number -= Hn * 1000;
                var Dn = Math.Abs(number / 100);       /* Tens (deca) */
                number = number % 100;               /* Ones */
                var tn = Math.Abs(number / 10);
                var one = Math.Abs(number % 10);
               

                if (Gn > 0)
                {
                    res += (convert_number(Gn) + " Crore");
                }
                if (kn > 0)
                {
                    res += (((res == "") ? "" : " ") +
                    convert_number(kn) + " Lakh");
                }
                if (Hn > 0)
                {
                    res += (((res == "") ? "" : " ") +
                        convert_number(Hn) + " Thousand");
                }

                if (Dn > 0)
                {
                    res += (((res == "") ? "" : " ") +
                        convert_number(Dn) + " Hundred");
                }
                res = SmallNumberToWord(number, res).ToString();
            
            return res;
        }

        private void xrLabel9_BeforePrint_1(object sender, PrintEventArgs e)
        {

            if (HttpContext.Current.Session["Username"] == null)
            {
                ((XRLabel)sender).Text = "";
            }
            else
            {
                ((XRLabel)sender).Text = HttpContext.Current.Session["Username"].ToString();
            }

        }

        private void xrPictureBox1_BeforePrint(object sender, PrintEventArgs e)
        {

            string imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/ReportImg/download.jpg");
            // Convert image to byte array
            byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
            //Convert byte arry to base64string
            string imreBase64Data = Convert.ToBase64String(byteData);
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(imreBase64Data));
            //(sender as XRPictureBox).Sizing= ImageSizeMode.AutoSize;
            (sender as XRPictureBox).Image = System.Drawing.Image.FromStream(stream);

        }

        private void xrRichText4_BeforePrint(object sender, PrintEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            ((XRRichText)sender).Text = DateTime.Now.ToShortDateString();
            if (LanguagePrmkey == 2)
            {
                string result = new String(DateTime.Now.ToString("dd-MM-yyyy hh:mm tt")
                .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c)
                .ToArray());
                xrRichText4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
                ((XRRichText)sender).Text = result;
            }
        }

        private void ReportHeader_BeforePrint(object sender, PrintEventArgs e)
        {
           //ReportHeader.Controls.Add(CreateDataGroupingReport());

            //var tt = Convert.ToString(this.GetCurrentColumnValue("Logo"));
        }

        private void BalanceList_AfterPrint(object sender, EventArgs e)
        {
            TableBrick PreviousVisualBrick = null;
            foreach (DevExpress.XtraPrinting.Page page in Pages)
            {
                var iterator = new NestedBrickIterator(page.InnerBricks);
                VisualBrick visualBrick = null;
                DevExpress.XtraPrinting.NativeBricks.TableBrick tableBrick = null;
                while (iterator.MoveNext())
                {

                    visualBrick = iterator.CurrentBrick as VisualBrick;
                    if (visualBrick is TableBrick && VisualBrickOwnerIsInASubReport(visualBrick.BrickOwner))
                    {

                        tableBrick = visualBrick as TableBrick;
                        // check if the page is a continuation of the last table in the previous page
                        if (PreviousVisualBrick != null && PreviousVisualBrick.BrickOwner == tableBrick.BrickOwner)
                        {
                            RowBrick rowBrick = PreviousVisualBrick.Bricks[0] as RowBrick;
                            foreach (BrickBase brick in rowBrick.Bricks)
                                ((VisualBrick)brick).Sides |= BorderSide.Bottom;
                            PreviousVisualBrick = null;
                        }
                    }
                }
                PreviousVisualBrick = tableBrick;



                PrintingSystem.Document.Name = "Balance List";
            }
        }

        private bool VisualBrickOwnerIsInASubReport(IBrickOwner brickOwner)
        {
            throw new NotImplementedException();
        }

        private void xrRichText31_BeforePrint(object sender, PrintEventArgs e)
        {
            xrRichText31.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            xrRichText31.LeftF = 55F;
            xrRichText31.Borders = BorderSide.None;
        }

        private void xrRichText31_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (PageTotalAmount.Count > 0)
            {
                decimal amount = PageTotalAmount[e.PageIndex];
                xrRichText31.Text = amount.ToString();
                if (LanguagePrmkey == 2)
                {
                    var result = configurationDetailRepository.TranslateNumberInRegionalLanguage(amount, LanguagePrmkey).ToString();
                    xrRichText31.Text = result;
                }
            }
        }

        private void xrLabel3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            e.Cancel = true;
        }

        private void xrRichText12_BeforePrint(object sender, PrintEventArgs e)
        {
            var GroupBy = Convert.ToString(Parameters["GroupBy"].Value) ?? string.Empty;
            if(GroupBy=="")
            {
                ((XRRichText)sender).Text = "None";
            }
            if (GroupBy == "MemberTypePrmkey")
            {
                ((XRRichText)sender).Text = "MemberType";

            }
            if (GroupBy == "GenderPrmKey")
            {
                ((XRRichText)sender).Text = "Gender";

            }

            if (GroupBy == "SchemePrmKey")
            {
                ((XRRichText)sender).Text = "Scheme";

            }
            

        }

        private void xrRichText23_BeforePrint(object sender, PrintEventArgs e)
        {
            var SortBy = Convert.ToString(Parameters["SortBy"].Value) ?? string.Empty;
            ((XRRichText)sender).Text = SortBy;
        }

        private void xrRichText24_BeforePrint(object sender, PrintEventArgs e)
        {
            bool OrderByIs = Convert.ToBoolean(Parameters["IsAscending"].Value);
            if (OrderByIs == true)
            {
               ((XRRichText)sender).Text = "Asce";
            }
            else
            {
                ((XRRichText)sender).Text = "Desc";
            }

        }

        private void xrRichText7_BeforePrint(object sender, PrintEventArgs e)
        {   var effectiveDate = Convert.ToDateTime(Parameters["EffectiveDate"].Value);
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            ((XRRichText)sender).Text = effectiveDate.ToShortDateString();
            if (LanguagePrmkey == 2)
            {
                string result = new String(effectiveDate.ToString("dd-MM-yyyy")
                .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
                ((XRRichText)sender).Text =result;
            }
        }

        private void xrLabel2_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value != null)
            {
                
                srno.Add(e.Value.ToString());
            }
        }

        private void xrLabel5_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            
            //BalanceListReport balanceListReport = new BalanceListReport();
            if (e.Value != null)
            {

                //xrLabel5.Value = e.Value.ToString();
                //XRLabel xRLabel = new XRLabel();
                //XRLabel label = Detail.Report.FindControl("xrLabel1", true) as XRLabel;
                //label.BeforePrint += xrLabel2_BeforePrint;

                //numval = Convert.ToInt32(e.Value.ToString());
                //srno.Add(e.Value.ToString());
            }
        }

        private void xrLabel5_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            e.Cancel = true;
            //for (int i = 0; i < srno.Count(); i++)
            //{
            //    //((XRLabel)sender).Text = srno[i];
            //}
                //int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
                //for (int i = 0; i < srno.Count(); i++)
                //{
                //    ((XRLabel)sender).Text = srno[i];
                //    if (LanguagePrmkey == 2)
                //    {
                //        string result = new String(srno[i]
                //        .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());

                //        // Report.FindControl("xrlabel2", true).ExpressionBindings
                //        //.Add(new ExpressionBinding()
                //        //{
                //        //    EventName = "BeforePrint",
                //        //    PropertyName = "Text",
                //        //    Expression = "FormatString('{0}', sumRecordNumber['" + srno[i] + "'])",
                //        //     //Summary="Group"

                //        // });

                //        ((XRLabel)sender).Text = result;
                //    }
                //}
            }

        private void calculatedField4_GetValue(object sender, GetValueEventArgs e)
        {
            e.Value.ToString();
        }

        private void xrLabel6_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            numval = Convert.ToInt32(e.Value.ToString());
        }

        private void xrLabel2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //int str = Convert.ToInt32(xrLabel2.Value);
            //for (int i = 0; i < srno.Count; i++)
            //{
              //int no = Convert.ToInt32(srno[i]);

                //xrLabel2.Text = String.Format("{0:#}", Convert.ToInt32(no));

            //xrLabel2.Summary = new XRSummary(SummaryRunning.None, SummaryFunc.RecordNumber, "{0:#}");
        //}
        }

        private void xrLabel2_BeforePrint(object sender, PrintEventArgs e)
        {
            int str = Convert.ToInt32(xrLabel5.Summary.GetResult());
            //int str1= Convert.ToInt32(xrLabel5.Value);
            //int str = Convert.ToInt32(xrLabel2.Value);
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (LanguagePrmkey == 2)
            {

                //if (str >= 0)
                //{
                    //i = 1;
                    //int no = Convert.ToInt32(str) + str1;
                    string result = new String(str.ToString()
                    .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
                    xrLabel2.Text = String.Format("{0:0}", result);
                    xrLabel2.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.RecordNumber, "{0:0}");
                ///}
                //else
                //{
                //    //int i = srno.Count - numval;
                //    //int ee =numval - 1;
                //    //int no = Convert.ToInt32(srno[i]) + 1;

                //    //xrLabel2.Text = i.ToString();

                //    //for (i = 0; i < srno.Count; i++)
                //    //{
                //    int no = Convert.ToInt32(str) + 1;
                //    string result = new String(no.ToString()
                //   .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
                //    //    //xrLabel2.TopF = 10F;
                //    xrLabel2.TextAlignment = TextAlignment.MiddleCenter;
                //    xrLabel2.Text = result.ToString();
                //    //xrLabel2.Text = String.Format("{0:#}", Convert.ToInt32(result));
                //    xrLabel2.Summary = new XRSummary(SummaryRunning.None, SummaryFunc.RecordNumber, "{0:#}");
                //    //}
                //}

            }
            else
            {
                //if (str2 >= 0)
                //{
                    //i = 1;
                    int no = Convert.ToInt32(str);
                   // string result = new String(no.ToString()
                   //.Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
                   // //xrLabel2.TextAlignment = TextAlignment.MiddleCenter;
                    ///xrLabel2.Text = i.ToString();
                    //int no = Convert.ToInt32(i);
                    xrLabel2.Text = String.Format("{0:0}", no);
                    xrLabel2.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.RecordNumber, "{0:0}");
                //}
                //else
                //{
                //    //int i = srno.Count - numval;
                //    //int ee =numval - 1;
                //    //int no = Convert.ToInt32(srno[i]) + 1;

                //    //xrLabel2.Text = i.ToString();

                //    //for (i = 0; i < srno.Count; i++)
                //    //{
                //    int no = Convert.ToInt32(str) + 1;
                //    //    //xrLabel2.TopF = 10F;
                //    //xrLabel2.TextAlignment = TextAlignment.MiddleCenter;
                //    //xrLabel2.ExpressionBindings.Add(new ExpressionBinding("Text", "sumRecordNumber('" + no + "')"));
                //    //this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                //    //new DevExpress.XtraReports.UI.XRBinding("Text", "FormatString('{0:0})",  "sumRecordNumber('" + no + "')")});
                //    //this.xrLabel2.Dpi = 100F;
                //    //xrLabel2.ExpressionBindings
                //    //.Add(new ExpressionBinding()
                //    //{
                //    //    //EventName = "BeforePrint",
                //    //    PropertyName = "Text",
                //    //    Expression = "FormatString('{0:0}', sumRecordNumber('" + no + "'))"
                //    //});
                //    //this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic);
                //    //this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(225.7918F, 76.74993F);
                //    //this.xrLabel2.Name = "xrLabel2";
                //    //this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
                //    //this.xrLabel2.SizeF = new System.Drawing.SizeF(401.2086F, 20F);
                //    //this.xrLabel2.StylePriority.UseFont = false;
                //    //XRSummary xrSummary1 = new XRSummary();
                //    ////xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
                //    //xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
                //    //this.xrLabel2.Summary = xrSummary1;
                //    //xrLabel2.DataBindings.Add(new XRBinding("Text",null, no.ToString()));
                //    //xrLabel2.Summary = new XRSummary(SummaryRunning.Page, SummaryFunc.RecordNumber,"{0:#)}");
                //    xrLabel2.CanGrow = true;
                //    xrLabel2.WordWrap = true;
                //    //var data = Convert.ToString(Convert.ToDouble(DetailReport.GetCurrentColumnValue("UnitsInStock")));
                //    xrLabel2.Text = String.Format("{0:#}", Convert.ToInt32(no));
                //    xrLabel2.Summary = new XRSummary(SummaryRunning.None, SummaryFunc.RecordNumber, "{0:#}");
                //    ///xrLabel2.Text = no.ToString();
                //    //}
                //}

            }





            //            this.xrLabelProfileCardsCount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "Cards")});
            //            this.xrLabelProfileCardsCount.Dpi = 100F;
            //            this.xrLabelProfileCardsCount.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic);
            //            this.xrLabelProfileCardsCount.LocationFloat = new DevExpress.Utils.PointFloat(225.7918F, 76.74993F);
            //            this.xrLabelProfileCardsCount.Name = "xrLabelProfileCardsCount";
            //            this.xrLabelProfileCardsCount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            //            this.xrLabelProfileCardsCount.SizeF = new System.Drawing.SizeF(401.2086F, 20F);
            //            this.xrLabelProfileCardsCount.StylePriority.UseFont = false;
            //            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            //            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            //            this.xrLabelProfileCardsCount.Summary = xrSummary1;
            //int yy = i-1;
            //xrLabel2.Text = i.ToString();

            // var t = Report.GetCurrentColumnValue(((XRLabel)sender).Value).ToString();
            //for (int i = 0; i < srno.Count(); i++)
            //{
            //    if (srno[i].Count() >=0)
            //    {
            //        xrLabel2.Text = srno[i].ToString();
            //    }

        }

        private void xrRichText35_BeforePrint(object sender, PrintEventArgs e)
        {
            //for (int i = 0; i < srno.Count(); i++)
            //{
            //    if (srno[i].Count() >= 0)
            //    {
            //        ((XRRichText)sender).Text = srno[i].ToString();
            //    }
            //}
        }

        private void xrLabel5_BeforePrint(object sender, PrintEventArgs e)
        {

            //int str = Convert.ToInt32(xrLabel5.Value);
            //int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            //if (LanguagePrmkey == 2)
            //{

            //    if (str >= 0)
            //    {
            //        int no = Convert.ToInt32(str) + 1;
            //        string result = new String(no.ToString()
            //        .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
            //        xrLabel2.TextAlignment = TextAlignment.MiddleCenter;
            //        xrLabel2.Text = result.ToString();
            //        //i = 1;
            //        //int no = Convert.ToInt32(str) + 1;
            //        //string result = new String(no.ToString()
            //        //.Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
            //        //xrLabel2.TextAlignment = TextAlignment.MiddleCenter;
            //        //xrLabel2.Text = no.ToString();
            //        //xrLabel2.Summary = new XRSummary(SummaryRunning.None, SummaryFunc.RecordNumber, "{0:#}");
            //    }
            //}
                ///int? dblValueArray = (sender as XRLabel).RootReport.GetCurrentColumnValue<int?>("xrLabel5");

                //for (int i = 0; i < srno.Count; i++)
                //{
                //    int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
                //    if (LanguagePrmkey == 2)
                //    {

                //        string result = new String(srno[i].ToString()
                //       .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
                //        //var result = configurationDetailRepository.TranslateNumberInRegionalLanguage(Convert.ToInt32(srno[i]), LanguagePrmkey).ToString();
                //        //
                //        //var bytes = Encoding.UTF8.GetBytes(result);
                //        //int t = Convert.ToInt32(bytes);
                //        //xrLabel5.Text = result;
                //        //var xrLabelText =xrLabel5.Text;
                //        //int intVal = CharUnicodeInfo.GetDecimalDigitValue('१');
                //        //xrLabel5.ExpressionBindings.Add(new ExpressionBinding("Text", "sumRecordNumber('" + xrLabelText + "')"));

                //    }
                //    else
                //    {
                //       //xrLabel5.Text = srno[i].ToString();
                //    }
                //}

                //private void XrTableCell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
                //{
                //    var xrTableCell = (XRTableCell)sender as XRTableCell;

                //    var xrTableCellText = xrTableCell.Text;

                //    xrTableCell.Controls.Clear();
                //    xrTableCell.Controls.Add(new XRRichText
                //    {
                //        Html = xrTableCellText,
                //        Borders = DevExpress.XtraPrinting.BorderSide.None,
                //        WidthF = xrTableCell.WidthF - xrTableCell.Padding.Left - xrTableCell.Padding.Right,
                //        HeightF = xrTableCell.HeightF,
                //        CanGrow = true,
                //        CanPublish = true
                //    });

                //    xrTableCell.Text = null;
                //}

            }

        private void xrTableCell31_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //var GroupBy = Convert.ToString(Parameters["GroupBy"].Value) ?? string.Empty;
            //if (GroupBy == "")
            //{
            //    e.Cancel = false;
            //    xrTable3.LocationF = new DevExpress.Utils.PointFloat(10F, 0F);
            //}

        }

        private void xrTableCell31_BeforePrint(object sender, PrintEventArgs e)
        {
            
            //((XRTableCell)sender).Visible = false;
            ///xrTable3.BeforePrint += new PrintEventHandler(xrTable3_BeforePrint);

        }

        private void xrTable3_BeforePrint(object sender, PrintEventArgs e)
        {
            ///xrTable3.ProcessHiddenCellMode = ProcessHiddenCellMode.ResizeCellsProportionally;
            xrTable3.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            //xrTable3.ProcessHiddenCellMode = ProcessHiddenCellMode.LeaveEmptySpace;
            //((XRTableCell)sender).Visible = false;
            //xrTable3.LocationF = new DevExpress.Utils.PointFloat(10F, 0F);
        }

        private void xrPageInfo1_TextChanged(object sender, EventArgs e)
        {
            e.ToString();

        }

        private void xrLabel6_BeforePrint(object sender, PrintEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (LanguagePrmkey == 2)
            {
                for (int i = 0; i <= PageTotal.Count; i++)
                {
                    int pnumber = i + 1;
                    if (pnumber > 0)
                    {

                        string pagenumber = new String(pnumber.ToString()
                       .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());

                        ((XRLabel)sender).Text = "पान क्रमांक  " + pagenumber + "  पैकी  ";

                    }
                }
            }
        }

        private void xrLabel6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (LanguagePrmkey == 2)
            {
                var str = xrLabel6.Text;
                string pagetotal = new String(e.PageCount.ToString()
               .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());

                ((XRLabel)sender).Text = str + pagetotal;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void xrPageInfo1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            if (LanguagePrmkey == 2)
            {
                e.Cancel = true;
            }
        }
    }
}

