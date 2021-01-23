using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class VerticalLayoutGroupDisablingComponent : MonoBehaviour
    {
        [SerializeField]
        private int _delayFramesCount;

        private VerticalLayoutGroup _verticalLayoutGroup;

        private void Awake()
        {
            _verticalLayoutGroup = this.gameObject.GetComponent<VerticalLayoutGroup>();
        }

        private void Start()
        {
            StartCoroutine(PerformActionAfterDelayFramesCountCoroutine(1, () =>
            {
                _verticalLayoutGroup.enabled = false;
            }));
        }

        private IEnumerator PerformActionAfterDelayFramesCountCoroutine(int delayFramesCount, Action action)
        {
            yield return new WaitForFrames(delayFramesCount);

            action?.Invoke();
        }
    }
}
