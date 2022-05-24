using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Models
{
    public enum Status
    {
        Active,
        Complete
    }

    public class Order
    {
        public string Id { get; set; }

        public List<(string productId, int quantity)> Products { get; set; }

        public Status Status { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string? TableId { get; set; }

        [ForeignKey("TableId")]
        public Table Table { get; set; }

        public double TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
