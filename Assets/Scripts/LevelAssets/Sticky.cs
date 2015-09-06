using UnityEngine;

namespace Assets.Scripts.LevelAssets
{
    public class Sticky : MonoBehaviour
    {
        public Transform Platform;

        void OnCollisionEnter(Collision collision)
        {
            var col = collision.collider.transform;
            if (col.name == "Player")
            {
                col.SetParent(Platform);
            }
        }

        void OnCollisionExit(Collision collision)
        {
            var col = collision.collider.transform;
            if (col.name == "Player")
            {
                col.SetParent(null);
            }
        }
    }
}
