using System.Collections.Generic;
using System.Linq;
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
            _timersManager.TimerModelsChanged += OnTimerModelsChanged;
            OnTimerModelsChanged(_timersManager.TimerModels);
        }
        private void OnDestroy()
        {
            _timersManager.TimerModelsChanged -= OnTimerModelsChanged;
        }

        private void OnTimerModelsChanged(List<TimerModel> timerModels)
        {
            ClearContent();
            CreateContent();
        }

        private void ClearContent()
        {
            foreach (var timerButtonViewInstance in _timerButtonViewInstances)
            {
                Destroy(timerButtonViewInstance.gameObject);
            }
            _timerButtonViewInstances.Clear();

            _customVerticalLayoutGroupView.ClearContent();
        }
        private void CreateContent()
        {
            foreach (var timerModel in _timersManager.TimerModels)
            {
                var timerButtonViewInstance = Instantiate(_timerButtonViewPrefab);
                timerButtonViewInstance.SetModel(timerModel);
                _timerButtonViewInstances.Add(timerButtonViewInstance);
            }

            var rectTransforms = _timerButtonViewInstances
                .Select(x => x.gameObject.GetComponent<RectTransform>()).ToList();
            _customVerticalLayoutGroupView.CreateContent(rectTransforms);
        }
    }
}
