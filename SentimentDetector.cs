// =============================================================
// SentimentDetector.cs
// Cybersecurity Awareness Chatbot - Sentiment Detection
// =============================================================
// Detects emotional tone of user input. Carried over from Part 2.
//
// References:
// [11] Liu, B. 2012. Sentiment Analysis and Opinion Mining.
//      Synthesis Lectures on Human Language Technologies, 5(1).
//      doi: https://doi.org/10.2200/S00416ED1V01Y201204HLT016
//      [Accessed 14 April 2026].
// =============================================================

namespace CybersecurityChatbotGUI
{
    public class SentimentDetector
    {
        private readonly string[] _worriedKeywords =
            { "worried", "scared", "anxious", "afraid", "nervous", "concerned",
              "fearful", "terrified", "panic", "stress", "frightened", "overwhelmed" };

        private readonly string[] _curiousKeywords =
            { "curious", "interested", "wondering", "want to know", "how does",
              "why does", "what is", "explain", "i wonder", "tell me more about" };

        private readonly string[] _frustratedKeywords =
            { "frustrated", "annoyed", "confused", "don't understand", "cant figure",
              "can't figure", "this is hard", "difficult", "complicated", "makes no sense" };

        private readonly string[] _positiveKeywords =
            { "great", "awesome", "thank you", "thanks", "helpful", "love it",
              "amazing", "fantastic", "good job", "well done", "excellent", "happy" };

        public string? Detect(string input)
        {
            string lowered = input.ToLower();
            if (ContainsKeyword(lowered, _worriedKeywords)) return "worried";
            if (ContainsKeyword(lowered, _frustratedKeywords)) return "frustrated";
            if (ContainsKeyword(lowered, _positiveKeywords)) return "positive";
            if (ContainsKeyword(lowered, _curiousKeywords)) return "curious";
            return null;
        }

        private bool ContainsKeyword(string input, string[] keywords)
        {
            foreach (string kw in keywords)
                if (input.Contains(kw)) return true;
            return false;
        }
    }
}
