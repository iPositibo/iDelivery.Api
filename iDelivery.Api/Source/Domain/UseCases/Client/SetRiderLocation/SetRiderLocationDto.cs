﻿namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetRiderLocationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int RiderStatusId { get; set; }
        public int RatingId { get; set; }
        public int? UserId { get; set; }
    }
}
