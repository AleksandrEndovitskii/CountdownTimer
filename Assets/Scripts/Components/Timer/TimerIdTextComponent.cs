using Models;
using TMPro;
using UnityEngine;
using Views;

namespace Components.Timer
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerIdTextComponent : MonoBehaviour
    {
        [SerializeField]
        private string _prefix;
        [SerializeField]
        private TimerButtonView _timerButtonView;

        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            _timerButtonView.TimerModelChanged += OnTimerModelChanged;
            OnTimerModelChanged(_timerButtonView.TimerModel);
        }
        private void OnDestroy()
        {
            _timerButtonView.TimerModelChanged -= OnTimerModelChanged;
        }

        private void OnTimerModelChanged(TimerModel timerModel)
        {
            if (timerModel == null)
            {
                return;
            }

            _textMeshPro.text = _prefix + timerModel.Id;
        }
    }
}
