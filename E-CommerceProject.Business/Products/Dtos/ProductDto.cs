﻿namespace E_CommerceProject.Business.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string BrandName { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
