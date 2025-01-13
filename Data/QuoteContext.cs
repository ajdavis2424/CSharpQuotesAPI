using Microsoft.EntityFrameworkCore;
using C__WEB_API_FALLENQUOTES.Models;

namespace C__WEB_API_FALLENQUOTES.Data
{
    public class QuoteContext : DbContext
    {
        public QuoteContext(DbContextOptions<QuoteContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
    }
}