using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class Conversation
    {
        public List<Line> Lines;
        public int CurrentLine;

        public bool IsComplete
        {
            get { return CurrentLine >= Lines.Count; }
        }
    }
}
