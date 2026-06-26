using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberSecurityBotGUI
{
    public partial class MainWindow : Window
    {
        private Chatbot bot = new Chatbot();
        private DatabaseHelper db = new DatabaseHelper();

        private bool waitingForReminder = false;
        private string pendingTask = "";

        private string userName = "";
        private bool nameCaptured = false;

        private List<string> activityLog = new List<string>();

        private List<(string Question, string A, string B, string C, string D, string Answer)> quiz;
        private int currentQuestion = 0;
        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            PlayGreeting();

            ChatBox.AppendText("Bot: Welcome to Cybersecurity Awareness Chatbot!\n");
            ChatBox.AppendText("Bot: What is your name?\n\n");

            quiz = new List<(string, string, string, string, string, string)>
            {
                ("What is phishing?", "Fake email scam", "Antivirus", "Firewall", "Password", "A"),
                ("Strong passwords should contain?", "Only numbers", "Only names", "Letters, numbers and symbols", "Birthdays", "C"),
                ("What does 2FA stand for?", "Two Factor Authentication", "Two File Access", "Fast Access", "Two Firewall Access", "A"),
                ("What is malware?", "Helpful software", "Harmful software", "Email", "Browser", "B"),
                ("What should you do with suspicious emails?", "Open them", "Reply", "Report them", "Ignore security", "C"),
                ("What is a firewall?", "Security barrier", "Game", "Virus", "Browser", "A"),
                ("Public WiFi is?", "Always safe", "Risky", "Encrypted", "Private", "B"),
                ("What protects against malware?", "Antivirus", "Music player", "Printer", "Mouse", "A"),
                ("Should you reuse passwords?", "Yes", "No", "Sometimes", "Always", "B"),
                ("What is social engineering?", "Manipulating people", "Programming", "Networking", "Encryption", "A")
            };

            LoadQuestion();
        }

        private void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Assets",
                    "greeting.wav");

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Load();
                    player.Play();
                }
            }
            catch
            {
            }
        }

        private void AddLog(string action)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] {action}";

            activityLog.Add(entry);

            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);

            if (LogListBox != null)
            {
                LogListBox.Items.Clear();

                foreach (string item in activityLog)
                    LogListBox.Items.Add(item);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return;

            ChatBox.AppendText("You: " + input + "\n");

            string lower = input.ToLower();
            string response;

            if (!nameCaptured)
            {
                userName = input;
                nameCaptured = true;

                response = $"Nice to meet you {userName}!";

                AddLog("User name captured");
            }
            else if (waitingForReminder)
            {
                DateTime? reminderDate = null;

                if (lower == "yes" || lower.Contains("yes"))
                {
                    reminderDate = DateTime.Now.AddDays(3);

                    db.AddTask(pendingTask,
                               "Added via chatbot",
                               reminderDate);

                    response = $"Got it! I'll remind you about '{pendingTask}' in 3 days.";

                    AddLog($"Reminder set for '{pendingTask}'");
                }
                else if (lower.Contains("day") || lower.Contains("remind"))
                {
                    int days = 3;

                    if (lower.Contains("1"))
                        days = 1;
                    else if (lower.Contains("2"))
                        days = 2;
                    else if (lower.Contains("3"))
                        days = 3;
                    else if (lower.Contains("5"))
                        days = 5;
                    else if (lower.Contains("7"))
                        days = 7;

                    reminderDate = DateTime.Now.AddDays(days);

                    db.AddTask(pendingTask,
                               "Added via chatbot",
                               reminderDate);

                    response = $"Got it! I'll remind you about '{pendingTask}' in {days} day(s).";

                    AddLog($"Reminder set for '{pendingTask}' in {days} days");
                }
                else if (lower == "no")
                {
                    db.AddTask(pendingTask,
                               "Added via chatbot",
                               null);

                    response = $"Task '{pendingTask}' saved without a reminder.";

                    AddLog($"Task saved without reminder: {pendingTask}");
                }
                else
                {
                    response = "Please answer Yes, No, or specify a reminder such as 'remind me in 3 days'.";
                }

                waitingForReminder = false;
                pendingTask = "";
            }
            else if (lower.Contains("add task"))
            {
                pendingTask = input.Replace("add task", "", StringComparison.OrdinalIgnoreCase).Trim();

                if (string.IsNullOrWhiteSpace(pendingTask))
                    pendingTask = "Unnamed Task";

                waitingForReminder = true;

                response = $"Task added: '{pendingTask}'. Would you like a reminder?";

                AddLog($"Task created: {pendingTask}");
            }
            else if (lower.Contains("activity log") ||
                     lower.Contains("what have you done"))
            {
                response = "Recent activity:\n";

                foreach (string log in activityLog)
                    response += "- " + log + "\n";
            }
            else
            {
                response = bot.GetResponse(input);
            }

            ChatBox.AppendText("Bot: " + response + "\n\n");

            InputBox.Clear();
            ChatBox.ScrollToEnd();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendButton_Click(sender, e);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string desc = TaskDescBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
                return;

            db.AddTask(title, desc, null);

            AddLog("Task added via GUI");

            MessageBox.Show("Task added successfully.");

            TaskTitleBox.Clear();
            TaskDescBox.Clear();

            LoadTasks();
        }

        private void LoadTasks_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
        }

        private void LoadTasks()
        {
            TaskListBox.Items.Clear();

            List<string> tasks = db.GetTasks();

            foreach (string task in tasks)
                TaskListBox.Items.Add(task);

            AddLog("Tasks loaded");
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem == null)
                return;

            string selected = TaskListBox.SelectedItem.ToString();

            try
            {
                int id = int.Parse(
                    selected.Split('|')[0]
                            .Replace("ID:", "")
                            .Trim());

                db.DeleteTask(id);

                AddLog("Task deleted");

                LoadTasks();
            }
            catch
            {
            }
        }

        private void LoadQuestion()
        {
            if (currentQuestion >= quiz.Count)
            {
                QuizQuestion.Text = "Quiz Complete!";

                QuizFeedback.Text =
                    $"Final Score: {score}/{quiz.Count}";

                AddLog($"Quiz completed. Score: {score}/{quiz.Count}");

                return;
            }

            var q = quiz[currentQuestion];

            QuizQuestion.Text = q.Question;

            OptionA.Content = "A) " + q.A;
            OptionB.Content = "B) " + q.B;
            OptionC.Content = "C) " + q.C;
            OptionD.Content = "D) " + q.D;

            QuizFeedback.Text = "";
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null)
                return;

            string selected =
                btn.Content.ToString().Substring(0, 1);

            var q = quiz[currentQuestion];

            if (selected == q.Answer)
            {
                score++;
                QuizFeedback.Text = "Correct!";
            }
            else
            {
                QuizFeedback.Text =
                    $"Incorrect. Correct answer: {q.Answer}";
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion++;
            LoadQuestion();
        }
    }
}
