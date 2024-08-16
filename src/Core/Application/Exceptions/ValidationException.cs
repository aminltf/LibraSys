#nullable disable

using System.Diagnostics;

namespace Application.Exceptions;

[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
public class ValidationException : Exception
{
    public ValidationException() { }
    public ValidationException(string message) : base(message) { }
    public ValidationException(string message, Exception innerException) : base(message, innerException) { }

    private string GetDebuggerDisplay()
    {
        return $"{GetType().Name}: {Message ?? "No message provided"}";
    }
}
