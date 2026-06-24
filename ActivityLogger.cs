// =============================================================
// ActivityLogger.cs
// Cybersecurity Awareness Chatbot - Activity Log Feature
// =============================================================
// Records all significant chatbot actions with timestamps.
// Stores entries in a List<string> and displays the last 10.
// Users can type "show activity log" to view the log.
//
// References:
// [13] Microsoft, 2024. List<T> Class [Online].
//      Available at: https://learn.microsoft.com/en-us/dotnet/
//      api/system.collections.generic.list-1
//      [Accessed 20 May 2026].
// =============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace CybersecurityChatbotGUI
{
    public class ActivityLogger
    {
        // List<string> stores all activity log entries with timestamps [13]
        // Using List as required by the brief (Part 3 — Activity Log)
        private readonly List<string> _log = new List<string>();

        // Maximum number of entries to display at once (brief says 5-10)
        private const int MaxDisplay = 10;

        // Adds a new entry to the activity log with a timestamp [13]
        public void Log(string action)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string entry = $"[{timestamp}] {action}";
            _log.Add(entry);
        }

        // Returns the last 10 log entries as a formatted string
        // Displayed when the user types "show activity log" or "what have you done"
        public string GetLog()
        {
            if (_log.Count == 0)
                return "No activity recorded yet. Start chatting, add tasks, or take the quiz!";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Here is a summary of recent actions:\n");

            // Show only the last MaxDisplay entries [Part 3 — Activity Log requirement]
            int start = Math.Max(0, _log.Count - MaxDisplay);
            int number = 1;

            for (int i = start; i < _log.Count; i++)
            {
                sb.AppendLine($"  {number}. {_log[i]}");
                number++;
            }

            if (_log.Count > MaxDisplay)
                sb.AppendLine($"\n  (Showing last {MaxDisplay} of {_log.Count} total actions)");

            return sb.ToString().TrimEnd();
        }

        // Returns total number of logged actions
        public int GetCount() => _log.Count;

    }
}
