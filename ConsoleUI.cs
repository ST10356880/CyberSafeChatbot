using System.Text;

namespace CyberSafeChatbot
{
    public static class ConsoleUI
    {
        public static void Initialize()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Failed to set console encoding: {ex.Message}");
            }

            Console.Title = "CyberSafe Chatbot";
        }

        public static void DisplayLogo()
        {
            string[] logoLines = new[]
            {
                " ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗ █████╗ ███████╗███████╗",
                "██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝",
                "██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗███████║█████╗  █████╗  ",
                "██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══██║██╔══╝  ██╔══╝  ",
                "╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║██║  ██║██║     ███████╗",
                " ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚══════╝"
            };

            int logoWidth = logoLines.Max(line => line.Length) + 4; // +4 for padding and borders
            int windowWidth = Console.WindowWidth;
            int padding = Math.Max((windowWidth - logoWidth) / 2, 0);

            string topBorder = "┌" + new string('─', logoWidth - 2) + "┐";
            string bottomBorder = "└" + new string('─', logoWidth - 2) + "┘";

            PrintCenteredLine(topBorder, padding, ConsoleColor.Cyan);
            foreach (var line in logoLines)
            {
                string paddedLine = line.PadRight(logoWidth - 4); // account for "│ │"
                PrintCenteredLine($"│ {paddedLine} │", padding, ConsoleColor.Cyan);
            }
            PrintCenteredLine(bottomBorder, padding, ConsoleColor.Cyan);

            PrintColoredText("\nHello and welcome to the CyberSafe Chatbot! I’m excited to assist you in discovering ways to protect yourself online. Let’s dive in!\n", ConsoleColor.White);
        }

        private static void PrintCenteredLine(string text, int padding, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(new string(' ', padding) + text);
            }
            finally
            {
                Console.ForegroundColor = previousColor;
            }
        }

        public static void PrintColoredText(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
            }
            finally
            {
                Console.ForegroundColor = previousColor;
            }
        }

        public static async Task TypeTextAsync(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                await Task.Delay(delay);
            }
            Console.WriteLine();
        }
    }
}
// Troelsen, A. and Japikse, P. (2022) Pro C# 10 with .NET 6: Foundational principles and practices in programming. 11th ed. Berlin, Germany: APress.

// https://youtu.be/wxznTygnRfQ?si=dGSmrUza34xHX8t9

// https://chatgpt.com/share/68092fdd-403c-800b-903c-6c2ac1dda1b2
