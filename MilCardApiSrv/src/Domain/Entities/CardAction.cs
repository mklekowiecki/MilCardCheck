namespace MilCardApiSrv.Domain.Entities;

public class CardAction: BaseAuditableEntity
{
    public CardAction(CardType cardType, CardStatus cardStatus, PinSet isPinSet, string cardActionDescription)
    {
        CardType = cardType;
        CardStatus = cardStatus;
        IsPinSet = isPinSet;
        CardActionDescription = cardActionDescription;
    }

    public CardType CardType { get; set; }

    public CardStatus CardStatus { get; set; }

    public PinSet IsPinSet { get; set; }

    public string CardActionDescription { get; set; } = "";
}
