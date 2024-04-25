using MailKit.Net.Smtp;
using MimeKit;

namespace UserDB.EmailService
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "xelperproject@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                try
                {
                    await client.AuthenticateAsync("xelperproject@mail.ru", "M3wZ4RgawviCK4042hNa");

                } catch (Exception ex) { }
                try
                {
                    await client.SendAsync(emailMessage);
                } catch (Exception ex) { }
              
                await client.DisconnectAsync(true);
            }
        }
    }
}
