using UnityEngine;

namespace Assets.Scripts
{
	public class MovingPlatform : MonoBehaviour
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;
        public float PlatformSpeed;
		public float IdleSeconds;

		private MovingPlatformState _movingPlatformState = MovingPlatformState.IdlingAtStartPosition;
		private float _timeToStopIdling;

		void Start()
		{
			_timeToStopIdling = Time.fixedTime + IdleSeconds;
		}

        void Update()
        {
	        switch (_movingPlatformState)
	        {
		        case MovingPlatformState.IdlingAtStartPosition:
			        if (Time.fixedTime > _timeToStopIdling)
			        {
				        _movingPlatformState = MovingPlatformState.MovingToEndPosition;
			        }
			        break;
				case MovingPlatformState.IdlingAtEndPosition:
					if (Time.fixedTime > _timeToStopIdling)
					{
						_movingPlatformState = MovingPlatformState.MovingToStartPosition;
					}
			        break;
				case MovingPlatformState.MovingToEndPosition:
					transform.position = Vector3.MoveTowards(transform.position, EndPosition, PlatformSpeed * Time.deltaTime);
					if (transform.position == EndPosition)
					{
						_movingPlatformState = MovingPlatformState.IdlingAtEndPosition;
						_timeToStopIdling = Time.fixedTime + IdleSeconds;
					}
			        break;
				case MovingPlatformState.MovingToStartPosition:
					transform.position = Vector3.MoveTowards(transform.position, StartPosition, PlatformSpeed * Time.deltaTime);
					if (transform.position == StartPosition)
					{
						_movingPlatformState = MovingPlatformState.IdlingAtStartPosition;
						_timeToStopIdling = Time.fixedTime + IdleSeconds;
					}
			        break;
	        }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(StartPosition, EndPosition);
            Gizmos.DrawCube(StartPosition, new Vector3(4, 1, 3));
            Gizmos.DrawCube(EndPosition, new Vector3(4, 1, 3));
        }
    }

	public enum MovingPlatformState
	{
		IdlingAtStartPosition,
		MovingToEndPosition,
		MovingToStartPosition,
		IdlingAtEndPosition
	}
}