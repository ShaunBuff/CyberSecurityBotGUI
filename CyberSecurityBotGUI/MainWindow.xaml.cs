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

            ChatBox.AppendText(
                "Bot: Welcome to the Cybersecurity Awareness Assistant!\n" +
                "I'm here to help you stay safe online.\n\n" +
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
                    MessageBox.Show(
                        "Voice file not found:\n" + path,
                        "File Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
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
                MessageBox.Show(
                    "Voice error:\n" + ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        // SEND BUTTON LOGIC
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            ChatBox.AppendText("You: " + input + "\n");

            string lower = input.ToLower();

            // STEP 1: NAME
            if (!nameCaptured)
            {
                userName = input;
                nameCaptured = true;

                ChatBox.AppendText($"Bot: Nice to meet you {userName}! What topic interests you?\n\n");

                InputBox.Clear();
                ChatBox.ScrollToEnd();
                return;
            }

            // STEP 2: TOPIC
            if (!topicCaptured)
            {
                favouriteTopic = lower;
                topicCaptured = true;
                lastTopic = lower;

                ChatBox.AppendText($"Bot: Great! I'll remember that you like {favouriteTopic}.\n\n");

                InputBox.Clear();
                ChatBox.ScrollToEnd();
                return;
            }

            string response;

            // MEMORY
            if (lower.Contains("remember") || lower.Contains("what do i like"))
            {
                response = $"You like {favouriteTopic}, {userName}.";
            }
            else if (lower.Contains("tell me more") ||
                     lower.Contains("another tip") ||
                     lower.Contains("explain more"))
            {
                response = bot.GetResponse(lastTopic);
            }
            else
            {
                response = bot.GetResponse(input);

                if (lower.Contains("phishing") ||
                    lower.Contains("password") ||
                    lower.Contains("scam") ||
                    lower.Contains("privacy") ||
                    lower.Contains("malware"))
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