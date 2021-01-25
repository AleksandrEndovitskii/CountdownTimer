using Components.Buttons;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(TimerButtonView))]
    public class ShowTimerWindowButtonAfterAnimationButtonComponent : MonoBehaviour
    {
        private Button _button;
        private TimerButtonView _timerButtonView;
        private ButtonsHidingAnimationComponent _buttonsHidingAnimationComponent;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _timerButtonView = gameObject.GetComponent<TimerButtonView>();
        }
        private void Start()
        {
            _button.onClick.AddListener(ButtonOnClick);

            _buttonsHidingAnimationComponent = this.gameObject.transform.parent.gameObject.GetComponent<ButtonsHidingAnimationComponent>();

            if (_buttonsHidingAnimationComponent == null)
            {
                return;
            }

            _buttonsHidingAnimationComponent.OnCompleted += DoTweenAnimationOnComplete;
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);

            if (_buttonsHidingAnimationComponent == null)
            {
                return;
            }

            _buttonsHidingAnimationComponent.OnCompleted -= DoTweenAnimationOnComplete;
        }

        private void ButtonOnClick()
        {
            if (_buttonsHidingAnimationComponent == null)
            {
                return;
            }

            _buttonsHidingAnimationComponent.PlayAnimation();
        }

        private void DoTweenAnimationOnComplete()
        {
            GameManager.Instance.UserInterfaceManager.ShowTimerWindow(_timerButtonView.TimerModel);
        }
    }
}
