namespace FastBubberDinner.Domain.Common.Errors;

public class ServiceException : Exception, IServiceError
{
    public int Status { get; set; }
    public string Detail { get; set; } = null!;
    public ServiceException(string message, string detail = "", int status = 500) : base(message)
    {
        Status = status;
        Detail = detail;
    }
}