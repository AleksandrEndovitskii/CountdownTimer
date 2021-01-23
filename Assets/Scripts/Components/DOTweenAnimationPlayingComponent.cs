using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Components
{
    public class DOTweenAnimationPlayingComponent : MonoBehaviour
    {
        [SerializeField]
        private string _dOTweenAnimationId;
        [SerializeField]
        private int _stepBetweenAnimationsPlayingSecondsCount;
        [SerializeField]
        private List<DOTweenAnimation> _doTweenAnimations = new List<DOTweenAnimation>();

        private void Awake()
        {
            _doTweenAnimations = this.gameObject.GetComponentsInChildren<DOTweenAnimation>().ToList();
            _doTweenAnimations = _doTweenAnimations.Where(x => x.id == _dOTweenAnimationId).ToList();
        }
        private void Start()
        {
            for (var i = 0; i < _doTweenAnimations.Count; i++)
            {
                var doTweenAnimation = _doTweenAnimations[i];
                StartCoroutine(PerformActionAfterDelaySecondsCountCoroutine(i * _stepBetweenAnimationsPlayingSecondsCount, () =>
                {
                    doTweenAnimation.DORestart();
                }));
            }
        }

        private IEnumerator PerformActionAfterDelaySecondsCountCoroutine(int delaySecondsCount, Action action)
        {
            yield return new WaitForSeconds(delaySecondsCount);

            action?.Invoke();
        }
    }
}
