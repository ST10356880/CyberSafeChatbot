using System;
using System.Threading.Tasks;

namespace CyberSafeChatbot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 1. Initialise the Console UI system.
            ConsoleUI.Initialize();

            // 2. Initialise the Audio Manager for sound playback.
            AudioManager.Initialize();

            // 3. Display the chatbot's logo on the console.
            ConsoleUI.DisplayLogo();

            // 4. Add a blank line for spacing.
            Console.WriteLine();

            // 5. Play a welcome audio message asynchronously.
            await AudioManager.PlayWelcomeAudioAsync();

            // 6. Create an instance of the ChatBot.
            var chatBot = new ChatBot();

            // 7. Start the ChatBot's main interaction loop asynchronously.
            await chatBot.StartAsync();

            // 8. After chatbot ends, display a green-colored thank you message.
            ConsoleUI.PrintColoredText("\nThank you for using CyberSafe Chatbot. Stay safe online!", ConsoleColor.Green);

            // 9. Prompt the user to press any key to exit.
            Console.WriteLine("\nPress any key to exit...");

            // 10. Wait for any key press before closing the application.
            Console.ReadKey();
        }
    }
}
