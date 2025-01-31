namespace DemoProject.WebUI.Reports
{
    partial class MemberIncrementReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberIncrementReport));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.BusinessOfficePrmKey = new DevExpress.XtraReports.Parameters.Parameter();
            this.FromYear = new DevExpress.XtraReports.Parameters.Parameter();
            this.ToYear = new DevExpress.XtraReports.Parameters.Parameter();
            this.LanguagePrmkey = new DevExpress.XtraReports.Parameters.Parameter();
            this.IsBranchWise = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 30F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 80.20834F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart1});
            this.ReportHeader.HeightF = 215.625F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrChart1
            // 
            this.xrChart1.BorderColor = System.Drawing.Color.Black;
            this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xyDiagram1.AxisX.Label.TextPattern = "{A}";
            xyDiagram1.AxisX.Title.Text = "Financial Month";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Label.TextPattern = "{V}";
            xyDiagram1.AxisY.Title.MaxLineCount = 7;
            xyDiagram1.AxisY.Title.Text = "Financial Year";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
            this.xrChart1.Diagram = xyDiagram1;
            this.xrChart1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.xrChart1.Legend.MarkerSize = new System.Drawing.Size(85, 17);
            this.xrChart1.Legend.Name = "Default Legend";
            this.xrChart1.Legend.TextVisible = false;
            this.xrChart1.Legend.Title.Text = "Member Increment";
            this.xrChart1.Legend.Title.Visible = true;
            this.xrChart1.Legend.Title.WordWrap = true;
            this.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(23.54164F, 5.624994F);
            this.xrChart1.Name = "xrChart1";
            series1.ArgumentDataMember = "Usp_RptMemberIncrement.YValue";
            series1.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.True;
            series1.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            sideBySideBarSeriesLabel1.DXFont = new DevExpress.Drawing.DXFont("Times New Roman", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            series1.Label = sideBySideBarSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.LegendName = "Default Legend";
            series1.Name = "Series 1";
            series1.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
            series1.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.xrChart1.SeriesTemplate.ArgumentDataMember = "Usp_RptMemberIncrement.XValue";
            this.xrChart1.SeriesTemplate.ValueDataMembersSerializable = "Usp_RptMemberIncrement.YValue";
            this.xrChart1.SizeF = new System.Drawing.SizeF(701.0417F, 200F);
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "EFDbContext";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "Usp_RptMemberIncrement";
            queryParameter1.Name = "@BusinessOfficePrmKey";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("BusinessOfficePrmKey", typeof(short));
            queryParameter2.Name = "@FromYear";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("FromYear", typeof(short));
            queryParameter3.Name = "@ToYear";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("ToYear", typeof(short));
            queryParameter4.Name = "@LanguagePrmkey";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("LanguagePrmkey", typeof(byte));
            queryParameter5.Name = "@IsBranchWise";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("IsBranchWise", typeof(bool));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.Parameters.Add(queryParameter5);
            storedProcQuery1.StoredProcName = "Usp_RptMemberIncrement";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // BusinessOfficePrmKey
            // 
            this.BusinessOfficePrmKey.Description = "BusinessOfficePrmKey";
            this.BusinessOfficePrmKey.Name = "BusinessOfficePrmKey";
            this.BusinessOfficePrmKey.Type = typeof(int);
            this.BusinessOfficePrmKey.ValueInfo = "0";
            // 
            // FromYear
            // 
            this.FromYear.Description = "FromYear";
            this.FromYear.Name = "FromYear";
            this.FromYear.Type = typeof(int);
            this.FromYear.ValueInfo = "0";
            // 
            // ToYear
            // 
            this.ToYear.Description = "ToYear";
            this.ToYear.Name = "ToYear";
            this.ToYear.Type = typeof(int);
            this.ToYear.ValueInfo = "0";
            // 
            // LanguagePrmkey
            // 
            this.LanguagePrmkey.Description = "LanguagePrmkey";
            this.LanguagePrmkey.Name = "LanguagePrmkey";
            this.LanguagePrmkey.Type = typeof(short);
            this.LanguagePrmkey.ValueInfo = "0";
            // 
            // IsBranchWise
            // 
            this.IsBranchWise.Description = "IsBranchWise";
            this.IsBranchWise.Name = "IsBranchWise";
            this.IsBranchWise.Type = typeof(bool);
            this.IsBranchWise.ValueInfo = "False";
            // 
            // MemberIncrementReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "Usp_RptMemberIncrement";
            this.DataSource = this.sqlDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(70, 10, 100, 30);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.BusinessOfficePrmKey,
            this.FromYear,
            this.ToYear,
            this.LanguagePrmkey,
            this.IsBranchWise});
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRChart xrChart1;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter BusinessOfficePrmKey;
        private DevExpress.XtraReports.Parameters.Parameter FromYear;
        private DevExpress.XtraReports.Parameters.Parameter ToYear;
        private DevExpress.XtraReports.Parameters.Parameter LanguagePrmkey;
        private DevExpress.XtraReports.Parameters.Parameter IsBranchWise;
    }
}
