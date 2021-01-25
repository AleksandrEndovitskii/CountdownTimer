using System;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Timer
{
    [RequireComponent(typeof(Button))]
    public class AddTimerButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private int _secondsCount;

        private Button _button;
        private TimersManager _timersManager;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();

            _timersManager = GameManager.Instance.TimersManager;
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
            var timeSpan = TimeSpan.FromSeconds(_secondsCount);
            var timerModel = new TimerModel(timeSpan);
            _timersManager.Add(timerModel);
        }
    }
}
