using UnityEngine;

namespace Assets.Scripts.PowerUps.Behaviors
{
    public class Sticky : Behavior
    {
        public override void ApplyBuffToPlayer(Player player)
        {
            var sphereCollider = player.GetComponent<SphereCollider>();
            sphereCollider.material.bounciness = 0;
        }

        public override void RemoveBuffFromPlayer(Player player)
        {
            var sphereCollider = player.GetComponent<SphereCollider>();
            sphereCollider.material.bounciness = 1;
        }
    }
}
