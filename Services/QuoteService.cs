//"ENGINE" class... Logic goes here

using C__WEB_API_FALLENQUOTES.Models;

namespace C__WEB_API_FALLENQUOTES.Services;

public class QuoteService{
  //create instance of id to track each quote add/deleted etc 
static int availableId = 3;

//Use a list to store our quotes, not a real DB -- cache
 static List<Quote>QuotesDB; 

 //static instance- this class must not be instantiated anymore--SINGLETON
static QuoteService(){
  //populate DB with small data 
  QuotesDB = new List<Quote>(){
    new Quote{
      Id=0, 
    Author="John McKay, USC", 
    Quotes= $"We didn’t tackle well today but we made up for it by not blocking."
    },
    new Quote{
      Id=1, 
    Author="Bob Devany, Nebraska", 
    Quotes= "I don’t want to win enough to be placed on NCAA probation, I just want to win enough to warrant an investigation."
    },
    new Quote{
      Id=2, 
    Author="Erk Russell, Georgia Southern", 
    Quotes= "At Georgia Southern, we don’t cheat. That costs money and we don’t have any."
    },
  };
}
//1. Method to get all the codes in the QuotesDB
public static List<Quote> GetAllQuotes()=>QuotesDB;
//2. Method to get single quote by Id
public static Quote? Get(int Id)=>QuotesDB.FirstOrDefault(quote =>quote.Id ==Id);

//3. Method to UPDATE(PUT) an existing quote
public static void Update(Quote quote){
  //get index number of quote
  var index = QuotesDB.FindIndex(q=>q.Id==quote.Id);
  //Quote not found? Then retun
  if(index == -1) return;
  //Update quote by index
  QuotesDB[index]=quote;
}
//4. Method to ADD new quote
public static void Add(Quote quote){
  quote.Id = availableId++;
  QuotesDB.Add(quote);
}
//5. Method to DELETE a quote by id
public static void Delete(int id ){
//get quote by id 
var quote =Get(id);
//check if id exists
if(quote is null) return;
QuotesDB.Remove(quote);
}
}
