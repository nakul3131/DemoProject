using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraPrinting;
using DevExpress.Printing.ExportHelpers;
using System.IO;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraPrinting.Native;


namespace DemoProject.WebUI.Reports
{
    public partial class SharesApplicationReport : DevExpress.XtraReports.UI.XtraReport
    {
       
        public SharesApplicationReport()
        {
            InitializeComponent();
        }

        private void xrRichText3_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrRichText3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular);
        }

        
    }
        
}
