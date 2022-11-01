using System;
using Cards;
using Enums;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class Utility
    {
#if UNITY_EDITOR
        [MenuItem("Assets/CreateCards")]
        public static void CreateCards()
        {
            for (int j = 0; j < 4; j++)
            {
                var suit = (Suit)j;
            
                for (int i = 0; i < Enum.GetNames(typeof(Value)).Length; i++)
                {
                    var asset = Resources.Load<CardData>($"Cards/{suit}s/{Enum.GetName(typeof(Value),i)}");
                    asset.suit = suit;
                    asset.value = (Value) i;
                
                    var num = i + 2 < 10 ? "0" + (i + 2) : (i + 2).ToString();
            
                    asset.sprite = Resources.Load<Sprite>
                        ($"Playing Cards/Image/PlayingCards/{Enum.GetName(typeof(Suit), suit)}{num}");
                
                    AssetDatabase.SaveAssets();
                }
            }
#endif
        }
    }
}