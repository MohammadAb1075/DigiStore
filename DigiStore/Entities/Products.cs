namespace DigiStore.Entities
{
    public partial class Products
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }

        // Navigation Property
        public virtual Categories Category { get; set; }
    }
}