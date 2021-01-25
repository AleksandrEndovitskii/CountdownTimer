using DG.Tweening;
using UnityEngine;
using Views;

namespace Components.Timer
{
    [RequireComponent(typeof(DOTweenAnimation))]
    public class IsTimerStartedChangedAnimationPlayingComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerButtonView _timerButtonView;

        private DOTweenAnimation _doTweenAnimation;

        private void Awake()
        {
            _doTweenAnimation = this.gameObject.GetComponent<DOTweenAnimation>();
        }
        private void Start()
        {
            _timerButtonView.TimerModel.IsStartedChanged += TimerModelOnIsStartedChanged;
        }
        private void OnDestroy()
        {
            _timerButtonView.TimerModel.IsStartedChanged -= TimerModelOnIsStartedChanged;
        }

        private void TimerModelOnIsStartedChanged(bool isStarted)
        {
            if (!isStarted)
            {
                _doTweenAnimation.DORestart();
            }
        }
    }
}
