using UnityEngine;

namespace pixelflag.DummyBox
{
    public class DummyBox : MonoBehaviour
    {
        void Awake()
        {
            Destroy(gameObject);
        }
    }
}