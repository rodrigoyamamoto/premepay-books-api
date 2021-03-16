using Microsoft.EntityFrameworkCore;
using PremePayBooks.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PremePayBooks.API.Data
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Add(book);
        }

        public void Remove(Book book)
        {
            _context.Remove(book);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}