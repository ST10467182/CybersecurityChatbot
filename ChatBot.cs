// =============================================================
// ChatBot.cs
// Cybersecurity Awareness Chatbot - Core Chatbot Logic
// =============================================================
// This class is the main controller of the chatbot application.
// It coordinates the UI, audio, and response systems to
// produce a complete, interactive console experience.
//
// References:
// [3] Pieterse, H. 2021. The Cyber Threat Landscape in South
//     Africa: A 10-Year Review. The African Journal of
//     Information and Communication, 28(28).
//     doi: https://doi.org/10.23962/10539/32213. [Online].
//     Available at: https://www.scielo.org.za/scielo.php?pid=
//     S2077-72132021000200003&script=sci_arttext
//     [Accessed 16 February 2026].
//
// [8] Microsoft, 2024. Console.ReadLine Method [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/
//     api/system.console.readline [Accessed 31 March 2026].
// =============================================================

using System;

namespace CybersecurityChatbot
{
    public class ChatBot
    {
        // The user's name — used throughout the session for personalisation
        private string _userName = "User";

        // Core components — each class handles a separate responsibility
        private readonly ResponseEngine _responseEngine;
        private readonly AudioPlayer _audioPlayer;

        public ChatBot()
        {
            _responseEngine = new ResponseEngine();
            _audioPlayer = new AudioPlayer();
        }

        // Main application loop — controls the full chatbot session
        public void Run()
        {
            // Step 1: Display ASCII art logo on launch
            UIHelper.DisplayAsciiLogo();

            // Step 2: Play the voice greeting before any text interaction
            UIHelper.PrintBotMessage("One moment — playing voice greeting...");
            _audioPlayer.PlayGreeting();

            // Step 3: Display shield symbol and intro section
            UIHelper.DisplayShield();
            UIHelper.PrintSection("WELCOME");

            // Step 4: Ask for and validate the user's name
            _userName = PromptForName();

            // Step 5: Display personalised welcome
            UIHelper.PrintBotMessage(
                $"Great to have you here, {_userName}! " +
                $"Cybersecurity awareness is now a national priority in South Africa [3]. " +
                $"Let's make sure you're equipped to stay safe online."
            );

            // Step 6: Display available topic hints
            UIHelper.PrintBotMessage(
                "You can ask me about phishing, passwords, malware, safe browsing, " +
                "social engineering, 2FA, public Wi-Fi, or data privacy. " +
                "Type 'topics' to see the full list, or type 'exit' to quit."
            );

            // Step 7: Enter the main conversation loop
            RunConversationLoop();
        }

        // Prompts the user for their name and validates the input
        // Returns a non-empty, trimmed name string
        // Input validation ensures empty entries are handled gracefully [8]
        private string PromptForName()
        {
            string name = string.Empty;

            UIHelper.PrintBotMessage("Before we begin, what is your name?");

            while (string.IsNullOrWhiteSpace(name))
            {
                UIHelper.PrintUserPrompt("You");
                name = Console.ReadLine()?.Trim() ?? string.Empty; // [8]

                if (string.IsNullOrWhiteSpace(name))
                {
                    // Input validation: handle empty or whitespace-only entries
                    UIHelper.PrintWarning("I didn't catch that. Please enter your name to continue.");
                }
            }

            return name;
        }

        // Core loop — reads user input and generates responses until exit
        private void RunConversationLoop()
        {
            bool isRunning = true;

            UIHelper.PrintSection("CHATBOT SESSION STARTED");

            while (isRunning)
            {
                // Display the user's input prompt with their name
                UIHelper.PrintUserPrompt(_userName);

                // Read input from the user [8]
                string? rawInput = Console.ReadLine();

                // Input validation — detect empty or null input
                if (string.IsNullOrWhiteSpace(rawInput))
                {
                    UIHelper.PrintWarning(
                        "I didn't quite understand that. Could you rephrase? " +
                        "Try typing 'topics' to see what I can help with."
                    );
                    continue; // Skip processing and prompt again
                }

                string input = rawInput.Trim();

                // Check for exit command — end the session cleanly
                if (IsExitCommand(input))
                {
                    UIHelper.PrintSection("SESSION ENDED");
                    UIHelper.PrintBotMessage(
                        $"Stay vigilant and stay safe online, {_userName}! " +
                        "Remember: cybersecurity is everyone's responsibility [3]. Goodbye!"
                    );
                    isRunning = false;
                    continue;
                }

                // Pass input to the ResponseEngine for keyword matching
                string? response = _responseEngine.GetResponse(input);

                if (response != null)
                {
                    // Keyword match found — display the relevant cybersecurity response
                    UIHelper.PrintBotMessage(response);
                }
                else
                {
                    // No match found — display a helpful fallback message
                    UIHelper.PrintBotMessage(
                        "I didn't quite understand that. Could you rephrase? " +
                        "I can help with phishing, passwords, malware, safe browsing, " +
                        "social engineering, 2FA, public Wi-Fi, or data privacy."
                    );
                }
            }

            Console.WriteLine();
        }

        // Checks whether the user's input is an exit command
        // Accepts multiple natural variations to improve user experience
        private bool IsExitCommand(string input)
        {
            string lowered = input.ToLower();
            return lowered == "exit" ||
                   lowered == "quit" ||
                   lowered == "bye" ||
                   lowered == "goodbye" ||
                   lowered == "leave";
        }
    }
}
