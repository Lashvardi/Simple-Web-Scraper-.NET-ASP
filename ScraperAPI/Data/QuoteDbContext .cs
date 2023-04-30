using Microsoft.EntityFrameworkCore;
using ScraperAPI.Models;

namespace ScraperAPI.Data
{
    public class QuoteDbContext : DbContext
    {
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options)
        {
        }

        public DbSet<ScrapedQuote> ScrapedQuotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any custom model configuration here
        }

    }
}
