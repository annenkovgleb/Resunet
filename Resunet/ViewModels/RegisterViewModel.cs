﻿using System.ComponentModel.DataAnnotations;

namespace ResunetBl.ViewModels
{
	public class RegisterViewModel: IValidatableObject
	{
		[Required(ErrorMessage = "Email обязателен")]
		[EmailAddress(ErrorMessage = "Некорректный формат")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Пароль обязателен")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{8,}$",
                ErrorMessage = "Пароль слишком простой")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwer1234")
            {
                yield return new ValidationResult("Пароль слишком простой", new[] { "Password" });
            }
        }
    }
}

