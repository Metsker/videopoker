using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Enums;
using Progress;
using TMPro;
using UnityEngine;

namespace Logic
{
    public class GameFlow : MonoBehaviour
    {
        private const int Bet = 5;
        private const int RestartDelay = 4;
        private const int EvaluateDelay = 3;

        [SerializeField] private List<Card> cards;
        [SerializeField] private PlayerProgress playerProgress;
        [SerializeField] private TextMeshProUGUI resultText;
        
        private Deck _deck;
        private float _evaluateTime;
        private bool _isTimerOn;

        public event Action UpdateUI;


        private void Start()
        {
            _deck = new Deck();
            SetupNewGame();
        }

        private void OnEnable()
        {
            foreach (Card card in cards) 
                card.Start += StartTimer;
        }

        private void OnDisable()
        {
            foreach (Card card in cards) 
                card.Start -= StartTimer;
        }

        private void SetupNewGame()
        {
            _deck.Shuffle();
            
            foreach (Card card in cards)
            {
                if (card.cardData != null) 
                    _deck.Return(card.cardData);
                
                ReplaceCard(card);
            }
        }

        private void ReplaceCard(Card card)
        {
            CardData next = _deck.GetNext();
            card.Construct(next);
        }

        private void StartTimer()
        {
            _evaluateTime = EvaluateDelay;
            
            if (_isTimerOn || playerProgress.Cash < Bet) 
                return;
            
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            _isTimerOn = true;

            while (_evaluateTime > 0)
            {
                _evaluateTime -= Time.deltaTime;
                yield return null;
            }
            Discard();
            StartCoroutine(FindResultAndRestart());

            _isTimerOn = false;
        }

        private void Discard()
        {
            foreach (Card card in cards.Where(c => !c.hold))
            {
                _deck.Return(card.cardData);
                ReplaceCard(card);
            }
        }

        private IEnumerator FindResultAndRestart()
        {
            PokerHands result = Result();
            
            playerProgress.Cash += (int)result;
            
            UpdateResultUI(result);
            
            foreach (Card card in cards) 
                card.ToggleHoldText(false);
            
            yield return new WaitForSeconds(RestartDelay);
            
            ToggleResultText(false);
            
            SetupNewGame();
        }

        private PokerHands Result()
        {
            ResultCalculator calculator = new ResultCalculator(cards);
            return calculator.CalculateResult();
        }

        private void UpdateResultUI(PokerHands result)
        {
            resultText.text = result.ToString();
            ToggleResultText(true);
            UpdateUI?.Invoke();
        }

        private void ToggleResultText(bool state) => 
            resultText.gameObject.SetActive(state);
    }
}
