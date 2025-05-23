﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyberSafeChatbot
{
    public class ChatBot
    {
        private string userName;
        private string lastTopic;
        private string userSentiment;
        private bool askedFollowUp = false;
        private bool waitingForYesNo = false;
        private string pendingFollowUpTopic;

        private readonly KnowledgeBase knowledgeBase;
        private readonly List<string> userInterests = new();
        private readonly List<string> conversationHistory = new();

        public ChatBot()
        {
            knowledgeBase = new KnowledgeBase();
        }

        public async Task StartAsync()
        {
            string inputName;
            do
            {
                ConsoleUI.PrintColoredText("What's your name? ", ConsoleColor.Cyan);
                inputName = Console.ReadLine()?.Trim();
            }
            while (string.IsNullOrWhiteSpace(inputName));

            userName = inputName;
            conversationHistory.Add(userName);

            Console.WriteLine();
            await ConsoleUI.TypeTextAsync($"Hello, {userName}! I'm your CyberSafe assistant. I can help you learn about cybersecurity in South Africa.");

            DisplayMenu();
            ConsoleUI.PrintColoredText("Type 'exit' or 'quit' to end our chat.\n", ConsoleColor.DarkGray);

            while (true)
            {
                ConsoleUI.PrintColoredText("\nWhat would you like to know about? ", ConsoleColor.Cyan);
                string input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                conversationHistory.Add(input);

                // Handle menu shortcuts
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

                // Check for exit
                if (input == "exit" || input == "quit")
                {
                    await AudioManager.PlayAudioAsync("goodbye.wav");
                    await ConsoleUI.TypeTextAsync($"\nGoodbye, {userName}! Stay safe online.");
                    break;
                }

                // Display help menu
                if (input == "help")
                {
                    await AudioManager.PlayAudioAsync("help.wav");
                    DisplayMenu();
                    continue;
                }

                // Respond to follow-up yes/no
                if (waitingForYesNo && (input == "yes" || input == "no"))
                {
                    if (input == "yes")
                    {
                        string moreInfo = knowledgeBase.GetFollowUpDetail(pendingFollowUpTopic);
                        await ConsoleUI.TypeTextAsync(moreInfo);
                    }
                    else
                    {
                        await ConsoleUI.TypeTextAsync("No problem. Let me know if there's something else you'd like help with.");
                    }
                    waitingForYesNo = false;
                    pendingFollowUpTopic = null;
                    continue;
                }

                // Detect sentiment
                var sentiment = knowledgeBase.DetectSentiment(input);
                if (!string.IsNullOrWhiteSpace(sentiment))
                    userSentiment = sentiment;

                bool isFollowUp = knowledgeBase.IsFollowUpQuestion(input);
                string topic = knowledgeBase.GetTopicFromInput(input);

                if (isFollowUp && !string.IsNullOrEmpty(lastTopic) && string.IsNullOrEmpty(topic))
                {
                    topic = lastTopic;
                    askedFollowUp = true;
                }

                if (!string.IsNullOrEmpty(topic))
                {
                    lastTopic = topic;
                    if (!userInterests.Contains(topic))
                    {
                        userInterests.Add(topic);
                        ConsoleUI.PrintColoredText($"Great! I see you're interested in {ToTitleCase(topic)}.", ConsoleColor.Green);
                    }

                    (string response, string followUp) = knowledgeBase.GetRandomResponse(topic);

                    if (!string.IsNullOrEmpty(userSentiment))
                        response = knowledgeBase.GetSentimentResponse(response, userSentiment);

                    if (!askedFollowUp && !string.IsNullOrEmpty(followUp))
                    {
                        response += $"\n{followUp} (yes/no)";
                        waitingForYesNo = true;
                        pendingFollowUpTopic = topic;
                    }
                    else
                    {
                        askedFollowUp = false;
                    }

                    Console.WriteLine();
                    await ConsoleUI.TypeTextAsync(response);
                    conversationHistory.Add(response);

                    string audioFile = topic switch
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
                        ConsoleUI.PrintColoredText($"🔊 Now reading: {ToTitleCase(topic)}", ConsoleColor.Yellow);
                        await AudioManager.PlayAudioAsync(audioFile);
                    }
                }
                else
                {
                    string fallback = knowledgeBase.GetResponse(input);
                    await ConsoleUI.TypeTextAsync(fallback);
                    await AudioManager.PlayAudioAsync("unknown.wav");

                    if (userInterests.Count > 0)
                    {
                        string suggestion = userInterests[^1];
                        await ConsoleUI.TypeTextAsync($"You’ve asked about {ToTitleCase(suggestion)} before. Want to learn more about it?");
                    }

                    await AudioManager.PlayAudioAsync("help.wav");
                    DisplayMenu();
                }
            }
        }

        private void DisplayMenu()
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