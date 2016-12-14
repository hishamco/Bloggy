using System;
using System.ComponentModel.DataAnnotations;

namespace Bloggy.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Required(ErrorMessage = "The Comment field is required.")]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        public DateTime PublishedAt { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
