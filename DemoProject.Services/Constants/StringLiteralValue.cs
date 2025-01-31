namespace DemoProject.Services.Constants
{
    public static class StringLiteralValue
    {
        //  const and readonly perform a similar function on data members, but they have a few important differences.
        //  A constant member is defined at compile time and cannot be changed at runtime.
        //  Constants are declared as a field, using the const keyword and must be initialized as they are declared.

        //  The static modifier is used to declare a static member, this means that the member is no longer tied to a specific object. 
        //  The value belongs to the class, additionally the member can be accessed without creating an instance of the class. 
        //  Only one copy of static fields and events exists, and static methods and properties can only access static fields and static events

        // Character 1 Size Constants

        public const string AgentSettlementAccount = "S";

        public const string ByHand = "H";

        public const string Day = "D";

        public const string Disable = "D";

        public const string Direct = "D";

        public const string FlatAmount = "F";

        public const string HalfYear = "H";

        public const string Mandatory = "M";

        public const string Month = "M";

        public const string Number = "N";

        public const string NotRequired = "N";

        public const string Optional = "O";

        public const string Percentage = "P";

        public const string Quarter = "Q";

        public const string Week = "W";

        public const string Year = "Y";


        // Character 3 Size Constants
        public const string Absent = "ABS";

        public const string AccountClosingCharges = "ACC";

        public const string AccountTransferCharges = "ATC";

        public const string AClassMember = "AMB";

        public const string Active = "ACT";

        public const string ActiveMember = "ACT";

        public const string AgentCommission = "ACO";

        public const string All = "ALL";

        public const string Amend = "AMN";

        public const string AnyMember = "ANY";

        public const string Approved = "APR";

        public const string AtMaturity = "MAT";

        public const string Authentication = "LGN";

        public const string ANY = "ANY";

        public const string Blocked = "BLK";

        public const string Both = "BTH";

        public const string BusinessOffice = "BSO";

        public const string ClearSession = "CLS";

        public const string ClearRecentSession = "ClearRecentSession";

        public const string Closed = "CLS";

        public const string CommandAmend = "Amend";

        public const string CommandDelete = "Delete";

        public const string CommandModify = "Modify";

        public const string CommandReject = "Reject";

        public const string CommandVerify = "Verify";

        public const string Create = "CRT";

        public const string CreditAccount = "CRP";

        public const string CreditInterestAccount = "CRI";

        public const string CurrentAccount = "CRN";

        public const string Customer = "CST";

        public const string DebitAccount = "DRD";

        public const string Delete = "DEL";

        public const string DemandDeposit = "DMN";

        public const string Depositor = "DEP";

        public const string DoNotRenew = "DNR";

        public const string DepositInterestProvision = "DIP";

        public const string Email = "EML";

        public const string Exit = "Exit";

        public const string Expired = "EXP";

        public const string Flexible = "FLX";

        public const string FirstDay = "FST";

        public const string FileName = "sblw-3hn8-sqoy19";

        public const string FineInterestReceivedOnDeposit = "FID";

        public const string FineInterestReceivedOnLoan = "FIL";

        public const string FixedDeposit = "FDP";

        public const string Inactive = "INA";

        public const string IncompleteLogin = "INC";

        public const string Initiated = "INA";

        public const string InterestPaidOnDeposit = "IPD";

        public const string InterestRebate = "IRB";

        public const string InterestReceivedOnLoan = "IRL";

        public const string LastDay = "LST";

        public const string LoanInterestProvision = "LIR";

        public const string Loan = "LON";

        public const string LoanCharges = "LNC";
    
        public const string Locked = "LOK";

        public const string LoggedIn = "LGN";

        public const string MemberAdmissionFee = "MAF";

        public const string Mobile = "MBL";

        public const string Modify = "MDF";

        public const string New = "NEW";

        public const string NominalMember = "NOM";

        public const string NotUsed = "NUS";

        public const string NoMembershipRequired = "NMR";

        public const string Old = "OLD";

        public const string OrdinaryMember = "ORD";

        public const string Pending = "PND";

        public const string PublicProvidentFund = "PPF";

        public const string ReadyForLogin = "RDY";

        public const string RecurringDeposit = "REC";

        public const string RoundUp = "RUP";

        public const string RoundDown = "RDN";

        public const string RoundNear = "NON";

        public const string Reject = "REJ";

        public const string ResetPassword = "ResetPassword";

        public const string Saving = "SAV";

        public const string SharesCapital = "SCP";

        public const string SharesTransferCharges = "STC";

        public const string TermDeposit = "TRM";

        public const string Truncate = "TRN";

        public const string UnlockUserAccount = "UNU";

        public const string Unverified = "UVF";

        public const string UserLocked = "UserLocked";

        public const string Verify = "VRF";


        public const string Save = "Save";

        //Loan Type

        public const string ConsumerSupplier = "CDRIT";

        public const string CashCreditLoan = "CCL";
        
        public const string EducationalLoan = "EDU";

        public const string ConsumerDurableLoan = "CDL";

        public const string GoldLoan = "GDL";

        public const string GuarantorLoan = "GRL";

        public const string HomeLoan = "HML";

        public const string LoanAgainstDeposit = "LAD";

        public const string LoanAgainstProperty = "LAP";

        public const string PersonalLoan = "PRL";

        public const string ShortTermBusinessLoan = "SBL";

        public const string VehicleLoan = "VHL";

        public const string Unmarried = "UNMARD";

        // Occupation SysName
        public const string Salaried = "SLRD";

        public const string SelfEmployedBusiness = "SEMB";

        public const string SelfEmployedProfessional = "SEMP";

        //Colour Name
        public const string OtherColour = "Other";

        //VehicleType
        public const string NewVehicle = "NEW";
        public const string PreOwnedVehicle = "PRE";
        public const string TakeOverOfVehicle = "TOV";

    }
}
