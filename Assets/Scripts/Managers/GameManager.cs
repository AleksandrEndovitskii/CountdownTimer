using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
                    Destroy(gameObject);
                }
            }

            Initialize();
        }
        private void OnDestroy()
        {
            UnInitialize();
        }

        public void Initialize()
        {

        }
        public void UnInitialize()
        {

        }
    }
}
