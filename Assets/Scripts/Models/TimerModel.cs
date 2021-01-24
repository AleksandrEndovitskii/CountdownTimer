using System;

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

        public TimerModel(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }
    }
}