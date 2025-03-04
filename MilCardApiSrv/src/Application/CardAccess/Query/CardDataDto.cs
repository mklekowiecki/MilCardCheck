using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilCardApiSrv.Domain.Entities;
using MilCardApiSrv.Domain.Enums;

namespace MilCardApiSrv.Application.CardAccess.Query;
internal class CardDataDto
{
    public CardType CardType { get; set; }

    public CardStatus CardStatus { get; set; }

    public PinSet IsPinSet { get; set; }

    public string CardActionDescription { get; set; } = "";

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CardAction, CardDataDto>();
        }
    }
}
