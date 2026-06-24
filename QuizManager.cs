// =============================================================
// QuizManager.cs
// Cybersecurity Awareness Chatbot - Mini Quiz Game
// =============================================================
// Manages the cybersecurity quiz with 12 questions (MC + T/F).
// Tracks score, current question index, and provides feedback.
// Uses List<QuizQuestion> and Dictionary for score feedback.
//
// References:
// [3] Pieterse, H. 2021. The Cyber Threat Landscape in South
//     Africa: A 10-Year Review. The African Journal of
//     Information and Communication, 28(28).
//     doi: https://doi.org/10.23962/10539/32213
//     [Accessed 16 February 2026].
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
    public class QuizManager
    {
        // All quiz questions stored in a List<QuizQuestion> [13]
        private readonly List<QuizQuestion> _questions;

        private int _currentIndex = 0;
        private int _score = 0;
        private bool _isActive = false;

        // Dictionary maps score ranges to feedback messages [Part 3 requirement]
        private readonly Dictionary<string, string> _scoreFeedback =
            new Dictionary<string, string>
            {
                ["excellent"] = "🏆 Excellent! You're a cybersecurity pro! Your digital safety knowledge is outstanding.",
                ["good"]      = "👍 Great job! You have solid cybersecurity knowledge. Keep it up!",
                ["average"]   = "📚 Not bad! There's room to improve — keep learning to stay safe online.",
                ["poor"]      = "⚠️  Keep learning! Cybersecurity knowledge is essential to staying safe online [3]."
            };

        public QuizManager()
        {
            _questions = BuildQuestions();
        }

        // Builds the full set of 12 quiz questions — mix of MC and True/False
        // Questions cover phishing, passwords, malware, browsing, and social engineering [3]
        private List<QuizQuestion> BuildQuestions()
        {
            return new List<QuizQuestion>
            {
                // QUESTION 1 — MC — Phishing [3]
                new QuizQuestion(
                    "What should you do if you receive an email asking for your password?",
                    new List<string> { "A) Reply with your password", "B) Delete the email",
                        "C) Report the email as phishing", "D) Ignore it" },
                    2,
                    "Correct! Reporting phishing emails helps protect others. Never share your password via email [3].",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 2 — True/False — Passwords
                new QuizQuestion(
                    "TRUE or FALSE: Using the same password for multiple accounts is safe as long as it is strong.",
                    new List<string> { "A) True", "B) False" },
                    1,
                    "FALSE. Even a strong password becomes a risk if reused — one breach exposes all your accounts.",
                    QuestionType.TrueFalse
                ),

                // QUESTION 3 — MC — Safe Browsing
                new QuizQuestion(
                    "Which of the following indicates a website is secure?",
                    new List<string> { "A) The URL starts with 'http://'",
                        "B) The URL starts with 'https://' and shows a padlock",
                        "C) The website has a professional design",
                        "D) The website asks for your email" },
                    1,
                    "Correct! HTTPS with a padlock means the connection is encrypted and the site is verified.",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 4 — True/False — Phishing [3]
                new QuizQuestion(
                    "TRUE or FALSE: Phishing emails always contain obvious spelling mistakes.",
                    new List<string> { "A) True", "B) False" },
                    1,
                    "FALSE. Modern phishing emails can be very convincing and professionally written [3]. Always verify the sender.",
                    QuestionType.TrueFalse
                ),

                // QUESTION 5 — MC — 2FA
                new QuizQuestion(
                    "What does Two-Factor Authentication (2FA) do?",
                    new List<string> { "A) Doubles your password length",
                        "B) Requires two passwords to log in",
                        "C) Adds a second verification step beyond your password",
                        "D) Sends your password to two email addresses" },
                    2,
                    "Correct! 2FA adds a second layer — usually an OTP — making it much harder for attackers to gain access.",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 6 — MC — Malware [3]
                new QuizQuestion(
                    "What is ransomware?",
                    new List<string> { "A) A type of antivirus software",
                        "B) Malware that encrypts files and demands payment",
                        "C) A tool for recovering lost passwords",
                        "D) A firewall program" },
                    1,
                    "Correct! Ransomware locks your files and demands payment. Regular backups are your best protection [3].",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 7 — True/False — Public Wi-Fi [3]
                new QuizQuestion(
                    "TRUE or FALSE: It is safe to do online banking on public Wi-Fi.",
                    new List<string> { "A) True", "B) False" },
                    1,
                    "FALSE. Public Wi-Fi is a prime target for cybercriminals. Always use a VPN or mobile data for banking [3].",
                    QuestionType.TrueFalse
                ),

                // QUESTION 8 — MC — Social Engineering [3]
                new QuizQuestion(
                    "What is social engineering in cybersecurity?",
                    new List<string> { "A) Building social media platforms",
                        "B) Using psychological manipulation to trick people into giving up information",
                        "C) Engineering software for social networks",
                        "D) A type of antivirus program" },
                    1,
                    "Correct! Social engineering exploits human trust rather than technical vulnerabilities [3].",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 9 — MC — Passwords
                new QuizQuestion(
                    "Which is the strongest password?",
                    new List<string> { "A) password123", "B) John1990",
                        "C) !Gx7#mK2$pL9", "D) qwerty" },
                    2,
                    "Correct! Strong passwords use a mix of uppercase, lowercase, numbers, and symbols with 12+ characters.",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 10 — True/False — Software Updates
                new QuizQuestion(
                    "TRUE or FALSE: Delaying software updates has no impact on your cybersecurity.",
                    new List<string> { "A) True", "B) False" },
                    1,
                    "FALSE. Updates patch known vulnerabilities. Delaying them leaves your system exposed to known attacks.",
                    QuestionType.TrueFalse
                ),

                // QUESTION 11 — MC — POPIA/Privacy
                new QuizQuestion(
                    "Which South African law protects your personal data online?",
                    new List<string> { "A) GDPR", "B) POPIA",
                        "C) RICA", "D) FICA" },
                    1,
                    "Correct! POPIA (Protection of Personal Information Act) is South Africa's data privacy law.",
                    QuestionType.MultipleChoice
                ),

                // QUESTION 12 — MC — SIM Swapping [3]
                new QuizQuestion(
                    "What is the best protection against SIM swapping attacks?",
                    new List<string> { "A) Using SMS-based 2FA",
                        "B) Changing your phone number regularly",
                        "C) Using an authenticator app instead of SMS OTPs",
                        "D) Turning off your phone at night" },
                    2,
                    "Correct! Authenticator apps are not linked to your SIM, so SIM swapping cannot intercept your OTPs [3].",
                    QuestionType.MultipleChoice
                ),
            };
        }

        // Starts the quiz — resets score and question index
        public void StartQuiz()
        {
            _currentIndex = 0;
            _score = 0;
            _isActive = true;
        }

        public bool IsActive() => _isActive;
        public int GetCurrentIndex() => _currentIndex;
        public int GetTotalQuestions() => _questions.Count;
        public int GetScore() => _score;

        // Returns the current question
        public QuizQuestion? GetCurrentQuestion()
        {
            if (_currentIndex < _questions.Count)
                return _questions[_currentIndex];
            return null;
        }

        // Processes the user's answer and returns feedback
        // Returns (isCorrect, feedbackMessage)
        public (bool isCorrect, string feedback) SubmitAnswer(int selectedIndex)
        {
            QuizQuestion current = _questions[_currentIndex];
            bool isCorrect = selectedIndex == current.CorrectAnswerIndex;

            if (isCorrect) _score++;
            _currentIndex++;

            if (_currentIndex >= _questions.Count)
                _isActive = false;

            string prefix = isCorrect ? "✅ Correct! " : "❌ Incorrect. ";
            return (isCorrect, prefix + current.Explanation);
        }

        // Returns a final score summary with personalised feedback
        public string GetFinalFeedback()
        {
            int total = _questions.Count;
            int percentage = (int)((_score / (double)total) * 100);

            string feedbackKey = percentage >= 90 ? "excellent"
                               : percentage >= 70 ? "good"
                               : percentage >= 50 ? "average"
                               : "poor";

            string feedback = _scoreFeedback[feedbackKey];
            return $"Quiz Complete!\n\nYour score: {_score}/{total} ({percentage}%)\n\n{feedback}";

        }
    }
}
