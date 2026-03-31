// =============================================================
// UIHelper.cs
// Cybersecurity Awareness Chatbot - Console UI Formatting
// =============================================================
// This class is responsible for all visual output in the
// console application. It handles ASCII art display, colour
// formatting, typing effects, and section dividers.
//
// References:
// [1] Microsoft, 2024. Console.ForegroundColor Property [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/api/
//     system.console.foregroundcolor [Accessed 31 March 2026].
//
// [2] Microsoft, 2024. Thread.Sleep(Int32) Method [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/api/
//     system.threading.thread.sleep [Accessed 31 March 2026].
// =============================================================

using System;
using System.Threading;

namespace CybersecurityChatbot
{
    public static class UIHelper
    {
        // Displays the ASCII art logo on application launch
        // ASCII art used as a visual header to improve user engagement [1]
        public static void DisplayAsciiLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(@"   ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗");
            Console.WriteLine(@"  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝");
            Console.WriteLine(@"  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   ");
            Console.WriteLine(@"  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   ");
            Console.WriteLine(@"  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   ");
            Console.WriteLine(@"   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝    ╚═╝  ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ==============================================================");
            Console.WriteLine("           >>>  CYBERSECURITY AWARENESS BOT  <<<");
            Console.WriteLine("           Protecting South African Citizens Online");
            Console.WriteLine("  ==============================================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Displays a shield ASCII art symbol as a secondary visual element
        public static void DisplayShield()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"         /\");
            Console.WriteLine(@"        /  \");
            Console.WriteLine(@"       / !! \");
            Console.WriteLine(@"      /______\");
            Console.WriteLine(@"      |  SA  |");
            Console.WriteLine(@"      | SAFE |");
            Console.WriteLine(@"      \______/");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Simulates a typing effect to make the chatbot feel conversational
        // Thread.Sleep is used to introduce character-by-character delay [2]
        public static void TypeWrite(string message, ConsoleColor color = ConsoleColor.White, int delayMs = 25)
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs); // Small delay per character for typing effect [2]
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        // Prints a horizontal divider line for visual structure [1]
        public static void PrintDivider(char symbol = '=', int length = 64)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  " + new string(symbol, length));
            Console.ResetColor();
        }

        // Prints a formatted bot response with a label and colour
        public static void PrintBotMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n  Bot >> ");
            Console.ResetColor();
            TypeWrite(message, ConsoleColor.White, 20);
        }

        // Prints the user input prompt with their name
        public static void PrintUserPrompt(string name)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  {name} >> ");
            Console.ResetColor();
        }

        // Prints a red warning for invalid inputs or errors [1]
        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [!] {message}");
            Console.ResetColor();
        }

        // Prints a named section header for readability and structure
        public static void PrintSection(string title)
        {
            Console.WriteLine();
            PrintDivider('-', 64);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"   {title}");
            Console.ResetColor();
            PrintDivider('-', 64);
        }
    }
}
