using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Infrastructure.Helpers.EmailNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SendEmailVerificationCommand : IRequest<bool>
    {
        public string Email { get; }
        
        public SendEmailVerificationCommand(string email) => this.Email = email;

        private class SendEmailVerificationCommandHandler : IRequestHandler<SendEmailVerificationCommand, bool>
        {
            private readonly IEmailSender emailSender;
            private DataContext context;
            private IMapper mapper;

            public SendEmailVerificationCommandHandler(DataContext context, IMapper mapper, IEmailSender emailSender)
            {
                this.context = context;
                this.mapper = mapper;
                this.emailSender = emailSender; 
            }

            public async Task<bool> Handle(SendEmailVerificationCommand request, CancellationToken cancellationToken)
            {
                var isEmailSent = default(bool);
                var emailVerification = $"https://i-delivery.ph/emailVerification?emailcode={GenerateCode()}";
                //var content = $"Verify Email: { emailVerification }";
                var content = GetEmailContent(emailVerification);
                var emailMessage = new EmailMessage("admin@api-idelivery.com", request.Email, content);
                try
                {
                    isEmailSent = await emailSender.SendEmailAsync(emailMessage);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                return isEmailSent;
            }

            private string GenerateCode()
            {
                var alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                var numbers = "1234567890";

                var characters = numbers;
                characters += alphabets + small_alphabets + numbers;
                var code = string.Empty;
                for (int i = 0; i < 4; i++) // OTP code length is 4
                {
                    var character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, characters.Length);
                        character = characters.ToCharArray()[index].ToString();
                    } while (code.IndexOf(character) != -1);
                    code += character;
                }

                return code;
            }

            private string GetEmailContent(string activationLink)
            {
                var welcomeNote = @"<html>
                                <body>
                                <p>Greetings from i-delivery Team,</p>
                                <br />
                                <p>We have received your request to reset activate your I-delivery Account. To activate simply click the activation link " + activationLink + "</p>";

                var thanksNote = @"<br />
                                   <p>Cheers,</p>
                                   <br/>
                                   i-Delivery Team";

                var footerNote = @"<br />
                                <p>---------------------------------------------------------------------------------------</p>
                                <p>This is a system generated email. Please do not reply.</p>
                                <br/>
                                <p>Connect with us! follow-i-Delivery on FACEBOOK https://www.facebook.com/iDeliveryman. If you have any questions, send an email to customersupport@i-delivery.ph or call us on 0906-062-0413(globe) or 0928-422-1861(smart)</p>
                                <br />
                                <p>Copyright C i-Delivery. All rights Reserved</p>
                                </body>
                                </html>";

                string body = string.Format("{0}{1}{2}", welcomeNote, thanksNote, footerNote);

                return body;
            }
        }
    }
}
