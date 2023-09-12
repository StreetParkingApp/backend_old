namespace StreetParking.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = String.Empty;
        private readonly string _mailFrom = String.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from the cloud subject: {subject} to mail to {_mailTo}");

        }
    }
}
