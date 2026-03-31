// =============================================================
// Program.cs
// Cybersecurity Awareness Chatbot - Entry Point
// Author: Nsika Beyers
// Student Number: ST10467182
// 
// =============================================================
// This file serves only as the application entry point.
// All logic is handled in separate classes as per good
// code structure principles (see ChatBot.cs, UIHelper.cs,
// ResponseEngine.cs, AudioPlayer.cs).
// =============================================================

using CybersecurityChatbot;

// Instantiate and launch the chatbot
ChatBot bot = new ChatBot();
bot.Run();
