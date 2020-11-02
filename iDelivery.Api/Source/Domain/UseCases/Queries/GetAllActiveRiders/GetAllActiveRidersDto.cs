﻿namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllActiveRidersDto
    {
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? RiderStatusId { get; set; }
        public string Status { get; set; }
        public int? RatingId { get; set; }
        public string Rating { get; set; }
        public int? UserId { get; set; }
        public bool IsOnline { get; set; }
    }
}