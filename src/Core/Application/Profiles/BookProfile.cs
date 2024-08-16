#nullable disable

using Application.Dtos.Book;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        // Map CreateBookDto to Book, ignoring all members
        CreateMap<CreateBookDto, Book>()
            .ForAllMembers(opt => opt.Ignore());

        // Map Book to CreateBookDto
        CreateMap<Book, CreateBookDto>();

        // Map UpdateBookDto to Book, ignoring all members
        CreateMap<UpdateBookDto, Book>()
            .ForAllMembers(opt => opt.Ignore());

        // Map Book to UpdateBookDto
        CreateMap<Book, UpdateBookDto>();

        // Map BookDto to Book, ignoring the Id property
        CreateMap<BookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Map Book to BookDto
        CreateMap<Book, BookDto>();
    }
}
