namespace GeekStore.Domain.Entities
{
    public class Address : BaseEntity
    {
        public required string Number { get; set; }
        public required string Street { get; set; }
        public string? Neighborhood { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }
    }
}