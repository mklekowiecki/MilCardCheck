using MilCardApiSrv.Application.CardAccess.Query;
using MilCardApiSrv.Application.Common.Interfaces;
using MilCardApiSrv.Application.Interfaces;
using MilCardApiSrv.Domain.Enums;

namespace MilCardCheck.Application.CardAccess.Commands;

public record GetCardAccessCommand : IRequest<CardActionResult>
{
    public string UserId { get; init; } = string.Empty;
    public string CardNumber { get; init; } = string.Empty;
}

public class GetCardAccessCommandHandler : IRequestHandler<GetCardAccessCommand, CardActionResult>
{
    private readonly IApplicationDbContext _context;
    private readonly ICardOwnerService _cardOwnerService;

    public GetCardAccessCommandHandler(IApplicationDbContext context, ICardOwnerService cardOwnerService)
    {
        _context = context;
        _cardOwnerService = cardOwnerService;
    }

    public async Task<CardActionResult> Handle(GetCardAccessCommand request, CancellationToken cancellationToken)
    {
        var cardData = await _cardOwnerService.GetCardDetails(
            userId: request.UserId,
            cardNumber: request.CardNumber);

        if (cardData == null)
            return new CardActionResult { Actions = new List<string> { "NotFound" } };

        var actions = await _context.CardActions
            .Where(
                x => (x.CardStatus == CardStatus.Any || x.CardStatus == cardData.CardStatus) &&
                     (x.CardType == CardType.Any || x.CardType == cardData.CardType) &&
                     (x.IsPinSet == PinSet.Ignore || x.IsPinSet == (cardData.IsPinSet ? PinSet.Set : PinSet.NotSet))
                )
            .Select(x => x.CardActionDescription)
            .ToListAsync(cancellationToken);

        return new CardActionResult { Actions = actions };
    }
}
