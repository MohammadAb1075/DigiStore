namespace DigiStore.Models
{
    public class PaginationModel<T>
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public List<T> Data { get; set; }
    }
}