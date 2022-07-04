namespace DigiStore.Entities
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation Property
        public virtual ICollection<Products>? Products { get; set; }
    }
}