using Microsoft.EntityFrameworkCore;
using ScraperAPI.Data;
using ScraperAPI.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<QuoteDbContext>(options =>
                options.UseMySql("server=localhost;user=root;password=;database=test1;",
                    ServerVersion.AutoDetect("server=localhost;user=root;password=;database=test1;")));

        builder.Services.AddSingleton<QuoteScraper>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Call the scraper's Scrape method
        var scraper = app.Services.GetRequiredService<QuoteScraper>();
        scraper.Scrape();

        app.Run();
    }
}
