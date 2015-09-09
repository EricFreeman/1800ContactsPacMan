using System;

namespace Assets.Scripts.PowerUps.Behaviors
{
    public abstract class Behavior
    {
        public DateTime _timeStamp = DateTime.Now;
        public DateTime TimeStamp { get { return _timeStamp; } }
        public float Duration = 30;

        public abstract void ApplyBuffToPlayer(Player player);
        public abstract void RemoveBuffFromPlayer(Player player);
    }
}