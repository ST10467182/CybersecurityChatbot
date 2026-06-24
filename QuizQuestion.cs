// =============================================================
// QuizQuestion.cs
// Cybersecurity Awareness Chatbot - Quiz Question Model
// =============================================================
// Represents a single quiz question with its options,
// correct answer, explanation, and type (MC or True/False).
//
// References:
// [14] Microsoft, 2024. Classes and Objects (C# Programming
//      Guide) [Online]. Available at: https://learn.microsoft.
//      com/en-us/dotnet/csharp/fundamentals/types/classes
//      [Accessed 20 May 2026].
// =============================================================

using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    // Defines the two supported question types [14]
    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse
    }

    // Model class for a single quiz question [14]
    public class QuizQuestion
    {
        public string Question { get; set; } = "";

        // Options stored in a List<string> as required by the brief
        public List<string> Options { get; set; } = new List<string>();

        // Index of the correct answer in the Options list (0-based)
        public int CorrectAnswerIndex { get; set; }

        // Explanation shown after the user answers
        public string Explanation { get; set; } = "";

        public QuestionType Type { get; set; } = QuestionType.MultipleChoice;

        public QuizQuestion(string question, List<string> options,
            int correctIndex, string explanation,
            QuestionType type = QuestionType.MultipleChoice)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctIndex;
            Explanation = explanation;
            Type = type;
        }
    }
}
