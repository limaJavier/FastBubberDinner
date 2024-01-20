namespace FastBubberDinner.Domain.Common.Errors;

public interface IServiceError
{
    string Detail { get; }
    int Status { get; }
}