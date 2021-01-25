using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CustomVerticalLayoutGroupView))]
    public class TimerButtonViewInstantiatingComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerButtonView _timerButtonViewPrefab;

        private List<TimerButtonView> _timerButtonViewInstances = new List<TimerButtonView>();

        private CustomVerticalLayoutGroupView _customVerticalLayoutGroupView;

        private TimersManager _timersManager;

        private void Awake()
        {
            _customVerticalLayoutGroupView = this.gameObject.GetComponent<CustomVerticalLayoutGroupView>();
            _timersManager = GameManager.Instance.TimersManager;
        }
        private void Start()
        {
            _timersManager.TimerModelAdded += OnTimerModelAdded;
            foreach (var timerModel in _timersManager.TimerModels)
            {
                OnTimerModelAdded(timerModel);
            }
        }
        private void OnDestroy()
        {
            _timersManager.TimerModelAdded -= OnTimerModelAdded;
        }

        private void OnTimerModelAdded(TimerModel timerModel)
        {
            AddElement(timerModel);
        }

        private void AddElement(TimerModel timerModel)
        {
            var timerButtonViewInstance = InstantiateElement(timerModel);
            _timerButtonViewInstances.Add(timerButtonViewInstance);
            _customVerticalLayoutGroupView.AddElement(
                timerButtonViewInstance.gameObject.GetComponent<RectTransform>());
        }

        private TimerButtonView InstantiateElement(TimerModel timerModel)
        {
            var timerButtonViewInstance = Instantiate(_timerButtonViewPrefab);
            timerButtonViewInstance.SetModel(timerModel);
            return timerButtonViewInstance;
        }
    }
}
