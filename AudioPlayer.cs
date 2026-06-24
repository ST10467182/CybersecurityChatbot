// =============================================================
// AudioPlayer.cs
// Cybersecurity Awareness Chatbot - Voice Greeting Module
// =============================================================
// Plays voice greeting using Windows TTS. Carried over from
// Part 1 and Part 2 into Part 3 unchanged.
//
// References:
// [6] Microsoft, 2024. SpeechSynthesizer Class [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/
//     api/system.speech.synthesis.speechsynthesizer
//     [Accessed 14 April 2026].
// =============================================================

using System;
using System.Speech.Synthesis;

namespace CybersecurityChatbotGUI
{
    public class AudioPlayer
    {
        public void PlayGreeting()
        {
            try
            {
                using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                {
                    synthesizer.SetOutputToDefaultAudioDevice();
                    synthesizer.Speak(
                        "Hello! Welcome to the Cybersecurity Awareness Bot. " +
                        "I am here to help you stay safe online. " +
                        "Together, we can build a more secure South Africa."
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Audio] Skipped: {ex.Message}");
            }
        }
    }
}
