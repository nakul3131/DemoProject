using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CenterTradingEntityDetailViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;

        public CenterTradingEntityDetailViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterTradingEntityDetailId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short TradingEntityPrmKey { get; set; }

        public long Volume { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CenterTradingEntityDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short CenterTradingEntityDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Center
        public Guid CenterId { get; set; }

        [StringLength(100)]
        public string NameOfCenter { get; set; }

        // Trading Entity
        public Guid TradingEntityId { get; set; }

        public string NameOfTradingEntity { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        //Dropdown
        public List<SelectListItem> TradingEntityDropdownList
        {
            get
            {
                return personDetailRepository.TradingEntityDropdownList;
            }
        }

    }
}
