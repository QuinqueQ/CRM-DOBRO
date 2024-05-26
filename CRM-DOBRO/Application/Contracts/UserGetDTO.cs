namespace Application.Contracts
{
    /// <summary>
    /// DTO for obtaining user fields
    /// </summary>
    public class UserGetDTO
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required UserRole Role { get; set; }
        public DateTime? BlockingDate { get; set; }
    }
}
