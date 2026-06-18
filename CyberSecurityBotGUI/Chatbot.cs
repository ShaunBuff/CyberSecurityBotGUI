using System;
using System.Collections.Generic;

// Added dictionary-based response system
// trigger CI run

namespace CyberSecurityBotGUI
{
    public class Chatbot
    {
        // 🎯 Delegate (REQUIRED FOR RUBRIC)
        public delegate string ResponseHandler(string input);
        private ResponseHandler handler;

        private Random random = new Random();

        // 📚 Dictionary+Lists (REQUIRED COLLECTIONS MARK)
        private Dictionary<string, List<string>> responses;

        public Chatbot()
        {
            responses = new Dictionary<string, List<string>>()
            {
                {
                    "phishing", new List<string>()
                    {
                        "Phishing is when attackers trick you into giving personal info. Always check email sources carefully.",
                        "Never click suspicious links in emails. Always verify the sender first.",
                        "Be cautious of urgent messages asking for passwords or banking details."
                    }
                },
                {
                    "password", new List<string>()
                    {
                        "Use strong, unique passwords for each account.",
                        "A good password includes letters, numbers, and symbols.",
                        "Consider using a password manager to stay secure."
                    }
                },
                {
                    "malware", new List<string>()
                    {
                        "Avoid downloading files from unknown sources.",
                        "Keep your antivirus software updated regularly.",
                        "Malware can steal data or damage your system."
                    }
                },
                {
                    "privacy", new List<string>()
                    {
                        "Review your privacy settings on social media.",
                        "Avoid oversharing personal information online.",
                        "Only share what is necessary and safe."
                    }
                },
                {
                    "scam", new List<string>()
                    {
                        "Scams often create urgency to trick users.",
                        "Always verify before sending money or information.",
                        "If it sounds too good to be true, it usually is."
                    }
                }
            };

            // 🧠 Delegate assignment
            handler = GetResponse;
        }

        // 🎯 Delegate entry method
        public string ProcessInput(string input)
        {
            return handler(input);
        }

        // 🧠 MAIN LOGIC
        public string GetResponse(string input)
        {
            input = input.ToLower();

            // 😟 SENTIMENT DETECTION (ENHANCED + FLOW)
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
            {
                return "It's completely understandable to feel that way.\n\n"
                     + GetRandom("scam")
                     + "\n\nWould you like another safety tip?";
            }

            if (input.Contains("confused") || input.Contains("not sure"))
            {
                return "No problem — I’ll explain it simply.\n\n"
                     + GetRandom("password")
                     + "\n\nWould you like more examples?";
            }

            if (input.Contains("frustrated") || input.Contains("angry"))
            {
                return "Let’s take it step by step.\n\n"
                     + GetRandom("privacy")
                     + "\n\nI’m here to help.";
            }

            // 👋 GREETING
            if (input.Contains("hello") || input.Contains("hi"))
            {
                return "Hello! Ask me about phishing, passwords, malware, privacy, or scams.";
            }

            // 🔐 KEYWORD DETECTION (COLLECTION BASED)
            foreach (var topic in responses.Keys)
            {
                if (input.Contains(topic))
                {
                    return GetRandom(topic);
                }
            }

            // ❌ DEFAULT RESPONSE
            return "I didn’t quite understand that. Try asking about phishing, passwords, malware, privacy, or scams.";
        }

        // 🎲 RANDOM RESPONSE (REQUIRED MARKS FEATURE)
        private string GetRandom(string topic)
        {
            List<string> options = responses[topic];
            int index = random.Next(options.Count);
            return options[index];
        }
    }
}