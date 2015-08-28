namespace Assets.Scripts.PowerUps.Behaviors
{
    public class SpeedUp : Behavior
    {
        public int Intensity = 2;
        public override void ApplyBuffToPlayer(Player player)
        {
            player.MaxSpeed *= Intensity;
            player.Acceleration *= Intensity;
        }

        public override void RemoveBuffFromPlayer(Player player)
        {
            player.MaxSpeed /= Intensity;
            player.Acceleration /= Intensity;
        }
    }
}