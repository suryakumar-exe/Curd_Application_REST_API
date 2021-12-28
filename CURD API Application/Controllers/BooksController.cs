using CURD_API_Application.Models;
using CURD_API_Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CURD_API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        //Action Methods
        [HttpGet]
        [Route("FetchAllBooks")]
        public async Task<IEnumerable<Book>> GetBooks() //Get All Books
        {
            return await _bookRepository.Get();
        }
       
        [HttpGet("GetBook/{id}")]
        
        public async Task<Book> GetBooks(int id) //Get book by id
        {
            return await _bookRepository.Get(id);
        }
        [HttpPost]
        [Route("CreateBook")]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book) //create book
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);

        }
        [HttpPut]
        [Route("UpdateBook/{id}")]
        public async Task<ActionResult> putBooks(int id,[FromBody] Book book) //update book
        {
            if(id != book.Id)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
       /* [Route("DeleteBook")]*/
        public async Task<ActionResult> delete(int id) //delete
        {
            var booktoDelete = await _bookRepository.Get(id);
            if(booktoDelete == null)
            {
                return NotFound();
            }
            await _bookRepository.Delete(booktoDelete.Id);
            return NoContent();
        }
        [HttpGet]
        [Route("Dictinary")]
        public string Dict() //Get All Books
        {
            /* Dictionary<int, String> d = new Dictionary<int, string>();
             d.Add(1, "Surya");
             d.Add(2, "Surya1");
             d.Add(3, "Surya2");
             d.Add(4, "Surya3");
             var json = JsonConvert.SerializeObject(d);
             return json;*//* Dictionary<int, String> d = new Dictionary<int, string>();
             d.Add(1, "Surya");
             d.Add(2, "Surya1");
             d.Add(3, "Surya2");
             d.Add(4, "Surya3");
             var json = JsonConvert.SerializeObject(d);
             return json;*/
            Dictionary<int, Book> d = new Dictionary<int, Book>();
            Book b = new Book();
            b.Id = 1;
            b.Title = "Newbook";
            b.Author = "surya";
            b.Description = "Good";
            Book b1 = new Book();
            b1.Id = 2;
            b1.Title = "Newbook11";
            b1.Author = "surya1";
            b1.Description = "Good1";
            d.Add(b.Id, b);
            d.Add(b1.Id, b1);
            string json = JsonConvert.SerializeObject(d);
            
            return json;
            /*List<Book> bo = new List<Book>();
            Book b = new Book();
            b.Id = 1;
            b.Title = "Newbook";
            b.Author = "surya";
            b.Description = "Good";
            bo.Add(b);
            var json = JsonConvert.SerializeObject(bo);
            return json;*/


        }

    }
}
