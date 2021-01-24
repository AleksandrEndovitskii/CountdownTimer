using System;

namespace Models
{
    public class TimerModel
    {
        public TimeSpan TimeSpan { get; private set; }

        public TimerModel(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }
    }
}