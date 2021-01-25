using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class TimersManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        public Action<List<TimerModel>> TimerModelsChanged = delegate { };

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

            TimerModelsChanged.Invoke(TimerModels);
        }

        private void CreateDemoTimerModels()
        {
            for (var i = 0; i < _timersCount; i++)
            {
                var timerModel = new TimerModel(new TimeSpan(0, i + 1, 0));
                Add(timerModel);
            }
        }
    }
}
