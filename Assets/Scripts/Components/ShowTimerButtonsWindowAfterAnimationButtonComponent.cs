using System.Linq;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class ShowTimerButtonsWindowAfterAnimationButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private string _animationId;
        [SerializeField]
        private GameObject _targetGameObject;

        private Button _button;
        private DOTweenAnimation _doTweenAnimation;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();

            _doTweenAnimation = _targetGameObject.GetComponents<DOTweenAnimation>()
                .FirstOrDefault(x => x.id == _animationId);
        }
        private void Start()
        {
            _button.onClick.AddListener(ButtonOnClick);

            if (_doTweenAnimation == null)
            {
                return;
            }

            _doTweenAnimation.onComplete.AddListener(DoTweenAnimationOnComplete);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);

            if (_doTweenAnimation == null)
            {
                return;
            }

            _doTweenAnimation.onComplete.RemoveListener(DoTweenAnimationOnComplete);
        }

        private void ButtonOnClick()
        {
            if (_doTweenAnimation == null)
            {
                return;
            }

            _doTweenAnimation.DORestart();
        }

        private void DoTweenAnimationOnComplete()
        {
            GameManager.Instance.UserInterfaceManager.ShowTimerButtonsWindow();
        }
    }
}
