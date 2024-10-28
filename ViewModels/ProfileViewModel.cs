using System.ComponentModel.DataAnnotations;

namespace Resunet.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string? ProfileName  { get; set; }

        [Required]
        public string? FileName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
} 