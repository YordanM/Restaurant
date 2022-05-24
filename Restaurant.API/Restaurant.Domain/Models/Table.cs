using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Models
{
    public class Table
    {
        public string Id { get; set; }

        public int TableNumber { get; set; }

        [Range(2, 12)]
        public int Capacity { get; set; }
    }
}
