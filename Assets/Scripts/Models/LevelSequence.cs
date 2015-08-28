namespace Assets.Scripts.Models
{
    public class LevelSequence
    {
        public string PrefabName;
        public string ConversationName;
        public string DisplayName;
        public string Image;

        public bool IsCutscene()
        {
            return !string.IsNullOrEmpty(ConversationName);
        }
    }
}