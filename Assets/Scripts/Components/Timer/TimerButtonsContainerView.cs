using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;

namespace Components.Timer
{
    public class TimerButtonsContainerView : MonoBehaviour
    {
        [SerializeField]
        private TimerButtonView _timerButtonViewPrefab;

        private List<TimerButtonView> _timerButtonViewInstances = new List<TimerButtonView>();

        private TimersManager _timersManager;

        private void Awake()
        {
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
            foreach (var timerModel in _timersManager.TimerModels)
            {
                var timerButtonViewInstance = Instantiate(_timerButtonViewPrefab, this.gameObject.transform);
                timerButtonViewInstance.SetModel(timerModel);
                _timerButtonViewInstances.Add(timerButtonViewInstance);
            }
        }
    }
}
