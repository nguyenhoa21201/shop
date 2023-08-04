namespace project.Models
{
    public class OrderSuccess
    {
        public DateTime? OrderDate { get; set; }

        public decimal? Total { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
