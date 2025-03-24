namespace ProductCatalog.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private Product() { } // Pour EF Core

        public Product(string name, string description, decimal price, int stock)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public void UpdateStock(int newStock)
        {
            if (newStock < 0)
                throw new ArgumentException("Stock cannot be negative");
            Stock = newStock;
        }

        public void Update(string name, string description, decimal price, int stock)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
} 