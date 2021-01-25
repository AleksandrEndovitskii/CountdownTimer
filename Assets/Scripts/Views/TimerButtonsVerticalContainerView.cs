using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class TimerButtonsVerticalContainerView : MonoBehaviour
    {
        [SerializeField]
        private TimerButtonView _timerButtonViewPrefab;

        private VerticalLayoutGroup _verticalLayoutGroup;

        private List<TimerButtonView> _timerButtonViewInstances = new List<TimerButtonView>();

        private TimersManager _timersManager;

        private int _verticalLayoutGroupUpdateDurationFramesCount = 3;

        private void Awake()
        {
            _verticalLayoutGroup = this.gameObject.GetComponent<VerticalLayoutGroup>();

            _timersManager = GameManager.Instance.TimersManager;
        }
        private void Start()
        {
            _timersManager.TimerModelsChanged += OnTimerModelsChanged;
            OnTimerModelsChanged(_timersManager.TimerModels);
        }
        private void OnDestroy()
        {
            _timersManager.TimerModelsChanged -= OnTimerModelsChanged;
        }

        private void OnTimerModelsChanged(List<TimerModel> timerModels)
        {
            Clear();
            Create();
        }

        private void Clear()
        {
            foreach (var timerButtonViewInstance in _timerButtonViewInstances)
            {
                Destroy(timerButtonViewInstance.gameObject);
            }

            _timerButtonViewInstances.Clear();
        }
        private void Create()
        {
            _verticalLayoutGroup.enabled = true;

            foreach (var timerModel in _timersManager.TimerModels)
            {
                var timerButtonViewInstance = Instantiate(_timerButtonViewPrefab, this.gameObject.transform);
                timerButtonViewInstance.SetModel(timerModel);
                _timerButtonViewInstances.Add(timerButtonViewInstance);
            }

            StartCoroutine(PerformActionAfterDelayFramesCountCoroutine(_verticalLayoutGroupUpdateDurationFramesCount, () =>
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
