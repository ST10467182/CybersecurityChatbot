// =============================================================
// AudioPlayer.cs
// Cybersecurity Awareness Chatbot - Voice Greeting Module
// =============================================================
// Uses Windows built-in SpeechSynthesizer to speak the greeting
// directly on launch. No WAV file required.
//
// References:
// [6] Microsoft, 2024. SpeechSynthesizer Class [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/
//     api/system.speech.synthesis.speechsynthesizer
//     [Accessed 31 March 2026].
// =============================================================

using System;
using System.Speech.Synthesis;

namespace CybersecurityChatbot
{
    public class AudioPlayer
    {
        // Speaks the welcome greeting aloud using Windows TTS engine [6]
        public void PlayGreeting()
        {
            try
            {
                using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                {
                    // Set output to the default audio device (speakers) [6]
                    synthesizer.SetOutputToDefaultAudioDevice();

                    // Speak the greeting synchronously so it finishes before
                    // the chatbot text interaction begins
                    synthesizer.Speak(
                        "Hello! Welcome to the Cybersecurity Awareness Bot. " +
                        "I am here to help you stay safe online. " +
                        "Together, we can build a more secure South Africa."
                    );
                }
            }
            catch (Exception ex)
            {
                // Audio failure should never crash the application
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  [Audio] Voice greeting skipped: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}