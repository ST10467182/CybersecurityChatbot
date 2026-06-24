// =============================================================
// NLPProcessor.cs
// Cybersecurity Awareness Chatbot - NLP Simulation
// =============================================================
// Simulates Natural Language Processing using string.Contains()
// and keyword arrays to understand user intent even when input
// is phrased differently.
//
// Detects intent for: task commands, quiz commands, log commands,
// reminder commands, and navigation commands.
//
// References:
// [17] Jurafsky, D. and Martin, J.H. 2023. Speech and Language
//      Processing (3rd ed.) [Online]. Available at:
//      https://web.stanford.edu/~jurafsky/slp3/
//      [Accessed 20 May 2026].
//
// [13] Microsoft, 2024. List<T> Class [Online].
//      Available at: https://learn.microsoft.com/en-us/dotnet/
//      api/system.collections.generic.list-1
//      [Accessed 20 May 2026].
// =============================================================

using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    // Defines all possible user intents the NLP processor can detect [17]
    public enum UserIntent
    {
        None,
        AddTask,
        ViewTasks,
        CompleteTask,
        DeleteTask,
        StartQuiz,
        ShowLog,
        SetReminder,
        ViewMemory,
        FollowUp,
        NavigateChat,
        NavigateTasks,
        NavigateQuiz,
        NavigateLog
    }

    public class NLPProcessor
    {
        // Each intent maps to a list of keyword phrases [13]
        // Using Dictionary<UserIntent, List<string>> for organised NLP lookup [17]
        private readonly Dictionary<UserIntent, List<string>> _intentKeywords;

        public NLPProcessor()
        {
            // Build the intent-to-keywords mapping
            // Allows for varied phrasing — NLP simulation via string matching [17]
            _intentKeywords = new Dictionary<UserIntent, List<string>>
            {
                [UserIntent.AddTask] = new List<string>
                {
                    "add task", "create task", "new task", "add a task",
                    "remind me to", "set a reminder", "add reminder",
                    "i need to", "i want to", "schedule", "create reminder",
                    "set reminder for", "remind me about"
                },

                [UserIntent.ViewTasks] = new List<string>
                {
                    "view tasks", "show tasks", "my tasks", "list tasks",
                    "what are my tasks", "show my tasks", "pending tasks",
                    "all tasks", "task list", "what tasks"
                },

                [UserIntent.CompleteTask] = new List<string>
                {
                    "complete task", "mark complete", "done with",
                    "finished", "mark as done", "task done",
                    "completed", "i completed", "mark task"
                },

                [UserIntent.DeleteTask] = new List<string>
                {
                    "delete task", "remove task", "cancel task",
                    "get rid of task", "delete reminder", "remove reminder"
                },

                [UserIntent.StartQuiz] = new List<string>
                {
                    "start quiz", "play quiz", "quiz me", "take quiz",
                    "begin quiz", "test my knowledge", "quiz",
                    "cybersecurity quiz", "test me", "i want a quiz",
                    "let's quiz", "start the quiz"
                },

                [UserIntent.ShowLog] = new List<string>
                {
                    "show activity log", "activity log", "show log",
                    "what have you done", "recent actions", "history",
                    "show history", "what have you done for me",
                    "log", "action log", "show recent"
                },

                [UserIntent.ViewMemory] = new List<string>
                {
                    "what do you remember", "what do you know about me",
                    "remember about me", "my profile", "what have i told you"
                },

                [UserIntent.NavigateChat] = new List<string>
                {
                    "go to chat", "open chat", "chat tab", "back to chat"
                },

                [UserIntent.NavigateTasks] = new List<string>
                {
                    "go to tasks", "open tasks", "tasks tab", "task manager"
                },

                [UserIntent.NavigateQuiz] = new List<string>
                {
                    "go to quiz", "open quiz", "quiz tab", "game tab"
                },

                [UserIntent.NavigateLog] = new List<string>
                {
                    "go to log", "open log", "activity tab", "log tab"
                },
            };
        }

        // Analyses user input and returns the most likely UserIntent [17]
        // Implements NLP simulation via keyword detection as required by the brief
        public UserIntent DetectIntent(string input)
        {
            string lowered = input.ToLower().Trim();

            // Iterate through all intents and check keyword matches
            foreach (var entry in _intentKeywords)
            {
                foreach (string keyword in entry.Value)
                {
                    if (lowered.Contains(keyword))
                        return entry.Key;
                }
            }

            return UserIntent.None;
        }

        // Extracts a task title from natural language input
        // Example: "Add task - Review privacy settings" → "Review privacy settings"
        // Example: "Remind me to update my password" → "Update my password"
        public string ExtractTaskTitle(string input)
        {
            string lowered = input.ToLower();

            // List of patterns to strip from the front of the input [13]
            List<string> prefixPatterns = new List<string>
            {
                "add task - ", "add task: ", "add task ",
                "create task - ", "create task: ", "create task ",
                "new task - ", "new task: ", "new task ",
                "remind me to ", "remind me about ", "set a reminder for ",
                "set reminder for ", "i need to ", "i want to "
            };

            foreach (string pattern in prefixPatterns)
            {
                if (lowered.StartsWith(pattern))
                {
                    // Return the remainder with proper capitalisation
                    string remainder = input.Substring(pattern.Length).Trim();
                    if (remainder.Length > 0)
                        return char.ToUpper(remainder[0]) + remainder.Substring(1);
                }
            }

            // If no pattern matched, return the original input capitalised
            return input.Length > 0
                ? char.ToUpper(input[0]) + input.Substring(1)
                : input;
        }

        // Extracts a reminder timeframe from input
        // Example: "remind me in 3 days" → "3 days"
        public string ExtractReminder(string input)
        {
            string lowered = input.ToLower();

            // Common reminder timeframe patterns
            string[] patterns = { "in ", "after ", "on ", "by ", "within " };

            foreach (string pattern in patterns)
            {
                int idx = lowered.IndexOf(pattern);
                if (idx >= 0)
                {
                    string timeframe = input.Substring(idx + pattern.Length).Trim();
                    // Clean up trailing punctuation
                    timeframe = timeframe.TrimEnd('.', '!', '?', ',');
                    if (timeframe.Length > 0) return timeframe;

                }
            }

            return "";

        }
    }
}
