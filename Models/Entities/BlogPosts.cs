using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Models.Entities
{
    public class BlogPosts
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title is required")]
        [StringLength(100, ErrorMessage = "The Title cannot be Longer than 100 characters.")]
        [Display(Name = "Blog Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name ="Is Active")]
        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}