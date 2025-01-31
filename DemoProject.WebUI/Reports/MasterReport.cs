using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace DemoProject.WebUI.Reports
{
    public partial class MasterReport : DevExpress.XtraReports.UI.XtraReport
    {

        public MasterReport()
        {
            InitializeComponent();
        }
        
        private void MasterReport_BeforePrint(object sender, CancelEventArgs e)
        {

            //MasterReport report = xrSubreport1.ReportSource as MasterReport;
            //report.Parameters["CategoryID"].Value = Parameters["CategoryID"].Value;

            //bool addPageBreak = report.ReportHieght <= (PageHeight - Margins.Top - Margins.Bottom);
            //xrPageBreak1.Visible = addPageBreak;
            //var variable1 = ((SubReport)xrSubreport1.ReportSource);
            //int t= Convert.ToInt32(variable1.GetCurrentColumnValue("TransactionNumber"));

            //var variable2 = ((SubReport)xrSubreport2.ReportSource);
            //int t1 = Convert.ToInt32(variable2.GetCurrentColumnValue("TransactionNumber"));

            //var key1 = GetCurrentColumnValue();
            //var key2 = GetCurrentColumnValue(grid.KeyFieldName);

            //if (key1 == key2)
            //{
            //    e.Handled = false;
            //    return;
            //}
            ///* custom sorting */
            //e.Handled = true;
            //if (key1.Equals(key))
            //    e.Result = (e.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? -1 : 1);
            //else if (key2.Equals(key))
            //    e.Result = (e.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? 1 : -1);
            //else
            //    e.Handled = false; /* default sorting */


            //GroupFooter1.FindControl("xrlabel3", true).ExpressionBindings
            //        .Add(new ExpressionBinding()
            //        {
            //            EventName = "BeforePrint",
            //            PropertyName = "Text",
            //            Expression = "FormatString('{0:0}', sumRecordNumber([PrmKey]))"
            //        });

            //CreateDataGroupingReport();
        }

        private void Logo_BeforePrint(object sender, CancelEventArgs e)
        {
            string imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Reportimages/download.jpg");
            // Convert image to byte array
            byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
            //Convert byte arry to base64string
            string imreBase64Data = Convert.ToBase64String(byteData);
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(imreBase64Data));
            //(sender as XRPictureBox).Sizing= ImageSizeMode.AutoSize;
            (sender as XRPictureBox).Image = System.Drawing.Image.FromStream(stream);
        }

        private void xrSubreport3_BeforePrint(object sender, CancelEventArgs e)
        {

            ((XRSubreport)sender).ReportSource.Parameters[4].Value = true;

        }

        private void xrSubreport4_BeforePrint(object sender, CancelEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters[4].Value = false;


        }
    }
}
