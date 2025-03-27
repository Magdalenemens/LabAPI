using Azure;
using Azure.Communication.Email;
using DeltaCare.Configuration;
using DeltaCare.Entity.Model;

namespace DeltaCare.Helper
{
    public class EmailSender
    {
        public static async Task<bool> SendEmailAsync(UserFLModel user)
        {
            try
            {
                // This code retrieves your connection string from an environment variable.
                string connectionString = ConfigHelper.EmailConfig.EmailConnectionStrings;
                var emailClient = new EmailClient(connectionString);

                EmailSendOperation emailSendOperation = emailClient.Send(
                    WaitUntil.Completed,
                    senderAddress: ConfigHelper.EmailConfig.senderAddress,
                    recipientAddress: user.EMAIL,
                    subject: "auto generated password",
                    htmlContent: $"<html><h1>Hi {user.FULL_NAME}</h1><p>Your Password is: {user.PASS_WORD}</p></html>"
                    //plainTextContent: "Hello world via email.asd"
                    );

                return await Task.FromResult(emailSendOperation.HasValue);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
