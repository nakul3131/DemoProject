﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.Data.Filtering;
using DevExpress.Data;
using System.Linq;
using DevExpress.DataProcessing;
using DevExpress.Data.Linq.Helpers;
using System.Web.Configuration;
using System.Collections.Generic;
using DevExpress.XtraRichEdit;

namespace DemoProject.WebUI.Reports
{
    public partial class SubReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SubReport()
        {
            InitializeComponent();
        }

        private void SubReport_BeforePrint(object sender, CancelEventArgs e)
        {
            var SortBy = Convert.ToString(Parameters[5].Value);
            bool OrderByIs = Convert.ToBoolean(Parameters[6].Value);
            GroupField groupFieldSortBy = new GroupField();
            groupFieldSortBy.FieldName = SortBy;

            if (OrderByIs == true)
            {
                groupFieldSortBy.SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
            }
            else
            { 
                groupFieldSortBy.SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
            }
            Detail.SortFields.Add(groupFieldSortBy);
        }
        
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GroupHeader1.PageBreak == PageBreak.None)
            {
                GroupFooter1.KeepTogether = true;
                GroupFooter1.PageBreak = PageBreak.AfterBandExceptLastEntry;

            }
        }

        private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        {
            int LanguagePrmkey = 1;//Convert.ToInt32(Parameters["LanguagePrmkey"].Value);
            DateTime dateTime = Convert.ToDateTime(GetCurrentColumnValue("TransactionDate"));
            ((XRLabel)sender).Text = dateTime.ToString("dd-MM-yyyy");
            if (LanguagePrmkey == 2)
            {
                string result = new String(dateTime.ToString("dd-MM-yyyy")
                .Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c)
                .ToArray());
                xrLabel5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
                ((XRLabel)sender).Text = result;
            }
        }
    }
}
