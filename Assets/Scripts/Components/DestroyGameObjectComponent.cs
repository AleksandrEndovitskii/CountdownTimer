using UnityEngine;

namespace Components
{
    public class DestroyGameObjectComponent : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}
