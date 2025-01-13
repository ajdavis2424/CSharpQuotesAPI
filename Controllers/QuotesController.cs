//Controller will connect service class to client

using C__WEB_API_FALLENQUOTES.Models;
using C__WEB_API_FALLENQUOTES.Services;
using Microsoft.AspNetCore.Mvc; //

namespace C__WEB_API_FALLENQUOTES.Controllers;

//Annotations--- Routes suomatically created 
[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase{
//http:localhost:5018/Quotes w/dotnet run -- can access w/Swagger too via program template!!
/*HTTP Action to GET all the quotes from our service and to the client(browser)
 Will call getAllQuotes method from QuoteServise */

[HttpGet] //-->Pass in List of Quote -->Get
public ActionResult<List<Quote>> Get() =>QuoteService.GetAllQuotes();

[HttpGet("{id}")] //--> Supply id for 
public ActionResult<Quote>Get(int id){
  var quote = QuoteService.Get(id);
  if(quote is null) return NotFound();
  return quote;
}
[HttpPut("{id}")]
public IActionResult Update(int id, Quote quote){
//check if I matches the new quote Id
if(id != quote.Id)return BadRequest();

//Check if the quote exists
var existingQuote = QuoteService.Get(id);
if(existingQuote is null) return NotFound();

QuoteService.Update(quote);
return NoContent();
}
[HttpPost()]
public IActionResult Create(Quote quote){
QuoteService.Add(quote);
return CreatedAtAction(nameof(Get), new{Id=quote.Id}, quote);
}
[HttpDelete("{id}")]
public IActionResult Delete(int id){
  //Check 
  var quote = QuoteService.Get(id);
  if(quote is null) return NotFound();
  QuoteService.Delete(id);
  return NoContent();
}
}