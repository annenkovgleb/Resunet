using System;
using System.ComponentModel.DataAnnotations;

namespace ResunetBl.ViewModels
{
    public class LoginViewModel
    {
        [Required] // должно быть инициализировано инициализатором объекта
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool? RememberMe { get; set; }
    }
}

