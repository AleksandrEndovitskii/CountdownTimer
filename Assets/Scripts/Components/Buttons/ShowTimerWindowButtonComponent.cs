using Managers;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(TimerButtonView))]
    public class ShowTimerWindowButtonComponent : MonoBehaviour
    {
        private Button _button;
        private TimerButtonView _timerButtonView;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _timerButtonView = gameObject.GetComponent<TimerButtonView>();

            _button.onClick.AddListener(ButtonOnClick);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            GameManager.Instance.UserInterfaceManager.ShowTimerWindow(_timerButtonView.TimerModel);
        }
    }
}
