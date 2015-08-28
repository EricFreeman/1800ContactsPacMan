namespace Assets.Scripts.Models
{
    public class LevelSequence
    {
        public string PrefabName;
        public string ConversationName;
        public string DisplayName;

        public bool IsCutscene()
        {
            return string.IsNullOrEmpty(PrefabName);
        }
    }
}