using Holdem.Library.Classes;
using System.Collections.Generic;
using Texas_Holdem.Library.Enums;
using Texas_Holdem.Library.Interfaces;
using Texas_Holdem.Library.Structs;

namespace Texas_Holdem.Library.Classes
{
    public class Player
    {
        public Hand _hand { get; }
        IHandEvaluator handEval;
        public string Name { get; }
        public List<Card> Cards { get => _hand.Cards; }
        public List<Card> BestCards { get => _hand.BestCards; }
        public List<Card> PlayerCards { get => _hand.PlayerCards; }

        public int CardCount { get => Cards.Count; }

        public Hands HandValue { get => _hand.HandValue; }
        public Suits Suit { get => _hand.Suit; }

        public Player(string name)
        {
            Name = name;
             handEval = new HandEvaluator();
            _hand = new Hand(handEval);

        }

        public void ReceiveCard(Card card, bool isPlayerCard = false)
        {
            _hand.AddCard(card, isPlayerCard);
        }
        public void ClearHand()
        {
            _hand.Clear();
        }
        public void EvaluateHand()
        {
            _hand.EvaluateHand();
        }
    }
}
