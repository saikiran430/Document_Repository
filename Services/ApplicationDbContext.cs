using Document_Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Document_Repository.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 

        }

        public DbSet<Document> Documents { get; set; }
    }
}
