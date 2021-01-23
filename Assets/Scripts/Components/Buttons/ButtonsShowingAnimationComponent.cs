using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Buttons
{
    public class ButtonsShowingAnimationComponent : MonoBehaviour
    {
        [SerializeField]
        private int _stepBetweenAnimationsPlayingSecondsCount;
        [SerializeField]
        private List<Button> _buttons = new List<Button>();

        private void Awake()
        {
            _buttons = this.gameObject.GetComponentsInChildren<Button>().ToList();
        }
        private void Start()
        {
            StartCoroutine(PerformActionAfterDelayFramesCountCoroutine(1, () =>
            {
                for (var i = 0; i < _buttons.Count; i++)
                {
                    var rectTransform = _buttons[i].GetComponent<RectTransform>();

                    var initialAnchoredPositionX = rectTransform.anchoredPosition.x;

                    var layoutElement = rectTransform.gameObject.GetComponent<LayoutElement>();
                    rectTransform.anchoredPosition = new Vector2(0 - layoutElement.preferredWidth,
                        rectTransform.anchoredPosition.y);

                    StartCoroutine(PerformActionAfterDelaySecondsCountCoroutine(
                        i * _stepBetweenAnimationsPlayingSecondsCount,
                        () =>
                        {
                            rectTransform.DOLocalMoveX(
                                initialAnchoredPositionX, _stepBetweenAnimationsPlayingSecondsCount);
                        }));
                }
            }));
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
