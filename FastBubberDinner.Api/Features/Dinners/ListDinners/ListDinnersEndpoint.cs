using FastEndpoints;
namespace FastBubberDinner.Api.Features.Dinner;

public class ListDinnersEndpoint : Endpoint<object>
{
    public override void Configure()
    {
        Get("/dinners");
        AllowAnonymous();
    }
    public override async Task HandleAsync(object req, CancellationToken ct)
    {
        await SendAsync(new List<string>());
    }
}