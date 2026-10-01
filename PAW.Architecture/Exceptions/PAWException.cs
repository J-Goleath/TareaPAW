public class PAWException : Exception
{
    private readonly Exception exception;

    public PAWException()
    {
        exception = new Exception();
    }

    public PAWException(string message) : base(message)
    {
        exception = new Exception(message);
    }

    public PAWException(Exception ex) : base(ex.Message, ex)
    {
        exception = ex;
    }

    public static PAWException MustThrow()
    {
        return new PAWException();
    }

    public static PAWException MustThrow(string message)
    {
        return new PAWException(message);
    }

    public static PAWException MustThrow(Exception ex)
    {
        return new PAWException(ex);
    }

    internal static void ThrowIfNull<TOut>(TOut? deseralized, string message)
    {
        if (deseralized is null)
        {
            throw new Exception(message);
        }
    }

    internal static void ThrowIfNull(object deseralized, string message)
    {
        if (deseralized is null)
        {
            throw new Exception(message);
        }
    }

    internal static void ThrowIfNullOrEmpty(string? clientBaseUrl, string message)
    {
        if (string.IsNullOrEmpty(clientBaseUrl))
        {
            throw new Exception(message);
        }
    }
}
