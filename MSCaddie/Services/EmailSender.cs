namespace MSCaddie.Services;


// ## appsettings.json
// {
//   "blablabla": "*",
//   "Smtp": {
//     "Host": "smtp.host.com",
//     "Port": "587",
//     "EnableSsl": true,
//     "Username": "user@name.com",
//     "Password": "password1"
//   }
// }

// ## Startup.cs
// // ...
// using Microsoft.AspNetCore.Identity.UI.Services;
// public class Startup
// {
//     // ...
//     public void ConfigureServices(IServiceCollection services)
//     {
//         services.AddControllersWithViews();
//         services.AddRazorPages();
//         // ...
//         services.AddTransient<IEmailSender, EmailSender>();
//         services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
//         // ...
//     }
//     // ...
// }

// ## SmtpOptions.cs
// Options class
// public class SmtpOptions
// {
//     public string Host { get; set; }
//     public int Port { get; set; }
//     public bool EnableSsl { get; set; }
//     public string Username { get; set; }
//     public string Password { get; set; }
// }

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

public class EmailSender : IEmailSender
{
    private readonly SmtpOptions _smtpOption;

    public EmailSender(IOptions<SmtpOptions> smtpOptionAccessor)
    {
        _smtpOption = smtpOptionAccessor.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email));
        }
        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentNullException(nameof(subject));
        }
        if (string.IsNullOrEmpty(htmlMessage))
        {
            throw new ArgumentNullException(nameof(htmlMessage));
        }

        await Execute(email, subject, htmlMessage);
    }

    private async Task Execute(string email, string subject, string htmlMessage)
    {
        var message = new MailMessage();
        message.To.Add(email);
        message.Subject = subject;
        message.Body = htmlMessage;
        message.IsBodyHtml = true;
        message.From = new MailAddress(_smtpOption.Username);

        using var smtp = new SmtpClient(_smtpOption.Host, _smtpOption.Port);
        smtp.EnableSsl = _smtpOption.EnableSsl;
        smtp.Credentials = new NetworkCredential(_smtpOption.Username, _smtpOption.Password);

        await smtp.SendMailAsync(message);
    }
}
