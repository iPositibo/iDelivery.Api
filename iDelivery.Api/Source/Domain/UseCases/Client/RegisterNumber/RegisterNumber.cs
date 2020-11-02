using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RegisterNumber : IRequest<int>
    {
        public RegisterNumberDto Dto { get; }

        public RegisterNumber(RegisterNumberDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<RegisterNumber, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(RegisterNumber request, CancellationToken cancellationToken)
            {
                request.Dto.DateRegistered = DateTime.UtcNow;
                request.Dto.Otpcode = GenerateOTPCode();
                var entity = mapper.Map<Otpregistration>(request.Dto);

                await context.SaveChangesAsync();

                // todo: send sms

                return entity.OtpregistrationId;
            }

            private string GenerateOTPCode()
            {
                var alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                var numbers = "1234567890";
                
                var characters = numbers;
                characters += alphabets + small_alphabets + numbers;
                var otp = string.Empty;
                for (int i = 0; i < 4; i++) // OTP code length is 4
                {
                    var character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, characters.Length);
                        character = characters.ToCharArray()[index].ToString();
                    } while (otp.IndexOf(character) != -1);
                    otp += character;
                }

                return otp;
            }
        }
    }
}
