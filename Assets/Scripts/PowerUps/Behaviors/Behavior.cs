using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps.Behaviors
{
    public abstract class Behavior
    {
        public DateTime _timeStamp = DateTime.Now;
        public DateTime TimeStamp { get { return _timeStamp; } }
        public abstract void ApplyBuffToPlayer(Player player);
        public abstract void RemoveBuffFromPlayer(Player player);
    }
}