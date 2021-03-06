using AutoMapper;
using EBook_Library.Common;
using EBook_Library.Core.Interface;
using EBook_Library.Model.Dto;
using EBook_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBook_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookRepository _repo;
        private readonly LogService _log;
        private readonly IMapper _mapper;

        public BookController(IBookRepository repo, LogService log, IMapper mapper)
        {
            _repo = repo;
            _log = log;
            _mapper = mapper;
        }


        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBookById(string id)
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(id))
            {
                ModelState.AddModelError("Book", "BookId Cannot be null");
                return BadRequest(Utilities.CreateResponse(message: "BookId not found", errs: ModelState, ""));
            }
            var book = await _repo.GetBookByIdAsync(id);

            if (book == null)
            {
                ModelState.AddModelError("Book", "Book does not exist");
                return NotFound(Utilities.CreateResponse(message: "Book not found", errs: ModelState, ""));
            }

            var bookReturn = _mapper.Map<Book, BookReturnDto>(book);

            return Ok(Utilities.CreateResponse("Book Details", null, bookReturn));
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookDto model)
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            if (!userAuth.IsAdmin)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));   

             var book = _mapper.Map<AddBookDto, Book>(model);

            var response = await _repo.AddBookAsync(book);

            if (!response)
            {
                ModelState.AddModelError("Book", "Could not add book");
                return BadRequest(Utilities.CreateResponse(message: "Book not added", errs: ModelState, data: ""));
            }

            var bookReturn = _mapper.Map<Book, BookReturnDto>(book);

            return CreatedAtRoute("GetBook", new { id =  bookReturn.BookId},
                Utilities.CreateResponse(message: "Added sucessfully", errs: null, data: bookReturn)); 
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBook([FromQuery] SearchDto query)
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            if (query == null)
                return BadRequest(Utilities.CreateResponse("No search parameter was provided", errs: ModelState, data: ""));

            var books =await  _repo.SearchBook(query);

            if (books == null)
            {
                ModelState.AddModelError("Book not found", $"No book found for {query} entered");
                return NotFound(Utilities.CreateResponse(message: "Book not found", errs: ModelState, ""));
            }

            var response = _mapper.Map<IEnumerable<Book>, IEnumerable<BookReturnDto>>(books);

            return Ok(Utilities.CreateResponse(message: "Books gotten", errs: null, data: response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            var books = await _repo.GetAllBooksAsync();

            var bookReturn = _mapper.Map<IEnumerable<BookReturnDto>>(books);

            return Ok(Utilities.CreateResponse(message: "All Books sorted by Publish year in ascending order", errs: null, data: bookReturn));
        }

       
    }
}
