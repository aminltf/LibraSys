#nullable disable

using MediatR;

namespace Application.Usecases.Author.Commands;

public class DeleteAuthorCommand : IRequest
{
    public Guid AuthorId { get; set; }
}
