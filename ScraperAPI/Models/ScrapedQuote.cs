using System;

namespace ScraperAPI.Models
{
    public class ScrapedQuote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
    }

}
