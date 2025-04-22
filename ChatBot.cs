using System;
using System.Threading.Tasks;

namespace CyberSafeChatbot
{
    public class ChatBot
    {
        private string userName;
        private readonly KnowledgeBase knowledgeBase;

        public ChatBot()
        {
            knowledgeBase = new KnowledgeBase();
        }

        public async Task StartAsync()
        {
            // Get user's name
            ConsoleUI.PrintColoredText("What's your name? ", ConsoleColor.Cyan);
            userName = Console.ReadLine()?.Trim() ?? "Friend";

            // Welcome message
            Console.WriteLine();
            await ConsoleUI.TypeTextAsync($"Hello, {userName}! I'm your CyberSafe assistant. I can help you learn about cybersecurity in South Africa.");

            DisplayMenu(); // [Updated] Show menu after welcome

            ConsoleUI.PrintColoredText("Type 'exit' or 'quit' to end our chat.\n", ConsoleColor.DarkGray);

            while (true)
            {
                ConsoleUI.PrintColoredText("\nWhat would you like to know about? ", ConsoleColor.Cyan);
                string input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                // [Updated] Support numeric topic shortcuts
                input = input switch
                {
                    "1" => "password",
                    "2" => "phishing",
                    "3" => "mobile",
                    "4" => "browsing",
                    "5" => "social",
                    "6" => "ransomware",
                    "7" => "wifi",
                    _ => input
                };

                if (input == "exit" || input == "quit")
                {
                    await AudioManager.PlayAudioAsync("goodbye.wav"); // [Updated]
                    await ConsoleUI.TypeTextAsync($"\nGoodbye, {userName}! Stay safe online.");
                    break;
                }

                if (input == "help") // [Updated]
                {
                    await AudioManager.PlayAudioAsync("help.wav");
                    DisplayMenu();
                    continue;
                }

                string response = knowledgeBase.GetResponse(input);

                // [Updated] Handle fallback message
                if (response.StartsWith("I'm not sure") || response.StartsWith("Sorry, I couldn’t understand"))
                {
                    await ConsoleUI.TypeTextAsync(response); // [Updated] Show fallback text first
                    await AudioManager.PlayAudioAsync("unknown.wav");
                    await AudioManager.PlayAudioAsync("help.wav");
                    DisplayMenu();
                }
                else
                {
                    Console.WriteLine();
                    await ConsoleUI.TypeTextAsync(response); // [Updated] Show response first

                    // [Updated] Then play voice-over
                    string audioFile = input switch
                    {
                        "password" => "password.wav",
                        "phishing" => "phishing.wav",
                        "mobile" => "mobile.wav",
                        "browsing" => "browsing.wav",
                        "social" => "social.wav",
                        "ransomware" => "ransomware.wav",
                        "wifi" => "wifi.wav",
                        _ => null
                    };

                    if (!string.IsNullOrEmpty(audioFile))
                    {
                        ConsoleUI.PrintColoredText($"🔊 Now reading: {ToTitleCase(input)}", ConsoleColor.Yellow);
                        await AudioManager.PlayAudioAsync(audioFile);
                    }
                }
            }
        }

        private void DisplayMenu() // [Updated]
        {
            ConsoleUI.PrintColoredText(@"
1. Strong Passwords
2. Phishing Scams
3. Mobile Security
4. Safe Browsing
5. Social Media Safety
6. Ransomware Protection
7. Wi-Fi Security
", ConsoleColor.DarkCyan);
        }

        private string ToTitleCase(string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }
    }
}
