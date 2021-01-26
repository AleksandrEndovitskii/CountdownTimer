using Models;
using UnityEngine;
using Utils;
using Views;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private Canvas _userInterfaceCanvasPrefab;
        [SerializeField]
        private RectTransform _timerButtonsWindowPrefab;
        [SerializeField]
        private TimerWindowView _timerWindowViewPrefab;

        private Canvas _userInterfaceCanvasInstance;
        private RectTransform _currentWindowInstance;

        public void Initialize()
        {
            _userInterfaceCanvasInstance = Instantiate(_userInterfaceCanvasPrefab);
        }
        public void UnInitialize()
        {

        }

        public void ShowTimerButtonsWindow()
        {
            CloseCurrentWindow();

            _currentWindowInstance = Instantiate(_timerButtonsWindowPrefab, _userInterfaceCanvasInstance.gameObject.transform);
        }
        public void ShowTimerWindow(TimerModel timerModel)
        {
            Debug.Log($"Timer window has been shown for timer with id [{timerModel.Id}]");

            CloseCurrentWindow();

            var timerWindowViewInstance = Instantiate(_timerWindowViewPrefab, _userInterfaceCanvasInstance.gameObject.transform);
            timerWindowViewInstance.SetModel(timerModel);

            _currentWindowInstance = timerWindowViewInstance.gameObject.GetComponent<RectTransform>();
        }
        public void CloseCurrentWindow()
        {
            if (_currentWindowInstance != null)
            {
                Destroy(_currentWindowInstance.gameObject);
            }
            _currentWindowInstance = null;
        }
    }
}
