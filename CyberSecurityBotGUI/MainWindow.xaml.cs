using System;
using System.IO;
using System.Media;
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

            PlayGreeting();
            ShowAscii();

            ChatBox.AppendText("Bot: Hello! What is your name?\n\n");
        }

        // 🎵 VOICE GREETING
        private void PlayGreeting()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch
            {
                ChatBox.AppendText("Bot: (Voice failed to load)\n\n");
            }
        }

        // ASCII ART FIXED FOR WPF
        private void ShowAscii()
        {
            ChatBox.AppendText(
@"=================================
   CYBERSECURITY AWARENESS BOT
=================================
   Stay Safe Online Always
=================================
" + "\n\n");
        }

        // 🔘 BUTTON CLICK
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            ChatBox.AppendText("You: " + input + "\n");

            string lower = input.ToLower();

            // 👤 NAME
            if (!nameCaptured)
            {
                userName = input;
                nameCaptured = true;

                ChatBox.AppendText($"Bot: Nice to meet you {userName}! What topic interests you?\n\n");

                InputBox.Clear();
                return;
            }

            // 📌 TOPIC
            if (!topicCaptured)
            {
                favouriteTopic = lower;
                topicCaptured = true;
                lastTopic = lower;

                ChatBox.AppendText($"Bot: Got it {userName}, I’ll remember {favouriteTopic}.\n\n");

                InputBox.Clear();
                return;
            }

            string response;

            // 🔄 FOLLOW-UP FLOW
            if (lower.Contains("more") ||
                lower.Contains("explain") ||
                lower.Contains("another") ||
                lower.Contains("why") ||
                lower.Contains("how"))
            {
                response = bot.GetResponse(lastTopic);
            }
            else
            {
                var handler = bot.GetResponseHandler();
                response = handler(input);

                if (lower.Contains("phishing") ||
                    lower.Contains("password") ||
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

            ChatBox.AppendText("Bot: " + response + "\n\n");

            InputBox.Clear();
        }
    }
}