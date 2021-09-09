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

        [HttpGet]
        public async Task<IActionResult> GetBookById(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                ModelState.AddModelError("Book", "BookId Cannot be null");
                return NotFound(Utilities.CreateResponse(message: "BookId not found", errs: ModelState, ""));
            }
            var book = await _repo.GetBookByIdAsync(bookId);

            if (book == null)
            {
                ModelState.AddModelError("Book", "Book does not exist");
                return NotFound(Utilities.CreateResponse(message: "Book not found", errs: ModelState, ""));
            }

            var bookReturn = _mapper.Map<Book, BookReturnDto>(book);

            return Ok(bookReturn);
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));   
            }

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
    }
}
