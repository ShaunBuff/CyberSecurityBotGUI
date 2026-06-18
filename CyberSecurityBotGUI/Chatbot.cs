using System;
using System.Collections.Generic;

namespace CyberSecurityBotGUI
{
    public class Chatbot
    {
        // Delegate
        public delegate string ResponseHandler(string input);

        private Random random = new Random();

        // Dictionary (Collections Requirement)
        private Dictionary<string, string> responses = new Dictionary<string, string>()
        {
            { "privacy", "Protect your personal data and regularly review your privacy settings." },
            { "malware", "Keep your antivirus updated and avoid downloading files from unknown sources." }
        };

        // Random phishing tips
        private List<string> phishingTips = new List<string>()
        {
            "Always check the sender's email address before clicking links.",
            "Be cautious of emails requesting personal information.",
            "Look for spelling mistakes and suspicious attachments.",
            "Verify unexpected messages with the organisation directly."
        };

        // Random password tips
        private List<string> passwordTips = new List<string>()
        {
            "Use strong, unique passwords for every account.",
            "Avoid using personal information in passwords.",
            "Consider using a password manager.",
            "Enable multi-factor authentication whenever possible."
        };

        // Random scam tips
        private List<string> scamTips = new List<string>()
        {
            "Scammers often create urgency to pressure victims.",
            "Never send money to someone you have not verified.",
            "Be suspicious of deals that seem too good to be true.",
            "Always verify requests for personal information."
        };

        public ResponseHandler GetResponseHandler()
        {
            return GetResponse;
        }

        public string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            // Sentiment Detection
            if (input.Contains("worried") || input.Contains("scared"))
            {
                return "It's understandable to feel worried. Here is a cybersecurity tip: Never click links from unknown senders and always verify requests for personal information.";
            }

            if (input.Contains("confused") || input.Contains("not sure"))
            {
                return "No problem. I'll explain things in a simpler way. Cybersecurity is all about protecting yourself and your information online.";
            }

            if (input.Contains("frustrated"))
            {
                return "I understand your frustration. Let's work through it together one step at a time.";
            }

            // Greeting
            if (input.Contains("hello") || input.Contains("hi"))
            {
                return "Hello! You can ask me about phishing, passwords, scams, privacy, or malware.";
            }

            // Random Responses
            if (input.Contains("phishing"))
            {
                return phishingTips[random.Next(phishingTips.Count)];
            }

            if (input.Contains("password"))
            {
                return passwordTips[random.Next(passwordTips.Count)];
            }

            if (input.Contains("scam"))
            {
                return scamTips[random.Next(scamTips.Count)];
            }

            // Dictionary Responses
            foreach (var item in responses)
            {
                if (input.Contains(item.Key))
                {
                    return item.Value;
                }
            }

            return "I’m not sure I understand. Try asking about phishing, passwords, scams, privacy, or malware.";
        }
    }
}