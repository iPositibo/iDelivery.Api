﻿namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateMenuItemDto
    {
        public int MenuItemId { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
    }
}