using System.ComponentModel.DataAnnotations;
using ResunetDAL.Models;

namespace Resunet.ViewModels
{
    public class PostContentItemViewModel
    {
        public enum ContentItemTypeEnum { Text, Image, Title }

        public int? ContentId { get; set; }

        public int ContentItemType { get; set; }

        public string? Value { get; set; }
    }

    public class PostViewModel
    {
        public int? PostId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Intro is required")]
        public string? Intro { get; set; }

        public List<PostContentItemViewModel> ContentItems { get; set; } = new List<PostContentItemViewModel>();

        public StatusEnum Status { get; set; }
    }
}
