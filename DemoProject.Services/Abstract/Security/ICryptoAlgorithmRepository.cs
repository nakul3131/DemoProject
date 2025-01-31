namespace DemoProject.Services.Abstract.Security
{
    //
    // Summarry
    // This Interface decouple the controller from the authentication methods.
    // Manages Encryption services for Web applications.
    public interface ICryptoAlgorithmRepository
    {
        //
        // Summary:
        //     Encrypt Data By Key

        // Parameters:
        //   input: Orginal String For Encryption.
        //   key : User Defined Key To Encrypt Data
        // Returns:
        //     Encrypt String
        string Encrypt(string _input, string _key);

        //
        // Summary:
        //     Decrypt Data By Key While Used At Encryption

        // Parameters:
        //   input: Encrypted String For Decryption.
        //   key : User Defined Key To Encrypt Data
        // Returns:
        //     Decrypt Encrypted String
        string Decrypt(string _input, string _key);

        //
        // Summary:
        //     Decrypt Data By Key While Used At Encryption

        // Parameters:

            // Returns:
        //     Decrypt Encrypted String

        string EncryptFileNameByAsymmetricKey();
    }
}
