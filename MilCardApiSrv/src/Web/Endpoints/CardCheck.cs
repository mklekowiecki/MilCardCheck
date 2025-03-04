using Microsoft.AspNetCore.Http.HttpResults;
using MilCardApiSrv.Application.CardAccess.Commands;
using MilCardCheck.Application.CardAccess.Commands;

namespace MilCardCheck.Web.Endpoints;

public class CardCheck : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapPost(CheckAccess);
    }

    public async Task<Ok<CardActionResult>> CheckAccess(ISender sender, GetCardAccessCommand command)
    {
        CardActionResult actionResult = await sender.Send(command).ConfigureAwait(false);
        return TypedResults.Ok(actionResult);
    }
}
