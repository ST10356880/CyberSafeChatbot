﻿using System;
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

            // 5. Play the welcome.wav audio asynchronously.
            await AudioManager.PlayAudioAsync("welcome.wav"); // [Updated] Replaced old welcome audio method with new dynamic PlayAudioAsync

            // 6. Create an instance of the ChatBot.
            var chatBot = new ChatBot();

            // 7. Start the ChatBot's main interaction loop asynchronously.
            await chatBot.StartAsync();

            
           
            // 8. Prompt the user to press any key to exit.
            Console.WriteLine("\nPress any key to exit...");

            // 9. Wait for any key press before closing the application.
            Console.ReadKey();
        }
    }
}
// Troelsen, A. and Japikse, P. (2022) Pro C# 10 with .NET 6: Foundational principles and practices in programming. 11th ed. Berlin, Germany: APress.

// https://youtu.be/wxznTygnRfQ?si=dGSmrUza34xHX8t9

// https://chatgpt.com/share/68092fdd-403c-800b-903c-6c2ac1dda1b2
