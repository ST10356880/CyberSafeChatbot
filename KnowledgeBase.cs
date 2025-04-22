using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CyberSafeChatbot
{
    public class KnowledgeBase
    {
        private Dictionary<string, Topic> topics;

        public KnowledgeBase()
        {
            topics = new Dictionary<string, Topic>
            {
                ["password"] = new Topic(
                    new[] { "password", "strong password", "passwords", "secure password" },
                    "Use strong passwords with a mix of letters, numbers, and symbols. Avoid using personal info and don't reuse passwords across sites."
                ),
                ["phishing"] = new Topic(
                    new[] { "phishing", "email scam", "scam email", "fake email", "fraud" },
                    "Be cautious with unsolicited emails. Look out for suspicious links, urgent language, and sender address mismatches."
                ),
                ["browsing"] = new Topic(
                    new[] { "browsing", "safe browsing", "web safety", "internet safety", "secure browsing" },
                    "Always check for HTTPS in URLs. Avoid downloading files from unknown sources and keep your browser up-to-date."
                ),
                ["mobile"] = new Topic(
                    new[] { "mobile", "phone security", "app safety", "smartphone", "android", "ios" },
                    "Keep your phone OS and apps updated. Only install apps from trusted sources like the Google Play Store or Apple App Store."
                ),
                ["social"] = new Topic(
                    new[] { "social", "social media", "facebook", "instagram", "twitter", "privacy" },
                    "Limit the personal information you share on social media. Use privacy settings and be cautious when accepting friend requests."
                ),
                ["ransomware"] = new Topic(
                    new[] { "ransomware", "malware", "data lock", "hacking", "virus" },
                    "Regularly back up your data and never click on suspicious links or attachments. Install trusted antivirus software."
                ),
                ["wifi"] = new Topic(
                    new[] { "wifi", "wi-fi", "wireless", "network", "public wifi" },
                    "Avoid using public Wi-Fi for sensitive transactions. Use a VPN when connecting to unsecured networks."
                )
            };
        }

        public string GetResponse(string userInput)
        {
            foreach (var topic in topics.Values)
            {
                foreach (var keyword in topic.Keywords)
                {
                    if (userInput.Contains(keyword))
                    {
                        return topic.Response;
                    }
                }
            }

            // If no match is found
            return "Sorry, I couldn’t understand that. Try asking about topics like: passwords, phishing, browsing, mobile, social, ransomware, or Wi-Fi.";
        }

        private class Topic
        {
            public string[] Keywords { get; }
            public string Response { get; }

            public Topic(string[] keywords, string response)
            {
                Keywords = keywords;
                Response = response;
            }
        }
    }
}
