using Microsoft.AspNetCore.Http.HttpResults;
using MilCardCheck.Application.CardAccess.Commands;

namespace MilCardCheck.Web.Endpoints;
public class CardCheck : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CheckAccess);
    }


    public async Task<Ok<int>> CheckAccess(ISender sender, GetCardAccessCommand command)
    {
        int id = await sender.Send(command);
        return TypedResults.Ok(id);
    }
}
