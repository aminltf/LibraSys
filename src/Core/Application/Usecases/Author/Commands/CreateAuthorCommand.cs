#nullable disable

using MediatR;

namespace Application.Usecases.Author.Commands;

public class CreateAuthorCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }
}
