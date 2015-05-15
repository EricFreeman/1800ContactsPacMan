using UnityEngine;

namespace Assets.Scripts
{
    public class TrackObject : MonoBehaviour
    {
        public GameObject ObjectToTrack;

        void Update()
        {
            transform.position = ObjectToTrack.transform.position;
        }
    }
}