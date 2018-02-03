using System.Collections.Generic;
using Texas_Holdem.Library.Enums;
using Texas_Holdem.Library.Structs;

namespace Texas_Holdem.Library.Interfaces
{
    public interface IHandEvaluator
    {
        (List<Card> Cards, Hands HandValue, Suits Suit) EvaluateHand(List<Card> cards);
    }
}

