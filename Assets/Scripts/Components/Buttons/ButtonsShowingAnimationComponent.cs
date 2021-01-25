using System;
using System.Collections;
using System.Collections.Generic;
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
        private int _stepBetweenAnimationsPlayingSecondsCount;

        private CustomVerticalLayoutGroupView _customVerticalLayoutGroupView;

        private List<Coroutine> _coroutines = new List<Coroutine>();

        private void Awake()
        {
            _customVerticalLayoutGroupView = this.gameObject.GetComponent<CustomVerticalLayoutGroupView>();
        }
        private void Start()
        {
            _customVerticalLayoutGroupView.ContentChanged += OnLayoutChanged;
            OnLayoutChanged(_customVerticalLayoutGroupView.Content);
        }
        private void OnDestroy()
        {
            _customVerticalLayoutGroupView.ContentChanged -= OnLayoutChanged;
        }

        private void StopAnimations()
        {
            foreach (var coroutine in _coroutines)
            {
                StopCoroutine(coroutine);
            }
            _coroutines.Clear();
        }
        private void StartAnimations(List<RectTransform> rectTransforms)
        {
            for (var i = 0; i < rectTransforms.Count; i++)
            {
                var rectTransform = rectTransforms[i];

                var initialAnchoredPositionX = rectTransform.anchoredPosition.x;

                var layoutElement = rectTransform.gameObject.GetComponent<LayoutElement>();
                rectTransform.anchoredPosition = new Vector2(0 - layoutElement.preferredWidth,
                    rectTransform.anchoredPosition.y);

                var performActionAfterDelaySecondsCountCoroutine = StartCoroutine(
                    PerformActionAfterDelaySecondsCountCoroutine(
                        i * _stepBetweenAnimationsPlayingSecondsCount,
                        () =>
                        {
                            rectTransform.DOLocalMoveX(
                                initialAnchoredPositionX, _stepBetweenAnimationsPlayingSecondsCount);
                        }));
                _coroutines.Add(performActionAfterDelaySecondsCountCoroutine);
            }
        }

        private void OnLayoutChanged(List<RectTransform> rectTransforms)
        {
            StopAnimations();
            StartAnimations(rectTransforms);
        }

        private IEnumerator PerformActionAfterDelaySecondsCountCoroutine(int delaySecondsCount, Action action)
        {
            yield return new WaitForSeconds(delaySecondsCount);

            action?.Invoke();
        }
        private IEnumerator PerformActionAfterDelayFramesCountCoroutine(int delayFramesCount, Action action)
        {
            yield return new WaitForFrames(delayFramesCount);

            action?.Invoke();
        }
    }
}
