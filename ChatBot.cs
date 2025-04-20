using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSafeChatbot
{
    public class ChatBot
    {
        private string userName;
        private KnowledgeBase knowledgeBase;

        public ChatBot()
        {
            knowledgeBase = new KnowledgeBase();
        }

        public async Task StartAsync()
        {
            // 1. Ask user for their name
            Console.Write("Hello! What's your name? ");
            userName = Console.ReadLine();

            // 2. Default to "Friend" if name is null or empty
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "Friend";
            }

            // 3. Display a personalised welcome message using typing animation
            await ConsoleUI.TypeTextAsync($"Welcome, {userName}! I'm here to help you stay safe online.");

            // 4. Show list of topics
            await ConsoleUI.TypeTextAsync("\nHere are some topics I can help you with:");
            await ConsoleUI.TypeTextAsync("- Passwords\n- Phishing\n- Browsing\n- Mobile\n- Social\n- Ransomware\n- Wi-Fi");

            // 5. Inform the user how to exit
            await ConsoleUI.TypeTextAsync("\nYou can type 'exit' or 'quit' at any time to leave the chat.");

            string input;

            // 6. Main interaction loop
            while (true)
            {
                Console.Write("\nAsk me a question or enter a topic: ");
                input = Console.ReadLine()?.Trim().ToLower();

                // 6c. Exit conditions
                if (input == "exit" || input == "quit")
                {
                    await ConsoleUI.TypeTextAsync($"Goodbye, {userName}. Stay cyber-safe!");
                    break;
                }

                // 6d. Get and display response
                string response = knowledgeBase.GetResponse(input);
                await ConsoleUI.TypeTextAsync(response);
            }
        }
    }
}

