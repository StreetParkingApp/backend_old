namespace StreetParking.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = String.Empty;
        private readonly string _mailFrom = String.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail subject: {subject} to mail to {_mailTo}");
        }
    }
}
