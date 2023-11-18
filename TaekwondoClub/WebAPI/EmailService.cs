using System.Net.Mail;
using System.Net;

namespace WebAPI;

public class EmailService
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public void SendEmail(string to, string subject, string body)
    {
        var fromAddress = new MailAddress(UserName, Name);
        var toAddress = new MailAddress(to);

        var smtp = new SmtpClient
        {
            Host = SmtpServer,
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, Password)
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
    }
}
