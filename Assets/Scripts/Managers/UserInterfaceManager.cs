using UnityEngine;
using Utils;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private Canvas _userInterfaceCanvasPrefab;
        [SerializeField]
        private RectTransform _buttonsWindowsPrefab;

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
    }
}
