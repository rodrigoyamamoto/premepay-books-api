using PremePayBooks.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PremePayBooks.API.Data
{
    public interface IBookRepository
    {
        void Add(Book book);

        void Remove(Book book);

        Task<bool> Save();

        Task<Book> GetBookById(Guid id);

        Task<IEnumerable<Book>> GetAllBooks();
    }
}