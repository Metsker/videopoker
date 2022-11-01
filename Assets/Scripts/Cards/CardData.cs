using Enums;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu]
    public class CardData : ScriptableObject
    {
        public Suit suit;
        public Value value;
        public Sprite sprite;
    }
}