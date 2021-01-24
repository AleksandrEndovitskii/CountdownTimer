using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    [RequireComponent(typeof(Image))]
    public class IsTimerStartedImageColoringComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerButtonView _timerButtonView;
        [SerializeField]
        private Color _isTimerStartedColor = Color.white;
        [SerializeField]
        private Color _isTimerNotStartedColor = Color.yellow;

        private Image _image;

        private void Awake()
        {
            _image = this.gameObject.GetComponent<Image>();
        }
        private void Start()
        {
            _timerButtonView.TimerModel.IsStartedChanged += TimerModelOnIsStartedChanged;
            TimerModelOnIsStartedChanged(_timerButtonView.TimerModel.IsStarted);
        }
        private void OnDestroy()
        {
            _timerButtonView.TimerModel.IsStartedChanged -= TimerModelOnIsStartedChanged;
        }

        private void TimerModelOnIsStartedChanged(bool isStarted)
        {
            if (isStarted)
            {
                _image.color = _isTimerStartedColor;
            }
            else
            {
                _image.color = _isTimerNotStartedColor;
            }
        }
    }
}
