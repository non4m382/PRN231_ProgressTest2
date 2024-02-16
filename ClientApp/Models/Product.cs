namespace ClientApp.Models
{
    internal class Product
    {
        public Product()
        {
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string? Image { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }
    }
}