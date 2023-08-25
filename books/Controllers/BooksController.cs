using books.Models;
using books.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksRepository _booksReposiotry;
        public BooksController(IBooksRepository booksRepository) 
        {
            this._booksReposiotry = booksRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var listOfBooks = await _booksReposiotry.GetAllAsync();

            return Ok(listOfBooks);
        }

        
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookById(Guid Id)
        {
            // ADD VALIDATION
            var singleBook = await _booksReposiotry.GetAsync(Id);

            if (singleBook == null) 
            {
                return NotFound();
            }

            return Ok(singleBook);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddNewBook([FromBody] Book book)
        {
            // ADD VALIDATION (make sure Id is empty)
            var addBook = await _booksReposiotry.AddAsync(book);

            if (addBook == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(AddNewBook), addBook);

        }

        
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateBookById([FromQuery] Guid Id, [FromBody] Book book)
        {
            // ADD VALIDATION
            var updateBook = await _booksReposiotry.UpdateAsync(Id, book);

            if (updateBook == null)
            {
                return NotFound();
            }

            return Ok(updateBook);

        }

        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBookByID(Guid Id)
        {
            // ADD VALIDATION
            var deleteSingleBook = await _booksReposiotry.DeleteAsync(Id);

            if (deleteSingleBook == null)
            {
                return BadRequest();
            }

            return Ok(deleteSingleBook);
        }
    }
}
