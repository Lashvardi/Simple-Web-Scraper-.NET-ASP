using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScraperAPI.Data;
using ScraperAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScraperAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapedQuotesController : ControllerBase
    {
        private readonly QuoteDbContext _context;

        public ScrapedQuotesController(QuoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScrapedQuote>>> GetScrapedQuotes()
        {
            return await _context.ScrapedQuotes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScrapedQuote>> GetScrapedQuote(int id)
        {
            var scrapedQuote = await _context.ScrapedQuotes.FindAsync(id);

            if (scrapedQuote == null)
            {
                return NotFound();
            }

            return scrapedQuote;
        }
    }
}
