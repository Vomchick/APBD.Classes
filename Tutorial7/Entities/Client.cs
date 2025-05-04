namespace Tutorial7.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Piesel { get; set; } = string.Empty;
        public List<Trip> Trips { get; set; } = [];
    }
}
