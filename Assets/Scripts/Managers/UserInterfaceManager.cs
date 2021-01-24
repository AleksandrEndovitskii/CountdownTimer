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
        private RectTransform _buttonsWindowsPrefab;
        [SerializeField]
        private TimerWindowView _timerWindowViewPrefab;

        private Canvas _userInterfaceCanvasInstance;
        private RectTransform _buttonsWindowsInstance;

        public void Initialize()
        {
            _userInterfaceCanvasInstance = Instantiate(_userInterfaceCanvasPrefab);

            _buttonsWindowsInstance = Instantiate(_buttonsWindowsPrefab, _userInterfaceCanvasInstance.gameObject.transform);
        }
        public void UnInitialize()
        {

        }

        public void ShowTimerWindow(TimerModel timerModel)
        {
            var timerWindowViewInstance = Instantiate(_timerWindowViewPrefab, _userInterfaceCanvasInstance.gameObject.transform);
            timerWindowViewInstance.SetModel(timerModel);
        }
    }
}
