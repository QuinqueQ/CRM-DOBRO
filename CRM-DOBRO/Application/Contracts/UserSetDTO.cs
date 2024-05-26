namespace Application.Contracts
{
    /// <summary>
    /// DTO for creating and changing a user
    /// </summary>
    public class UserSetDTO
    {
        [DisplayName("ФИО")]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]  
        [MaxLength(150, ErrorMessage = Message.MAX_LENGTH)]
        public string? FullName { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
        [MaxLength(20, ErrorMessage = Message.MAX_LENGTH)]
        [EmailAddress(ErrorMessage = Message.EMAIL)]
        public required string Email { get; init; }

        [DisplayName("Пароль")]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
        [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
        public required string Password { get; init; }

        [DisplayName("Роль пользователя")]
        [Required(ErrorMessage = Message.REQUIRED)]
        [EnumDataType(typeof(UserRole), ErrorMessage = Message.ENUM)]
        public UserRole Role { get; init; }
    }
}
