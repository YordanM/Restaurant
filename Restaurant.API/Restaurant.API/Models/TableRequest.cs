using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Models
{
    public class TableRequest
    {
        public string Id { get; set; }

        public int TableNumber { get; set; }

        [Range(2, 12,
            ErrorMessage = "Value must be between 2 and 12")]
        public int Capacity { get; set; }
    }
}
