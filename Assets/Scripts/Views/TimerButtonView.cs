using System;
using Models;
using UnityEngine;

namespace Views
{
    public class TimerButtonView : MonoBehaviour
    {
        public Action<TimerModel> TimerModelsChanged = delegate { };

        public TimerModel TimerModel
        {
            get
            {
                return _timerModel;
            }
            set
            {
                if (value == _timerModel)
                {
                    return;
                }

                _timerModel = value;

                TimerModelsChanged.Invoke(_timerModel);
            }
        }

        private TimerModel _timerModel;

        public void SetModel(TimerModel timerModel)
        {
            TimerModel = timerModel;
        }
    }
}
