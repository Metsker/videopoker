using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI holdText;
        
        public CardData cardData;
        

        public bool hold;

        public event Action Start;

        public void Construct(CardData data)
        {
            cardData = data;
            image.sprite = cardData.sprite;
        }
        
        public void ToggleHoldText(bool state)
        {
            hold = state;
            holdText.gameObject.SetActive(state);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ToggleHoldText(true);
            Start?.Invoke();
        }
    }
}