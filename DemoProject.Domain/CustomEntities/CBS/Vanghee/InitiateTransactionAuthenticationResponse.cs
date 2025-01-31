namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class InitiateTransactionAuthenticationResponse
    {
        public string Code { get; set; }

        public string APIKey { get; set; }

        public string ErrorOrSuccessMessage { get; set; }

        public InitiateTransactionAuthenticationResponseValue InitiateTransactionAuthenticationResponseValue   { get; set; }

        public string LocalDateTime { get; set; }
    }
}
