﻿namespace MobiliTree.Domain.Models
{
    public class Session
    {
        public string ParkingFacilityId { get; set; }
        public string CustomerId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal Cost { get; set; }
    }
}