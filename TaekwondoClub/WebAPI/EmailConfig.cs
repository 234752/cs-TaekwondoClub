namespace WebAPI;

public class EmailConfig
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}

public class Pog
{
    public string email { get; set; }
}