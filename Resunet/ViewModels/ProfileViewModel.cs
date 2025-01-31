using System.ComponentModel.DataAnnotations;
using ResunetDal.Models;

namespace ResunetBl.ViewModels
{
    public class ProfileViewModel
    {
        public int? ProfileId { get; set; }
        [Required] public string? ProfileName { get; set; }
        [Required] public string? FirstName { get; set; }
        [Required] public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
        public ProfileStatusEnum ProfileStatus { get; set; }
    }
}