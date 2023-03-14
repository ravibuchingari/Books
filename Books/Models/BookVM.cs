using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class BookVM
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Name cannot be empty.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than {1} characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Author name cannot be empty.")]
        [MaxLength(100, ErrorMessage = "Author name cannot be longer than {1} characters")]

        public string authorName { get; set; }
    }
}
