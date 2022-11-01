using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Deck
    {
        private const string Path = "Cards";
        private readonly Queue<CardData> _deck = new Queue<CardData>();

        public Deck()
        {
            foreach (CardData card in Resources.LoadAll<CardData>(Path)) 
                _deck.Enqueue(card);
        }

        public void Shuffle() => 
            _deck.Shuffle();

        public CardData GetNext() => 
            _deck.Dequeue();
        
        public void Return(CardData card) => 
            _deck.Enqueue(card);
    }
}