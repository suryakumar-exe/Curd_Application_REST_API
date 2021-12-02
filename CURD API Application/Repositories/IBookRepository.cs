using CURD_API_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CURD_API_Application.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get(); //Get All Books
        Task<Book> Get(int id); //Get Book By ID
        Task<Book> Create(Book book);
        Task Update(Book book);
        Task Delete(int id);
    }
}
