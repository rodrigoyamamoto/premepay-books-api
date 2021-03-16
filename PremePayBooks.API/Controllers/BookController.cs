using Microsoft.AspNetCore.Mvc;
using PremePayBooks.API.Data;
using PremePayBooks.API.Models;
using System;
using System.Threading.Tasks;

namespace PremePayBooks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var book = await _bookRepository.GetAllBooks();
            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepository.GetBookById(id);
            return Ok(book);
        }

        [HttpPost()]
        public async Task<IActionResult> AddBook(Book book)
        {
            _bookRepository.Add(book);
            await _bookRepository.Save();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, Book book)
        {
            var bookToUpdate = await _bookRepository.GetBookById(id);

            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Price = book.Price;

            if (await _bookRepository.Save())
                return NoContent();

            throw new Exception($"Updating book {id} failed on save");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var book = await _bookRepository.GetBookById(id);

            if (book == null) return NotFound();

            _bookRepository.Remove(book);
            await _bookRepository.Save();

            return NoContent();
        }
    }
}