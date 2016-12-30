using System.ComponentModel.DataAnnotations;

namespace Bloggy.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
