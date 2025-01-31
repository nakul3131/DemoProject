namespace DemoProject.Services.Abstract.SMS
{
    public interface ISMSRepository
    {
        string CreateTeleVerificationSMSToken(string _mobileNumber);

        int GetRecentTeleVerificationTokenPrmKey(string _mobileNumber);

        bool IsValidMobileVerificationToken(string _teleNumber, string _token);

    }
}
