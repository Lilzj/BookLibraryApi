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
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckOutRepository _checkOutRepo;
        private readonly IBookRepository _bookRepo;
        private readonly LogService _log;
        private readonly IMapper _mapper;

        public CheckoutController(ICheckOutRepository repo, LogService log, 
            IMapper mapper, IBookRepository bookRepository)
        {
            _checkOutRepo = repo;
            _bookRepo = bookRepository;
            _log = log;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckoutDto model)
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            if (!userAuth.IsAdmin)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));

            var book = await _bookRepo.GetBookByIdAsync(model.BookId);

            if (book == null)
            {
                ModelState.AddModelError("Book", "Book was not found");
                return NotFound(Utilities.CreateResponse(message: "Book not found", errs: ModelState, data: ""));
            }

            var checkOut = _mapper.Map<CheckoutDto, BookActivity>(model);

            checkOut.Book = book;

            var response = await _checkOutRepo.BookCheckOutAsync(checkOut);

            book.AvailabilityStatus = false;
            await _bookRepo.UpdateAsync(book);

            if (!response)
            {
                ModelState.AddModelError("BookActivity", "Could not checkout book");
                return BadRequest(Utilities.CreateResponse(message: "BookActivity not added", errs: ModelState, data: ""));
            }

            var checkOutReturn = _mapper.Map<BookActivity, CheckoutReturnDto>(checkOut);

            return CreatedAtRoute("GetBookActivity", new { id = checkOutReturn.BookActivityId },
                Utilities.CreateResponse(message: "Added sucessfully", errs: null, data: checkOutReturn));
        }

        [HttpGet("{id}", Name = "GetBookActivity")]
        public async Task<IActionResult> GetBookActivityById(string id)
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(id))
            {
                ModelState.AddModelError("Book", "BookId Cannot be null");
                return NotFound(Utilities.CreateResponse(message: "BookId not found", errs: ModelState, ""));
            }
            var bookActivity = await _checkOutRepo.GetBookActivityByIdAsync(id);

            if (bookActivity == null)
            {
                ModelState.AddModelError("Book", "Book does not exist");
                return NotFound(Utilities.CreateResponse(message: "Book not found", errs: ModelState, ""));
            }

            var bookReturn = _mapper.Map<BookActivity, CheckoutReturnDto>(bookActivity);

            return Ok(Utilities.CreateResponse("Book Details", null, bookReturn));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CheckIn(string id)
        {
            var userAuth = UserAuthentication.Authenticate(Request.Headers);

            if (!userAuth.IsAuthenticated)
                return Unauthorized();

            if (!userAuth.IsAdmin)
                return Unauthorized();

            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("CheckIn", "Checkoutid was not provided");
                return BadRequest(Utilities.CreateResponse(message: "Checkoutid was not provided", errs: ModelState, ""));
            }

            var activity = await _checkOutRepo.GetBookActivityByIdAsync(id);

            if(activity == null)
            {
                ModelState.AddModelError("Book Details", "checkout details not exist");
                return NotFound(Utilities.CreateResponse(message: "Checkout deatils not found", errs: ModelState, ""));
            }
            var late = (DateTime.Now - activity.ExpectedDateOfReturn).Days;
            activity.Book.AvailabilityStatus = true;
            activity.CheckInDate = DateTime.Now;

            activity.NoOfDaysLate = late > 0 ? late : 0;
            activity.PenaltyFee = late > 0 ? (late * 200) : 0;           

            await _checkOutRepo.UpdateAsync(activity);

           return Ok(Utilities.CreateResponse("Check in was successful", null, ""));

        }
    }
}
