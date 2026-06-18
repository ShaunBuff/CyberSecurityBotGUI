using System.Windows;

namespace CyberSecurityBotGUI
{
    public partial class MainWindow : Window
    {
        private Chatbot bot = new Chatbot();

        private string userName = "";
        private string favouriteTopic = "";
        private string lastTopic = "";

        private bool nameCaptured = false;
        private bool topicCaptured = false;

        public MainWindow()
        {
            InitializeComponent();

            // 🎤 Voice already working (keep your existing WAV code if present)
            ChatBox.AppendText("Cybersecurity Bot: Hello! What is your name?\n\n");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            ChatBox.AppendText("You: " + input + "\n");

            string lower = input.ToLower();

            // 👤 NAME STEP
            if (!nameCaptured)
            {
                userName = input;
                nameCaptured = true;

                ChatBox.AppendText($"Bot: Nice to meet you {userName}! What cybersecurity topic interests you?\n\n");

                InputBox.Clear();
                ChatBox.ScrollToEnd();
                return;
            }

            // 📌 TOPIC STEP
            if (!topicCaptured)
            {
                favouriteTopic = lower;
                topicCaptured = true;
                lastTopic = lower;

                ChatBox.AppendText($"Bot: Got it {userName}, I’ll remember you like {favouriteTopic}.\n\n");

                InputBox.Clear();
                ChatBox.ScrollToEnd();
                return;
            }

            string response;

            // 🔄 CONVERSATION FLOW (FOLLOW-UPS)
            if (lower.Contains("tell me more") ||
                lower.Contains("another tip") ||
                lower.Contains("explain more") ||
                lower.Contains("why") ||
                lower.Contains("how"))
            {
                response = bot.ProcessInput(lastTopic);
            }
            else
            {
                response = bot.ProcessInput(input);

                if (lower.Contains("phishing") ||
                    lower.Contains("password") ||
                    lower.Contains("malware") ||
                    lower.Contains("privacy") ||
                    lower.Contains("scam"))
                {
                    lastTopic = lower;
                }
            }

            // 🧠 MEMORY
            if (lower.Contains("what do i like") || lower.Contains("remember"))
            {
                response = $"You like {favouriteTopic}, {userName}.";
            }

            ChatBox.AppendText($"Bot: {response}\n\n");

            InputBox.Clear();
            ChatBox.ScrollToEnd();
        }
    }
}