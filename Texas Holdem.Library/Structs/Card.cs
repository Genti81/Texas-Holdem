using System;
using Texas_Holdem.Library.Enums;

namespace Texas_Holdem.Library.Structs
{
    public struct Card : IComparable
    {
        public Values Value { get; }
        public Suits Suit { get; set; }
        public string Output
        {
            get
            {
                var value = (int)Value <= 10 ? ((int)Value).ToString() : Value.ToString().Substring(0, 1);
                return (char)(Suit) + "\n" + value;
            }   
        }
        public Card(Values value, Suits suit)
        {
            Value = value;
            Suit = suit;
        }

        public int CompareTo(object otherCard)
        {
            if (Value > ((Card)otherCard).Value)
                return 1;
            else if (Value.Equals(((Card)otherCard).Value))
                return 0;
            else
                return -1;
        }
    }
}
    