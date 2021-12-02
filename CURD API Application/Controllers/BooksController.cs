using CURD_API_Application.Models;
using CURD_API_Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("UpdateBook")]
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

    }
}
