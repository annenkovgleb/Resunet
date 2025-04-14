using System.ComponentModel.DataAnnotations;

namespace ResunetDAL.Models;

public class UserModel
{
    [Key]
    public int? UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    // солить пароль, пароль нужно солить не просто в чистом виде, не просто шифровать
    // а они должны быть зашифрованы необратимым шифрованием с использованием соли
    // соль должны создаваться где-то в BL уровне
    public string Salt { get; set; } = null!;
    public int Status { get; set; } = 0;
}

