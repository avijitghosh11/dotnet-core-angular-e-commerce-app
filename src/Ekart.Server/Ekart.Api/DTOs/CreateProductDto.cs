﻿using System.ComponentModel.DataAnnotations;

namespace Ekart.Api.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be getter than zero")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Quantity in stock must be at least one")]
        public int QuantityInStock { get; set; }
    }
}
