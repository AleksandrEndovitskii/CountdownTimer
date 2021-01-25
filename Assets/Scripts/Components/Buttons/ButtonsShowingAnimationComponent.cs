using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components.Buttons
{
    [RequireComponent(typeof(CustomVerticalLayoutGroupView))]
    public class ButtonsShowingAnimationComponent : MonoBehaviour
    {
        [SerializeField]
        private int _stepBetweenAnimationsPlayingSecondsCount = 1;

        private CustomVerticalLayoutGroupView _customVerticalLayoutGroupView;

        private DateTime _lastAnimationStartDateTime = DateTime.MinValue;

        private void Awake()
        {
            _customVerticalLayoutGroupView = this.gameObject.GetComponent<CustomVerticalLayoutGroupView>();
        }
        private void Start()
        {
            _customVerticalLayoutGroupView.ElementAdded += OnElementAdded;
            foreach (var rectTransform in _customVerticalLayoutGroupView.Content)
            {
                OnElementAdded(rectTransform);
            }
        }
        private void OnDestroy()
        {
            _customVerticalLayoutGroupView.ElementAdded -= OnElementAdded;
        }

        private void OnElementAdded(RectTransform rectTransform)
        {
            StartAnimation(rectTransform);
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
                        rectTransform.DOLocalMoveX(
                            initialAnchoredPositionX, _stepBetweenAnimationsPlayingSecondsCount);
                    }));

            return performActionAfterDelaySecondsCountCoroutine;
        }

        private IEnumerator PerformActionAfterDelaySecondsCountCoroutine(float delaySecondsCount, Action action)
        {
            yield return new WaitForSeconds(delaySecondsCount);

            action?.Invoke();
        }
    }
}
