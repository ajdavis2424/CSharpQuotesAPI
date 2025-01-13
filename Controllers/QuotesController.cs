//Controller will connect service class to client
using C__WEB_API_FALLENQUOTES.Models;
using C__WEB_API_FALLENQUOTES.Services;
using Microsoft.AspNetCore.Mvc;

namespace C__WEB_API_FALLENQUOTES.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase
{
    private readonly QuoteService _quoteService;

    // Constructor to inject the QuoteService
    public QuotesController(QuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Quote>>> Get()
    {
        return await _quoteService.GetAllQuotes();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Quote>> Get(int id)
    {
        var quote = await _quoteService.Get(id);
        if (quote is null) return NotFound();
        return quote;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Quote quote)
    {
        if (id != quote.Id) return BadRequest();

        var existingQuote = await _quoteService.Get(id);
        if (existingQuote is null) return NotFound();

        await _quoteService.Update(quote);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Quote quote)
    {
        await _quoteService.Add(quote);
        return CreatedAtAction(nameof(Get), new { Id = quote.Id }, quote);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var quote = await _quoteService.Get(id);
        if (quote is null) return NotFound();
        
        await _quoteService.Delete(id);
        return NoContent();
    }
}