using AutoMapper;
using EBook_Library.Model.Dto;
using EBook_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBook_Library.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookDto, Book>().ReverseMap();
            CreateMap<Book, BookReturnDto>().ReverseMap();
            CreateMap<CheckoutDto, BookActivity>().ReverseMap();
            CreateMap<BookActivity, CheckoutReturnDto>()
                 .ForMember(CheckoutReturnDto => CheckoutReturnDto.BookId, x =>
                x.MapFrom(Book => Book.Book.BookId));

            CreateMap<BookActivity, BookActivityDto>();
        }
    }
}
