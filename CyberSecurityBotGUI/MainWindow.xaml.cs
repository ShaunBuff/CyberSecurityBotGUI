using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;

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

            PlayGreeting();
            ShowAscii();

            ChatBox.AppendText(
                "Bot: Welcome to the Cybersecurity Awareness Assistant!\n" +
                "I'm here to help you learn about online safety.\n\n" +
                "What is your name?\n\n");

            InputBox.Focus();
        }

        // ENTER KEY SUPPORT
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButton_Click(sender, e);
            }
        }

        // VOICE GREETING
        private void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Assets",
                    "greeting.wav");

                if (!File.Exists(path))
                {
                    ChatBox.AppendText("Bot: Voice file not found.\n\n");
                    return;
                }

                using (SoundPlayer player = new SoundPlayer(path))
                {
                    player.Load();
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                ChatBox.AppendText("Bot: Voice error: " + ex.Message + "\n\n");
            }
        }

        // ASCII ART
        private void ShowAscii()
        {
            ChatBox.AppendText(
@"========================================
      CYBERSECURITY AWARENESS BOT
========================================
        STAY SAFE ONLINE
========================================

");
        }

        // SEND BUTTON
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            ChatBox.AppendText("You: " + input + "\n");

            string lower = input.ToLower();

            // STEP 1 - GET USER NAME
            if (!nameCaptured)
            {
                userName = input;
                nameCaptured = true;

                ChatBox.AppendText(
                    $"Bot: Nice to meet you {userName}! What cybersecurity topic interests you most?\n\n");

                InputBox.Clear();
                ChatBox.ScrollToEnd();
                return;
            }

            // STEP 2 - GET FAVOURITE TOPIC
            if (!topicCaptured)
            {
                favouriteTopic = lower;
                topicCaptured = true;
                lastTopic = lower;

                ChatBox.AppendText(
                    $"Bot: Great! I'll remember that you're interested in {favouriteTopic}, {userName}.\n\n");

                InputBox.Clear();
                ChatBox.ScrollToEnd();
                return;
            }

            string response;

            // MEMORY RECALL
            if (lower.Contains("remember") ||
                lower.Contains("what do i like"))
            {
                response = $"You told me that you're interested in {favouriteTopic}, {userName}.";
            }

            // FOLLOW-UP CONVERSATION FLOW
            else if (lower.Contains("tell me more") ||
                     lower.Contains("another tip") ||
                     lower.Contains("explain more"))
            {
                var handler = bot.GetResponseHandler();
                response = handler(lastTopic);
            }

            else
            {
                var handler = bot.GetResponseHandler();
                response = handler(input);

                if (lower.Contains("phishing") ||
                    lower.Contains("password") ||
                    lower.Contains("privacy") ||
                    lower.Contains("malware") ||
                    lower.Contains("scam"))
                {
                    lastTopic = lower;
                }
            }

            ChatBox.AppendText("Bot: " + response + "\n\n");

            InputBox.Clear();
            ChatBox.ScrollToEnd();
            InputBox.Focus();
        }
    }
}