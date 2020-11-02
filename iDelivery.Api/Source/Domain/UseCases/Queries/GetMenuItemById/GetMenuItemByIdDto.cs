namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuItemByIdDto
    {
        public int MenuItemId { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
    }
}
