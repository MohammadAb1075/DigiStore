namespace DigiStore.Utilities
{
    public class PaginationResult<T>
    {
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public int? Limit { get; set; }
        public int Total { get; set; }
        public IQueryable<T> Data { get; set; }
    }
    public class PaginationResultService<T>
    {
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public int? Limit { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}

