using System.Net.Mail;
using System.Net;

namespace WebAPI;

public class EmailService
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public void SendEmail(EmailDTO dto)
    {
        var fromAddress = new MailAddress(UserName);
        var toAddress = new MailAddress(dto.Recipent);

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
            Subject = dto.Subject,
            Body = dto.Body
        })
        {
            smtp.Send(message);
        }
    }
}
