#nullable disable

using MediatR;

namespace Application.Usecases.Author.Commands;

public class UpdateAuthorCommand : IRequest
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }
}
