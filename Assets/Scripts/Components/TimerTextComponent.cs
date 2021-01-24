using System;
using TMPro;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerTextComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerWindowView _timerWindowView;

        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            _timerWindowView.TimerModel.TimeSpanChanged += OnTimeSpanChanged;
            OnTimeSpanChanged(_timerWindowView.TimerModel.TimeSpan);
        }
        private void OnDestroy()
        {
            _timerWindowView.TimerModel.TimeSpanChanged -= OnTimeSpanChanged;
        }

        private void OnTimeSpanChanged(TimeSpan timeSpan)
        {
            _textMeshPro.text = timeSpan.ToString();
        }
    }
}
