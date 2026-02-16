namespace FrameworkTask1.Errors;


/// <summary>
/// Базовое исключение предметной области.
/// </summary>
public abstract class BaseException : Exception
{
    protected BaseException(string code, string message, int statusCode)
        : base(message)
    {
        Code = code;
        StatusCode = statusCode;
    }

    public string Code { get; }

    public int StatusCode { get; }
}