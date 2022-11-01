using Logic;
using Progress;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CashText : MonoBehaviour
    {
        [SerializeField] private PlayerProgress playerProgress;
        [SerializeField] private GameFlow gameFlow;
        
        [SerializeField] private TextMeshProUGUI text;

        private void Start() => 
            UpdateUI();

        private void OnEnable() => 
            gameFlow.UpdateUI += UpdateUI;

        private void OnDisable() =>
            gameFlow.UpdateUI -= UpdateUI;

        private void UpdateUI() => 
            text.text = playerProgress.Cash + "$";
    }
}