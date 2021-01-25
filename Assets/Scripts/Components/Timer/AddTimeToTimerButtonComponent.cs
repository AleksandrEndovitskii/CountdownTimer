using System;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components.Timer
{
    [RequireComponent(typeof(Button))]
    public class AddTimeToTimerButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerWindowView _timerWindowView;

        [SerializeField]
        private int _stepSecondsCount;

        private Button _button;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
        }
        private void Start()
        {
            _button.onClick.AddListener(ButtonOnClick);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            var timeSpan = new TimeSpan(0, 0, _stepSecondsCount);
            _timerWindowView.TimerModel.TimeSpan += timeSpan;
        }
    }
}
