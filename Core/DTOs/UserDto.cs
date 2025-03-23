namespace Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string NationalId { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid? LevelId { get; set; }
        public string LevelName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
