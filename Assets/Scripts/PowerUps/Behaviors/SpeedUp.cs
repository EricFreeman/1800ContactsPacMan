namespace Assets.Scripts.PowerUps.Behaviors
{
    public class SpeedUp : Behavior
    {
        public int Intensity = 10;
        public override void ApplyBuffToPlayer(Player player)
        {
            player.Speed += Intensity;
        }

        public override void RemoveBuffFromPlayer(Player player)
        {
            player.Speed -= Intensity;
        }
    }
}