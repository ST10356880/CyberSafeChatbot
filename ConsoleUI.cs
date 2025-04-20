using System;
using System.Text;
using System.Threading.Tasks;

namespace CyberSafeChatbot
{
    public static class ConsoleUI
    {
        public static void Initialize()
        {
            // Optional: Set output encoding if using extended ASCII characters in logo
            Console.OutputEncoding = Encoding.UTF8;

            // Optional: Configure console colours or title
            Console.Title = "CyberSafe Chatbot";
        }

        public static void DisplayLogo()
        {
            string logo = @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗ █████╗ ███████╗███████╗
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗███████║█████╗  █████╗  
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══██║██╔══╝  ██╔══╝  
╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║██║  ██║██║     ███████╗
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚══════╝
";
            PrintColoredText(logo, ConsoleColor.Cyan);
        }

        public static void PrintColoredText(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = previousColor;
        }

        public static async Task TypeTextAsync(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                await Task.Delay(delay); // Delay for typing effect
            }
            Console.WriteLine(); // New line after the message
        }
    }
}
