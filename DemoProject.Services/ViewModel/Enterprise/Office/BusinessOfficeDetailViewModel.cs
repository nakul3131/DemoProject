using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeDetailViewModel
    {
        //BusinessOfficeDetail

        public short PrmKey { get; set; }

        public Guid BusinessOfficeDetailId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short CenterPrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

        public byte OfficeSchedulePrmKey { get; set; }

        public short BusinessOfficeTypePrmKey { get; set; }

        public byte BusinessNaturePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public short CommandAreaRadius { get; set; }

        public int PopulationOfTheCommandArea { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoAuthorization { get; set; }

        [StringLength(3)]
        public string GeneralLedgerGroupCode { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //BusinessOfficeDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // For SelectListItem

        public Guid CenterId { get; set; }

        public Guid OfficeScheduleId { get; set; }

        public Guid BusinessOfficeTypeId { get; set; }

        public Guid BusinessNatureId { get; set; }

        public Guid RegionalLanguageId { get; set; }

        public Guid GeneralLedgerId { get; set; }

        public Guid LocalCurrencyId { get; set; }

        public Guid BusinessOfficeId { get; set; }


    }
}
