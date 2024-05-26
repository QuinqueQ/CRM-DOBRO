namespace Application.Validation;

/// <summary>
/// Error messages for validation
/// </summary>
public class Message
{
    public const string REQUIRED = "Поле {0} не может быть пустым";
    public const string MAX_LENGTH = "Поле {0} не может быть больше {1}";
    public const string EMAIL = "Поле {0} должно содержать email адресс";
    public const string PHONE = "Поле {0} должно содержать номер телефона";
    public const string ENUM = "Поле {0} должно содержать только существующее значение";

}
