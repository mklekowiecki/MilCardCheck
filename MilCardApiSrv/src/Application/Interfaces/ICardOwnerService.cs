using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilCardApiSrv.Domain.Enums;

namespace MilCardApiSrv.Application.Interfaces;

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);

public interface ICardOwnerService
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
}
