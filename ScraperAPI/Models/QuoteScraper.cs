using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ScraperAPI.Data;
using ScraperAPI.Models;

namespace ScraperAPI.Services
{
    public class QuoteScraper
    {
        private readonly IServiceProvider _serviceProvider;

        public QuoteScraper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Scrape()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<QuoteDbContext>();

                var web = new HtmlWeb();
                var page = 1;
                var hasNextPage = true;

                while (hasNextPage)
                {
                    var doc = web.Load($"https://quotes.toscrape.com/page/{page}/");

                    var quotes = new List<ScrapedQuote>();

                    foreach (var node in doc.DocumentNode.SelectNodes("//div[@class='quote']"))
                    {
                        var textNode = node.SelectSingleNode(".//span[@class='text']");
                        var authorNode = node.SelectSingleNode(".//small[@class='author']");

                        var quote = new ScrapedQuote
                        {
                            Text = textNode?.InnerText.Trim(),
                            Author = authorNode?.InnerText.Trim()
                        };

                        quotes.Add(quote);
                    }

                    var newQuotes = quotes.Where(q => !context.ScrapedQuotes.Any(sq => sq.Text == q.Text && sq.Author == q.Author)).ToList();

                    if (newQuotes.Any())
                    {
                        context.ScrapedQuotes.AddRange(newQuotes);
                        context.SaveChanges();
                    }

                    var nextPageLink = doc.DocumentNode.SelectSingleNode("//li[@class='next']/a");

                    if (nextPageLink == null)
                    {
                        hasNextPage = false;
                    }
                    else
                    {
                        page++;
                    }
                }
            }
        }
    }
}
