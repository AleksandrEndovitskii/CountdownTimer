using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Models
{
    public class TimerModel
    {
        public Action<bool> IsStartedChanged = delegate {  };
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
                    IsStarted = false;
                }

                TimeSpanChanged.Invoke(_timeSpan);
            }
        }

        public bool IsStarted
        {
            get
            {
                return _secondCountingCoroutine != null;
            }
            set
            {
                if (value == true)
                {
                    if (IsStarted)
                    {
                        return;
                    }

                    _secondCountingCoroutine = GameManager.Instance.StartCoroutine(SecondCounting());

                    Debug.Log("Timer started");
                    IsStartedChanged.Invoke(IsStarted);
                }
                else
                {
                    if (!IsStarted)
                    {
                        return;
                    }

                    GameManager.Instance.StopCoroutine(_secondCountingCoroutine);
                    _secondCountingCoroutine = null;

                    Debug.Log("Timer stopped");
                    IsStartedChanged.Invoke(IsStarted);
                }
            }
        }

        public readonly int Id;

        private TimeSpan _timeSpan;

        private int _stepSecondsCount = 1;

        private Coroutine _secondCountingCoroutine;

        public TimerModel(int id, TimeSpan timeSpan)
        {
            Id = id;
            TimeSpan = timeSpan;
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