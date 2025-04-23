using System.Media;

namespace CyberSafeChatbot
{
    public static class AudioManager
    {
        private static string audioDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio"); // [Updated] Set audio directory dynamically

        public static void Initialize()
        {
            if (!Directory.Exists(audioDirectory))
            {
                Directory.CreateDirectory(audioDirectory); // [Updated] Automatically creates Audio folder if missing
            }
        }

        public static async Task PlayAudioAsync(string fileName)
        {
            string audioPath = Path.Combine(audioDirectory, fileName); // [Updated] Combine path to target specific audio file

            if (File.Exists(audioPath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        await Task.Run(() => player.PlaySync()); // [Updated] Play audio file asynchronously
                    }
                }
                catch (Exception ex)
                {
                    ConsoleUI.PrintColoredText($"Error playing audio: {ex.Message}", ConsoleColor.Red); // [Updated] Print error if playback fails
                }
            }
            else
            {
                ConsoleUI.PrintColoredText($"Audio file '{fileName}' not found in /Audio", ConsoleColor.DarkYellow); // [Updated] Warn if audio file is missing
            }
        }
    }
}
// Troelsen, A. and Japikse, P. (2022) Pro C# 10 with .NET 6: Foundational principles and practices in programming. 11th ed. Berlin, Germany: APress.

// https://youtu.be/wxznTygnRfQ?si=dGSmrUza34xHX8t9

// https://chatgpt.com/share/68092fdd-403c-800b-903c-6c2ac1dda1b2
