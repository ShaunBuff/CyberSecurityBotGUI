using System;
using System.Collections.Generic;

namespace CyberSecurityBotGUI
{
    public class Chatbot
    {
        // 📦 COLLECTIONS (MARKS REQUIREMENT)
        private Dictionary<string, string> responses = new Dictionary<string, string>()
        {
            { "phishing", "Phishing is when attackers trick you into giving personal info." },
            { "password", "Use strong, unique passwords and a password manager." },
            { "privacy", "Protect your personal data and review privacy settings." },
            { "scam", "Scams often create urgency. Always verify before acting." },
            { "malware", "Avoid unknown downloads and keep antivirus updated." }
        };

        // 🎭 DELEGATE (MARKS REQUIREMENT)
        public delegate string ResponseHandler(string input);

        public ResponseHandler GetResponseHandler()
        {
            return GetResponse;
        }

        // 🧠 MAIN LOGIC
        public string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            // 😟 SENTIMENT (UPGRADED)
            if (input.Contains("worried") || input.Contains("scared"))
            {
                return "It's okay to feel that way. Let me help you.\n\n" +
                       "🔐 Tip: Never click unknown links in emails.\n" +
                       "Always verify the sender before responding.";
            }

            if (input.Contains("confused") || input.Contains("not sure"))
                return "No problem — I’ll explain it simply.";

            if (input.Contains("frustrated"))
                return "Let’s go step by step together.";

            // 👋 GREETING
            if (input.Contains("hello") || input.Contains("hi"))
                return "Hello! Ask me about phishing, passwords, privacy, scams or malware.";

            // 🔐 COLLECTION LOOKUP
            foreach (var item in responses)
            {
                if (input.Contains(item.Key))
                {
                    return item.Value;
                }
            }

            return "Try asking about phishing, passwords, privacy, scams or malware.";
        }
    }
}