using System.Text;


namespace CyberSafeChatbot
{
    public static class ConsoleUI
    {
        public static void Initialize()
        {
            try
            {
                // Set output encoding if using extended ASCII characters in logo
                Console.OutputEncoding = Encoding.UTF8;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Failed to set console encoding: {ex.Message}");
            }

            // Configure console title
            Console.Title = "CyberSafe Chatbot";
        }

        public static void DisplayLogo()
        {
            const string logo = @"
     ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗ █████╗ ███████╗███████╗
    ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝
    ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗███████║█████╗  █████╗  
    ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══██║██╔══╝  ██╔══╝  
    ╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║██║  ██║██║     ███████╗
     ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚══════╝
    ";
            PrintColoredText(logo, ConsoleColor.Cyan);

            // Welcome message under artwork
            PrintColoredText("\nHello and welcome to the CyberSafe Chatbot! I’m excited to assist you in discovering ways to protect yourself online. Let’s dive in!\n", ConsoleColor.White);
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
                await Task.Delay(delay); // Delay for typing effect
            }
            Console.WriteLine(); // New line after the message
        }
    }
}
