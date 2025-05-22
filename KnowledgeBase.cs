using System;
using System.Collections.Generic;

namespace CyberSafeChatbot
{
    /// <summary>
    /// Stores the knowledge base of the chatbot, including topics, keywords, responses, and sentiment handling.
    /// </summary>
    public class KnowledgeBase
    {
        private static readonly Random random = new Random();

        // Dictionary mapping topics to a list of keywords for matching user input
        private readonly Dictionary<string, List<string>> topics = new(StringComparer.OrdinalIgnoreCase)
        {
            { "phishing", new() { "phishing", "fake email", "scam email", "fraudulent email" } },
            { "malware", new() { "malware", "virus", "trojan", "spyware", "ransomware" } },
            { "passwords", new() { "password", "passwords", "strong password", "password manager" } },
            { "social engineering", new() { "social engineering", "trick", "manipulate", "impersonate" } },
            { "safe browsing", new() { "safe browsing", "secure websites", "https", "safe internet" } },
            { "mobile security", new() { "mobile", "smartphone", "phone security", "app permissions" } },
            { "updates", new() { "update", "patch", "software update", "system update" } },
            { "wi-fi", new() { "wi-fi", "wifi", "public wifi", "secure connection" } },
            { "identity theft", new() { "identity theft", "stolen identity", "impersonation" } },
            { "cyberbullying", new() { "cyberbullying", "online bullying", "harassment", "abuse" } },
            { "support", new() { "help", "support", "report", "contact", "emergency" } }
        };

        // Dictionary mapping topics to a list of response-follow-up pairs
        private readonly Dictionary<string, List<(string Response, string FollowUp)>> responses = new(StringComparer.OrdinalIgnoreCase)
        {
            { "phishing", new() { ("Phishing attacks try to trick you into giving away personal info. Always verify links and emails.", "Do you want tips to recognize phishing emails?"),
                                  ("Avoid clicking suspicious links or downloading attachments from unknown senders.", "Have you ever received a suspicious email?") } },

            { "malware", new() { ("Malware is harmful software designed to damage or steal information. Use antivirus software to protect yourself.", "Want to know how to spot malware symptoms?"),
                                 ("Keep your devices secure by avoiding untrusted downloads and websites.", "Would you like guidance on malware removal?") } },

            { "passwords", new() { ("Strong passwords should include uppercase letters, numbers, and symbols.", "Need help creating a secure password?"),
                                   ("Using a password manager can help you store and generate complex passwords.", "Want to know which password managers are best?") } },

            { "support", new() { ("For help, contact your service provider or local authority. Always report suspicious activity.", "Do you need a list of emergency cyber helplines?"),
                                 ("Support is available. You're not alone. Reach out if you've been targeted.", "Would you like steps on how to report cybercrime?") } }
        };

        /// <summary>
        /// Gets all defined topics.
        /// </summary>
        public List<string> GetAllTopics() => new(topics.Keys);

        /// <summary>
        /// Attempts to match the user input to a known topic.
        /// </summary>
        public string GetTopicFromInput(string userInput)
        {
            foreach (var (topic, keywords) in topics)
            {
                if (keywords.Exists(keyword => userInput.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Matched Topic: {topic}"); // Debugging
                    return topic;
                }
            }
            return null; // No match found
        }

        /// <summary>
        /// Returns a random response-follow-up pair for the given topic.
        /// </summary>
        public (string Response, string FollowUp) GetRandomResponse(string topic)
        {
            if (responses.TryGetValue(topic, out var responseList) && responseList.Count > 0)
            {
                return responseList[random.Next(responseList.Count)];
            }
            return ("I'm still learning about that topic! Feel free to ask another question.", "");
        }

        /// <summary>
        /// Generates a sentiment-aware response by adjusting tone based on user emotion.
        /// </summary>
        public string GetSentimentResponse(string baseResponse, string sentiment)
        {
            return sentiment?.ToLower() switch
            {
                "positive" => $"{baseResponse} I'm glad you're feeling confident about your online safety!",
                "negative" => $"{baseResponse} It's okay to feel concerned. Cybersecurity can be tricky, but you're not alone.",
                "neutral" => $"{baseResponse} Let me know if you'd like more information on this.",
                "anxious" => $"{baseResponse} No worries. I'm here to guide you step-by-step.",
                "angry" => $"{baseResponse} I understand this can be frustrating. Let's work through it together.",
                "confused" => $"{baseResponse} Feel free to ask me anything you'd like me to explain further.",
                _ => baseResponse
            };
        }

        internal string? DetectSentiment(string input)
        {
            throw new NotImplementedException();
        }

        internal bool IsFollowUpQuestion(string input)
        {
            throw new NotImplementedException();
        }

        internal string GetResponse(string input)
        {
            throw new NotImplementedException();
        }
    }
}
