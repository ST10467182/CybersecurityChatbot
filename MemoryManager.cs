// =============================================================
// MemoryManager.cs
// Cybersecurity Awareness Chatbot - Memory and Recall
// =============================================================
// Stores user session data. Carried over from Part 2.
//
// References:
// [12] Microsoft, 2024. Dictionary<TKey,TValue> Class [Online].
//      Available at: https://learn.microsoft.com/en-us/dotnet/
//      api/system.collections.generic.dictionary-2
//      [Accessed 14 April 2026].
// =============================================================

using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class MemoryManager
    {
        private readonly Dictionary<string, string> _memory;

        public MemoryManager()
        {
            _memory = new Dictionary<string, string>
            {
                ["name"] = "User",
                ["favourite_topic"] = "",
                ["last_topic"] = ""
            };
        }

        public void SetUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                string formatted = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                _memory["name"] = formatted;
            }
        }

        public string GetUserName() =>
            _memory.ContainsKey("name") ? _memory["name"] : "User";

        public void SetFavouriteTopic(string topic)
        {
            if (!string.IsNullOrWhiteSpace(topic))
            {
                _memory["last_topic"] = _memory.ContainsKey("favourite_topic")
                    ? _memory["favourite_topic"] : "";
                _memory["favourite_topic"] = topic;
            }
        }

        public string GetFavouriteTopic() =>
            _memory.ContainsKey("favourite_topic") ? _memory["favourite_topic"] : "";

        public string Recall()
        {
            string name = GetUserName();
            string topic = GetFavouriteTopic();
            string result = $"Here's what I remember about you, {name}:\n\n";
            result += $"     • Your name: {name}\n";
            result += string.IsNullOrEmpty(topic)
                ? "     • You haven't asked about a specific topic yet.\n"
                : $"     • Most recent topic of interest: {topic}\n";
            result += "\n  Feel free to keep asking — I'm here to help!";
            return result;
        }
    }
}
