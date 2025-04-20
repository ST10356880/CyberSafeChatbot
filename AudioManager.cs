using System.IO;
using System;
using System.Media;
using System.Threading.Tasks;


namespace CyberSafeChatbot
{
    public static class AudioManager
    {
        private static SoundPlayer player;

        public static void Initialize()
        {
            // Path to your welcome audio (must be a .wav file)
            string audioPath = "welcome.wav";

            try
            {
                player = new SoundPlayer(audioPath);
                player.LoadAsync(); // Load in background
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AudioManager] Error loading audio: {ex.Message}");
            }
        }

        public static async Task PlayWelcomeAudioAsync()
        {
            try
            {
                await Task.Run(() => player?.PlaySync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AudioManager] Error playing audio: {ex.Message}");
            }
        }
    }
}
