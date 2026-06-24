// =============================================================
// ConversationManager.cs
// Cybersecurity Awareness Chatbot - Conversation Flow
// =============================================================
// Manages follow-up conversation flow. Carried over from Part 2.
//
// References:
// [13] Microsoft, 2024. List<T> Class [Online].
//      Available at: https://learn.microsoft.com/en-us/dotnet/
//      api/system.collections.generic.list-1
//      [Accessed 20 May 2026].
// =============================================================

using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class ConversationManager
    {
        private string _lastTopic = "";
        private string _followUpResponse = "";

        private readonly List<string> _followUpTriggers = new List<string>
        {
            "tell me more", "more info", "explain more", "another tip",
            "give me another", "more details", "elaborate", "continue",
            "what else", "keep going", "more please", "say more",
            "expand on that", "go on", "and then", "what about"
        };

        public void SetLastTopic(string topic, string? followUp)
        {
            _lastTopic = topic;
            _followUpResponse = followUp ?? "";
        }

        public bool IsFollowUp(string input)
        {
            string lowered = input.ToLower().Trim();
            foreach (string trigger in _followUpTriggers)
                if (lowered.Contains(trigger)) return true;
            return false;
        }

        public string? GetFollowUp()
        {
            if (string.IsNullOrEmpty(_followUpResponse)) return null;
            return _followUpResponse;
        }

        public string GetLastTopic() => _lastTopic;
    }
}
