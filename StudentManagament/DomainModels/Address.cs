namespace StudentManagament.DomainModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? PhysicalAdress { get; set; }
        public string? PostalAddress { get; set; }
        public Guid StudentId { get; set; }
    }
}
