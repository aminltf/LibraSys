#nullable disable

using System.Diagnostics;

namespace Application.Exceptions;

[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

    private string GetDebuggerDisplay()
    {
        return $"{GetType().Name}: {Message ?? "No message provided"}";
    }
}
