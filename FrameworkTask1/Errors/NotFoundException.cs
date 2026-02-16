namespace FrameworkTask1.Errors;


public sealed class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(code: "not_found", message: message, statusCode: 404)
    {
    }
}
