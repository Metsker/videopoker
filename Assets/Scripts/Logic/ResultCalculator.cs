using System.Collections.Generic;
using System.Linq;
using Cards;
using Enums;

namespace Logic
{
    public class ResultCalculator
    {
        private readonly List<Card> _cards;

        public ResultCalculator(List<Card> cards)
        {
            _cards = cards;
        }

        public PokerHands CalculateResult()
        {
            PokerHands result = PokerHands.None;
            
            if (IsJacksAndHigherPair()) 
                result = PokerHands.Pair;

            if (IsTwoPair()) 
                result = PokerHands.TwoPair;

            if (IsThreeOfKind()) 
                result = PokerHands.ThreeOfKind;

            if (IsStraight()) 
                result = PokerHands.Straight;

            if (IsFlush()) 
                result = PokerHands.Flush;

            if (IsFullHouse())
                result = PokerHands.FullHouse;

            if (IsFourOfKind()) 
                result = PokerHands.FourOfKind;

            if (IsStraightFlush()) 
                result = PokerHands.StraightFlush;

            if (IsRoyalFlush()) 
                result = PokerHands.RoyalFlush;
            
            return result;
        }

        private bool HasPair() =>
            _cards.GroupBy(card => card.cardData.value)
                .Count(group => group.Count() == 2) == 1;

        private bool HasJacksAndHigherPair() =>
            _cards.Where(card => card.cardData.value > Value.Ten).GroupBy(card => card.cardData.value)
                .Count(group => group.Count() == 2) == 1;

        private bool IsJacksAndHigherPair() =>
            _cards.Where(card => card.cardData.value > Value.Ten).GroupBy(card => card.cardData.value)
                .Count(group => group.Count() == 3) == 0
            && HasJacksAndHigherPair();

        private bool IsTwoPair() =>
            _cards.GroupBy(card => card.cardData.value)
                .Count(group => group.Count() >= 2) == 2;

        private bool IsStraight() =>
            _cards.GroupBy(card => card.cardData.value)
                .Count() == _cards.Count()
            && _cards.Max(card => (int)card.cardData.value)
            - _cards.Min(card => (int)card.cardData.value) == 4;

        private bool HasThreeOfKind() =>
            _cards.GroupBy(card => card.cardData.value)
                .Any(group => group.Count() == 3);

        private bool IsThreeOfKind() => 
            HasThreeOfKind() && !HasPair();

        private bool IsFlush() =>
            _cards.GroupBy(card => card.cardData.suit).Count() == 1;

        private bool IsFourOfKind() => 
            _cards.GroupBy(card => card.cardData.value)
                .Any(group => group.Count() == 4);

        private bool IsFullHouse() =>
            HasPair() && HasThreeOfKind();

        private bool HasStraightFlush() =>
            IsFlush() && IsStraight();

        private bool IsRoyalFlush() =>
            _cards.Min(card => (int)card.cardData.value) == (int)Value.Ten
            && HasStraightFlush();

        private bool IsStraightFlush() =>
            HasStraightFlush() && !IsRoyalFlush();
    }
}