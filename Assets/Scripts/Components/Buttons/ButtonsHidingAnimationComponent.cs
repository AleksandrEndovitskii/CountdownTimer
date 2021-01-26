using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components.Buttons
{
    [RequireComponent(typeof(CustomVerticalLayoutGroupView))]
    public class ButtonsHidingAnimationComponent : MonoBehaviour
    {
        public event Action OnCompleted = delegate { };

        [SerializeField]
        private int _stepBetweenAnimationsPlayingSecondsCount = 1;

        private CustomVerticalLayoutGroupView _customVerticalLayoutGroupView;

        private DateTime _lastAnimationStartDateTime = DateTime.MinValue;
        private int _complitedAnimationsCount;

        private void Awake()
        {
            _customVerticalLayoutGroupView = this.gameObject.GetComponent<CustomVerticalLayoutGroupView>();
        }

        public void PlayAnimation()
        {
            _complitedAnimationsCount = 0;
            foreach (var rectTransform in _customVerticalLayoutGroupView.Content)
            {
                StartAnimation(rectTransform);
            }
        }

        private Coroutine StartAnimation(RectTransform rectTransform)
        {
            var initialAnchoredPositionX = rectTransform.anchoredPosition.x;

            var layoutElement = rectTransform.gameObject.GetComponent<LayoutElement>();
            rectTransform.anchoredPosition = new Vector2(0 - layoutElement.preferredWidth,
                rectTransform.anchoredPosition.y);

            if (_lastAnimationStartDateTime == DateTime.MinValue)
            {
                _lastAnimationStartDateTime = DateTime.Now.AddSeconds(-1);
            }
            var fromLastAnimationStartSecondsCount = Math.Abs((float)(DateTime.Now - _lastAnimationStartDateTime).TotalSeconds);
            if (fromLastAnimationStartSecondsCount < _stepBetweenAnimationsPlayingSecondsCount)
            {
                _lastAnimationStartDateTime = _lastAnimationStartDateTime.AddSeconds(
                    _stepBetweenAnimationsPlayingSecondsCount);
            }
            else
            {
                _lastAnimationStartDateTime = DateTime.Now;
            }

            var animationStartDelaySecondsCount = Math.Abs((float)(DateTime.Now - _lastAnimationStartDateTime).TotalSeconds);
            var performActionAfterDelaySecondsCountCoroutine = StartCoroutine(
                PerformActionAfterDelaySecondsCountCoroutine(animationStartDelaySecondsCount,
                    () =>
                    {
                        var tweenerCore = rectTransform.DOLocalMoveX(
                            initialAnchoredPositionX, _stepBetweenAnimationsPlayingSecondsCount).
                            From();
                        tweenerCore.onComplete += AnimationOnComplete;
                    }));

            return performActionAfterDelaySecondsCountCoroutine;
        }

        private void AnimationOnComplete()
        {
            _complitedAnimationsCount++;

            if (_complitedAnimationsCount == _customVerticalLayoutGroupView.Content.Count)
            {
                Debug.Log($"Hiding animation for [{_complitedAnimationsCount}] elements completed");

                OnCompleted.Invoke();
            }
        }

        private IEnumerator PerformActionAfterDelaySecondsCountCoroutine(float delaySecondsCount, Action action)
        {
            yield return new WaitForSeconds(delaySecondsCount);

            action?.Invoke();
        }
    }
}
