using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Infrastructure.Helpers.EmailNotification
{
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("admin@api-idelivery.com", "@Hotsauce_11");

                string path = $"{Directory.GetCurrentDirectory()}{@"\images"}";

                LinkedResource LinkedImage = new LinkedResource(path + "/logo.jpg");
                LinkedImage.ContentId = "MyPic";
                LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                  message.Content + " <img src=cid:MyPic>",
                  null, "text/html");

                htmlView.LinkedResources.Add(LinkedImage);

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(message.From),
                    Subject = "iDelivery Email Test",
                    Body = message.Content
                };

                mail.AlternateViews.Add(htmlView);

                mail.IsBodyHtml = true;
                //mail.Attachments.Add(att);
                mail.To.Add(new MailAddress(message.To));

                // Smtp client
                var client = new SmtpClient()
                {
                    //Port = 587,
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    //Host = "smtp.gmail.com",
                    Host = "mail.api-idelivery.com",
                    //EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
