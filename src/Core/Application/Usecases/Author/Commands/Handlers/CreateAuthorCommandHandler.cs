#nullable disable

using Application.Interfaces;
using MediatR;

namespace Application.Usecases.Author.Commands.Handlers;

//public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
//{
//    private readonly IAuthorRepository _authorRepository;

//    public CreateAuthorCommandHandler(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

//    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
//    {
//        var author = new Author(
//            request.FirstName,
//            request.LastName,
//            request.DateOfBirth,
//            request.Biography
//        );
//        await _authorRepository.Add(author, cancellationToken);
//        return author.AuthorId;
//    }
//}
