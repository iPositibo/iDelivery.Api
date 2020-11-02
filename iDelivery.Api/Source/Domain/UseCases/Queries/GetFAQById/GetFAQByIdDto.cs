namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFAQByIdDto
    {
        public int Faqid { get; set; }
        public string Faqcontent { get; set; }
        public string Answer { get; set; }
    }
}
