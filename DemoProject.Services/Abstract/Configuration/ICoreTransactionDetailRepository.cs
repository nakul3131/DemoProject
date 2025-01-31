using System.Collections.Generic;
using DemoProject.Domain.CustomEntities.CBS;
using DemoProject.Domain.CustomEntities.CBS.Vanghee;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Configuration
{
    public interface ICoreTransactionDetailRepository
    {

        IEnumerable<CBSProviderAccountDetail> CBSProviderAccountDetails { get; }

        IEnumerable<CBSProvider> CBSProviders { get; }



        void SaveCBSProvider(CBSProvider _cBSProvider);

        void SaveCBSProviderAccountDetail(CBSProviderAccountDetail _cBSProviderAccountDetail);

        string GetLoginRequestChecksum();

        string GetLoginRequestEncryptedValue();

        CBSAccountCredential GetCBSAccountCredentials(string _nameOfProvider);

        AddBeneficiaryDetailResponse AddBeneficiaryDetailResponse(BeneficiaryDetailViewModel _beneficiaryDetailViewModel);
    }
}
