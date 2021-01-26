using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class TimersManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        public Action<TimerModel> TimerModelAdded = delegate { };

        public List<TimerModel> TimerModels = new List<TimerModel>();

        private int _timersCount = 3;

        public void Initialize()
        {
            CreateDemoTimerModels();
        }
        public void UnInitialize()
        {

        }

        public void Add(TimerModel timerModel)
        {
            TimerModels.Add(timerModel);

            TimerModelAdded.Invoke(timerModel);
        }

        public void CreateTimerModel(TimeSpan timeSpan)
        {
            var id = TimerModels.Count + 1;
            var timerModel = new TimerModel(id, timeSpan);
            TimerModels.Add(timerModel);

            TimerModelAdded.Invoke(timerModel);
        }
        private void CreateDemoTimerModels()
        {
            for (var i = 0; i < _timersCount; i++)
            {
                var timeSpan = new TimeSpan(0, i + 1, 0);
                CreateTimerModel(timeSpan);
            }
        }
    }
}
