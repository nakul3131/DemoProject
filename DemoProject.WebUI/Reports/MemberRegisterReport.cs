using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace DemoProject.WebUI.Reports
{
    public partial class MemberRegisterReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MemberRegisterReport()
        {
            InitializeComponent();
        }

        private void xrRichText31_BeforePrint(object sender, CancelEventArgs e)
        {
            //var transactionDate = Convert.ToDateTime(Parameters["TransactionDate"].Value);
            //int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            //long customerAccountPrmKey = Convert.ToInt64(Parameters["CustomerAccountPrmKey"].Value);
            //((XRRichText)sender).Text = transactionDate.ToShortDateString();
            //if (LanguagePrmkey == 2 || customerAccountPrmKey != 0)
            //{
            //    string result = new String(transactionDate.ToString("dd-MM-yyyy")
            //    .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
            //    ((XRRichText)sender).Text = result;
            //}
        }

        private void xrRichText33_BeforePrint(object sender, CancelEventArgs e)
        {
            //var applicationDate = Convert.ToDateTime(Parameters["ApplicationDate"].Value);
            //int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            //long customerAccountPrmKey = Convert.ToInt64(Parameters["CustomerAccountPrmKey"].Value);
            //((XRRichText)sender).Text = applicationDate.ToShortDateString();
            //if (LanguagePrmkey == 2 || customerAccountPrmKey != 0)
            //{
            //    string result = new String(applicationDate.ToString("dd-MM-yyyy")
            //    .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
            //    ((XRRichText)sender).Text = result;
            //}

        }

        private void xrRichText34_BeforePrint(object sender, CancelEventArgs e)
        {
            //var allotmentDate = Convert.ToDateTime(Parameters["AllotmentDate"].Value);
            //int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            //long customerAccountPrmKey = Convert.ToInt64(Parameters["CustomerAccountPrmKey"].Value);
            //((XRRichText)sender).Text = allotmentDate.ToShortDateString();
            //if (LanguagePrmkey == 2 || customerAccountPrmKey != 0)
            //{
            //    string result = new String(allotmentDate.ToString("dd-MM-yyyy")
            //    .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());
            //    ((XRRichText)sender).Text = result;
            //}
        }
    }
}
