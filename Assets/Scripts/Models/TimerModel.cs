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

                if (_timeSpan == TimeSpan.Zero)
                {
                    StopTimer();
                }

                TimeSpanChanged.Invoke(_timeSpan);
            }
        }

        public bool IsStarted => _secondCountingCoroutine != null;

        private TimeSpan _timeSpan;

        private int _stepSecondsCount = 1;

        private Coroutine _secondCountingCoroutine;

        public TimerModel(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        public void StartTimer()
        {
            if (IsStarted)
            {
                return;
            }

            Debug.Log("Timer started");

            _secondCountingCoroutine = GameManager.Instance.StartCoroutine(SecondCounting());
        }
        public void StopTimer()
        {
            Debug.Log("Timer stopped");

            GameManager.Instance.StopCoroutine(_secondCountingCoroutine);
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