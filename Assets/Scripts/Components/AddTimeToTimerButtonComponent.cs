using System;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class AddTimeToTimerButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerWindowView _timerWindowView;

        [SerializeField]
        private TimeSpan _timeSpan = new TimeSpan(0, 0, 1);

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
            _timerWindowView.TimerModel.TimeSpan += _timeSpan;
        }
    }
}
