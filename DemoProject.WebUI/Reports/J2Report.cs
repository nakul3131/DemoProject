﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace DemoProject.WebUI.Reports
{
    public partial class J2Report : DevExpress.XtraReports.UI.XtraReport
    {
        public J2Report()
        {
            InitializeComponent();
        }

        private void xrRichText6_BeforePrint(object sender, CancelEventArgs e)
        {
            //var categoryDate = Convert.ToDateTime(Parameters["CategoryDate"].Value);
            //int LanguagePrmkey = Convert.ToInt32(Parameters["LanguagePrmkey"].Value);

            //((XRRichText)sender).Text = categoryDate.ToShortDateString();

            //if (LanguagePrmkey == 2)
            //{
            //    string result = new string(categoryDate.ToString("dd-MM-yyyy")
            //    .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());

            //    ((XRRichText)sender).Text = result;
            //}
        }
    }
}
