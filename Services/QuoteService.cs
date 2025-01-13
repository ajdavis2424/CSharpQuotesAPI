//"ENGINE" class... Logic goes here
using C__WEB_API_FALLENQUOTES.Models;
using C__WEB_API_FALLENQUOTES.Data;
using Microsoft.EntityFrameworkCore;

namespace C__WEB_API_FALLENQUOTES.Services;

public class QuoteService
{
    private readonly QuoteContext _context;

    // Constructor to inject database context and seed initial data if needed
    public QuoteService(QuoteContext context)
    {
        _context = context;
        // Check if any data exists, if not, seed the database
        if (!_context.Quotes.Any())
        {
            SeedInitialData();
        }
    }

    // Initial data seeding method
    private void SeedInitialData()
    {
        // populate DB with small data - same as your original list
        var initialQuotes = new List<Quote>()
        {
            new Quote
            {
                Author = "John McKay, USC",
                Quotes = "We didn't tackle well today but we made up for it by not blocking."
            },
            new Quote
            {
                Author = "Bob Devany, Nebraska",
                Quotes = "I don't want to win enough to be placed on NCAA probation, I just want to win enough to warrant an investigation."
            },
            new Quote
            {
                Author = "Erk Russell, Georgia Southern",
                Quotes = "At Georgia Southern, we don't cheat. That costs money and we don't have any."
            },
        };

        _context.Quotes.AddRange(initialQuotes);
        _context.SaveChanges();
    }

    //1. Method to get all the quotes in the QuotesDB
    public async Task<List<Quote>> GetAllQuotes()
    {
        return await _context.Quotes.ToListAsync();
    }

    //2. Method to get single quote by Id
    public async Task<Quote?> Get(int id)
    {
        return await _context.Quotes.FindAsync(id);
    }

    //3. Method to UPDATE(PUT) an existing quote
    public async Task Update(Quote quote)
    {
        _context.Entry(quote).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    //4. Method to ADD new quote
    public async Task Add(Quote quote)
    {
        await _context.Quotes.AddAsync(quote);
        await _context.SaveChangesAsync();
    }

    //5. Method to DELETE a quote by id
    public async Task Delete(int id)
    {
        //get quote by id
        var quote = await Get(id);
        //check if id exists
        if (quote is null) return;
        
        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();
    }
}