using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Models
{
    public class TimerModel
    {
        public Action<TimeSpan> TimeSpanChanged = delegate {  };

        public TimeSpan TimeSpan
        {
            get
            {
                return _timeSpan;
            }
            set
            {
                if (value == _timeSpan)
                {
                    return;
                }

                _timeSpan = value;

                TimeSpanChanged.Invoke(_timeSpan);
            }
        }

        private TimeSpan _timeSpan;

        private int _stepSecondsCount = 1;

        private bool _isStarted = false;

        public TimerModel(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        public void StartTimer()
        {
            if (_isStarted)
            {
                return;
            }

            _isStarted = true;

            GameManager.Instance.StartCoroutine(SecondCounting());
        }

        private IEnumerator SecondCounting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_stepSecondsCount);

                var timeSpan = TimeSpan.FromSeconds(_stepSecondsCount);
                TimeSpan -= timeSpan;
            }
        }
    }
}