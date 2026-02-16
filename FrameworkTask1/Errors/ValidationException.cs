namespace FrameworkTask1.Errors;


public sealed class ValidationException : BaseException
{
    public ValidationException(string message)
        : base(code: "validation", message: message, statusCode: 400)
    {
    }
}