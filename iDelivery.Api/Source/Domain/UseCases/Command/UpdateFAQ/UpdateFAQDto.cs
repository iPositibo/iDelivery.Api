namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFAQDto
    {
        public int Faqid { get; set; }
        public string Faqcontent { get; set; }
        public string Answer { get; set; }
    }
}
