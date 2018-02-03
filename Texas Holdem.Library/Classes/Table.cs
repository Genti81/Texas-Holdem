using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texas_Holdem.Library.Enums;
using Texas_Holdem.Library.Interfaces;

namespace Texas_Holdem.Library.Classes
{
    public class Table
    {
        private Deck _deck = new Deck();
        public List<Player> Player { get; } = new List<Player>();
        public Player Dealer { get; } = new Player("Dealer");

        public Table(string[] playerNames)
        {
            if (playerNames == null || playerNames.Count() == 0 || playerNames.Count() < 2 || playerNames.Count() > 4)
            {
                throw new ArgumentException("Incorrect number of players");
            }
            else
            {
                for (int i = 0; i < playerNames.Count(); i++)
                {
                    Player.Add(new Player(playerNames[i]));
                }
            }
        }

        private void DealPlayersCards()
        {
            foreach (var aPlayer in Player)
            {
                aPlayer.ClearHand();
                aPlayer.ReceiveCard(_deck.DrawCard(), true);
                aPlayer.ReceiveCard(_deck.DrawCard(), true);
            }
        }
        public void DealNewHand()
        {
            _deck.ShuffleDeck(150);
            Dealer.ClearHand();
            DealPlayersCards();
        }
        public void DealerDrawCard(int count = 1)
        {
            if (Dealer.CardCount == 5)
                return;
            for (int i = 0; i < count; i++)
            {
                var card = _deck.DrawCard();
                Dealer.ReceiveCard(card);
                foreach (var player in Player)
                {
                    
                    player.ReceiveCard(card, true);
                }
            }

        }
        public void EvaluatePlayerHands()
        {
            foreach (var player in Player)
            {
                player.BestCards.Clear();
                player.EvaluateHand();
            }
        }
        public List<Player> DetermineWinner()
        {
            var highestHand = Player.Max(m => m.HandValue);
            var bestHands = Player.Where(p => p.HandValue.Equals(highestHand));

            if (bestHands.Count().Equals(1)) return bestHands.ToList();

            List<Player> players;
            switch (highestHand)
            {
                #region High Card
                case Hands.Nothing:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);
                        var card2 = savedPlayerCards[3].CompareTo(currentPlayerCards[3]);
                        var card3 = savedPlayerCards[2].CompareTo(currentPlayerCards[2]);
                        var card4 = savedPlayerCards[1].CompareTo(currentPlayerCards[1]);
                        var card5 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]);

                        if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && card4.Equals(0) && card5.Equals(0)) players.Add(player);
                        if (card1.Equals(-1) || card2.Equals(-1) || card3.Equals(-1) || card4.Equals(-1) || card5.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Pair
                case Hands.Pair:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]);
                        var card2 = savedPlayerCards[1].CompareTo(currentPlayerCards[1]);
                        var card3 = savedPlayerCards[2].CompareTo(currentPlayerCards[2]);
                        var card4 = savedPlayerCards[3].CompareTo(currentPlayerCards[3]);
                        var card5 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && card4.Equals(0) && card5.Equals(0)) players.Add(player);
                        else if (card1.Equals(0) && card2.Equals(0) && (card3.Equals(-1) || card4.Equals(-1) || card5.Equals(-1)))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                        else if (card1.Equals(-1) && card2.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Two Pair
                case Hands.TwoPair:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]);
                        var card2 = savedPlayerCards[1].CompareTo(currentPlayerCards[1]);
                        var card3 = savedPlayerCards[2].CompareTo(currentPlayerCards[2]);
                        var card4 = savedPlayerCards[3].CompareTo(currentPlayerCards[3]);
                        var card5 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && card4.Equals(0) && card5.Equals(0)) players.Add(player);
                        else if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && card4.Equals(0) && card5.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                        else if (card1.Equals(0) && card2.Equals(0) && card3.Equals(-1) && card4.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                        else if (card1.Equals(-1) && card2.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Three of a kind
                case Hands.ThreeOfAKind:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]);
                        var card2 = savedPlayerCards[1].CompareTo(currentPlayerCards[1]);
                        var card3 = savedPlayerCards[2].CompareTo(currentPlayerCards[2]);
                        var card4 = savedPlayerCards[3].CompareTo(currentPlayerCards[3]);
                        var card5 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && card4.Equals(0) && card5.Equals(0)) players.Add(player);
                        else if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && (card4.Equals(-1) || card5.Equals(-1)))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                        else if (card1.Equals(-1) && card2.Equals(-1) && card3.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Straight
                case Hands.Straight:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card1.Equals(0)) players.Add(player);
                        if (card1.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Flush
                case Hands.Flush:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]);
                        var card2 = savedPlayerCards[1].CompareTo(currentPlayerCards[1]);
                        var card3 = savedPlayerCards[2].CompareTo(currentPlayerCards[2]);
                        var card4 = savedPlayerCards[3].CompareTo(currentPlayerCards[3]);
                        var card5 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card1.Equals(0) && card2.Equals(0) && card3.Equals(0) && card4.Equals(0) && card5.Equals(0)) players.Add(player);
                        else if (card1.Equals(-1) || card2.Equals(-1) || card3.Equals(-1) || card4.Equals(-1) || card5.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Full House
                case Hands.FullHouse:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]); // Pair
                        var card3 = savedPlayerCards[2].CompareTo(currentPlayerCards[2]); // Three of a kind

                        if (card1.Equals(0) && card3.Equals(0)) players.Add(player);
                        else if (card3.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                        else if (card1.Equals(-1) && card3.Equals(0))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Four of a Kind
                case Hands.FourOfAKind:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card1 = savedPlayerCards[0].CompareTo(currentPlayerCards[0]);
                        var card5 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card1.Equals(0) && card5.Equals(0)) players.Add(player);
                        else if (card1.Equals(0) && card5.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                        else if (card1.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Straight Flush
                case Hands.StraightFlush:
                    players = bestHands.Take(1).ToList();
                    foreach (var player in bestHands.Skip(1))
                    {
                        var savedPlayerCards = players.First().BestCards;
                        var currentPlayerCards = player.BestCards;

                        var card5 = savedPlayerCards[4].CompareTo(currentPlayerCards[4]);

                        if (card5.Equals(0)) players.Add(player);           
                        else if (card5.Equals(-1))
                        {
                            players.Clear();
                            players.Add(player);
                        }
                    }
                    return players;
                #endregion
                #region Royal Straight Flush
                case Hands.RoyalStraightFlush:
                    return bestHands.ToList();
                    #endregion
            }

            return null;
        }
                
    }
}
