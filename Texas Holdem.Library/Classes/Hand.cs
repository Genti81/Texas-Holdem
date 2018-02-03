using Holdem.Library.Classes;
using System.Collections.Generic;
using Texas_Holdem.Library.Enums;
using Texas_Holdem.Library.Interfaces;
using Texas_Holdem.Library.Structs;

namespace Texas_Holdem.Library.Classes
{
    public class Hand
    {
        public List<Card> Cards { get; } = new List<Card>();
        public List<Card> BestCards { get; } = new List<Card>();
        public List<Card> PlayerCards { get; } = new List<Card>();
        private IHandEvaluator _eval;
        public Hands HandValue { get; private set; }
        public Suits Suit { get; private set; }

        //public Hand() { }

        public void Clear()
        {
            Cards.Clear();
            BestCards.Clear();
            PlayerCards.Clear();
            HandValue = Hands.Nothing;
            Suit = 0;
        }
        public void AddCard(Card card, bool isPlayerCard)
        {
            if (isPlayerCard == true && PlayerCards.Count < 2)
            {
                PlayerCards.Add(card);
            }
            Cards.Add(card);
        }

        // 3.
        public Hand(IHandEvaluator eval)
        {
            _eval = eval;
        }

        // 4.
        public void EvaluateHand()
        {
            if (Cards.Count >= 5)
            {
                var eval = _eval.EvaluateHand(Cards);
                BestCards.AddRange(eval.Cards);
                HandValue = eval.HandValue;
                Suit = eval.Suit;
            }
        }
    }
}
