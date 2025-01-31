namespace DemoProject.WebUI.Reports
{
    partial class MasterReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterReport));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter8 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.BranchPrmkey = new DevExpress.XtraReports.Parameters.Parameter();
            this.FromDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.ToDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.LanguagePrmKey = new DevExpress.XtraReports.Parameters.Parameter();
            this.SortyBy = new DevExpress.XtraReports.Parameters.Parameter();
            this.IsAscending = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.Logo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrRichText16 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrRichText15 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrRichText14 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrRichText13 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrRichText17 = new DevExpress.XtraReports.UI.XRRichText();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubreport3 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport4 = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 42F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 61F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // BranchPrmkey
            // 
            this.BranchPrmkey.Description = "BranchPrmkey";
            this.BranchPrmkey.Name = "BranchPrmkey";
            this.BranchPrmkey.Type = typeof(int);
            this.BranchPrmkey.ValueInfo = "1";
            // 
            // FromDate
            // 
            this.FromDate.Description = "FromDate";
            this.FromDate.Name = "FromDate";
            this.FromDate.Type = typeof(System.DateTime);
            this.FromDate.ValueInfo = "2022-04-04";
            // 
            // ToDate
            // 
            this.ToDate.Description = "ToDate";
            this.ToDate.Name = "ToDate";
            this.ToDate.Type = typeof(System.DateTime);
            this.ToDate.ValueInfo = "2022-04-04";
            // 
            // LanguagePrmKey
            // 
            this.LanguagePrmKey.Name = "LanguagePrmKey";
            this.LanguagePrmKey.Type = typeof(byte);
            this.LanguagePrmKey.ValueInfo = "1";
            // 
            // SortyBy
            // 
            this.SortyBy.AllowNull = true;
            this.SortyBy.Description = "SortyBy";
            this.SortyBy.Name = "SortyBy";
            // 
            // IsAscending
            // 
            this.IsAscending.Description = "IsAscending";
            this.IsAscending.Name = "IsAscending";
            this.IsAscending.Type = typeof(bool);
            this.IsAscending.ValueInfo = "False";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.xrPanel1});
            this.ReportHeader.HeightF = 201.8331F;
            this.ReportHeader.KeepTogether = true;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBandExceptFirstEntry;
            // 
            // xrPanel2
            // 
            this.xrPanel2.BorderColor = System.Drawing.Color.Gold;
            this.xrPanel2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrPanel2.BorderWidth = 2F;
            this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Logo});
            this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel2.Name = "xrPanel2";
            this.xrPanel2.SizeF = new System.Drawing.SizeF(91.66666F, 170.2081F);
            this.xrPanel2.StylePriority.UseBorderColor = false;
            this.xrPanel2.StylePriority.UseBorders = false;
            this.xrPanel2.StylePriority.UseBorderWidth = false;
            // 
            // Logo
            // 
            this.Logo.BorderColor = System.Drawing.Color.White;
            this.Logo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Logo.BorderWidth = 2F;
            this.Logo.EditOptions.Enabled = true;
            this.Logo.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.TopCenter;
            this.Logo.ImageUrl = "Images\\ReportImg\\download.jpg";
            this.Logo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.Logo.Name = "Logo";
            this.Logo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Logo.SizeF = new System.Drawing.SizeF(91.66666F, 159.7915F);
            this.Logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.Logo.StylePriority.UseBorderColor = false;
            this.Logo.StylePriority.UseBorders = false;
            this.Logo.StylePriority.UseBorderWidth = false;
            this.Logo.StylePriority.UsePadding = false;
            this.Logo.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Logo_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.Linen;
            this.xrPanel1.BorderColor = System.Drawing.Color.Gold;
            this.xrPanel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrPanel1.BorderWidth = 2F;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrRichText16,
            this.xrRichText15,
            this.xrRichText14,
            this.xrRichText13,
            this.xrRichText17});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(91.66666F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 50, 0, 0, 100F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(1020.333F, 170.2081F);
            this.xrPanel1.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 50, 0, 0, 100F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            this.xrPanel1.StylePriority.UseBorderColor = false;
            this.xrPanel1.StylePriority.UseBorders = false;
            this.xrPanel1.StylePriority.UseBorderWidth = false;
            this.xrPanel1.StylePriority.UsePadding = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Usp_RptSocietyDetails].[GSTRegistrationNumber]")});
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(9.999992F, 137.8331F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(981.1248F, 23F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "xrLabel4";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrRichText16
            // 
            this.xrRichText16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrRichText16.Font = new DevExpress.Drawing.DXFont("Arial", 11.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrRichText16.LocationFloat = new DevExpress.Utils.PointFloat(9.999992F, 115.8747F);
            this.xrRichText16.Name = "xrRichText16";
            this.xrRichText16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRichText16.SerializableRtfString = resources.GetString("xrRichText16.SerializableRtfString");
            this.xrRichText16.SizeF = new System.Drawing.SizeF(981.1248F, 23.00001F);
            this.xrRichText16.StylePriority.UseBorders = false;
            this.xrRichText16.StylePriority.UseFont = false;
            // 
            // xrRichText15
            // 
            this.xrRichText15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrRichText15.Font = new DevExpress.Drawing.DXFont("Arial", 11.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrRichText15.LocationFloat = new DevExpress.Utils.PointFloat(9.999992F, 92.87472F);
            this.xrRichText15.Name = "xrRichText15";
            this.xrRichText15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRichText15.SerializableRtfString = resources.GetString("xrRichText15.SerializableRtfString");
            this.xrRichText15.SizeF = new System.Drawing.SizeF(981.1248F, 23F);
            this.xrRichText15.StylePriority.UseBorders = false;
            this.xrRichText15.StylePriority.UseFont = false;
            // 
            // xrRichText14
            // 
            this.xrRichText14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrRichText14.Font = new DevExpress.Drawing.DXFont("Arial", 11.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrRichText14.LocationFloat = new DevExpress.Utils.PointFloat(9.999992F, 69.8747F);
            this.xrRichText14.Name = "xrRichText14";
            this.xrRichText14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRichText14.SerializableRtfString = resources.GetString("xrRichText14.SerializableRtfString");
            this.xrRichText14.SizeF = new System.Drawing.SizeF(981.9591F, 23F);
            this.xrRichText14.StylePriority.UseBorders = false;
            this.xrRichText14.StylePriority.UseFont = false;
            // 
            // xrRichText13
            // 
            this.xrRichText13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrRichText13.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrRichText13.LocationFloat = new DevExpress.Utils.PointFloat(9.999847F, 46.87468F);
            this.xrRichText13.Name = "xrRichText13";
            this.xrRichText13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRichText13.SerializableRtfString = resources.GetString("xrRichText13.SerializableRtfString");
            this.xrRichText13.SizeF = new System.Drawing.SizeF(981.1249F, 23F);
            this.xrRichText13.StylePriority.UseBorders = false;
            this.xrRichText13.StylePriority.UseFont = false;
            // 
            // xrRichText17
            // 
            this.xrRichText17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrRichText17.Font = new DevExpress.Drawing.DXFont("Arial", 15F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrRichText17.LocationFloat = new DevExpress.Utils.PointFloat(9.999992F, 15.625F);
            this.xrRichText17.Name = "xrRichText17";
            this.xrRichText17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRichText17.SerializableRtfString = resources.GetString("xrRichText17.SerializableRtfString");
            this.xrRichText17.SizeF = new System.Drawing.SizeF(981.1249F, 31.24968F);
            this.xrRichText17.StylePriority.UseBorders = false;
            this.xrRichText17.StylePriority.UseFont = false;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "EFDbContext";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "Usp_RptSocietyDetails";
            queryParameter1.Name = "@LanguagePrmkey";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("?LanguagePrmKey", typeof(byte));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "Usp_RptSocietyDetails";
            storedProcQuery2.Name = "Usp_RptCashScroll";
            queryParameter2.Name = "@BranchPrmKey";
            queryParameter2.Type = typeof(short);
            queryParameter2.ValueInfo = "1";
            queryParameter3.Name = "@FromDate";
            queryParameter3.Type = typeof(System.DateTime);
            queryParameter3.ValueInfo = "01/01/2022 12:00:00";
            queryParameter4.Name = "@ToDate";
            queryParameter4.Type = typeof(System.DateTime);
            queryParameter4.ValueInfo = "01/01/2024 12:00:00";
            queryParameter5.Name = "@LanguagePrmkey";
            queryParameter5.Type = typeof(byte);
            queryParameter5.ValueInfo = "1";
            queryParameter6.Name = "@IsCredit";
            queryParameter6.Type = typeof(bool);
            queryParameter6.ValueInfo = "True";
            queryParameter7.Name = "@SortBy";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("", typeof(string));
            queryParameter8.Name = "@IsAscending";
            queryParameter8.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter8.Value = new DevExpress.DataAccess.Expression("False", typeof(bool));
            storedProcQuery2.Parameters.Add(queryParameter2);
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.Parameters.Add(queryParameter4);
            storedProcQuery2.Parameters.Add(queryParameter5);
            storedProcQuery2.Parameters.Add(queryParameter6);
            storedProcQuery2.Parameters.Add(queryParameter7);
            storedProcQuery2.Parameters.Add(queryParameter8);
            storedProcQuery2.StoredProcName = "Usp_RptCashScroll";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1,
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.HeightF = 91.75208F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1012F, 68.75207F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo1.TextFormatString = "Page {0} of {1}";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport3,
            this.xrSubreport4});
            this.Detail.FillEmptySpace = true;
            this.Detail.HeightF = 50.08332F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            // 
            // xrSubreport3
            // 
            this.xrSubreport3.CanShrink = true;
            this.xrSubreport3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreport3.Name = "xrSubreport3";
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("BranchPrmKey", this.BranchPrmkey));
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("FromDate", this.FromDate));
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("ToDate", this.ToDate));
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("LanguagePrmKey", this.LanguagePrmKey));
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("IsCredit", null, null));
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("SortBy", this.SortyBy));
            this.xrSubreport3.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("IsAscending", this.IsAscending));
            this.xrSubreport3.ReportSource = new DemoProject.WebUI.Reports.SubReport();
            this.xrSubreport3.SizeF = new System.Drawing.SizeF(535.4166F, 50.08332F);
            this.xrSubreport3.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrSubreport3_BeforePrint);
            // 
            // xrSubreport4
            // 
            this.xrSubreport4.CanShrink = true;
            this.xrSubreport4.LocationFloat = new DevExpress.Utils.PointFloat(551.0418F, 0F);
            this.xrSubreport4.LockedInUserDesigner = true;
            this.xrSubreport4.Name = "xrSubreport4";
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("BranchPrmKey", this.BranchPrmkey));
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("FromDate", this.FromDate));
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("ToDate", this.ToDate));
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("LanguagePrmKey", this.LanguagePrmKey));
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("IsCredit", null, null));
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("SortBy", this.SortyBy));
            this.xrSubreport4.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("IsAscending", this.IsAscending));
            this.xrSubreport4.ReportSource = new DemoProject.WebUI.Reports.SubReport();
            this.xrSubreport4.Scripts.OnLocationChanged = "xrSubreport2_LocationChanged";
            this.xrSubreport4.SizeF = new System.Drawing.SizeF(560.9578F, 50.08332F);
            this.xrSubreport4.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrSubreport4_BeforePrint);
            // 
            // MasterReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "Usp_RptSocietyDetails";
            this.DataSource = this.sqlDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.HorizontalContentSplitting = DevExpress.XtraPrinting.HorizontalContentSplitting.Smart;
            this.Landscape = true;
            this.Margins = new DevExpress.Drawing.DXMargins(25, 31, 42, 61);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.LanguagePrmKey,
            this.BranchPrmkey,
            this.ToDate,
            this.FromDate,
            this.SortyBy,
            this.IsAscending});
            this.Version = "19.2";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.MasterReport_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel2;
        private DevExpress.XtraReports.UI.XRPictureBox Logo;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRRichText xrRichText16;
        private DevExpress.XtraReports.UI.XRRichText xrRichText15;
        private DevExpress.XtraReports.UI.XRRichText xrRichText14;
        private DevExpress.XtraReports.UI.XRRichText xrRichText13;
        private DevExpress.XtraReports.UI.XRRichText xrRichText17;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter LanguagePrmKey;
        private DevExpress.XtraReports.Parameters.Parameter BranchPrmkey;
        private DevExpress.XtraReports.Parameters.Parameter ToDate;
        private DevExpress.XtraReports.Parameters.Parameter FromDate;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.Parameters.Parameter SortyBy;
        private DevExpress.XtraReports.Parameters.Parameter IsAscending;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport3;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport4;
    }
}
