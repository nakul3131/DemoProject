using DemoProject.Domain.CustomEntities;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Domain.Entities.Account.FinancialStatement;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Domain.Entities.Account.Layout;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Domain.Entities.Account.Parameter;
using DemoProject.Domain.Entities.Account.SystemEntity;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Domain.Entities.Enterprise.Establishment;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Domain.Entities.Enterprise.Schedule;
using DemoProject.Domain.Entities.Enterprise.SystemEntity;
using DemoProject.Domain.Entities.MachineLearning;
using DemoProject.Domain.Entities.Management.Conference;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Domain.Entities.Management.Parameter;
using DemoProject.Domain.Entities.Management.Servant;
using DemoProject.Domain.Entities.Management.SystemEntity;
using DemoProject.Domain.Entities.Master.General.Notice;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Domain.Entities.PersonInformation.Parameter;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;
using DemoProject.Domain.Entities.Security;
using DemoProject.Domain.Entities.Security.Log;
using DemoProject.Domain.Entities.Security.Master;
using DemoProject.Domain.Entities.Security.Parameter;
using DemoProject.Domain.Entities.Security.SystemEntity;
using DemoProject.Domain.Entities.Security.UserRoles;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Domain.Entities.SMS;
using System.Data.Entity;

namespace DemoProject.Services.Concrete
{
    //
    //  Entity Framework :
    //                      We Use Entity Framework because it is simple and easy to get it up and working;
    //                      The Integration with LINQ is first reason.
    //
    //  LINQ :
    //          LINQ can work with different sources of data, and one of these is the Entity Framework
    // 
    // Summary :
    //          Context class that will associate the model with the database.
    //          We need to create a class that is derived from System.Data.Entity.DbContext.
    //          This class automatically defines a property for each table in the database that we want to work with.
    //          The name of the property specifies the table, and the type parameter of the DbSet result specifies the model
    //          type that the Entity Framework should use to represent rows in that table.
    //          In following case the property name is MLChapters, MLSubjects, AuthenticationFactors, AuthenticationMethods and the 
    //          type parameter is MLChapter, MLSubject, AuthenticationFactor, AuthenticationMethod table.
    //          We added a database connection string to the Web.config file in the PathPranali.WebUI project with the same name
    //          as the context that tell the Entity Framework tell the Entity Framework how to connect to the database

    public class EFDbContext : DbContext
    {
        //
        // Summary:
        //     This method is called when the model for a derived context has been initialized,
        //     but before the model has been locked down and used to initialize the context.
        //     The default implementation of this method does nothing, but it can be overridden
        //     in a derived class such that the model can be further configured before it is
        //     locked down.
        //
        // Parameters:
        //   modelBuilder:
        //     The builder that defines the model for the context being created.
        //
        // Remarks:
        //     Typically, this method is called only once when the first instance of a derived
        //     context is created. The model for that context is then cached and is for all
        //     further instances of the context in the app domain. This caching can be disabled
        //     by setting the ModelCaching property on the given ModelBuidler, but note that
        //     this can seriously degrade performance. More control over caching is provided
        //     through use of the DbModelBuilder and DbContextFactory classes directly.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Note - ** Main Module Name In CAPITAL and Parent Module Name In Upper Camel Case and Child Module Name In lower case **
            // Module Name - @@@@@ With C A P I T A L And One Space Between Every Character
            // Folder Name - ##### With CAPITAL Case Without Any Space
            // Entity Name - ***** With UpperCamelCase Without Any Space

            // @@@@@@@@@@@@@@@@@@@@@@@@@@ A C C O U N T @@@@@@@@@@@@@@@@@@@@@@@@@@
            // ################ Customer ###################
            modelBuilder.Entity<BeneficiaryDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBeneficiaryDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BeneficiaryDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBeneficiaryDetail")));

