using System.Text.RegularExpressions;

namespace CyberSafeChatbot
{
    public class KnowledgeBase
    {
        // Random instance for selecting random responses
        private static readonly Random random = new Random();

        // Dictionary mapping topics to a list of associated keywords (case-insensitive)
        private readonly Dictionary<string, List<string>> topics = new(StringComparer.OrdinalIgnoreCase)
        {
            { "phishing", new() { "phishing", "fake email", "scam email", "fraudulent email" } },
            { "malware", new() { "malware", "virus", "trojan", "spyware", "ransomware" } },
            { "passwords", new() { "password", "strong password", "password manager" } },
            { "social engineering", new() { "social engineering", "trick", "manipulate", "impersonate" } },
            { "safe browsing", new() { "safe browsing", "secure websites", "https", "safe internet" } },
            { "mobile security", new() { "mobile", "smartphone", "phone security", "app permissions" } },
            { "updates", new() { "update", "patch", "software update", "system update" } },
            { "wi-fi", new() { "wi-fi", "wifi", "public wifi", "secure wifi", "wireless security" } },
            { "identity theft", new() { "identity theft", "stolen identity", "impersonation" } },
            { "cyberbullying", new() { "cyberbullying", "online bullying", "harassment", "abuse" } },
            { "support", new() { "help", "support", "report", "contact", "emergency" } }
        };

        // Dictionary storing response templates and follow-up questions for each topic
        private readonly Dictionary<string, List<(string Response, string FollowUp)>> responses = new(StringComparer.OrdinalIgnoreCase)
        {
            { "phishing", new() {
                ("Phishing attacks try to trick you into giving away personal info. Always verify links and emails.", "Do you want tips to recognise phishing emails?"),
                ("Avoid clicking suspicious links or downloading attachments from unknown senders.", "Have you ever received a suspicious email?")
            }},
            { "malware", new() {
                ("Malware is harmful software. Use antivirus tools and keep systems updated.", "Want to know how to spot malware?"),
                ("Avoid downloading from untrusted sources.", "Would you like guidance on malware removal?")
            }},
            { "passwords", new() {
                ("Use strong passwords with symbols and numbers.", "Need help creating a secure password?"),
                ("Password managers are helpful for storing complex passwords.", "Want to know good password managers?")
            }},
            { "wi-fi", new() {
                ("Public Wi-Fi is risky. Avoid banking or logging into sensitive accounts on it.", "Would you like safe public Wi-Fi tips?"),
                ("Secure your home Wi-Fi with WPA3 and a strong password.", "Need help securing your router?")
            }},
            { "support", new() {
                ("For help, contact your service provider or report to cyber authorities.", "Want emergency cyber helpline numbers?"),
                ("You're not alone. Help is available if you've been targeted.", "Want to learn how to report cybercrime?")
            }}
        };

        // Dictionary to match words to sentiment categories
        private readonly Dictionary<string, List<string>> sentimentWords = new()
        {
            { "positive", new() { "good", "happy", "safe", "secure", "great", "confident" } },
            { "negative", new() { "bad", "scared", "unsafe", "hacked", "problem", "worried" } },
            { "anxious", new() { "nervous", "anxious", "concerned", "stress", "afraid" } },
            { "angry", new() { "angry", "frustrated", "upset", "annoyed" } },
            { "confused", new() { "confused", "unsure", "uncertain", "lost" } }
        };

        // Common follow-up question starters to detect continued dialogue
        private readonly List<string> followUpPhrases = new()
        {
            "tell me more", "can you elaborate", "what about", "how does that work",
            "why", "how", "what if", "can i", "should i", "do i need to"
        };

        // Returns a list of all defined topics
        public List<string> GetAllTopics() => new(topics.Keys);

        // Identifies the topic from user input by checking for keyword matches
        public string GetTopicFromInput(string userInput)
        {
            var normalised = Normalise(userInput);
            foreach (var (topic, keywords) in topics)
            {
                if (keywords.Any(keyword => normalised.Contains(keyword)))
                {
                    Console.WriteLine($"Matched Topic: {topic}"); // Debug info
                    return topic;
                }
            }
            return null;
        }

        // Selects a random response for a given topic
        public (string Response, string FollowUp) GetRandomResponse(string topic)
        {
            if (responses.TryGetValue(topic, out var responseList) && responseList.Count > 0)
            {
                return responseList[random.Next(responseList.Count)];
            }
            return ("I'm still learning about that topic! Feel free to ask another question.", "");
        }

        // Adds a sentiment-based suffix to a response message
        public string GetSentimentResponse(string baseResponse, string sentiment)
        {
            return sentiment switch
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

        // Determines sentiment from user input
        public string DetectSentiment(string input)
        {
            var lowerInput = Normalise(input);
            foreach (var (sentiment, words) in sentimentWords)
            {
                if (words.Any(word => lowerInput.Contains(word))) return sentiment;
            }
            return "neutral";
        }

        // Checks if the input seems like a follow-up question
        public bool IsFollowUpQuestion(string input)
        {
            var lower = Normalise(input);
            return followUpPhrases.Any(phrase => lower.StartsWith(phrase));
        }

        // Returns a generic response when no topic is matched
        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Could you clarify what you mean? I want to help you as best I can!";

            var generalResponses = new List<string>
            {
                "Cybersecurity is essential! What aspect of online safety would you like to learn more about?",
                "I can help with topics like phishing, strong passwords, or safe browsing. What would you like to ask?",
                "That's an interesting topic! Could you provide more details so I can assist better?",
                "I'm here to help you stay safe online. Feel free to ask about any cybersecurity concerns you have!"
            };

            return generalResponses[random.Next(generalResponses.Count)];
        }

        // Converts input to lowercase, strips punctuation, replaces hyphens with spaces, and trims
        private string Normalise(string input)
        {
            return Regex.Replace(input.ToLower(), @"[^a-z0-9\s]", " ").Replace("-", " ").Trim();
        }
    }
}
