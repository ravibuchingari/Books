using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class BookDBContext: DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options): base(options) {
        
        }

        public DbSet<BookDM> Books { get; set; }
        
    }
}
