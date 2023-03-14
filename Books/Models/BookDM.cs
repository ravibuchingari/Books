using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class BookDM
    {
        [StringLength(100)]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string authorName { get; set; }
    }
}
