// =============================================================
// CyberTask.cs
// Cybersecurity Awareness Chatbot - Task Model
// =============================================================
// Represents a single cybersecurity task created by the user.
// Each task has a title, description, optional reminder, and
// a completion status. Tasks are stored in MySQL.
//
// References:
// [14] Microsoft, 2024. Classes and Objects (C# Programming
//      Guide) [Online]. Available at: https://learn.microsoft.
//      com/en-us/dotnet/csharp/fundamentals/types/classes
//      [Accessed 20 May 2026].
// =============================================================

using System;

namespace CybersecurityChatbotGUI
{
    // Model class representing a single cybersecurity task [14]
    public class CyberTask
    {
        // Auto-implemented properties as required by the brief
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Reminder { get; set; } = "";
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Parameterless constructor required for object initialisation [14]
        public CyberTask() { }

        // Convenience constructor for creating tasks quickly
        public CyberTask(string title, string description, string reminder = "")
        {
            Title = title;
            Description = description;
            Reminder = reminder;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
        }

        // Returns a formatted summary string for display in the UI
        public override string ToString()
        {
            string status = IsCompleted ? "[DONE]" : "[PENDING]";
            string rem = string.IsNullOrEmpty(Reminder) ? "No reminder set" : $"Reminder: {Reminder}";
            return $"{status} {Title} — {rem}";
        }
    }
}
