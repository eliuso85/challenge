using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tools;

public class Utils
{
    public static async Task SendEmailAsync(string toEmail, string nombre )
    {
        var subject = "Bienvenido a nuestra plataforma";
        var bodyHtml = $"<h2>Gracias por registrarte</h2><h3>{nombre}</h3><p>Disfruta nuestros servicios.</p>";
        var fromEmail = "eliuso85@gmail.com";
        var smtpHost = "smtp.gmail.com";
        var smtpPort = 587;
        var smtpPass = "ptyc zjig pgvv pzfe";

        var smtpClient = new SmtpClient(smtpHost)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(fromEmail, smtpPass),
            EnableSsl = true // Asegúrate de habilitar SSL/TLS
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = bodyHtml,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage); // Enviar el correo de forma asíncrona
    }

}
