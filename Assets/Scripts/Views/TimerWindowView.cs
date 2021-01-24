using System;
using Models;
using UnityEngine;

namespace Views
{
    public class TimerWindowView : MonoBehaviour
    {
        public Action<TimerModel> TimerModelChanged = delegate { };

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

                TimerModelChanged.Invoke(_timerModel);
            }
        }

        private TimerModel _timerModel;

        public void SetModel(TimerModel timerModel)
        {
            TimerModel = timerModel;
        }
    }
}