            modelBuilder.Entity<BeneficiaryDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBeneficiaryDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ChequeBookMaster>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddChequeBookMaster")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ChequeBookMaster>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateChequeBookMaster")));

            modelBuilder.Entity<ChequeBookMasterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddChequeBookMasterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@ Master @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<FixedAssetItem>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFixedAssetItem")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<FixedAssetItem>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateFixedAssetItem")));

            modelBuilder.Entity<FixedAssetItemMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFixedAssetItemMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<FixedAssetItemModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFixedAssetItemModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<FixedAssetItemModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateFixedAssetItemModification")));

            modelBuilder.Entity<FixedAssetItemModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFixedAssetItemModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<FixedAssetItemTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFixedAssetItemTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<FixedAssetItemTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateFixedAssetItemTranslation")));

            modelBuilder.Entity<FixedAssetItemTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFixedAssetItemTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ConsumerDurableItem>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddConsumerDurableItem")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ConsumerDurableItem>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateConsumerDurableItem")));

            modelBuilder.Entity<ConsumerDurableItemMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddConsumerDurableItemMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ConsumerDurableItemModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddConsumerDurableItemModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ConsumerDurableItemModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateConsumerDurableItemModification")));

            modelBuilder.Entity<ConsumerDurableItemModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddConsumerDurableItemModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ConsumerDurableItemTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddConsumerDurableItemTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ConsumerDurableItemTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateConsumerDurableItemTranslation")));

            modelBuilder.Entity<ConsumerDurableItemTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddConsumerDurableItemTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // ###################### ASSET ######################################

            // ###################### CREDIT BUEREAU ######################################

            // ###################### CUSTOMER ######################################
            modelBuilder.Entity<CustomerAccount>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccount")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccount>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccount")));

            modelBuilder.Entity<CustomerAccountMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountModification")));

            modelBuilder.Entity<CustomerAccountModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountDetail")));

            modelBuilder.Entity<CustomerAccountDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNominee>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNominee")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNominee>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountNominee")));

            modelBuilder.Entity<CustomerAccountNomineeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNomineeTranslation>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeTranslation")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNomineeTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountNomineeTranslation")));

            modelBuilder.Entity<CustomerAccountNomineeTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNomineeGuardian>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeGuardian")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNomineeGuardian>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountNomineeGuardian")));

            modelBuilder.Entity<CustomerAccountNomineeGuardianMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeGuardianMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNomineeGuardianTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeGuardianTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNomineeGuardianTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountNomineeGuardianTranslation")));

            modelBuilder.Entity<CustomerAccountNomineeGuardianTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNomineeGuardianTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNoticeSchedule>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNoticeSchedule")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountNoticeSchedule>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountNoticeSchedule")));

            modelBuilder.Entity<CustomerAccountNoticeScheduleMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountNoticeScheduleMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerJointAccountHolder>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerJointAccountHolder")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerJointAccountHolder>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerJointAccountHolder")));

            modelBuilder.Entity<CustomerJointAccountHolderMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerJointAccountHolderMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountTurnOverLimit>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountTurnOverLimit")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountTurnOverLimit>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountTurnOverLimit")));

            modelBuilder.Entity<CustomerAccountTurnOverLimitMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountTurnOverLimitMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountSweepDetail>()
        .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountSweepDetail")
        .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountSweepDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountSweepDetail")));

            modelBuilder.Entity<CustomerAccountSweepDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountSweepDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountPhotoSign>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountPhotoSign")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountPhotoSign>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountPhotoSign")));

            modelBuilder.Entity<CustomerAccountPhotoSignMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountPhotoSignMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountEmailService>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountEmailService")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountEmailService>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountEmailService")));

            modelBuilder.Entity<CustomerAccountEmailServiceMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountEmailServiceMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountSmsService>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountSmsService")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountSmsService>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountSmsService")));

            modelBuilder.Entity<CustomerAccountSmsServiceMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountSmsServiceMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountChequeBookRequestDetail>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountChequeBookRequestDetail")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountChequeBookRequestDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountChequeBookRequestDetail")));

            modelBuilder.Entity<CustomerAccountChequeBookRequestDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountChequeBookRequestDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountChequeDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountChequeDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountChequeDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountChequeDetail")));

            modelBuilder.Entity<CustomerAccountChequeDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountChequeDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountBeneficiaryDetail>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountBeneficiaryDetail")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountBeneficiaryDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountBeneficiaryDetail")));

            modelBuilder.Entity<CustomerAccountBeneficiaryDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountBeneficiaryDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerSharesCapitalAccount>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerSharesCapitalAccount")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerSharesCapitalAccount>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerSharesCapitalAccount")));

            modelBuilder.Entity<CustomerSharesCapitalAccountMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerSharesCapitalAccountMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerDepositAccount>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerDepositAccount")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerDepositAccount>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerDepositAccount")));

            modelBuilder.Entity<CustomerDepositAccountMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerDepositAccountMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerDepositAccountAgent>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerDepositAccountAgent")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerDepositAccountAgent>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerDepositAccountAgent")));

            modelBuilder.Entity<CustomerDepositAccountAgentMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerDepositAccountAgentMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerTermDepositAccountDetail>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerTermDepositAccountDetail")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerTermDepositAccountDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerTermDepositAccountDetail")));

            modelBuilder.Entity<CustomerTermDepositAccountDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerTermDepositAccountDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            modelBuilder.Entity<CustomerAccountContactDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountContactDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountContactDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountContactDetail")));

            modelBuilder.Entity<CustomerAccountContactDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountContactDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountAddressDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountAddressDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountAddressDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountAddressDetail")));

            modelBuilder.Entity<CustomerAccountAddressDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountAddressDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountAdditionalIncomeDetail>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountAdditionalIncomeDetail")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountAdditionalIncomeDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountAdditionalIncomeDetail")));

            modelBuilder.Entity<CustomerLoanAccountAdditionalIncomeDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountAdditionalIncomeDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountCourtCaseDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountCourtCaseDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountCourtCaseDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountCourtCaseDetail")));

            modelBuilder.Entity<CustomerLoanAccountCourtCaseDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountCourtCaseDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountBorrowingDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountBorrowingDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountBorrowingDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountBorrowingDetail")));

            modelBuilder.Entity<CustomerLoanAccountBorrowingDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountBorrowingDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountIncomeTaxDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountIncomeTaxDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountIncomeTaxDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountIncomeTaxDetail")));

            modelBuilder.Entity<CustomerLoanAccountIncomeTaxDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountIncomeTaxDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountEmploymentDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountEmploymentDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountEmploymentDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountEmploymentDetail")));

            modelBuilder.Entity<CustomerAccountEmploymentDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountEmploymentDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountInterestRate>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountInterestRate")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountInterestRate>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountInterestRate")));

            modelBuilder.Entity<CustomerAccountInterestRateMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountInterestRateMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAgainstPropertyCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAgainstPropertyCollateralDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAgainstPropertyCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAgainstPropertyCollateralDetail")));

            modelBuilder.Entity<CustomerLoanAgainstPropertyCollateralDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAgainstPropertyCollateralDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAgainstDepositCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAgainstDepositCollateralDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAgainstDepositCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAgainstDepositCollateralDetail")));

            modelBuilder.Entity<CustomerLoanAgainstDepositCollateralDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAgainstDepositCollateralDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerBusinessLoanCollateralDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerBusinessLoanCollateralDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerBusinessLoanCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerBusinessLoanCollateralDetail")));

            modelBuilder.Entity<CustomerBusinessLoanCollateralDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerBusinessLoanCollateralDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountDocument>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountDocument")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountDocument>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountDocument")));

            modelBuilder.Entity<CustomerAccountDocumentMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountDocumentMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountReferencePersonDetail>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountReferencePersonDetail")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountReferencePersonDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountReferencePersonDetail")));

            modelBuilder.Entity<CustomerAccountReferencePersonDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountReferencePersonDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            modelBuilder.Entity<CustomerAccountStandingInstruction>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountStandingInstruction")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountStandingInstruction>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountStandingInstruction")));

            modelBuilder.Entity<CustomerAccountStandingInstructionMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountStandingInstructionMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            modelBuilder.Entity<CustomerLoanAccount>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccount")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccount>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccount")));

            modelBuilder.Entity<CustomerLoanAccountMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountTranslation")));

            modelBuilder.Entity<CustomerLoanAccountTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountGuarantorDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountGuarantorDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountGuarantorDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountGuarantorDetail")));

            modelBuilder.Entity<CustomerLoanAccountGuarantorDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountGuarantorDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanInsuranceDetail>()
                         .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanInsuranceDetail")
                         .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanInsuranceDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerVehicleLoanInsuranceDetail")));

            modelBuilder.Entity<CustomerVehicleLoanInsuranceDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanInsuranceDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanPermitDetail>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanPermitDetail")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanPermitDetail>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerVehicleLoanPermitDetail")));

            modelBuilder.Entity<CustomerVehicleLoanPermitDetailMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanPermitDetailMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanContractDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanContractDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanContractDetail>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerVehicleLoanContractDetail")));

            modelBuilder.Entity<CustomerVehicleLoanContractDetailMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanContractDetailMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanFieldInvestigation>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanFieldInvestigation")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanFieldInvestigation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanFieldInvestigation")));

            modelBuilder.Entity<CustomerLoanFieldInvestigationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanFieldInvestigationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountDebtToIncomeRatio>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountDebtToIncomeRatio")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAccountDebtToIncomeRatio>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAccountDebtToIncomeRatio")));

            modelBuilder.Entity<CustomerLoanAccountDebtToIncomeRatioMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAccountDebtToIncomeRatioMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            modelBuilder.Entity<CustomerEducationalLoanDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerEducationalLoanDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerEducationalLoanDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerEducationalLoanDetail")));

            modelBuilder.Entity<CustomerEducationalLoanDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerEducationalLoanDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerEducationalLoanDetailTranslation>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerEducationalLoanDetailTranslation")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerEducationalLoanDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerEducationalLoanDetailTranslation")));

            modelBuilder.Entity<CustomerEducationalLoanDetailTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerEducationalLoanDetailTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerCashCreditLoanAccount>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerCashCreditLoanAccount")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerCashCreditLoanAccount>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerCashCreditLoanAccount")));

            modelBuilder.Entity<CustomerCashCreditLoanAccountMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerCashCreditLoanAccountMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerPreOwnedVehicleLoanInspection>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerPreOwnedVehicleLoanInspection")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerPreOwnedVehicleLoanInspection>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerPreOwnedVehicleLoanInspection")));

            modelBuilder.Entity<CustomerPreOwnedVehicleLoanInspectionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerPreOwnedVehicleLoanInspectionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanPhoto>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanPhoto")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanPhoto>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerVehicleLoanPhoto")));

            modelBuilder.Entity<CustomerVehicleLoanPhotoMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanPhotoMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanCollateralDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerVehicleLoanCollateralDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerVehicleLoanCollateralDetail")));

            modelBuilder.Entity<CustomerVehicleLoanCollateralDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerVehicleLoanCollateralDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAcquaintanceDetail>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAcquaintanceDetail")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerLoanAcquaintanceDetail>()
                           .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerLoanAcquaintanceDetail")));

            modelBuilder.Entity<CustomerLoanAcquaintanceDetailMakerChecker>()
                           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerLoanAcquaintanceDetailMakerChecker")
                           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerConsumerLoanCollateralDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerConsumerLoanCollateralDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerConsumerLoanCollateralDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerConsumerLoanCollateralDetail")));

            modelBuilder.Entity<CustomerConsumerLoanCollateralDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerConsumerLoanCollateralDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerGoldLoanCollateralDetail>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerGoldLoanCollateralDetail")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerGoldLoanCollateralDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerGoldLoanCollateralDetail")));

            modelBuilder.Entity<CustomerGoldLoanCollateralDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerGoldLoanCollateralDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerGoldLoanCollateralPhoto>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerGoldLoanCollateralPhoto")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerGoldLoanCollateralPhoto>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerGoldLoanCollateralPhoto")));

            modelBuilder.Entity<CustomerGoldLoanCollateralPhotoMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerGoldLoanCollateralPhotoMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerGoldLoanReappraisal>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerGoldLoanReappraisal")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerGoldLoanReappraisal>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerGoldLoanReappraisal")));

            modelBuilder.Entity<CustomerGoldLoanReappraisalMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerGoldLoanReappraisalMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ###################### FINANCIAL CYCLE AND PERIOD ######################
            modelBuilder.Entity<FinancialCycle>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFinancialCycle")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<FinancialCycle>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateFinancialCycle")));

            modelBuilder.Entity<FinancialCycleMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddFinancialCycleMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PeriodCode>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPeriodCode")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PeriodCode>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePeriodCode")));

            modelBuilder.Entity<PeriodCodeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPeriodCodeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ###################### GL ######################################
            modelBuilder.Entity<GeneralLedger>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedger")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedger>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedger")));

            modelBuilder.Entity<GeneralLedgerMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerModification>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerModification")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerModification>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedgerModification")));

            modelBuilder.Entity<GeneralLedgerModificationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerModificationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerTransactionType>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerTransactionType")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerTransactionType>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedgerTransactionType")));

            modelBuilder.Entity<GeneralLedgerTransactionTypeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerTransactionTypeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            modelBuilder.Entity<GeneralLedgerTranslation>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerTranslation")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerTranslation>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedgerTranslation")));

            modelBuilder.Entity<GeneralLedgerTranslationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerTranslationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeGeneralLedger>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeGeneralLedger")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeGeneralLedger>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeGeneralLedger")));

            modelBuilder.Entity<SchemeGeneralLedgerMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeGeneralLedgerMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerBusinessOffice>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerBusinessOffice")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerBusinessOffice>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedgerBusinessOffice")));

            modelBuilder.Entity<GeneralLedgerBusinessOfficeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerBusinessOfficeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerCurrency>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerCurrency")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerCurrency>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedgerCurrency")));

            modelBuilder.Entity<GeneralLedgerCurrencyMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerCurrencyMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerCustomerType>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerCustomerType")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GeneralLedgerCustomerType>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGeneralLedgerCustomerType")));

            modelBuilder.Entity<GeneralLedgerCustomerTypeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGeneralLedgerCustomerTypeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RevenueGeneralLedgerParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRevenueGeneralLedgerParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RevenueGeneralLedgerParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRevenueGeneralLedgerParameter")));

            modelBuilder.Entity<RevenueGeneralLedgerParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRevenueGeneralLedgerParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RevenueGeneralLedgerTaxParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRevenueGeneralLedgerTaxParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RevenueGeneralLedgerTaxParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRevenueGeneralLedgerTaxParameter")));

            modelBuilder.Entity<RevenueGeneralLedgerTaxParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRevenueGeneralLedgerTaxParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            modelBuilder.Entity<ChequeBookMaster>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddChequeBookMaster")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ChequeBookMaster>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateChequeBookMaster")));

            modelBuilder.Entity<ChequeBookMasterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddChequeBookMasterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ###################### LAYOUT ######################################
            // Scheme
            modelBuilder.Entity<Scheme>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheme")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Scheme>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateScheme")));

            modelBuilder.Entity<SchemeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeTranslation
            modelBuilder.Entity<SchemeTranslation>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTranslation")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTranslation>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTranslation")));

            modelBuilder.Entity<SchemeTranslationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTranslationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeAccountParameter
            modelBuilder.Entity<SchemeAccountParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeAccountParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeAccountParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeAccountParameter")));

            modelBuilder.Entity<SchemeAccountParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeAccountParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeApplicationParameter
            modelBuilder.Entity<SchemeApplicationParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeApplicationParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeApplicationParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeApplicationParameter")));

            modelBuilder.Entity<SchemeApplicationParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeApplicationParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeAccountBankingChannelParameter
            modelBuilder.Entity<SchemeAccountBankingChannelParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeAccountBankingChannelParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeAccountBankingChannelParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeAccountBankingChannelParameter")));

            modelBuilder.Entity<SchemeAccountBankingChannelParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeAccountBankingChannelParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // SchemeCustomerAccountNumber
            modelBuilder.Entity<SchemeCustomerAccountNumber>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeCustomerAccountNumber")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeCustomerAccountNumber>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeCustomerAccountNumber")));

            modelBuilder.Entity<SchemeCustomerAccountNumberMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeCustomerAccountNumberMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeChargesDetail
            modelBuilder.Entity<SchemeChargesDetail>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeChargesDetail")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeChargesDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeChargesDetail")));

            modelBuilder.Entity<SchemeChargesDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeChargesDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeNoticeSchedule
            modelBuilder.Entity<SchemeNoticeSchedule>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeNoticeSchedule")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeNoticeSchedule>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeNoticeSchedule")));

            modelBuilder.Entity<SchemeNoticeScheduleMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeNoticeScheduleMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeTenureList
            modelBuilder.Entity<SchemeTenureList>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTenureList")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTenureList>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTenureList")));

            modelBuilder.Entity<SchemeTenureListMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTenureListMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeTenure
            modelBuilder.Entity<SchemeTenure>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTenure")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTenure>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTenure")));

            modelBuilder.Entity<SchemeTenureMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTenureMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeReportFormat
            modelBuilder.Entity<SchemeReportFormat>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeReportFormat")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeReportFormat>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeReportFormat")));

            modelBuilder.Entity<SchemeReportFormatMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeReportFormatMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeEstimateTarget
            modelBuilder.Entity<SchemeEstimateTarget>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeEstimateTarget")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeEstimateTarget>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeEstimateTarget")));

            modelBuilder.Entity<SchemeEstimateTargetMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeEstimateTargetMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLimit
            modelBuilder.Entity<SchemeLimit>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLimit")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLimit>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLimit")));

            modelBuilder.Entity<SchemeLimitMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLimitMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeFixedDepositParameter
            modelBuilder.Entity<SchemeFixedDepositParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeFixedDepositParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeFixedDepositParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeFixedDepositParameter")));

            modelBuilder.Entity<SchemeFixedDepositParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeFixedDepositParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositPledgeLoanParameter
            modelBuilder.Entity<SchemeDepositPledgeLoanParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositPledgeLoanParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositPledgeLoanParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositPledgeLoanParameter")));

            modelBuilder.Entity<SchemeDepositPledgeLoanParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositPledgeLoanParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositAccountRenewalParameter
            modelBuilder.Entity<SchemeDepositAccountRenewalParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAccountRenewalParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositAccountRenewalParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositAccountRenewalParameter")));

            modelBuilder.Entity<SchemeDepositAccountRenewalParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAccountRenewalParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositAccountClosureParameter
            modelBuilder.Entity<SchemeDepositAccountClosureParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAccountClosureParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositAccountClosureParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositAccountClosureParameter")));

            modelBuilder.Entity<SchemeDepositAccountClosureParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAccountClosureParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeBusinessOffice
            modelBuilder.Entity<SchemeBusinessOffice>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeBusinessOffice")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeBusinessOffice>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeBusinessOffice")));

            modelBuilder.Entity<SchemeBusinessOfficeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeBusinessOfficeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeSharesCapitalAccountParameter
            modelBuilder.Entity<SchemeSharesCapitalAccountParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesCapitalAccountParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeSharesCapitalAccountParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeSharesCapitalAccountParameter")));

            modelBuilder.Entity<SchemeSharesCapitalAccountParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesCapitalAccountParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeSharesCertificateParameter
            modelBuilder.Entity<SchemeSharesCertificateParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesCertificateParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeSharesCertificateParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeSharesCertificateParameter")));

            modelBuilder.Entity<SchemeSharesCertificateParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesCertificateParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeSharesCapitalDividendParameter
            modelBuilder.Entity<SchemeSharesCapitalDividendParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesCapitalDividendParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeSharesCapitalDividendParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeSharesCapitalDividendParameter")));

            modelBuilder.Entity<SchemeSharesCapitalDividendParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesCapitalDividendParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeClosingCharges
            modelBuilder.Entity<SchemeClosingCharges>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeClosingCharges")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeClosingCharges>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeClosingCharges")));

            modelBuilder.Entity<SchemeClosingChargesMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeClosingChargesMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeSharesTransferCharges
            modelBuilder.Entity<SchemeSharesTransferCharges>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesTransferCharges")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeSharesTransferCharges>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeSharesTransferCharges")));

            modelBuilder.Entity<SchemeSharesTransferChargesMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeSharesTransferChargesMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositAccountParameter
            modelBuilder.Entity<SchemeDepositAccountParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAccountParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositAccountParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositAccountParameter")));

            modelBuilder.Entity<SchemeDepositAccountParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAccountParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositAgentIncentive
            modelBuilder.Entity<SchemeDepositAgentIncentive>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAgentIncentive")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositAgentIncentive>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositAgentIncentive")));

            modelBuilder.Entity<SchemeDepositAgentIncentiveMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAgentIncentiveMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositAgentParameter
            modelBuilder.Entity<SchemeDepositAgentParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAgentParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositAgentParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositAgentParameter")));

            modelBuilder.Entity<SchemeDepositAgentParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositAgentParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositCertificateParameter
            modelBuilder.Entity<SchemeDepositCertificateParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositCertificateParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositCertificateParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositCertificateParameter")));

            modelBuilder.Entity<SchemeDepositCertificateParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositCertificateParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositInstallmentParameter
            modelBuilder.Entity<SchemeDepositInstallmentParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInstallmentParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositInstallmentParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositInstallmentParameter")));

            modelBuilder.Entity<SchemeDepositInstallmentParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInstallmentParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositInterestParameter
            modelBuilder.Entity<SchemeDepositInterestParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInterestParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositInterestParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositInterestParameter")));

            modelBuilder.Entity<SchemeDepositInterestParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInterestParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositInterestProvisionParameter
            modelBuilder.Entity<SchemeDepositInterestProvisionParameter>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInterestProvisionParameter")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositInterestProvisionParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositInterestProvisionParameter")));

            modelBuilder.Entity<SchemeDepositInterestProvisionParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInterestProvisionParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositInterestPayoutFrequency
            modelBuilder.Entity<SchemeDepositInterestPayoutFrequency>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInterestPayoutFrequency")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositInterestPayoutFrequency>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositInterestPayoutFrequency")));

            modelBuilder.Entity<SchemeDepositInterestPayoutFrequencyMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositInterestPayoutFrequencyMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeInterestPayoutFrequency
            modelBuilder.Entity<SchemeInterestPayoutFrequency>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeInterestPayoutFrequency")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeInterestPayoutFrequency>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeInterestPayoutFrequency")));

            modelBuilder.Entity<SchemeInterestPayoutFrequencyMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeInterestPayoutFrequencyMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeNumberOfTransactionLimit
            modelBuilder.Entity<SchemeNumberOfTransactionLimit>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeNumberOfTransactionLimit")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeNumberOfTransactionLimit>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeNumberOfTransactionLimit")));

            modelBuilder.Entity<SchemeNumberOfTransactionLimitMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeNumberOfTransactionLimitMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemePaymentCardFeature
            modelBuilder.Entity<SchemePaymentCardFeature>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemePaymentCardFeature")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemePaymentCardFeature>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemePaymentCardFeature")));

            modelBuilder.Entity<SchemePaymentCardFeatureMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemePaymentCardFeatureMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeTransactionAmountLimit
            modelBuilder.Entity<SchemeTransactionAmountLimit>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTransactionAmountLimit")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTransactionAmountLimit>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTransactionAmountLimit")));

            modelBuilder.Entity<SchemeTransactionAmountLimitMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTransactionAmountLimitMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // SchemeDemandDepositDetail
            modelBuilder.Entity<SchemeDemandDepositDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDemandDepositDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDemandDepositDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDemandDepositDetail")));

            modelBuilder.Entity<SchemeDemandDepositDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDemandDepositDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeDepositClosingMode
            modelBuilder.Entity<SchemeDepositClosingMode>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositClosingMode")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDepositClosingMode>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDepositClosingMode")));

            modelBuilder.Entity<SchemeDepositClosingModeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDepositClosingModeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeTermDepositDetail
            modelBuilder.Entity<SchemeTermDepositDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTermDepositDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTermDepositDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTermDepositDetail")));

            modelBuilder.Entity<SchemeTermDepositDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTermDepositDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemePassbook
            modelBuilder.Entity<SchemePassbook>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemePassbook")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemePassbook>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemePassbook")));

            modelBuilder.Entity<SchemePassbookMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemePassbookMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //################## LOAN SCHEME ########################################################
            // SchemeLoanChargesParameter
            modelBuilder.Entity<SchemeLoanChargesParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanChargesParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanChargesParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanChargesParameter")));

            modelBuilder.Entity<SchemeLoanChargesParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanChargesParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // SchemeDocumentType
            modelBuilder.Entity<SchemeDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeDocument>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeDocument")));

            modelBuilder.Entity<SchemeDocumentMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeDocumentMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanDistributorParameter
            modelBuilder.Entity<SchemeLoanDistributorParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanDistributorParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanDistributorParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanDistributorParameter")));

            modelBuilder.Entity<SchemeLoanDistributorParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanDistributorParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanFunderParameter
            modelBuilder.Entity<SchemeLoanFunderParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanFunderParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanFunderParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanFunderParameter")));

            modelBuilder.Entity<SchemeLoanFunderParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanFunderParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanOverduesAction
            modelBuilder.Entity<SchemeLoanOverduesAction>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanOverduesAction")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanOverduesAction>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanOverduesAction")));

            modelBuilder.Entity<SchemeLoanOverduesActionMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanOverduesActionMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanInstallmentParameter
            modelBuilder.Entity<SchemeLoanInstallmentParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInstallmentParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanInstallmentParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanInstallmentParameter")));

            modelBuilder.Entity<SchemeLoanInstallmentParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInstallmentParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeTargetGroup
            modelBuilder.Entity<SchemeTargetGroup>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTargetGroup")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTargetGroup>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTargetGroup")));

            modelBuilder.Entity<SchemeTargetGroupMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTargetGroupMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeTargetGroupOccupation
            modelBuilder.Entity<SchemeTargetGroupOccupation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTargetGroupOccupation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTargetGroupOccupation>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTargetGroupOccupation")));

            modelBuilder.Entity<SchemeTargetGroupOccupationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTargetGroupOccupationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeTargetGroupGender
            modelBuilder.Entity<SchemeTargetGroupGender>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTargetGroupGender")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeTargetGroupGender>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeTargetGroupGender")));

            modelBuilder.Entity<SchemeTargetGroupGenderMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeTargetGroupGenderMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanAccountParameter
            modelBuilder.Entity<SchemeLoanAccountParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAccountParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanAccountParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanAccountParameter")));

            modelBuilder.Entity<SchemeLoanAccountParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAccountParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanArrearParameter
            modelBuilder.Entity<SchemeLoanArrearParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanArrearParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanArrearParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanArrearParameter")));

            modelBuilder.Entity<SchemeLoanArrearParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanArrearParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanInterestParameter
            modelBuilder.Entity<SchemeLoanInterestParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInterestParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanInterestParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanInterestParameter")));

            modelBuilder.Entity<SchemeLoanInterestParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInterestParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanSanctionAuthority
            modelBuilder.Entity<SchemeLoanSanctionAuthority>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanSanctionAuthority")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanSanctionAuthority>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanSanctionAuthority")));

            modelBuilder.Entity<SchemeLoanSanctionAuthorityMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanSanctionAuthorityMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // SchemeCashCreditLoanParameter
            modelBuilder.Entity<SchemeCashCreditLoanParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeCashCreditLoanParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeCashCreditLoanParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeCashCreditLoanParameter")));

            modelBuilder.Entity<SchemeCashCreditLoanParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeCashCreditLoanParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeHomeLoan
            modelBuilder.Entity<SchemeHomeLoan>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeHomeLoan")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeHomeLoan>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeHomeLoan")));

            modelBuilder.Entity<SchemeHomeLoanMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeHomeLoanMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanAgainstProperty
            modelBuilder.Entity<SchemeLoanAgainstProperty>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgainstProperty")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanAgainstProperty>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanAgainstProperty")));

            modelBuilder.Entity<SchemeLoanAgainstPropertyMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgainstPropertyMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeBusinessLoan
            modelBuilder.Entity<SchemeBusinessLoan>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeBusinessLoan")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeBusinessLoan>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeBusinessLoan")));

            modelBuilder.Entity<SchemeBusinessLoanMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeBusinessLoanMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanRecoveryAction
            modelBuilder.Entity<SchemeLoanRecoveryAction>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanRecoveryAction")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanRecoveryAction>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanRecoveryAction")));

            modelBuilder.Entity<SchemeLoanRecoveryActionMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanRecoveryActionMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanPaymentReminderParameter
            modelBuilder.Entity<SchemeLoanPaymentReminderParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanPaymentReminderParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanPaymentReminderParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanPaymentReminderParameter")));

            modelBuilder.Entity<SchemeLoanPaymentReminderParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanPaymentReminderParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // VehicleTypeLoanParameter
            modelBuilder.Entity<SchemeVehicleTypeLoanParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeVehicleTypeLoanParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeVehicleTypeLoanParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeVehicleTypeLoanParameter")));

            modelBuilder.Entity<SchemeVehicleTypeLoanParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeVehicleTypeLoanParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemePreownedVehicleLoanParameter
            modelBuilder.Entity<SchemePreownedVehicleLoanParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemePreownedVehicleLoanParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemePreownedVehicleLoanParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemePreownedVehicleLoanParameter")));

            modelBuilder.Entity<SchemePreownedVehicleLoanParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemePreownedVehicleLoanParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //ConsumerDurableLoanItem
            modelBuilder.Entity<SchemeConsumerDurableLoanItem>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeConsumerDurableLoanItem")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeConsumerDurableLoanItem>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeConsumerDurableLoanItem")));

            modelBuilder.Entity<SchemeConsumerDurableLoanItemMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeConsumerDurableLoanItemMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            //SchemeLoanAgainstDepositGeneralLedger

            modelBuilder.Entity<SchemeLoanAgainstDepositGeneralLedger>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgainstDepositGeneralLedger")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanAgainstDepositGeneralLedger>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanAgainstDepositGeneralLedger")));

            modelBuilder.Entity<SchemeLoanAgainstDepositGeneralLedgerMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgainstDepositGeneralLedgerMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            //SchemeLoanAgainstDepositParameter
            modelBuilder.Entity<SchemeLoanAgainstDepositParameter>()
                    .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgainstDepositParameter")
                   .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanAgainstDepositParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanAgainstDepositParameter")));

            modelBuilder.Entity<SchemeLoanAgainstDepositParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgainstDepositParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));



            // SchemeLoanFineInterestParameter
            modelBuilder.Entity<SchemeLoanFineInterestParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanFineInterestParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanFineInterestParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanFineInterestParameter")));

            modelBuilder.Entity<SchemeLoanFineInterestParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanFineInterestParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanInterestProvisionParameter
            modelBuilder.Entity<SchemeLoanInterestProvisionParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInterestProvisionParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanInterestProvisionParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanInterestProvisionParameter")));

            modelBuilder.Entity<SchemeLoanInterestProvisionParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInterestProvisionParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanRepaymentScheduleParameter
            modelBuilder.Entity<SchemeLoanRepaymentScheduleParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanRepaymentScheduleParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanRepaymentScheduleParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanRepaymentScheduleParameter")));

            modelBuilder.Entity<SchemeLoanRepaymentScheduleParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanRepaymentScheduleParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanSettlementAccountParameter
            modelBuilder.Entity<SchemeLoanSettlementAccountParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanSettlementAccountParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanSettlementAccountParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanSettlementAccountParameter")));

            modelBuilder.Entity<SchemeLoanSettlementAccountParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanSettlementAccountParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanInterestRebateParameter
            modelBuilder.Entity<SchemeLoanInterestRebateParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInterestRebateParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanInterestRebateParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanInterestRebateParameter")));

            modelBuilder.Entity<SchemeLoanInterestRebateParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanInterestRebateParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemePreFullPaymentParameter
            modelBuilder.Entity<SchemeLoanPreFullPaymentParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanPreFullPaymentParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanPreFullPaymentParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanPreFullPaymentParameter")));

            modelBuilder.Entity<SchemeLoanPreFullPaymentParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanPreFullPaymentParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemePrePartPaymentParameter
            modelBuilder.Entity<SchemeLoanPrePartPaymentParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanPrePartPaymentParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanPrePartPaymentParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanPrePartPaymentParameter")));

            modelBuilder.Entity<SchemeLoanPrePartPaymentParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanPrePartPaymentParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeEducationLoanParameter
            modelBuilder.Entity<SchemeEducationLoanParameter>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeEducationLoanParameter")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeEducationLoanParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeEducationLoanParameter")));

            modelBuilder.Entity<SchemeEducationLoanParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeEducationLoanParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeInstitute
            modelBuilder.Entity<SchemeInstitute>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeInstitute")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeInstitute>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeInstitute")));

            modelBuilder.Entity<SchemeInstituteMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeInstituteMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //SchemeEducationalCourse
            modelBuilder.Entity<SchemeEducationalCourse>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeEducationalCourse")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeEducationalCourse>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeEducationalCourse")));

            modelBuilder.Entity<SchemeEducationalCourseMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeEducationalCourseMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeGoldLoanParameter
            modelBuilder.Entity<SchemeGoldLoanParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeGoldLoanParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeGoldLoanParameter>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeGoldLoanParameter")));

            modelBuilder.Entity<SchemeGoldLoanParameterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeGoldLoanParameterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // SchemeLoanAgreementNumber
            modelBuilder.Entity<SchemeLoanAgreementNumber>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgreementNumber")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeLoanAgreementNumber>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeLoanAgreementNumber")));

            modelBuilder.Entity<SchemeLoanAgreementNumberMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeLoanAgreementNumberMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // SchemeInterestRate
            modelBuilder.Entity<SchemeInterestRate>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeInterestRate")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SchemeInterestRate>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchemeInterestRate")));

            modelBuilder.Entity<SchemeInterestRateMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchemeInterestRateMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));


            // ############################# Transaction #############################

            modelBuilder.Entity<TransactionParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionParameter")));

            modelBuilder.Entity<TransactionParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));
            //---------------------------------------------------------------
            modelBuilder.Entity<TransactionMaster>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionMaster")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionMaster>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionMaster")));

            modelBuilder.Entity<TransactionMasterMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionMasterMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));
            //--------------------------------------------------------------
            modelBuilder.Entity<TransactionCustomerAccount>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionCustomerAccount")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionCustomerAccount>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionCustomerAccount")));

            modelBuilder.Entity<TransactionCustomerAccountMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionCustomerAccountMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));
            //-----------------------------------------------------------------------
            modelBuilder.Entity<TransactionCustomerAccountOtherSubscription>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionCustomerAccountOtherSubscription")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionCustomerAccountOtherSubscription>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionCustomerAccountOtherSubscription")));

            modelBuilder.Entity<TransactionCustomerAccountOtherSubscriptionMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionCustomerAccountOtherSubscriptionMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            //--------------------------------------------------------------------
            modelBuilder.Entity<SharesCessationTransaction>()
                    .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCessationTransaction")
                   .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesCessationTransaction>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesCessationTransaction")));

            modelBuilder.Entity<SharesCessationTransactionMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCessationTransactionMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            //-------------------------------------------------------------------------

            modelBuilder.Entity<TransactionGeneralLedger>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionGeneralLedger")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionGeneralLedger>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionGeneralLedger")));

            modelBuilder.Entity<TransactionGeneralLedgerMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionGeneralLedgerMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            //----------------------------------------------------------------------------
            modelBuilder.Entity<TransactionGSTDetail>()
                 .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionGSTDetail")
                 .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionGSTDetail>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionGSTDetail")));

            modelBuilder.Entity<TransactionGSTDetailMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionGSTDetailMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));


            //-------------------------------------------------------------------
            modelBuilder.Entity<SharesCapitalTransaction>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCapitalTransaction")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesCapitalTransaction>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesCapitalTransaction")));

            modelBuilder.Entity<SharesCapitalTransactionMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCapitalTransactionMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            //----------------------------------------------------------------------
            modelBuilder.Entity<TransactionCashDenomination>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionCashDenomination")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionCashDenomination>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionCashDenomination")));

            modelBuilder.Entity<TransactionCashDenominationMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionCashDenominationMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            //--------------------------------------------------------------------------------

            modelBuilder.Entity<OpeningBalance>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOpeningBalance")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OpeningBalance>()
                .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateOpeningBalance")));

            modelBuilder.Entity<OpeningBalanceMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOpeningBalanceMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OpeningBalanceDeposit>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOpeningBalanceDeposit")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OpeningBalanceLoan>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOpeningBalanceLoan")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OpeningBalanceShare>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOpeningBalanceShares")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OpeningBalanceInvestment>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOpeningBalanceInvestment")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OpeningBalanceDeposit>()
                .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateOpeningBalanceDeposit")));

            modelBuilder.Entity<OpeningBalanceLoan>()
                .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateOpeningBalanceLoan")));

            modelBuilder.Entity<OpeningBalanceShare>()
                .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateOpeningBalanceShares")));

            modelBuilder.Entity<OpeningBalanceInvestment>()
                .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateOpeningBalanceInvestment")));

            // ###################### SHARES ######################################
            // ************* InwardOutwardType *************
            modelBuilder.Entity<InwardOutwardType>()
                   .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardOutwardType")
                   .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardOutwardType>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateInwardOutwardType")));

            modelBuilder.Entity<InwardOutwardType>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteInwardOutwardType")));

            modelBuilder.Entity<InwardOutwardTypeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardOutwardTypeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardOutwardTypeModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardOutwardTypeModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardOutwardTypeModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateInwardOutwardTypeModification")));

            modelBuilder.Entity<InwardOutwardTypeModification>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteInwardOutwardTypeModification")));

            modelBuilder.Entity<InwardOutwardTypeModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardOutwardTypeModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardOutwardTypeTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardOutwardTypeTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardOutwardTypeTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateInwardOutwardTypeTranslation")));

            modelBuilder.Entity<InwardOutwardTypeTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteInwardOutwardTypeTranslation")));

            modelBuilder.Entity<InwardOutwardTypeTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardOutwardTypeTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardTransaction>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardTransaction")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardTransaction>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateInwardTransaction")));

            modelBuilder.Entity<InwardTransaction>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteInwardTransaction")));

            modelBuilder.Entity<InwardTransactionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardTransactionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardTransactionTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardTransactionTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardTransactionTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateInwardTransactionTranslation")));

            modelBuilder.Entity<InwardTransactionTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteInwardTransactionTranslation")));

            modelBuilder.Entity<InwardTransactionTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardTransactionTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardTransactionDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardTransactionDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InwardTransactionDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateInwardTransactionDetail")));

            modelBuilder.Entity<InwardTransactionDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteInwardTransactionDetail")));

            modelBuilder.Entity<InwardTransactionDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInwardTransactionDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OutwardTransaction>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOutwardTransaction")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OutwardTransaction>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOutwardTransaction")));

            modelBuilder.Entity<OutwardTransaction>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteOutwardTransaction")));

            modelBuilder.Entity<OutwardTransactionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOutwardTransactionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OutwardTransactionTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOutwardTransactionTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OutwardTransactionTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOutwardTransactionTranslation")));

            modelBuilder.Entity<OutwardTransactionTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteOutwardTransactionTranslation")));

            modelBuilder.Entity<OutwardTransactionTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOutwardTransactionTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OutwardTransactionDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOutwardTransactionDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OutwardTransactionDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOutwardTransactionDetail")));

            modelBuilder.Entity<OutwardTransactionDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteOutwardTransactionDetail")));

            modelBuilder.Entity<OutwardTransactionDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOutwardTransactionDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ************* SharesApplication *************
            modelBuilder.Entity<SharesApplication>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplication")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplication>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesApplication")));

            modelBuilder.Entity<SharesApplication>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteSharesApplication")));

            modelBuilder.Entity<SharesApplicationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplicationModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplicationModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesApplicationModification")));

            modelBuilder.Entity<SharesApplicationModification>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteSharesApplicationModification")));

            modelBuilder.Entity<SharesApplicationModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplicationTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplicationTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesApplicationTranslation")));

            modelBuilder.Entity<SharesApplicationTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteSharesApplicationTranslation")));

            modelBuilder.Entity<SharesApplicationTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplicationDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesApplicationDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesApplicationDetail")));

            modelBuilder.Entity<SharesApplicationDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteSharesApplicationDetail")));

            modelBuilder.Entity<SharesApplicationDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesApplicationDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));



            // @@@@@@@@@@@@@@@@@@@@@ A P P     S U P P O R T E D @@@@@@@@@@@@@@@@@@@@@



            // @@@@@@@@@@@@@@@@@@@@@ A R C H I V E @@@@@@@@@@@@@@@@@@@@@


            // @@@@@@@@@@@@@@@@@@@@@ C O M M U N I C A T I O N @@@@@@@@@@@@@@@@@@@@@



            // @@@@@@@@@@@@@@@@@@@@@ C O N F I G U R A T I O N @@@@@@@@@@@@@@@@@@@@@


            // @@@@@@@@@@@@@@@@@@@@@ E N T E R P R I S E @@@@@@@@@@@@@@@@@@@@@

            // ############################# CAPITAL #############################
            modelBuilder.Entity<AuthorizedSharesCapital>()
                     .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAuthorizedSharesCapital")
                    .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AuthorizedSharesCapital>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateAuthorizedSharesCapital")));

            modelBuilder.Entity<AuthorizedSharesCapitalMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAuthorizedSharesCapitalMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# ESTABLISHMENT #############################
            modelBuilder.Entity<Organization>()
                         .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganization")
                        .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Organization>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganization")));

            modelBuilder.Entity<OrganizationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // OrganizationTranslation
            modelBuilder.Entity<OrganizationTranslation>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationTranslation>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationTranslation")));

            modelBuilder.Entity<OrganizationTranslationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationTranslationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // OrganizationContactDetail
            modelBuilder.Entity<OrganizationContactDetail>()
                      .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationContactDetail")
                     .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationContactDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationContactDetail")));

            modelBuilder.Entity<OrganizationContactDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationContactDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // OrganizationFund
            modelBuilder.Entity<OrganizationFund>()
                         .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationFund")
                        .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationFund>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationFund")));

            modelBuilder.Entity<OrganizationFundMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationFundMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationFundTranslation>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationFundTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationFundTranslation>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationFundTranslation")));

            modelBuilder.Entity<OrganizationFundTranslationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationFundTranslationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // OrganizationLoanType
            modelBuilder.Entity<OrganizationLoanType>()
                         .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationLoantype")
                        .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationLoanType>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationLoanType")));

            modelBuilder.Entity<OrganizationLoanTypeMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationLoanTypeMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationLoanTypeTranslation>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationLoanTypeTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationLoanTypeTranslation>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationLoanTypeTranslation")));

            modelBuilder.Entity<OrganizationLoanTypeTranslationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationLoanTypeTranslationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            // OrganizationGSTRegistrationDetail
            modelBuilder.Entity<OrganizationGSTRegistrationDetail>()
                      .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationGSTRegistrationDetail")
                     .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OrganizationGSTRegistrationDetail>()
             .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOrganizationGSTRegistrationDetail")));

            modelBuilder.Entity<OrganizationGSTRegistrationDetailMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOrganizationGSTRegistrationDetailMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));



            // ########################### OFFICE ##############################

            //***************BusinessOffice * ************
            modelBuilder.Entity<BusinessOffice>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOffice")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOffice>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOffice")));

            modelBuilder.Entity<BusinessOfficeTranslation>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeTranslation")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeTranslation>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeTranslation")));

            modelBuilder.Entity<BusinessOfficeTranslationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeTranslationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeDetail *************
            modelBuilder.Entity<BusinessOfficeDetail>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeDetail")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeDetail>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeDetail")));

            modelBuilder.Entity<BusinessOfficeDetailMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeDetailMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeModification *************
            modelBuilder.Entity<BusinessOfficeModification>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeModification")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeModification>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateBusinessOfficeModification")));

            modelBuilder.Entity<BusinessOfficeModificationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeModificationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeCoopRegistration *************
            modelBuilder.Entity<BusinessOfficeCoopRegistration>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCoopRegistration")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeCoopRegistration>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateBusinessOfficeCoopRegistration")));

            modelBuilder.Entity<BusinessOfficeCoopRegistrationTranslation>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCoopRegistrationTranslation")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeCoopRegistrationTranslation>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateBusinessOfficeCoopRegistrationTranslation")));

            modelBuilder.Entity<BusinessOfficeCoopRegistrationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCoopRegistrationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeCoopRegistrationTranslationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCoopRegistrationTranslationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficePasswordPolicy *************
            modelBuilder.Entity<BusinessOfficePasswordPolicy>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficePasswordPolicy")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficePasswordPolicy>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateBusinessOfficePasswordPolicy")));

            modelBuilder.Entity<BusinessOfficePasswordPolicyMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficePasswordPolicyMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeRBIRegistration *************
            modelBuilder.Entity<BusinessOfficeRBIRegistration>()
          .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeRBIRegistration")
          .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeRBIRegistration>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateBusinessOfficeRBIRegistration")));

            modelBuilder.Entity<BusinessOfficeRBIRegistrationTranslation>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeRBIRegistrationTranslation")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeRBIRegistrationTranslation>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateBusinessOfficeRBIRegistrationTranslation")));

            modelBuilder.Entity<BusinessOfficeRBIRegistrationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeRBIRegistrationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeRBIRegistrationTranslationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeRBIRegistrationTranslationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeMenu *************
            modelBuilder.Entity<BusinessOfficeMenu>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeMenu")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeMenu>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeMenu")));

            modelBuilder.Entity<BusinessOfficeMenuMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeMenuMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeSpecialPermission *************
            modelBuilder.Entity<BusinessOfficeSpecialPermission>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeSpecialPermission")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeSpecialPermission>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeSpecialPermission")));

            modelBuilder.Entity<BusinessOfficeSpecialPermissionMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeSpecialPermissionMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeTransactionLimit *************
            modelBuilder.Entity<BusinessOfficeTransactionLimit>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeTransactionLimit")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeTransactionLimit>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeTransactionLimit")));

            modelBuilder.Entity<BusinessOfficeTransactionLimitMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeTransactionLimitMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeTransactionParameter *************
            modelBuilder.Entity<BusinessOfficeTransactionParameter>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeTransactionParameter")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeTransactionParameter>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeTransactionParameter")));

            modelBuilder.Entity<BusinessOfficeTransactionParameterMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeTransactionParameterMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeAccountNumber *************
            modelBuilder.Entity<BusinessOfficeAccountNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeAccountNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeAccountNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeAccountNumber")));

            modelBuilder.Entity<BusinessOfficeAccountNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeAccountNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeAgreementNumber *************
            modelBuilder.Entity<BusinessOfficeAgreementNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeAgreementNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeAgreementNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeAgreementNumber")));

            modelBuilder.Entity<BusinessOfficeAgreementNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeAgreementNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeApplicationNumber *************
            modelBuilder.Entity<BusinessOfficeApplicationNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeApplicationNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeApplicationNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeApplicationNumber")));

            modelBuilder.Entity<BusinessOfficeApplicationNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeApplicationNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeCurrency *************
            modelBuilder.Entity<BusinessOfficeCurrency>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCurrency")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeCurrency>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeCurrency")));

            modelBuilder.Entity<BusinessOfficeCurrencyMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCurrencyMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeDepositCertificateNumber *************
            modelBuilder.Entity<BusinessOfficeDepositCertificateNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeDepositCertificateNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeDepositCertificateNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeCurrency")));

            modelBuilder.Entity<BusinessOfficeDepositCertificateNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeDepositCertificateNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeSharesCertificateNumber *************
            modelBuilder.Entity<BusinessOfficeSharesCertificateNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeSharesCertificateNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeSharesCertificateNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeSharesCertificateNumber")));

            modelBuilder.Entity<BusinessOfficeSharesCertificateNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeSharesCertificateNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficePersonInformationNumber *************
            modelBuilder.Entity<BusinessOfficePersonInformationNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficePersonInformationNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficePersonInformationNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficePersonInformationNumber")));

            modelBuilder.Entity<BusinessOfficePersonInformationNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficePersonInformationNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficePassbookNumber *************
            modelBuilder.Entity<BusinessOfficePassbookNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficePassbookNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficePassbookNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficePassbookNumber")));

            modelBuilder.Entity<BusinessOfficePassbookNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficePassbookNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));


            //*************** BusinessOfficeCustomerNumber *************
            modelBuilder.Entity<BusinessOfficeCustomerNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCustomerNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeCustomerNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeCustomerNumber")));

            modelBuilder.Entity<BusinessOfficeCustomerNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeCustomerNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** BusinessOfficeMemberNumber *************
            modelBuilder.Entity<BusinessOfficeMemberNumber>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeMemberNumber")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BusinessOfficeMemberNumber>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBusinessOfficeMemberNumber")));

            modelBuilder.Entity<BusinessOfficeMemberNumberMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBusinessOfficeMemberNumberMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));




            // ############################# SCHEDULE ############################

            modelBuilder.Entity<OfficeSchedule>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOfficeSchedule")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OfficeSchedule>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOfficeSchedule")));

            modelBuilder.Entity<OfficeScheduleMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOfficeScheduleMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OfficeScheduleTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOfficeScheduleTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OfficeScheduleTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOfficeScheduleTranslation")));

            modelBuilder.Entity<OfficeScheduleTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOfficeScheduleTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OfficeScheduleModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOfficeScheduleModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<OfficeScheduleModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateOfficeScheduleModification")));

            modelBuilder.Entity<OfficeScheduleModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddOfficeScheduleModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<WorkingSchedule>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddWorkingSchedule")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<WorkingSchedule>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateWorkingSchedule")));

            modelBuilder.Entity<WorkingScheduleMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddWorkingScheduleMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<WorkingScheduleTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddWorkingScheduleTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<WorkingScheduleTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateWorkingScheduleTranslation")));

            modelBuilder.Entity<WorkingScheduleTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddWorkingScheduleTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<WorkingScheduleModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddWorkingScheduleModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<WorkingScheduleModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateWorkingScheduleModification")));

            modelBuilder.Entity<WorkingScheduleModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddWorkingScheduleModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));



            // @@@@@@@@@@@@@@@@@@@@@@@@@@@ H E A L T H  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@




            // @@@@@@@@@@@@@@@@@@@@ H U M A N     R E S O U R C E S @@@@@@@@@@@@@@@@@@
            // ###################################### SERVANT ####################################################
            modelBuilder.Entity<Employee>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployee")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Employee>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployee")));

            modelBuilder.Entity<EmployeeMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeModification>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeModification")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeModification>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateEmployeeModification")));

            modelBuilder.Entity<EmployeeModificationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeModificationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDepartment>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDepartment")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDepartment>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeeDepartment")));

            modelBuilder.Entity<EmployeeDepartmentMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDepartmentMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDesignation>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDesignation")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDesignation>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateEmployeeDesignation")));

            modelBuilder.Entity<EmployeeDesignationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDesignationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDetail>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDetail")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDetail>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeeDetail")));

            modelBuilder.Entity<EmployeeDetailMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDetailMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDocument>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDocument")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeDocument>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeeDocument")));

            modelBuilder.Entity<EmployeeDocumentMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeDocumentMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeePerformanceRating>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeePerformanceRating")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeePerformanceRating>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeePerformanceRating")));

            modelBuilder.Entity<EmployeePerformanceRatingMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeePerformanceRatingMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeePhoto>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeePhoto")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeePhoto>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeePhoto")));

            modelBuilder.Entity<EmployeePhotoMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeePhotoMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeSalaryStructure>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeSalaryStructure")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeSalaryStructure>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeeSalaryStructure")));

            modelBuilder.Entity<EmployeeSalaryStructureMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeSalaryStructureMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeWorkingSchedule>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeWorkingSchedule")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EmployeeWorkingSchedule>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEmployeeWorkingSchedule")));

            modelBuilder.Entity<EmployeeWorkingScheduleMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEmployeeWorkingScheduleMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@ EvaluationSection @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<EvaluationSection>()
                   .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSection")
                   .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EvaluationSection>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEvaluationSection")));

            modelBuilder.Entity<EvaluationSectionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EvaluationSectionModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectionModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EvaluationSectionModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEvaluationSectionModification")));

            modelBuilder.Entity<EvaluationSectionModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectionModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EvaluationSectionTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectionTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EvaluationSectionTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEvaluationSectionTranslation")));

            modelBuilder.Entity<EvaluationSectionTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectionTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@ contentItem @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<ContentItem>()
                   .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddContentItem")
                   .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ContentItem>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateContentItem")));

            modelBuilder.Entity<ContentItemMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddContentItemMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ContentItemModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddContentItemModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ContentItemModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateContentItemModification")));

            modelBuilder.Entity<ContentItemModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddContentItemModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ContentItemTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddContentItemTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ContentItemTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateContentItemTranslation")));

            modelBuilder.Entity<ContentItemTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddContentItemTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@ EvaluationSectorContentItem @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<EvaluationSectorContentItem>()
                   .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectorContentItem")
                   .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EvaluationSectorContentItem>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEvaluationSectorContentItem")));

            modelBuilder.Entity<EvaluationSectorContentItemMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEvaluationSectorContentItemMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            //*************************************** MACHINE LEARNING *************************************

            //*************************************** Management *************************************
            // ###################################### Conference ####################################################
            //**************************************** Meeting *******************************
            modelBuilder.Entity<Meeting>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeeting")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Meeting>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeeting")));

            modelBuilder.Entity<MeetingMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingModification>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingModification")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingModification>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingModification")));

            modelBuilder.Entity<MeetingModificationMakerChecker>()
                 .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingModificationMakerChecker")
                 .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingTranslation>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingTranslation")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingTranslation>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingTranslation")));

            modelBuilder.Entity<MeetingTranslationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingTranslationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MeetingAllowance *******************************
            modelBuilder.Entity<MeetingAllowance>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAllowance")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingAllowance>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingAllowance")));

            modelBuilder.Entity<MeetingAllowanceMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAllowanceMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingAllowanceModification>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAllowanceModification")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingAllowanceModification>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingAllowanceModification")));

            modelBuilder.Entity<MeetingAllowanceModificationMakerChecker>()
                 .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAllowanceModificationMakerChecker")
                 .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingAllowanceTranslation>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAllowanceTranslation")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingAllowanceTranslation>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingAllowanceTranslation")));

            modelBuilder.Entity<MeetingAllowanceTranslationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAllowanceTranslationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MeetingAgenda *******************************
            modelBuilder.Entity<MeetingAgenda>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAgenda")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingAgenda>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingAgenda")));

            modelBuilder.Entity<MeetingAgendaMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingAgendaMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MeetingInviteeBoardOfDirector *******************************
            modelBuilder.Entity<MeetingInviteeBoardOfDirector>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingInviteeBoardOfDirector")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingInviteeBoardOfDirector>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingInviteeBoardOfDirector")));

            modelBuilder.Entity<MeetingInviteeBoardOfDirectorMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingInviteeBoardOfDirectorMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MeetingInviteeMember *******************************
            modelBuilder.Entity<MeetingInviteeMember>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingInviteeMember")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingInviteeMember>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingInviteeMember")));

            modelBuilder.Entity<MeetingInviteeMemberMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingInviteeMemberMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MeetingNotice *******************************
            modelBuilder.Entity<MeetingNotice>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingNotice")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MeetingNotice>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMeetingNotice")));

            modelBuilder.Entity<MeetingNoticeMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMeetingNoticeMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MinuteOfMeetingAgenda *******************************
            modelBuilder.Entity<MinuteOfMeetingAgenda>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMinuteOfMeetingAgenda")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MinuteOfMeetingAgenda>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMinuteOfMeetingAgenda")));

            modelBuilder.Entity<MinuteOfMeetingAgendaMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMinuteOfMeetingAgendaMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            //**************************************** MinuteOfMeetingSpokesperson *******************************
            modelBuilder.Entity<MinuteOfMeetingAgendaSpokesperson>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMinuteOfMeetingAgendaSpokesperson")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MinuteOfMeetingAgendaSpokesperson>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMinuteOfMeetingAgendaSpokesperson")));

            modelBuilder.Entity<MinuteOfMeetingAgendaSpokespersonMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMinuteOfMeetingAgendaSpokespersonMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MinuteOfMeetingAgendaSpokespersonTranslation>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMinuteOfMeetingAgendaSpokespersonTranslation")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<MinuteOfMeetingAgendaSpokespersonTranslation>()
                .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateMinuteOfMeetingAgendaSpokespersonTranslation")));

            modelBuilder.Entity<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddMinuteOfMeetingAgendaSpokespersonTranslationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Notification #############################
            // @@@@@@@@@@@@@@@@@@@@@ Event @@@@@@@@@@@@@@@@@@@@

            modelBuilder.Entity<EventMaster>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEventMaster")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EventMaster>()
           .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEventMaster")));

            modelBuilder.Entity<EventMaster>()
           .MapToStoredProcedures(sp => sp.Delete(i => i.HasName("Usp_DeleteEventMaster")));

            modelBuilder.Entity<EventMasterTranslation>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEventMasterTranslation")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EventMasterTranslation>()
           .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEventMasterTranslation")));

            modelBuilder.Entity<EventMasterTranslation>()
           .MapToStoredProcedures(sp => sp.Delete(i => i.HasName("Usp_DeleteEventMasterTranslation")));

            modelBuilder.Entity<EventMasterModification>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEventMasterModification")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EventMasterModification>()
           .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEventMasterModification")));

            modelBuilder.Entity<EventMasterModificationMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEventMasterModificationMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EventRepeatReminder>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEventRepeatReminder")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<EventRepeatReminder>()
           .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateEventRepeatReminder")));

            modelBuilder.Entity<EventRepeatReminderMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddEventRepeatReminderMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@ M A S T E R @@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //##################################Account###########################

            //*************** Responsibility *************
            modelBuilder.Entity<Responsibility>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddResponsibility")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Responsibility>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateResponsibility")));

            modelBuilder.Entity<ResponsibilityMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddResponsibilityMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ResponsibilityModification>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddResponsibilityModification")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ResponsibilityModification>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateResponsibilityModification")));

            modelBuilder.Entity<ResponsibilityModificationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddResponsibilityModificationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ResponsibilityTranslation>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddResponsibilityTranslation")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ResponsibilityTranslation>()
            .MapToStoredProcedures(sp => sp.Update(i => i.HasName("Usp_UpdateResponsibilityTranslation")));

            modelBuilder.Entity<ResponsibilityTranslationMakerChecker>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddResponsibilityTranslationMakerChecker")
            .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@ Center @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<Center>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenter")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Center>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenter")));

            modelBuilder.Entity<CenterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenterTranslation")));

            modelBuilder.Entity<CenterTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterModification>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenterModification")));

            modelBuilder.Entity<CenterModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterDemographicDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterDemographicDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterDemographicDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenterDemographicDetail")));

            modelBuilder.Entity<CenterDemographicDetailMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterDemographicDetailMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterISOCode>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterISOCode")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterISOCode>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenterISOCode")));

            modelBuilder.Entity<CenterISOCodeMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterISOCodeMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterOccupation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterOccupation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterOccupation>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenterOccupation")));

            modelBuilder.Entity<CenterOccupationMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterOccupationMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterTradingEntityDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterTradingEntityDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CenterTradingEntityDetail>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCenterTradingEntityDetail")));

            modelBuilder.Entity<CenterTradingEntityDetailMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCenterTradingEntityDetailMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CountryAdditionalDetail>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCountryAdditionalDetail")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CountryAdditionalDetail>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCountryAdditionalDetail")));

            modelBuilder.Entity<CountryAdditionalDetailMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCountryAdditionalDetailMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Bank Detail #############################
            // @@@@@@@@@@@@@@@@@@@@@ CreditSociety @@@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<CreditSociety>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCreditSociety")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CreditSociety>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCreditSociety")));

            modelBuilder.Entity<CreditSocietyMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCreditSocietyMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CreditSocietyTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCreditSocietyTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CreditSocietyTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCreditSocietyTranslation")));

            modelBuilder.Entity<CreditSocietyTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCreditSocietyTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // @@@@@@@@@@@@@@@@@@@@@ PowerAndFunction @@@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<PowerAndFunction>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPowerAndFunction")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PowerAndFunction>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePowerAndFunction")));

            modelBuilder.Entity<PowerAndFunctionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPowerAndFunctionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PowerAndFunctionTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPowerAndFunctionTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PowerAndFunctionTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePowerAndFunctionTranslation")));

            modelBuilder.Entity<PowerAndFunctionTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPowerAndFunctionTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@ BoardOfDirector @@@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<BoardOfDirector>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirector")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BoardOfDirector>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBoardOfDirector")));

            modelBuilder.Entity<BoardOfDirectorMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // @@@@@@@@@@@@@@@@@@@@@ BoardOfDirectorPowerAndFunction @@@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<BoardOfDirectorPowerAndFunction>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorPowerAndFunction")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BoardOfDirectorPowerAndFunction>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBoardOfDirectorPowerAndFunction")));

            modelBuilder.Entity<BoardOfDirectorPowerAndFunctionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorPowerAndFunctionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BoardOfDirectorPowerAndFunctionTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorPowerAndFunctionTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BoardOfDirectorPowerAndFunctionTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBoardOfDirectorPowerAndFunctionTranslation")));

            modelBuilder.Entity<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorPowerAndFunctionTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // ############################# General #############################

            // @@@@@@@@@@@@@@@@@@@@@ Agenda @@@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<Agenda>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgenda")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Agenda>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateAgenda")));

            modelBuilder.Entity<AgendaMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AgendaTranslation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaTranslation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AgendaTranslation>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateAgendaTranslation")));

            modelBuilder.Entity<AgendaTranslationMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaTranslationMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AgendaModification>()
                          .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaModification")
                         .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AgendaModification>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateAgendaModification")));

            modelBuilder.Entity<AgendaModificationMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaModificationMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AgendaMeetingType>()
                          .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaMeetingType")
                         .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AgendaMeetingType>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateAgendaMeetingType")));

            modelBuilder.Entity<AgendaMeetingTypeMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAgendaMeetingTypeMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));


            // @@@@@@@@@@@@@@@@@@@@@ Department @@@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<Department>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepartment")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Department>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDepartment")));

            modelBuilder.Entity<DepartmentMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepartmentMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DepartmentTranslation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepartmentTranslation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DepartmentTranslation>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDepartmentTranslation")));

            modelBuilder.Entity<DepartmentTranslationMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepartmentTranslationMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DepartmentModification>()
                          .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepartmentModification")
                         .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DepartmentModification>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDepartmentModification")));

            modelBuilder.Entity<DepartmentModificationMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepartmentModificationMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));


            // @@@@@@@@@@@@@@@@@@@@@ Designation @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<Designation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDesignation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Designation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDesignation")));

            modelBuilder.Entity<DesignationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDesignationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DesignationModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDesignationModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DesignationModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDesignationModification")));

            modelBuilder.Entity<DesignationModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDesignationModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DesignationTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDesignationTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DesignationTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDesignationTranslation")));

            modelBuilder.Entity<DesignationTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDesignationTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // ################### Schedule  ###################

            modelBuilder.Entity<Schedule>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSchedule")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Schedule>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSchedule")));

            modelBuilder.Entity<Schedule>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteSchedule")));

            modelBuilder.Entity<ScheduleMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ScheduleFrequency>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleFrequency")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ScheduleFrequency>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateScheduleFrequency")));

            modelBuilder.Entity<ScheduleFrequency>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteScheduleFrequency")));

            modelBuilder.Entity<ScheduleFrequencyMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleFrequencyMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ScheduleModification>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleModification")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ScheduleModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateScheduleModification")));

            modelBuilder.Entity<ScheduleModification>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteScheduleModification")));

            modelBuilder.Entity<ScheduleModificationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleModificationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ScheduleTranslation>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleTranslation")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ScheduleTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateScheduleTranslation")));

            modelBuilder.Entity<ScheduleTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteScheduleTranslation")));

            modelBuilder.Entity<ScheduleTranslationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddScheduleTranslationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));


            // @@@@@@@@@@@@@@@@@@@@@ SMS  @@@@@@@@@@@@@@@@@@@@
            modelBuilder.Entity<DLTHeaderRegistration>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDLTHeaderRegistration")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DLTHeaderRegistration>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDLTHeaderRegistration")));

            modelBuilder.Entity<DLTHeaderRegistrationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDLTHeaderRegistrationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DLTRegistration>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDLTRegistration")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DLTRegistration>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDLTRegistration")));

            modelBuilder.Entity<DLTRegistrationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDLTRegistrationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DLTTemplateRegistration>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDLTTemplateRegistration")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DLTTemplateRegistration>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDLTTemplateRegistration")));

            modelBuilder.Entity<DLTTemplateRegistrationMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDLTTemplateRegistrationMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));


            // ############################# Vehicle #############################

            // VehicleMake

            modelBuilder.Entity<VehicleMake>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleMake")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleMake>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleMake")));

            modelBuilder.Entity<VehicleMakeMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleMakeMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleMakeModification>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleMakeModification")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleMakeModification>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleMakeModification")));

            modelBuilder.Entity<VehicleMakeModificationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleMakeModificationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleMakeTranslation>().
                MapToStoredProcedures(sp =>
                sp.Insert(i => i.HasName("Usp_AddVehicleMakeTranslation").Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleMakeTranslation>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleMakeTranslation")));

            modelBuilder.Entity<VehicleMakeTranslationMakerChecker>().
               MapToStoredProcedures(sp =>
               sp.Insert(i => i.HasName("Usp_AddVehicleMakeTranslationMakerChecker").Result(rs => rs.PrmKey, "PrmKey")));

            // VehicleModel

            modelBuilder.Entity<VehicleModel>().
                MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleModel")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleModel>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleModel")));

            modelBuilder.Entity<VehicleModelMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleModelMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleModelModification>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleModelModification")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleModelModification>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleModelModification")));

            modelBuilder.Entity<VehicleModelModificationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleModelModificationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleModelTranslation>().
                MapToStoredProcedures(sp =>
                sp.Insert(i => i.HasName("Usp_AddVehicleModelTranslation").Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleModelTranslation>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleModelTranslation")));

            modelBuilder.Entity<VehicleModelTranslationMakerChecker>().
               MapToStoredProcedures(sp =>
               sp.Insert(i => i.HasName("Usp_AddVehicleModelTranslationMakerChecker").Result(rs => rs.PrmKey, "PrmKey")));


            // VehicleVariant

            modelBuilder.Entity<VehicleVariant>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleVariant")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleVariant>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleVariant")));

            modelBuilder.Entity<VehicleVariantMakerChecker>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleVariantMakerChecker")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleVariantModification>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleVariantModification")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleVariantModification>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleVariantModification")));

            modelBuilder.Entity<VehicleVariantModificationMakerChecker>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddVehicleVariantModificationMakerChecker")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleVariantTranslation>().
                MapToStoredProcedures(sp =>
                sp.Insert(i => i.HasName("Usp_AddVehicleVariantTranslation").Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<VehicleVariantTranslation>()
              .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateVehicleVariantTranslation")));

            modelBuilder.Entity<VehicleVariantTranslationMakerChecker>().
               MapToStoredProcedures(sp =>
               sp.Insert(i => i.HasName("Usp_AddVehicleVariantTranslationMakerChecker").Result(rs => rs.PrmKey, "PrmKey")));


            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ P A R A M E T E R @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            // ############################# Account #############################

            //*************** DepositSchemeParameter *************
            modelBuilder.Entity<DepositSchemeParameter>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepositSchemeParameter")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<DepositSchemeParameter>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateDepositSchemeParameter")));

            modelBuilder.Entity<DepositSchemeParameterMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddDepositSchemeParameterMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** LoanSchemeParameter *************
            modelBuilder.Entity<LoanSchemeParameter>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddLoanSchemeParameter")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<LoanSchemeParameter>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateLoanSchemeParameter")));

            modelBuilder.Entity<LoanSchemeParameterMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddLoanSchemeParameterMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** ByLawsLoanScheduleParameter *************
            modelBuilder.Entity<ByLawsLoanScheduleParameter>()
            .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddByLawsLoanScheduleParameter")
            .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ByLawsLoanScheduleParameter>()
            .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateByLawsLoanScheduleParameter")));

            modelBuilder.Entity<ByLawsLoanScheduleParameterMakerChecker>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddByLawsLoanScheduleParameterMakerChecker")
           .Result(rs => rs.PrmKey, "PrmKey")));

            //*************** SharesCapitalSchemeParameter *************
            modelBuilder.Entity<SharesCapitalSchemeParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCapitalSchemeParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesCapitalSchemeParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesCapitalSchemeParameter")));

            modelBuilder.Entity<SharesCapitalSchemeParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCapitalSchemeParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Transaction #############################

            modelBuilder.Entity<TransactionParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TransactionParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateTransactionParameter")));

            modelBuilder.Entity<TransactionParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTransactionParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# BoardOfDirector Parameter #############################
            modelBuilder.Entity<BoardOfDirectorParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<BoardOfDirectorParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateBoardOfDirectorParameter")));

            modelBuilder.Entity<BoardOfDirectorParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddBoardOfDirectorParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Legal #############################
            modelBuilder.Entity<SharesCapitalByLawsParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCapitalByLawsParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<SharesCapitalByLawsParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateSharesCapitalByLawsParameter")));

            modelBuilder.Entity<SharesCapitalByLawsParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSharesCapitalByLawsParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // ######################### PersonInformationParameter #########################

            modelBuilder.Entity<PersonInformationParameter>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInformationParameter")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonInformationParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonInformationParameter")));

            modelBuilder.Entity<PersonInformationParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInformationParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ********************** PersonInformationParameterDocumentType **********************

            modelBuilder.Entity<PersonInformationParameterDocumentType>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInformationParameterDocumentType")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonInformationParameterDocumentType>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonInformationParameterDocumentType")));

            modelBuilder.Entity<PersonInformationParameterDocumentTypeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInformationParameterDocumentTypeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ********************** PersonInformationParameterNoticeType **********************

            modelBuilder.Entity<PersonInformationParameterNoticeType>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInformationParameterNoticeType")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonInformationParameterNoticeType>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonInformationParameterNoticeType")));

            modelBuilder.Entity<PersonInformationParameterNoticeTypeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInformationParameterNoticeTypeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Security #############################
            modelBuilder.Entity<UserAuthenticationParameter>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserAuthenticationParameter")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserAuthenticationParameter>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserAuthenticationParameter")));


            modelBuilder.Entity<UserAuthenticationParameterMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserAuthenticationParameterMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Users #############################

            modelBuilder.Entity<UserAuthenticationToken>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserAuthenticationToken")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfile>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfile")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfile>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfile")));

            modelBuilder.Entity<UserProfile>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfile")));

            modelBuilder.Entity<UserProfileMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileBusinessOffice>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileBusinessOffice")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileBusinessOffice>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileBusinessOffice")));

            modelBuilder.Entity<UserProfileBusinessOffice>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileBusinessOffice")));

            modelBuilder.Entity<UserProfileBusinessOfficeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileBusinessOfficeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileCurrency>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileCurrency")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileCurrency>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileCurrency")));

            modelBuilder.Entity<UserProfileCurrency>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileCurrency")));

            modelBuilder.Entity<UserProfileCurrencyMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileCurrencyMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileGeneralLedger>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileGeneralLedger")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileGeneralLedger>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileGeneralLedger")));

            modelBuilder.Entity<UserProfileGeneralLedger>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileGeneralLedger")));

            modelBuilder.Entity<UserProfileGeneralLedgerMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileGeneralLedgerMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileGroup>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileGroup")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileGroup>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileGroup")));

            modelBuilder.Entity<UserProfileGroup>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileGroup")));

            modelBuilder.Entity<UserProfileGroupMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileGroupMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileHomeBusinessOffice>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileHomeBusinessOffice")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileHomeBusinessOffice>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileHomeBusinessOffice")));

            modelBuilder.Entity<UserProfileHomeBusinessOffice>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileHomeBusinessOffice")));

            modelBuilder.Entity<UserProfileHomeBusinessOfficeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileHomeBusinessOfficeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileIdentity>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileIdentity")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileIdentity>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileIdentity")));

            modelBuilder.Entity<UserProfileIdentity>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileIdentity")));

            modelBuilder.Entity<UserProfileLoginDevice>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileLoginDevice")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileLoginDevice>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileLoginDevice")));

            modelBuilder.Entity<UserProfileLoginDevice>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileLoginDevice")));

            modelBuilder.Entity<UserProfileLoginDeviceMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileLoginDeviceMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileMenu>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileMenu")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileMenu>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileMenu")));

            modelBuilder.Entity<UserProfileMenu>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileMenu")));

            modelBuilder.Entity<UserProfileMenuMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileMenuMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileModification>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileModification")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileModification")));

            modelBuilder.Entity<UserProfileModification>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileModification")));

            modelBuilder.Entity<UserProfileModificationMakerChecker>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileModificationMakerChecker")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfilePassword>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfilePassword")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfilePasswordPolicy>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfilePasswordPolicy")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfilePasswordPolicy>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfilePasswordPolicy")));

            modelBuilder.Entity<UserProfilePasswordPolicy>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfilePasswordPolicy")));

            modelBuilder.Entity<UserProfilePasswordPolicyMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfilePasswordPolicyMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileSpecialPermission>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileSpecialPermission")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileSpecialPermission>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileSpecialPermission")));

            modelBuilder.Entity<UserProfileSpecialPermission>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileSpecialPermission")));

            modelBuilder.Entity<UserProfileSpecialPermissionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileSpecialPermissionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileTransactionLimit>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileTransactionLimit")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserProfileTransactionLimit>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserProfileTransactionLimit")));

            modelBuilder.Entity<UserProfileTransactionLimit>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserProfileTransactionLimit")));

            modelBuilder.Entity<UserProfileTransactionLimitMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserProfileTransactionLimitMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserRoleProfile>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserRoleProfile")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<UserRoleProfile>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateUserRoleProfile")));

            modelBuilder.Entity<UserRoleProfile>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteUserRoleProfile")));

            modelBuilder.Entity<UserRoleProfileMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserRoleProfileMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# ChecksumAlgorithm #############################           


            // @@@@@@@@@@@@@@@@@@ P E R S O N     I N F O R M A T I O N @@@@@@@@@@@@@@@@@@

            // ############################# Person #############################

            modelBuilder.Entity<Person>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPerson")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<Person>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePerson")));

            modelBuilder.Entity<Person>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePerson")));

            modelBuilder.Entity<PersonMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonModification>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonModification")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonModification")));

            modelBuilder.Entity<PersonModification>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonModification")));

            modelBuilder.Entity<PersonModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonTranslation")));

            modelBuilder.Entity<PersonTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonTranslation")));

            modelBuilder.Entity<PersonTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# ForeignerPerson #############################
            modelBuilder.Entity<ForeignerPerson>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddForeignerPerson")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<ForeignerPerson>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateForeignerPerson")));

            modelBuilder.Entity<ForeignerPerson>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteForeignerPerson")));

            modelBuilder.Entity<ForeignerPersonMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddForeignerPersonMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# GuardianPerson #############################
            modelBuilder.Entity<GuardianPerson>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGuardianPerson")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GuardianPerson>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGuardianPerson")));

            modelBuilder.Entity<GuardianPerson>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteGuardianPerson")));

            modelBuilder.Entity<GuardianPersonMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGuardianPersonMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GuardianPersonTranslation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGuardianPersonTranslation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<GuardianPersonTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateGuardianPersonTranslation")));

            modelBuilder.Entity<GuardianPersonTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeleteGuardianPersonTranslation")));

            modelBuilder.Entity<GuardianPersonTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddGuardianPersonTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonAdditionalDetail #############################
            modelBuilder.Entity<PersonAdditionalDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAdditionalDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAdditionalDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAdditionalDetail")));

            modelBuilder.Entity<PersonAdditionalDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAdditionalDetail")));

            modelBuilder.Entity<PersonAdditionalDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAdditionalDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAdditionalDetailTranslation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAdditionalDetailTranslation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAdditionalDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAdditionalDetailTranslation")));

            modelBuilder.Entity<PersonAdditionalDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAdditionalDetailTranslation")));

            modelBuilder.Entity<PersonAdditionalDetailTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAdditionalDetailTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Person Additional Income Detail #############################

            modelBuilder.Entity<PersonAdditionalIncomeDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAdditionalIncomeDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAdditionalIncomeDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAdditionalIncomeDetail")));

            modelBuilder.Entity<PersonAdditionalIncomeDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAdditionalIncomeDetail")));

            modelBuilder.Entity<PersonAdditionalIncomeDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAdditionalIncomeDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonAddress #############################

            modelBuilder.Entity<PersonAddress>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAddress")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAddress>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAddress")));

            modelBuilder.Entity<PersonAddress>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAddress")));

            modelBuilder.Entity<PersonAddressMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAddressMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAddressTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAddressTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAddressTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAddressTranslation")));

            modelBuilder.Entity<PersonAddressTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAddressTranslation")));

            modelBuilder.Entity<PersonAddressTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAddressTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonAgricultureAsset #############################

            modelBuilder.Entity<PersonAgricultureAsset>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAgricultureAsset")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAgricultureAsset>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAgricultureAsset")));

            modelBuilder.Entity<PersonAgricultureAsset>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAgricultureAsset")));

            modelBuilder.Entity<PersonAgricultureAssetMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAgricultureAssetMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAgricultureAssetDocument>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAgricultureAssetDocument")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonAgricultureAssetDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonAgricultureAssetDocument")));

            modelBuilder.Entity<PersonAgricultureAssetDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonAgricultureAssetDocument")));

            modelBuilder.Entity<PersonAgricultureAssetDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonAgricultureAssetDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonBankDetail #############################
            modelBuilder.Entity<PersonBankDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBankDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonBankDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonBankDetail")));

            modelBuilder.Entity<PersonBankDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonBankDetail")));

            modelBuilder.Entity<PersonBankDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBankDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonBankDetailDocument #############################
            modelBuilder.Entity<PersonBankDetailDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBankDetailDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonBankDetailDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonBankDetailDocument")));

            modelBuilder.Entity<PersonBankDetailDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonBankDetailDocument")));

            modelBuilder.Entity<PersonBankDetailDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBankDetailDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Person Group Authorized Signatory #############################
            modelBuilder.Entity<PersonGroupAuthorizedSignatory>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGroupAuthorizedSignatory")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonGroupAuthorizedSignatory>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonGroupAuthorizedSignatory")));

            modelBuilder.Entity<PersonGroupAuthorizedSignatory>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonGroupAuthorizedSignatory")));

            modelBuilder.Entity<PersonGroupAuthorizedSignatoryMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGroupAuthorizedSignatoryMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Person Group Authorized Signatory Translation #############################
            modelBuilder.Entity<PersonGroupAuthorizedSignatoryTranslation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGroupAuthorizedSignatoryTranslation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonGroupAuthorizedSignatoryTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonGroupAuthorizedSignatoryTranslation")));

            modelBuilder.Entity<PersonGroupAuthorizedSignatoryTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonGroupAuthorizedSignatoryTranslation")));

            modelBuilder.Entity<PersonGroupAuthorizedSignatoryTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGroupAuthorizedSignatoryTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            //// ############################# PersonBoardOfDirectorRelation #############################
            modelBuilder.Entity<PersonBoardOfDirectorRelation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBoardOfDirectorRelation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonBoardOfDirectorRelation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonBoardOfDirectorRelation")));

            modelBuilder.Entity<PersonBoardOfDirectorRelation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonBoardOfDirectorRelation")));

            modelBuilder.Entity<PersonBoardOfDirectorRelationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBoardOfDirectorRelationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonBorrowingDetail #############################

            modelBuilder.Entity<PersonBorrowingDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBorrowingDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonBorrowingDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonBorrowingDetail")));

            modelBuilder.Entity<PersonBorrowingDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonBorrowingDetail")));

            modelBuilder.Entity<PersonBorrowingDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBorrowingDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonBorrowingDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBorrowingDetailTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonBorrowingDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonBorrowingDetailTranslation")));

            modelBuilder.Entity<PersonBorrowingDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonBorrowingDetailTranslation")));

            modelBuilder.Entity<PersonBorrowingDetailTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonBorrowingDetailTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonChronicDisease #############################
            modelBuilder.Entity<PersonChronicDisease>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonChronicDisease")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonChronicDisease>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonChronicDisease")));

            modelBuilder.Entity<PersonChronicDisease>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonChronicDisease")));

            modelBuilder.Entity<PersonChronicDiseaseMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonChronicDiseaseMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonCommoditiesAsset #############################
            modelBuilder.Entity<PersonCommoditiesAsset>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCommoditiesAsset")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonCommoditiesAsset>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonCommoditiesAsset")));

            modelBuilder.Entity<PersonCommoditiesAsset>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonCommoditiesAsset")));

            modelBuilder.Entity<PersonCommoditiesAssetMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCommoditiesAssetMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonContactDetail #############################
            modelBuilder.Entity<PersonContactDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonContactDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonContactDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonContactDetail")));

            modelBuilder.Entity<PersonContactDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonContactDetail")));

            modelBuilder.Entity<PersonContactDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonContactDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonCourtCase #############################
            modelBuilder.Entity<PersonCourtCase>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCourtCase")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonCourtCase>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonCourtCase")));

            modelBuilder.Entity<PersonCourtCaseMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCourtCaseMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonCreditRating #############################
            modelBuilder.Entity<PersonCreditRating>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCreditRating")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonCreditRating>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonCreditRating")));

            modelBuilder.Entity<PersonCreditRating>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonCreditRating")));

            modelBuilder.Entity<PersonCreditRatingMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCreditRatingMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonCustomField #############################
            modelBuilder.Entity<PersonCustomField>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCustomField")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonCustomField>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonCustomField")));

            modelBuilder.Entity<PersonCustomField>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonCustomField")));

            modelBuilder.Entity<PersonCustomFieldMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonCustomFieldMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonDeath #############################
            modelBuilder.Entity<PersonDeath>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonDeath")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonDeath>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonDeath")));

            modelBuilder.Entity<PersonDeath>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonDeath")));

            modelBuilder.Entity<PersonDeathMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonDeathMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonDeathDocument #############################
            modelBuilder.Entity<PersonDeathDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonDeathDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonDeathDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonDeathDocument")));

            modelBuilder.Entity<PersonDeathDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonDeathDocument")));

            modelBuilder.Entity<PersonDeathDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonDeathDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonEmploymentDetail #############################

            modelBuilder.Entity<PersonEmploymentDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonEmploymentDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonEmploymentDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonEmploymentDetail")));

            modelBuilder.Entity<PersonEmploymentDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonEmploymentDetail")));

            modelBuilder.Entity<PersonEmploymentDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonEmploymentDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonEmploymentDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonEmploymentDetailTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonEmploymentDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonEmploymentDetailTranslation")));

            modelBuilder.Entity<PersonEmploymentDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonEmploymentDetailTranslation")));

            modelBuilder.Entity<PersonEmploymentDetailTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonEmploymentDetailTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonFamilyDetail #############################

            modelBuilder.Entity<PersonFamilyDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFamilyDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFamilyDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonFamilyDetail")));

            modelBuilder.Entity<PersonFamilyDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonFamilyDetail")));

            modelBuilder.Entity<PersonFamilyDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFamilyDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFamilyDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFamilyDetailTranslation")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFamilyDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonFamilyDetailTranslation")));

            modelBuilder.Entity<PersonFamilyDetailTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonFamilyDetailTranslation")));

            modelBuilder.Entity<PersonFamilyDetailTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFamilyDetailTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonFinancialAsset #############################
            modelBuilder.Entity<PersonFinancialAsset>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFinancialAsset")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFinancialAsset>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonFurnitureAsset")));

            modelBuilder.Entity<PersonFinancialAsset>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonFinancialAsset")));

            modelBuilder.Entity<PersonFinancialAssetMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFinancialAssetMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFinancialAssetTranslation>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFinancialAssetTranslation")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFinancialAssetTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonFinancialAssetTranslation")));

            modelBuilder.Entity<PersonFinancialAssetTranslation>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonFinancialAssetTranslation")));

            modelBuilder.Entity<PersonFinancialAssetTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFinancialAssetTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonFinancialAssetDocument #############################
            modelBuilder.Entity<PersonFinancialAssetDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFinancialAssetDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonFinancialAssetDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonFinancialAssetDocument")));

            modelBuilder.Entity<PersonFinancialAssetDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonFinancialAssetDocument")));

            modelBuilder.Entity<PersonFinancialAssetDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonFinancialAssetDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonGSTRegistrationDetail #############################
            modelBuilder.Entity<PersonGSTRegistrationDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGSTRegistrationDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonGSTRegistrationDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonGSTRegistrationDetail")));

            modelBuilder.Entity<PersonGSTRegistrationDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonGSTRegistrationDetail")));

            modelBuilder.Entity<PersonGSTRegistrationDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGSTRegistrationDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonGSTReturn #############################
            modelBuilder.Entity<PersonGSTReturnDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGSTReturnDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonGSTReturnDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonGSTReturnDocument")));

            modelBuilder.Entity<PersonGSTReturnDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonGSTReturnDocument")));

            modelBuilder.Entity<PersonGSTReturnDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGSTReturnDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // ############################# PersonGroup #############################
            modelBuilder.Entity<PersonGroup>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGroup")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonGroup>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonGroup")));

            modelBuilder.Entity<PersonGroup>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonGroup")));

            modelBuilder.Entity<PersonGroupMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonGroupMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonHomeBranch #############################
            modelBuilder.Entity<PersonHomeBranch>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonHomeBranch")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonHomeBranch>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonHomeBranch")));

            modelBuilder.Entity<PersonHomeBranch>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonHomeBranch")));

            modelBuilder.Entity<PersonHomeBranchMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonHomeBranchMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonImmovableAsset #############################
            modelBuilder.Entity<PersonImmovableAsset>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonImmovableAsset")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonImmovableAsset>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonImmovableAsset")));

            modelBuilder.Entity<PersonImmovableAsset>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonImmovableAsset")));

            modelBuilder.Entity<PersonImmovableAssetMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonImmovableAssetMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonImmovableAssetDocument #############################
            modelBuilder.Entity<PersonImmovableAssetDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonImmovableAssetDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonImmovableAssetDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonImmovableAssetDocument")));

            modelBuilder.Entity<PersonImmovableAssetDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonImmovableAssetDocument")));

            modelBuilder.Entity<PersonImmovableAssetDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonImmovableAssetDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonIncomeTaxDetail #############################
            modelBuilder.Entity<PersonIncomeTaxDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonIncomeTaxDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonIncomeTaxDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonIncomeTaxDetail")));

            modelBuilder.Entity<PersonIncomeTaxDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonIncomeTaxDetail")));

            modelBuilder.Entity<PersonIncomeTaxDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonIncomeTaxDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonIncomeTaxDetailDocument #############################
            modelBuilder.Entity<PersonIncomeTaxDetailDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonIncomeTaxDetailDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonIncomeTaxDetailDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonIncomeTaxDetailDocument")));

            modelBuilder.Entity<PersonIncomeTaxDetailDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonIncomeTaxDetailDocument")));

            modelBuilder.Entity<PersonIncomeTaxDetailDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonIncomeTaxDetailDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            // ############################# PersonInsuranceDetail #############################
            modelBuilder.Entity<PersonInsuranceDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInsuranceDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonInsuranceDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonInsuranceDetail")));

            modelBuilder.Entity<PersonInsuranceDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonInsuranceDetail")));

            modelBuilder.Entity<PersonInsuranceDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonInsuranceDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonKYCDetail #############################
            modelBuilder.Entity<PersonKYCDetail>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonKYCDetail")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonKYCDetail>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonKYCDetail")));

            modelBuilder.Entity<PersonKYCDetail>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonKYCDetail")));

            modelBuilder.Entity<PersonKYCDetailMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonKYCDetailMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonKYCDetailDocument #############################
            modelBuilder.Entity<PersonKYCDetailDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonKYCDetailDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonKYCDetailDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonKYCDetailDocument")));

            modelBuilder.Entity<PersonKYCDetailDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonKYCDetailDocument")));

            modelBuilder.Entity<PersonKYCDetailDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonKYCDetailDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonMachineryAsset #############################

            modelBuilder.Entity<PersonMachineryAsset>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMachineryAsset")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonMachineryAsset>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonMachineryAsset")));

            modelBuilder.Entity<PersonMachineryAssetMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMachineryAssetMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonMachineryAssetDocument>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMachineryAssetDocument")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonMachineryAssetDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonMachineryAssetDocument")));

            modelBuilder.Entity<PersonMachineryAssetDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMachineryAssetDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonMovableAsset #############################
            modelBuilder.Entity<PersonMovableAsset>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMovableAsset")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonMovableAsset>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonMovableAsset")));

            modelBuilder.Entity<PersonMovableAsset>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonMovableAsset")));

            modelBuilder.Entity<PersonMovableAssetMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMovableAssetMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonMovableAssetDocument #############################
            modelBuilder.Entity<PersonMovableAssetDocument>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMovableAssetDocument")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonMovableAssetDocument>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonMovableAssetDocument")));

            modelBuilder.Entity<PersonMovableAssetDocument>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonMovableAssetDocument")));

            modelBuilder.Entity<PersonMovableAssetDocumentMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonMovableAssetDocumentMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonPhoto #############################
            modelBuilder.Entity<PersonPhoto>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonPhoto")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonPhoto>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonPhoto")));

            modelBuilder.Entity<PersonPhoto>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonPhoto")));

            modelBuilder.Entity<PersonPhoto>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonPhotoMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonPhotoSign #############################
            modelBuilder.Entity<PersonPhotoSign>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonPhotoSign")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonPhotoSign>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonPhotoSign")));

            modelBuilder.Entity<PersonPhotoSign>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonPhotoSign")));

            modelBuilder.Entity<PersonPhotoSignMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonPhotoSignMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonPrefix #############################
            modelBuilder.Entity<PersonPrefix>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonPrefix")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonPrefix>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonPrefix")));

            modelBuilder.Entity<PersonPrefix>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonPrefix")));

            modelBuilder.Entity<PersonPrefixMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonPrefixMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonRelative #############################
            modelBuilder.Entity<PersonRelative>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonRelative")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonRelative>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonRelative")));

            modelBuilder.Entity<PersonRelative>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonRelative")));

            modelBuilder.Entity<PersonRelativeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonRelativeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonSMSAlert #############################
            modelBuilder.Entity<PersonSMSAlert>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonSMSAlert")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonSMSAlert>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonSMSAlert")));

            modelBuilder.Entity<PersonSMSAlert>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonSMSAlert")));

            modelBuilder.Entity<PersonSMSAlertMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonSMSAlertMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<TeleVerificationToken>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddTeleVerificationToken")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# PersonSocialMedia #############################
            modelBuilder.Entity<PersonSocialMedia>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonSocialMedia")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PersonSocialMedia>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePersonSocialMedia")));

            modelBuilder.Entity<PersonSocialMedia>()
               .MapToStoredProcedures(sp => sp.Delete(d => d.HasName("Usp_DeletePersonSocialMedia")));

            modelBuilder.Entity<PersonSocialMediaMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPersonSocialMediaMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ S E C U R I T Y @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            // ############################# Log #############################

            modelBuilder.Entity<LoginLog>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddLoginLog")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<InvalidLoginLog>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddInvalidLoginLog")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<AccountRecoveryLog>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddAccountRecoveryLog")
                .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Password  #############################
            // ********************* PasswordPolicy  *********************
            modelBuilder.Entity<PasswordPolicy>()
           .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPasswordPolicy")
           .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PasswordPolicy>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePasswordPolicy")));

            modelBuilder.Entity<PasswordPolicyMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPasswordPolicyMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PasswordPolicyModification>()
             .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPasswordPolicyModification")
             .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<PasswordPolicyModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdatePasswordPolicyModification")));

            modelBuilder.Entity<PasswordPolicyModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddPasswordPolicyModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# RoleProfile #############################

            modelBuilder.Entity<RoleProfile>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfile")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfile>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfile")));

            modelBuilder.Entity<RoleProfileMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileBusinessOffice>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileBusinessOffice")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileBusinessOffice>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileBusinessOffice")));

            modelBuilder.Entity<RoleProfileBusinessOfficeMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileBusinessOfficeMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileGeneralLedger>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileGeneralLedger")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileGeneralLedger>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileGeneralLedger")));

            modelBuilder.Entity<RoleProfileGeneralLedgerMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileGeneralLedgerMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileMenu>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileMenu")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileMenu>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileMenu")));

            modelBuilder.Entity<RoleProfileMenuMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileMenuMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileModification>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileModification")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileModification>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileModification")));

            modelBuilder.Entity<RoleProfileModificationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileModificationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileSpecialPermission>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileSpecialPermission")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileSpecialPermission>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileSpecialPermission")));

            modelBuilder.Entity<RoleProfileSpecialPermissionMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileSpecialPermissionMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileTransactionLimit>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileTransactionLimit")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileTransactionLimit>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileTransactionLimit")));

            modelBuilder.Entity<RoleProfileTransactionLimitMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileTransactionLimitMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileTranslation>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileTranslation")
                .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<RoleProfileTranslation>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateRoleProfileTranslation")));

            modelBuilder.Entity<RoleProfileTranslationMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddRoleProfileTranslationMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));

            // ############################# Users #############################

            modelBuilder.Entity<UserAuthenticationToken>()
                .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddUserAuthenticationToken")
                .Result(rs => rs.PrmKey, "PrmKey")));


            //*************************************** S M S  **************************************************
            // Sample With Parameter
            modelBuilder.Entity<SmsUserAuthenticationToken>().
               MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddSmsUserAuthenticationToken")
               .Parameter(pm => pm.UserAuthenticationTokenPrmKey, "UserAuthenticationTokenPrmKey")
               .Parameter(pm => pm.SendingDate, "SendingDate")
               .Parameter(pm => pm.SMSProviderMessageID, "SMSProviderMessageID")
               .Parameter(pm => pm.SMSProviderClientID, "SMSProviderClientID")
               .Result(rs => rs.PrmKey, "PrmKey")));

            //*****************************  CutomerSmsService ********************
            modelBuilder.Entity<CustomerAccountSmsService>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountSmsService")
               .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountSmsService>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountSmsService")));

            modelBuilder.Entity<CustomerAccountSmsServiceMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountSmsServiceMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));


            //*****************************  CutomerEmailService ********************

            modelBuilder.Entity<CustomerAccountEmailService>()
              .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountEmailService")
              .Result(rs => rs.PrmKey, "PrmKey")));

            modelBuilder.Entity<CustomerAccountEmailService>()
               .MapToStoredProcedures(sp => sp.Update(u => u.HasName("Usp_UpdateCustomerAccountEmailService")));

            modelBuilder.Entity<CustomerAccountEmailServiceMakerChecker>()
               .MapToStoredProcedures(sp => sp.Insert(i => i.HasName("Usp_AddCustomerAccountEmailServiceMakerChecker")
               .Result(rs => rs.PrmKey, "PrmKey")));



            //*************************************** SYSTEM ENTITY *****************************************
            //*************** Application Page *************
            //*************** Authentication ***************
            //*************** Navigation *******************

        }

        // DbSet

        // The DbSet class represents an entity set that can be used for create, read, update, and delete operations.
        // The context class (derived from DbContext) must include the DbSet type properties for the entities which map to database tables and views.

        // Note - ** Main Module Name In CAPITAL and Parent Module Name In Upper Camel Case and Child Module Name In lower case **

        // @@@@@@@@@@@@@@@@@@@@@ A C C O U N T @@@@@@@@@@@@@@@@@@@@@
        // #################### Customer ####################
        public virtual DbSet<BeneficiaryDetail> BeneficiaryDetails { get; set; }
        public virtual DbSet<BeneficiaryDetailMakerChecker> BeneficiaryDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountType> CustomerAccountTypes { get; set; }
        public virtual DbSet<ChequeBookMaster> ChequeBookMasters { get; set; }
        public virtual DbSet<ChequeBookMasterMakerChecker> ChequeBookMasterMakerCheckers { get; set; }


        // #################### Layout ####################
        public virtual DbSet<SchemeLoanChargesParameter> SchemeLoanChargesParameters { get; set; }
        public virtual DbSet<SchemeLoanChargesParameterMakerChecker> SchemeLoanChargesParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDocument> SchemeDocuments { get; set; }
        public virtual DbSet<SchemeDocumentMakerChecker> SchemeDocumentMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanDistributorParameter> SchemeLoanDistributorParameters { get; set; }
        public virtual DbSet<SchemeLoanDistributorParameterMakerChecker> SchemeLoanDistributorParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanFunderParameter> SchemeLoanFunderParameters { get; set; }
        public virtual DbSet<SchemeLoanFunderParameterMakerChecker> SchemeLoanFunderParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanOverduesAction> SchemeLoanOverduesActions { get; set; }
        public virtual DbSet<SchemeLoanOverduesActionMakerChecker> SchemeLoanOverduesActionMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanInstallmentParameter> SchemeLoanInstallmentParameters { get; set; }
        public virtual DbSet<SchemeLoanInstallmentParameterMakerChecker> SchemeLoanInstallmentParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeTargetGroup> SchemeTargetGroups { get; set; }
        public virtual DbSet<SchemeTargetGroupMakerChecker> SchemeTargetGroupMakerCheckers { get; set; }
        public virtual DbSet<SchemeTargetGroupOccupation> SchemeTargetGroupOccupations { get; set; }
        public virtual DbSet<SchemeTargetGroupOccupationMakerChecker> SchemeTargetGroupOccupationMakerCheckers { get; set; }
        public virtual DbSet<SchemeTargetGroupGender> SchemeTargetGroupGenders { get; set; }
        public virtual DbSet<SchemeTargetGroupGenderMakerChecker> SchemeTargetGroupGenderMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanAccountParameter> SchemeLoanAccountParameters { get; set; }
        public virtual DbSet<SchemeLoanAccountParameterMakerChecker> SchemeLoanAccountParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanArrearParameter> SchemeLoanArrearParameters { get; set; }
        public virtual DbSet<SchemeLoanArrearParameterMakerChecker> SchemeLoanArrearParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanInterestParameter> SchemeLoanInterestParameters { get; set; }
        public virtual DbSet<SchemeLoanInterestParameterMakerChecker> SchemeLoanInterestParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanSanctionAuthority> SchemeLoanSanctionAuthorities { get; set; }
        public virtual DbSet<SchemeLoanSanctionAuthorityMakerChecker> SchemeLoanSanctionAuthorityMakerCheckers { get; set; }
        public virtual DbSet<SchemeCashCreditLoanParameter> SchemeCashCreditLoanParameters { get; set; }
        public virtual DbSet<SchemeCashCreditLoanParameterMakerChecker> SchemeCashCreditLoanParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeHomeLoan> SchemeHomeLoans { get; set; }
        public virtual DbSet<SchemeHomeLoanMakerChecker> SchemeHomeLoanMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanAgainstProperty> SchemeLoanAgainstProperties { get; set; }
        public virtual DbSet<SchemeLoanAgainstPropertyMakerChecker> SchemeLoanAgainstPropertyMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanRecoveryAction> SchemeLoanRecoveryActions { get; set; }
        public virtual DbSet<SchemeLoanRecoveryActionMakerChecker> SchemeLoanRecoveryActionMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanPaymentReminderParameter> SchemeLoanPaymentReminderParameters { get; set; }
        public virtual DbSet<SchemeLoanPaymentReminderParameterMakerChecker> SchemeLoanPaymentReminderParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeVehicleTypeLoanParameter> SchemeVehicleTypeLoanParameters { get; set; }
        public virtual DbSet<SchemeVehicleTypeLoanParameterMakerChecker> SchemeVehicleTypeLoanParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemePreownedVehicleLoanParameter> SchemePreownedVehicleLoanParameters { get; set; }
        public virtual DbSet<SchemePreownedVehicleLoanParameterMakerChecker> SchemePreownedVehicleLoanParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeConsumerDurableLoanItem> SchemeConsumerDurableLoanItems { get; set; }
        public virtual DbSet<SchemeConsumerDurableLoanItemMakerChecker> SchemeConsumerDurableLoanItemMakerCheckers { get; set; }
        public virtual DbSet<ConsumerDurableItem> ConsumerDurableItems { get; set; }
        public virtual DbSet<ConsumerDurableItemBrand> ConsumerDurableItemBrands { get; set; }
        public virtual DbSet<ConsumerDurableItemMakerChecker> ConsumerDurableItemMakerCheckers { get; set; }
        public virtual DbSet<ConsumerDurableItemModification> ConsumerDurableItemModifications { get; set; }
        public virtual DbSet<ConsumerDurableItemModificationMakerChecker> ConsumerDurableItemModificationMakerCheckers { get; set; }
        public virtual DbSet<ConsumerDurableItemTranslation> ConsumerDurableItemTranslations { get; set; }
        public virtual DbSet<ConsumerDurableItemTranslationMakerChecker> ConsumerDurableItemTranslationMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanAgainstDepositGeneralLedger> SchemeLoanAgainstDepositGeneralLedgers { get; set; }

        public virtual DbSet<SchemeLoanAgainstDepositGeneralLedgerMakerChecker> SchemeLoanAgainstDepositGeneralLedgerMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanAgainstDepositParameter> SchemeLoanAgainstDepositParameters { get; set; }
        public virtual DbSet<SchemeLoanAgainstDepositParameterMakerChecker> SchemeLoanAgainstDepositParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanFineInterestParameter> SchemeLoanFineInterestParameters { get; set; }
        public virtual DbSet<SchemeLoanFineInterestParameterMakerChecker> SchemeLoanFineInterestParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanInterestProvisionParameter> SchemeLoanInterestProvisionParameters { get; set; }
        public virtual DbSet<SchemeLoanInterestProvisionParameterMakerChecker> SchemeLoanInterestProvisionParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanRepaymentScheduleParameter> SchemeLoanRepaymentScheduleParameters { get; set; }
        public virtual DbSet<SchemeLoanRepaymentScheduleParameterMakerChecker> SchemeLoanRepaymentScheduleParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanSettlementAccountParameter> SchemeLoanSettlementAccountParameters { get; set; }
        public virtual DbSet<SchemeLoanSettlementAccountParameterMakerChecker> SchemeLoanSettlementAccountParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanInterestRebateParameter> SchemeLoanInterestRebateParameters { get; set; }
        public virtual DbSet<SchemeLoanInterestRebateParameterMakerChecker> SchemeLoanInterestRebateParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemePassbook> SchemePassbooks { get; set; }
        public virtual DbSet<SchemePassbookMakerChecker> SchemePassbookMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanPreFullPaymentParameter> SchemePreFullPaymentParameters { get; set; }
        public virtual DbSet<SchemeLoanPreFullPaymentParameterMakerChecker> SchemePreFullPaymentParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeTenure> SchemeTenures { get; set; }
        public virtual DbSet<SchemeTenureMakerChecker> SchemeTenureMakerCheckers { get; set; }
        public virtual DbSet<SchemeLoanPrePartPaymentParameter> SchemePrePartPaymentParameters { get; set; }
        public virtual DbSet<SchemeLoanPrePartPaymentParameterMakerChecker> SchemePrePartPaymentParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeGoldLoanParameter> SchemeGoldLoanParameters { get; set; }
        public virtual DbSet<SchemeGoldLoanParameterMakerChecker> SchemeGoldLoanParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeEducationLoanParameter> SchemeEducationLoanParameters { get; set; }
        public virtual DbSet<SchemeEducationLoanParameterMakerChecker> SchemeEducationLoanParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeInstitute> SchemeInstitutes { get; set; }
        public virtual DbSet<SchemeInstituteMakerChecker> SchemeInstituteMakerCheckers { get; set; }
        public virtual DbSet<SchemeEducationalCourse> SchemeEducationalCourses { get; set; }
        public virtual DbSet<SchemeEducationalCourseMakerChecker> SchemeEducationalCourseMakerCheckers { get; set; }

        // #################### Master ####################
        public virtual DbSet<InterestMethod> InterestMethods { get; set; }
        public virtual DbSet<InterestMethodTranslation> InterestMethodTranslations { get; set; }
        public virtual DbSet<ChargesType> ChargesTypes { get; set; }
        public virtual DbSet<ChargesTypeTranslation> ChargesTypeTranslations { get; set; }
        public virtual DbSet<ChargesBase> ChargesBases { get; set; }
        public virtual DbSet<ChargesBaseTranslation> ChargesBaseTranslations { get; set; }

        public virtual DbSet<GoldLoanRate> GoldLoanRates { get; set; }
        public virtual DbSet<GoldLoanRateMakerChecker> GoldLoanRateMakerCheckers { get; set; }
        public virtual DbSet<GoldOrnament> GoldOrnaments { get; set; }
        public virtual DbSet<GoldOrnamentMakerChecker> GoldOrnamentMakerCheckers { get; set; }
        public virtual DbSet<GoldOrnamentModification> GoldOrnamentModifications { get; set; }
        public virtual DbSet<GoldOrnamentModificationMakerChecker> GoldOrnamentModificationMakerCheckers { get; set; }
        public virtual DbSet<GoldOrnamentTranslation> GoldOrnamentTranslations { get; set; }
        public virtual DbSet<GoldOrnamentTranslationMakerChecker> GoldOrnamentTranslationMakerCheckers { get; set; }

        public virtual DbSet<LendingChargesBase> LendingChargesBases { get; set; }
        public virtual DbSet<LendingInterestMethod> LendingInterestMethods { get; set; }
        public virtual DbSet<LendingInterestPostingFrequency> LendingInterestPostingFrequencies { get; set; }
        public virtual DbSet<LendingInterestPostingFrequencyTranslation> LendingInterestPostingFrequencyTranslations { get; set; }
        public virtual DbSet<InterestRebateApplyFrequency> InterestRebateApplyFrequencies { get; set; }
        public virtual DbSet<InterestRebateApplyFrequencyTranslation> InterestRebateApplyFrequencyTranslations { get; set; }
        public virtual DbSet<InterestRebateCriteria> InterestRebateCriterias { get; set; }
        public virtual DbSet<InterestRebateCriteriaTranslation> InterestRebateCriteriaTranslations { get; set; }
        public virtual DbSet<LoanRecoveryAction> LoanRecoveryActions { get; set; }
        public virtual DbSet<LoanRecoveryActionTranslation> LoanRecoveryActionTranslations { get; set; }
        public virtual DbSet<TargetGroup> TargetGroups { get; set; }
        public virtual DbSet<TargetGroupTranslation> TargetGroupTranslations { get; set; }
        public virtual DbSet<TargetGroupMakerChecker> TargetGroupMakerCheckers { get; set; }
        public virtual DbSet<DaysInYear> DaysInYears { get; set; }
        public virtual DbSet<DaysInYearTranslation> DaysInYearTranslations { get; set; }
        public virtual DbSet<InterestCalculationFrequency> InterestCalculationFrequencies { get; set; }
        public virtual DbSet<InterestCalculationFrequencyTranslation> InterestCalculationFrequencyTranslations { get; set; }
        public virtual DbSet<InterestCompoundingFrequency> InterestCompoundingFrequencies { get; set; }
        public virtual DbSet<InterestCompoundingFrequencyTranslation> InterestCompoundingFrequencyTranslations { get; set; }
        public virtual DbSet<LendingRepaymentsInterestCalculation> LendingRepaymentsInterestCalculations { get; set; }
        public virtual DbSet<LendingRepaymentsInterestCalculationTranslation> LendingRepaymentsInterestCalculationTranslations { get; set; }
        public virtual DbSet<FixedAssetItem> FixedAssetItems { get; set; }
        public virtual DbSet<FixedAssetItemMakerChecker> FixedAssetItemMakerCheckers { get; set; }
        public virtual DbSet<FixedAssetItemModification> FixedAssetItemModifications { get; set; }
        public virtual DbSet<FixedAssetItemModificationMakerChecker> FixedAssetItemModificationMakerCheckers { get; set; }
        public virtual DbSet<FixedAssetItemTranslation> FixedAssetItemTranslations { get; set; }
        public virtual DbSet<FixedAssetItemTranslationMakerChecker> FixedAssetItemTranslationMakerCheckers { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }
        public virtual DbSet<LoanTypeTranslation> LoanTypeTranslations { get; set; }
        public virtual DbSet<SchemeBusinessLoan> SchemeBusinessLoans { get; set; }
        public virtual DbSet<SchemeBusinessLoanMakerChecker> SchemeBusinessLoanMakerCheckers { get; set; }

        // #################### ASSET ####################
        public virtual DbSet<AgricultureLandType> AgricultureLandTypes { get; set; }
        public virtual DbSet<AgricultureLandTypeTranslation> AgricultureLandTypeTranslations { get; set; }
        public virtual DbSet<AssetClass> AssetClasses { get; set; }
        public virtual DbSet<AssetClassTranslation> AssetClassTranslations { get; set; }
        public virtual DbSet<FinancialAssetType> FinancialAssetTypes { get; set; }
        public virtual DbSet<FinancialAssetTypeTranslation> FinancialAssetTypeTranslations { get; set; }
        public virtual DbSet<FurnitureAssetType> FurnitureAssetTypes { get; set; }
        public virtual DbSet<FurnitureAssetTypeTranslation> FurnitureAssetTypeTranslations { get; set; }
        public virtual DbSet<InstallmentFrequency> InstallmentFrequencies { get; set; }
        public virtual DbSet<InstallmentFrequencyTranslation> InstallmentFrequencyTranslations { get; set; }

        // #################### CREDIT BUREAU ####################
        public virtual DbSet<CreditBureauAgency> CreditBureauAgencies { get; set; }
        public virtual DbSet<CreditBureauAgencyTranslation> CreditBureauAgencyTranslations { get; set; }


        // #################### CUSTOMER ####################
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public virtual DbSet<CustomerAccountDetail> CustomerAccountDetails { get; set; }
        public virtual DbSet<CustomerAccountDetailMakerChecker> CustomerAccountDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountInterestRate> CustomerAccountInterestRates { get; set; }
        public virtual DbSet<CustomerAccountInterestRateMakerChecker> CustomerAccountInterestRateMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAgainstPropertyCollateralDetail> CustomerLoanAgainstPropertyCollateralDetails { get; set; }
        public virtual DbSet<CustomerLoanAgainstPropertyCollateralDetailMakerChecker> CustomerLoanAgainstPropertyCollateralDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerBusinessLoanCollateralDetail> CustomerBusinessLoanCollateralDetails { get; set; }
        public virtual DbSet<CustomerBusinessLoanCollateralDetailMakerChecker> CustomerBusinessLoanCollateralDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAgainstDepositCollateralDetail> CustomerLoanAgainstDepositCollateralDetails { get; set; }
        public virtual DbSet<CustomerLoanAgainstDepositCollateralDetailMakerChecker> CustomerLoanAgainstDepositCollateralDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountContactDetail> CustomerAccountContactDetails { get; set; }
        public virtual DbSet<CustomerAccountContactDetailMakerChecker> CustomerAccountContactDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountAddressDetail> CustomerAccountAddressDetails { get; set; }
        public virtual DbSet<CustomerAccountAddressDetailMakerChecker> CustomerAccountAddressDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountAdditionalIncomeDetail> CustomerLoanAccountAdditionalIncomeDetails { get; set; }
        public virtual DbSet<CustomerLoanAccountAdditionalIncomeDetailMakerChecker> CustomerLoanAccountAdditionalIncomeDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountCourtCaseDetail> CustomerLoanAccountCourtCaseDetails { get; set; }
        public virtual DbSet<CustomerLoanAccountCourtCaseDetailMakerChecker> CustomerLoanAccountCourtCaseDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountBorrowingDetail> CustomerLoanAccountBorrowingDetails { get; set; }
        public virtual DbSet<CustomerLoanAccountBorrowingDetailMakerChecker> CustomerLoanAccountBorrowingDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountIncomeTaxDetail> CustomerLoanAccountIncomeTaxDetails { get; set; }
        public virtual DbSet<CustomerLoanAccountIncomeTaxDetailMakerChecker> CustomerLoanAccountIncomeTaxDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountEmploymentDetail> CustomerAccountEmploymentDetails { get; set; }
        public virtual DbSet<CustomerAccountEmploymentDetailMakerChecker> CustomerAccountEmploymentDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountDocument> CustomerAccountDocuments { get; set; }
        public virtual DbSet<CustomerAccountDocumentMakerChecker> CustomerAccountDocumentMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountReferencePersonDetail> CustomerAccountReferencePersonDetails { get; set; }
        public virtual DbSet<CustomerAccountReferencePersonDetailMakerChecker> CustomerAccountReferencePersonDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountStandingInstruction> CustomerAccountStandingInstructions { get; set; }
        public virtual DbSet<CustomerAccountStandingInstructionMakerChecker> CustomerAccountStandingInstructionMakerCheckers { get; set; }
        public virtual DbSet<CustomerJointAccountHolder> CustomerJointAccountHolders { get; set; }
        public virtual DbSet<CustomerJointAccountHolderMakerChecker> CustomerJointAccountHolderMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountLimitStatus> CustomerAccountLimitStatuses { get; set; }
        public virtual DbSet<CustomerAccountLimitStatusMakerChecker> CustomerAccountLimitStatusMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountMakerChecker> CustomerAccountMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountModification> CustomerAccountModifications { get; set; }
        public virtual DbSet<CustomerAccountModificationMakerChecker> CustomerAccountModificationMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountNominee> CustomerAccountNominees { get; set; }
        public virtual DbSet<CustomerAccountNomineeGuardian> CustomerAccountNomineeGuardians { get; set; }
        public virtual DbSet<CustomerAccountNomineeGuardianMakerChecker> CustomerAccountNomineeGuardianMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountNomineeGuardianTranslation> CustomerAccountNomineeGuardianTranslations { get; set; }
        public virtual DbSet<CustomerAccountNomineeGuardianTranslationMakerChecker> CustomerAccountNomineeGuardianTranslationMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountNomineeMakerChecker> CustomerAccountNomineeMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountNomineeTranslation> CustomerAccountNomineeTranslations { get; set; }
        public virtual DbSet<CustomerAccountNomineeTranslationMakerChecker> CustomerAccountNomineeTranslationMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountNoticeSchedule> CustomerAccountNoticeSchedules { get; set; }
        public virtual DbSet<CustomerAccountNoticeScheduleMakerChecker> CustomerAccountNoticeScheduleMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountTurnOverLimit> CustomerAccountTurnOverLimits { get; set; }
        public virtual DbSet<CustomerAccountTurnOverLimitMakerChecker> CustomerAccountTurnOverLimitMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountSweepDetail> CustomerAccountSweepDetails { get; set; }
        public virtual DbSet<CustomerAccountSweepDetailMakerChecker> CustomerAccountSweepDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountPhotoSign> CustomerAccountPhotoSigns { get; set; }
        public virtual DbSet<CustomerAccountPhotoSignMakerChecker> CustomerAccountPhotoSignMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountEmailService> CustomerAccountEmailServices { get; set; }
        public virtual DbSet<CustomerAccountEmailServiceMakerChecker> CustomerAccountEmailServiceMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountSmsService> CustomerAccountSmsServices { get; set; }
        public virtual DbSet<CustomerAccountSmsServiceMakerChecker> CustomerAccountSmsServiceMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountChequeBookRequestDetail> CustomerAccountChequeBookRequestDetails { get; set; }
        public virtual DbSet<CustomerAccountChequeBookRequestDetailMakerChecker> CustomerAccountChequeBookRequestDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountChequeDetail> CustomerAccountChequeDetails { get; set; }
        public virtual DbSet<CustomerAccountChequeDetailMakerChecker> CustomerAccountChequeDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerAccountBeneficiaryDetail> CustomerAccountBeneficiaryDetails { get; set; }
        public virtual DbSet<CustomerAccountBeneficiaryDetailMakerChecker> CustomerAccountBeneficiaryDetailMakerCheckers { get; set; }


        public virtual DbSet<CustomerSharesCapitalAccount> CustomerSharesCapitalAccounts { get; set; }
        public virtual DbSet<CustomerSharesCapitalAccountMakerChecker> CustomerSharesCapitalAccountMakerCheckers { get; set; }
        public virtual DbSet<CustomerDepositAccount> CustomerDepositAccounts { get; set; }
        public virtual DbSet<CustomerDepositAccountMakerChecker> CustomerDepositAccountMakerCheckers { get; set; }
        public virtual DbSet<CustomerDepositAccountAgent> CustomerDepositAccountAgents { get; set; }
        public virtual DbSet<CustomerDepositAccountAgentMakerChecker> CustomerDepositAccountAgentMakerCheckers { get; set; }
        public virtual DbSet<CustomerTermDepositAccountDetail> CustomerTermDepositAccountDetails { get; set; }
        public virtual DbSet<CustomerTermDepositAccountDetailMakerChecker> CustomerTermDepositAccountDetailMakerCheckers { get; set; }
        public virtual DbSet<AccountOperationMode> AccountOperationModes { get; set; }
        public virtual DbSet<AccountOperationModeTranslation> AccountOperationModeTranslations { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentMakerChecker> AgentMakerCheckers { get; set; }
        public virtual DbSet<RenewType> RenewTypes { get; set; }
        public virtual DbSet<RenewTypeTranslation> RenewTypeTranslations { get; set; }


        public virtual DbSet<CustomerLoanAccount> CustomerLoanAccounts { get; set; }
        public virtual DbSet<CustomerLoanAccountMakerChecker> CustomerLoanAccountMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountTranslation> CustomerLoanAccountTranslations { get; set; }
        public virtual DbSet<CustomerLoanAccountTranslationMakerChecker> CustomerLoanAccountTranslationMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountGuarantorDetail> CustomerLoanAccountGuarantorDetails { get; set; }
        public virtual DbSet<CustomerLoanAccountGuarantorDetailMakerChecker> CustomerLoanAccountGuarantorDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerVehicleLoanInsuranceDetail> CustomerVehicleLoanInsuranceDetails { get; set; }
        public virtual DbSet<CustomerVehicleLoanInsuranceDetailMakerChecker> CustomerVehicleLoanInsuranceDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanFieldInvestigation> CustomerLoanFieldInvestigations { get; set; }
        public virtual DbSet<CustomerLoanFieldInvestigationMakerChecker> CustomerLoanFieldInvestigationMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAccountDebtToIncomeRatio> CustomerLoanAccountDebtToIncomeRatios { get; set; }
        public virtual DbSet<CustomerLoanAccountDebtToIncomeRatioMakerChecker> CustomerLoanAccountDebtToIncomeRatioMakerCheckers { get; set; }
        public virtual DbSet<CustomerPreOwnedVehicleLoanInspection> CustomerPreOwnedVehicleLoanInspections { get; set; }
        public virtual DbSet<CustomerPreOwnedVehicleLoanInspectionMakerChecker> CustomerPreOwnedVehicleLoanInspectionMakerCheckers { get; set; }
        public virtual DbSet<CustomerVehicleLoanPhoto> CustomerVehicleLoanPhotos { get; set; }
        public virtual DbSet<CustomerVehicleLoanPhotoMakerChecker> CustomerVehicleLoanPhotoMakerCheckers { get; set; }
        public virtual DbSet<CustomerVehicleLoanCollateralDetail> CustomerVehicleLoanCollateralDetails { get; set; }
        public virtual DbSet<CustomerVehicleLoanCollateralDetailMakerChecker> CustomerVehicleLoanCollateralDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerVehicleLoanPermitDetail> CustomerVehicleLoanPermitDetails { get; set; }
        public virtual DbSet<CustomerVehicleLoanPermitDetailMakerChecker> CustomerVehicleLoanPermitDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerVehicleLoanContractDetail> CustomerVehicleLoanContractDetails { get; set; }
        public virtual DbSet<CustomerVehicleLoanContractDetailMakerChecker> CustomerVehicleLoanContractDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerLoanAcquaintanceDetail> CustomerLoanAcquaintanceDetails { get; set; }
        public virtual DbSet<CustomerLoanAcquaintanceDetailMakerChecker> CustomerLoanAcquaintanceDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerConsumerLoanCollateralDetail> CustomerConsumerLoanCollateralDetails { get; set; }
        public virtual DbSet<CustomerConsumerLoanCollateralDetailMakerChecker> CustomerConsumerLoanCollateralDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerGoldLoanCollateralDetail> CustomerGoldLoanCollateralDetails { get; set; }
        public virtual DbSet<CustomerGoldLoanCollateralDetailMakerChecker> CustomerGoldLoanCollateralDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerGoldLoanCollateralPhoto> CustomerGoldLoanCollateralPhotos { get; set; }
        public virtual DbSet<CustomerGoldLoanCollateralPhotoMakerChecker> CustomerGoldLoanCollateralPhotoMakerCheckers { get; set; }
        public virtual DbSet<CustomerGoldLoanReappraisal> CustomerGoldLoanReappraisals { get; set; }
        public virtual DbSet<CustomerGoldLoanReappraisalMakerChecker> CustomerGoldLoanReappraisalMakerCheckers { get; set; }
        public virtual DbSet<CustomerCashCreditLoanAccount> CustomerCashCreditLoanAccounts { get; set; }
        public virtual DbSet<CustomerCashCreditLoanAccountMakerChecker> CustomerCashCreditLoanAccountMakerCheckers { get; set; }
        public virtual DbSet<CustomerEducationalLoanDetail> CustomerEducationalLoanDetails { get; set; }
        public virtual DbSet<CustomerEducationalLoanDetailMakerChecker> CustomerEducationalLoanDetailMakerCheckers { get; set; }
        public virtual DbSet<CustomerEducationalLoanDetailTranslation> CustomerEducationalLoanDetailTranslations { get; set; }
        public virtual DbSet<CustomerEducationalLoanDetailTranslationMakerChecker> CustomerEducationalLoanDetailTranslationMakerCheckers { get; set; }


        // @@@@@@@@@@@@@@@@@@@@@ FinancialCycleAndPeriod @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<FinancialCycle> FinancialCycles { get; set; }
        public virtual DbSet<FinancialCycleMakerChecker> FinancialCycleMakerCheckers { get; set; }

        public virtual DbSet<PeriodCode> PeriodCodes { get; set; }
        public virtual DbSet<PeriodCodeMakerChecker> PeriodCodeMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Shares @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<SharesApplication> SharesApplications { get; set; }
        public virtual DbSet<SharesApplicationMakerChecker> SharesApplicationMakerCheckers { get; set; }
        public virtual DbSet<SharesApplicationModification> SharesApplicationModifications { get; set; }
        public virtual DbSet<SharesApplicationModificationMakerChecker> SharesApplicationModificationMakerCheckers { get; set; }
        public virtual DbSet<SharesApplicationTranslation> SharesApplicationTranslations { get; set; }
        public virtual DbSet<SharesApplicationTranslationMakerChecker> SharesApplicationTranslationMakerCheckers { get; set; }
        public virtual DbSet<SharesApplicationDetail> SharesApplicationDetails { get; set; }
        public virtual DbSet<SharesApplicationDetailMakerChecker> SharesApplicationDetailMakerCheckers { get; set; }

        public virtual DbSet<InwardOutwardType> InwardOutwardTypes { get; set; }
        public virtual DbSet<InwardOutwardTypeMakerChecker> InwardOutwardTypeMakerCheckers { get; set; }
        public virtual DbSet<InwardOutwardTypeModification> InwardOutwardTypeModifications { get; set; }
        public virtual DbSet<InwardOutwardTypeModificationMakerChecker> InwardOutwardTypeModificationMakerCheckers { get; set; }
        public virtual DbSet<InwardOutwardTypeTranslation> InwardOutwardTypeTranslations { get; set; }
        public virtual DbSet<InwardOutwardTypeTranslationMakerChecker> InwardOutwardTypeTranslationMakerCheckers { get; set; }

        public virtual DbSet<InwardTransaction> InwardTransactions { get; set; }
        public virtual DbSet<InwardTransactionMakerChecker> InwardTransactionMakerCheckers { get; set; }
        public virtual DbSet<InwardTransactionTranslation> InwardTransactionTranslations { get; set; }
        public virtual DbSet<InwardTransactionTranslationMakerChecker> InwardTransactionTranslationMakerCheckers { get; set; }
        public virtual DbSet<InwardTransactionDetail> InwardTransactionsDetails { get; set; }
        public virtual DbSet<InwardTransactionDetailMakerChecker> InwardTransactionDetailMakerCheckerss { get; set; }

        public virtual DbSet<OutwardTransaction> OutwardTransactions { get; set; }
        public virtual DbSet<OutwardTransactionMakerChecker> OutwardTransactionMakerCheckers { get; set; }
        public virtual DbSet<OutwardTransactionTranslation> OutwardTransactionTranslations { get; set; }
        public virtual DbSet<OutwardTransactionTranslationMakerChecker> OutwardTransactionTranslationMakerCheckers { get; set; }
        public virtual DbSet<OutwardTransactionDetail> OutwardTransactionsDetails { get; set; }
        public virtual DbSet<OutwardTransactionDetailMakerChecker> OutwardTransactionDetailMakerCheckerss { get; set; }

        //############################# GL #############################
        public virtual DbSet<GeneralLedger> GeneralLedgers { get; set; }
        public virtual DbSet<GeneralLedgerBusinessOffice> GeneralLedgerBusinessOffices { get; set; }
        public virtual DbSet<GeneralLedgerBusinessOfficeMakerChecker> GeneralLedgerBusinessOfficeMakerCheckers { get; set; }
        public virtual DbSet<GeneralLedgerCurrency> GeneralLedgerCurrencies { get; set; }
        public virtual DbSet<GeneralLedgerCurrencyMakerChecker> GeneralLedgerCurrencyMakerCheckers { get; set; }
        public virtual DbSet<GeneralLedgerCustomerType> GeneralLedgerCustomerTypes { get; set; }
        public virtual DbSet<GeneralLedgerCustomerTypeMakerChecker> GeneralLedgerCustomerTypeMakerCheckers { get; set; }
        public virtual DbSet<GeneralLedgerMakerChecker> GeneralLedgerMakerCheckers { get; set; }
        public virtual DbSet<GeneralLedgerModification> GeneralLedgerModifications { get; set; }
        public virtual DbSet<GeneralLedgerModificationMakerChecker> GeneralLedgerModificationMakerCheckers { get; set; }
        public virtual DbSet<GeneralLedgerTransactionType> GeneralLedgerTransactionTypes { get; set; }
        public virtual DbSet<GeneralLedgerTransactionTypeMakerChecker> GeneralLedgerTransactionTypeMakerCheckers { get; set; }
        public virtual DbSet<SharesCessationTransaction> SharesCessationTransactions { get; set; }
        public virtual DbSet<SharesCessationTransactionMakerChecker> SharesCessationTransactionMakerCheckers { get; set; }
        public virtual DbSet<GeneralLedgerTranslation> GeneralLedgerTranslations { get; set; }
        public virtual DbSet<GeneralLedgerTranslationMakerChecker> GeneralLedgerTranslationMakerCheckers { get; set; }
        public virtual DbSet<SchemeGeneralLedger> SchemeGeneralLedgers { get; set; }
        public virtual DbSet<SchemeGeneralLedgerMakerChecker> SchemeGeneralLedgerMakerCheckers { get; set; }
        public virtual DbSet<RevenueGeneralLedgerParameter> RevenueGeneralLedgerParameters { get; set; }
        public virtual DbSet<RevenueGeneralLedgerParameterMakerChecker> RevenueGeneralLedgerParameterMakerCheckers { get; set; }
        public virtual DbSet<RevenueGeneralLedgerTaxParameter> RevenueGeneralLedgerTaxParameters { get; set; }
        public virtual DbSet<RevenueGeneralLedgerTaxParameterMakerChecker> RevenueGeneralLedgerTaxParameterMakerCheckers { get; set; }

        //############################# Layout #############################
        public virtual DbSet<Scheme> Schemes { get; set; }
        public virtual DbSet<SchemeMakerChecker> SchemeMakerCheckers { get; set; }

        public virtual DbSet<SchemeTranslation> SchemeTranslations { get; set; }
        public virtual DbSet<SchemeTranslationMakerChecker> SchemeTranslationMakerCheckers { get; set; }

        public virtual DbSet<SchemeAccountParameter> SchemeAccountParameters { get; set; }
        public virtual DbSet<SchemeAccountParameterMakerChecker> SchemeAccountParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeAccountBankingChannelParameter> SchemeAccountBankingChannelParameters { get; set; }
        public virtual DbSet<SchemeAccountBankingChannelParameterMakerChecker> SchemeAccountBankingChannelParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeApplicationParameter> SchemeApplicationParameters { get; set; }
        public virtual DbSet<SchemeApplicationParameterMakerChecker> SchemeApplicationParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeBusinessOffice> SchemeBusinessOffices { get; set; }
        public virtual DbSet<SchemeBusinessOfficeMakerChecker> SchemeBusinessOfficeMakerCheckers { get; set; }

        public virtual DbSet<SchemeChargesDetail> SchemeChargesDetails { get; set; }
        public virtual DbSet<SchemeChargesDetailMakerChecker> SchemeChargesDetailMakerCheckers { get; set; }

        public virtual DbSet<SchemeCustomerAccountNumber> SchemeCustomerAccountNumbers { get; set; }
        public virtual DbSet<SchemeCustomerAccountNumberMakerChecker> SchemeCustomerAccountNumberMakerCheckers { get; set; }

        public virtual DbSet<SchemeLoanAgreementNumber> SchemeLoanAgreementNumbers { get; set; }
        public virtual DbSet<SchemeLoanAgreementNumberMakerChecker> SchemeLoanAgreementNumberMakerCheckers { get; set; }

        public virtual DbSet<SchemeEstimateTarget> SchemeEstimateTargets { get; set; }
        public virtual DbSet<SchemeEstimateTargetMakerChecker> SchemeEstimateTargetMakerCheckers { get; set; }

        public virtual DbSet<SchemeInterestRate> SchemeInterestRates { get; set; }
        public virtual DbSet<SchemeInterestRateMakerChecker> SchemeInterestRateMakerCheckers { get; set; }

        public virtual DbSet<SchemeLimit> SchemeLimits { get; set; }
        public virtual DbSet<SchemeLimitMakerChecker> SchemeLimitMakerCheckers { get; set; }

        public virtual DbSet<SchemeDepositAccountRenewalParameter> SchemeDepositAccountRenewalParameters { get; set; }
        public virtual DbSet<SchemeDepositAccountRenewalParameterMakerChecker> SchemeDepositAccountRenewalParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeDepositPledgeLoanParameter> SchemeDepositPledgeLoanParameters { get; set; }
        public virtual DbSet<SchemeDepositPledgeLoanParameterMakerChecker> SchemeDepositPledgeLoanParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeFixedDepositParameter> SchemeFixedDepositParameters { get; set; }
        public virtual DbSet<SchemeFixedDepositParameterMakerChecker> SchemeFixedDepositParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeDepositAccountClosureParameter> SchemeDepositAccountClosureParameters { get; set; }
        public virtual DbSet<SchemeDepositAccountClosureParameterMakerChecker> SchemeDepositAccountClosureParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeNoticeSchedule> SchemeNoticeSchedules { get; set; }
        public virtual DbSet<SchemeNoticeScheduleMakerChecker> SchemeNoticeScheduleMakerCheckers { get; set; }

        public virtual DbSet<SchemeTenureList> SchemeTenureLists { get; set; }
        public virtual DbSet<SchemeTenureListMakerChecker> SchemeTenureListMakerCheckers { get; set; }

        public virtual DbSet<SchemeTenureListTranslation> SchemeTenureListTranslations { get; set; }
        public virtual DbSet<SchemeTenureListTranslationMakerChecker> SchemeTenureListTranslationMakerCheckers { get; set; }

        public virtual DbSet<SchemeClosingCharges> SchemeClosingCharges { get; set; }
        public virtual DbSet<SchemeClosingChargesMakerChecker> SchemeClosingChargesMakerCheckers { get; set; }

        public virtual DbSet<SchemeSharesTransferCharges> SchemeSharesTransferCharges { get; set; }
        public virtual DbSet<SchemeSharesTransferChargesMakerChecker> SchemeSharesTransferChargesMakerCheckers { get; set; }

        public virtual DbSet<SchemeReportFormat> SchemeReportFormats { get; set; }
        public virtual DbSet<SchemeReportFormatMakerChecker> SchemeReportFormatMakerCheckers { get; set; }

        public virtual DbSet<SchemeSharesCapitalAccountParameter> SchemeSharesCapitalAccountParameters { get; set; }
        public virtual DbSet<SchemeSharesCapitalAccountParameterMakerChecker> SchemeSharesCapitalAccountParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeSharesCapitalDividendParameter> SchemeSharesCapitalDividendParameters { get; set; }
        public virtual DbSet<SchemeSharesCapitalDividendParameterMakerChecker> SchemeSharesCapitalDividendParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeSharesCertificateParameter> SchemeSharesCertificateParameters { get; set; }
        public virtual DbSet<SchemeSharesCertificateParameterMakerChecker> SchemeSharesCertificateParameterMakerCheckers { get; set; }

        public virtual DbSet<SchemeType> SchemeTypes { get; set; }

        public virtual DbSet<SchemeTypeTranslation> SchemeTypeTranslations { get; set; }

        public virtual DbSet<SchemeVehicleLoanParameter> SchemeVehicleLoanParameters { get; set; }

        //*************************************** DEPOSIT SCHEME ****************************************
        public virtual DbSet<SchemeDepositAccountParameter> SchemeDepositAccountParameters { get; set; }
        public virtual DbSet<SchemeDepositAccountParameterMakerChecker> SchemeDepositAccountParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositAgentIncentive> SchemeDepositAgentIncentives { get; set; }
        public virtual DbSet<SchemeDepositAgentIncentiveMakerChecker> SchemeDepositAgentIncentiveMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositAgentParameter> SchemeDepositAgentParameters { get; set; }
        public virtual DbSet<SchemeDepositAgentParameterMakerChecker> SchemeDepositAgentParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositCertificateParameter> SchemeDepositCertificateParameters { get; set; }
        public virtual DbSet<SchemeDepositCertificateParameterMakerChecker> SchemeDepositCertificateParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositInstallmentParameter> SchemeDepositInstallmentParameters { get; set; }
        public virtual DbSet<SchemeDepositInstallmentParameterMakerChecker> SchemeDepositInstallmentParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositInterestParameter> SchemeDepositInterestParameters { get; set; }
        public virtual DbSet<SchemeDepositInterestParameterMakerChecker> SchemeDepositInterestParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositInterestProvisionParameter> SchemeDepositInterestProvisionParameters { get; set; }
        public virtual DbSet<SchemeDepositInterestProvisionParameterMakerChecker> SchemeDepositInterestProvisionParameterMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositInterestPayoutFrequency> SchemeDepositInterestPayoutFrequencies { get; set; }
        public virtual DbSet<SchemeDepositInterestPayoutFrequencyMakerChecker> SchemeDepositInterestPayoutFrequencyMakerCheckers { get; set; }
        public virtual DbSet<SchemeInterestPayoutFrequency> SchemeInterestPayoutFrequencies { get; set; }
        public virtual DbSet<SchemeInterestPayoutFrequencyMakerChecker> SchemeInterestPayoutFrequencyMakerCheckers { get; set; }
        public virtual DbSet<SchemeNumberOfTransactionLimit> SchemeNumberOfTransactionLimits { get; set; }
        public virtual DbSet<SchemeNumberOfTransactionLimitMakerChecker> SchemeNumberOfTransactionLimitMakerCheckers { get; set; }
        public virtual DbSet<SchemePaymentCardFeature> SchemePaymentCardFeatures { get; set; }
        public virtual DbSet<SchemePaymentCardFeatureMakerChecker> SchemePaymentCardFeatureMakerCheckers { get; set; }
        public virtual DbSet<SchemeTransactionAmountLimit> SchemeTransactionAmountLimits { get; set; }
        public virtual DbSet<SchemeTransactionAmountLimitMakerChecker> SchemeTransactionAmountLimitMakerCheckers { get; set; }


        public virtual DbSet<SchemeDemandDepositDetail> SchemeDemandDepositDetails { get; set; }
        public virtual DbSet<SchemeDemandDepositDetailMakerChecker> SchemeDemandDepositDetailMakerCheckers { get; set; }
        public virtual DbSet<SchemeDepositClosingMode> SchemeDepositClosingModes { get; set; }
        public virtual DbSet<SchemeDepositClosingModeMakerChecker> SchemeDepositClosingModeMakerCheckers { get; set; }
        public virtual DbSet<SchemeTermDepositDetail> SchemeTermDepositDetails { get; set; }
        public virtual DbSet<SchemeTermDepositDetailMakerChecker> SchemeTermDepositDetailMakerCheckers { get; set; }

        //############################# Transaction #############################
        public virtual DbSet<TransactionCashDenomination> TransactionCashDenominations { get; set; }
        public virtual DbSet<TransactionCashDenominationMakerChecker> TransactionCashDenominationMakerCheckers { get; set; }

        public virtual DbSet<TransactionCustomerAccount> TransactionCustomerAccounts { get; set; }
        public virtual DbSet<TransactionCustomerAccountMakerChecker> TransactionCustomerAccountMakerCheckers { get; set; }

        public virtual DbSet<TransactionGeneralLedger> TransactionGeneralLedgers { get; set; }
        public virtual DbSet<TransactionGeneralLedgerMakerChecker> TransactionGeneralLedgerMakerCheckers { get; set; }
        public virtual DbSet<TransactionGSTDetail> TransactionGSTDetails { get; set; }
        public virtual DbSet<TransactionGSTDetailMakerChecker> TransactionGSTDetailMakerCheckers { get; set; }

        public virtual DbSet<TransactionMaster> TransactionMasters { get; set; }
        public virtual DbSet<TransactionMasterMakerChecker> TransactionMasterMakerCheckers { get; set; }

        public virtual DbSet<TransactionCustomerAccountOtherSubscription> TransactionCustomerAccountOtherSubscriptions { get; set; }
        public virtual DbSet<TransactionCustomerAccountOtherSubscriptionMakerChecker> TransactionCustomerAccountOtherSubscriptionsMakerCheckers { get; set; }

        public virtual DbSet<SharesCapitalTransaction> SharesCapitalTransactions { get; set; }
        public virtual DbSet<SharesCapitalTransactionMakerChecker> SharesCapitalTransactionMakerCheckers { get; set; }

        public virtual DbSet<OpeningBalance> OpeningBalances { get; set; }
        public virtual DbSet<OpeningBalanceMakerChecker> OpeningBalanceMakerCheckers { get; set; }
        public virtual DbSet<OpeningBalanceDeposit> OpeningBalanceDeposits { get; set; }
        public virtual DbSet<OpeningBalanceLoan> OpeningBalanceLoans { get; set; }
        public virtual DbSet<OpeningBalanceShare> OpeningBalanceShares { get; set; }
        public virtual DbSet<OpeningBalanceInvestment> OpeningBalanceInvestments { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ F I N A N C I A L      S T A T E M E N T S @@@@@@@@@@@@@@@@@@@@@

        public virtual DbSet<FinancialYear> FinancialYears { get; set; }
        public virtual DbSet<TrialBalanceSheet> TrialBalanceSheets { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ A R C H I V E @@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentDocumentType> DocumentDocumentTypes { get; set; }
        public virtual DbSet<DocumentTranslation> DocumentTranslations { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DocumentTypeTranslation> DocumentTypeTranslations { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ C O N F I G U R A T I O N @@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<AppConfig> AppConfigs { get; set; }

        public virtual DbSet<NumberInWord> NumberInWords { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ E N T E R P R I S E @@@@@@@@@@@@@@@@@@@@@

        // ############################# Capital #############################
        public virtual DbSet<AuthorizedSharesCapital> AuthorizedSharesCapitals { get; set; }
        public virtual DbSet<AuthorizedSharesCapitalMakerChecker> AuthorizedSharesCapitalMakerCheckers { get; set; }

        // ############################# Establishment #############################
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationTranslation> OrganizationTranslations { get; set; }
        public virtual DbSet<OrganizationMakerChecker> OrganizationMakerCheckers { get; set; }
        public virtual DbSet<OrganizationTranslationMakerChecker> OrganizationTranslationMakerCheckers { get; set; }
        public virtual DbSet<OrganizationContactDetail> OrganizationContactDetails { get; set; }
        public virtual DbSet<OrganizationContactDetailMakerChecker> OrganizationContactDetailMakerCheckers { get; set; }
        public virtual DbSet<OrganizationFund> OrganizationFunds { get; set; }
        public virtual DbSet<OrganizationFundTranslation> OrganizationFundTranslations { get; set; }
        public virtual DbSet<OrganizationFundMakerChecker> OrganizationFundMakerCheckers { get; set; }
        public virtual DbSet<OrganizationFundTranslationMakerChecker> OrganizationFundTranslationMakerCheckers { get; set; }
        public virtual DbSet<OrganizationAuditClass> OrganizationAuditClasses { get; set; }

        public virtual DbSet<OrganizationLoanType> OrganizationLoanTypes { get; set; }
        public virtual DbSet<OrganizationLoanTypeTranslation> OrganizationLoanTypeTranslations { get; set; }
        public virtual DbSet<OrganizationLoanTypeMakerChecker> OrganizationLoanTypeMakerCheckers { get; set; }
        public virtual DbSet<OrganizationLoanTypeTranslationMakerChecker> OrganizationLoanTypeTranslationMakerCheckers { get; set; }

        public virtual DbSet<OrganizationGSTRegistrationDetail> OrganizationGSTRegistrationDetails { get; set; }
        public virtual DbSet<OrganizationGSTRegistrationDetailMakerChecker> OrganizationGSTRegistrationDetailMakerCheckers { get; set; }

        // ####################### Policy ##########################

        // ############################# Schedule #############################

        //*************************************** BusinessOffices *****************************************
        public virtual DbSet<BusinessNature> BusinessNatures { get; set; }
        public virtual DbSet<BusinessNatureTranslation> BusinessNatureTranslations { get; set; }
        public virtual DbSet<BusinessOffice> BusinessOffices { get; set; }
        public virtual DbSet<BusinessOfficeCoopRegistration> BusinessOfficeCoopRegistrations { get; set; }
        public virtual DbSet<BusinessOfficeCoopRegistrationMakerChecker> BusinessOfficeCoopRegistrationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeCoopRegistrationTranslation> BusinessOfficeCoopRegistrationTranslations { get; set; }
        public virtual DbSet<BusinessOfficeCoopRegistrationTranslationMakerChecker> BusinessOfficeCoopRegistrationTranslationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeDetail> BusinessOfficeDetails { get; set; }
        public virtual DbSet<BusinessOfficeDetailMakerChecker> BusinessOfficeDetailMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeMakerChecker> BusinessOfficeMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeModification> BusinessOfficeModifications { get; set; }
        public virtual DbSet<BusinessOfficeModificationMakerChecker> BusinessOfficeModificationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficePasswordPolicy> BusinessOfficePasswordPolicies { get; set; }
        public virtual DbSet<BusinessOfficePasswordPolicyMakerChecker> BusinessOfficePasswordPolicyMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeRBIRegistration> BusinessOfficeRBIRegistrations { get; set; }
        public virtual DbSet<BusinessOfficeRBIRegistrationMakerChecker> BusinessOfficeRBIRegistrationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeRBIRegistrationTranslation> BusinessOfficeRBIRegistrationTranslations { get; set; }
        public virtual DbSet<BusinessOfficeRBIRegistrationTranslationMakerChecker> BusinessOfficeRBIRegistrationTranslationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeTranslation> BusinessOfficeTranslations { get; set; }
        public virtual DbSet<BusinessOfficeTranslationMakerChecker> BusinessOfficeTranslationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeType> BusinessOfficeTypes { get; set; }
        public virtual DbSet<BusinessOfficeTypeMakerChecker> BusinessOfficeTypeMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeTypeTranslation> BusinessOfficeTypeTranslations { get; set; }
        public virtual DbSet<BusinessOfficeTypeTranslationMakerChecker> BusinessOfficeTypeTranslationMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeMenu> BusinessOfficeMenus { get; set; }
        public virtual DbSet<BusinessOfficeMenuMakerChecker> BusinessOfficeMenuMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeSpecialPermission> BusinessOfficeSpecialPermissions { get; set; }
        public virtual DbSet<BusinessOfficeSpecialPermissionMakerChecker> BusinessOfficeSpecialPermissionMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeTransactionLimit> BusinessOfficeTransactionLimits { get; set; }
        public virtual DbSet<BusinessOfficeTransactionLimitMakerChecker> BusinessOfficeTransactionLimitMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeCurrency> BusinessOfficeCurrencies { get; set; }
        public virtual DbSet<BusinessOfficeCurrencyMakerChecker> BusinessOfficeCurrencyMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeAccountNumber> BusinessOfficeAccountNumbers { get; set; }
        public virtual DbSet<BusinessOfficeAccountNumberMakerChecker> BusinessOfficeAccountNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeAgreementNumber> BusinessOfficeAgreementNumbers { get; set; }
        public virtual DbSet<BusinessOfficeAgreementNumberMakerChecker> BusinessOfficeAgreementNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeApplicationNumber> BusinessOfficeApplicationNumbers { get; set; }
        public virtual DbSet<BusinessOfficeApplicationNumberMakerChecker> BusinessOfficeApplicationNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeSharesCertificateNumber> BusinessOfficeSharesCertificateNumbers { get; set; }
        public virtual DbSet<BusinessOfficeSharesCertificateNumberMakerChecker> BusinessOfficeSharesCertificateNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeDepositCertificateNumber> BusinessOfficeDepositCertificateNumbers { get; set; }
        public virtual DbSet<BusinessOfficeDepositCertificateNumberMakerChecker> BusinessOfficeDepositCertificateNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficePersonInformationNumber> BusinessOfficePersonInformationNumbers { get; set; }
        public virtual DbSet<BusinessOfficePersonInformationNumberMakerChecker> BusinessOfficePersonInformationNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficePassbookNumber> BusinessOfficePassbookNumbers { get; set; }
        public virtual DbSet<BusinessOfficePassbookNumberMakerChecker> BusinessOfficePassbookNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeCustomerNumber> BusinessOfficeCustomerNumbers { get; set; }
        public virtual DbSet<BusinessOfficeCustomerNumberMakerChecker> BusinessOfficeCustomerNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeMemberNumber> BusinessOfficeMemberNumbers { get; set; }
        public virtual DbSet<BusinessOfficeMemberNumberMakerChecker> BusinessOfficeMemberNumberMakerCheckers { get; set; }
        public virtual DbSet<BusinessOfficeTransactionParameter> BusinessOfficeTransactionParameters { get; set; }
        public virtual DbSet<BusinessOfficeTransactionParameterMakerChecker> BusinessOfficeTransactionParameterMakerCheckers { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyTranslation> CurrencyTranslations { get; set; }

        // ############################# Schedule #############################
        // ############################# OfficeSchedule #############################
        public virtual DbSet<OfficeSchedule> OfficeSchedules { get; set; }
        public virtual DbSet<OfficeScheduleMakerChecker> OfficeScheduleMakerCheckers { get; set; }
        public virtual DbSet<OfficeScheduleTranslation> OfficeScheduleTranslations { get; set; }
        public virtual DbSet<OfficeScheduleTranslationMakerChecker> OfficeScheduleTranslationMakerCheckers { get; set; }
        public virtual DbSet<OfficeScheduleModification> OfficeScheduleModifications { get; set; }
        public virtual DbSet<OfficeScheduleModificationMakerChecker> OfficeScheduleModificationMakerCheckers { get; set; }

        // ############################# WorkingSchedule #############################
        public virtual DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public virtual DbSet<WorkingScheduleMakerChecker> WorkingScheduleMakerCheckers { get; set; }
        public virtual DbSet<WorkingScheduleTranslation> WorkingScheduleTranslations { get; set; }
        public virtual DbSet<WorkingScheduleTranslationMakerChecker> WorkingScheduleTranslationMakerCheckers { get; set; }
        public virtual DbSet<WorkingScheduleModification> WorkingScheduleModifications { get; set; }
        public virtual DbSet<WorkingScheduleModificationMakerChecker> WorkingScheduleModificationMakerCheckers { get; set; }


        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@ H E A L T H @@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<DiseaseTranslation> DiseaseTranslations { get; set; }



        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@ H U M A N     R E S O U R C E @@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // ###################### SERVANT ######################
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeMakerChecker> EmployeeMakerCheckers { get; set; }
        public virtual DbSet<EmployeeModification> EmployeeModifications { get; set; }
        public virtual DbSet<EmployeeModificationMakerChecker> EmployeeModificationMakerCheckers { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public virtual DbSet<EmployeeDepartmentMakerChecker> EmployeeDepartmentMakerCheckers { get; set; }
        public virtual DbSet<EmployeeDesignation> EmployeeDesignations { get; set; }
        public virtual DbSet<EmployeeDesignationMakerChecker> EmployeeDesignationMakerCheckers { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public virtual DbSet<EmployeeDetailMakerChecker> EmployeeDetailMakerCheckers { get; set; }
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual DbSet<EmployeeDocumentMakerChecker> EmployeeDocumentMakerCheckers { get; set; }
        public virtual DbSet<EmployeePerformanceRating> EmployeePerformanceRatings { get; set; }
        public virtual DbSet<EmployeePerformanceRatingMakerChecker> EmployeePerformanceRatingMakerCheckers { get; set; }
        public virtual DbSet<EmployeePhoto> EmployeePhotos { get; set; }
        public virtual DbSet<EmployeePhotoMakerChecker> EmployeePhotoMakerCheckers { get; set; }
        public virtual DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        public virtual DbSet<EmployeeSalaryStructureMakerChecker> EmployeeSalaryStructureMakerCheckers { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<EmployeeTypeTranslation> EmployeeTypeTranslations { get; set; }
        public virtual DbSet<EmployeeWorkingSchedule> EmployeeWorkingSchedules { get; set; }
        public virtual DbSet<EmployeeWorkingScheduleMakerChecker> EmployeeWorkingScheduleMakerCheckers { get; set; }

        // #####################################################################################
        public virtual DbSet<ContentItem> ContentItems { get; set; }
        public virtual DbSet<ContentItemMakerChecker> ContentItemMakerCheckers { get; set; }
        public virtual DbSet<ContentItemTranslation> ContentItemTranslations { get; set; }
        public virtual DbSet<ContentItemTranslationMakerChecker> ContentItemTranslationMakerCheckers { get; set; }
        public virtual DbSet<ContentItemModification> ContentItemModifications { get; set; }
        public virtual DbSet<ContentItemModificationMakerChecker> ContentItemModificationMakerCheckers { get; set; }
        public virtual DbSet<EvaluationSection> EvaluationSections { get; set; }
        public virtual DbSet<EvaluationSectionMakerChecker> EvaluationSectionMakerCheckers { get; set; }
        public virtual DbSet<EvaluationSectionTranslation> EvaluationSectionTranslations { get; set; }
        public virtual DbSet<EvaluationSectionTranslationMakerChecker> EvaluationSectionTranslationMakerCheckers { get; set; }
        public virtual DbSet<EvaluationSectionModification> EvaluationSectionModifications { get; set; }
        public virtual DbSet<EvaluationSectionModificationMakerChecker> EvaluationSectionModificationMakerCheckers { get; set; }
        public virtual DbSet<EvaluationSectorContentItem> EvaluationSectorContentItems { get; set; }
        public virtual DbSet<EvaluationSectorContentItemMakerChecker> EvaluationSectorContentItemMakerCheckers { get; set; }
        public virtual DbSet<SalaryBreakup> SalaryBreakups { get; set; }
        public virtual DbSet<SalaryBreakupTranslation> SalaryBreakupTranslations { get; set; }


        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@ IRDAI @@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public virtual DbSet<InsuranceCompanyTranslation> InsuranceCompanyTranslations { get; set; }
        public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }
        public virtual DbSet<InsuranceTypeTranslation> InsuranceTypeTranslations { get; set; }


        // @@@@@@@@@@@@@@@@@@ M A C H I N E      L E A R N I N G @@@@@@@@@@@@@@@@@@
        // ############################# Enterprise #############################
        public virtual DbSet<MLChapter> MLChapters { get; set; }
        public virtual DbSet<MLChapterPoint> MLChapterPoints { get; set; }
        public virtual DbSet<MLParameterConfig> MLParameterConfigs { get; set; }
        public virtual DbSet<MLSubject> MLSubjects { get; set; }
        public virtual DbSet<MLWord> MLWords { get; set; }
        public virtual DbSet<MLWordDefination> MLWordDefinations { get; set; }
        public virtual DbSet<MLWordDefinationTranslation> MLWordDefinationTranslations { get; set; }
        public virtual DbSet<MLWordTranslation> MLWordTranslations { get; set; }





        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@ M A N A G E M E N T @@@@@@@@@@@@@@@@@@@@@@@@
        // ############################# CONFERENCE #############################
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<MeetingMakerChecker> MeetingMakerCheckers { get; set; }
        public virtual DbSet<MeetingModification> MeetingModifications { get; set; }
        public virtual DbSet<MeetingModificationMakerChecker> MeetingModificationMakerCheckers { get; set; }
        public virtual DbSet<MeetingTranslation> MeetingTranslations { get; set; }
        public virtual DbSet<MeetingTranslationMakerChecker> MeetingTranslationMakerCheckers { get; set; }
        public virtual DbSet<MeetingAgenda> MeetingAgendas { get; set; }
        public virtual DbSet<MeetingAgendaMakerChecker> MeetingAgendaMakerCheckers { get; set; }
        public virtual DbSet<MeetingAllowance> MeetingAllowances { get; set; }
        public virtual DbSet<MeetingAllowanceMakerChecker> MeetingAllowanceMakerCheckers { get; set; }
        public virtual DbSet<MeetingAllowanceModification> MeetingAllowanceModifications { get; set; }
        public virtual DbSet<MeetingAllowanceModificationMakerChecker> MeetingAllowanceModificationMakerCheckers { get; set; }
        public virtual DbSet<MeetingAllowanceTranslation> MeetingAllowanceTranslations { get; set; }
        public virtual DbSet<MeetingAllowanceTranslationMakerChecker> MeetingAllowanceTranslationMakerCheckers { get; set; }
        public virtual DbSet<MeetingInviteeBoardOfDirector> MeetingInviteeBoardOfDirectors { get; set; }
        public virtual DbSet<MeetingInviteeBoardOfDirectorMakerChecker> MeetingInviteeBoardOfDirectorMakerCheckers { get; set; }
        public virtual DbSet<MeetingInviteeMember> MeetingInviteeMembers { get; set; }
        public virtual DbSet<MeetingInviteeMemberMakerChecker> MeetingInviteeMemberMakerCheckers { get; set; }
        public virtual DbSet<MeetingNotice> MeetingNotices { get; set; }
        public virtual DbSet<MeetingNoticeMakerChecker> MeetingNoticeMakerCheckers { get; set; }

        public virtual DbSet<MeetingType> MeetingTypes { get; set; }
        public virtual DbSet<MeetingTypeTranslation> MeetingTypeTranslations { get; set; }

        public virtual DbSet<NoticeMedia> NoticeMedias { get; set; }
        public virtual DbSet<NoticeMediaTranslation> NoticeMediaTranslations { get; set; }

        public virtual DbSet<MinuteOfMeetingAgenda> MinuteOfMeetingAgendas { get; set; }
        public virtual DbSet<MinuteOfMeetingAgendaMakerChecker> MinuteOfMeetingAgendaMakerCheckers { get; set; }

        public virtual DbSet<MinuteOfMeetingAgendaSpokesperson> MinuteOfMeetingAgendaSpokespeople { get; set; }
        public virtual DbSet<MinuteOfMeetingAgendaSpokespersonMakerChecker> MinuteOfMeetingAgendaSpokespersonMakerCheckers { get; set; }

        public virtual DbSet<MinuteOfMeetingAgendaSpokespersonTranslation> MinuteOfMeetingAgendaSpokespersonTranslations { get; set; }
        public virtual DbSet<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker> MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers { get; set; }

        // ############################# DOCKET #############################
        // ############################# Agenda #############################
        public virtual DbSet<Agenda> Agendas { get; set; }
        public virtual DbSet<AgendaMakerChecker> AgendaMakerCheckers { get; set; }
        public virtual DbSet<AgendaModification> AgendaModifications { get; set; }
        public virtual DbSet<AgendaModificationMakerChecker> AgendaModificationMakerCheckers { get; set; }
        public virtual DbSet<AgendaTranslation> AgendaTranslations { get; set; }
        public virtual DbSet<AgendaTranslationMakerChecker> AgendaTranslationMakerCheckers { get; set; }
        public virtual DbSet<AgendaMeetingType> AgendaMeetingTypes { get; set; }
        public virtual DbSet<AgendaMeetingTypeMakerChecker> AgendaMeetingTypeMakerCheckers { get; set; }

        // ############################# Notification #############################
        // @@@@@@@@@@@@@@@@@@@@@ Event @@@@@@@@@@@@@@@@@@@@

        public virtual DbSet<EventMaster> EventMasters { get; set; }
        public virtual DbSet<EventMasterMakerChecker> EventMasterMakerCheckers { get; set; }
        public virtual DbSet<EventMasterModification> EventMasterModifications { get; set; }
        public virtual DbSet<EventMasterModificationMakerChecker> EventMasterModificationMakerCheckers { get; set; }
        public virtual DbSet<EventMasterTranslation> EventMasterTranslations { get; set; }
        public virtual DbSet<EventMasterTranslationMakerChecker> EventMasterTranslationMakerCheckers { get; set; }
        public virtual DbSet<EventRepeatReminder> EventRepeatReminders { get; set; }
        public virtual DbSet<EventRepeatReminderMakerChecker> EventRepeatReminderMakerCheckers { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<EventTypeTranslation> EventTypeTranslations { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@@@@@ M A S T E R @@@@@@@@@@@@@@@@@@@@@@@@@

        // ##################### ACCCOUNT #####################
        public virtual DbSet<FundTransferFrequency> FundTransferFrequencies { get; set; }
        public virtual DbSet<FundTransferFrequencyTranslation> FundTransferFrequencyTranslations { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<CustomerTypeTranslation> CustomerTypeTranslations { get; set; }
        public virtual DbSet<JointAccountHolderType> JointAccountHolderTypes { get; set; }
        public virtual DbSet<JointAccountHolderTypeTranslation> JointAccountHolderTypeTranslations { get; set; }

        public virtual DbSet<ChequeReturnReason> ChequeReturnReasons { get; set; }
        public virtual DbSet<ChequeReturnReasonTranslation> ChequeReturnReasonTranslations { get; set; }

        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<FrequencyTranslation> FrequencyTranslations { get; set; }
        public virtual DbSet<PaymentCard> PaymentCards { get; set; }
        public virtual DbSet<PaymentCardTranslation> PaymentCardTranslations { get; set; }

        public virtual DbSet<InterestRateType> InterestRateTypes { get; set; }
        public virtual DbSet<InterestType> InterestTypes { get; set; }
        public virtual DbSet<InterestRateChargedDuration> InterestRateChargedDurations { get; set; }
        public virtual DbSet<InterestRateChargedDurationTranslation> InterestRateChargedDurationTranslations { get; set; }

        public virtual DbSet<Responsibility> Responsibilities { get; set; }
        public virtual DbSet<ResponsibilityMakerChecker> ResponsibilityMakerCheckers { get; set; }
        public virtual DbSet<ResponsibilityModification> ResponsibilityModifications { get; set; }
        public virtual DbSet<ResponsibilityModificationMakerChecker> ResponsibilityModificationMakerCheckers { get; set; }
        public virtual DbSet<ResponsibilityTranslation> ResponsibilityTranslations { get; set; }
        public virtual DbSet<ResponsibilityTranslationMakerChecker> ResponsibilityTranslationMakerCheckers { get; set; }

        // ############################# Layout #############################
        public virtual DbSet<PayInPayOutMode> PayInPayOutModes { get; set; }
        public virtual DbSet<PayInPayOutModeTranslation> PayInPayOutModeTranslations { get; set; }
        public virtual DbSet<SweepOutFrequency> SweepOutFrequencies { get; set; }
        public virtual DbSet<SweepOutFrequencyTranslation> SweepOutFrequencyTranslations { get; set; }

        // ############################# Transaction #############################
        public virtual DbSet<Denomination> Denominations { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<TransactionTypeTranslation> TransactionTypeTranslations { get; set; }


        // #############################  #############################
        public virtual DbSet<AreaOfOperation> AreaOfOperations { get; set; }
        public virtual DbSet<AreaOfOperationTranslation> AreaOfOperationTranslations { get; set; }

        public virtual DbSet<CoopSocietyClass> CoopSocietyClasses { get; set; }
        public virtual DbSet<CoopSocietyClassTranslation> CoopSocietyClassTranslations { get; set; }

        public virtual DbSet<CoopSocietySubClass> CoopSocietySubClasses { get; set; }
        public virtual DbSet<CoopSocietySubClassTranslation> CoopSocietySubClassTranslations { get; set; }
        // ############################# CreditSociety #############################
        public virtual DbSet<CreditSociety> CreditSocieties { get; set; }
        public virtual DbSet<CreditSocietyMakerChecker> CreditSocietyMakerCheckers { get; set; }
        public virtual DbSet<CreditSocietyTranslation> CreditSocietyTranslations { get; set; }
        public virtual DbSet<CreditSocietyTranslationMakerChecker> CreditSocietyTranslationMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Center @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<Center> Centers { get; set; }
        public virtual DbSet<CenterDemographicDetail> CenterDemographicDetails { get; set; }
        public virtual DbSet<CenterDemographicDetailMakerChecker> CenterDemographicDetailMakerCheckers { get; set; }
        public virtual DbSet<CenterISOCode> CenterISOCodes { get; set; }
        public virtual DbSet<CenterISOCodeMakerChecker> CenterISOCodeMakerCheckers { get; set; }
        public virtual DbSet<CenterMakerChecker> CenterMakerCheckers { get; set; }
        public virtual DbSet<CenterOccupation> CenterOccupations { get; set; }
        public virtual DbSet<CenterOccupationMakerChecker> CenterOccupationMakerCheckers { get; set; }
        public virtual DbSet<CenterTradingEntityDetail> CenterTradingEntityDetails { get; set; }
        public virtual DbSet<CenterTradingEntityDetailMakerChecker> CenterTradingEntityDetailMakerCheckers { get; set; }
        public virtual DbSet<CenterTranslation> CenterTranslations { get; set; }
        public virtual DbSet<CenterTranslationMakerChecker> CenterTranslationMakerCheckers { get; set; }
        public virtual DbSet<CountryAdditionalDetail> CountryAdditionalDetails { get; set; }
        public virtual DbSet<CountryAdditionalDetailMakerChecker> CountryAdditionalDetailMakerCheckers { get; set; }
        public virtual DbSet<CenterModification> CenterModifications { get; set; }
        public virtual DbSet<CenterModificationMakerChecker> CenterModificationMakerCheckers { get; set; }

        // ############################# Enterprise #############################

        // @@@@@@@@@@@@@@@@@@@@@ OrganizationType @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<FinancialOrganizationType> FinancialOrganizationTypes { get; set; }
        public virtual DbSet<FinancialOrganizationTypeTranslation> FinancialOrganizationTypeTranslations { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Power And Function @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<PowerAndFunction> PowerAndFunctions { get; set; }
        public virtual DbSet<PowerAndFunctionMakerChecker> PowerAndFunctionMakerCheckers { get; set; }
        public virtual DbSet<PowerAndFunctionTranslation> PowerAndFunctionTranslations { get; set; }
        public virtual DbSet<PowerAndFunctionTranslationMakerChecker> PowerAndFunctionTranslationMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Board Of Director @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<BoardOfDirector> BoardOfDirectors { get; set; }
        public virtual DbSet<BoardOfDirectorMakerChecker> BoardOfDirectorMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Board Of Director Power And Function @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<BoardOfDirectorPowerAndFunction> BoardOfDirectorPowerAndFunctions { get; set; }
        public virtual DbSet<BoardOfDirectorPowerAndFunctionMakerChecker> BoardOfDirectorPowerAndFunctionMakerCheckers { get; set; }
        public virtual DbSet<BoardOfDirectorPowerAndFunctionTranslation> BoardOfDirectorPowerAndFunctionTranslations { get; set; }
        public virtual DbSet<BoardOfDirectorPowerAndFunctionTranslationMakerChecker> BoardOfDirectorPowerAndFunctionTranslationMakerCheckers { get; set; }

        // ############################# General #############################

        // @@@@@@@@@@@@@@@@@@@@@ CastCategory @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<CastCategory> CastCategories { get; set; }
        public virtual DbSet<CastCategoryTranslation> CastCategoryTranslations { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<ReligionTranslation> ReligionTranslations { get; set; }
        public virtual DbSet<ReservationCategory> ReservationCategories { get; set; }
        public virtual DbSet<ReservationCategoryTranslation> ReservationCategoryTranslations { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Department @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentMakerChecker> DepartmentMakerCheckers { get; set; }
        public virtual DbSet<DepartmentModification> DepartmentModifications { get; set; }
        public virtual DbSet<DepartmentModificationMakerChecker> DepartmentModificationMakerCheckers { get; set; }
        public virtual DbSet<DepartmentTranslation> DepartmentTranslations { get; set; }
        public virtual DbSet<DepartmentTranslationMakerChecker> DepartmentTranslationMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Designation @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<DesignationMakerChecker> DesignationMakerCheckers { get; set; }
        public virtual DbSet<DesignationModification> DesignationModifications { get; set; }
        public virtual DbSet<DesignationModificationMakerChecker> DesignationModificationMakerCheckers { get; set; }
        public virtual DbSet<DesignationTranslation> DesignationTranslations { get; set; }
        public virtual DbSet<DesignationTranslationMakerChecker> DesignationTranslationMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ EducationQualification @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<EducationQualification> EducationQualifications { get; set; }
        public virtual DbSet<EducationQualificationTranslation> EducationQualificationTranslations { get; set; }

        //####################### Group ###################
        //@@@@@@@@@@@@@@@@@@@@@@@@@@Group Of AccountClass@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<GroupMaster> GroupMasters { get; set; }
        public virtual DbSet<GroupMasterMakerChecker> GroupMasterMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ IncomeSource @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<IncomeSource> IncomeSources { get; set; }
        public virtual DbSet<IncomeSourceTranslation> IncomeSourceTranslations { get; set; }

        //####################### Mask ###################
        //@@@@@@@@@@@@@@@@@@@@@@@@@@ Mask Type @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<MaskType> MaskTypes { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ LoanReason @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<LoanReason> LoanReasons { get; set; }
        public virtual DbSet<LoanReasonTranslation> LoanReasonTranslations { get; set; }

        // ############################# NoticeSchedule #############################

        public virtual DbSet<NoticeSchedule> NoticeSchedules { get; set; }
        public virtual DbSet<NoticeScheduleMakerChecker> NoticeScheduleMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleModification> NoticeScheduleModifications { get; set; }
        public virtual DbSet<NoticeScheduleModificationMakerChecker> NoticeScheduleModificationMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleOnDate> NoticeScheduleOnDates { get; set; }
        public virtual DbSet<NoticeScheduleOnDateMakerChecker> NoticeScheduleOnDateMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleOnDateTime> NoticeScheduleOnDateTimes { get; set; }
        public virtual DbSet<NoticeScheduleOnDateTimeMakerChecker> NoticeScheduleOnDateTimeMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfMonth> NoticeScheduleOnDaysOfMonths { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfMonthMakerChecker> NoticeScheduleOnDaysOfMonthMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfMonthTime> NoticeScheduleOnDaysOfMonthTimes { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfMonthTimeMakerChecker> NoticeScheduleOnDaysOfMonthTimeMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfWeek> NoticeScheduleOnDaysOfWeeks { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfWeekMakerChecker> NoticeScheduleOnDaysOfWeekMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfWeekTime> NoticeScheduleOnDaysOfWeekTimes { get; set; }
        public virtual DbSet<NoticeScheduleOnDaysOfWeekTimeMakerChecker> NoticeScheduleOnDaysOfWeekTimeMakerCheckers { get; set; }
        public virtual DbSet<NoticeScheduleTranslation> NoticeScheduleTranslations { get; set; }
        public virtual DbSet<NoticeScheduleTranslationMakerChecker> NoticeScheduleTranslationMakerCheckers { get; set; }

        // ################### Schedule  ###################

        public virtual DbSet<CommunicationMedia> CommunicationMedias { get; set; }
        public virtual DbSet<CommunicationMediaTranslation> CommunicationMediaTranslations { get; set; }
        public virtual DbSet<NoticeType> NoticeTypes { get; set; }
        public virtual DbSet<NoticeTypeTemplate> NoticeTypeTemplates { get; set; }
        public virtual DbSet<NoticeTypeTranslation> NoticeTypeTranslations { get; set; }
        public virtual DbSet<DaysOfMonth> DaysOfMonths { get; set; }
        public virtual DbSet<DaysOfMonthTranslation> DaysOfMonthTranslations { get; set; }
        public virtual DbSet<DaysOfWeek> DaysOfWeeks { get; set; }
        public virtual DbSet<DaysOfWeekTranslation> DaysOfWeekTranslations { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleFrequency> ScheduleFrequencies { get; set; }
        public virtual DbSet<ScheduleFrequencyMakerChecker> ScheduleFrequencyMakerCheckers { get; set; }
        public virtual DbSet<ScheduleMakerChecker> ScheduleMakerCheckers { get; set; }
        public virtual DbSet<ScheduleModification> ScheduleModifications { get; set; }
        public virtual DbSet<ScheduleModificationMakerChecker> ScheduleModificationMakerCheckers { get; set; }
        public virtual DbSet<ScheduleTranslation> ScheduleTranslations { get; set; }
        public virtual DbSet<ScheduleTranslationMakerChecker> ScheduleTranslationMakerCheckers { get; set; }
        public virtual DbSet<ScheduleType> ScheduleTypes { get; set; }
        public virtual DbSet<ScheduleTypeTranslation> ScheduleTypeTranslations { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ Occupation  @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<OccupationTranslation> OccupationTranslations { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ SMS  @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<DLTHeaderRegistration> DLTHeaderRegistrations { get; set; }
        public virtual DbSet<DLTHeaderRegistrationMakerChecker> DLTHeaderRegistrationMakerCheckers { get; set; }
        public virtual DbSet<DLTPortal> DLTPortals { get; set; }
        public virtual DbSet<DLTRegistration> DLTRegistrations { get; set; }
        public virtual DbSet<DLTRegistrationMakerChecker> DLTRegistrationMakerCheckers { get; set; }
        public virtual DbSet<DLTTemplateRegistration> DLTTemplateRegistrations { get; set; }
        public virtual DbSet<DLTTemplateRegistrationMakerChecker> DLTTemplateRegistrationMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@ TradingEntity  @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<TradingEntity> TradingEntities { get; set; }
        public virtual DbSet<TradingEntityTranslation> TradingEntityTranslations { get; set; }

        // ############################# Master --> PersonInformaton #############################
        // @@@@@@@@@@@@@@@@@@@@@ Shares  @@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<MemberTypeTranslation> MemberTypeTranslations { get; set; }

        // ############################# Vehicle #############################
        public virtual DbSet<VehicleBodyType> VehicleBodyTypes { get; set; }
        public virtual DbSet<VehicleBodyTypeTranslation> VehicleBodyTypeTranslations { get; set; }
        public virtual DbSet<VehicleMake> VehicleMakes { get; set; }
        public virtual DbSet<VehicleMakeMakerChecker> VehicleMakeMakerCheckers { get; set; }
        public virtual DbSet<VehicleMakeModification> VehicleMakeModifications { get; set; }
        public virtual DbSet<VehicleMakeModificationMakerChecker> VehicleMakeModificationMakerCheckers { get; set; }
        public virtual DbSet<VehicleMakeTranslation> VehicleMakeTranslations { get; set; }
        public virtual DbSet<VehicleMakeTranslationMakerChecker> VehicleMakeTranslationMakerCheckers { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }
        public virtual DbSet<VehicleModelMakerChecker> VehicleModelMakerCheckers { get; set; }
        public virtual DbSet<VehicleModelModification> VehicleModelModifications { get; set; }
        public virtual DbSet<VehicleModelTranslation> VehicleModelTranslations { get; set; }
        public virtual DbSet<VehicleModelTranslationMakerChecker> VehicleModelTranslationMakerCheckers { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<VehicleTypeTranslation> VehicleTypeTranslations { get; set; }
        public virtual DbSet<VehicleVariant> VehicleVariants { get; set; }
        public virtual DbSet<VehicleVariantMakerChecker> VehicleVariantMakerCheckers { get; set; }
        public virtual DbSet<VehicleVariantModification> VehicleVariantModifications { get; set; }
        public virtual DbSet<VehicleVariantModificationMakerChecker> VehicleVariantModificationMakerCheckers { get; set; }
        public virtual DbSet<VehicleVariantTranslation> VehicleVariantTranslations { get; set; }
        public virtual DbSet<VehicleVariantTranslationMakerChecker> VehicleVariantTranslationMakerCheckers { get; set; }
        public virtual DbSet<VehicleModelModificationMakerChecker> VehicleModelModificationMakerCheckers { get; set; }
        public virtual DbSet<VehicleSupplier> VehicleSuppliers { get; set; }
        public virtual DbSet<VehicleSupplierMakerChecker> VehicleSupplierMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ P A R A M E T E R @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // ############################# Account #############################
        // ############################# Layout #############################

        public virtual DbSet<DepositSchemeParameter> DepositSchemeParameters { get; set; }
        public virtual DbSet<DepositSchemeParameterMakerChecker> DepositSchemeParameterMakerCheckers { get; set; }
        public virtual DbSet<LoanSchemeParameter> LoanSchemeParameters { get; set; }
        public virtual DbSet<LoanSchemeParameterMakerChecker> LoanSchemeParameterMakerCheckers { get; set; }
        public virtual DbSet<ByLawsLoanScheduleParameter> ByLawsLoanScheduleParameters { get; set; }
        public virtual DbSet<ByLawsLoanScheduleParameterMakerChecker> ByLawsLoanScheduleParameterMakerCheckers { get; set; }
        public virtual DbSet<SharesCapitalSchemeParameter> SharesCapitalSchemeParameters { get; set; }
        public virtual DbSet<SharesCapitalSchemeParameterMakerChecker> SharesCapitalSchemeParameterMakerCheckers { get; set; }

        // ############################# Transaction #############################

        public virtual DbSet<TransactionParameter> TransactionParameters { get; set; }
        public virtual DbSet<TransactionParameterMakerChecker> TransactionParameterMakerCheckers { get; set; }

        // ############################# BoardOfDirector #############################

        public virtual DbSet<BoardOfDirectorParameter> BoardOfDirectorParameters { get; set; }
        public virtual DbSet<BoardOfDirectorParameterMakerChecker> BoardOfDirectorParameterMakerCheckers { get; set; }

        // ############################# ByLaws #############################

        public virtual DbSet<AssuranceDeedFormat> AssuranceDeedFormats { get; set; }

        // ############################# Legal #############################
        public virtual DbSet<SharesCapitalActParameter> SharesCapitalActParameters { get; set; }
        public virtual DbSet<SharesCapitalActParameterMakerChecker> SharesCapitalActParameterMakerCheckers { get; set; }
        public virtual DbSet<SharesCapitalByLawsParameter> SharesCapitalByLawsParameters { get; set; }
        public virtual DbSet<SharesCapitalByLawsParameterMakerChecker> SharesCapitalByLawsParameterMakerCheckers { get; set; }
        public virtual DbSet<SharesCapitalRuleParameter> SharesCapitalRuleParameters { get; set; }
        public virtual DbSet<SharesCapitalRuleParameterMakerChecker> SharesCapitalRuleParameterMakerCheckers { get; set; }
        public virtual DbSet<IncomeTaxActParameter> IncomeTaxActParameters { get; set; }

        // ############################# Person #############################       
        public virtual DbSet<PersonInformationParameter> PersonInformationParameters { get; set; }
        public virtual DbSet<PersonInformationParameterMakerChecker> PersonInformationParameterMakerCheckers { get; set; }

        public virtual DbSet<PersonInformationParameterDocumentType> PersonInformationParameterDocumentTypes { get; set; }
        public virtual DbSet<PersonInformationParameterDocumentTypeMakerChecker> PersonInformationParameterDocumentTypeMakerCheckers { get; set; }

        public virtual DbSet<PersonInformationParameterNoticeType> PersonInformationParameterNoticeTypes { get; set; }
        public virtual DbSet<PersonInformationParameterNoticeTypeMakerChecker> PersonInformationParameterNoticeTypeMakerCheckers { get; set; }

        // ############################# Security #############################
        public virtual DbSet<UserAuthenticationParameter> UserAuthenticationParameters { get; set; }
        public virtual DbSet<UserAuthenticationParameterMakerChecker> UserAuthenticationParameterMakerCheckers { get; set; }

        // @@@@@@@@@@@@@@@@@ P E R S O N     I N F O R M A T I O N @@@@@@@@@@@@@@@@@

        // ########################## MASTER  #######################
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GenderTranslation> GenderTranslations { get; set; }
        public virtual DbSet<GuardianType> GuardianTypes { get; set; }
        public virtual DbSet<GuardianTypeTranslation> GuardianTypeTranslations { get; set; }
        public virtual DbSet<Identification> Identifications { get; set; }
        public virtual DbSet<IdentificationTranslation> IdentificationTranslations { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<MaritalStatusTranslation> MaritalStatusTranslations { get; set; }
        public virtual DbSet<OwnershipType> OwnershipTypes { get; set; }
        public virtual DbSet<OwnershipTypeTranslation> OwnershipTypeTranslations { get; set; }
        public virtual DbSet<PersonType> PersonTypes { get; set; }
        public virtual DbSet<PersonTypeTranslation> PersonTypeTranslations { get; set; }
        public virtual DbSet<PersonCategory> PersonCategories { get; set; }
        public virtual DbSet<PersonStatus> PersonStatuses { get; set; }
        public virtual DbSet<PersonStatusMakerChecker> PersonStatusMakerCheckers { get; set; }
        public virtual DbSet<PersonCategoryTranslation> PersonCategoryTranslations { get; set; }
        public virtual DbSet<PhysicalStatus> PhysicalStatuses { get; set; }
        public virtual DbSet<PhysicalStatusTranslation> PhysicalStatusTranslations { get; set; }
        public virtual DbSet<PovertyStatus> PovertyStatuses { get; set; }
        public virtual DbSet<PovertyStatusTranslation> PovertyStatusTranslations { get; set; }
        public virtual DbSet<Prefix> Prefixes { get; set; }
        public virtual DbSet<PrefixTranslation> PrefixTranslations { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<RelationTranslation> RelationTranslations { get; set; }
        public virtual DbSet<ResidenceType> ResidenceTypes { get; set; }
        public virtual DbSet<ResidenceTypeTranslation> ResidenceTypeTranslations { get; set; }

        // ##########################         #######################
        public virtual DbSet<ForeignerPerson> ForeignerPersons { get; set; }
        public virtual DbSet<ForeignerPersonMakerChecker> ForeignerPersonMakerCheckers { get; set; }
        public virtual DbSet<GuardianPerson> GuardianPersons { get; set; }
        public virtual DbSet<GuardianPersonMakerChecker> GuardianPersonMakerCheckers { get; set; }
        public virtual DbSet<GuardianPersonTranslation> GuardianPersonTranslations { get; set; }
        public virtual DbSet<GuardianPersonTranslationMakerChecker> GuardianPersonTranslationMakerCheckers { get; set; }
        public virtual DbSet<JewelAssayer> JewelAssayers { get; set; }
        public virtual DbSet<JewelAssayerMakerChecker> JewelAssayerMakerCheckers { get; set; }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonAdditionalDetail> PersonAdditionalDetails { get; set; }
        public virtual DbSet<PersonAdditionalDetailMakerChecker> PersonAdditionalDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonAdditionalDetailTranslation> PersonAdditionalDetailTranslations { get; set; }
        public virtual DbSet<PersonAdditionalDetailTranslationMakerChecker> PersonAdditionalDetailTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonAdditionalIncomeDetail> PersonAdditionalIncomeDetails { get; set; }
        public virtual DbSet<PersonAdditionalIncomeDetailMakerChecker> PersonAdditionalIncomeDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonAddress> PersonAddresses { get; set; }
        public virtual DbSet<PersonAddressMakerChecker> PersonAddressMakerCheckers { get; set; }
        public virtual DbSet<PersonAddressTranslation> PersonAddressTranslations { get; set; }
        public virtual DbSet<PersonAddressTranslationMakerChecker> PersonAddressTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonAgricultureAsset> PersonAgricultureAssets { get; set; }
        public virtual DbSet<PersonAgricultureAssetDocument> PersonAgricultureAssetDocuments { get; set; }
        public virtual DbSet<PersonAgricultureAssetDocumentMakerChecker> PersonAgricultureAssetDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonAgricultureAssetMakerChecker> PersonAgricultureAssetMakerCheckers { get; set; }
        public virtual DbSet<PersonBankDetail> PersonBankDetails { get; set; }
        public virtual DbSet<PersonBankDetailMakerChecker> PersonBankDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonBankDetailDocument> PersonBankDetailDocuments { get; set; }
        public virtual DbSet<PersonBankDetailDocumentMakerChecker> PersonBankDetailDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonGroupAuthorizedSignatory> PersonGroupAuthorizedSignatories { get; set; }
        public virtual DbSet<PersonGroupAuthorizedSignatoryMakerChecker> PersonGroupAuthorizedSignatoryMakerCheckers { get; set; }
        public virtual DbSet<PersonGroupAuthorizedSignatoryTranslation> PersonGroupAuthorizedSignatoryTranslations { get; set; }
        public virtual DbSet<PersonGroupAuthorizedSignatoryTranslationMakerChecker> PersonGroupAuthorizedSignatoryTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonGroup> PersonGroups { get; set; }
        public virtual DbSet<PersonGroupMakerChecker> PersonGroupMakerCheckers { get; set; }
        public virtual DbSet<PersonBoardOfDirectorRelation> PersonBoardOfDirectorRelations { get; set; }
        public virtual DbSet<PersonBoardOfDirectorRelationMakerChecker> PersonBoardOfDirectorRelationMakerCheckers { get; set; }
        public virtual DbSet<PersonBorrowingDetail> PersonBorrowingDetails { get; set; }
        public virtual DbSet<PersonBorrowingDetailMakerChecker> PersonBorrowingDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonBorrowingDetailTranslation> PersonBorrowingDetailTranslations { get; set; }
        public virtual DbSet<PersonBorrowingDetailTranslationMakerChecker> PersonBorrowingDetailTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonChronicDisease> PersonChronicDiseases { get; set; }
        public virtual DbSet<PersonChronicDiseaseMakerChecker> PersonChronicDiseaseMakerCheckers { get; set; }
        public virtual DbSet<PersonContactDetail> PersonContactDetails { get; set; }
        public virtual DbSet<PersonContactDetailMakerChecker> PersonContactDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonCourtCase> PersonCourtCases { get; set; }
        public virtual DbSet<PersonCourtCaseMakerChecker> PersonCourtCaseMakerCheckers { get; set; }
        public virtual DbSet<PersonCommoditiesAsset> PersonCommoditiesAssets { get; set; }
        public virtual DbSet<PersonCommoditiesAssetMakerChecker> PersonCommoditiesAssetMakerCheckers { get; set; }
        public virtual DbSet<PersonCreditRating> PersonCreditRatings { get; set; }
        public virtual DbSet<PersonCreditRatingMakerChecker> PersonCreditRatingMakerCheckers { get; set; }
        public virtual DbSet<PersonCustomField> PersonCustomFields { get; set; }
        public virtual DbSet<PersonCustomFieldMakerChecker> PersonCustomFieldMakerCheckers { get; set; }
        public virtual DbSet<PersonDeath> PersonDeaths { get; set; }
        public virtual DbSet<PersonDeathMakerChecker> PersonDeathMakerCheckers { get; set; }
        public virtual DbSet<PersonDeathDocument> PersonDeathDocuments { get; set; }
        public virtual DbSet<PersonDeathDocumentMakerChecker> PersonDeathDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonEmploymentDetail> PersonEmploymentDetails { get; set; }
        public virtual DbSet<PersonEmploymentDetailMakerChecker> PersonEmploymentDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonEmploymentDetailTranslation> PersonEmploymentDetailTranslations { get; set; }
        public virtual DbSet<PersonEmploymentDetailTranslationMakerChecker> PersonEmploymentDetailTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonFamilyDetail> PersonFamilyDetails { get; set; }
        public virtual DbSet<PersonFamilyDetailMakerChecker> PersonFamilyDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonFamilyDetailTranslation> PersonFamilyDetailTranslations { get; set; }
        public virtual DbSet<PersonFamilyDetailTranslationMakerChecker> PersonFamilyDetailTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonFinancialAsset> PersonFinancialAssets { get; set; }
        public virtual DbSet<PersonFinancialAssetMakerChecker> PersonFinancialAssetMakerCheckers { get; set; }
        public virtual DbSet<PersonFinancialAssetTranslation> PersonFinancialAssetTranslations { get; set; }
        public virtual DbSet<PersonFinancialAssetTranslationMakerChecker> PersonFinancialAssetTranslationMakerCheckers { get; set; }
        public virtual DbSet<PersonFinancialAssetDocument> PersonFinancialAssetDocuments { get; set; }
        public virtual DbSet<PersonFinancialAssetDocumentMakerChecker> PersonFinancialAssetDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonFinancialAssetBorrowingDetail> PersonFinancialAssetBorrowingDetails { get; set; }
        public virtual DbSet<PersonFinancialAssetBorrowingDetailMakerChecker> PersonFinancialAssetBorrowingDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonGSTRegistrationDetail> PersonGSTRegistrationDetails { get; set; }
        public virtual DbSet<PersonGSTRegistrationDetailMakerChecker> PersonGSTRegistrationDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonGSTReturnDocument> PersonGSTReturnDocuments { get; set; }
        public virtual DbSet<PersonGSTReturnDocumentMakerChecker> PersonGSTReturnDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonHomeBranch> PersonHomeBranches { get; set; }
        public virtual DbSet<PersonHomeBranchMakerChecker> PersonHomeBranchMakerCheckers { get; set; }
        public virtual DbSet<PersonImmovableAsset> PersonImmovableAssets { get; set; }
        public virtual DbSet<PersonImmovableAssetDocument> PersonImmovableAssetDocuments { get; set; }
        public virtual DbSet<PersonImmovableAssetDocumentMakerChecker> PersonImmovableAssetDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonImmovableAssetMakerChecker> PersonImmovableAssetMakerCheckers { get; set; }
        public virtual DbSet<PersonIncomeTaxDetail> PersonIncomeTaxDetails { get; set; }
        public virtual DbSet<PersonIncomeTaxDetailMakerChecker> PersonIncomeTaxDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonIncomeTaxDetailDocument> PersonIncomeTaxDetailDocuments { get; set; }
        public virtual DbSet<PersonIncomeTaxDetailDocumentMakerChecker> PersonIncomeTaxDetailDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonInsuranceDetail> PersonInsuranceDetails { get; set; }
        public virtual DbSet<PersonInsuranceDetailMakerChecker> PersonInsuranceDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonKYCDetail> PersonKYCDetails { get; set; }
        public virtual DbSet<PersonKYCDetailMakerChecker> PersonKYCDetailMakerCheckers { get; set; }
        public virtual DbSet<PersonKYCDetailDocument> PersonKYCDetailDocuments { get; set; }
        public virtual DbSet<PersonKYCDetailDocumentMakerChecker> PersonKYCDetailDocumentMakerCheckers { get; set; }

        public virtual DbSet<PersonMachineryAsset> PersonMachineryAssets { get; set; }
        public virtual DbSet<PersonMachineryAssetMakerChecker> PersonMachineryAssetMakerCheckers { get; set; }
        public virtual DbSet<PersonMachineryAssetDocument> PersonMachineryAssetDocuments { get; set; }
        public virtual DbSet<PersonMachineryAssetDocumentMakerChecker> PersonMachineryAssetDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonMakerChecker> PersonMakerCheckers { get; set; }
        public virtual DbSet<PersonModification> PersonModifications { get; set; }
        public virtual DbSet<PersonModificationMakerChecker> PersonModificationMakerCheckers { get; set; }
        public virtual DbSet<PersonMovableAsset> PersonMovableAssets { get; set; }
        public virtual DbSet<PersonMovableAssetMakerChecker> PersonMovableAssetMakerCheckers { get; set; }
        public virtual DbSet<PersonMovableAssetDocument> PersonMovableAssetDocuments { get; set; }
        public virtual DbSet<PersonMovableAssetDocumentMakerChecker> PersonMovableAssetDocumentMakerCheckers { get; set; }
        public virtual DbSet<PersonPhoto> PersonPhotoes { get; set; }
        public virtual DbSet<PersonPhotoMakerChecker> PersonPhotoMakerCheckers { get; set; }
        public virtual DbSet<PersonPhotoSign> PersonPhotoSigns { get; set; }
        public virtual DbSet<PersonPhotoSignMakerChecker> PersonPhotoSignMakerCheckers { get; set; }
        public virtual DbSet<PersonPrefix> PersonPrefixes { get; set; }
        public virtual DbSet<PersonPrefixMakerChecker> PersonPrefixMakerCheckers { get; set; }
        public virtual DbSet<PersonRelative> PersonRelatives { get; set; }
        public virtual DbSet<PersonRelativeMakerChecker> PersonRelativeMakerCheckers { get; set; }
        public virtual DbSet<PersonSMSAlert> PersonSMSAlertes { get; set; }
        public virtual DbSet<PersonSMSAlertMakerChecker> PersonSMSAlertMakerCheckers { get; set; }
        public virtual DbSet<PersonSocialMedia> PersonSocialMedias { get; set; }
        public virtual DbSet<PersonSocialMediaMakerChecker> PersonSocialMediaMakerCheckers { get; set; }
        public virtual DbSet<PersonTranslation> PersonTranslations { get; set; }
        public virtual DbSet<PersonTranslationMakerChecker> PersonTranslationMakerCheckers { get; set; }





        // @@@@@@@@@@@@@@@@@@@@@@@@@ S E C U R I T Y @@@@@@@@@@@@@@@@@@@@@@@@@

        //*************** Algorithm ************************
        public virtual DbSet<ChecksumAlgorithm> ChecksumAlgorithms { get; set; }

        //*************** Log ************************
        public virtual DbSet<LoginLog> LoginLogs { get; set; }
        public virtual DbSet<InvalidLoginLog> InvalidLoginLogs { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<AccountRecoveryLog> AccountRecoveryLogs { get; set; }

        //*************** Login ************************
        public virtual DbSet<EmergencyScreen> EmergencyScreens { get; set; }

        //*************** PasswordPolicies ************************
        public virtual DbSet<PasswordPolicy> PasswordPolicies { get; set; }
        public virtual DbSet<PasswordPolicyMakerChecker> PasswordPolicyMakerCheckers { get; set; }
        public virtual DbSet<PasswordPolicyModification> PasswordPolicyModifications { get; set; }
        public virtual DbSet<PasswordPolicyModificationMakerChecker> PasswordPolicyModificationMakerCheckers { get; set; }

        //*************** LoginDevice ************************
        public virtual DbSet<LoginDevice> LoginDevices { get; set; }

        //*************** Permission ************************
        public virtual DbSet<SpecialPermission> SpecialPermissions { get; set; }

        //*************** RoleProfile ************************

        public virtual DbSet<RoleProfile> RoleProfiles { get; set; }
        public virtual DbSet<RoleProfileBusinessOffice> RoleProfileBusinessOffices { get; set; }
        public virtual DbSet<RoleProfileBusinessOfficeMakerChecker> RoleProfileBusinessOfficeMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileGeneralLedger> RoleProfileGeneralLedgers { get; set; }
        public virtual DbSet<RoleProfileGeneralLedgerMakerChecker> RoleProfileGeneralLedgerMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileMakerChecker> RoleProfileMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileMenu> RoleProfileMenus { get; set; }
        public virtual DbSet<RoleProfileMenuMakerChecker> RoleProfileMenuMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileModification> RoleProfileModifications { get; set; }
        public virtual DbSet<RoleProfileModificationMakerChecker> RoleProfileModificationMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileSpecialPermission> RoleProfileSpecialPermissions { get; set; }
        public virtual DbSet<RoleProfileSpecialPermissionMakerChecker> RoleProfileSpecialPermissionMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileTransactionLimit> RoleProfileTransactionLimits { get; set; }
        public virtual DbSet<RoleProfileTransactionLimitMakerChecker> RoleProfileTransactionLimitMakerCheckers { get; set; }
        public virtual DbSet<RoleProfileTranslation> RoleProfileTranslations { get; set; }
        public virtual DbSet<RoleProfileTranslationMakerChecker> RoleProfileTranslationMakerCheckers { get; set; }

        //*************** Users **********************
        public virtual DbSet<UserAuthenticationToken> UserAuthenticationTokens { get; set; }
        public virtual DbSet<UserProfileAccessibility> UserProfileAccessibilities { get; set; }
        public virtual DbSet<UserProfileHomeBranch> UserProfileHomeBranches { get; set; }
        public virtual DbSet<UserProfilePassword> UserProfilePasswords { get; set; }
        public virtual DbSet<UserProfilePhoto> UserProfilePhotoes { get; set; }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserProfileMakerChecker> UserProfileMakerCheckers { get; set; }
        public virtual DbSet<UserProfileModification> UserProfileModifications { get; set; }
        public virtual DbSet<UserProfileModificationMakerChecker> UserProfileModificationMakerCheckers { get; set; }
        public virtual DbSet<UserProfileBusinessOffice> UserProfileBusinessOffices { get; set; }
        public virtual DbSet<UserProfileBusinessOfficeMakerChecker> UserProfileBusinessOfficeMakerCheckers { get; set; }
        public virtual DbSet<UserProfileCurrency> UserProfileCurrencys { get; set; }
        public virtual DbSet<UserProfileCurrencyMakerChecker> UserProfileCurrencyMakerCheckers { get; set; }
        public virtual DbSet<UserProfileGeneralLedger> UserProfileGeneralLedgers { get; set; }
        public virtual DbSet<UserProfileGeneralLedgerMakerChecker> UserProfileGeneralLedgerMakerCheckers { get; set; }
        public virtual DbSet<UserProfileGroup> UserProfileGroups { get; set; }
        public virtual DbSet<UserProfileGroupMakerChecker> UserProfileGroupMakerCheckers { get; set; }
        public virtual DbSet<UserProfileHomeBusinessOffice> UserProfileHomeBusinessOffices { get; set; }
        public virtual DbSet<UserProfileHomeBusinessOfficeMakerChecker> UserProfileHomeBusinessOfficeMakerCheckers { get; set; }
        public virtual DbSet<UserProfileIdentity> UserProfileIdentitys { get; set; }
        public virtual DbSet<UserProfileLoginDevice> UserProfileLoginDevices { get; set; }
        public virtual DbSet<UserProfileLoginDeviceMakerChecker> UserProfileLoginDeviceMakerCheckers { get; set; }
        public virtual DbSet<UserProfileMenu> UserProfileMenus { get; set; }
        public virtual DbSet<UserProfileMenuMakerChecker> UserProfileMenuMakerCheckers { get; set; }
        public virtual DbSet<UserProfilePasswordPolicy> UserProfilePasswordPolicy { get; set; }
        public virtual DbSet<UserProfilePasswordPolicyMakerChecker> UserProfilePasswordPolicyMakerCheckers { get; set; }
        public virtual DbSet<UserProfileSpecialPermission> UserProfileSpecialPermissions { get; set; }
        public virtual DbSet<UserProfileSpecialPermissionMakerChecker> UserProfileSpecialPermissionMakerCheckers { get; set; }
        public virtual DbSet<UserProfileTransactionLimit> UserProfileTransactionLimits { get; set; }
        public virtual DbSet<UserProfileTransactionLimitMakerChecker> UserProfileTransactionLimitMakerCheckers { get; set; }
        public virtual DbSet<UserRoleProfile> UserRoleProfiles { get; set; }
        public virtual DbSet<UserRoleProfileMakerChecker> UserRoleProfileMakerCheckers { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        //*************************************** SMS **************************************************
        public virtual DbSet<SmsProvider> SmsProviders { get; set; }
        public virtual DbSet<SmsProviderAccountDetail> SmsProviderAccountDetails { get; set; }
        public virtual DbSet<SmsUserAuthenticationToken> SmsUserAuthenticationTokens { get; set; }
        public virtual DbSet<TeleVerificationToken> TeleVerificationTokens { get; set; }
        public virtual DbSet<MessageLog> MessageLogs { get; set; }


        //@@@@@@@@@@@@@@@@@ S Y S T E M       E NT I T Y @@@@@@@@@@@@@@@@@@
        // ########################### ACCOUNT ###########################
        public virtual DbSet<AccountClass> AccountClasses { get; set; }
        public virtual DbSet<AccountClassTranslation> AccountClassTranslations { get; set; }
        public virtual DbSet<Fund> Funds { get; set; }
        public virtual DbSet<FundTranslation> FundTranslations { get; set; }

        public virtual DbSet<BalanceType> BalanceTypes { get; set; }
        public virtual DbSet<BalanceTypeTranslation> BalanceTypeTranslations { get; set; }

        // ########################### Address #######################################
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<AddressTypeTranslation> AddressTypeTranslations { get; set; }
        public virtual DbSet<AreaType> AreaTypes { get; set; }
        public virtual DbSet<AreaTypeTranslation> AreaTypeTranslations { get; set; }
        public virtual DbSet<CenterCategory> CenterCategories { get; set; }
        public virtual DbSet<CenterCategoryTranslation> CenterCategoryTranslations { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<DirectionTranslation> DirectionTranslations { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<EducationLevelTranslation> EducationLevelTranslations { get; set; }
        public virtual DbSet<FamilySystem> FamilySystems { get; set; }
        public virtual DbSet<FamilySystemTranslation> FamilySystemTranslations { get; set; }
        public virtual DbSet<LocalGovernment> LocalGovernments { get; set; }
        public virtual DbSet<LocalGovernmentTranslation> LocalGovernmentTranslations { get; set; }
        public virtual DbSet<WorldWideTimeZone> WorldWideTimeZones { get; set; }


        // ########################### APP #######################################
        public virtual DbSet<AppLanguage> AppLanguages { get; set; }

        // ########################### Calculation Method #######################################
        public virtual DbSet<DividendCalculationMethod> DividendCalculationMethods { get; set; }
        public virtual DbSet<DividendCalculationMethodTranslation> DividendCalculationMethodTranslations { get; set; }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@ Contact @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public virtual DbSet<ContactGroup> ContactGroups { get; set; }
        public virtual DbSet<ContactGroupTranslation> ContactGroupTranslations { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<ContactTypeTranslation> ContactTypeTranslations { get; set; }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@ Colour @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        public virtual DbSet<Colour> Colours { get; set; }
        public virtual DbSet<ColourTranslation> ColourTranslations { get; set; }

        // ########################### Human Resource #######################################
        public virtual DbSet<EmployerNature> EmployerNatures { get; set; }
        public virtual DbSet<EmployerNatureTranslation> EmployerNatureTranslations { get; set; }
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }
        public virtual DbSet<EmploymentTypeTranslation> EmploymentTypeTranslations { get; set; }

        // ######################### LEGAL #######################
        public virtual DbSet<CourtCaseStage> CourtCaseStages { get; set; }
        public virtual DbSet<CourtCaseStageTranslation> CourtCaseStageTranslations { get; set; }
        public virtual DbSet<CourtCaseType> CourtCaseTypes { get; set; }
        public virtual DbSet<CourtCaseTypeTranslation> CourtCaseTypeTranslations { get; set; }
        public virtual DbSet<CourtType> CourtTypes { get; set; }
        public virtual DbSet<CourtTypeTranslation> CourtTypeTranslations { get; set; }


        // ########################### Tax#######################################

        public virtual DbSet<GSTRegistrationType> GSTRegistrationTypes { get; set; }
        public virtual DbSet<GSTRegistrationTypeTranslation> GSTRegistrationTypeTranslations { get; set; }
        public virtual DbSet<GSTReturnPeriodicity> GSTReturnPeriodicities { get; set; }
        public virtual DbSet<GSTReturnPeriodicityTranslation> GSTReturnPeriodicityTranslations { get; set; }

        // ############################# General #############################
        public virtual DbSet<AppDataType> AppDataTypes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        //public virtual DbSet<AppSupportedLanguage> AppSupportedLanguages { get; set; }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankTranslation> BankTranslations { get; set; }

        public virtual DbSet<BankBranch> BankBranches { get; set; }
        public virtual DbSet<BankBranchTranslation> BankBranchTranslations { get; set; }

        public virtual DbSet<SocialMedia> SocialMedias { get; set; }
        public virtual DbSet<SocialMediaTranslation> SocialMediaTranslations { get; set; }


        // ############################# Application Page #############################
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageAddOnSubscription> PageAddOnSubscriptions { get; set; }
        public virtual DbSet<PageCosting> PageCostings { get; set; }
        public virtual DbSet<PageTable> PageTables { get; set; }
        public virtual DbSet<PageTableField> PageTableFields { get; set; }
        public virtual DbSet<PageTableFieldHistory> PageTableFieldHistories { get; set; }
        public virtual DbSet<PageTableFieldTranslation> PageTableFieldTranslations { get; set; }
        public virtual DbSet<PageTranslation> PageTranslations { get; set; }

        //*************** Authentication **************
        public virtual DbSet<AuthenticationFactor> AuthenticationFactors { get; set; }
        public virtual DbSet<AuthenticationFactorTranslation> AuthenticationFactorTranslations { get; set; }
        public virtual DbSet<AuthenticationMethod> AuthenticationMethods { get; set; }
        public virtual DbSet<AuthenticationMethodTranslation> AuthenticationMethodTranslations { get; set; }

        //*************** Navigation *******************
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuLabel> MenuLabels { get; set; }

        public virtual DbSet<ReportType> ReportTypes { get; set; }
        public virtual DbSet<ReportTypeFormat> ReportTypeFormats { get; set; }
        public virtual DbSet<ReportTypeTranslation> ReportTypeTranslations { get; set; }

        //*************** Region *******************
        public virtual DbSet<RepaymentIntervalFrequency> RepaymentIntervalFrequencies { get; set; }
        public virtual DbSet<RepaymentIntervalFrequencyTranslation> RepaymentIntervalFrequencyTranslations { get; set; }
        public virtual DbSet<TimePeriodUnit> TimePeriodUnits { get; set; }
        public virtual DbSet<TimePeriodUnitTranslation> TimePeriodUnitTranslations { get; set; }

        //*************************************** CUSTOM ENTITY *****************************************
        public virtual DbSet<CustomInvalidLoginLog> CustomInvalidLoginLogs { get; set; }

        //*************************************** Search Menus *****************************************
        public virtual DbSet<MenuSearchQueryResult> MenuSearchQueryResults { get; set; }
        public virtual DbSet<SearchQuery> SearchQueries { get; set; }
        public virtual DbSet<SearchQueryResult> SearchQueryResults { get; set; }

        //************************************** System Entity *************************************

        public virtual DbSet<Institute> Institutes { get; set; }
        public virtual DbSet<InstituteTranslation> InstituteTranslations { get; set; }
        public virtual DbSet<EducationalCourse> EducationalCourses { get; set; }
        public virtual DbSet<EducationalCourseTranslation> EducationalCourseTranslations { get; set; }

    }
}
